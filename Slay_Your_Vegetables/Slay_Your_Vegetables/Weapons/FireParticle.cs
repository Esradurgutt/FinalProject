using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Slay_Your_Vegetables
{
    public class FireParticle
    {
        public Vector2 Position;
        public int TargetLine; 
        public float Life = 1.0f;//full visibility

        public FireParticle(Vector2 pos, int line) 
        {
            Position = pos;
            TargetLine = line; 
        }

        public void Update()
        {
            Life -= 0.02f; //to disappear by becoming transparent
        }
        public void Draw(SpriteBatch sb, Texture2D tex)
        {
            if (Life > 0)
            {
              
                int yPos = 120 + (TargetLine * 185) + 10; // This way, the fire lands in the same spot as the enemy.

                
                Rectangle destRect = new Rectangle((int)Position.X, yPos, 80, 80);//the space it occupies on the screen
                
                sb.Draw(tex, destRect, null, Color.White * Life, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            }
        }
    }
}