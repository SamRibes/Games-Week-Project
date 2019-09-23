using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading.Tasks;

namespace Zombie_Attack
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ZombieGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Texture2D PlayerTexture { get; private set; }
        public static Texture2D BulletTexture { get; private set; }
        public static Texture2D EnemySpitTexture { get; private set; }
        public static Texture2D BasicZombieTexture { get; private set; }
        public static Texture2D FastZombieTexture { get; private set; }
        public static Texture2D TankZombieTexture { get; private set; }
        public static Texture2D RangedZombieTexture { get; private set; }
        public Texture2D GroundTexture { get; private set; }
        public static ZombieGame Instance { get; private set; }
        public static Viewport Viewport
        {
            get
            {
                return Instance.GraphicsDevice.Viewport;
            }
        }
        public static Vector2 ScreenSize
        {
            get
            {
                return new Vector2(Viewport.Width, Viewport.Height);
            }
        }
        public static SpriteFont Font { get; private set; }
        public SpriteFont BigFont { get; private set; }

        public static int GameTimeInSeconds, LastGameTimeInSeconds;
        public static int CurrentStage { get; set; }
        public static bool StageChange{ get; set; }

        private int stageChangeCountDown = 0;


        public ZombieGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 1200;
            Instance = this;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            EntityManager.Add(Player.Instance);
            LastGameTimeInSeconds = 0;
            CurrentStage = 1;

            base.IsMouseVisible = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            PlayerTexture = Content.Load<Texture2D>("Player/Player");
            BulletTexture = Content.Load<Texture2D>("Bullets/Bullet");
            EnemySpitTexture = Content.Load<Texture2D>("Bullets/EnemySpit");
            BasicZombieTexture = Content.Load<Texture2D>("Enemies/BasicZombie");
            FastZombieTexture = Content.Load<Texture2D>("Enemies/FastZombie");
            TankZombieTexture = Content.Load<Texture2D>("Enemies/TankZombie");
            RangedZombieTexture = Content.Load<Texture2D>("Enemies/RangedZombie");
            GroundTexture = Content.Load<Texture2D>("Backgrounds/GroundTexture");
            Font = Content.Load<SpriteFont>("Fonts/Font");
            BigFont = Content.Load<SpriteFont>("Fonts/BigStringFont");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            GameTimeInSeconds = (int)gameTime.TotalGameTime.TotalSeconds;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            Input.Update();
            EntityManager.Update();
            EnemySpawner.Update();
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred);

            spriteBatch.Draw(GroundTexture, Vector2.Zero, Color.White);

            if (StageChange == true)
            {
                stageChangeCountDown = 120;
                CurrentStage++;
                if (Player.Instance.IsDead) { CurrentStage--; }
                StageChange = false;
            }

            if(Player.Instance.IsDead)
            {
                spriteBatch.DrawString(BigFont, $"GAME OVER. You got to round {CurrentStage}.", new Vector2(ScreenSize.X / 2, ScreenSize.Y / 2), Color.White);
            }

            else if (stageChangeCountDown > 0)
            {
                spriteBatch.DrawString(BigFont, $"Stage {CurrentStage}", new Vector2(ScreenSize.X / 2, ScreenSize.Y / 2), Color.White);
                EntityManager.PauseEnemies = true;
                stageChangeCountDown--;
            }
            else
            {
                EntityManager.PauseEnemies = false;
                spriteBatch.DrawString(Font, $"Stage {CurrentStage}", new Vector2(ScreenSize.X / 40, ScreenSize.Y / 40), Color.White);
                spriteBatch.DrawString(Font, $"Score: {Player.Instance.Score}", new Vector2(ScreenSize.X / 40, (ScreenSize.Y / 40)*3), Color.White);
                spriteBatch.DrawString(Font, $"Next wave in: {EnemySpawner.NextWaveIn}", new Vector2(ScreenSize.X / 40, (ScreenSize.Y / 40)*5), Color.White);
                spriteBatch.DrawString(Font, $"Enemies left: {EnemySpawner.EnemiesLeft}", new Vector2((ScreenSize.X / 40)*35, ScreenSize.Y / 40), Color.White);

                EntityManager.Draw(spriteBatch);
            }

            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}