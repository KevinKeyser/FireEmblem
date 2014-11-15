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
        public TurnOrder turnorder;
        Dictionary<Vector2, Tile> tiles;
        int boardHeight;
        int boardWidth;
        int tileSize;
        Vector2 MoveBackLocation;
        List<Vector2> lastMoveTiles;
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
        BattleMenu battleMenu;
        Camera2D camera;

        public GameBoard(ContentManager Content, int tileSize, int boardWidth, int boardHeight)
        {
            turnorder = TurnOrder.Picking;
            this.tileSize = tileSize;
            this.boardHeight = boardHeight;
            this.boardWidth = boardWidth;
            selectedCoords = Vector2.Zero;
            selectedTile = null;
            tiles = new Dictionary<Vector2, Tile>();
            for (int x = 0; x < boardWidth; x++)
            {
                for (int y = 0; y < boardHeight; y++)
                {
                    tiles.Add(new Vector2(x, y), new Tile(Content.Load<Texture2D>("Tile"), new Vector2(tileSize * x, tileSize * y), tileSize));
                }
            }
            tiles[new Vector2(5)].Piece = new Character(Content.Load<Texture2D>("Circle"), tiles[new Vector2(5)].Rectangle, new CharacterStatistics(10, 10, 10, 10, 4, 4, 4, 4), new CharacterClass("Warror", new WeaponType[]{}, new CharacterStatistics(0,0,0,0,0,0,0,0)));
            tiles[new Vector2(3, 5)].Piece = new Character(Content.Load<Texture2D>("Circle"), tiles[new Vector2(3, 5)].Rectangle, new CharacterStatistics(10, 10, 10, 10, 3, 3, 3, 3), new CharacterClass("Warror", new WeaponType[] { }, new CharacterStatistics(0, 0, 0, 0, 0, 0, 0, 0)));
            tiles[new Vector2(4, 5)].Piece = new Character(Content.Load<Texture2D>("Circle"), tiles[new Vector2(4, 5)].Rectangle, new CharacterStatistics(10, 10, 10, 10, 3, 3, 3, 3), new CharacterClass("Warror", new WeaponType[] { }, new CharacterStatistics(0, 0, 0, 0, 0, 0, 0, 0)));
            tiles[new Vector2(2, 5)].Piece = new Character(Content.Load<Texture2D>("Circle"), tiles[new Vector2(2, 5)].Rectangle, new CharacterStatistics(10, 10, 10, 10, 3, 3, 3, 3), new CharacterClass("Warror", new WeaponType[] { }, new CharacterStatistics(0, 0, 0, 0, 0, 0, 0, 0)));
            battleMenu = new BattleMenu(Content.Load<Texture2D>("Tile").GraphicsDevice, Content);
            battleMenu.IsVisible = true;
            camera = new Camera2D(Content.Load<Texture2D>("Tile").GraphicsDevice);
            camera.Position = new Vector2(SelectedCoords.X * tileSize + tileSize / 2, selectedCoords.Y * tileSize + tileSize / 2);
            selectedTile = tiles[selectedCoords];
            battleMenu.OptionChoosen += battleMenu_OptionChoosen;
        }

        void battleMenu_OptionChoosen(object sender, EventArgs e)
        {
            string name = sender.ToString();
            if (name == "Attack")
            {
                turnorder = TurnOrder.Attack;
            }
            else if (name == "Items")
            {
                turnorder = TurnOrder.Items;
            }
            else if (name == "Wait")
            {
                turnorder = TurnOrder.Picking;
                Character character = ((Character)tiles[MoveBackLocation].Piece);
                character.HasMoved = true;
                tiles[MoveBackLocation].Piece = character;
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                for (int y = 0; y < boardHeight; y++)
                {
                    tiles[new Vector2(x, y)].Update(gameTime);
                    tiles[new Vector2(x, y)].Color = Color.White;
                }
            }
            if (turnorder != TurnOrder.BattleChoice && turnorder != TurnOrder.Items)
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
            }
            switch (turnorder)
            {
                case TurnOrder.Picking:
                    selectedTile.Color = Color.Blue;
                    selectedTile = tiles[selectedCoords];
                    if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
                    {
                        if (selectedTile.Piece != null && !((Character)selectedTile.Piece).HasMoved)
                        {
                            turnorder = TurnOrder.Moving;
                        }
                    }
                    if (selectedTile.Piece != null && !((Character)selectedTile.Piece).HasMoved)
                    {
                        ShowMovement();
                    }
                    break;
                case TurnOrder.Moving:
                    ShowMovement();
                    if (tiles[selectedCoords].Color == Color.Purple)
                    {
                        tiles[selectedCoords].Color = Color.Lerp(Color.Red, Color.Purple, .5f);
                    }
                    else
                    {
                        tiles[selectedCoords].Color = Color.Red;
                    }
                    if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
                    {
                        if (tiles[selectedCoords].Color == Color.Lerp(Color.Red, Color.Purple, .5f))
                        {
                            turnorder = TurnOrder.BattleChoice;
                            battleMenu.Piece = selectedTile.Piece;
                            if (tiles[selectedCoords] != selectedTile)
                            {
                                tiles[selectedCoords].Piece = selectedTile.Piece;
                                selectedTile.Piece = null;
                            }
                            ShowAttackRange(tiles[selectedCoords]);
                            if (attackableTiles.Count == 0)
                            {
                                battleMenu.IsAttack = false;
                                battleMenu.IsItem = true;
                            }
                            else
                            {
                                battleMenu.IsAttack = true;
                                battleMenu.IsItem = true;
                            }
                            MoveBackLocation = new Vector2(tiles[selectedCoords].Position.X / tileSize, tiles[selectedCoords].Position.Y / tileSize);
                        }
                    }

                    if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
                    {
                        turnorder = TurnOrder.Picking;
                    }
                    break;
                case TurnOrder.BattleChoice:
                    battleMenu.Update(gameTime);
                    ShowAttackRange(tiles[MoveBackLocation]);
                    if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
                    {
                        if (tiles[selectedCoords] != selectedTile)
                        {
                            selectedTile.Piece = tiles[selectedCoords].Piece;
                            tiles[selectedCoords].Piece = null;
                        }
                        turnorder = TurnOrder.Moving;
                        battleMenu.Piece = null;
                    }
                    break;
                case TurnOrder.Attack:
                    ShowAttackRange(tiles[MoveBackLocation]);
                    if (tiles[selectedCoords].Color == Color.Green)
                    {
                        tiles[selectedCoords].Color = Color.Lerp(Color.Red, Color.Green, .5f);
                    }
                    else
                    {
                        tiles[selectedCoords].Color = Color.Green;
                    }
                    if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
                    {
                        if (tiles[selectedCoords].Color == Color.Lerp(Color.Red, Color.Green, .5f))
                        {
                            turnorder = TurnOrder.BattleChoice;
                            battleMenu.IsAttack = false;
                            battleMenu.IsItem = true;
                        }
                    }
                    if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
                    {
                        selectedCoords = MoveBackLocation;
                        turnorder = TurnOrder.BattleChoice;
                    }
                    break;
                case TurnOrder.Items:
                    if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
                    {
                        turnorder = TurnOrder.BattleChoice;
                    }
                    break;
            }
            camera.Position = new Vector2(SelectedCoords.X * tileSize + tileSize / 2, selectedCoords.Y * tileSize + tileSize / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, camera.GetViewMatrix());
            for (int x = 0; x < boardWidth; x++)
            {
                for (int y = 0; y < boardHeight; y++)
                {
                    tiles[new Vector2(x, y)].Draw(spriteBatch);
                }
            }
            spriteBatch.End();
            spriteBatch.Begin();
            if (turnorder == TurnOrder.BattleChoice)
            {
                battleMenu.Draw(spriteBatch);
            }
        }

        List<Vector2> moveableTiles;
        public void ShowMovement()
        {
            moveableTiles = new List<Vector2>();
            attackableTiles = new List<Vector2>();
            moveableTiles.Add(new Vector2(selectedTile.Position.X / selectedTile.Width, selectedTile.Position.Y / selectedTile.Height));
            int currentMovement = 0;
            int currentIndex = 0;
            while (currentMovement < ((Character)selectedTile.Piece).TotalStats.Speed)
            {
                lastMoveTiles = new List<Vector2>();
                for (int i = currentIndex; i < moveableTiles.Count; i++)
                {
                    Vector2 aboveTile = Vector2.Clamp(new Vector2(moveableTiles[i].X, moveableTiles[i].Y - 1), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1));
                    Vector2 belowTile = Vector2.Clamp(new Vector2(moveableTiles[i].X, moveableTiles[i].Y + 1), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1));
                    Vector2 rightTile = Vector2.Clamp(new Vector2(moveableTiles[i].X + 1, moveableTiles[i].Y), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1));
                    Vector2 leftTile = Vector2.Clamp(new Vector2(moveableTiles[i].X - 1, moveableTiles[i].Y), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1));
                    if (!moveableTiles.Contains(aboveTile) && !lastMoveTiles.Contains(aboveTile))
                    {
                        if (tiles[aboveTile].Piece == null)
                        {
                            lastMoveTiles.Add(aboveTile);
                        }
                        else
                        {
                            attackableTiles.Add(aboveTile);
                        }
                    }
                    if (!moveableTiles.Contains(belowTile) && !lastMoveTiles.Contains(belowTile))
                    {
                        if (tiles[belowTile].Piece == null)
                        {
                            lastMoveTiles.Add(belowTile);
                        }
                        else
                        {
                            attackableTiles.Add(belowTile);
                        }
                    }
                    if (!moveableTiles.Contains(rightTile) && !lastMoveTiles.Contains(rightTile))
                    {
                        if (tiles[rightTile].Piece == null)
                        {
                            lastMoveTiles.Add(rightTile);
                        }
                        else
                        {
                            attackableTiles.Add(rightTile);
                        }
                    }
                    if (!moveableTiles.Contains(leftTile) && !lastMoveTiles.Contains(leftTile))
                    {
                        if (tiles[leftTile].Piece == null)
                        {
                            lastMoveTiles.Add(leftTile);
                        }
                        else
                        {
                            attackableTiles.Add(leftTile);
                        }
                    }
                }
                currentIndex = moveableTiles.Count - 1;
                currentMovement++;
                moveableTiles.AddRange(lastMoveTiles);
            }

            for (int i = 0; i < moveableTiles.Count; i++)
            {
                tiles[moveableTiles[i]].Color = Color.Purple;
            }
            ShowAttackRangeAfterMovement();
        }

        List<Vector2> attackableTiles;
        public void ShowAttackRangeAfterMovement()
        {
            int startcount = attackableTiles.Count;
            attackableTiles.AddRange(lastMoveTiles);
            int initalcount = attackableTiles.Count;
            int currentAttackRange = 1;
            int currentIndex = 0;
            while (currentAttackRange <= 2)//selectedTile.Piece.stats.AttackRange - 1)
            {
                List<Vector2> lastAttackTiles = new List<Vector2>();
                for (int i = currentIndex; i < attackableTiles.Count; i++)
                {
                    Vector2 aboveTile = Vector2.Clamp(new Vector2(attackableTiles[i].X, attackableTiles[i].Y - 1), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1));
                    Vector2 belowTile = Vector2.Clamp(new Vector2(attackableTiles[i].X, attackableTiles[i].Y + 1), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1));
                    Vector2 rightTile = Vector2.Clamp(new Vector2(attackableTiles[i].X + 1, attackableTiles[i].Y), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1));
                    Vector2 leftTile = Vector2.Clamp(new Vector2(attackableTiles[i].X - 1, attackableTiles[i].Y), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1));
                    if (!attackableTiles.Contains(aboveTile) && !lastAttackTiles.Contains(aboveTile) && !moveableTiles.Contains(aboveTile))
                    {
                        lastAttackTiles.Add(aboveTile);
                    }
                    if (!attackableTiles.Contains(belowTile) && !lastAttackTiles.Contains(belowTile) && !moveableTiles.Contains(belowTile))
                    {
                        lastAttackTiles.Add(belowTile);
                    }
                    if (!attackableTiles.Contains(rightTile) && !lastAttackTiles.Contains(rightTile) && !moveableTiles.Contains(rightTile))
                    {
                        lastAttackTiles.Add(rightTile);
                    }
                    if (!attackableTiles.Contains(leftTile) && !lastAttackTiles.Contains(leftTile) && !moveableTiles.Contains(leftTile))
                    {
                        lastAttackTiles.Add(leftTile);
                    }
                }
                currentIndex = attackableTiles.Count - 1;
                currentAttackRange++;
                attackableTiles.AddRange(lastAttackTiles);
            }
            attackableTiles.RemoveRange(startcount, initalcount - startcount);
            for (int i = 0; i < attackableTiles.Count; i++)
            {
                tiles[attackableTiles[i]].Color = Color.Green;
            }
        }

        public void ShowAttackRange(Tile tile)
        {
            if (tile.Piece != null)
            {
                attackableTiles = new List<Vector2>();
                attackableTiles.Add(new Vector2(tile.Position.X / tile.Width, tile.Position.Y / tile.Height));
                int currentAttackRange = 1;
                int currentIndex = 0;
                while (currentAttackRange <= 2)// tile.Piece.stats.Speed)
                {
                    List<Vector2> lastAttackTiles = new List<Vector2>();
                    for (int i = currentIndex; i < attackableTiles.Count; i++)
                    {
                        Vector2 aboveTile = Vector2.Clamp(new Vector2(attackableTiles[i].X, attackableTiles[i].Y - 1), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1));
                        Vector2 belowTile = Vector2.Clamp(new Vector2(attackableTiles[i].X, attackableTiles[i].Y + 1), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1));
                        Vector2 rightTile = Vector2.Clamp(new Vector2(attackableTiles[i].X + 1, attackableTiles[i].Y), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1));
                        Vector2 leftTile = Vector2.Clamp(new Vector2(attackableTiles[i].X - 1, attackableTiles[i].Y), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1));
                        if (!attackableTiles.Contains(aboveTile) && !lastAttackTiles.Contains(aboveTile))
                        {
                            if (tiles[aboveTile].Piece != null)
                            {
                                lastAttackTiles.Add(aboveTile);
                            }
                        }
                        if (!attackableTiles.Contains(belowTile) && !lastAttackTiles.Contains(belowTile))
                        {
                            if (tiles[belowTile].Piece != null)
                            {
                                lastAttackTiles.Add(belowTile);
                            }
                        }
                        if (!attackableTiles.Contains(rightTile) && !lastAttackTiles.Contains(rightTile))
                        {
                            if (tiles[rightTile].Piece != null)
                            {
                                lastAttackTiles.Add(rightTile);
                            }
                        }
                        if (!attackableTiles.Contains(leftTile) && !lastAttackTiles.Contains(leftTile))
                        {
                            if (tiles[leftTile].Piece != null)
                            {
                                lastAttackTiles.Add(leftTile);
                            }
                        }
                    }
                    currentIndex = attackableTiles.Count - 1;
                    currentAttackRange++;
                    attackableTiles.AddRange(lastAttackTiles);
                }
                attackableTiles.RemoveAt(0);
                for (int i = 0; i < attackableTiles.Count; i++)
                {
                    tiles[attackableTiles[i]].Color = Color.Green;
                }
            }
        }
    }
}
