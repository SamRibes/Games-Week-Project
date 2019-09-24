using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Zombie_Attack
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ZombieGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Button> menuList = new List<Button>();
        public static bool SetQuit = false;

        public static Texture2D PlayerTexture { get; private set; }
        public static Texture2D BulletTexture { get; private set; }
        public static Texture2D EnemySpitTexture { get; private set; }
        public static Texture2D BasicZombieTexture { get; private set; }
        public static Texture2D FastZombieTexture { get; private set; }
        public static Texture2D TankZombieTexture { get; private set; }
        public static Texture2D RangedZombieTexture { get; private set; }
        public static Texture2D GroundTexture { get; private set; }
        public static Texture2D StartButtonTexture { get; private set; }
        public static Texture2D ExitButtonTexture { get; private set; }
        public static Texture2D FireRateTexture { get; private set; }
        public static Texture2D TripleShotTexture { get; private set; }
        public static Texture2D SpeedTexture { get; private set; }
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
        private static Vector2 TopLeftOfScreen
        {
            get
            {
                return new Vector2(ScreenSize.X / 40, ScreenSize.Y / 40);
            }
        }
        public static SpriteFont Font { get; private set; }
        public SpriteFont BigFont { get; private set; }

        public static GameState _state;
        public enum GameState
        {
            MainMenu,
            MainGame,
            GameComplete,
            GameOver,
        }

        public static int GameTimeInSeconds, LastGameTimeInSeconds;
        public static int CurrentStage { get; set; }
        public static bool StageChange{ get; set; }

        private int stageChangeCountDown = 0;

        public ZombieGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            //graphics.PreferredBackBufferWidth = 1200;
            //graphics.PreferredBackBufferHeight = 1200;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
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
            ButtonManager.Add(new StartButton(StartButtonTexture));
            ButtonManager.Add(new ExitButton(ExitButtonTexture));

            _state = GameState.MainMenu;

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
            BulletTexture = Content.Load<Texture2D>("Bullets/PlayerBullet");
            EnemySpitTexture = Content.Load<Texture2D>("Bullets/EnemySpit");
            BasicZombieTexture = Content.Load<Texture2D>("Enemies/BasicZombie");
            FastZombieTexture = Content.Load<Texture2D>("Enemies/FastZombie");
            TankZombieTexture = Content.Load<Texture2D>("Enemies/TankZombie");
            RangedZombieTexture = Content.Load<Texture2D>("Enemies/RangedZombie");
            GroundTexture = Content.Load<Texture2D>("Backgrounds/GroundTexture");
            StartButtonTexture = Content.Load<Texture2D>("Buttons/StartButton");
            ExitButtonTexture = Content.Load<Texture2D>("Buttons/ExitButton");
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

            switch (_state)
            {
                case GameState.MainMenu:
                    Input.Update();
                    ButtonManager.Update();
                    break;
                case GameState.MainGame:
                    Input.Update();
                    EntityManager.Update();
                    EnemySpawner.Update();

                    if (StageChange == true)
                    {
                        stageChangeCountDown = 120;
                        CurrentStage++;
                        if (Player.Instance.IsRespawning) { CurrentStage--; }
                        StageChange = false;
                    }

                    if (stageChangeCountDown > 0)
                    {
                        EntityManager.ClearAll();
                        Player.Instance.Position = ScreenSize/2;
                        stageChangeCountDown--;
                    }
                    
                    break;
                case GameState.GameComplete:
                    break;
                case GameState.GameOver:
                    Input.Update();
                    ButtonManager.Update();
                    break;
            }
            if (SetQuit)
            {
                this.Quit();
            }
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

            switch (_state)
            {
                case GameState.MainMenu:
                    ButtonManager.Draw(spriteBatch);
                    break;
                case GameState.MainGame:
                    spriteBatch.Draw(GroundTexture, Vector2.Zero, Color.White);
                    
                    if (stageChangeCountDown > 0)
                    {
                        spriteBatch.DrawString(BigFont, $"Stage {CurrentStage}", new Vector2(ScreenSize.X / 2, ScreenSize.Y / 2), Color.White);
                    }
                    else
                    {
                        EntityManager.PauseEnemies = false;
                        spriteBatch.DrawString(Font, $"Stage {CurrentStage}", TopLeftOfScreen, Color.White);
                        spriteBatch.DrawString(Font, $"Score: {Player.Instance.Score}", TopLeftOfScreen * 3, Color.White);
                        spriteBatch.DrawString(Font, $"Next wave in: {EnemySpawner.NextWaveIn}", TopLeftOfScreen * 5, Color.White);
                        spriteBatch.DrawString(Font, $"Enemies left: {EnemySpawner.EnemiesLeft}", new Vector2((ScreenSize.X / 40) * 35, ScreenSize.Y / 40), Color.White);
                        spriteBatch.DrawString(Font, $"Lives left: {Player.Instance.Lives}", TopLeftOfScreen * 7, Color.White);

                        EntityManager.Draw(spriteBatch);
                    }
                    break;

                case GameState.GameComplete:
                    break;
                case GameState.GameOver:
                    spriteBatch.DrawString(BigFont, $"GAME OVER.", new Vector2(ScreenSize.X / 9, ScreenSize.Y / 3), Color.White);
                    spriteBatch.DrawString(BigFont, $"You got to round {CurrentStage}.", new Vector2(ScreenSize.X / 9, ScreenSize.Y / 2), Color.White);
                    ButtonManager.Draw(spriteBatch);
                    break;
                default:
                    break;
            }
            

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Quit()
        {
            this.Exit();
        }

    }
}