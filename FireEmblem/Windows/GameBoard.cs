using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiLib.CoreTypes;
using Microsoft.Xna.Framework.Content;

namespace FireEmblem
{
    public class GameBoard
    {
        Dictionary<Vector2, Tile> tiles;
        int boardHeight;
        int boardWidth;
        public Tile selectedTile;
        private Vector2 selectedCoords;
        public Vector2 SelectedCoords
        {
            get
            {
                return selectedCoords;
            }
            set
            {
                value.X = (int)value.X;
                value.Y = (int)value.Y;
                selectedCoords = Vector2.Clamp(value, new Vector2(0, 0), new Vector2(boardWidth - 1, boardHeight - 1));
            }
        }

        public GameBoard(ContentManager Content, int tileSize, int boardWidth, int boardHeight)
        {
            this.boardHeight = boardHeight;
            this.boardWidth = boardWidth;
            selectedCoords = Vector2.Zero;
            selectedTile = null;
            tiles = new Dictionary<Vector2, Tile>();
            for(int x = 0; x < boardWidth; x++)
            {
                for(int y = 0; y < boardHeight; y++)
                {
                    tiles.Add(new Vector2(x, y), new Tile(Content.Load<Texture2D>("Tile"), new Vector2(tileSize * x, tileSize * y), tileSize));
                }
            }
            tiles[new Vector2(5)].Piece = new GamePiece(Content.Load<Texture2D>("Circle"), tiles[new Vector2(5)].Rectangle);
        }

        public void Update(GameTime gameTime)
        {
            if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                selectedCoords.X = MathHelper.Clamp(selectedCoords.X + 1, 0, boardWidth - 1);
            }
            if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Left))
            {
                selectedCoords.X = MathHelper.Clamp(selectedCoords.X - 1, 0, boardWidth - 1);
            }
            if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
            {
                selectedCoords.Y = MathHelper.Clamp(selectedCoords.Y - 1, 0, boardHeight - 1);
            }
            if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
            {
                selectedCoords.Y = MathHelper.Clamp(selectedCoords.Y + 1, 0, boardHeight - 1);
            }
            if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
            {
                if (tiles[selectedCoords].Color == Color.Lerp(Color.Red, Color.Purple, .5f))
                {
                    tiles[selectedCoords].Piece = selectedTile.Piece;
                    selectedTile.Piece = null;
                }
                selectedTile = tiles[selectedCoords];
            }
            for (int x = 0; x < boardWidth; x++)
            {
                for (int y = 0; y < boardHeight; y++)
                {
                    tiles[new Vector2(x, y)].Update(gameTime);
                    tiles[new Vector2(x, y)].Color = Color.White;
                }
            }
            if (selectedTile != null)
            {
                if (selectedTile.Piece != null)
                {
                    ShowMovement();
                }
                selectedTile.Color = Color.Blue;
            }
            if (tiles[selectedCoords].Color == Color.Purple)
            {
                tiles[selectedCoords].Color = Color.Lerp(Color.Red, Color.Purple, .5f);
            }
            else
            {
                tiles[selectedCoords].Color = Color.Red;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                for (int y = 0; y < boardHeight; y++)
                {
                    tiles[new Vector2(x, y)].Draw(spriteBatch);
                }
            }
        }

        public void ShowMovement()
        {
            for (int x = -selectedTile.Piece.stats.MovementSpeed; x <= selectedTile.Piece.stats.MovementSpeed; x++)
            {
                for (int y = -selectedTile.Piece.stats.MovementSpeed; y <= selectedTile.Piece.stats.MovementSpeed; y++)
                {
                    if (Math.Abs(x) + Math.Abs(y) <= 3)
                    {
                        Vector2 changedSelection = Vector2.Clamp(new Vector2(x, y) + new Vector2(selectedTile.X / selectedTile.Width, selectedTile.Y / selectedTile.Height), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1));
                        tiles[changedSelection].Color = Color.Purple;
                    }
                }
            }
        }
    }
}
