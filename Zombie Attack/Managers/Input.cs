using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Zombie_Attack
{
    static class Input
    {
        private static KeyboardState keyboardState, lastKeyboardState;
        private static MouseState mouseState, lastMouseState;

        public static Vector2 MousePosition
        {
            get
            {
                return new Vector2(mouseState.X, mouseState.Y);
            }
        }

        public static Vector2 MousePositionForCursor
        {
            get
            {
                return new Vector2(mouseState.X - 15, mouseState.Y - 15);
            }
        }

        public static void Update()
        {
            lastKeyboardState = keyboardState;
            lastMouseState = mouseState;

            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        public static bool WasKeyPressed(Keys key)
        {
            return lastKeyboardState.IsKeyUp(key) && keyboardState.IsKeyDown(key);
        }

        public static bool WasLeftMouseClicked()
        {
            if ((mouseState.LeftButton == ButtonState.Pressed))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Vector2 GetMovementDirection()
        {
            Vector2 direction = new Vector2(0, 0);

            if(keyboardState.IsKeyDown(Keys.A))
            {
                direction.X -= 1;
            }
            if(keyboardState.IsKeyDown(Keys.D))
            {
                direction.X += 1;
            }
            if(keyboardState.IsKeyDown(Keys.W))
            {
                direction.Y -= 1;
            }
            if(keyboardState.IsKeyDown(Keys.S))
            {
                direction.Y += 1;
            }

            if (direction.LengthSquared() > 1)
            {
                direction.Normalize();
            }

            return direction;
        }

        public static Vector2 GetAimDirection()
        {
            Vector2 direction = MousePosition - Player.Instance.Position;

            if(direction == Vector2.Zero)
            {
                return Vector2.Zero;
            }
            else
            {
                return Vector2.Normalize(direction);
            }
        }

    }
}
