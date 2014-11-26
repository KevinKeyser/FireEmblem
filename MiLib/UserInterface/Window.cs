using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiLib.UserInterface
{
    public class Window : UIComponent
    {
        public Color WindowColor;
        public Color ForegroundSelectionColor;
        public Color BackgroundSelectionColor;

        public Texture2D WindowTexture;
        public SpriteFont Font;

        public List<Label> Items;

        public int SelectedIndex = 0;

        public Window(Vector2 position, Vector2 size, SpriteFont font, Texture2D windowTexture)
            : base(position, size)
        {
            Items = new List<Label>();
            Font = font;
            WindowTexture = windowTexture;
            WindowColor = Color.White;
            ForegroundSelectionColor = Color.White;
            BackgroundSelectionColor = Color.Blue;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (isVisible)
            {
                foreach (Label label in Items)
                {
                    label.Update(gameTime);
                    label.Bounds = new Rectangle(label.Bounds.X, label.Bounds.Y, Bounds.Width, label.Bounds.Height);
                    label.IsSelected = false;
                    label.CenterText();
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                spriteBatch.Draw(WindowTexture, Bounds, WindowColor);
                foreach (Label label in Items)
                {
                    label.Draw(spriteBatch);
                }
            }
        }

    }
}
