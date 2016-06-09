using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fenix
{
    public class Text
    {
        public List<string> list { get; set; }
        private int maxLength = 5;
        public float timer { get; set; } = 0;
        private int listLength;
        private int coordinateY;
        private string[] readArray;

        public SpriteFont font { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            listLength = list.Count;
            list.Reverse();
            coordinateY = 300;

            if (listLength < maxLength)
            {
                for (int i = 0; i < listLength; i++)
                {
                    spriteBatch.DrawString(font, list[i], new Vector2(10, coordinateY), Color.Green);
                    coordinateY += 30;
                }
            }
            else
            {
                for (int i = 0; i < maxLength; i++)
                {
                    spriteBatch.DrawString(font, list[i], new Vector2(10, coordinateY), Color.Green);
                    coordinateY += 30;
                }
            }
            System.IO.File.WriteAllLines(@"results.txt", list);
            list.Reverse();
        }

        public void Read(SpriteBatch spriteBatch)
        {
            readArray = System.IO.File.ReadAllLines(@"results.txt");
            coordinateY = 0;

            int i = 0;

            foreach (string time in readArray)
            {
                spriteBatch.DrawString(font, time, new Vector2(10, coordinateY), Color.Red);
                coordinateY += 30;
                i++;

                if (i >= maxLength)
                {
                    return;

                }
            }
        }
    }
}
