using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Slay_Your_Vegetables
{
    public class Blowtorch : Weapons
    {
        public Blowtorch()
        {
            Name = "Blowtorch";
            Damage = 50f;
            EffectiveFoods = new List<string> 
            { 
                "Tuna", "Ground beef", "Eggplant", "Butter", "Chicken", "Chocolate" 
            };
        }

        public override void Ultimate(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                enemy.TakeDamage(Damage * 2f, Name);   
            }
        }
    }
}