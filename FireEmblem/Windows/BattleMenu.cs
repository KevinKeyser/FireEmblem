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
    public class BattleMenu
    {
        Rectangle area;
        List<Label> labels;
        public GamePiece Piece;
        Color color;
        Texture2D texture;
        public bool IsVisible;
        int selectedIndex = 0;
        GraphicsDevice graphicsDevice;
        SpriteFont font;
        private bool isAttack;
        private bool isItem;
        private int width = 200;
        public bool IsAttack
        {
            set
            {
                if(value != isAttack)
                {
                    selectedIndex = 0;
                    if(value)
                    {
                        foreach(Label label in labels)
                        {
                            label.Bounds = new Rectangle(area.X, (int)label.Position.Y + labels[0].Bounds.Height, label.Bounds.Width, label.Bounds.Height);
                        }
                        labels.Add(new Label("Attack", graphicsDevice, font, "Attack", new Vector2(area.X, area.Y)));
                        for(int i = labels.Count - 1; i > 0; i--)
                        {
                            Label temp;
                            temp = labels[i];
                            labels[i] = labels[i - 1];
                            labels[i - 1] = temp;
                        }
                    }
                    else
                    {
                        int index = 0;
                        for(int i = 0; i < labels.Count; i++)
                        {
                            labels[i].Bounds = new Rectangle(area.X, (int)labels[i].Position.Y - labels[0].Bounds.Height, labels[i].Bounds.Width, labels[i].Bounds.Height);
                            if(labels[i].Name == "Attack")
                            {
                                index = i;
                            }
                        }
                        labels.RemoveAt(index);
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
                    selectedIndex = 0;
                    if (value)
                    {
                        foreach (Label label in labels)
                        {
                            label.Bounds = new Rectangle(area.X, (int)label.Position.Y + labels[0].Bounds.Height, label.Bounds.Width, label.Bounds.Height);
                        }
                        labels.Add(new Label("Items", graphicsDevice, font, "Items", new Vector2(area.X, area.Y + labels[0].Bounds.Height)));
                        for (int i = labels.Count - 1; i > 1; i--)
                        {
                            Label temp;
                            temp = labels[i];
                            labels[i] = labels[i - 1];
                            labels[i - 1] = temp;
                        }
                    }
                    else
                    {
                        int index = 0;
                        for (int i = 1; i < labels.Count; i++)
                        {
                            labels[i].Bounds = new Rectangle(area.X, (int)labels[i].Position.Y - labels[0].Bounds.Height, labels[i].Bounds.Width, labels[i].Bounds.Height);
                            if (labels[i].Name == "Items")
                            {
                                index = i;
                            }
                        }
                        labels.RemoveAt(index);
                    }
                    isItem = value;
                }
            }
        }

        public BattleMenu(GraphicsDevice graphicsDevice, ContentManager Content)
        {
            Piece = null;
            IsVisible = false;
            isAttack = true;
            isItem = true;
            labels = new List<Label>();
            color = Color.Brown;
            texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData<Color>(new Color[] { Color.White } );
            this.graphicsDevice = graphicsDevice;
            font = Content.Load<SpriteFont>("Font");
            labels.Add(new Label("Attack", graphicsDevice, font, "Attack", new Vector2(area.X, area.Y)));
            labels.Add(new Label("Items", graphicsDevice, font, "Items", new Vector2(area.X, labels[labels.Count - 1].Bounds.Y + labels[labels.Count - 1].Bounds.Height)));
            labels.Add(new Label("Wait", graphicsDevice, font, "Wait", new Vector2(area.X, labels[labels.Count - 1].Bounds.Y + labels[labels.Count - 1].Bounds.Height)));
        }

        public event BattleMenuEvent OptionChoosen;

        private void InvokeEvent(BattleMenuEvent myEvent, string senderName)
        {
            if (myEvent != null)
            {
                myEvent.Invoke(senderName, null);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (IsVisible)
            {
                if(InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down) && selectedIndex < labels.Count - 1)
                {
                    selectedIndex++;
                }
                if(InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up) && selectedIndex > 0)
                {
                    selectedIndex--;
                }
                if(InputManager.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
                {
                    InvokeEvent(OptionChoosen, labels[selectedIndex].Name);
                }
                foreach (Label label in labels)
                {
                    label.Update(gameTime);
                    label.Bounds = new Rectangle(label.Bounds.X, label.Bounds.Y, width, label.Bounds.Height);
                    label.IsSelected = false;
                    label.CenterText();
                }
                labels[selectedIndex].IsSelected = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                foreach (Label label in labels)
                {
                    label.Draw(spriteBatch);
                }
            }
        }
    }
}
