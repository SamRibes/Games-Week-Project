using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie_Attack
{
    class Player : Entity
    {
        private static Player instance;
        const int cooldownFrames = 30;
        int cooldownLeft = 0;

        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player();
                }
                return instance;

            }
        }

        private Player()
        {
            image = ZombieGame.PlayerTexture;
            Position = ZombieGame.ScreenSize / 2;
            Radius = 10;
        }

        public override void Update()
        {
            const float speed = 8;

            Velocity = speed * Input.GetMovementDirection();

            Position += Velocity;
            Position = Vector2.Clamp(Position, Size/2, ZombieGame.ScreenSize - Size/2);

            if (Velocity.LengthSquared() > 0)
            {
                Orientation = Velocity.ToAngle();
            }

            var aim = Input.GetAimDirection();
            if (cooldownLeft <= 0)
            {
                cooldownLeft = cooldownFrames;
                EntityManager.Add(new Bullet(Position, aim));
            }
            if (cooldownLeft > 0)
            {
                cooldownLeft--;
            }
        }
    }
}
