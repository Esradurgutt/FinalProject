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
      
       if (timer>= Frametime) //Has the waiting period expired?
        {
            timer=0f;//Reset the stopwatch
            CurrentFrame++;

            if(CurrentFrame >= Frames.Count) // to reset frames and make the animation again
            {
                CurrentFrame=0;//Go back to the beginning
                loop++;
                LoopComplete();
            }
            
        }
    } 
    protected abstract void LoopComplete();
}
public class WalkAnimation :Animation
{
    public WalkAnimation(List<Texture2D> frames, float frametime): base(frames,frametime)
    {
        
    }

    protected override void LoopComplete()
    {
        if (loop>= 4) // YOU CAN CHANGE THE WALKING ANIMATION LOOP IN HERE
        {
            isfinished = true;
        }
    }
}


    
public class AttackAnimation: Animation
{
    public AttackAnimation(List<Texture2D> frames, float frametime) : base(frames,frametime) { }

    protected override void LoopComplete()
    {
        if (loop >= 1) // attack anim plays once
        {
            isfinished = true;
        }
    }
}
