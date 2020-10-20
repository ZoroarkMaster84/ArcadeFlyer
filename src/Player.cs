using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArcadeFlyer2D
{
    class Player 
    {
        private float spriteWidth;
        private ArcadeFlyerGame root;
        private Vector2 position;
        private Texture2D spriteImage;
        private float movementSpeed = 4.0f;
        public float SpriteHeight 
        {
            get{
                float scale = spriteWidth / spriteImage.Width;
                return spriteImage.Height * scale;
            }
            set{

            }
        }

        public Rectangle PositionRectangle 
        {
            get{
                Rectangle rec = new Rectangle((int)position.X, (int)position.Y, (int)spriteWidth, (int)SpriteHeight);
                return rec;
            }
        }
        public Player(ArcadeFlyerGame root, Vector2 position)
        {
            this.root = root;
            this.position = position;
            this.spriteWidth = 128.0f;

            LoadContent();
        }

        public void LoadContent()
        {
            this.spriteImage = root.Content.Load<Texture2D>("MainChar");
        }

        public void Update(GameTime gameTime) 
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            HandleInput(currentKeyboardState);
        }

        private void HandleInput(KeyboardState currentKeyboardState)
        {
            bool upKeyPressed = currentKeyboardState.IsKeyDown(Keys.Up);
            if(upKeyPressed)
            {
                position.Y -= movementSpeed;
            }

            bool downKeyPressed = currentKeyboardState.IsKeyDown(Keys.Down);
            if(downKeyPressed)
            {
                position.Y += movementSpeed;
            }

            bool leftKeyPressed = currentKeyboardState.IsKeyDown(Keys.Left);
            if(leftKeyPressed)
            {
                position.X -= movementSpeed;
            }

            bool rightKeyPressed = currentKeyboardState.IsKeyDown(Keys.Right);
            if(rightKeyPressed)
            {
                position.X += movementSpeed;
            }

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteImage, PositionRectangle, Color.White);
        }
    }
}