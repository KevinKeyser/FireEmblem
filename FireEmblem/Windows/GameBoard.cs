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
        public Team Turn;
        Texture2D pixel;
        public TurnOrder lastTurnOrder;
        public TurnOrder turnOrder;
        Weapon lastWeapon;
        Dictionary<Vector2, Tile> tiles;
        int boardHeight;
        int boardWidth;
        int tileSize;
        List<Vector2> lastMoveTiles;
        public Tile selectedTile;

        Vector2 lastCoords;
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
        EndMenu endMenu;
        Camera2D camera;

        public GameBoard(ContentManager Content, int tileSize, int boardWidth, int boardHeight)
        {
            Random random = new Random();
            Turn = (Team)random.Next(2);
            pixel = new Texture2D(Content.Load<Texture2D>("Tile").GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
            lastWeapon = null;
            turnOrder = TurnOrder.Picking;
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
            tiles[new Vector2(5)].Piece = new Character(CharacterName.Anna, tiles[new Vector2(5, 5)].Rectangle, new CharacterStatistics(), ClassName.Archer, Team.Red);
            tiles[new Vector2(5)].Piece.ToCharacter().Items = new Item[] { new HealthItem("Potion", 0, 0, 10, 3, 1000), (Weapon)Info.Weapons[WeaponName.BronzeBow].Clone() };
            tiles[new Vector2(3, 5)].Piece = new Character(CharacterName.Brady, tiles[new Vector2(3, 5)].Rectangle, new CharacterStatistics(), ClassName.GriffonRider, Team.Blue);
            tiles[new Vector2(3, 5)].Piece.ToCharacter().Items = new Item[] { new HealthItem("Potion", 0, 0, 10, 3, 1000), (Weapon)Info.Weapons[WeaponName.BronzeSword].Clone(), (Weapon)Info.Weapons[WeaponName.BronzeAxe].Clone() };
            
            battleMenu = new BattleMenu(new Vector2(0, 0), new Vector2(200, 400), Content.Load<SpriteFont>("Font"), pixel);
            itemMenu = new ItemMenu(Vector2.Zero, new Vector2(200, 400), Content.Load<SpriteFont>("Font"), pixel);
            endMenu = new EndMenu(Vector2.Zero, new Vector2(200, 400), Content.Load<SpriteFont>("Font"), pixel);
            camera = new Camera2D(Content.Load<Texture2D>("Tile").GraphicsDevice);
            camera.Position = new Vector2(SelectedCoords.X * tileSize + tileSize / 2, selectedCoords.Y * tileSize + tileSize / 2);
            selectedTile = tiles[selectedCoords];
            battleMenu.OptionChoosen += battleMenu_OptionChoosen;
            itemMenu.OptionChoosen += itemMenu_OptionChoosen;
            endMenu.OptionChoosen += endMenu_OptionChoosen;
        }

        void battleMenu_OptionChoosen(object sender, EventArgs e)
        {
            string name = sender.ToString();
            if (name == "Attack")
            {
                lastTurnOrder = turnOrder;
                turnOrder = TurnOrder.Attack;
            }
            else if (name == "Items")
            {
                lastTurnOrder = turnOrder;
                turnOrder = TurnOrder.Items;
                itemMenu.Piece = battleMenu.Piece;
                lastWeapon = battleMenu.Piece.ToCharacter().Weapon;
            }
            else if (name == "Wait")
            {
                turnOrder = TurnOrder.Picking;
                Character character = ((Character)tiles[lastCoords].Piece);
                character.HasMoved = true;
                tiles[lastCoords].Piece = character;
            }
            battleMenu.SelectedIndex = 0;
        }

        void itemMenu_OptionChoosen(object sender, EventArgs e)
        {
            int index = (int)sender;
            switch (itemMenu.Piece.ToCharacter().Items[index].ItemType)
            {
                case ItemType.Weapon :
                    itemMenu.Piece.ToCharacter().Weapon = (Weapon)itemMenu.Piece.ToCharacter().Items[index];
                    lastTurnOrder = turnOrder;
                    turnOrder = TurnOrder.BattleChoice;
                    break;
                case ItemType.Health :
                    itemMenu.Piece.ToCharacter().CurrentHealth += ((HealthItem)itemMenu.Piece.ToCharacter().Items[index]).Heal(itemMenu.Piece.ToCharacter().TotalStats.HealthPoints);
                    battleMenu.IsItem = false;
                    battleMenu.IsAttack = false;
                    lastTurnOrder = turnOrder;
                    turnOrder = TurnOrder.BattleChoice;
                    itemMenu.Piece.ToCharacter().Items[index].UsesCurrent--; 
                    break;
                case ItemType.StatModifier:
                    break;
                case ItemType.Ect :
                    break;
            }
            itemMenu.SelectedIndex = 0;
        }

        void endMenu_OptionChoosen(object sender, EventArgs e)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                for (int x = 0; x < boardWidth; x++)
                {
                    if (tiles[new Vector2(x, y)].Piece != null && tiles[new Vector2(x, y)].Piece.ToCharacter() != null)
                    {
                        tiles[new Vector2(x, y)].Piece.ToCharacter().HasMoved = false;
                        tiles[new Vector2(x, y)].Piece.ToCharacter().HasAttacked = false;
                    }
                }
            }
            endMenu.SelectedIndex = 0;
            Turn = (Team)((((int)Turn) + 1) % 2);
            lastTurnOrder = turnOrder;
            turnOrder = TurnOrder.Picking;
        }

        private void keyMovementUpdate()
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
            switch (turnOrder)
            {
                
                case TurnOrder.Picking:
                    keyMovementUpdate();
                    selectedTile.Color = Color.Blue;
                    selectedTile = tiles[selectedCoords];
                    if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
                    {
                        if (selectedTile.Piece != null && selectedTile.Piece.ToCharacter() != null ? selectedTile.Piece.ToCharacter().Team == Turn ? !selectedTile.Piece.ToCharacter().HasMoved : false : false)
                        {
                            lastTurnOrder = turnOrder;
                            turnOrder = TurnOrder.Moving;
                        }
                    }
                    if(InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
                    {
                        lastTurnOrder = turnOrder;
                        turnOrder = TurnOrder.End;
                    }
                    if (selectedTile.Piece != null && selectedTile.Piece.ToCharacter() != null ? selectedTile.Piece.ToCharacter().Team == Turn ? !selectedTile.Piece.ToCharacter().HasMoved : false : false)
                    {
                        ShowMovement();
                    }
                    break;
                case TurnOrder.Moving:
                    keyMovementUpdate();
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
                            lastTurnOrder = turnOrder;
                            turnOrder = TurnOrder.BattleChoice;
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
                            lastCoords = new Vector2(tiles[selectedCoords].Position.X / tileSize, tiles[selectedCoords].Position.Y / tileSize);
                        }
                    }

                    if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
                    {
                        turnOrder = TurnOrder.Picking;
                    }
                    break;
                case TurnOrder.BattleChoice:
                    battleMenu.Update(gameTime);
                    ShowAttackRange(tiles[lastCoords]);
                    if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape) && tiles[lastCoords].Piece.ToCharacter() != null ? !tiles[lastCoords].Piece.ToCharacter().HasAttacked : false)
                    {
                        if (tiles[selectedCoords] != selectedTile)
                        {
                            selectedTile.Piece = tiles[selectedCoords].Piece;
                            tiles[selectedCoords].Piece = null;
                        }
                        turnOrder = TurnOrder.Moving;
                        battleMenu.Piece = null;
                    }
                    break;
                case TurnOrder.Attack:
                    keyMovementUpdate();
                    ShowAttackRange(tiles[lastCoords]);
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
                            lastTurnOrder = turnOrder;
                            turnOrder = TurnOrder.BattleChoice;
                            battleMenu.IsAttack = false;
                            battleMenu.IsItem = true;
                            Character character = tiles[lastCoords].Piece.ToCharacter();
                            character.HasAttacked = true;
                            tiles[lastCoords].Piece = character;
                            Character enemy = tiles[selectedCoords].Piece.ToCharacter();
                            enemy.CurrentHealth -= character.TotalStats.Strength + character.Weapon.Might - enemy.TotalStats.Defense;
                            tiles[selectedCoords].Piece = enemy;
                            if(enemy.IsDead)
                            {
                                tiles[selectedCoords].Piece = null;
                            }
                        }
                    }
                    if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
                    {
                        selectedCoords = lastCoords;
                        turnOrder = lastTurnOrder;
                    }
                    break;
                case TurnOrder.Items:
                    itemMenu.Update(gameTime);
                    if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
                    {
                        tiles[selectedCoords].Piece.ToCharacter().Weapon = lastWeapon;
                        turnOrder = TurnOrder.BattleChoice;
                    }
                    break;
                case TurnOrder.End:
                    endMenu.Update(gameTime);
                    if(InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
                    {
                        lastTurnOrder = turnOrder;
                        turnOrder = TurnOrder.Picking;
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
            switch(turnOrder)
            {
                case TurnOrder.Picking:
                    break;
                case TurnOrder.Moving:
                    break;
                case TurnOrder.BattleChoice:
                    battleMenu.Draw(spriteBatch);
                    break;
                case TurnOrder.Attack:
                    break;
                case TurnOrder.Items:
                    itemMenu.Draw(spriteBatch);
                    break;
                case TurnOrder.End:
                    endMenu.Draw(spriteBatch);
                    break;
            }
        }

        List<Vector2> moveableTiles;
        public void ShowMovement()
        {
            if (selectedTile.Piece != null)
            {
                moveableTiles = new List<Vector2>();
                attackableTiles = new List<Vector2>();
                List<Vector2> moveThroughTiles = new List<Vector2>();

                moveableTiles.Add(new Vector2(selectedTile.Position.X / selectedTile.Width, selectedTile.Position.Y / selectedTile.Height));
                int currentMovement = 0;
                int currentIndex = 0;
                while (currentMovement < selectedTile.Piece.ToCharacter().TotalStats.Movement)
                {
                    lastMoveTiles = new List<Vector2>();
                    for (int i = currentIndex; i < moveableTiles.Count; i++)
                    {
                        Vector2[] vectors = new Vector2[]{
                            Vector2.Clamp(new Vector2(moveableTiles[i].X, moveableTiles[i].Y - 1), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1)),
                            Vector2.Clamp(new Vector2(moveableTiles[i].X, moveableTiles[i].Y + 1), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1)),
                            Vector2.Clamp(new Vector2(moveableTiles[i].X + 1, moveableTiles[i].Y), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1)),
                            Vector2.Clamp(new Vector2(moveableTiles[i].X - 1, moveableTiles[i].Y), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1))
                        };

                        foreach(Vector2 vect in vectors)
                        {
                            if (!moveableTiles.Contains(vect) && !lastMoveTiles.Contains(vect) && !moveThroughTiles.Contains(vect))
                            {
                                if (tiles[vect].Piece == null)
                                {
                                    lastMoveTiles.Add(vect);
                                }
                                else if(tiles[vect].Piece.ToCharacter().Team == selectedTile.Piece.ToCharacter().Team)
                                {
                                    lastMoveTiles.Add(vect);
                                    moveThroughTiles.Add(vect);
                                }
                                else
                                {
                                    attackableTiles.Add(vect);
                                }
                            }
                        }
                    }
                    currentIndex = moveableTiles.Count - 1;
                    currentMovement++;
                    moveableTiles.AddRange(lastMoveTiles);
                }
                while(moveThroughTiles.Count > 0)
                {
                    moveableTiles.Remove(moveThroughTiles[0]);
                    moveThroughTiles.RemoveAt(0);
                }
                for (int i = 0; i < moveableTiles.Count; i++)
                {
                    tiles[moveableTiles[i]].Color = Color.Purple;
                }
                ShowAttackRangeAfterMovement();
            }
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
                        Vector2[] vectors = new Vector2[]{
                            Vector2.Clamp(new Vector2(attackableTiles[i].X, attackableTiles[i].Y - 1), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1)),
                            Vector2.Clamp(new Vector2(attackableTiles[i].X, attackableTiles[i].Y + 1), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1)),
                            Vector2.Clamp(new Vector2(attackableTiles[i].X + 1, attackableTiles[i].Y), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1)),
                            Vector2.Clamp(new Vector2(attackableTiles[i].X - 1, attackableTiles[i].Y), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1))
                        };
                        foreach (Vector2 vect in vectors)
                        {
                            if (!attackableTiles.Contains(vect) && !lastAttackTiles.Contains(vect) && !moveableTiles.Contains(vect))
                            {
                                if (tiles[vect].Piece != null && tiles[vect].Piece.ToCharacter().Team != selectedTile.Piece.ToCharacter().Team)
                                {
                                    lastAttackTiles.Add(vect);
                                }
                            }
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
                        Vector2[] vectors = new Vector2[]{
                            Vector2.Clamp(new Vector2(attackableTiles[i].X, attackableTiles[i].Y - 1), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1)),
                            Vector2.Clamp(new Vector2(attackableTiles[i].X, attackableTiles[i].Y + 1), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1)),
                            Vector2.Clamp(new Vector2(attackableTiles[i].X + 1, attackableTiles[i].Y), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1)),
                            Vector2.Clamp(new Vector2(attackableTiles[i].X - 1, attackableTiles[i].Y), Vector2.Zero, new Vector2(boardWidth - 1, boardHeight - 1))
                        };
                        foreach (Vector2 vect in vectors)
                        {
                            if (!attackableTiles.Contains(vect) && !lastAttackTiles.Contains(vect))
                            {
                                lastAttackTiles.Add(vect);
                                rangeTiles.Add(currentAttackRange);
                            }
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
                    if (tiles[attackableTiles[i]].Piece != null && tiles[attackableTiles[i]].Piece.ToCharacter().Team != tile.Piece.ToCharacter().Team)
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
