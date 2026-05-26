using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System; 

namespace Slay_Your_Vegetables
{
    public class SpawnManage
    {
        private List<Enemy> enemies = new List<Enemy>();
        public LevelManage lvl;
        private Dictionary<int, Texture2D> textures;
        private float spawnTimer = 0f;
        private Random random = new Random();

        public SpawnManage(LevelManage level, Dictionary<int, Texture2D> textures) 
        { 
            this.lvl = level; 
            this.textures = textures;
        }

        public void Update(GameTime gameTime, Player player)
        {
            spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (spawnTimer >= lvl.CurrentLevel.spawnPeriod)//Spawn creates a new enemy when the spawnperiod expires.
            {
                spawnTimer = 0f;
                int randomId = lvl.CurrentLevel.SpawnPool[random.Next(lvl.CurrentLevel.SpawnPool.Count)];//It draws a random enemy ID from the SpawnPool.
                int chosenLine = random.Next(0, 4);
                float spawnY = 120 + (chosenLine * 185) + 10;
                Spawn(randomId, new Vector2(1920, spawnY), chosenLine);
            }
            //reaching and dying the player
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(gameTime);

                if (enemies[i].Position.X < 500) 
                { 
                    player.TakeDamage((int)enemies[i].AttackPower); 
                    enemies.RemoveAt(i); 
                }
                else if (enemies[i].IsDead) 
                { 
                    int id = enemies[i].ID; 
                    
                    if (lvl.CurrentLevel.DefeatedCounters.ContainsKey(id))
                    {
                        lvl.CurrentLevel.DefeatedCounters[id]++;
                    }
                    enemies.RemoveAt(i); 
                }
            }
        }

        public void Draw(SpriteBatch sb) { foreach (var e in enemies) e.Draw(sb); }
        public List<Enemy> GetActiveEnemies() => enemies;

        public void Spawn(int id, Vector2 position, int line)
        {
            Enemy n_enemy = Enemy.CreateEnemy(id, position, line);
            if (n_enemy != null) 
            {
                enemies.Add(n_enemy);
            }
        }
    }
}