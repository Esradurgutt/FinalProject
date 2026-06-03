using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Slay_Your_Vegetables;

namespace Slay_Your_Vegetables
{
    // Yogurt, cream
    // normal hp (300), normal speed

    public class Yogurt : Enemy
    {
        public Yogurt(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Name = "Yogurt";
            MaxHP = 300;
            CurrentHP = MaxHP;
            AttackPower = 10;
            Speed = 2.0f;

            List<Texture2D> wFrames = new List<Texture2D>();
            for (int i = 0; i < 24; i++)
            {
                string asset = "YogurtWalk/yogurtW_" + i.ToString("D5");
                wFrames.Add(Game1.ContentManager.Load<Texture2D>(asset));
            }

            this.animation  = new Animation(wFrames, 0.04f);
        }

        public override void DealDamage() { base.DealDamage(); }
        public override void TakeDamage(float amount, string weaponName) { base.TakeDamage(amount, weaponName); }
        public override void PushBack(float amount) { base.PushBack(amount); }
    }

    public class Cream : Enemy
    {
        public Cream(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Name = "Cream";
            MaxHP = 300;
            CurrentHP = MaxHP;
            AttackPower = 10;
            Speed = 2.0f;

            List<Texture2D> wFrames = new List<Texture2D>();
            for (int i = 0; i < 24; i++)
            {
                string asset = "CreamWalk/creamW_" + i.ToString("D5");
                wFrames.Add(Game1.ContentManager.Load<Texture2D>(asset));
            }

            this.animation  = new Animation(wFrames, 0.04f);
        }

        public override void DealDamage() { base.DealDamage(); }
        public override void TakeDamage(float amount, string weaponName) { base.TakeDamage(amount, weaponName); }
        public override void PushBack(float amount) { base.PushBack(amount); }
    }
}