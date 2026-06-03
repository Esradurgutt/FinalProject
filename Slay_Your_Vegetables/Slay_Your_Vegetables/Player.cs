using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Slay_Your_Vegetables
{
    public class Player : Sprite
    {
        public int BaseSpeed = 9;   
        public int TiredSpeed = 3;  
        public int Width = 200; 
        public int Height = 200;
        public int MaxHP = 100;
        public int CurrentHP;
        public int MaxMana = 100;
        public float CurrentMana;
        public int MaxStamina = 100;
        public float CurrentStamina;

        public List<Weapons> WeaponInventory;
        public int CurrentWeaponIndex = 0;
        public Weapons ActiveWeapon => WeaponInventory[CurrentWeaponIndex];
        
        public int KnifeCount = 0;
        public int BlowtorchCount = 0;
        public int WhiskCount = 0;


        private List<Texture2D> animationFrames;
        private int currentFrame = 0;
        private float frameTimer = 0f;
        private float frameRate = 0.07f; 

        public Player(Texture2D dummyTexture, Vector2 position) : base(dummyTexture, position) 
        {
            CurrentHP = MaxHP;
            CurrentMana = MaxMana;
            CurrentStamina = MaxStamina;

            WeaponInventory = new List<Weapons> { new Knife(), new Blowtorch(), new Whisk() };
            animationFrames = new List<Texture2D>();
            
           
            for (int i = 0; i < 5; i++)
            {
                Texture2D loadedTex = null;
                try { loadedTex = Game1.ContentManager.Load<Texture2D>($"chefWalk_0000{i}"); } catch { }
                if (loadedTex == null) { try { loadedTex = Game1.ContentManager.Load<Texture2D>($"ChefWalk/chefWalk_0000{i}"); } catch { } }
                if (loadedTex != null) animationFrames.Add(loadedTex);
            }
        }
        
        public void AddKnifeAttack() { if (KnifeCount < 10) KnifeCount++; }
        public void ResetKnifeCount() { KnifeCount = 0; }

        public void AddBlowtorchAttack() { if (BlowtorchCount < 10) BlowtorchCount++; }
        public void ResetBlowtorchCount() { BlowtorchCount = 0; }

        public void AddWhiskAttack() { if (WhiskCount < 10) WhiskCount++; }
        public void ResetWhiskCount() { WhiskCount = 0; }

        public void TakeDamage(int amount) { CurrentHP -= amount; if (CurrentHP < 0) CurrentHP = 0; }
        public void ChangeWeapon(int index) { if (index >= 0 && index < WeaponInventory.Count) CurrentWeaponIndex = index; }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (CurrentMana < MaxMana) CurrentMana += dt * 20f;

            KeyboardState state = Keyboard.GetState();
            Vector2 movement = Vector2.Zero;

            if (state.IsKeyDown(Keys.W)) movement.Y -= 1;
            if (state.IsKeyDown(Keys.S)) movement.Y += 1;

            if (animationFrames == null || animationFrames.Count == 0) return;

            

            if (movement != Vector2.Zero)
            {
                if (CurrentStamina > 0) CurrentStamina -= dt * 60f;
                if (CurrentStamina < 0) CurrentStamina = 0;

                int currentSpeed = (CurrentStamina <= 0) ? TiredSpeed : BaseSpeed;
                Position += movement * currentSpeed;

                if (Position.Y < 120) Position.Y = 120;//collision
                if (Position.Y > 845 - Height) Position.Y = 845 - Height;

                frameTimer += dt;
                if (frameTimer >= frameRate)
                {
                    currentFrame++;
                    if (currentFrame >= animationFrames.Count) currentFrame = 0;
                    frameTimer = 0f;
                }
            }
            else 
            {
                if (CurrentStamina < MaxStamina) CurrentStamina += dt * 12f;
                currentFrame = 0;
                frameTimer = 0f;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destinationRect = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            if (animationFrames != null && animationFrames.Count > currentFrame)
                spriteBatch.Draw(animationFrames[currentFrame], destinationRect, Color.White);
            else
                base.Draw(spriteBatch);
        }
    }
}