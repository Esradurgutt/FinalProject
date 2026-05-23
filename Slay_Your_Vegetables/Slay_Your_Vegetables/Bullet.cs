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

        // For a boomerang and spinning effect

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
            //MOVEMENT AND BOOMERANG LOGIC 
            if (weapon.Name == "Whisk")
            {
                Rotation += 0.2f; 

                if (!IsReturning)
                {
                    Position.X += Speed; // While moving forward
                    if (Position.X > 1900) 
                        IsReturning = true; //When it reaches the end of the line, start returning

                }
                else
                {
                    Position.X -= Speed; // While returning
                    if (Position.X < 300) 
                        IsActive = false; //To make it disappear when it reaches the boss
                }
            }
            else
            {
                // Knife, Axe, Whisk, Blowtorch move straight normally.
                Position.X += Speed;
                if (Position.X > 2000) 
                { 
                    IsActive = false; 
                    return; 
                }
            }

            // COLLISION CHECK
            foreach (var e in enemies)
            {
                bool sameLine = (e.Line == this.BulletLine) || (Math.Abs(e.Position.Y - this.Position.Y) < 80);
                bool isTouching = Math.Abs(e.Position.X - this.Position.X) < 120;

                if (sameLine && !e.IsDead && isTouching)
                {
                    if (!hitEnemies.Contains(e))
                    {
                        // 1. Calculate the damage.
                        float finalDamage = weapon.EffectiveFoods.Contains(e.Name) ? currentDamage : currentDamage * 0.1f;

                        // 2. Apply the damage
                        e.TakeDamage(finalDamage, weapon.Name);
                        hitEnemies.Add(e);

                        // 3.REACTION DEPENDING ON THE WEAPON:
                        if (weapon.Name == "Axe") 
                        {
                            currentDamage *= 0.5f; // The axe’s damage decreases by a certain percentage and it pierces through.
                        }
                        else if (weapon.Name == "Whisk")
                        {
                            IsReturning = true; // The whisk starts returning after hitting the first enemy it hits.
                        }
                        else 
                        { 
                            IsActive = false; // Knife or Fire: Disappears on impact
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
                    //For the whisk’s boomerang effect
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