using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Animation 
{
    private List<Texture2D> Frames;
    private int CurrentFrame;
    private float Frametime; 
    private float timer; 
    
    public Texture2D CurrentTexture => Frames[CurrentFrame];

    public Animation(List<Texture2D> frames, float frametime)
    {
        this.Frames= frames;
        this.Frametime=frametime;
    }

    public virtual void Update(GameTime gameTime)
    {
       
       
       timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
      
       if (timer>= Frametime) 
        {
            timer=0f;//Reset time
            CurrentFrame++;

            if(CurrentFrame >= Frames.Count) // to reset frames (then restart the anim again)
            {
                CurrentFrame=0;//Go back to the beginning
            }
            
        }
        return;
    } 
   
}


    
