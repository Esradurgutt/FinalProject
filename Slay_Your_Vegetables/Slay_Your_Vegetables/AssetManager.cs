using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Slay_Your_Vegetables
{
    public static class AssetManager
    {
        public static Texture2D Pixel, Requirements, Checkmark, WeaponGuide;
        public static Texture2D Line1, Line2, Line3, Line4;
        public static Texture2D KnifeTex, TorchTex, WhiskTex, AxTex, FireTex;
        public static SpriteFont GameFont;
        public static SpriteFont TitleFont; 
        public static List<Texture2D> GirdapTexs = new List<Texture2D>();

        public static Texture2D CreateTexture(GraphicsDevice graphics, int width, int height, Color color)
        {
            var texture = new Texture2D(graphics, width, height);
            var data = new Color[width * height];
            for (int i = 0; i < data.Length; i++) data[i] = color;
            texture.SetData(data);
            return texture;
        }

        public static void LoadAllContent(ContentManager content, GraphicsDevice graphics)
        {
            Pixel = CreateTexture(graphics, 1, 1, Color.White);
            Requirements = CreateTexture(graphics, 1, 1, Color.White);

            Line1 = content.Load<Texture2D>("line1");
            Line2 = content.Load<Texture2D>("line2");
            Line3 = content.Load<Texture2D>("line3");
            Line4 = content.Load<Texture2D>("line4");

            KnifeTex = content.Load<Texture2D>("Knife");
            TorchTex = content.Load<Texture2D>("Blowtorch");
            WhiskTex = content.Load<Texture2D>("Whisk");
            FireTex = content.Load<Texture2D>("Fire");

            try { AxTex = content.Load<Texture2D>("Ax"); } catch { AxTex = Pixel; }
            try { WeaponGuide = content.Load<Texture2D>("WeaponGuide"); } catch { WeaponGuide = Pixel; }
            try { Checkmark = content.Load<Texture2D>("Checkmark"); } catch { Checkmark = null; }

            GameFont = content.Load<SpriteFont>("MainMenu");
            try { TitleFont = content.Load<SpriteFont>("TitleFont"); } catch { TitleFont = GameFont; }

            for (int i = 1; i <= 5; i++)
            {
                try { GirdapTexs.Add(content.Load<Texture2D>($"WhiskUltimate/Whirlpool{i}")); } catch { }
            }

            LoadEnemyTextures(content);
        }

        private static void LoadEnemyTextures(ContentManager content)
        {
            if (Enemy.textures.Count == 0)
            {
                Enemy.textures.Add(0, content.Load<Texture2D>("TomatoWalk/tomatoW_00000"));
                Enemy.textures.Add(1, content.Load<Texture2D>("LettuceWalk/lettuceW_00000"));
                Enemy.textures.Add(2, content.Load<Texture2D>("LemonWalk/lemonW_00000"));
                Enemy.textures.Add(3, content.Load<Texture2D>("TunaWalk/tunaW_00000"));
                Enemy.textures.Add(4, content.Load<Texture2D>("BreadWalk/breadW_00000"));
                Enemy.textures.Add(5, content.Load<Texture2D>("GBeefWalk/gbeefW_00000"));
                Enemy.textures.Add(6, content.Load<Texture2D>("EggplantWalk/eggplantW_00000"));
                Enemy.textures.Add(7, content.Load<Texture2D>("YogurtWalk/yogurtW_00000"));
                Enemy.textures.Add(8, content.Load<Texture2D>("CreamWalk/creamW_00000"));
                Enemy.textures.Add(9, content.Load<Texture2D>("ButterWalk/butterW_00000"));
                Enemy.textures.Add(10, content.Load<Texture2D>("ChickenWalk/chickenW_00000"));
                Enemy.textures.Add(11, content.Load<Texture2D>("MushroomWalk/mushroomW_00000"));
                Enemy.textures.Add(12, content.Load<Texture2D>("ChocolateWalk/chocolateW_00000"));
                Enemy.textures.Add(13, content.Load<Texture2D>("BananaWalk/bananaW_00000"));
                Enemy.textures.Add(14, content.Load<Texture2D>("BiscuitWalk/biscuitW_00000"));
            }
        }
    }
}