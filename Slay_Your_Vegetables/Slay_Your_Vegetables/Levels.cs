using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Slay_Your_Vegetables
{
    public interface ILevel
    {
        Dictionary<int, int> Goals { get; }
        Dictionary<int, int> SpawnedCounters { get; }
        Dictionary<int, int> DefeatedCounters { get; } // It will keep track of the number of those killed
        List<int> SpawnPool { get; }
        float spawnPeriod { get; }
        bool IsTutorialActive { get; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
    //for levels
    public class LevelManage
    {
        public ILevel CurrentLevel { get; private set; }
        private GraphicsDevice graphicsDevice;
        private SpriteFont font, titleF;
        private Texture2D recipeT;
        
        public LevelManage(GraphicsDevice graphicsDevice, SpriteFont font, SpriteFont title,Texture2D recipe)
    {
        this.graphicsDevice = graphicsDevice;
        this.font = font;
        recipeT = recipe;
        titleF= title;
    }
        public void LoadLevel(int lvlNumber)
        {
            switch (lvlNumber)
            {
                case 1: CurrentLevel = new Level1(graphicsDevice,font,titleF,recipeT); break;
                case 2: CurrentLevel = new Level2(graphicsDevice,font,titleF,recipeT); break;
                case 3: CurrentLevel = new Level3(graphicsDevice,font,titleF,recipeT); break;
                case 4: CurrentLevel = new Level4(graphicsDevice,font,titleF,recipeT); break;
                case 5: CurrentLevel = new Level5(graphicsDevice,font,titleF,recipeT); break;
                default: CurrentLevel = null; break;
            }
        }
    }

    public class Level1 : ILevel //Salad tomato(0),lettuce(1),lemon(2),tuna(3)
    {
        public Dictionary<int, int> Goals => new Dictionary<int, int> { { 0, 4 }, { 1, 3 }, { 2, 2 }, { 3, 2 } };//first - enemy ID second - enemy count
        public Dictionary<int, int> SpawnedCounters { get; } = new Dictionary<int, int> { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 } };// Enemy ID, Beginning count
        public Dictionary<int, int> DefeatedCounters { get; } = new Dictionary<int, int> { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 } };
        public List<int> SpawnPool => new List<int> { 0, 1, 2, 3 };// The requirement (enemy ID's)
        public float spawnPeriod => 3.0f;
        public bool IsTutorialActive { get; private set; } = true;
     private Recipe recipe;
     public Level1(GraphicsDevice graphicsDevice, SpriteFont font,SpriteFont title, Texture2D recipeTexture)
    {
        recipe= new Salad(graphicsDevice,font,title,recipeTexture);
        recipe.Pressed += () => IsTutorialActive= false;
    }
    public void Update(GameTime gameTime)
    {
        if (IsTutorialActive)
        {
            recipe.Update(gameTime);
            return;
        }
    }
     public void Draw(SpriteBatch spriteBatch)
    {
        if (IsTutorialActive)
        {
            recipe.Draw(spriteBatch);
        }
    }
    }

    public class Level2 : ILevel // Hamburger tomato(0),lettuce(1),bread(4),gbeef(5)
    {
        public Dictionary<int, int> Goals => new Dictionary<int, int> { { 0, 3 }, { 1, 2 }, { 4, 2 }, { 5, 4 } };//first - enemy ID second - enemy count
        public Dictionary<int, int> SpawnedCounters { get; } = new Dictionary<int, int> { { 0, 0 }, { 1, 0 }, { 4, 0 }, { 5, 0 } };// Enemy ID, Beginning count
        public Dictionary<int, int> DefeatedCounters { get; } = new Dictionary<int, int> { { 0, 0 }, { 1, 0 }, { 4, 0 }, { 5, 0 } };
        public List<int> SpawnPool => new List<int> { 0, 1, 4, 5 };// The requirement (enemy ID's)
        public float spawnPeriod => 3.0f;
         public bool IsTutorialActive { get; private set; } = true;
     private Recipe recipe;
     public Level2(GraphicsDevice graphicsDevice, SpriteFont font,SpriteFont title, Texture2D recipeTexture)
    {
        recipe= new Hamburger(graphicsDevice,font,title,recipeTexture);
        recipe.Pressed += () => IsTutorialActive= false;
    }
    public void Update(GameTime gameTime)
    {
        if (IsTutorialActive)
        {
            recipe.Update(gameTime);
            return;
        }
    }
     public void Draw(SpriteBatch spriteBatch)
    {
        if (IsTutorialActive)
        {
            recipe.Draw(spriteBatch);
        }
    }
    }

    public class Level3 : ILevel// Ali Nazik tomato(0), gbeef(5),eggplant(6),yogurt(7)
    {
        public Dictionary<int, int> Goals => new Dictionary<int, int> { { 0, 2 }, { 5, 3 }, { 6, 4 }, { 7, 2 } }; //first - enemy ID second - enemy count
        public Dictionary<int, int> SpawnedCounters { get; } = new Dictionary<int, int> { { 0, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 } };// Enemy ID, Beginning count
        public Dictionary<int, int> DefeatedCounters { get; } = new Dictionary<int, int> { { 0, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 } };
        public List<int> SpawnPool => new List<int> { 0, 5, 6, 7 };// The requirement (enemy ID's)
        public float spawnPeriod => 3.0f;

          public bool IsTutorialActive { get; private set; } = true;
     private Recipe recipe;
     public Level3(GraphicsDevice graphicsDevice, SpriteFont font,SpriteFont title, Texture2D recipeTexture)
    {
        recipe= new AliNazik(graphicsDevice,font,title,recipeTexture);
        recipe.Pressed += () => IsTutorialActive= false;
    }
    public void Update(GameTime gameTime)
    {
        if (IsTutorialActive)
        {
            recipe.Update(gameTime);
            return;
        }
    }
     public void Draw(SpriteBatch spriteBatch)
    {
        if (IsTutorialActive)
        {
            recipe.Draw(spriteBatch);
        }
    }
    }

    public class Level4 : ILevel// Soup cream(8),butter(9),chicken(10),mushroom(11)
    {
        public Dictionary<int, int> Goals => new Dictionary<int, int> { { 8, 3 }, { 9, 2 }, { 10, 4 }, { 11, 3 } };//first - enemy ID second - enemy count
        public Dictionary<int, int> SpawnedCounters { get; } = new Dictionary<int, int> { { 8, 0 }, { 9, 0 }, { 10, 0 }, { 11, 0 } };// Enemy ID, Beginning count
        public Dictionary<int, int> DefeatedCounters { get; } = new Dictionary<int, int> { { 8, 0 }, { 9, 0 }, { 10, 0 }, { 11, 0 } };
        public List<int> SpawnPool => new List<int> { 8, 9, 10, 11 };// The requirement (enemy ID's)
        public float spawnPeriod => 3.0f;
        public bool IsTutorialActive { get; private set; } = true;
     private Recipe recipe;
     public Level4(GraphicsDevice graphicsDevice, SpriteFont font,SpriteFont title, Texture2D recipeTexture)
    {
        recipe= new Soup(graphicsDevice,font,title,recipeTexture);
        recipe.Pressed += () => IsTutorialActive= false;
    }
    public void Update(GameTime gameTime)
    {
        if (IsTutorialActive)
        {
            recipe.Update(gameTime);
            return;
        }
    }
     public void Draw(SpriteBatch spriteBatch)
    {
        if (IsTutorialActive)
        {
            recipe.Draw(spriteBatch);
        }
    }
    }

    public class Level5 : ILevel// Dessert cream(8),chocolate(12),banana(13), biscuit(14)
    {
        public Dictionary<int, int> Goals => new Dictionary<int, int> { { 8, 3 }, { 12, 3 }, { 13, 2 }, { 14, 4 } };//first - enemy ID second - enemy count
        public Dictionary<int, int> SpawnedCounters { get; } = new Dictionary<int, int> { { 8, 0 }, { 12, 0 }, { 13, 0 }, { 14, 0 } };// Enemy ID, Beginning count
        public Dictionary<int, int> DefeatedCounters { get; } = new Dictionary<int, int> { { 8, 0 }, { 12, 0 }, { 13, 0 }, { 14, 0 } };
        public List<int> SpawnPool => new List<int> { 8, 12, 13, 14 };// The requirement (enemy ID's)
        public float spawnPeriod => 3.0f;

         public bool IsTutorialActive { get; private set; } = true;
     private Recipe recipe;
     public Level5(GraphicsDevice graphicsDevice, SpriteFont font, SpriteFont title,Texture2D recipeTexture)
    {
        recipe= new Dessert(graphicsDevice,font,title,recipeTexture);
        recipe.Pressed += () => IsTutorialActive= false;
    }
    public void Update(GameTime gameTime)
    {
        if (IsTutorialActive)
        {
            recipe.Update(gameTime);
            return;
        }
    }
     public void Draw(SpriteBatch spriteBatch)
    {
        if (IsTutorialActive)
        {
            recipe.Draw(spriteBatch);
        }
    }
    }
}