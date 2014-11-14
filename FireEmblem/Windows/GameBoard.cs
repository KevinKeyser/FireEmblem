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
            tiles[new Vector2(3, 5)].Piece = new GamePiece(Content.Load<Texture2D>("Circle"), tiles[new Vector2(3, 5)].Rectangle);
            tiles[new Vector2(4, 5)].Piece = new GamePiece(Content.Load<Texture2D>("Circle"), tiles[new Vector2(4, 5)].Rectangle);
            tiles[new Vector2(2, 5)].Piece = new GamePiece(Content.Load<Texture2D>("Circle"), tiles[new Vector2(2, 5)].Rectangle);
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
            if(InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                selectedTile = null;
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
            List<Vector2> moveableTiles = new List<Vector2>();
            moveableTiles.Add(new Vector2(selectedTile.Position.X  / selectedTile.Width, selectedTile.Position.Y / selectedTile.Height));
            int currentMovement = 0;
            int currentIndex = 0;
            while (currentMovement <  selectedTile.Piece.stats.MovementSpeed)
            {
                List<Vector2> newTiles = new List<Vector2>();
                for (int i = currentIndex; i < moveableTiles.Count; i++)
                {
                    Vector2 aboveTile = Vector2.Clamp(new Vector2(moveableTiles[i].X, moveableTiles[i].Y - 1), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1));
                    Vector2 belowTile = Vector2.Clamp(new Vector2(moveableTiles[i].X, moveableTiles[i].Y + 1), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1));
                    Vector2 rightTile = Vector2.Clamp(new Vector2(moveableTiles[i].X + 1, moveableTiles[i].Y), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1));
                    Vector2 leftTile = Vector2.Clamp(new Vector2(moveableTiles[i].X - 1, moveableTiles[i].Y), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1));
                    if (!moveableTiles.Contains(aboveTile) && !newTiles.Contains(aboveTile) && tiles[aboveTile].Piece == null)
                    {
                        newTiles.Add(aboveTile);
                    }
                    if (!moveableTiles.Contains(belowTile) && !newTiles.Contains(belowTile) && tiles[belowTile].Piece == null)
                    {
                        newTiles.Add(belowTile);
                    }
                    if (!moveableTiles.Contains(rightTile) && !newTiles.Contains(rightTile) && tiles[rightTile].Piece == null)
                    {
                        newTiles.Add(rightTile);
                    }
                    if (!moveableTiles.Contains(leftTile) && !newTiles.Contains(leftTile) && tiles[leftTile].Piece == null)
                    {
                        newTiles.Add(leftTile);
                    }
                }
                currentIndex = moveableTiles.Count - 1;
                currentMovement++;
                moveableTiles.AddRange(newTiles);
            }

            for(int i = 0; i < moveableTiles.Count; i++)
            {
                tiles[moveableTiles[i]].Color = Color.Purple;
            }
        }
    }
}
