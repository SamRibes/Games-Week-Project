using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Zombie_Attack
{
    class Player : Entity
    {
        private static Player instance;
        private int cooldownFrames = 30;
        private int cooldownLeft = 0;
        private int framesTillRespawn = 0;
        public int Lives = 3;

        #region Pickup Cooldowns
        private int fireRateUpCooldownFrames = 300;
        private int fireRateUpCoolDownLeft = 0;
        private int speedUpCooldownFrames = 300;
        private int speedUpCoolDownLeft = 0;
        private int tripleShotCooldownFrames = 300;
        private int tripleShotCoolDownLeft = 0;
        #endregion

        public bool IsRespawning
        {
            get
            {
                return framesTillRespawn > 0;
            }
        }
        public bool HasFireRateUp
        {
            get
            {
                return fireRateUpCoolDownLeft > 0;
            }
        }
        public bool HasSpeedUp
        {
            get
            {
                return speedUpCoolDownLeft > 0;
            }
        }
        public bool HasTripleShot
        {
            get
            {
                return tripleShotCoolDownLeft > 0;
            }
        }

        public int Score;
        float speed = 8;

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
            if (IsRespawning)
            {
                framesTillRespawn--;
                return;
            }

            if (HasFireRateUp)
            {
                cooldownFrames = 5;
            }
            else
            {
                cooldownFrames = 30;
            }
            if (HasSpeedUp)
            {
                speed = 4;
            }
            else
            {
                speed = 8;
            }
            if (HasTripleShot)
            {
                cooldownFrames = 5;
            }
            else
            {
                cooldownFrames = 30;
            }

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
            if(!IsRespawning)
            {
                base.Draw(spriteBatch);
            }
        }

        public void Kill()
        {
            if (Lives == 0)
            {
                File.AppendAllText("scores.txt", (Instance.Score.ToString() + "\n"));
                ButtonManager.ClearButtons();
                ButtonManager.Add(new StartButton(ZombieGame.StartButtonTexture));
                ButtonManager.Add(new ExitButton(ZombieGame.ExitButtonTexture));
                ZombieGame._state = ZombieGame.GameState.GameOver;
            }
            else
            {
                Lives--;
                framesTillRespawn = 120;
            }
        }

        public void GotFireRatePowerUp()
        {
            fireRateUpCoolDownLeft = fireRateUpCooldownFrames;
        }

        public void GotTripleShotPowerUp()
        {

        }

        public void GotSpeedPowerUp()
        {

        }

    }
}