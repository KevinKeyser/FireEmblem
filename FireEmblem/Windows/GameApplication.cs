using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiLib.CoreTypes;

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
                spriteBatch.DrawString(font, gameBoard.turnorder.ToString() + "\n" + gameBoard.selectedTile.ToString(), new Vector2(Global.ScreenWidth - 300, 0), Color.Black);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
