using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class Recipe  //How to play 
{
   public Texture2D recipe, button;
   public SpriteFont font, titleF;
   public Rectangle recipeRec, buttonRec;
   public Vector2 titlePos, textPos;

   protected string Title = "Title here", Content = "Content here", ButtonText = "Start";
   protected Color
   NormalButtonC = Color.Red,
   PressedButtonC = Color.DarkRed;


   public event Action Pressed;
   private bool isPressed = false;
   private MouseState mouseState;

   public Recipe(GraphicsDevice graphicsDevice, SpriteFont font, SpriteFont title, Texture2D recipeTexture)
   {
      this.font = font;
      titleF = title;
      recipe = recipeTexture;

      button = new Texture2D(graphicsDevice, 1, 1); // it is for creating a texture (we don't have a texture for buttons,just using rec)
      button.SetData(new[] { Color.White });


      int screenWidth = graphicsDevice.Viewport.Width;  // screen size
      int screenHeight = graphicsDevice.Viewport.Height;
      int panelWidth = 700, panelHeight = 900; // recipe panel size
      recipeRec = new Rectangle((screenWidth / 2) - 400, (screenHeight / 2) - 475, panelWidth, panelHeight);

      textPos = new Vector2(recipeRec.X + 85, recipeRec.Y + 230);

      int buttonWidth = 160, buttonHeight = 50; //start button size 
      buttonRec = new Rectangle(
            recipeRec.Right - buttonWidth - 50,
            recipeRec.Bottom - buttonHeight - 50,
            buttonWidth, buttonHeight);
   }
   public void Update(GameTime gameTime)
   {
      MouseState currentmouseState = Mouse.GetState();
      float scaleX = (float)button.GraphicsDevice.Viewport.Width / 1920f;
      float scaleY = (float)button.GraphicsDevice.Viewport.Height / 1080f;
      Point virtualMousePos = new Point(
          (int)(currentmouseState.X / scaleX),
          (int)(currentmouseState.Y / scaleY)
      );
      isPressed = buttonRec.Contains(virtualMousePos);
      if (isPressed && currentmouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
      {
         Pressed?.Invoke();
      }

      mouseState = currentmouseState;
   }
   public void Draw(SpriteBatch spriteBatch)
   {
      spriteBatch.Draw(recipe, recipeRec, Color.White); //background
      

      Vector2 rawTitleSize = titleF.MeasureString(Title);//to center the title
      Vector2 originTitle = new Vector2(rawTitleSize.X / 2f, rawTitleSize.Y / 2f);
      float panelCenterX = recipeRec.X + (recipeRec.Width / 2f);
      Vector2 newTitlepos = new Vector2(panelCenterX, recipeRec.Y + 100f);
      spriteBatch.DrawString(titleF, Title, newTitlepos, Color.DarkSlateGray, 0f, originTitle, 1.0f, SpriteEffects.None, 0f);
      
      spriteBatch.DrawString(font, Content, textPos, Color.SlateGray);  //for text (how to play)
      
      
      Color currentButtonColor = isPressed ? PressedButtonC : NormalButtonC; // for change the color of mousestate
      spriteBatch.Draw(button, buttonRec, currentButtonColor); //button
      Vector2 textSize = font.MeasureString(ButtonText);
      Vector2 buttonTextPos = new Vector2(
      buttonRec.X + (buttonRec.Width - textSize.X) / 2,
      buttonRec.Y + (buttonRec.Height - textSize.Y) / 2);

      spriteBatch.DrawString(font, ButtonText, buttonTextPos, Color.White); //button font
   }

}




public class Salad : Recipe //targets: tomato 4 ,lettuce 3,lemon 2,tuna2
{
   public Salad(GraphicsDevice graphicsDevice, SpriteFont font, SpriteFont title, Texture2D recipeTexture) : base(graphicsDevice, font, title, recipeTexture)
   {
      Title = "How to Make a Salad?";
      Content = "Requirements: \n" +
      " - 4 Tomatoes\n" +
      " - 3 Lettuces\n" +
      " - 2 Lemons \n" +
      " - 2 Tunas \n\n" +

      "1- CHOP tomatoes and lettuces\n" +
      "2- Add tunas that you COOKED \n" +
      "3- CUT lemons and squeeze it in bowl\n" +
      "4- Mix the ingredients then \nyour salad is ready!";

      ButtonText = "Let's COOK!";

      NormalButtonC = Color.Red;
      PressedButtonC = Color.DarkRed;
   }
}
public class Hamburger : Recipe// targets: ground beef 4, bread 2, lettuce 2 , tomato 3
{
   public Hamburger(GraphicsDevice graphicsDevice, SpriteFont font, SpriteFont title, Texture2D recipeTexture) : base(graphicsDevice, font, title, recipeTexture)
   {
      Title = "How to Make a Hamburger?";
      Content = "Requirements\n" +
      " - 400 gram Ground Beef\n" +
      " - 2 slice of Bread \n" +
      " - 2 Lettuces\n" +
      " - 3 Tomatoes\n\n" +

      "1- Make a meatball with ground beef\n    and GRILL it\n" +
      "2- SLICE the bread in half and seperate\n    the slices\n" +
      "3- SLICE tomatoes and lettuces\n" +
      "4- Add meatball, tomato and lettuce on \n    the bottom slice,then cover with the \n    remaining slice.\n" +
      "   Hamburger is ready, yummy!";


      ButtonText = "Let's COOK!";
      NormalButtonC = Color.Red;
      PressedButtonC = Color.DarkRed;
   }
}
public class AliNazik : Recipe// T: eggplant 4, ground beef 3, yogurt 2, tomato 2
{
   public AliNazik(GraphicsDevice graphicsDevice, SpriteFont font, SpriteFont title, Texture2D recipeTexture) : base(graphicsDevice, font, title, recipeTexture)
   {
      Title = "How to Make an Alinazik?";
      Content = "Requirements\n" +
      "- 4 Eggplants\n"+
      "- 300 gram Ground Beef\n"+
      "- 2 bowls of yogurt\n"+
      "- 2 Tomatoes\n\n"+
      "1- DICE tomatoes\n"+
      "2- COOK the ground beef in pan then \n    add tomatoes in it\n"+
      "3- ROAST eggplants\n"+
      "4- MIX the yogurt then add eggplants in it\n"+
      "5- Pour the ground beef on top of the \n    mixture with eggplant\n "+
      "The most delicious way to enjoy eggplant!";
      ButtonText = "Let's COOK!";

      NormalButtonC = Color.Red;
      PressedButtonC = Color.DarkRed;
   }
}
public class Soup : Recipe// T: cream 3 , butter 2, chicken 4, mushroom 3
{
   public Soup(GraphicsDevice graphicsDevice, SpriteFont font, SpriteFont title, Texture2D recipeTexture) : base(graphicsDevice, font, title, recipeTexture)
   {
      Title = "How to Make a Soup?";
      Content = "Requirements\n" +
      "- 3 packs of Cream\n"+
      "- 2 TS Butter\n"+
      "- 4 piece of Chickens\n"+
      "- 3 Mushrooms\n\n"+
      "1- COOK Chickens and add in the pot \n    with chicken broth\n"+
      "2- SLICE mushrooms then add in the \npot too\n"+
      "3- Pour creams in it then MIX it up\n"+
      "4- When it is boiling, add butter in it \nand MIX the soup\n"+
      " It is literally a bowl of healing!";
      
      ButtonText = "Let's COOK!";

      NormalButtonC = Color.Red;
      PressedButtonC = Color.DarkRed;
   }
}
public class Dessert : Recipe//t: cream 3, chocolate 3, banana 2, biscuit 4
{
   public Dessert(GraphicsDevice graphicsDevice, SpriteFont font, SpriteFont title, Texture2D recipeTexture) : base(graphicsDevice, font, title, recipeTexture)
   {
      Title = "How to Make a Dessert?";
      Content = "Requirements\n" +
      "- 3 packs of Cream\n"+
      "- 3 packs of Chocolate\n"+
      "- 2 Bananas\n"+
      "- 4 packs of Biscuit\n\n"+
      "1- WHISK creams in the bowl\n"+
      "2- SLICE bananas and add in the bowl\n"+
      "3- Smash the biscuits with KNIFE then\n    pour it the bottom of pyrex\n"+
      "4- Pour the mixture on the biscuits then \n    pour on top the MELTED chocolate\n"+
      " So Sweet!";
      
      ButtonText = "Let's COOK!";

      NormalButtonC = Color.Red;
      PressedButtonC = Color.DarkRed;
   }
}