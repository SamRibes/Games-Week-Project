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
                entities.Add(entity);
            }
            else
            {
                addedEntities.Add(entity);
            }
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
                entities.Add(entity);
            }

            addedEntities.Clear();

            entities = entities.Where(entity => !entity.IsExpired).ToList();
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
