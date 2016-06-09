using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fenix
{
    public class Object : ObjectInterface
    {
        public Texture2D ObjectTexture { get; set; }
        public Rectangle ObjectRectangle { get; set; }
        public float Speed { get; set; }
        public Vector2 Position { get; set; }

        public int Width
        {
            get { return ObjectTexture.Width; }
        }
        public int Height
        {
            get { return ObjectTexture.Height; }
        }

        public void Initialize(Texture2D texture, float speed, Vector2 position)
        {
            ObjectTexture = texture;
            Speed = speed;
            Position = position;
        }

        public void Initialize(float speed, Vector2 position)
        {
            Speed = speed;
            Position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ObjectTexture, ObjectRectangle, Color.White);
        }
    }
}
