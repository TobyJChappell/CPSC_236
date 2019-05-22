using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace tChappell_proj06
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameContent gameContent;

        private SpaceShip spaceShip;
        private Shield[] shields;
        private AlienFleet alienFleet;
        private UFO ufo;
        private GameBorder gameBorder;

        private int screenWidth = 0;
        private int screenHeight = 0;
        private MouseState oldMouseState;
        private KeyboardState oldKeyboardState;
        private bool readyToStart = true;
        private int livesRemaining = 3;
        private int aliensKilled = 0;
        private int score = 0;
        private bool firstTime = false;
        private bool hitBottom = false;
        private int alienFireRate = 5000;
        private int level = 1;
        private double elapsedTime = 500;
        private double spaceShipTime = 0;
        private double alienTime = 0;
        public SoundEffectInstance speed1;
        public SoundEffectInstance speed2;
        public SoundEffectInstance speed3;
        public SoundEffectInstance speed4;

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
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            if (screenWidth >= 2500)
            {
                screenWidth = 2500;
            }
            if (screenHeight >= 1250)
            {
                screenHeight = 1250;
            }
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();
            speed1 = gameContent.speed1Sound.CreateInstance();
            speed2 = gameContent.speed2Sound.CreateInstance();
            speed3 = gameContent.speed3Sound.CreateInstance();
            speed4 = gameContent.speed4Sound.CreateInstance();

            int spaceShipX = (screenWidth - gameContent.imgSpaceShip.Width/4) / 2;
            int spaceShipY = screenHeight - 150;
            spaceShip = new SpaceShip(spaceShipX, spaceShipY, screenWidth, spriteBatch, gameContent);
            shields = new Shield[3];
            for (int x = 0; x < 3; x++)
            {
                shields[x] = new Shield(screenWidth / 3 * x + screenWidth/8, spaceShip.Y - 150, spriteBatch, GraphicsDevice);
            }
            alienFleet = new AlienFleet(50, 150, screenWidth, spaceShip.Y, spriteBatch, gameContent);

            ufo = new UFO(0, 60, screenWidth, spriteBatch, gameContent);

            gameBorder = new GameBorder(screenWidth, screenHeight, spriteBatch, gameContent);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

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
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            KeyboardState newKeyboardState = Keyboard.GetState();
            MouseState newMouseState = Mouse.GetState();                  
            if (!spaceShip.Visible)
            {
                spaceShipTime += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (spaceShipTime <  1000 && livesRemaining != 0)
                {
                    return;
                }
                spaceShipTime = 0;
                readyToStart = true;
            }
            else
            {
                if (oldMouseState.X != newMouseState.X)
                {
                    if (newMouseState.X >= 0 || newMouseState.X < screenWidth)
                    {
                        spaceShip.MoveTo(newMouseState.X);
                    }
                }
                if (newKeyboardState.IsKeyDown(Keys.Left))
                {
                    spaceShip.MoveLeft();
                }
                if (newKeyboardState.IsKeyDown(Keys.Right))
                {
                    spaceShip.MoveRight();
                }
            }
            
            if (((newKeyboardState.IsKeyDown(Keys.Space) && !oldKeyboardState.IsKeyDown(Keys.Space)) || (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton != ButtonState.Pressed)) && livesRemaining == 0 && spaceShip.Visible == false)
            {
                readyToStart = true;
                RestartGame();
            }
            else if (((newKeyboardState.IsKeyDown(Keys.Space) && !oldKeyboardState.IsKeyDown(Keys.Space)) || (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton != ButtonState.Pressed)) && readyToStart)
            {
                readyToStart = false;
            }
            if (!readyToStart)
            {
                if ((newKeyboardState.IsKeyDown(Keys.Space) || newMouseState.LeftButton == ButtonState.Pressed) && elapsedTime >= 365)
                {
                    elapsedTime = 0;
                    spaceShip.fireBullet();
                }

                foreach (Bullet bullet in spaceShip.bullets)
                {
                    if (bullet.Visible)
                    {
                        bullet.Fire();
                        foreach(Shield shield in shields)
                        {
                            bullet.ShootShield(shield);
                        }
                        int temp = aliensKilled;
                        aliensKilled += bullet.ShootAliens(alienFleet);
                        if(aliensKilled > temp)
                        {
                            firstTime = true;
                        }
                        score += level*(aliensKilled - temp);
                        if (bullet.ShootUFO(ufo))
                        {
                            score += level * 10;
                        }
                    }
                }
                ufo.Move();
                if(alienFleet.Move())
                {
                    livesRemaining = 0;
                    spaceShip.ExplodeSpaceShip();
                    readyToStart = true;
                    hitBottom = true;
                }
                else
                {
                    alienFleet.FireBlasters(spaceShip, shields, alienFireRate);
                    alienTime += gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (alienFleet.Speed <= 1.5)
                    {
                        if (speed1.State != SoundState.Playing && alienTime >= 625)
                        {
                            speed1.Play();
                            alienTime = 0;
                        }
                    }
                    else if (alienFleet.Speed <= 2)
                    {
                        if (speed2.State != SoundState.Playing && alienTime >= 500)
                        {
                            speed2.Play();
                            alienTime = 0;
                        }
                    }
                    else if (alienFleet.Speed <= 2.5)
                    {
                        if (speed3.State != SoundState.Playing && alienTime >= 365)
                        {
                            speed3.Play();
                            alienTime = 0;
                        }
                    }
                    else if(alienTime >= 250)
                    {
                        if (speed4.State != SoundState.Playing)
                        {
                            speed4.Play();
                            alienTime = 0;
                        }
                    }
                }
            }

            oldMouseState = newMouseState;    
            oldKeyboardState = newKeyboardState;

            base.Update(gameTime);
        }

        public void RestartGame()
        {
            livesRemaining = 3;
            aliensKilled = 0;
            alienFireRate = 5000;
            firstTime = false;
            hitBottom = false;
            level = 1;
            score = 0;
            int spaceShipX = (screenWidth - gameContent.imgSpaceShip.Width / 4) / 2;
            int spaceShipY = screenHeight - 150;
            spaceShip = new SpaceShip(spaceShipX, spaceShipY, screenWidth, spriteBatch, gameContent);
            shields = new Shield[3];
            for (int x = 0; x < 3; x++)
            {
                shields[x] = new Shield(screenWidth / 3 * x + screenWidth / 8, spaceShip.Y - 150, spriteBatch, GraphicsDevice);
            }
            alienFleet = new AlienFleet(50, 150, screenWidth, spaceShip.Y, spriteBatch, gameContent);

            ufo = new UFO(0, 60, screenWidth, spriteBatch, gameContent);

            gameBorder = new GameBorder(screenWidth, screenHeight, spriteBatch, gameContent);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spaceShip.Draw();
            foreach(Shield shield in shields)
            {
                shield.Draw();
            }
            alienFleet.Draw();
            ufo.Draw(spaceShip);
            gameBorder.Draw();
            spriteBatch.DrawString(gameContent.labelFont, "Lives: " + livesRemaining.ToString(), new Vector2(40, 10), Color.White);
            Vector2 levelSpace = gameContent.labelFont.MeasureString("Level: " + level.ToString());
            spriteBatch.DrawString(gameContent.labelFont, "Level: " + level.ToString(), new Vector2((screenWidth - levelSpace.X)/2, 10), Color.White);
            spriteBatch.DrawString(gameContent.labelFont, "Score: " + score.ToString(), new Vector2(screenWidth - 200, 10), Color.White);

            if (readyToStart)
            {
                if (spaceShip.Visible && !hitBottom)
                {
                    ufo.Visible = false;
                    string startMsg = "Press <Space> or Click to start the level";
                    Vector2 startSpace = gameContent.labelFont.MeasureString(startMsg);
                    spriteBatch.DrawString(gameContent.labelFont, startMsg, new Vector2((screenWidth - startSpace.X) / 2, screenHeight / 16), Color.White);
                }
                else if (livesRemaining > 0)
                {
                    for(int x = 0; x < 11; x++)
                    {
                        for(int y = 0; y < 5; y++)
                        {
                            foreach (Blaster blaster in alienFleet.aliens[x, y].blasters)
                            {
                                blaster.Visible = false;
                            }
                        }
                    }
                    spaceShip.RespawnSpaceShip();
                    livesRemaining--;
                    readyToStart = false;
                }
                else
                {
                    string endMsg = "Game Over";
                    Vector2 endSpace = gameContent.labelFont.MeasureString(endMsg);
                    spriteBatch.DrawString(gameContent.labelFont, endMsg, new Vector2((screenWidth - endSpace.X) / 2, screenHeight / 16 - endSpace.Y), Color.White);
                    string startMsg = "Press <Space> or Click to start the level";
                    Vector2 startSpace = gameContent.labelFont.MeasureString(startMsg);
                    spriteBatch.DrawString(gameContent.labelFont, startMsg, new Vector2((screenWidth - startSpace.X) / 2, screenHeight / 16 + endSpace.Y), Color.White);
                }
            }
            else if(aliensKilled % 55 == 0 && firstTime)
            {
                alienFleet = new AlienFleet(50, 150, screenWidth, spaceShip.Y, spriteBatch, gameContent);
                foreach (Bullet bullet in spaceShip.bullets)
                {
                    bullet.Visible = false;
                }
                readyToStart = true;
                firstTime = false;
                alienFireRate /= 2;
                level++;
                livesRemaining++;
                foreach (Shield shield in shields)
                {
                    for (int x = 0; x < level; x++)
                    {
                        shield.Rebuild();
                    }
                } 
            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
