using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie_Attack
{
    class Player : Entity
    {
        private static Player instance;

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
            // Player coding in here
        }
    }
}
