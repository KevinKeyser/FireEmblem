using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MiLib.CoreTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiLib.UserInterface
{
    public delegate void LabelEvent(object sender, EventArgs e);
    public class Label
    {
        public string Name;
        public SpriteFont Font;
        private Texture2D texture;
        public Color BackgroundColor;
        public Color SelectedBackgroundColor;
        public Color ForegroundColor;
        public Color SelectedForegroundColor;
        public Rectangle Bounds { get; set; }
        public bool IsSelected;
        protected Vector2 position;
		public Vector2 Position 
		{ 
			get
			{ 
				return position; 
			}
			set 
			{
				position = value;
			}
		}

        public string Text;

        public Label(string name, GraphicsDevice graphicsDevice, SpriteFont font, string text, Vector2 position)
        {
            Name = name;
            Position = position; 
            texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData<Color>(new Color[] { Color.White });
            ForegroundColor = Color.Black;
            BackgroundColor = Color.White;
            Font = font;
            Text = text;
            SelectedBackgroundColor = Color.Blue;
            SelectedForegroundColor = Color.Black;
            IsSelected = false;
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, (int)(Font.MeasureString(Text).X), (int)(Font.MeasureString(Text).Y));
        }

        #region Button Events
        /// <summary>
        /// Occurs when left button is held down
        /// </summary>
        public event LabelEvent LeftDown;
        /// <summary>
        /// Occurs when left button is released.
        /// </summary>
        public event LabelEvent LeftClicked;
        /// <summary>
        /// Occurs when right button is held down
        /// </summary>
        public event LabelEvent RightDown;
        /// <summary>
        /// Occurs when right button is released.
        /// </summary>
        public event LabelEvent RightClicked;
        /// <summary>
        /// Occurs when middle scrollwheel is held down
        /// </summary>
        public event LabelEvent MiddleDown;
        /// <summary>
        /// Occurs when middle scrollwheel is released.
        /// </summary>
        public event LabelEvent MiddleClicked;
        /// <summary>
        /// Occurs when mouse is hovering.
        /// </summary>
        public event LabelEvent Hovered;
        /// <summary>
        /// Occurs when mouse is down and dragging.
        /// </summary>
        public event LabelEvent Drag;
        #endregion
        #region Event Invokes
        public void InvokeEvent(LabelEvent myEvent)
        {
            if (myEvent != null)
            {
                myEvent.Invoke(null, null);
            }
        }

        private void InvokeEvent(LabelEvent myEvent, object sender)
        {
            if (myEvent != null)
            {
                myEvent.Invoke(sender, null);
            }
        }

        private void InvokeEvent(LabelEvent myEvent, object sender, EventArgs e)
        {
            if (myEvent != null)
            {
                myEvent.Invoke(sender, e);
            }
        }

        private void InvokeEvent(LabelEvent myEvent, EventArgs e)
        {
            if (myEvent != null)
            {
                myEvent.Invoke(this, e);
            }
        }
        #endregion
        public void Update(GameTime gameTime)
        {
            if (InputManager.IsDragged(Bounds))
            {
                InvokeEvent(Drag, InputManager.MouseDragAmount());
            }
            else if (InputManager.IsLeftClicked(Bounds))
            {
                InvokeEvent(LeftClicked);
            }
            else if (InputManager.IsRightClicked(Bounds))
            {
                InvokeEvent(RightClicked);
            }
            else if (InputManager.IsMiddleClicked(Bounds))
            {
                InvokeEvent(MiddleClicked);
            }
            else if (InputManager.IsLeftDown(Bounds))
            {
                InvokeEvent(LeftDown);
            }
            else if (InputManager.IsRightDown(Bounds))
            {
                InvokeEvent(RightDown);
            }
            else if (InputManager.IsMiddleDown(Bounds))
            {
                InvokeEvent(MiddleDown);
            }
            else if (InputManager.isMouseHovering(Bounds))
            {
                InvokeEvent(Hovered);
            }
        }
        public void CenterText()
        {
            position = new Vector2((Bounds.Width - Font.MeasureString(Text).X) / 2 + Bounds.X, Bounds.Y);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

            if (IsSelected)
            {
                spriteBatch.Draw(texture, Bounds, SelectedBackgroundColor);
                spriteBatch.DrawString(Font, Text, position, SelectedForegroundColor);
            }
            else
            {
                spriteBatch.Draw(texture, Bounds, BackgroundColor);
                spriteBatch.DrawString(Font, Text, position, ForegroundColor);
            }
        }
    }
}
