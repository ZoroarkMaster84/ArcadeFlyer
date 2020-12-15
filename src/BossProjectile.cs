using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeFlyer2D
{
    class BossProjectile : Projectile
    {
        public BossProjectile(Vector2 position, Vector2 velocity, Texture2D spriteImage, ProjectileType projectileType) : base(position, velocity, spriteImage, projectileType)
        {
            this.velocity = velocity;
            this.SpriteWidth = 175.0f;
            this.SpriteImage = spriteImage;
            this.projectileType = projectileType;
        }

    }
}
