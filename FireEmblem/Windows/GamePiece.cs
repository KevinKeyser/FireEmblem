using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    public enum GamePieceType
    {
        Character,
        Object
    }
    public class GamePiece : Sprite
    {
        public GamePieceType GamePieceType { get; protected set; }

        public GamePiece(Texture2D texture, Vector2 position, GamePieceType gamePieceType)
            : base(texture, position) 
        {
            GamePieceType = gamePieceType;
        }

        public GamePiece(Texture2D texture, Rectangle rectangle, GamePieceType gamePieceType)
            : base(texture, rectangle)
        {
            GamePieceType = gamePieceType;
        }

        public Character ToCharacter()
        {
            if(GamePieceType == GamePieceType.Character)
            {
                return (Character)this;
            }
            return null;
        }
    }
}
