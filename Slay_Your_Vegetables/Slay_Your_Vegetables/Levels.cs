using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Slay_Your_Vegetables
{
    public interface ILevel
    {
        Dictionary<int, int> Goals { get; }
        Dictionary<int, int> SpawnedCounters { get; }
        Dictionary<int, int> DefeatedCounters { get; } // Öldürülenlerin sayısını tutacak
        List<int> SpawnPool { get; }
        float spawnPeriod { get; }
    }

    public class LevelManage
    {
        public ILevel CurrentLevel { get; private set; }
        public void LoadLevel(int lvlNumber)
        {
            switch (lvlNumber)
            {
                case 1: CurrentLevel = new Level1(); break;
                case 2: CurrentLevel = new Level2(); break;
                case 3: CurrentLevel = new Level3(); break;
                case 4: CurrentLevel = new Level4(); break;
                case 5: CurrentLevel = new Level5(); break;
                default: CurrentLevel = null; break;
            }
        }
    }

    public class Level1 : ILevel
    {
        public Dictionary<int, int> Goals => new Dictionary<int, int> { { 0, 4 }, { 1, 3 }, { 2, 2 }, { 3, 2 } };
        public Dictionary<int, int> SpawnedCounters { get; } = new Dictionary<int, int> { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 } };
        public Dictionary<int, int> DefeatedCounters { get; } = new Dictionary<int, int> { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 } };
        public List<int> SpawnPool => new List<int> { 0, 1, 2, 3 };
        public float spawnPeriod => 3.0f;
    }

    public class Level2 : ILevel
    {
        public Dictionary<int, int> Goals => new Dictionary<int, int> { { 0, 3 }, { 1, 2 }, { 4, 2 }, { 5, 4 } };
        public Dictionary<int, int> SpawnedCounters { get; } = new Dictionary<int, int> { { 0, 0 }, { 1, 0 }, { 4, 0 }, { 5, 0 } };
        public Dictionary<int, int> DefeatedCounters { get; } = new Dictionary<int, int> { { 0, 0 }, { 1, 0 }, { 4, 0 }, { 5, 0 } };
        public List<int> SpawnPool => new List<int> { 0, 1, 4, 5 };
        public float spawnPeriod => 3.0f;
    }

    public class Level3 : ILevel
    {
        public Dictionary<int, int> Goals => new Dictionary<int, int> { { 0, 2 }, { 5, 3 }, { 6, 4 }, { 7, 2 } };
        public Dictionary<int, int> SpawnedCounters { get; } = new Dictionary<int, int> { { 0, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 } };
        public Dictionary<int, int> DefeatedCounters { get; } = new Dictionary<int, int> { { 0, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 } };
        public List<int> SpawnPool => new List<int> { 0, 5, 6, 7 };
        public float spawnPeriod => 3.0f;
    }

    public class Level4 : ILevel
    {
        public Dictionary<int, int> Goals => new Dictionary<int, int> { { 8, 3 }, { 9, 2 }, { 10, 4 }, { 11, 3 } };
        public Dictionary<int, int> SpawnedCounters { get; } = new Dictionary<int, int> { { 8, 0 }, { 9, 0 }, { 10, 0 }, { 11, 0 } };
        public Dictionary<int, int> DefeatedCounters { get; } = new Dictionary<int, int> { { 8, 0 }, { 9, 0 }, { 10, 0 }, { 11, 0 } };
        public List<int> SpawnPool => new List<int> { 8, 9, 10, 11 };
        public float spawnPeriod => 3.0f;
    }

    public class Level5 : ILevel
    {
        public Dictionary<int, int> Goals => new Dictionary<int, int> { { 8, 3 }, { 12, 3 }, { 13, 2 }, { 14, 4 } };
        public Dictionary<int, int> SpawnedCounters { get; } = new Dictionary<int, int> { { 8, 0 }, { 12, 0 }, { 13, 0 }, { 14, 0 } };
        public Dictionary<int, int> DefeatedCounters { get; } = new Dictionary<int, int> { { 8, 0 }, { 12, 0 }, { 13, 0 }, { 14, 0 } };
        public List<int> SpawnPool => new List<int> { 8, 12, 13, 14 };
        public float spawnPeriod => 3.0f;
    }
}