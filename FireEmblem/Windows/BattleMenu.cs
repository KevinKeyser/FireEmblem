using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MiLib.CoreTypes;
using MiLib.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    public delegate void BattleMenuEvent(object sender, EventArgs e);
    public class BattleMenu : Window
    {
        public GamePiece Piece;
        private bool isAttack;
        private bool isItem;
        public bool IsAttack
        {
            set
            {
                if(value != isAttack)
                {
                    SelectedIndex = 0;
                    if(value)
                    {
                        foreach(Label label in Items)
                        {
                            label.Bounds = new Rectangle(Bounds.X, (int)label.Position.Y + Items[0].Bounds.Height, label.Bounds.Width, label.Bounds.Height);
                        }
                        Items.Add(new Label("Attack", WindowTexture.GraphicsDevice, Font, "Attack", new Vector2(Bounds.X, Bounds.Y)));
                        for(int i = Items.Count - 1; i > 0; i--)
                        {
                            Label temp;
                            temp = Items[i];
                            Items[i] = Items[i - 1];
                            Items[i - 1] = temp;
                        }
                    }
                    else
                    {
                        int index = 0;
                        for(int i = 0; i < Items.Count; i++)
                        {
                            Items[i].Bounds = new Rectangle(Bounds.X, (int)Items[i].Position.Y - Items[0].Bounds.Height, Items[i].Bounds.Width, Items[i].Bounds.Height);
                            if(Items[i].Name == "Attack")
                            {
                                index = i;
                            }
                        }
                        Items.RemoveAt(index);
                    }
                    isAttack = value;
                }
            }
        }

        public bool IsItem
        {
            set
            {
                if (value != isItem)
                {
                    SelectedIndex = 0;
                    if (value)
                    {
                        foreach (Label label in Items)
                        {
                            label.Bounds = new Rectangle(Bounds.X, (int)label.Position.Y + Items[0].Bounds.Height, label.Bounds.Width, label.Bounds.Height);
                        }
                        Items.Add(new Label("Items", WindowTexture.GraphicsDevice, Font, "Items", new Vector2(Bounds.X, Bounds.Y + Items[0].Bounds.Height)));
                        for (int i = Items.Count - 1; i > 1; i--)
                        {
                            Label temp;
                            temp = Items[i];
                            Items[i] = Items[i - 1];
                            Items[i - 1] = temp;
                        }
                    }
                    else
                    {
                        int index = 0;
                        for (int i = 1; i < Items.Count; i++)
                        {
                            Items[i].Bounds = new Rectangle(Bounds.X, (int)Items[i].Position.Y - Items[0].Bounds.Height, Items[i].Bounds.Width, Items[i].Bounds.Height);
                            if (Items[i].Name == "Items")
                            {
                                index = i;
                            }
                        }
                        Items.RemoveAt(index);
                    }
                    isItem = value;
                }
            }
        }

        public BattleMenu(Vector2 position, Vector2 size, SpriteFont font, Texture2D texture)
            : base(position, size, font, texture)
        {
            Piece = null;
            isAttack = true;
            isItem = true;
            Items.Add(new Label("Attack", texture.GraphicsDevice, Font, "Attack", new Vector2(Bounds.X, Bounds.Y)));
            Items.Add(new Label("Items", texture.GraphicsDevice, Font, "Items", new Vector2(Bounds.X, Items[Items.Count - 1].Bounds.Y + Items[Items.Count - 1].Bounds.Height)));
            Items.Add(new Label("Wait", texture.GraphicsDevice, Font, "Wait", new Vector2(Bounds.X, Items[Items.Count - 1].Bounds.Y + Items[Items.Count - 1].Bounds.Height)));
        }

        public event BattleMenuEvent OptionChoosen;

        private void InvokeEvent(BattleMenuEvent myEvent, string senderName)
        {
            if (myEvent != null)
            {
                myEvent.Invoke(senderName, null);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsVisible)
            {
                if (Items.Count > 0)
                {
                    if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down) && SelectedIndex < Items.Count - 1)
                    {
                        SelectedIndex++;
                    }
                    if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up) && SelectedIndex > 0)
                    {
                        SelectedIndex--;
                    }
                    if (InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
                    {
                        InvokeEvent(OptionChoosen, Items[SelectedIndex].Name);
                    }
                    Items[SelectedIndex].IsSelected = true;
                }
            }
        }

    }
}
