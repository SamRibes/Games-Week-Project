using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zombie_Attack
{
    abstract class Button
    {
        private int x = (ZombieGame.Viewport.Width / 2) - (200 / 2),
            width = 200;
        protected Texture2D texture;
        public int Y = 0, Height = 100;
        public Rectangle ButtonRectangle;

        public void Draw(SpriteBatch spriteBatch)
        {
            ButtonRectangle = new Rectangle(x, Y, width, Height);
            spriteBatch.Draw(texture, ButtonRectangle, Color.White);
        }

        abstract public void Update();

    }
}
