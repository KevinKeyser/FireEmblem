using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MiLib.UserInterface
{
    public class UIComponent
    {
        protected bool isVisible;

        public virtual bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        protected bool isEnabled;

        public virtual bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; }
        }
        
        
        protected Rectangle bounds;
        public virtual Rectangle Bounds
        {
            get
            {
                return bounds;
            }
            set
            {
                bounds = Bounds;
                position.X = bounds.X;
                position.Y = bounds.Y;
                size.X = bounds.Width;
                size.Y = bounds.Height;
            }
        }

        protected Vector2 position;
        public virtual Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                bounds.X = (int)position.X;
                bounds.Y = (int)position.Y;
            }
        }

        private Vector2 size;

        public virtual Vector2 Size
        {
            get { return size; }
            set
            {
                if(value.X >= 0 && value.Y >= 0)
                {
                    size = value;
                    bounds.Width = (int)size.X;
                    bounds.Height = (int)size.Y;
                }
                else
                {
                    Debug.WriteLine("UIComponent - Size must be positive");
                }
            }
        }

        public UIComponent(Vector2 position, Vector2 size)
            : this(new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y)) { }

        public UIComponent(Rectangle bounds)
        {
            this.bounds = bounds;
            position = new Vector2(bounds.X, bounds.Y);
            Size = new Vector2(bounds.Width, bounds.Height);

            isEnabled = true;
            isVisible = true;
        }
    }
}
