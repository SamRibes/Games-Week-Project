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
        public static int EnemiesLeft => (ZombieGame.CurrentStage * zombiesToSpawn) - enemiesForRound;

        public static int NextWaveIn => ZombieGame.GameTimeInSeconds % spawnDelay;
        public static int SpawnDelay { set => spawnDelay = 6; }

        public static void Update()
        {
            Random rand = new Random();
            float screenSizeX = ZombieGame.ScreenSize.X;
            float screenSizeY = ZombieGame.ScreenSize.Y;

            if (ZombieGame.CurrentStage == 3)
            {
                spawnDelay = 5;
            }
            if (ZombieGame.CurrentStage == 5)
            {
                spawnDelay = 4;
            }
            if (ZombieGame.CurrentStage > 5)
            {
                spawnDelay = 4;
            }
            if (ZombieGame.CurrentStage == 2)
            {
                zombiesToSpawn = 4;
            }
            if (ZombieGame.CurrentStage == 4)
            {
                zombiesToSpawn = 6;
            }
            if (ZombieGame.CurrentStage == 6)
            {
                zombiesToSpawn = 8;
            }
            if (ZombieGame.CurrentStage > 6)
            {
                zombiesToSpawn = 10;
            }

            if (enemiesForRound < (ZombieGame.CurrentStage * zombiesToSpawn))
            {
                if ((ZombieGame.GameTimeInSeconds != ZombieGame.LastGameTimeInSeconds) &&
                    ZombieGame.GameTimeInSeconds % spawnDelay == 0)
                {
                    ZombieGame.LastGameTimeInSeconds = ZombieGame.GameTimeInSeconds;

                    for (int i = 0; i < zombiesToSpawn; i++)
                    {
                        enemiesForRound++;
                        Vector2 posisitonToSpawn = Vector2.Zero;

                        #region Choose a position for the zombie to spawn
                        switch (rand.Next(4))
                        {
                            case 0:
                                posisitonToSpawn = new Vector2(-10f, screenSizeY / 2f);
                                break;
                            case 1:
                                posisitonToSpawn = new Vector2(screenSizeX + 10f, screenSizeY / 2f);
                                break;
                            case 2:
                                posisitonToSpawn = new Vector2(screenSizeX / 2f, screenSizeY + 10f);
                                break;
                            case 3:
                                posisitonToSpawn = new Vector2(screenSizeX / 2f, -10f);
                                break;
                            default:
                                break;
                        }
                        #endregion
                        #region Choose a random zombie type
                        //EntityManager.Add(Enemy.CreateBasicZombie(posisitonToSpawn));
                        switch (rand.Next(4))
                        {
                            case 0:
                                EntityManager.Add(Enemy.CreateBasicZombie(posisitonToSpawn));
                                break;
                            case 1:
                                EntityManager.Add(Enemy.CreateFastZombie(posisitonToSpawn));
                                break;
                            case 2:
                                EntityManager.Add(Enemy.CreateTankZombie(posisitonToSpawn));
                                break;
                            case 3:
                                EntityManager.Add(Enemy.CreateRangedZombie(posisitonToSpawn));
                                break;
                            default:
                                break;
                        }
                        #endregion

                    }
                }
            }
            else if (EntityManager.EnemyCount == 0)
            {
                //if (ZombieGame.CurrentStage == 3 || ZombieGame.CurrentStage == 5)
                //{
                //    spawnDelay--;
                //    Console.WriteLine("Spawn delay" + spawnDelay);
                //}
                //if (ZombieGame.CurrentStage == 2 || ZombieGame.CurrentStage == 4 || ZombieGame.CurrentStage == 6)
                //{
                //    zombiesToSpawn--;
                //    Console.WriteLine("Spawn delay" + spawnDelay);
                //}
                
                enemiesForRound = 0;
                ZombieGame.StageChange = true;
            }

        }

    }
}

