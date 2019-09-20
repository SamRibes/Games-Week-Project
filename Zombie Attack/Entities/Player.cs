using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        int framesTillRespawn = 0;
        public bool IsDead
        {
            get
            {
                return framesTillRespawn > 0;
            }
        }
        public int Score;

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
            if (IsDead)
            {
                framesTillRespawn--;
                return;
            }

            const float speed = 8;
            var aim = Input.GetAimDirection();
            Velocity = speed * Input.GetMovementDirection();

            Position += Velocity;
            Position = Vector2.Clamp(Position, Size/2, ZombieGame.ScreenSize - Size/2);
            
            Orientation = aim.ToAngle();
           
            
            if (Input.WasLeftMouseClicked() == true)
            {
                cooldownLeft--;
            }

            if (cooldownLeft <= 0)
            {
                cooldownLeft = cooldownFrames;
                EntityManager.Add(new Bullet(Position, aim*15));
                if (cooldownLeft > 0)
                {
                    cooldownLeft--;
                }
            }

            if(Input.WasLeftMouseClicked() == false)
            {
                cooldownLeft = 1;
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(!IsDead)
            {
                base.Draw(spriteBatch);
            }
        }

        public void Kill()
        {
            framesTillRespawn = 120;
        }
    }
}
