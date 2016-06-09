using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fenix
{
    public class Player : Object
    {
        public bool Death { get; set; }
        public int Health { get; set; }

        public void Initialize(Texture2D texture, float speed, Vector2 position, bool death, int health)
        {
            ObjectTexture = texture;
            Speed = speed;
            Position = position;
            Death = death;
            Health = health;
        }

        public void Initialize(float speed, Vector2 position, bool death, int health)
        {
            Speed = speed;
            Position = position;
            Death = death;
            Health = health;
        }

        public void playerHealth(GameTime gameTime)
        {
            if (Position.Y < 70)
            {
                Health--;
                if (Health < 0)
                {
                    Health = 0;
                    Death = true;
                }
            }
            else if (Position.Y + 100 > 730)
            {
                Health++;
                if (Health > 100)
                {
                    Health = 100;
                }
            }
        }
    }
}
