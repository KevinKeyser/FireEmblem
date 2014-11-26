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
    public delegate void ItemMenuEvent(object sender, EventArgs e);
    public class ItemMenu : Window
    {
        private GamePiece piece;
        public GamePiece Piece
        {
            get
            {
                return piece;
            }
            set
            {
                piece = value;
                if(piece != null)
                {
                    Items.Clear();
                    for(int i = 0; i < piece.ToCharacter().Items.Length; i++)
                    {
                        if(piece.ToCharacter().Items[i] != null)
                        {
                            Items.Add(new Label(piece.ToCharacter().Items[i].Name, WindowTexture.GraphicsDevice, Font, piece.ToCharacter().Items[i].ToString(), new Vector2(position.X, position.Y + Items.Count > 0 ? Items[i-1].Position.Y + Items[i-1].Bounds.Height : 0)));
                        }
                    }
                }
            }
        }

        public ItemMenu(Vector2 position, Vector2 size, SpriteFont font, Texture2D texture)
            : base(position, size, font, texture)
        {
            Piece = null;
        }

        public event ItemMenuEvent OptionChoosen;

        private void InvokeEvent(ItemMenuEvent myEvent, string senderName)
        {
            if (myEvent != null)
            {
                myEvent.Invoke(senderName, null);
            }
        }

        private void InvokeEvent(ItemMenuEvent myEvent, int index)
        {
            if (myEvent != null)
            {
                myEvent.Invoke(index, null);
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
                        InvokeEvent(OptionChoosen, SelectedIndex);
                    }
                    Items[SelectedIndex].IsSelected = true;
                }
            }
        }
    }
}
