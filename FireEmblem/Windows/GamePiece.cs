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
        public CharacterStatistics stats;
        public GamePiece(Texture2D texture, Vector2 position)
            : base(texture, position) 
        {
            stats = new CharacterStatistics(3, 4, 5, 3, 2, 5);
        }

        public GamePiece(Texture2D texture, Rectangle rectangle)
            : base(texture, rectangle)
        {
            stats = new CharacterStatistics(3, 4, 5, 3, 2, 5);
        }

        public override string ToString()
        {
            return stats.ToString();
        }
    }
}
