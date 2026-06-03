using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Slay_Your_Vegetables
{
    public class Bullet
    {
        public Vector2 Position;
        public int Speed = 10;
        public Texture2D Texture;
        public bool IsActive = true;
        public int Width = 120;
        public int Height = 120;
        public int BulletLine; 
        
        private Weapons weapon;
        public float currentDamage; 
        private List<Enemy> hitEnemies = new List<Enemy>();

        
        public float Rotation = 0f;
        public bool IsReturning = false;

        public Bullet(Texture2D tex, Vector2 pos, Weapons w, int line) 
        { 
            Texture = tex;
            Position = pos; 
            weapon = w; 
            BulletLine = line; 
            currentDamage = w.Damage; 
        }

        public void Update(GameTime gameTime, List<Enemy> enemies)
        { 
            if (weapon.Name == "Whisk")
            {
                Rotation += 0.2f; 

                if (!IsReturning)
                {
                    Position.X += Speed; 
                    if (Position.X > 1900) 
                        IsReturning = true;

                }
                else
                {
                    Position.X -= Speed; 
                    if (Position.X < 300) 
                        IsActive = false; 
                }
            }
            else
            {
                Position.X += Speed;// K,A,B
                if (Position.X > 2000) 
                { 
                    IsActive = false; 
                    return; 
                }
            }

            
            foreach (var e in enemies)
            {
                bool sameLine = (e.Line == this.BulletLine) || (Math.Abs(e.Position.Y - this.Position.Y) < 80);
                bool isTouching = Math.Abs(e.Position.X - this.Position.X) < 120;

                if (sameLine && !e.IsDead && isTouching)
                {
                    if (!hitEnemies.Contains(e))//ilkkez
                    {
                        float finalDamage = weapon.EffectiveFoods.Contains(e.Name) ? currentDamage : currentDamage * 0.1f;//tamhasar
                        e.TakeDamage(finalDamage, weapon.Name);
                        hitEnemies.Add(e);

                        if (weapon.Name == "Axe") 
                        {
                            currentDamage *= 0.5f; 
                        }
                        else if (weapon.Name == "Whisk")
                        {
                            IsReturning = true; 
                        }
                        else 
                        { 
                            IsActive = false; //diğerleriyokolur carptığıgibi
                            break; 
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            if (IsActive)
            {
                if (weapon.Name == "Whisk")
                {
                    Vector2 origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
                    Rectangle destRect = new Rectangle((int)Position.X + Width / 2, (int)Position.Y + Height / 2, Width, Height);
                    
                    sb.Draw(Texture, destRect, null, Color.White, Rotation, origin, SpriteEffects.None, 0f);
                }
                else
                {
                    sb.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), Color.White);
                }
            }
        }
    }
}