using System.Collections.Generic;

namespace Slay_Your_Vegetables
{
    public class Knife : Weapons
    {
        public Knife()
        {
            Name = "Knife";
            Damage = 75f;
            EffectiveFoods = new List<string> 
            { 
                "Tomato", "Lettuce", "Lemon", "Bread", "Mushroom", "Banana", "Biscuit" 
            };
        }

        public override void Ultimate(List<Enemy> enemies)
        {
            
        }
    }
}