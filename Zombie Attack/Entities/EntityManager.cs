using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie_Attack
{
    static class EntityManager
    {
        //List of all entities
        static List<Entity> entities = new List<Entity>();

        static List<Enemy> enemies = new List<Enemy>();

        static List<Bullet> bullets = new List<Bullet>();

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
        }

        //Check that the distance between two entities is lesss than their radii combined (checking the distance squared against the radii squared is faster than not squaring)
        private static bool IsColliding(Entity a, Entity b)
        {
            float radius = a.Radius + b.Radius;
            return !a.IsExpired && !b.IsExpired 
                && Vector2.DistanceSquared(a.Position, b.Position) < radius * radius;
        }

        //used to loop through the entities list and make each of them update their properties
        //Also cleans up any destroyed entities
        public static void Update()
        {
            isUpdating = true;

            foreach(var entity in entities)
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

        }

        //Called to draw all of the entities to the canvas
        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach(var entity in entities)
            {
                entity.Draw(spriteBatch);
            }
        }

        

    }
}
