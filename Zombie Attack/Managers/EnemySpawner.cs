using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie_Attack
{
    class EnemySpawner
    {
        public static void Update()
        {
            //level = current round * 5
            Random rand = new Random();
            float screenSizeX = ZombieGame.ScreenSize.X;
            float screenSizeY = ZombieGame.ScreenSize.Y;

            if (ZombieGame.GameTimeInSeconds != ZombieGame.LastGameTimeInSeconds)
            {
                if (EntityManager.EnemyCount < 10 && (ZombieGame.GameTimeInSeconds % 5 == 0))
                {
                    ZombieGame.LastGameTimeInSeconds = ZombieGame.GameTimeInSeconds;
                    switch (rand.Next(3))
                    {
                        case 0:
                            //right
                            EntityManager.Add(Enemy.CreateZombie(new Vector2(-10f, screenSizeY / 2f)));
                            break;
                        case 1:
                            //left
                            EntityManager.Add(Enemy.CreateZombie(new Vector2(screenSizeX + 10f, screenSizeY / 2f)));
                            break;
                        case 2:
                            //top
                            EntityManager.Add(Enemy.CreateZombie(new Vector2(screenSizeX / 2f, screenSizeY + 10f)));
                            break;
                        case 3:
                            //bottom
                            EntityManager.Add(Enemy.CreateZombie(new Vector2(screenSizeX / 2f, -10f)));
                            break;
                        default:
                            break;
                    }

                }

            }

        }
    }
}
