using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Slay_Your_Vegetables
{
    public enum LocalGameState { MainMenu, Playing, Options, GameOver }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public static Microsoft.Xna.Framework.Content.ContentManager ContentManager;
        private SpriteBatch _spriteBatch;
        
        private LevelManage _levelManage;
        private SpawnManage _spawnManage;
        private UIManager _uiManager;
        private Player _player;

        public List<Bullet> activeBullets = new List<Bullet>();
        private List<FireParticle> fireParticles = new List<FireParticle>();
        private List<WhiskUltimate> activeWhiskUltimates = new List<WhiskUltimate>();

        private LocalGameState _currentState = LocalGameState.MainMenu;
        private Rectangle playButton, optionsButton, exitButton;
        private int currentLevelIndex = 1;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * 0.8f);
            _graphics.PreferredBackBufferHeight = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.8f);
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            _levelManage = new LevelManage();
            _levelManage.LoadLevel(currentLevelIndex);

            int centerX = (1920 / 2) - 200;
            playButton = new Rectangle(centerX, 300, 400, 90);
            optionsButton = new Rectangle(centerX, 410, 400, 90);
            exitButton = new Rectangle(centerX, 520, 400, 90);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentManager = this.Content;

            AssetManager.LoadAllContent(ContentManager, GraphicsDevice);
            _uiManager = new UIManager(_spriteBatch, this);
            _spawnManage = new SpawnManage(_levelManage, Enemy.textures);

            Texture2D chefTex = null;
            try { chefTex = Content.Load<Texture2D>("chefWalk_00000"); } catch { }
            if (chefTex == null) { chefTex = AssetManager.Pixel; }

            _player = new Player(chefTex, new Vector2(300, 430));
            _player.ChangeWeapon(0);
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();

            if (InputManager.IsKeyPressed(Keys.Escape)) Exit();

            switch (_currentState)
            {
                case LocalGameState.MainMenu:
                    HandleMenuLogic();
                    break;

                case LocalGameState.Playing:
                    UpdateGameplay(gameTime);
                    break;

                case LocalGameState.Options:
                    if (InputManager.IsKeyPressed(Keys.Back)) _currentState = LocalGameState.MainMenu;
                    break;

                case LocalGameState.GameOver:
                    if (InputManager.IsKeyPressed(Keys.R)) RestartLevel();
                    if (InputManager.IsKeyPressed(Keys.Back)) { currentLevelIndex = 1; _currentState = LocalGameState.MainMenu; }
                    break;
            }

            base.Update(gameTime);
        }

        private void HandleMenuLogic()
        {
            if (InputManager.IsLeftMouseClicked())
            {
                float scaleX = (float)GraphicsDevice.Viewport.Width / 1920f;
                float scaleY = (float)GraphicsDevice.Viewport.Height / 1080f;
                Point mousePos = InputManager.GetMousePosition(scaleX, scaleY);

                if (playButton.Contains(mousePos)) RestartLevel();
                else if (optionsButton.Contains(mousePos)) _currentState = LocalGameState.Options;
                else if (exitButton.Contains(mousePos)) Exit();
            }
        }

        private void UpdateGameplay(GameTime gameTime)
        {
            if (_player.CurrentHP <= 0) { _currentState = LocalGameState.GameOver; return; }

            if (CheckLevelComplete())
            {
                currentLevelIndex = currentLevelIndex > 4 ? 1 : currentLevelIndex + 1;
                LoadNewLevel(currentLevelIndex);
                if (currentLevelIndex == 1) _currentState = LocalGameState.MainMenu;
                return;
            }

            _player.Update(gameTime);
            _spawnManage.Update(gameTime, _player);
            HandleCombatInput();

            for (int i = activeBullets.Count - 1; i >= 0; i--)
            {
                activeBullets[i].Update(gameTime, _spawnManage.GetActiveEnemies());
                if (!activeBullets[i].IsActive) activeBullets.RemoveAt(i);
            }

            for (int i = activeWhiskUltimates.Count - 1; i >= 0; i--)
            {
                activeWhiskUltimates[i].Update(gameTime, _spawnManage.GetActiveEnemies());
                if (!activeWhiskUltimates[i].IsActive) activeWhiskUltimates.RemoveAt(i);
            }

            for (int i = fireParticles.Count - 1; i >= 0; i--)
            {
                fireParticles[i].Update();
                if (fireParticles[i].Life <= 0) fireParticles.RemoveAt(i);
            }
        }

        private void HandleCombatInput()
        {
            if (InputManager.IsKeyPressed(Keys.Q)) _player.ChangeWeapon(0);
            if (InputManager.IsKeyPressed(Keys.E)) _player.ChangeWeapon(1);
            if (InputManager.IsKeyPressed(Keys.R)) _player.ChangeWeapon(2);

            int currentLine = GetPlayerLine();

            // SİLAH ATEŞLEME
            if (InputManager.IsKeyPressed(Keys.Space) && _player.CurrentMana >= 15)
            {
                Texture2D currentTex = _player.CurrentWeaponIndex switch
                {
                    1 => AssetManager.TorchTex,
                    2 => AssetManager.WhiskTex,
                    _ => AssetManager.KnifeTex
                };
                
                _player.CurrentMana -= 15f;
                Vector2 spawnPos = new Vector2(_player.Position.X + _player.Width, _player.Position.Y + (_player.Height / 3));
                activeBullets.Add(new Bullet(currentTex, spawnPos, _player.ActiveWeapon, currentLine));
                
                if (_player.CurrentWeaponIndex == 0) _player.AddKnifeAttack();
                else if (_player.CurrentWeaponIndex == 1) _player.AddBlowtorchAttack();
                else _player.AddWhiskAttack();
            }

            // SİLİNEN ULTI KODLARI 
            if (InputManager.IsKeyPressed(Keys.X))
            {
                if (_player.CurrentWeaponIndex == 0 && _player.KnifeCount >= 10) 
                { 
                    activeBullets.Add(new Bullet(AssetManager.AxTex, new Vector2(_player.Position.X, _player.Position.Y), new Axe(), currentLine)); 
                    _player.ResetKnifeCount(); 
                }
                else if (_player.CurrentWeaponIndex == 1 && _player.BlowtorchCount >= 10) 
                { 
                    System.Random rnd = new System.Random();
                    for(int i = 0; i < 40; i++) 
                    {
                        int randomLine = rnd.Next(0, 4); 
                        fireParticles.Add(new FireParticle(new Vector2(rnd.Next(500, 1900), 0), randomLine));
                    }
                    _player.ActiveWeapon.Ultimate(_spawnManage.GetActiveEnemies());
                    _player.ResetBlowtorchCount(); 
                }
                else if (_player.CurrentWeaponIndex == 2 && _player.WhiskCount >= 10)
                {
                    System.Random rnd = new System.Random();
                    float pX = _player.Position.X; 
                    activeWhiskUltimates.Add(new WhiskUltimate(new Vector2(pX, 160f), 0, AssetManager.GirdapTexs, rnd));
                    activeWhiskUltimates.Add(new WhiskUltimate(new Vector2(pX, 345f), 1, AssetManager.GirdapTexs, rnd));
                    activeWhiskUltimates.Add(new WhiskUltimate(new Vector2(pX, 530f), 2, AssetManager.GirdapTexs, rnd));
                    activeWhiskUltimates.Add(new WhiskUltimate(new Vector2(pX, 715f), 3, AssetManager.GirdapTexs, rnd));
                    _player.ResetWhiskCount();
                }
            }
        }

        private int GetPlayerLine()
        {
            if (_player.Position.Y < 250) return 0;
            if (_player.Position.Y < 430) return 1;
            if (_player.Position.Y < 615) return 2;
            return 3;
        }

        private bool CheckLevelComplete()
        {
            foreach (var goal in _levelManage.CurrentLevel.Goals)
            {
                if (_levelManage.CurrentLevel.DefeatedCounters[goal.Key] < goal.Value) return false;
            }
            return true;
        }

        private void LoadNewLevel(int levelNum)
        {
            _levelManage.LoadLevel(levelNum);
            _spawnManage = new SpawnManage(_levelManage, Enemy.textures);
            activeBullets.Clear();
            activeWhiskUltimates.Clear();
            _player.Position = new Vector2(300, 430);
            _player.CurrentHP = _player.MaxHP;
        }

        private void RestartLevel()
        {
            LoadNewLevel(currentLevelIndex);
            _currentState = LocalGameState.Playing;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Matrix scale = Matrix.CreateScale((float)GraphicsDevice.Viewport.Width / 1920f, (float)GraphicsDevice.Viewport.Height / 1080f, 1.0f);

            _spriteBatch.Begin(transformMatrix: scale);

            if (_currentState == LocalGameState.MainMenu) _uiManager.DrawMainMenu(playButton, optionsButton, exitButton);
            else if (_currentState == LocalGameState.GameOver) _uiManager.DrawGameOver();
            else if (_currentState == LocalGameState.Playing)
            {
                _spriteBatch.Draw(AssetManager.Line1, new Rectangle(500, 120, 1450, 170), Color.Beige);
                _spriteBatch.Draw(AssetManager.Line2, new Rectangle(500, 305, 1450, 170), Color.Beige);
                _spriteBatch.Draw(AssetManager.Line3, new Rectangle(500, 490, 1450, 170), Color.Beige);
                _spriteBatch.Draw(AssetManager.Line4, new Rectangle(500, 675, 1450, 170), Color.Beige);

                foreach (var wp in activeWhiskUltimates) wp.Draw(_spriteBatch);
                _player.Draw(_spriteBatch);
                foreach (var enemy in _spawnManage.GetActiveEnemies()) enemy.Draw(_spriteBatch);
                foreach (var bullet in activeBullets) bullet.Draw(_spriteBatch);
                
                // ATEŞ EFEKTİ
                foreach (var particle in fireParticles) particle.Draw(_spriteBatch, AssetManager.FireTex);

                // HUD ÇİZİMİ  
                _uiManager.DrawGameHUD(_player, _levelManage, currentLevelIndex);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}