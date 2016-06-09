using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Fenix
{
    public class MovingBackground
    {
        public Texture2D Texture { get; set; }
        public Rectangle backgroundRectangle { get; set; }
        public Rectangle backgroundRectangle2 { get; set; }
        public string Symbol { get; set; }
        public Vector2 Position1 { get; set; }
        public Vector2 Position2 { get; set; }
        public int Speed { get; set; }

        public void Initialize(Texture2D texture, Vector2 position1, Vector2 position2, int speed, string symbol)
        {
            Texture = texture;
            Position1 = position1;
            Position2 = position2;
            Speed = speed;
            Symbol = symbol;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position1, Color.White);
            spriteBatch.Draw(Texture, Position2, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            Position1 += new Vector2(Speed, 0);
            Position2 += new Vector2(Speed, 0);

            if (Position1.X >= 900)
            {
                if (Symbol == "lava")
                {
                    Position1 = new Vector2(-1, 730);
                    Position2 = new Vector2(-900, 730);
                }
                else if (Symbol == "water")
                {
                    Position1 = new Vector2(-1, 0);
                    Position2 = new Vector2(-900, 0);
                }

            }
        }
    }
}
