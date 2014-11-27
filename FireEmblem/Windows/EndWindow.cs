using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiLib.CoreTypes;
using MiLib.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    public delegate void EndMenuEvent(object sender, EventArgs e);
    class EndMenu : Window
    {
        public EndMenu(Vector2 position, Vector2 size, SpriteFont font, Texture2D texture)
            : base(position, size, font, texture)
        {
            Items.Add(new Label("End Turn", texture.GraphicsDevice, Font, "End Turn", new Vector2(Bounds.X, Bounds.Y)));
        }

        public event EndMenuEvent OptionChoosen;
        private void InvokeEvent(EndMenuEvent myEvent, string senderName)
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
