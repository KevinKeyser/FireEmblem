using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    public class GamePiece : Sprite
    {
        public GamePiece(Texture2D texture, Vector2 position)
            : base(texture, position) 
        {
        }

        public GamePiece(Texture2D texture, Rectangle rectangle)
            : base(texture, rectangle)
        {
        }
    }
}
