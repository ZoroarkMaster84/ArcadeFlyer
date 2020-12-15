using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    class Projectile : Sprite
    {
        public Vector2 velocity;

        public ProjectileType projectileType;
        public ProjectileType ProjectileType
        {
            get { return projectileType; }
            set { projectileType = value; }
        }
        

        public Projectile(Vector2 position, Vector2 velocity, Texture2D spriteImage, ProjectileType projectileType) : base(position)
        {
            this.velocity = velocity;
            this.SpriteWidth = 32.0f;
            this.SpriteImage = spriteImage;
            this.projectileType = projectileType;
        }

        public void Update()
        {
            position += velocity;
        }
    }
}
