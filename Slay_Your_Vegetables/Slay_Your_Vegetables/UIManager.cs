using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Slay_Your_Vegetables
{
    public class UIManager
    {
        private SpriteBatch _sb;
        private Game1 _game;

        public UIManager(SpriteBatch spriteBatch, Game1 game)
        {
            _sb = spriteBatch;
            _game = game;
        }

        public void DrawMainMenu(Rectangle playBtn, Rectangle optBtn, Rectangle exitBtn)
        {
            if (AssetManager.GameFont != null)
            {
                string title = "SLAY YOUR VEGETABLES";
                Vector2 size = AssetManager.GameFont.MeasureString(title) * 2f;
                _sb.DrawString(AssetManager.GameFont, title, new Vector2((1920 / 2) - (size.X / 2), 80), Color.Orange, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            }

            DrawButton(playBtn, "PLAY", Color.Green);
            DrawButton(optBtn, "OPTIONS", Color.Yellow);
            DrawButton(exitBtn, "EXIT", Color.Red);
        }

        public void DrawGameOver()
        {
            _game.GraphicsDevice.Clear(Color.DarkRed);
            if (AssetManager.GameFont != null)
            {
                string text1 = "YOU DIED!";
                Vector2 size1 = AssetManager.GameFont.MeasureString(text1) * 2f;
                _sb.DrawString(AssetManager.GameFont, text1, new Vector2(1920 / 2 - size1.X / 2, 300), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                _sb.DrawString(AssetManager.GameFont, "Press 'R' to Try Again", new Vector2(750, 450), Color.Yellow);
                _sb.DrawString(AssetManager.GameFont, "Press 'Backspace' to Main Menu", new Vector2(650, 500), Color.LightGray);
            }
        }

        private void DrawButton(Rectangle rect, string text, Color color)
        {
            float scaleX = (float)_game.GraphicsDevice.Viewport.Width / 1920f;
            float scaleY = (float)_game.GraphicsDevice.Viewport.Height / 1080f;
            Point mousePos = InputManager.GetMousePosition(scaleX, scaleY);

            bool isHovered = rect.Contains(mousePos);
            Color renderColor = isHovered ? Color.Lerp(color, Color.White, 0.3f) : color;
            
            _sb.Draw(AssetManager.Pixel, rect, renderColor);
            
            if (AssetManager.GameFont != null)
            {
                Vector2 textSize = AssetManager.GameFont.MeasureString(text);
                _sb.DrawString(AssetManager.GameFont, text, new Vector2(rect.X + (rect.Width - textSize.X) / 2, rect.Y + (rect.Height - textSize.Y) / 2), Color.Black);
            }
        }

        // UNUTTUĞUM OYUN İÇİ ARAYÜZÜ METODU
        public void DrawGameHUD(Player player, LevelManage levelManage, int currentLevelIndex)
        {
            if (AssetManager.GameFont != null)
            {
                string levelStr = $"LEVEL {currentLevelIndex} : {GetLevelName(currentLevelIndex)}";
                Vector2 strSize = AssetManager.GameFont.MeasureString(levelStr) * 1.5f;
                int boxWidth = (int)strSize.X + 60;
                int boxHeight = (int)strSize.Y + 20;
                int boxX = 960 - (boxWidth / 2);
                int boxY = 20;
                
                if (AssetManager.Pixel != null)
                {
                    _sb.Draw(AssetManager.Pixel, new Rectangle(boxX - 4, boxY - 4, boxWidth + 8, boxHeight + 8), Color.DarkSlateGray);
                    _sb.Draw(AssetManager.Pixel, new Rectangle(boxX, boxY, boxWidth, boxHeight), Color.Black * 0.75f);
                }
                _sb.DrawString(AssetManager.GameFont, levelStr, new Vector2(960 - (strSize.X / 2), boxY + 10), Color.Gold, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            }

            int goalCount = levelManage.CurrentLevel.Goals.Count;
            int dynamicWidth = (goalCount * 180) + 20;
            if (AssetManager.Requirements != null)
                _sb.Draw(AssetManager.Requirements, new Rectangle(500, 925, dynamicWidth, 110), Color.LightGray); 
            
            int startX = 510;
            int startY = 935;
            foreach (var goal in levelManage.CurrentLevel.Goals)
            {
                int id = goal.Key;
                int targetCount = goal.Value;
                int currentCount = levelManage.CurrentLevel.DefeatedCounters[id];

                if (Enemy.textures.ContainsKey(id))
                    _sb.Draw(Enemy.textures[id], new Rectangle(startX, startY, 90, 90), Color.White);

                Texture2D wTex = GetWeaponForEnemy(id);
                if (wTex != null)
                    _sb.Draw(wTex, new Rectangle(startX + 95, startY + 5, 45, 45), Color.White);

                string counterText = $"{currentCount}/{targetCount}";
                if(AssetManager.GameFont != null)
                {
                    _sb.DrawString(AssetManager.GameFont, counterText, new Vector2(startX + 95, startY + 55), Color.Black, 0f, Vector2.Zero, 1.2f, SpriteEffects.None, 0f);
                    if (currentCount >= targetCount)
                    {
                        if (AssetManager.Checkmark != null) _sb.Draw(AssetManager.Checkmark, new Rectangle(startX + 145, startY + 50, 30, 30), Color.White);
                        else _sb.DrawString(AssetManager.GameFont, "OK", new Vector2(startX + 145, startY + 55), Color.LimeGreen, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
                    }
                }
                startX += 180;
            }

            if (player != null)
            {
                int healthWidth = (int)((player.CurrentHP / (float)player.MaxHP) * 150);
                int staminaWidth = (int)((player.CurrentStamina / (float)player.MaxStamina) * 150);
                int manaWidth = (int)((player.CurrentMana / (float)player.MaxMana) * 150);

                if (AssetManager.Pixel != null)
                {
                    _sb.Draw(AssetManager.Pixel, new Rectangle(10, 20, healthWidth, 20), Color.Green);
                    _sb.Draw(AssetManager.Pixel, new Rectangle(10, 45, staminaWidth, 20), Color.Yellow);
                    _sb.Draw(AssetManager.Pixel, new Rectangle(10, 70, manaWidth, 20), Color.Blue);
                }

                int[] counts = { player.KnifeCount, player.BlowtorchCount, player.WhiskCount };
                Texture2D[] texs = { AssetManager.KnifeTex, AssetManager.TorchTex, AssetManager.WhiskTex };
                Rectangle[] rects = { new Rectangle(50, 930, 100, 100), new Rectangle(190, 930, 100, 100), new Rectangle(330, 930, 100, 100) };

                for (int i = 0; i < 3; i++)
                {
                    bool isReady = counts[i] >= 10;
                    bool isSelected = player.CurrentWeaponIndex == i;
                    Color borderColor = isReady ? Color.Red : (isSelected ? Color.LimeGreen : Color.Transparent);

                    _sb.Draw(AssetManager.Pixel, new Rectangle(rects[i].X - 5, rects[i].Y - 5, 110, 110), borderColor);
                    _sb.Draw(AssetManager.Pixel, rects[i], Color.DarkGray);
                    if (texs[i] != null) _sb.Draw(texs[i], rects[i], Color.White);

                    int barHeight = Math.Min(counts[i] * 10, 100); 
                    _sb.Draw(AssetManager.Pixel, new Rectangle(rects[i].X - 20, rects[i].Y + (100 - barHeight), 10, barHeight), isReady ? Color.Gold : Color.Orange);
                }
            }

            int guideX = 1400; int guideY = 850; int guideWidth = 550; int guideHeight = 230; 
            if (AssetManager.WeaponGuide != null && AssetManager.WeaponGuide != AssetManager.Pixel)
                _sb.Draw(AssetManager.WeaponGuide, new Rectangle(guideX, guideY, guideWidth, guideHeight), Color.White);

            int knifeRowY = guideY + 50; int knifeVegX = guideX + 130; int knifeWeaponX = guideX + 300; 
            int[] knifeEnemies = { 0, 1, 2, 4, 11, 13, 14 };
            int currentX = knifeVegX;
            foreach (int e in knifeEnemies)
            {
                if (Enemy.textures.ContainsKey(e)) _sb.Draw(Enemy.textures[e], new Rectangle(currentX, knifeRowY + 5, 30, 30), Color.White);
                currentX += 19; 
            }
            if (AssetManager.KnifeTex != null) _sb.Draw(AssetManager.KnifeTex, new Rectangle(knifeWeaponX, knifeRowY, 40, 40), Color.White);

            int torchRowY = guideY + 90; int torchVegX = guideX + 120; int torchWeaponX = guideX + 300; 
            int[] torchEnemies = { 3, 5, 6, 9, 10, 12 };
            currentX = torchVegX;
            foreach (int e in torchEnemies)
            {
                if (Enemy.textures.ContainsKey(e)) _sb.Draw(Enemy.textures[e], new Rectangle(currentX, torchRowY + 5, 30, 30), Color.White);
                currentX += 24; 
            }
            if (AssetManager.TorchTex != null) _sb.Draw(AssetManager.TorchTex, new Rectangle(torchWeaponX, torchRowY, 40, 40), Color.White);

            int whiskRowY = guideY + 130; int whiskVegX = guideX + 120; int whiskWeaponX = guideX + 300; 
            int[] whiskEnemies = { 7, 8 };
            currentX = whiskVegX;
            foreach (int e in whiskEnemies)
            {
                if (Enemy.textures.ContainsKey(e)) _sb.Draw(Enemy.textures[e], new Rectangle(currentX, whiskRowY + 5, 30, 30), Color.White);
                currentX += 32; 
            }
            if (AssetManager.WhiskTex != null) _sb.Draw(AssetManager.WhiskTex, new Rectangle(whiskWeaponX, whiskRowY, 40, 40), Color.White);
        }

        private string GetLevelName(int levelIndex)
        {
            switch (levelIndex)
            {
                case 1: return "SALAD";
                case 2: return "HAMBURGER";
                case 3: return "ALI NAZIK";
                case 4: return "SOUP";
                case 5: return "DESSERT";
                default: return "MENU";
            }
        }

        private Texture2D GetWeaponForEnemy(int id)
        {
            if (id == 0 || id == 1 || id == 2 || id == 4 || id == 11 || id == 13 || id == 14) return AssetManager.KnifeTex;
            if (id == 3 || id == 5 || id == 6 || id == 9 || id == 10 || id == 12) return AssetManager.TorchTex;
            if (id == 7 || id == 8) return AssetManager.WhiskTex;
            return null;
        }
    }
}