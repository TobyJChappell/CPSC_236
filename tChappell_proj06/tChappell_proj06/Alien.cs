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
    class Alien
    {
        public float X { get; set; } //x position of alien on screen
        public float Y { get; set; } //y position of alien on screen
        public float Speed = 1;
        public float Width { get; set; } //width of alien
        public float Height { get; set; } //height of alien
        public float ScreenWidth { get; set; }
        public float SpaceShipHeight { get; set; }
        public bool Visible { get; set; }
        private GameContent gameContent;
        private Texture2D imgAlien { get; set; }  //cached image of the alien
        private SpriteBatch spriteBatch;
        public List<Blaster> blasters { get; set; }

        public Alien(float x, float y, float screenWidth, float spaceShipHeight, SpriteBatch spriteBatch, GameContent gameContent)
        {
            blasters = new List<Blaster>();
            X = x;
            Y = y;
            ScreenWidth = screenWidth;
            SpaceShipHeight = spaceShipHeight;
            this.gameContent = gameContent;
            imgAlien = gameContent.imgAlien;
            Width = imgAlien.Width*7/40;
            Height = imgAlien.Height*7/40;
            Visible = true;
            this.spriteBatch = spriteBatch;
        }

        public void MoveLeft()
        {
            if(Visible)
            {
                X -= Speed;
            }           
        }

        public void MoveRight()
        {
            if(Visible)
            {
                X += Speed;
            }
        }

        public void MoveDown()
        {
            if(Visible)
            {
                Y += Height;
                Speed += 0.5f;
            }
        }

        public bool HitLeft()
        {
            float x = X - Speed;
            if (x < 0 && Visible)
            {
                return true;
            }
            return false;
        }

        public bool HitRight()
        {
            float x = X + Speed;
            if ((x + Width) > ScreenWidth && Visible)
            {
                return true;
            }
            return false;
        }

        public bool HitBottom()
        {
            float y = Y + Height;
            if ((y + Height) > SpaceShipHeight - 125 && Visible)
            {
                return true;
            }
            return false;
        }

        public void fireBlaster()
        {
            if(Visible)
            {
                Blaster blaster = new Blaster(X + Width / 2, Y + Height, spriteBatch, gameContent);
                blasters.Add(blaster);
            }
        }

        public void Draw()
        {
            if(Visible)
            {
                spriteBatch.Draw(imgAlien, new Vector2(X, Y), null, Color.White, 0, new Vector2(0, 0), 0.175f, SpriteEffects.None, 0); 
            }
            foreach (Blaster blaster in blasters)
            {
                blaster.Draw();
            }
        }
    }
}
