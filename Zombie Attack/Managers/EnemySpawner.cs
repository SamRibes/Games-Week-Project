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
        private static int enemiesForRound = 0;
        private static int spawnDelay = 6;
        private static int zombiesToSpawn = 2;
        public static int EnemiesLeft
        {
            get
            {
                return (ZombieGame.CurrentStage * zombiesToSpawn) - enemiesForRound;
            }
        }
        public static int NextWaveIn
        {
            get
            {
                return ZombieGame.GameTimeInSeconds % spawnDelay;
            }
        }

        public static void Update()
        {
            //level = current round * 5
            Random rand = new Random();
            float screenSizeX = ZombieGame.ScreenSize.X;
            float screenSizeY = ZombieGame.ScreenSize.Y;


            if (enemiesForRound < (ZombieGame.CurrentStage * zombiesToSpawn))
            {
                if ((ZombieGame.GameTimeInSeconds != ZombieGame.LastGameTimeInSeconds) && 
                    ZombieGame.GameTimeInSeconds % spawnDelay == 0)
                {
                    Console.WriteLine("In the if statement");
                    ZombieGame.LastGameTimeInSeconds = ZombieGame.GameTimeInSeconds;

                    for (int i = 0; i < zombiesToSpawn; i++)
                    {
                        enemiesForRound++;
                        EntityManager.Add(Enemy.CreateZombie(new Vector2(100f, screenSizeY / 2f)));

                        //switch (rand.Next(3))
                        //{
                        //    case 0:
                        //        //right
                        //        EntityManager.Add(Enemy.CreateZombie(new Vector2(-10f, screenSizeY / 2f)));
                        //        break;
                        //    case 1:
                        //        //left
                        //        EntityManager.Add(Enemy.CreateZombie(new Vector2(screenSizeX + 10f, screenSizeY / 2f)));
                        //        break;
                        //    case 2:
                        //        //top
                        //        EntityManager.Add(Enemy.CreateZombie(new Vector2(screenSizeX / 2f, screenSizeY + 10f)));
                        //        break;
                        //    case 3:
                        //        //bottom
                        //        EntityManager.Add(Enemy.CreateZombie(new Vector2(screenSizeX / 2f, -10f)));
                        //        break;
                        //    default:
                        //        break;
                        //}
                    }
                }
            }
            else if(EntityManager.EnemyCount == 0)
            {
                if (ZombieGame.CurrentStage == 3 || ZombieGame.CurrentStage == 5 || ZombieGame.CurrentStage == 7 || ZombieGame.CurrentStage == 9)
                {
                    spawnDelay--;
                    Console.WriteLine("Spawn delay" + spawnDelay);
                }
                enemiesForRound = 0;
                ZombieGame.StageChange = true;
                ZombieGame.CurrentStage++;
            }

        }

    }
}

