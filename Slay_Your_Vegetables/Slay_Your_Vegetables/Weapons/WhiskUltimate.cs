using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Slay_Your_Vegetables
{
    public class WhiskUltimate
    {
        public Vector2 Position;
        public int Line;
        public bool IsActive = true;
        private Texture2D texture;
        private float rotation = 0f;
        public float Speed = 500f; 
        private Vector2 origin;
        public WhiskUltimate(Vector2 startPos, int line, List<Texture2D> texs, Random rnd)
        {
            
            Position = new Vector2(startPos.X, startPos.Y + 55f); 
            Line = line;
            
            if (texs != null && texs.Count > 0)//Random whirlpools will appear from 5 visuals.
            {
                texture = texs[rnd.Next(texs.Count)];
                //We set the pivot point to the exact center so it rotates around itself.

                origin = new Vector2(texture.Width / 2f, texture.Height / 2f); 
            }
        }

        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
            if (!IsActive) return;

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            rotation += 10f * dt; //For a boomerang effect.

            Position.X += Speed * dt;
            
            //To make it disappear when it exits the right side of the screen.

            if (Position.X > 2200) 
            {
                IsActive = false;
                return;
            }

            foreach (var enemy in enemies)
            {
                int enemyLine = 0;
                if (enemy.Position.Y < 250) enemyLine = 0;
                else if (enemy.Position.Y < 430) enemyLine = 1;
                else if (enemy.Position.Y < 615) enemyLine = 2;
                else enemyLine = 3;

                // Girdap merkezli olduğu için mesafeyi (70f) ona göre kontrol ediyoruz
                if (enemyLine == Line && Math.Abs(enemy.Position.X - Position.X) < 70f)
                {
                    enemy.Position = new Vector2(enemy.Position.X + 200f, enemy.Position.Y);//Knock the enemy back
                    enemy.CurrentHP -= 20f; // Deal low damage.
                    IsActive = false; 
                    break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsActive || texture == null) return;
            
            float scale = 110f / Math.Max(texture.Width, texture.Height);
            spriteBatch.Draw(texture, Position, null, Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
        }
    }
}