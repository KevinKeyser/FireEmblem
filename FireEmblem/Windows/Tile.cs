using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    public class Tile : Sprite
    {
        public GamePiece Piece
        {
            get
            {
                return piece;
            }
            set
            {
                piece = value;
                if (piece != null)
                {
                    piece.Position = Position;
                }
            }
        }

        private GamePiece piece;

        public Terrian terrian;

        public bool IsSelected = false;
        public Rectangle Rectangle
        {
            get
            {
                return rectangle;
            }
        }

        public Tile(Texture2D texture, Vector2 position, int size)
            : base(texture, new Rectangle((int)position.X, (int)position.Y, size, size))
        {
            terrian = Terrian.Plain;
            piece = null;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (piece != null)
            {
                piece.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if(piece != null)
            {
                piece.Draw(spriteBatch);
            }
        }

        public override string ToString()
        {
            if(piece == null)
            {
                return string.Format("Terrian: {0}", terrian.ToString());
            }
            return string.Format("Terrian: {0}\n{1}", terrian.ToString(), piece.ToString());
        }
    }
}
