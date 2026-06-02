using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Slay_Your_Vegetables;
namespace Slay_Your_Vegetables
//Tomato, Lettuce, Lemon, Bread, Mushroom, Banana, Biscuit
//high hp (300-500), low speed
{
    public class Tomato : Enemy
    {
        public Tomato(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Name = "Tomato";
            MaxHP = 300;
            CurrentHP = MaxHP;
            AttackPower = 10;
            Speed = 3.0f; 

            List<Texture2D> wFrames = new List<Texture2D>();
            for (int i = 0; i < 24; i++)
            {
                wFrames.Add(Game1.ContentManager.Load<Texture2D>("TomatoWalk/tomatoW_" + i.ToString("D5")));// d5 =0000
            }
            this.walkAnimation = new WalkAnimation(wFrames, 0.04f);// YOU CAN CHANGE THE ANIMATION SPEED IN HERE!!!
            this.animation = walkAnimation;
        }

        public override void Update(GameTime gameTime) => base.Update(gameTime);
        public override void DealDamage() { base.DealDamage(); }
        public override void TakeDamage(float a, string n) { base.TakeDamage(a, n); }
        public override void PushBack(float a) { base.PushBack(a); }
    }

    public class Lettuce : Enemy
    {
        public Lettuce(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Name = "Lettuce";
            MaxHP = 400;
            CurrentHP = MaxHP;
            AttackPower = 10;
            Speed = 3.0f; 

            List<Texture2D> wFrames = new List<Texture2D>();
            for (int i = 0; i < 24; i++)
            {
                wFrames.Add(Game1.ContentManager.Load<Texture2D>("LettuceWalk/lettuceW_" + i.ToString("D5")));// d5=0000
            }
            this.walkAnimation = new WalkAnimation(wFrames, 0.04f);// YOU CAN CHANGE THE ANIMATION SPEED IN HERE!!!
            this.animation = walkAnimation;
        }
    }

    public class Lemon : Enemy
    {
        public Lemon(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Name = "Lemon";
            MaxHP = 300;
            CurrentHP = MaxHP;
            AttackPower = 10;
            Speed = 3.0f; 

            List<Texture2D> wFrames = new List<Texture2D>();
            for (int i = 0; i < 24; i++)
            {
                wFrames.Add(Game1.ContentManager.Load<Texture2D>("LemonWalk/lemonW_" + i.ToString("D5")));
            }
            this.walkAnimation = new WalkAnimation(wFrames, 0.04f);
            this.animation = walkAnimation;
        }
    }

    public class Bread : Enemy
    {
        public Bread(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Name = "Bread";
            MaxHP = 350;
            CurrentHP = MaxHP;
            AttackPower = 10;
            Speed = 3.0f; // Hız güncellendi

            List<Texture2D> wFrames = new List<Texture2D>();
            for (int i = 0; i < 24; i++)
            {
                wFrames.Add(Game1.ContentManager.Load<Texture2D>("BreadWalk/breadW_" + i.ToString("D5")));
            }
            this.walkAnimation = new WalkAnimation(wFrames, 0.04f);
            this.animation = walkAnimation;
        }
    }

    public class Mushroom : Enemy
    {
        public Mushroom(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Name = "Mushroom";
            MaxHP = 350;
            CurrentHP = MaxHP;
            AttackPower = 10;
            Speed = 3.0f; // Hız güncellendi

            List<Texture2D> wFrames = new List<Texture2D>();
            for (int i = 0; i < 24; i++)
            {
                wFrames.Add(Game1.ContentManager.Load<Texture2D>("MushroomWalk/mushroomW_" + i.ToString("D5")));
            }
            this.walkAnimation = new WalkAnimation(wFrames, 0.04f);
            this.animation = walkAnimation;
        }
    }

    public class Banana : Enemy
    {
        public Banana(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Name = "Banana";
            MaxHP = 350;
            CurrentHP = MaxHP;
            AttackPower = 10;
            Speed = 3.0f; // Hız güncellendi

            List<Texture2D> wFrames = new List<Texture2D>();
            for (int i = 0; i < 24; i++)
            {
                wFrames.Add(Game1.ContentManager.Load<Texture2D>("BananaWalk/bananaW_" + i.ToString("D5")));
            }
            this.walkAnimation = new WalkAnimation(wFrames, 0.04f);
            this.animation = walkAnimation;
        }
    }

    public class Biscuit : Enemy
    {
        public Biscuit(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Name = "Biscuit";
            MaxHP = 350;
            CurrentHP = MaxHP;
            AttackPower = 10;
            Speed = 3.0f; // Hız güncellendi

            List<Texture2D> wFrames = new List<Texture2D>();
            for (int i = 0; i < 24; i++)
            {
                wFrames.Add(Game1.ContentManager.Load<Texture2D>("BiscuitWalk/biscuitW_" + i.ToString("D5")));
            }
            this.walkAnimation = new WalkAnimation(wFrames, 0.04f);
            this.animation = walkAnimation;
        }
    }
}