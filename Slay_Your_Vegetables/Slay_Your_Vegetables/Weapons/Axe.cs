using System.Collections.Generic;

namespace Slay_Your_Vegetables
{
    public class Axe : Weapons //The knife’s ultimate
    {
        public Axe()
        {
            Name = "Axe";
            Damage = 130f; 
            EffectiveFoods = new List<string> { "Tomato", "Lettuce", "Lemon", "Bread", "Mushroom", "Banana", "Biscuit" };
        }

        public override void Ultimate(List<Enemy> enemies)
        {
            
        }
    }
}