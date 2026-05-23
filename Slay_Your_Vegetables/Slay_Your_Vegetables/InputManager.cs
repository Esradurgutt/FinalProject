using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Slay_Your_Vegetables
{
    public static class InputManager
    {
        private static KeyboardState _currentKey, _previousKey;
        private static MouseState _currentMouse, _previousMouse;

        public static void Update()
        {
            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();
        }

        public static bool IsKeyPressed(Keys key)
        {
            return _currentKey.IsKeyDown(key) && _previousKey.IsKeyUp(key);
        }

        public static bool IsKeyHeld(Keys key)
        {
            return _currentKey.IsKeyDown(key);
        }

        public static bool IsLeftMouseClicked()
        {
            return _currentMouse.LeftButton == ButtonState.Pressed && _previousMouse.LeftButton == ButtonState.Released;
        }

        public static Point GetMousePosition(float scaleX, float scaleY)
        {
            return new Point((int)(_currentMouse.X / scaleX), (int)(_currentMouse.Y / scaleY));
        }
    }
}