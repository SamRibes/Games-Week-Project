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
        private List<IEnumerator<int>> behaviours = new List<IEnumerator<int>>();
        private int hitPoints;
        private int cooldownFrames = 120;
        private int cooldownRemaining = 0;
        private static Random rand = new Random();

        public Enemy(Texture2D image, Vector2 position, int hitPoints)
        {
            this.image = image;
            Position = position;
            Radius = image.Width / 2f;
            this.hitPoints = hitPoints;
        }

        public override void Update()
        {
            Position += Velocity;
            Position = Vector2.Clamp(Position, Size / 2, ZombieGame.ScreenSize - Size / 2);

            Velocity *= 0.8f;

            ApplyBehaviours();
        }

        public static Enemy CreateBasicZombie(Vector2 position)
        {
            var enemy = new Enemy(ZombieGame.BasicZombieTexture, position, 2);
            enemy.AddBehaviour(enemy.BasicZombie());

            return enemy;
        }

        public static Enemy CreateFastZombie(Vector2 position)
        {
            var enemy = new Enemy(ZombieGame.FastZombieTexture, position, 1);
            enemy.AddBehaviour(enemy.FastZombie());

            return enemy;
        }

        public static Enemy CreateTankZombie(Vector2 position)
        {
            var enemy = new Enemy(ZombieGame.TankZombieTexture, position, 3);
            enemy.AddBehaviour(enemy.TankZombie());

            return enemy;
        }

        public static Enemy CreateRangedZombie(Vector2 position)
        {
            var enemy = new Enemy(ZombieGame.RangedZombieTexture, position, 1);
            enemy.AddBehaviour(enemy.RangedZombie());

            return enemy;
        }

        public void WasShot()
        {
            hitPoints--;
            if (hitPoints == 0)
            {
                IsExpired = true;
                Player.Instance.Score += 50;
            }
        }
        public void WasDestroyed()
        {
            IsExpired = true;
        }

        public void HandleCollision(Enemy other)
        {
            var d = Position - other.Position;
            if (Position == other.Position)
            {
                Position.X = Position.X + 10;
            }
            else
            {
                Velocity += 10 * d / (d.LengthSquared() + 1);
            }
        }

        private void AddBehaviour(IEnumerable<int> behaviour)
        {
            behaviours.Add(behaviour.GetEnumerator());
        }

        private void ApplyBehaviours()
        {
            for (int i = 0; i < behaviours.Count; i++)
            {
                if (!behaviours[i].MoveNext())
                {
                    behaviours.RemoveAt(i--);
                }
            }
        }

        #region EnemyTypes

        IEnumerable<int> BasicZombie(float acceleration = 0.8f)
        {
            while (true)
            {
                Velocity += (Player.Instance.Position - Position).ScaleTo(acceleration);

                if (Velocity != Vector2.Zero)
                {
                    Orientation = Velocity.ToAngle();
                }

                yield return 0;
            }
        }

        IEnumerable<int> FastZombie(float acceleration = 2f)
        {
            while (true)
            {
                Velocity += (Player.Instance.Position - Position).ScaleTo(acceleration);

                if (Velocity != Vector2.Zero)
                {
                    Orientation = Velocity.ToAngle();
                }

                yield return 0;
            }
        }

        IEnumerable<int> TankZombie(float acceleration = 0.5f)
        {
            while (true)
            {
                Velocity += (Player.Instance.Position - Position).ScaleTo(acceleration);

                if (Velocity != Vector2.Zero)
                {
                    Orientation = Velocity.ToAngle();
                }

                yield return 0;
            }
        }

        IEnumerable<int> RangedZombie(float acceleration = 0.2f)
        {
            while (true)
            {
                var bulletVelocity = 5f;
                var aim = (Player.Instance.Position - Position);
                Velocity += aim.ScaleTo(acceleration);

                if (Velocity != Vector2.Zero)
                {
                    Orientation = Velocity.ToAngle();
                }

                if (cooldownRemaining <= 0)
                {
                    cooldownRemaining = cooldownFrames;
                    float aimAngle = aim.ToAngle();

                    EntityManager.Add(new EnemyBullet(Position, aim.ScaleTo(bulletVelocity)));
                }

                if (cooldownRemaining > 0)
                    cooldownRemaining--;

                yield return 0;
            }
        }

        #endregion
    }
}
