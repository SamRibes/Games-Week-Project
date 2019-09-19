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

            ApplyBehaviours();
        }

        public static Enemy CreateZombie(Vector2 position)
        {
            var enemy = new Enemy(ZombieGame.EnemyTexture, position);
            enemy.AddBehaviour(enemy.FollowPlayer());

            return enemy;
        }

        public void WasShot()
        {
            IsExpired = true;
        }

        public void HandleCollision(Enemy other)
        {
            var d = Position - other.Position;
            Velocity += 10 * d / (d.LengthSquared());
        }

        private void AddBehaviour(IEnumerable<int> behaviour)
        {
            behaviours.Add(behaviour.GetEnumerator());
        }

        private void ApplyBehaviours()
        {
            for (int i = 0; i < behaviours.Count; i++)
            {
                if(!behaviours[i].MoveNext())
                {
                    behaviours.RemoveAt(i--);
                }
            }
        }

        #region EnemyTypes

        IEnumerable<int> FollowPlayer(float acceleration = 0.5f)
        {
            while (true)
            {
                Velocity += (Player.Instance.Position - Position).ScaleTo(acceleration);

                if(Velocity != Vector2.Zero)
                {
                    Orientation = Velocity.ToAngle();
                }

                yield return 0;
            }
        }

        #endregion
    }
}
