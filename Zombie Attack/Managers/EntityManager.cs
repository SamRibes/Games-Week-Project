using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Zombie_Attack
{
    static class EntityManager
    {
        public static bool PauseEnemies { get; set; }
        //List of all entities
        static List<Entity> entities = new List<Entity>();

        static List<Enemy> enemies = new List<Enemy>();

        static List<Bullet> bullets = new List<Bullet>();

        static List<EnemyBullet> enemyBullets = new List<EnemyBullet>();

        static List<Pickup> pickups = new List<Pickup>();

        //Checked when looping through entities
        static bool isUpdating;
        //A list of entities to add which is populated every game loop
        static List<Entity> addedEntities = new List<Entity>();

        //returns the number of entities
        public static int Count
        {
            get
            {
                return entities.Count;
            }
        }

        public static int EnemyCount
        {
            get
            {
                return enemies.Count;
            }
        }

        //Used to add an entity to the entities List
        public static void Add(Entity entity)
        {
            if (!isUpdating)
            {
                AddEntity(entity);
            }
            else
            {
                addedEntities.Add(entity);
            }
        }

        private static void AddEntity(Entity entity)
        {
            entities.Add(entity);

            if (entity is Bullet)
            {
                bullets.Add(entity as Bullet);
            }
            else if (entity is Enemy)
            {
                enemies.Add(entity as Enemy);
            }
            else if (entity is EnemyBullet)
            {
                enemyBullets.Add(entity as EnemyBullet);
            }
            else if (entity is Pickup)
            {
                pickups.Add(entity as Pickup);
            }
        }

        //Check that the distance between two entities is lesss than their radii combined (checking the distance squared against the radii squared is faster than not squaring)
        private static bool IsColliding(Entity a, Entity b)
        {
            float radius = a.Radius + b.Radius;
            return !a.IsExpired && !b.IsExpired
                && Vector2.DistanceSquared(a.Position, b.Position) < radius * radius;
        }

        static void HandleCollisions()
        {
            //Handle collision between player and pickups
            for (int i = 0; i < EnemyCount; i++)
            {
                if (IsColliding(Player.Instance, pickups[i]))
                {
                    switch (pickups[i].ID)
                    {
                        case 1:
                            Player.Instance.GotFireRatePowerUp();
                            break;
                        case 2:
                            Player.Instance.GotTripleShotPowerUp();
                            break;
                        case 3:
                            Player.Instance.GotSpeedPowerUp();
                            break;
                        default:
                            break;
                    }
                    break;
                }
            }

            //Handle collision between enemies
            for (int i = 0; i < EnemyCount; i++)
            {
                for (int j = i + 1; j < EnemyCount; j++)
                {
                    if (IsColliding(enemies[i], enemies[j]))
                    {
                        enemies[i].HandleCollision(enemies[j]);
                        enemies[j].HandleCollision(enemies[i]);
                    }
                }
            }

            //Handle collision between enemies and bullets
            for (int i = 0; i < EnemyCount; i++)
            {
                for (int j = 0; j < bullets.Count; j++)
                {
                    if (IsColliding(enemies[i], bullets[j]))
                    {
                        enemies[i].WasShot();
                        bullets[j].IsExpired = true;
                    }
                }
            }

            //Handle collision between enemies and player
            for (int i = 0; i < EnemyCount; i++)
            {
                if (IsColliding(Player.Instance, enemies[i]))
                {
                    Player.Instance.Kill();
                    enemies.ForEach(e => e.WasDestroyed());
                    bullets.ForEach(e => e.WasDestroyed());
                    enemyBullets.ForEach(e => e.WasDestroyed());
                    break;
                }
            }

            //Handle collision between enemy bullets and player
            for (int i = 0; i < enemyBullets.Count; i++)
            {
                if (IsColliding(Player.Instance, enemyBullets[i]))
                {
                    Player.Instance.Kill();
                    enemies.ForEach(e => e.WasDestroyed());
                    bullets.ForEach(e => e.WasDestroyed());
                    enemyBullets.ForEach(e => e.WasDestroyed());
                    pickups.ForEach(p => p.WasDestroyed());
                    break;
                }
            }

        }

        public static void ClearAll()
        {
            foreach (var enemy in enemies)
            {
                enemy.WasDestroyed();
            }
            foreach (var bullet in bullets)
            {
                bullet.WasDestroyed();
            }
            foreach (var enemyBullet in enemyBullets)
            {
                enemyBullet.WasDestroyed();
            }
            foreach (var pickup in pickups)
            {
                pickup.WasDestroyed();
            }

        }

        //used to loop through the entities list and make each of them update their properties
        //Also cleans up any destroyed entities
        public static void Update()
        {
            if (!PauseEnemies)
            {
                isUpdating = true;
                HandleCollisions();

                foreach (var entity in entities)
                {
                    entity.Update();
                }

                isUpdating = false;

                foreach (var entity in addedEntities)
                {
                    AddEntity(entity);
                }

                addedEntities.Clear();

                entities = entities.Where(e => !e.IsExpired).ToList();
                bullets = bullets.Where(b => !b.IsExpired).ToList();
                enemies = enemies.Where(e => !e.IsExpired).ToList();
                enemyBullets = enemyBullets.Where(eB => !eB.IsExpired).ToList();
                pickups = pickups.Where(p => !p.IsExpired).ToList();
            }

        }

        //Called to draw all of the entities to the canvas
        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in entities)
            {
                entity.Draw(spriteBatch);
            }
            

        }
    }
}
