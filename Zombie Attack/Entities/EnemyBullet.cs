using Microsoft.Xna.Framework;

namespace Zombie_Attack
{
    class EnemyBullet : Entity
    {
        public EnemyBullet(Vector2 position, Vector2 velocity)
        {
            image = ZombieGame.EnemySpitTexture;
            this.Position = position;
            this.Velocity = velocity;
            Orientation = Velocity.ToAngle();
            Radius = 8;
        }
        public override void Update(GameTime gameTime)
        {
            if (Velocity.LengthSquared() > 0)
            {
                Orientation = Velocity.ToAngle();
            }

            Position += Velocity;

            if (!ZombieGame.Viewport.Bounds.Contains(Position.ToPoint()))
            {
                IsExpired = true;
            }
        }
        public void WasDestroyed()
        {
            IsExpired = true;
        }
    }
}
