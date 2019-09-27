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

        #region Texture Objects
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
        public static Texture2D CrossHairTexture { get; private set; }
        public static SpriteFont Font { get; private set; }
        public SpriteFont BigFont { get; private set; }
        #endregion

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
        public static Vector2 TopLeftOfScreen
        {
            get
            {
                return new Vector2(ScreenSize.X / 40, ScreenSize.Y / 40);
            }
        }
        public static Vector2 CenterOfScreen
        {
            get
            {
                return new Vector2(ScreenSize.X / 2, ScreenSize.Y / 2);
            }
        }

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
            graphics.IsFullScreen = true;
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
            IsMouseVisible = false;
            _state = GameState.MainMenu;
            EntityManager.Add(Player.Instance);
            LastGameTimeInSeconds = 0;
            CurrentStage = 1;
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
            //PlayerTexture = Content.Load<Texture2D>("Player/PlayerPlaceholder");
            BulletTexture = Content.Load<Texture2D>("Bullets/PlayerBullet");
            //BulletTexture = Content.Load<Texture2D>("Bullets/BulletPlaceHolder");
            EnemySpitTexture = Content.Load<Texture2D>("Bullets/EnemySpit");
            BasicZombieTexture = Content.Load<Texture2D>("Enemies/BasicZombie");
            //BasicZombieTexture = Content.Load<Texture2D>("Enemies/EnemyPlaceholder");
            FastZombieTexture = Content.Load<Texture2D>("Enemies/FastZombie");
            TankZombieTexture = Content.Load<Texture2D>("Enemies/TankZombie");
            RangedZombieTexture = Content.Load<Texture2D>("Enemies/RangedZombie");
            GroundTexture = Content.Load<Texture2D>("Backgrounds/GroundTexture");
            StartButtonTexture = Content.Load<Texture2D>("Buttons/StartButton");
            ExitButtonTexture = Content.Load<Texture2D>("Buttons/ExitButton");
            CrossHairTexture = Content.Load<Texture2D>("Crosshair");
            FireRateTexture = Content.Load<Texture2D>("Pickups/FireRateUpPickup");
            SpeedTexture = Content.Load<Texture2D>("Pickups/SpeedUpPickup");
            TripleShotTexture = Content.Load<Texture2D>("Pickups/TripleShotPickup");
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
                Quit();
            }
            if (Input.WasKeyPressed(Keys.T))
                CurrentStage++;

            switch (_state)
            {
                case GameState.MainMenu:
                    Input.Update();
                    ButtonManager.Update();
                    break;
                case GameState.MainGame:
                    Input.Update();
                    EntityManager.Update(gameTime);
                    EnemySpawner.Update(gameTime);

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
            GraphicsDevice.Clear(Color.White);

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
                        spriteBatch.DrawString(BigFont, $"Stage {CurrentStage}", new Vector2(ScreenSize.X / 2-50, ScreenSize.Y / 2- 10), Color.Black);
                    }
                    else
                    {
                        EntityManager.PauseEnemies = false;
                        spriteBatch.DrawString(Font, $"Stage {CurrentStage}", TopLeftOfScreen, Color.Black);
                        spriteBatch.DrawString(Font, $"Score: {Player.Instance.Score}", new Vector2(TopLeftOfScreen.X, TopLeftOfScreen.Y + 20), Color.Black);
                        spriteBatch.DrawString(Font, $"Next wave in: {EnemySpawner.NextWaveIn}", new Vector2(TopLeftOfScreen.X, TopLeftOfScreen.Y + 40), Color.Black);
                        spriteBatch.DrawString(Font, $"Enemies left: {EnemySpawner.EnemiesLeft}", new Vector2(TopLeftOfScreen.X, TopLeftOfScreen.Y + 60), Color.Black);
                        spriteBatch.DrawString(Font, $"Lives left: {Player.Instance.Lives}", new Vector2(TopLeftOfScreen.X, TopLeftOfScreen.Y + 80), Color.Black);

                        EntityManager.Draw(spriteBatch);
                    }
                    break;

                case GameState.GameComplete:
                    break;
                case GameState.GameOver:
                    spriteBatch.DrawString(BigFont, $"GAME OVER.", new Vector2(ScreenSize.X / 9, ScreenSize.Y / 3), Color.Black);
                    spriteBatch.DrawString(BigFont, $"You got to round {CurrentStage}.", new Vector2(ScreenSize.X / 9, ScreenSize.Y / 2), Color.Black);
                    ButtonManager.Draw(spriteBatch);
                    break;
                default:
                    break;
            }

            spriteBatch.Draw(ZombieGame.CrossHairTexture, Input.MousePositionForCursor, Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Quit()
        {
            this.Exit();
        }

    }
}