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
    class SpaceShip
    {
        public float X { get; set; } //x position of spaceship on screen
        public float Y { get; set; } //y position of spaceship on screen
        public float Width { get; set; } //width of spaceship
        public float Height { get; set; } //height of spaceship
        public float ScreenWidth { get; set; } //width of game screen
        public List<Bullet> bullets { get; set; }
        public bool Visible { get; set; }
        public Texture2D imgSpaceShip { get; set; }  //cached image of the space ship
        private SpriteBatch spriteBatch;
        private GameContent gameContent;

        public SpaceShip(float x, float y, float screenWidth, SpriteBatch spriteBatch, GameContent gameContent)
        {
            X = x;
            Y = y;
            this.gameContent = gameContent;
            imgSpaceShip = gameContent.imgSpaceShip;
            Width = imgSpaceShip.Width/4;
            Height = imgSpaceShip.Height/4;
            this.spriteBatch = spriteBatch;
            ScreenWidth = screenWidth;
            bullets = new List<Bullet>();
            Visible = true;
        }

        public void Draw()
        {
            if(Visible)
            {
                spriteBatch.Draw(imgSpaceShip, new Vector2(X, Y), null, Color.White, 0, new Vector2(0, 0), 0.25f, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(imgSpaceShip, new Vector2(X - 40, Y + 10), null, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            }
            foreach (Bullet bullet in bullets)
            {
                bullet.Draw();
            }
        }

        public void RespawnSpaceShip()
        {
            imgSpaceShip = gameContent.imgSpaceShip;
            Visible = true;
        }

        public void ExplodeSpaceShip()
        {
            imgSpaceShip = gameContent.imgDestroyedSpaceShip;
            Visible = false;
        }

        public void MoveLeft()
        {
            X = X - 10;
            if (X < 1)
            {
                X = 1;
            }
        }

        public void MoveRight()
        {
            X = X + 10;
            if ((X + Width) > ScreenWidth)
            {
                X = ScreenWidth - Width;
            }
        }

        public void MoveTo(float x)
        {
            if (x >= 0)
            {
                if (x < ScreenWidth - Width)
                {
                    X = x;
                }
                else
                {
                    X = ScreenWidth - Width;
                }
            }
            else
            {
                if (x < 0)
                {
                    X = 0;
                }
            }
        }

        public void fireBullet()
        {
            Bullet bullet = new Bullet(X + Width/2, Y, spriteBatch, gameContent);
            bullets.Add(bullet);
            PlaySound(gameContent.shootSound);
        }

        private void PlaySound(SoundEffect sound)
        {
            float volume = 0.06f;
            float pitch = 0.0f;
            float pan = 0.0f;
            sound.Play(volume, pitch, pan);
        }
    }
}
