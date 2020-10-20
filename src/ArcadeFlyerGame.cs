using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    // The Game itself
    class ArcadeFlyerGame : Game
    {
        // Graphics Manager
        private GraphicsDeviceManager graphics;

        // Sprite Drawer
        private SpriteBatch spriteBatch;

        //Player Character Graphic
        private Texture2D playerImage;
        private Player player;
        private int screenWidth = 1600;
        public int ScreenWidth
        {
            get { return screenWidth; }
            set { screenWidth = value; }
        }

        private int screenHeight = 900;
        public int ScreenHeight
        {
            get { return screenHeight; }
            set { screenHeight = value; }
        }
        

        

        // Initalized the game
        public ArcadeFlyerGame()
        {
            // Get the graphics
            graphics = new GraphicsDeviceManager(this);

            // Set the height and width
            graphics.PreferredBackBufferWidth = screenHeight;
            graphics.PreferredBackBufferHeight = screenWidth;
            graphics.ApplyChanges();

            // Set up the directory containing the assets
            Content.RootDirectory = "Content";

            // Make mouse visible
            IsMouseVisible = true;

            Vector2 position = new Vector2(0.0f, 0.0f);
            player = new Player(this, position);
        }

        // Initialize
        protected override void Initialize()
        {
            base.Initialize();
        }

        // Load the content for the game, called automatically on start
        protected override void LoadContent()
        {
            // playerImage = Content.Load<Texture2D>("MainChar");
            // Create the sprite batch
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        // Called every frame
        protected override void Update(GameTime gameTime)
        {   
            player.Update(gameTime);

            // Update base game
            base.Update(gameTime);
        }

        // Draw everything in the game
        protected override void Draw(GameTime gameTime)
        {
            // First clear the screen
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            //Drawing will happen here
            //Rectangle playerDestinationRect = new Rectangle(0,0,playerImage.Width, playerImage.Height);
            //spriteBatch.Draw(playerImage, playerDestinationRect, Color.White);

            player.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
