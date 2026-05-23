using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Slay_Your_Vegetables
{
    public abstract class Enemy : ScaledSprite
    {
        public static Dictionary<int, Texture2D> textures = new Dictionary<int, Texture2D>();
    
        public int ID { get; set; } 
        
        public string Name { get; set; }
        public float MaxHP { get; set; }
        public float CurrentHP { get; set; }
        public float Speed { get; set; }
        public float AttackPower { get; set; }
        public bool IsDead => CurrentHP <= 0;
        
        public int Line { get; set; } 

        public Enemy(Texture2D t, Vector2 p, string n, float h, float s) : base(t, p)
        {
            Name = n; MaxHP = h; CurrentHP = h; Speed = s; AttackPower = 10f;
        }

        public virtual void TakeDamage(float amount, string weaponName) { CurrentHP -= amount; }
        public virtual void PushBack(float amount) { Position.X += amount * 50f; }

        public override void Update(GameTime gameTime)
        {
            if (IsDead) return;
            Position.X -= Speed;
            string folder = (Name == "Ground beef") ? "GBeefWalk" : Name + "Walk";
            string prefix = (Name == "Ground beef") ? "gbeefW_" : Name.ToLower() + "W_";
            int frame = (int)(gameTime.TotalGameTime.TotalMilliseconds / 50) % 20;
            try { texture = Game1.ContentManager.Load<Texture2D>($"{folder}/{prefix}{frame:D5}"); } catch { }
        }

        public override void Draw(SpriteBatch sb)
        {
            if (!IsDead) 
            {
                sb.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, 150, 150), Color.White);

                if (AssetManager.Pixel != null)
                {
                    float hpPercent = CurrentHP / MaxHP;
                    if (hpPercent < 0) hpPercent = 0;
                    int barY = (int)Position.Y - 15;
                    
                    sb.Draw(AssetManager.Pixel, new Rectangle((int)Position.X, barY, 150, 10), Color.Black);
                    sb.Draw(AssetManager.Pixel, new Rectangle((int)Position.X, barY, (int)(150 * hpPercent), 10), Color.Red);
                }
            }
        }

        public static Enemy CreateEnemy(int id, Vector2 position, int line)
        {
            if (textures == null || !textures.ContainsKey(id)) return null;
            Texture2D tex = textures[id];
            
            Enemy createdEnemy = null;
            
            switch (id)
            {
                case 0: createdEnemy = new Tomato(tex, position); break;
                case 1: createdEnemy = new Lettuce(tex, position); break;
                case 2: createdEnemy = new Lemon(tex, position); break;
                case 4: createdEnemy = new Bread(tex, position); break;
                case 11: createdEnemy = new Mushroom(tex, position); break;
                case 13: createdEnemy = new Banana(tex, position); break;
                case 14: createdEnemy = new Biscuit(tex, position); break;
                case 3: createdEnemy = new Tuna(tex, position); break;
                case 5: createdEnemy = new GBeef(tex, position); break;
                case 6: createdEnemy = new Eggplant(tex, position); break;
                case 9: createdEnemy = new Butter(tex, position); break;
                case 10: createdEnemy = new Chicken(tex, position); break;
                case 12: createdEnemy = new Chocolate(tex, position); break;
                case 7: createdEnemy = new Yogurt(tex, position); break;
                case 8: createdEnemy = new Cream(tex, position); break;
            }
            
            if (createdEnemy != null)
            {
                createdEnemy.Line = line;
                createdEnemy.ID = id; 
            }
            
            return createdEnemy;
        }
    }
    
    public abstract class SharpResponsiveEnemy : Enemy { public SharpResponsiveEnemy(Texture2D t, Vector2 p, string n, float h, float s) : base(t, p, n, h, s) { } }
    public abstract class ThermalResponsiveEnemy : Enemy { public ThermalResponsiveEnemy(Texture2D t, Vector2 p, string n, float h, float s) : base(t, p, n, h, s) { } }
    public abstract class FluidResponsiveEnemy : Enemy { public FluidResponsiveEnemy(Texture2D t, Vector2 p, string n, float h, float s) : base(t, p, n, h, s) { } }
}