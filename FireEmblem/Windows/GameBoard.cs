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
        Texture2D pixel;
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
        ItemMenu itemMenu;
        Camera2D camera;

        public GameBoard(ContentManager Content, int tileSize, int boardWidth, int boardHeight)
        {
            pixel = new Texture2D(Content.Load<Texture2D>("Tile").GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] {Color.White});

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
            tiles[new Vector2(5)].Piece = new Character(Content.Load<Texture2D>("Circle"), tiles[new Vector2(5)].Rectangle, new CharacterStatistics(), Info.Classes[ClassName.Mercenary]) { Weapon = Info.Weapons[WeaponName.IronSword] };
            tiles[new Vector2(5)].Piece.ToCharacter().Items = new Item[] { new HealthItem("Potion", 0, 0, 10, 3, 1000), Info.Weapons[WeaponName.BronzeSword], Info.Weapons[WeaponName.IronSword] };
            tiles[new Vector2(3, 5)].Piece = new Character(Content.Load<Texture2D>("Circle"), tiles[new Vector2(3, 5)].Rectangle, new CharacterStatistics(), Info.Classes[ClassName.Myrmidon]) { Weapon = Info.Weapons[WeaponName.IronSword] };
            tiles[new Vector2(4, 5)].Piece = new Character(Content.Load<Texture2D>("Circle"), tiles[new Vector2(4, 5)].Rectangle, new CharacterStatistics(), Info.Classes[ClassName.Lord]) { Weapon = Info.Weapons[WeaponName.IronSword] };
            tiles[new Vector2(2, 5)].Piece = new Character(Content.Load<Texture2D>("Circle"), tiles[new Vector2(2, 5)].Rectangle, new CharacterStatistics(), Info.Classes[ClassName.Tactician]) { Weapon = Info.Weapons[WeaponName.IronSword] };
            battleMenu = new BattleMenu(new Vector2(0, 0), new Vector2(200, 400), Content.Load<SpriteFont>("Font"), pixel);
            itemMenu = new ItemMenu(Vector2.Zero, new Vector2(200, 400), Content.Load<SpriteFont>("Font"), pixel);
            camera = new Camera2D(Content.Load<Texture2D>("Tile").GraphicsDevice);
            camera.Position = new Vector2(SelectedCoords.X * tileSize + tileSize / 2, selectedCoords.Y * tileSize + tileSize / 2);
            selectedTile = tiles[selectedCoords];
            battleMenu.OptionChoosen += battleMenu_OptionChoosen;
            itemMenu.OptionChoosen += itemMenu_OptionChoosen;
        }

        void itemMenu_OptionChoosen(object sender, EventArgs e)
        {
            int index = (int)sender;
            switch(itemMenu.Piece.ToCharacter().Items[index].ItemType)
            {
                case ItemType.Weapon :
                    itemMenu.Piece.ToCharacter().Weapon = (Weapon)itemMenu.Piece.ToCharacter().Items[index];
                    turnorder = TurnOrder.BattleChoice;
                    break;
                case ItemType.Usable :
                    break;
                case ItemType.Ect :
                    break;
            }
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
                itemMenu.Piece = battleMenu.Piece;
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
                        if (selectedTile.Piece != null && selectedTile.Piece.ToCharacter() != null ? !selectedTile.Piece.ToCharacter().HasMoved : false)
                        {
                            turnorder = TurnOrder.Moving;
                        }
                    }
                    if (selectedTile.Piece != null && selectedTile.Piece.ToCharacter() != null ? !selectedTile.Piece.ToCharacter().HasMoved : false)
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
                    if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape) && tiles[MoveBackLocation].Piece.ToCharacter() != null ? !tiles[MoveBackLocation].Piece.ToCharacter().HasAttacked : false)
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
                            Character character = tiles[MoveBackLocation].Piece.ToCharacter();
                            character.HasAttacked = true;
                            tiles[MoveBackLocation].Piece = character;
                            Character enemy = tiles[selectedCoords].Piece.ToCharacter();
                            enemy.CurrentHealth -= character.TotalStats.Strength + character.Weapon.Might - enemy.TotalStats.Defense;
                            tiles[selectedCoords].Piece = enemy;
                            if(enemy.CurrentHealth <= 0)
                            {
                                enemy.IsDead = true;
                                tiles[selectedCoords].Piece = null;
                            }
                        }
                    }
                    if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
                    {
                        selectedCoords = MoveBackLocation;
                        turnorder = TurnOrder.BattleChoice;
                    }
                    break;
                case TurnOrder.Items:
                    itemMenu.Update(gameTime);
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
            if(turnorder == TurnOrder.Items)
            {
                itemMenu.Draw(spriteBatch);
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
            while (currentMovement < selectedTile.Piece.ToCharacter().TotalStats.Movement)
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
            if (selectedTile.Piece.ToCharacter().Weapon != null)
            {
                int startcount = attackableTiles.Count;
                attackableTiles.AddRange(lastMoveTiles);
                int initalcount = attackableTiles.Count;
                int currentAttackRange = 1;
                int currentIndex = 0;
                while (currentAttackRange <= selectedTile.Piece.ToCharacter().Weapon.MaxRange)
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
        }

        public void ShowAttackRange(Tile tile)
        {
            if (tile.Piece != null && tile.Piece.ToCharacter().Weapon != null)
            {
                List<int> rangeTiles = new List<int>();
                attackableTiles = new List<Vector2>();
                attackableTiles.Add(new Vector2(tile.Position.X / tile.Width, tile.Position.Y / tile.Height));
                rangeTiles.Add(0);
                int currentAttackRange = 1;
                int currentIndex = 0;
                while (currentAttackRange <= tile.Piece.ToCharacter().Weapon.MaxRange)
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
                            lastAttackTiles.Add(aboveTile);
                            rangeTiles.Add(currentAttackRange);
                        }
                        if (!attackableTiles.Contains(belowTile) && !lastAttackTiles.Contains(belowTile))
                        {
                            lastAttackTiles.Add(belowTile);
                            rangeTiles.Add(currentAttackRange);
                        }
                        if (!attackableTiles.Contains(rightTile) && !lastAttackTiles.Contains(rightTile))
                        {
                            lastAttackTiles.Add(rightTile);
                            rangeTiles.Add(currentAttackRange);
                        }
                        if (!attackableTiles.Contains(leftTile) && !lastAttackTiles.Contains(leftTile))
                        {
                            lastAttackTiles.Add(leftTile);
                            rangeTiles.Add(currentAttackRange);
                        }
                    }
                    currentIndex = attackableTiles.Count - 1;
                    currentAttackRange++;
                    attackableTiles.AddRange(lastAttackTiles);
                }
                attackableTiles.RemoveAt(0);
                rangeTiles.RemoveAt(0);
                for (int i = 0; i < attackableTiles.Count; i++)
                {
                    if (tiles[attackableTiles[i]].Piece != null)
                    {
                        if (rangeTiles[i] >= tile.Piece.ToCharacter().Weapon.MinRange && rangeTiles[i] <= tile.Piece.ToCharacter().Weapon.MaxRange)
                        {
                            tiles[attackableTiles[i]].Color = Color.Green;
                        }
                    }
                }
            }
        }
    }
}
