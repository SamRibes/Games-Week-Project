using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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
        private float bulletVelocity = 15f;

        #region Pickup Variables
        private int fireRateUpCooldownFrames = 120;
        private int fireRateUpCoolDownLeft = 0;
        private int speedUpCooldownFrames = 300;
        private int speedUpCoolDownLeft = 0;
        private int tripleShotCooldownFrames = 300;
        private int tripleShotCoolDownLeft = 0;
        private bool tripleShotActive = false;
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

        public override void Update(GameTime gameTime)
        {
            if (IsRespawning)
            {
                framesTillRespawn--;
                return;
            }

            if (HasFireRateUp)
            {
                fireRateUpCoolDownLeft--;
                cooldownFrames = 5;
            }
            else
            {
                cooldownFrames = 30;
            }
            if (HasSpeedUp)
            {
                speed = 12;
                speedUpCoolDownLeft--;
            }
            else
            {
                speed = 8;
            }
            if (HasTripleShot)
            {
                tripleShotActive = true;
                tripleShotCoolDownLeft--;
            }
            else
            {
                tripleShotActive = false;
            }
            
            var aim = Input.GetAimDirection();

            speed = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Velocity = speed * Input.GetMovementDirection();

            Position += Velocity;
            Position = Vector2.Clamp(Position, Size / 2, ZombieGame.ScreenSize - Size / 2);

            Orientation = aim.ToAngle();


            if (Input.WasLeftMouseClicked() == true)
            {
                cooldownLeft--;
            }

            if (cooldownLeft <= 0)
            {
                cooldownLeft = cooldownFrames;
                float aimAngle = aim.ToAngle();
                if (!tripleShotActive)
                {
                    Quaternion aimQuat = Quaternion.CreateFromYawPitchRoll(0, 0, aimAngle);

                    Vector2 offset = Vector2.Transform(new Vector2(25, 0), aimQuat);
                    EntityManager.Add(new Bullet(Position + offset, aim * bulletVelocity));
                }
                else
                {
                    Quaternion aimQuat = Quaternion.CreateFromYawPitchRoll(0, 0, aimAngle);

                    Vector2 offset = Vector2.Transform(new Vector2(25, -15), aimQuat);
                    EntityManager.Add(new Bullet(Position + offset, aim * bulletVelocity));
                    offset = Vector2.Transform(new Vector2(25, 15), aimQuat);
                    EntityManager.Add(new Bullet(Position + offset, aim * bulletVelocity));
                    offset = Vector2.Transform(new Vector2(30, 0), aimQuat);
                    EntityManager.Add(new Bullet(Position + offset, aim * bulletVelocity));
                }
                if (cooldownLeft > 0)
                {
                    cooldownLeft--;
                }
            }

            if (Input.WasLeftMouseClicked() == false)
            {
                cooldownLeft = 1;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsRespawning)
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
                Position = ZombieGame.CenterOfScreen;
                framesTillRespawn = 120;
            }
        }


        public void GotFireRatePowerUp()
        {
            fireRateUpCoolDownLeft = fireRateUpCooldownFrames;
        }
        public void GotTripleShotPowerUp()
        {
            tripleShotCoolDownLeft = tripleShotCooldownFrames;
        }
        public void GotSpeedPowerUp()
        {
            speedUpCoolDownLeft = speedUpCooldownFrames;
        }

    }
}