using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie_Attack
{
    class Pickup : Entity
    {
        private List<IEnumerator<int>> PickUpTypes = new List<IEnumerator<int>>();
        public int FramesTillDespawn;
        public int ID;

        public Pickup(Texture2D image, Vector2 position, int id)
        {
            this.image = image;
            Position = position;
            Radius = image.Width / 2f;
            FramesTillDespawn = 300;
            ID = id;
        }

        public void WasDestroyed()
        {
            IsExpired = true;
        }

        
        public static Pickup CreateFireRatePickup(Vector2 position)
        {
            var pickup = new Pickup(ZombieGame.FireRateTexture, position, 1);
            pickup.AddPickUpType(pickup.FireRateUp());
            return pickup;
        }
        public static Pickup CreateTripleShotPickup(Vector2 position)
        {
            var pickup = new Pickup(ZombieGame.TripleShotTexture, position, 2);
            pickup.AddPickUpType(pickup.TripleShot());
            return pickup;
        }
        public static Pickup CreateSpeedUpPickup(Vector2 position)
        {
            var pickup = new Pickup(ZombieGame.SpeedTexture, position, 3);
            pickup.AddPickUpType(pickup.SpeedUp());
            return pickup;
        }

        public override void Update(GameTime gameTime)
        {
            ApplyPickUpTypes();
            FramesTillDespawn--;
            if (FramesTillDespawn == 0)
                IsExpired = true;

            if (!ZombieGame.Viewport.Bounds.Contains(Position.ToPoint()))
                IsExpired = true;
        }

        private void AddPickUpType(IEnumerable<int> PickUpType)
        {
            PickUpTypes.Add(PickUpType.GetEnumerator());
        }

        private void ApplyPickUpTypes()
        {
            for (int i = 0; i < PickUpTypes.Count; i++)
            {
                if (!PickUpTypes[i].MoveNext())
                    PickUpTypes.RemoveAt(i--);
            }
        }

        #region Pick up types

        IEnumerable<int> FireRateUp()
        {
            while (true)
            {
                Orientation += 0.025f;
                yield return 0;
            }
        }

        IEnumerable<int> TripleShot()
        {
            while (true)
            {
                Orientation += 0.025f;
                yield return 0;
            }
        }

        IEnumerable<int> SpeedUp()
        {
            while (true)
            {
                Orientation += 0.025f;
                yield return 0;
            }
        }

        #endregion

    }
}
