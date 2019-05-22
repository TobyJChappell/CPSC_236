using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tChappell_proj06
{
    class UFO
    {
        public float X { get; set; } //x position of ufo on screen
        public float Y { get; set; } //y position of ufo on screen
        public float Width { get; set; } //width of ufo
        public float Height { get; set; } //height of ufo
        public float ScreenWidth { get; set; }
        public bool Visible { get; set; }
        private GameContent gameContent;
        private Texture2D imgUFO { get; set; }  //cached image of the ufo
        private SpriteBatch spriteBatch;
        public List<Blaster> blasters { get; set; }
        private bool GoingRight;
        public SoundEffectInstance ufoSoundInstance;


        public UFO(float x, float y, float screenWidth, SpriteBatch spriteBatch, GameContent gameContent)
        {
            X = x;
            Y = y;
            ScreenWidth = screenWidth;
            this.gameContent = gameContent;
            imgUFO = gameContent.imgUFO;
            Width = imgUFO.Width / 5;
            Height = imgUFO.Height / 5;
            Visible = false;
            this.spriteBatch = spriteBatch;
            ufoSoundInstance = gameContent.ufoSound.CreateInstance();
        }

        public void MoveLeft()
        {
            if (X < -Width)
            {
                Visible = false;
            }
            if (Visible)
            {
                X -= 5;
            }
        }

        public void MoveRight()
        {
            if(X > ScreenWidth)
            {
                Visible = false;
            }
            if (Visible)
            {
                X += 5;
            }
        }

        public void Move()
        {
            if(Visible)
            {
                if(GoingRight)
                {
                    MoveRight();
                }
                else
                {
                    MoveLeft();
                }
            }
            else {
                Random random = new Random();
                int r = random.Next(0, 2500);
                if (r == 0)
                {
                    Visible = true;
                    r = random.Next(0, 2);
                    if (r == 0)
                    {
                        GoingRight = true;
                        X = -Width;
                    }
                    else
                    {
                        GoingRight = false;
                        X = ScreenWidth;
                    }
                }
            }           
        }

        public void Draw(SpaceShip spaceShip)
        {
            if (Visible)
            {
                if(!spaceShip.Visible)
                {
                    ufoSoundInstance.Pause();
                }
                else if (ufoSoundInstance.State != SoundState.Playing)
                {
                    ufoSoundInstance.Play();
                }
                spriteBatch.Draw(imgUFO, new Vector2(X, Y), null, Color.Red, 0, new Vector2(0, 0), 0.2f, SpriteEffects.None, 0);
            }
        }
    }
}
