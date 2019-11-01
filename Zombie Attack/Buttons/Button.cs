using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zombie_Attack
{
    internal abstract class Button
    {
        private int x = (ZombieGame.Viewport.Width / 2) - (200 / 2);

        private const int Width = 200;
        protected Texture2D Texture;
        public int Y = 0, Height = 100;
        public Rectangle ButtonRectangle;

        public void Draw(SpriteBatch spriteBatch)
        {
            ButtonRectangle = new Rectangle(x, Y, Width, Height);
            spriteBatch.Draw(Texture, ButtonRectangle, Color.White);
        }

        public abstract void Update();

    }
}
