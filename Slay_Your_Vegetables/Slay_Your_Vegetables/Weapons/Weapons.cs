using System.Collections.Generic;

namespace Slay_Your_Vegetables
{
    public abstract class Weapons
    {
        public string Name;
        public float Damage;
        public List<string> EffectiveFoods;
        public abstract void Ultimate(List<Enemy> enemies);
    }
}