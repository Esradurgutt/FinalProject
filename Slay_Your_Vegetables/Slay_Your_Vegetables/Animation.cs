using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class Animation 
{
    private List<Texture2D> Frames;
    private int CurrentFrame;
    private float Frametime; 
    private float timer; 
    protected int loop;
    public bool isfinished {get; protected set;}
    public Texture2D CurrentTexture => Frames[CurrentFrame];

    public Animation(List<Texture2D> frames, float frametime)
    {
        this.Frames= frames;
        this.Frametime=frametime;
    }

    public virtual void Update(GameTime gameTime)
    {
        if (isfinished) 
       return;
       timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
      
       if (timer>= Frametime) 
        {
            timer=0f;//Reset
            CurrentFrame++;

            if(CurrentFrame >= Frames.Count) // to reset frames and make the animation again
            {
                CurrentFrame=0;//Go back to the beginning
                loop++;
                
            }
            
        }
    } 
    
}
public class WalkAnimation :Animation
{
    public WalkAnimation(List<Texture2D> frames, float frametime): base(frames,frametime)
    {
        
    }

}

