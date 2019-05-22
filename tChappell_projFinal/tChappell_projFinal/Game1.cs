using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tChappell_projFinal
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameContent gameContent;

        private Board board;
        private Menu menu;

        private int screenWidth = 0;
        private int screenHeight = 0;
        private int rows = 0;
        private int columns = 0;
        private int distanceFromTop = 70;
        private int numMines = 0;
        private double seconds = 0;
        private int minutes = 0;
        private bool startPlaying;
        private bool gameOver;
        private bool newGame;
        private Vector2 loseSpace;
        private Vector2 winSpace;

        private MouseState oldMouseState;
        private KeyboardState oldKeyboardState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameContent = new GameContent(Content);
            NewGame();
            menu = new Menu(screenWidth, screenHeight, spriteBatch, gameContent);
            loseSpace = gameContent.text.MeasureString("You Lose");
            winSpace = gameContent.text.MeasureString("You Win");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            MouseState newMouseState = Mouse.GetState();
            KeyboardState newKeyboardState = Keyboard.GetState();

            if (gameOver)
            {
                if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton != ButtonState.Pressed || newKeyboardState != oldKeyboardState)
                {
                    NewGame();
                }
                oldMouseState = newMouseState;
                oldKeyboardState = newKeyboardState;
                base.Update(gameTime);
                return;
            }
            if (newGame)
            {
                if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton != ButtonState.Pressed)
                {
                    SelectMode(menu.contains(newMouseState.X, newMouseState.Y));
                }
                oldMouseState = newMouseState;
                oldKeyboardState = newKeyboardState;
                base.Update(gameTime);
                return;
            }
            if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton != ButtonState.Pressed)
            {
                if (board.ChooseTile(newMouseState.X, newMouseState.Y))
                {
                    startPlaying = true;
                }
            }
            if (newKeyboardState.IsKeyDown(Keys.Space) && oldKeyboardState.IsKeyUp(Keys.Space))
            {
                int temp = board.FlagTile(newMouseState.X, newMouseState.Y);
                if (temp != 0)
                {
                    startPlaying = true;
                }
                numMines += temp;
            }         
            oldMouseState = newMouseState;
            oldKeyboardState = newKeyboardState;
            if (!startPlaying)
            {
                seconds = 0;
                minutes = 0;
                base.Update(gameTime);
                return;
            }
            if(HasWon() || HasLost())
            {
                gameOver = true;
            }
            else
            {
                seconds += gameTime.ElapsedGameTime.TotalSeconds;
            }
            
            base.Update(gameTime);
        }

        /**
         * Determines if the player has won
         * @return bool True if player wins
         */ 
        public bool HasWon()
        {
            foreach (Tile tile in board.Tiles)
            {
                if (!tile.Visible && !tile.hasMine)
                {
                    return false;
                }
            }
            return true;
        }

        /**
         * Determines if the player has lost
         * @return bool True if player loses
         */ 
        public bool HasLost()
        {
            foreach (Tile tile in board.Tiles)
            {
                if (tile.Visible && tile.hasMine)
                {
                    return true;
                }
            }
            return false;
        }

        /**
         * Resets the game to the title screen
         */ 
        public void NewGame()
        {
            newGame = true;
            screenWidth = 500;
            screenHeight = 600;
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();
            gameOver = false;
            startPlaying = false;
        }

        /**
         * Updates screen to mode that the player chose
         * @param selection Label that the player selected
         */ 
        public void SelectMode(int selection)
        {
            if(selection == 0)
            {
                return;
            }
            else if (selection == 1)
            {
                rows = 9;
                columns = 9;
                numMines = 10;
            }
            else if (selection == 2)
            {
                rows = 16;
                columns = 16;
                numMines = 40;
            }
            else
            {
                rows = 30;
                columns = 16;
                numMines = 99;
            }
            seconds = 0;
            minutes = 0;
            board = new Board(rows, columns, distanceFromTop, numMines, spriteBatch, gameContent);
            newGame = false;
            screenWidth = rows * gameContent.imgBlankTile.Width * 3;
            screenHeight = columns * gameContent.imgBlankTile.Height * 3 + distanceFromTop;
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);
            spriteBatch.Begin();
            if(newGame)
            {
                menu.Draw();
            }
            else
            {
                spriteBatch.DrawString(gameContent.text, "Mines Left: " + numMines, new Vector2(20, 20), Color.Black);
                board.Draw();
                int temp = (int)seconds;
                if(temp >= 60)
                {
                    seconds = 0;
                    minutes++;
                }
                if(temp <= 9)
                {
                    spriteBatch.DrawString(gameContent.text, minutes + ":0" + temp, new Vector2(screenWidth - 70, 20), Color.Black);
                }
                else
                {
                    spriteBatch.DrawString(gameContent.text, minutes + ":" + temp, new Vector2(screenWidth - 70, 20), Color.Black);
                }
            }
            if(gameOver && !newGame)
            {
                if(HasLost())
                {
                    spriteBatch.DrawString(gameContent.text, "You Lose", new Vector2((screenWidth - loseSpace.X)/ 2, 20), Color.Red);
                }
                else
                {
                    spriteBatch.DrawString(gameContent.text, "You Win", new Vector2((screenWidth - winSpace.X)/ 2, 20), Color.Green);
                }
            }         
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
