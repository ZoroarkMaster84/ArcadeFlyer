using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ArcadeFlyer2D
{
    // The Game itself
    class ArcadeFlyerGame : Game
    {
        // Graphics Manager
        private GraphicsDeviceManager graphics;

        // Sprite Drawer
        private SpriteBatch spriteBatch;

        // The player
        private Player player;

        private int life = 5;

        private int score = 0;

        private bool gameOver = false;

        private List<Enemy> enemies;

        private List<Boss> bosses;

        private Timer enemyCreationTimer;

        private Timer bossTimer;

        private List<Projectile> projectiles;

        private Texture2D playerProjectileSprite;

        private Texture2D enemyProjectileSprite;

        private Texture2D bossProjectileSprite;

        private SpriteFont textFont;

        private int bossHealth = 10000;

        private bool isBossHere;

        // Screen width
        private int screenWidth = 1600;
        public int ScreenWidth
        {
            get { return screenWidth; }
            private set { screenWidth = value; }
        }

        // Screen height
        private int screenHeight = 900;
        public int ScreenHeight
        {
            get { return screenHeight; }
            private set { screenHeight = value; }
        }

        // Initalized the game
        public ArcadeFlyerGame()
        {
            // Get the graphics
            graphics = new GraphicsDeviceManager(this);

            // Set the height and width
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();

            // Set up the directory containing the assets
            Content.RootDirectory = "Content";

            // Make mouse visible
            IsMouseVisible = true;

            // Initialize the player to be in the top left
            player = new Player(this, new Vector2(0.0f, 0.0f));

            enemies = new List<Enemy>();

            // Initialize an enemy to be on the right side
            enemies.Add(new Enemy(this, new Vector2(screenWidth, 0)));

            bosses = new List<Boss>();

            enemyCreationTimer = new Timer(3.0f);
            enemyCreationTimer.StartTimer();

            bossTimer = new Timer(31.0f);
            bossTimer.StartTimer();

            projectiles = new List<Projectile>();
        }

        // Initialize
        protected override void Initialize()
        {
            base.Initialize();
        }

        // Load the content for the game, called automatically on start
        protected override void LoadContent()
        {
            // Create the sprite batch
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerProjectileSprite = Content.Load<Texture2D>("PlayerFire");
            enemyProjectileSprite = Content.Load<Texture2D>("EnemyFire");
            bossProjectileSprite = Content.Load<Texture2D>("BossFire");

            textFont = Content.Load<SpriteFont>("Text");
        }

        // Called every frame
        protected override void Update(GameTime gameTime)
        {
            // Update base game
            base.Update(gameTime);

            if (gameOver)
            {
                return;
            }

            // Update the components
            player.Update(gameTime);

            foreach (Enemy enemy in enemies)
            {
                enemy.Update(gameTime);
            }

            foreach (Boss boss in bosses)
            {
                boss.Update(gameTime);
            }

            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                Projectile p = projectiles[i];
                p.Update();

                bool isPlayerProjectile = p.ProjectileType == ProjectileType.Player;
                bool isEnemyProjectile = p.ProjectileType == ProjectileType.Enemy;
                bool isBossProjectile = p.ProjectileType == ProjectileType.Boss;
                    
                if (!isPlayerProjectile && player.Overlaps(p))
                {
                    projectiles.Remove(p);
                    life--;

                    if (life == 0)
                    {
                        gameOver = true;
                    }
                }
                else if (isPlayerProjectile)
                {
                    for (int x = enemies.Count - 1; x >= 0; x--)
                    {
                        Enemy e = enemies[x];

                        if (e.Overlaps(p))
                        {
                            projectiles.Remove(p);
                            enemies.Remove(e);
                            score += 1000;
                        }
                    }

                    for (int x = bosses.Count - 1; x >= 0; x--)
                    {
                        Boss e = bosses[x];

                        if (e.Overlaps(p))
                        {
                            projectiles.Remove(p);
                            bossHealth -= 500;
                            if(bossHealth == 0)
                            {
                                bosses.Remove(e);
                                score += 100000;
                                isBossHere = false;
                                bossTimer.StartTimer();
                                bossHealth = 10000;
                            }
                        }
                    }
                }
            }

            if (!enemyCreationTimer.Active)
            {
                enemies.Add(new Enemy(this, new Vector2(screenWidth, 0)));
                enemyCreationTimer.StartTimer();
            }

            if (!bossTimer.Active)
            {
                bosses.Add(new Boss(this, new Vector2(screenWidth, 0)));
                bossTimer.StartTimer();
            }

            enemyCreationTimer.Update(gameTime);
            bossTimer.Update(gameTime);
        }

        // Draw everything in the game
        protected override void Draw(GameTime gameTime)
        {
            // First clear the screen
            GraphicsDevice.Clear(Color.Black);

            // Start batch draw
            spriteBatch.Begin();

            if (!gameOver)
            {
                player.Draw(gameTime, spriteBatch);

                foreach (Enemy enemy in enemies)
            {
                enemy.Draw(gameTime, spriteBatch);
            }

            foreach (Projectile p in projectiles)
            {
                p.Draw(gameTime, spriteBatch);
            }

            foreach (Boss boss in bosses)
            {
                boss.Draw(gameTime, spriteBatch);
            }
            
            }

            

            string scoreString = "Score: " + score.ToString();
            string livesString = "Lives: " + life.ToString();
            spriteBatch.DrawString(textFont, scoreString, Vector2.Zero, Color.White);
            spriteBatch.DrawString(textFont, livesString, new Vector2(0f, 20f), Color.White);

            if (gameOver)
            {
                isBossHere = false;
                spriteBatch.DrawString(textFont, "You should have thought about what I am willing to do in order to get the job done", new Vector2(screenWidth / 2 - 300, screenHeight / 2 - 20), Color.Black);
                spriteBatch.DrawString(textFont, "GAME OVER", new Vector2(screenWidth / 2 - 90, screenHeight / 2), Color.Black);
                GraphicsDevice.Clear(Color.Red);
            }



            if (!isBossHere && bossTimer.Active && (int)bossTimer.currentTime < 30)
            {
                spriteBatch.DrawString(textFont, $"Boss is coming in: {30 - (int)bossTimer.currentTime}", new Vector2(screenWidth / 2, 0), Color.White);
            }

            else if((int)bossTimer.currentTime == 30) 
            {
                isBossHere = true;
            }

            else if(isBossHere)
            {
                spriteBatch.DrawString(textFont, $"Boss Health: {bossHealth}", new Vector2(screenWidth / 2, 0), Color.White);
                GraphicsDevice.Clear(Color.Maroon);
            }

            

           


            // End batch draw
            spriteBatch.End();
        }

        public void FireProjectile(Vector2 position, Vector2 velocity, ProjectileType projectileType)
        {
            Texture2D projectileTexture;

            if (projectileType == ProjectileType.Enemy)
            {
                projectileTexture = enemyProjectileSprite;

                Projectile firedProjectile = new Projectile(position, velocity, projectileTexture, projectileType);
                projectiles.Add(firedProjectile);
            }

            else if (projectileType == ProjectileType.Boss)
            {
                projectileTexture = bossProjectileSprite;
                BossProjectile firedProjectile = new BossProjectile(position, velocity, projectileTexture, projectileType);
                projectiles.Add(firedProjectile);
            }

            else
            {
                projectileTexture = playerProjectileSprite;

                Projectile firedProjectile = new Projectile(position, velocity, projectileTexture, projectileType);
                projectiles.Add(firedProjectile);
            }
        }
    }
}
