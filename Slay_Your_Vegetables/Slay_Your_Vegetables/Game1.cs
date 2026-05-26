using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Slay_Your_Vegetables
{
    public enum LocalGameState { MainMenu, Playing, Options, GameOver }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public static Microsoft.Xna.Framework.Content.ContentManager ContentManager;
        private SpriteBatch _spriteBatch;
        
        // Oyun Yönetimi
        private Song _backgroundMusic;
        private LevelManage _levelManage;
        private SpawnManage _spawnManage;
        private UIManager _uiManager;
        private Player _player;
        private Texture2D kitchenT, recipeT;
        private SpriteFont font, titleF;

        // Oyun Listeleri
        public List<Bullet> activeBullets = new List<Bullet>();
        private List<FireParticle> fireParticles = new List<FireParticle>();
        private List<WhiskUltimate> activeWhiskUltimates = new List<WhiskUltimate>();
        
        // Oyun Durumu
        private LocalGameState _currentState = LocalGameState.MainMenu;
        private Rectangle playButton, optionsButton, exitButton;
        private int CurrentLevel = 1;

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
            int btnWidth = 500;
            int btnHeight = 110;
            int centerX = (1920 / 2) - (btnWidth / 2);

            playButton = new Rectangle(centerX, 320, btnWidth, btnHeight);
            optionsButton = new Rectangle(centerX, 460, btnWidth, btnHeight);
            exitButton = new Rectangle(centerX, 600, btnWidth, btnHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentManager = this.Content;
            
            font = Content.Load<SpriteFont>("Fonts/font1");
            titleF = Content.Load<SpriteFont>("Fonts/titleF");
            recipeT = Content.Load<Texture2D>("Recipe");
            kitchenT = Content.Load<Texture2D>("kitchenT");

            AssetManager.LoadAllContent(ContentManager, GraphicsDevice);

            _backgroundMusic = Content.Load<Song>("Music");
            MediaPlayer.Play(_backgroundMusic);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.5f;

            _uiManager = new UIManager(_spriteBatch, this);
            _levelManage = new LevelManage(GraphicsDevice, font, titleF, recipeT);
            _spawnManage = new SpawnManage(_levelManage, Enemy.textures);
            
            _levelManage.LoadLevel(CurrentLevel);

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
                case LocalGameState.MainMenu: HandleMenuLogic(); break;
                case LocalGameState.Playing: 
                    if (_levelManage.CurrentLevel != null && _levelManage.CurrentLevel.IsTutorialActive)
                        _levelManage.CurrentLevel.Update(gameTime);
                    else
                        UpdateGameplay(gameTime);
                    break;
                case LocalGameState.Options:
                    if (InputManager.IsKeyPressed(Keys.Back)) _currentState = LocalGameState.MainMenu;
                    break;
                case LocalGameState.GameOver:
                    if (InputManager.IsKeyPressed(Keys.R)) RestartLevel();
                    if (InputManager.IsKeyPressed(Keys.Back)) { CurrentLevel = 1; _currentState = LocalGameState.MainMenu; }
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
                CurrentLevel = CurrentLevel > 4 ? 1 : CurrentLevel + 1;
                LoadNewLevel(CurrentLevel);
                if (CurrentLevel == 1) _currentState = LocalGameState.MainMenu;
                return;
            }

            _player.Update(gameTime);
            _spawnManage.Update(gameTime, _player);
            HandleCombatInput();

            // Güncelleme Döngüleri
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

            if (InputManager.IsKeyPressed(Keys.X))
            {
                System.Random rnd = new System.Random();
                if (_player.CurrentWeaponIndex == 0 && _player.KnifeCount >= 10)
                {
                    activeBullets.Add(new Bullet(AssetManager.AxTex, new Vector2(_player.Position.X, _player.Position.Y), new Axe(), currentLine));
                    _player.ResetKnifeCount();
                }
                else if (_player.CurrentWeaponIndex == 1 && _player.BlowtorchCount >= 10)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        int randomLine = rnd.Next(0, 4);
                        fireParticles.Add(new FireParticle(new Vector2(rnd.Next(500, 1900), 0), randomLine));
                    }
                    _player.ActiveWeapon.Ultimate(_spawnManage.GetActiveEnemies());
                    _player.ResetBlowtorchCount();
                }
                else if (_player.CurrentWeaponIndex == 2 && _player.WhiskCount >= 10)
                {
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
            LoadNewLevel(CurrentLevel);
            _currentState = LocalGameState.Playing;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Matrix scale = Matrix.CreateScale((float)GraphicsDevice.Viewport.Width / 1920f, (float)GraphicsDevice.Viewport.Height / 1080f, 1.0f);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, scale);
            
            _spriteBatch.Draw(kitchenT, new Rectangle(0, 0, 2448, 2448), Color.White); 
            
            if (_currentState == LocalGameState.MainMenu) 
                _uiManager.DrawMainMenu(playButton, optionsButton, exitButton);
            else if (_currentState == LocalGameState.GameOver) 
                _uiManager.DrawGameOver();
            else if (_currentState == LocalGameState.Playing)
            {
                Color sutluKahve = new Color(200, 160, 120);

                _spriteBatch.Draw(AssetManager.Line1, new Rectangle(500, 120, 1450, 170), sutluKahve); 
                _spriteBatch.Draw(AssetManager.Line2, new Rectangle(500, 305, 1450, 170), sutluKahve);
                _spriteBatch.Draw(AssetManager.Line3, new Rectangle(500, 490, 1450, 170), sutluKahve);
                _spriteBatch.Draw(AssetManager.Line4, new Rectangle(500, 675, 1450, 170), sutluKahve);

                foreach (var wp in activeWhiskUltimates) wp.Draw(_spriteBatch);
                _player.Draw(_spriteBatch);
                foreach (var enemy in _spawnManage.GetActiveEnemies()) enemy.Draw(_spriteBatch);
                foreach (var bullet in activeBullets) bullet.Draw(_spriteBatch);
                foreach (var particle in fireParticles) particle.Draw(_spriteBatch, AssetManager.FireTex);

                _uiManager.DrawGameHUD(_player, _levelManage, CurrentLevel);

                if (_levelManage.CurrentLevel != null && _levelManage.CurrentLevel.IsTutorialActive)
                {
                    _levelManage.CurrentLevel.Draw(_spriteBatch);
                }
            }
            
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}