using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie_Attack
{
    class Enemy : Entity
    {
        public Enemy(Texture2D image, Vector2 position)
        {
            this.image = image;
            Position = position;
            Radius = image.Width / 2f;
        }

        public override void Update()
        {
            Position += Velocity;
            Position = Vector2.Clamp(Position, Size / 2, ZombieGame.ScreenSize - Size / 2);

            Velocity *= 0.8f;
        }

        public void WasShot()
        {
            IsExpired = true;
        }
    }
}
