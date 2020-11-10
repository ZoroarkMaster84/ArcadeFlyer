using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ArcadeFlyer2D
{
    class Timer
    {
        private float endTime;
        private float currentTime;
        public bool Active
        {
            get;
            private set;
        }

        public Timer(float endTime)
        {
            this.endTime = endTime;
            this.currentTime = 0.0f;
            this.Active = false;
        }

        public void StartTimer()
        {
            Active = true;
            currentTime = 0.0f;
        }

        public  void Update(GameTime gameTime)
        {
            if (Active)
            {
                currentTime = currentTime + (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (currentTime >= endTime)
                {
                    Active = false;
                }
            }
        }
    }
}