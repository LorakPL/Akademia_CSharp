using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fenix
{
    interface ObjectInterface
    {
        void Initialize(Texture2D texture, float speed, Vector2 position);
        void Draw(SpriteBatch spriteBatch);
    }
}
