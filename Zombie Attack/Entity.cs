using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie_Attack
{
    abstract class Entity
    {
        protected Texture2D image;
        protected Color colour = Color.White;
        public Vector2 Position, Velocity;
        public float Orientation;
        public float Radius = 20;
        public bool IsExpired;

        public Vector2 Size
        {
            get
            {
                if (image == null)
                {
                    return new Vector2(0, 0);
                }
                else
                {
                    return new Vector2(image.Width, image.Height);
                }
            }
        }

        public abstract void Update();

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, null, colour, Orientation, Size/2f, 1f, 0, 0);
        }

    }
}
