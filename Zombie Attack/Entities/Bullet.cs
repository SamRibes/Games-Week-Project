using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie_Attack
{
    class Bullet : Entity
    {
        public Bullet (Vector2 position, Vector2 velocity)
        {
            image = ZombieGame.BulletTexture;
            this.Position = position;
            this.Velocity = velocity;
            Orientation = Velocity.ToAngle();
            Radius = 8;
        }
        public override void Update()
        {
            if(Velocity.LengthSquared() > 0)
            {
                Orientation = Velocity.ToAngle();
            }

            Position += Velocity;

            if (!ZombieGame.Viewport.Bounds.Contains(Position.ToPoint()))
            {
                IsExpired = true;
            }
        }
    }
}
