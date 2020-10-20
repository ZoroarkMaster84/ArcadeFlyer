using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArcadeFlyer2D
{
    class Enemy 
    {
    private ArcadeFlyerGame root;
    private Vector2 position;
    private Texture2D spriteImage;
    private float spriteWidth;
    private Vector2 velocity;
    public float SpriteHeight
    {
        get
        { 
            float scale = spriteWidth / spriteImage.Width;
            return spriteImage.Height * scale;
        }
    }

    public Rectangle PositionRectangle
    {
        get
        {
            return new Rectangle((int)position.X, (int)position.Y, (int)spriteWidth, (int)SpriteHeight);
        }
    }
    
    

        public Enemy()
        {
            
        }
    }
}