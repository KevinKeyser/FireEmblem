using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiLib.CoreTypes;
using System;
using System.Collections.Generic;
using System.IO;

namespace FireEmblem
{
    public class GameApplication : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameBoard gameBoard;
        SpriteFont font;

        public GameApplication()
            : base()
        {
            IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = Global.ScreenHeight;
            graphics.PreferredBackBufferWidth = Global.ScreenWidth;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {

            //1st standing front left - pingpong
            //2nd attack front left
            //3rd walking front left - pingpong
            //4th walking front right - pingpong
            //5th walking front - pingpong
            //6th walking back - pingpong
            //7th standing front right - pingpong
            //8th walking back right
            //9th walking back left


            //Adding Animations
            for (int i = 0; i < Enum.GetValues(typeof(ClassName)).Length; i++)
            {
                int yaddition = 32;
                if ((ClassName)i == ClassName.WyvernRider ||
                   (ClassName)i == ClassName.WyvernLord ||
                   (ClassName)i == ClassName.PegasusKnight ||
                   (ClassName)i == ClassName.GriffonRider ||
                   (ClassName)i == ClassName.FalconKnight ||
                   (ClassName)i == ClassName.DarkFlier)
                {
                    yaddition = 35;
                }
                Info.Animations.Add((ClassName)i, new Dictionary<Team, Dictionary<AnimationName, Animation>>());
                for (int team = 0; team < Enum.GetValues(typeof(Team)).Length; team++)
                {
                    Info.Animations[(ClassName)i].Add((Team)team, new Dictionary<AnimationName, Animation>());
                    for (int row = 0; row < 2; row++)
                    {
                        List<Frame> frames = new List<Frame>();
                        for (int col = 0; col < 4; col++)
                        {
                            frames.Add(new Frame(new Rectangle(col * 32 + team * 32 * 4, 2 + row * yaddition, 32, yaddition)));
                        }
                        Info.Animations[(ClassName)i][(Team)team].Add((AnimationName)row, new Animation(AnimationType.PingPong, true, TimeSpan.FromMilliseconds(250), frames));
                    }
                }
            }

            //Adding Images
            DirectoryInfo directory = new DirectoryInfo(Environment.CurrentDirectory + "\\" + Content.RootDirectory + "\\Characters");
            try
            {
                DirectoryInfo[] subdirects = directory.GetDirectories();
                foreach (DirectoryInfo subinfo in subdirects)
                {
                    string name = subinfo.Name;
                    CharacterName enumname;
                    if (!Enum.TryParse<CharacterName>(name, out enumname))
                    {
                        throw new FormatException(string.Format("CharacterName does not exist: {0}", name));
                    }
                    Info.Images.Add(enumname, new Dictionary<ClassName, Texture2D>());
                    FileInfo[] files = subinfo.GetFiles("*.xnb");
                    foreach (FileInfo file in files)
                    {
                        string classname = file.Name.Replace(name + " ", "").Replace(" ", "").Replace(".xnb", "");
                        ClassName enumclassname;
                        if (!Enum.TryParse<ClassName>(classname, out enumclassname))
                        {
                            throw new FormatException(string.Format("ClassName does not exist: {0}", classname));
                        }
                        Info.Images[enumname].Add(enumclassname, Content.Load<Texture2D>("Characters\\" + name + "\\" + file.Name.Replace(".xnb", "")));
                    }
                }
            }
            catch { }

            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameBoard = new GameBoard(Content, 50, 11, 11);
            font = Content.Load<SpriteFont>("Font");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();
            gameBoard.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            gameBoard.Draw(spriteBatch);
            if (gameBoard.selectedTile != null)
            {
                spriteBatch.DrawString(font, "Turn: " + gameBoard.Turn + "\n" + gameBoard.turnOrder.ToString() + "\n" + gameBoard.selectedTile.ToString(), new Vector2(Global.ScreenWidth - 300, 0), Color.Black);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
