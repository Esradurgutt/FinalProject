using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Slay_Your_Vegetables;

namespace Slay_Your_Vegetables
{
    //Tuna, GroundBeef, Eggplant, Butter, Chicken, Chocolate
    //low hp(100-300), high speed

    public class Tuna : Enemy
    {
        public Tuna(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Name = "Tuna";
            MaxHP = 150;
            CurrentHP = MaxHP;
            AttackPower = 10;
            Speed = 2.5f;

            List<Texture2D> wFrames = new List<Texture2D>();
            for (int i = 0; i < 24; i++)
            {
                wFrames.Add(Game1.ContentManager.Load<Texture2D>("TunaWalk/tunaW_" + i.ToString("D5")));
            }
           
        }

        public override void DealDamage() { base.DealDamage(); }
        public override void TakeDamage(float amount, string weaponName) { base.TakeDamage(amount, weaponName); }
        public override void PushBack(float amount) { base.PushBack(amount); }
    }

    public class GBeef : Enemy
    {
        public GBeef(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Name = "Ground Beef";
            MaxHP = 150;
            CurrentHP = MaxHP;
            AttackPower = 10;
            Speed = 2.5f;

            List<Texture2D> wFrames = new List<Texture2D>();
            for (int i = 0; i < 24; i++)
            {
                wFrames.Add(Game1.ContentManager.Load<Texture2D>("GBeefWalk/gbeefW_" + i.ToString("D5")));
            }
            
        }

        public override void DealDamage() { base.DealDamage(); }
        public override void TakeDamage(float amount, string weaponName) { base.TakeDamage(amount, weaponName); }
        public override void PushBack(float amount) { base.PushBack(amount); }
    }

    public class Eggplant : Enemy
    {
        public Eggplant(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Name = "Eggplant";
            MaxHP = 250;
            CurrentHP = MaxHP;
            AttackPower = 10;
            Speed = 2.5f;

            List<Texture2D> wFrames = new List<Texture2D>();
            for (int i = 0; i < 24; i++)
            {
                wFrames.Add(Game1.ContentManager.Load<Texture2D>("EggplantWalk/eggplantW_" + i.ToString("D5")));
            }
           
        }

        public override void DealDamage() { base.DealDamage(); }
        public override void TakeDamage(float amount, string weaponName) { base.TakeDamage(amount, weaponName); }
        public override void PushBack(float amount) { base.PushBack(amount); }
    }

    public class Butter : Enemy
    {
        public Butter(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Name = "Butter";
            MaxHP = 200;
            CurrentHP = MaxHP;
            AttackPower = 10;
            Speed = 2.5f;

            List<Texture2D> wFrames = new List<Texture2D>();
            for (int i = 0; i < 24; i++)
            {
                wFrames.Add(Game1.ContentManager.Load<Texture2D>("ButterWalk/butterW_" + i.ToString("D5")));
            }
            
        }

        public override void DealDamage() { base.DealDamage(); }
        public override void TakeDamage(float amount, string weaponName) { base.TakeDamage(amount, weaponName); }
        public override void PushBack(float amount) { base.PushBack(amount); }
    }

    public class Chicken : Enemy
    {
        public Chicken(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Name = "Chicken";
            MaxHP = 250;
            CurrentHP = MaxHP;
            AttackPower = 10;
            Speed = 2.5f;

            List<Texture2D> wFrames = new List<Texture2D>();
            for (int i = 0; i < 24; i++)
            {
                wFrames.Add(Game1.ContentManager.Load<Texture2D>("ChickenWalk/chickenW_" + i.ToString("D5")));
            }
           
        }

        public override void DealDamage() { base.DealDamage(); }
        public override void TakeDamage(float amount, string weaponName) { base.TakeDamage(amount, weaponName); }
        public override void PushBack(float amount) { base.PushBack(amount); }
    }

    public class Chocolate : Enemy
    {
        public Chocolate(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Name = "Chocolate";
            MaxHP = 250;
            CurrentHP = MaxHP;
            AttackPower = 10;
            Speed = 2.5f;

            List<Texture2D> wFrames = new List<Texture2D>();
            for (int i = 0; i < 24; i++)
            {
                wFrames.Add(Game1.ContentManager.Load<Texture2D>("ChocolateWalk/chocolateW_" + i.ToString("D5")));
            }
            
        }

        public override void DealDamage() { base.DealDamage(); }
        public override void TakeDamage(float amount, string weaponName) { base.TakeDamage(amount, weaponName); }
        public override void PushBack(float amount) { base.PushBack(amount); }
    }
}