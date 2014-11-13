using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FireEmblem
{
    public class Sprite
    {
        protected Texture2D texture;

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        protected Rectangle rectangle;

        public Vector2 Position
        {
            get
            {
                return new Vector2(rectangle.X, rectangle.Y);
            }
            set
            {
                rectangle.X = (int)value.X;
                rectangle.Y = (int)value.Y;
            }
        }

        public int X
        {
            get
            {
                return rectangle.X;
            }
            set
            {
                rectangle.X = value;
            }
        }

        public int Y
        {
            get
            {
                return rectangle.Y;
            }
            set
            {
                rectangle.Y = value;
            }
        }

        public int Width
        {
            get
            {
                return rectangle.Width;
            }
            set
            {
                rectangle.Width = value;
            }
        }

        public int Height
        {
            get
            {
                return rectangle.Height;
            }
            set
            {
                rectangle.Height = value;
            }
        }
        
        protected Rectangle? sourceRectangle;

        public Rectangle? SourceRectangle
        {
            get { return sourceRectangle; }
            set { sourceRectangle = value; }
        }

        protected Color color;

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        protected float rotation;

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        protected Vector2 origin;

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        protected SpriteEffects effects;

        public SpriteEffects Effects
        {
            get { return effects; }
            set { effects = value; }
        }

        protected float depth;

        public float Depth
        {
            get { return depth; }
            set { depth = value; }
        }
        
        public Sprite(Texture2D texture, Vector2 position)
            : this(texture, new Rectangle((int)position.X,(int)position.Y, texture.Width, texture.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0) { }

        public Sprite(Texture2D texture, Rectangle rectangle)
            : this(texture, rectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0) { }

        public Sprite(Texture2D texture, Rectangle rectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float depth)
        {
            this.texture = texture;
            this.rectangle = rectangle;
            this.sourceRectangle = sourceRectangle;
            this.color = color;
            this.rotation = rotation;
            this.origin = origin;
            this.effects = effects;
            this.depth = depth;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, sourceRectangle, color, rotation, origin, effects, depth);
        }
    }
}
