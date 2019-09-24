using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Zombie_Attack
{
    class Player : Entity
    {
        private static Player instance;
        const int cooldownFrames = 30;
        int cooldownLeft = 0;
        int framesTillRespawn = 0;
        public int Lives = 3;
        public bool IsRespawning
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
            if (IsRespawning)
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

        }

        public void GotTripleShotPowerUp()
        {

        }

        public void GotSpeedPowerUp()
        {

        }

    }
}