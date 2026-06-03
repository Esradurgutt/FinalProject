using System.Collections.Generic;

namespace Slay_Your_Vegetables
{
    public class Whisk : Weapons
    {
        public Whisk()
        {
            Name = "Whisk";
            Damage = 60f;
            EffectiveFoods = new List<string> { "Yogurt", "Cream" };
        }

        public override void Ultimate(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                if (EffectiveFoods.Contains(enemy.Name))
                {
                    enemy.TakeDamage(Damage * 2.5f, Name);
                    enemy.PushBack(2f); 
                }
            }
        }
    }
}