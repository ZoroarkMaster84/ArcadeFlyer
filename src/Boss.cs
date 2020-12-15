using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    
    class Boss : Sprite
    {
        private ArcadeFlyerGame root;

        private Vector2 velocity;

        private Timer projectileCooldown;

        public Boss(ArcadeFlyerGame root, Vector2 position) : base(position)
        {
            // Initialize values
            this.root = root;
            this.position = position;
            this.SpriteWidth = 450.0f;
            this.velocity = new Vector2(-0.5f, 3.0f);

            projectileCooldown = new Timer(1.0f);

            LoadContent();
        }

        public void LoadContent()
        {
            this.SpriteImage = root.Content.Load<Texture2D>("Boss");
        }

        public void Update(GameTime gameTime)
        {
            // Handle movement
            position += velocity;

            // Bounce on top and bottom
            if (position.Y < 0 || position.Y > (root.ScreenHeight - SpriteHeight))
            {
                velocity.Y *= -1;
            }

            projectileCooldown.Update(gameTime);

            if (!projectileCooldown.Active)
            {
                projectileCooldown.StartTimer();
                Vector2 projectilePosition = new Vector2();
                projectilePosition.X = position.X;
                projectilePosition.Y = position.Y + (SpriteHeight / 2);
                Vector2 projectileVelocity = new Vector2();
                projectileVelocity.X = -6.0f;
                projectileVelocity.Y = 0f;

                root.FireProjectile(projectilePosition, projectileVelocity, ProjectileType.Boss);
            }
        }
    }
}