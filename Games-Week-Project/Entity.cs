using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games_Week_Project
{
    abstract class Entity
    {
        protected Texture2D image;
        protected Color colour;
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
                    return Vector2.Zero;
                }
                else
                {
                    return new Vector2(image.Width, image.Height);
                }
            }
        }

        public abstract void Update();


    }
}
