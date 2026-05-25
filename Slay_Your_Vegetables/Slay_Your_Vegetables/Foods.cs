using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Slay_Your_Vegetables
{
    
    //Knife Group
    
    public class Tomato : SharpResponsiveEnemy { public Tomato(Texture2D t, Vector2 p) : base(t, p, "Tomato", 300f, 3.0f) { } }
    public class Lettuce : SharpResponsiveEnemy { public Lettuce(Texture2D t, Vector2 p) : base(t, p, "Lettuce", 400f, 3.0f) { } }
    public class Lemon : SharpResponsiveEnemy { public Lemon(Texture2D t, Vector2 p) : base(t, p, "Lemon", 300f, 3.0f) { } }
    public class Bread : SharpResponsiveEnemy { public Bread(Texture2D t, Vector2 p) : base(t, p, "Bread", 350f, 3.0f) { } } 
    public class Mushroom : SharpResponsiveEnemy { public Mushroom(Texture2D t, Vector2 p) : base(t, p, "Mushroom", 350f, 3.0f) { } }
    public class Banana : SharpResponsiveEnemy { public Banana(Texture2D t, Vector2 p) : base(t, p, "Banana", 350f, 3.0f) { } }
    public class Biscuit : SharpResponsiveEnemy { public Biscuit(Texture2D t, Vector2 p) : base(t, p, "Biscuit", 350f, 3.0f) { } }

    //Blowtorch Group
    
    public class Tuna : ThermalResponsiveEnemy { public Tuna(Texture2D t, Vector2 p) : base(t, p, "Tuna", 150f, 2.5f) { } }
    public class GBeef : ThermalResponsiveEnemy { public GBeef(Texture2D t, Vector2 p) : base(t, p, "Ground beef", 150f, 2.5f) { } }
    public class Eggplant : ThermalResponsiveEnemy { public Eggplant(Texture2D t, Vector2 p) : base(t, p, "Eggplant", 250f, 2.5f) { } }
    public class Butter : ThermalResponsiveEnemy { public Butter(Texture2D t, Vector2 p) : base(t, p, "Butter", 200f, 2.5f) { } }
    public class Chicken : ThermalResponsiveEnemy { public Chicken(Texture2D t, Vector2 p) : base(t, p, "Chicken", 250f, 2.5f) { } }
    public class Chocolate : ThermalResponsiveEnemy { public Chocolate(Texture2D t, Vector2 p) : base(t, p, "Chocolate", 250f, 2.5f) { } }

    // Whisk Group
   
    public class Yogurt : FluidResponsiveEnemy { public Yogurt(Texture2D t, Vector2 p) : base(t, p, "Yogurt", 300f, 2.0f) { } }
    public class Cream : FluidResponsiveEnemy { public Cream(Texture2D t, Vector2 p) : base(t, p, "Cream", 300f, 2.0f) { } }
}