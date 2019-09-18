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
        //Texture for entity
        protected Texture2D image;
        //Used to change the transparency of the entity
        protected Color colour = Color.White;
        //Used to update/get the position and velocity of entities
        public Vector2 Position, Velocity;
        //Used to get/set the direction an entity is facing
        public float Orientation;
        //Used to for collision detection
        public float Radius = 20;
        //Used to detect if an entity has been destroyed
        public bool IsExpired;

        //Returns a vector of 0 if the image hasn't been set
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

        //Abstract so that the update logic can be different for different kinds of entities
        public abstract void Update();

        //Called when drawing the entity to the canvas
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, null, colour, Orientation, Size / 2f, 1f, 0, 0);
        }

    }
}
