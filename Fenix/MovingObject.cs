using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fenix
{
    public class MovingObject : Object
    {
        public void Initialize(Texture2D texture, Vector2 position)
        {
            ObjectTexture = texture;
            Position = position;
        }
    }
}
