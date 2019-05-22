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
    class AlienFleet
    {
        public Alien[,] aliens { get; set; }
        public float ScreenWidth { get; set; } //width of game screen
        public float SpaceShipHeight { get; set; } //height of game screen
        private bool GoingRight = true;
        private bool MovingDown;
        public float Speed = 1;
        private GameContent gameContent;

        public AlienFleet(float x, float y, float screenWidth, float spaceShipHeight, SpriteBatch spriteBatch, GameContent gameContent)
        {
            this.gameContent = gameContent;
            aliens = new Alien[11, 5];
            float alienX = x;
            float alienY = y;
            ScreenWidth = screenWidth;
            SpaceShipHeight = spaceShipHeight;
            for (int i = 0; i < 11; i++)
            {
                alienX = x + i * ((gameContent.imgAlien.Width + 1)*7/40 + 35);
                for (int j = 0; j < 5; j++)
                {
                    alienY = y + j * ((gameContent.imgAlien.Height + 1)*7/40 + 35);
                    Alien alien = new Alien(alienX, alienY, ScreenWidth, SpaceShipHeight, spriteBatch, gameContent);
                    aliens[i, j] = alien;
                }
            }
            
        }

        public bool Move()
        {
            MovingDown = false;
            bool aliensWin = false;
            foreach(Alien alien in aliens)
            {
                if (alien.Visible)
                {
                    if(alien.HitBottom())
                    {
                        return true;
                    }
                    if (GoingRight)
                    {
                        if (alien.HitRight())
                        {
                            MovingDown = true;
                            GoingRight = false;
                        }
                    }
                    else
                    {
                        if (alien.HitLeft())
                        {
                            MovingDown = true;
                            GoingRight = true;
                        }
                    }
                }
            }
            foreach(Alien alien in aliens)
            {
                if(alien.Visible)
                {
                    if (MovingDown)
                    {
                        alien.MoveDown();
                    }
                    else
                    {
                        if (GoingRight)
                        {
                            alien.MoveRight();
                        }
                        else
                        {
                            alien.MoveLeft();
                        }
                        Speed = alien.Speed;
                    }
                }   
            }
            return aliensWin;
        }

        public void FireBlasters(SpaceShip spaceShip, Shield[] shields, int fireRate)
        {
            Random random = new Random();
            foreach (Alien alien in aliens)
            {
                int r = random.Next(0, fireRate);
                if (r == 0)
                {
                    alien.fireBlaster();
                }
                foreach (Blaster blaster in alien.blasters)
                {
                    blaster.Fire();
                    foreach (Shield shield in shields)
                    {
                        blaster.ShootShield(shield);
                    }
                    blaster.ShootSpaceShip(spaceShip);
                }
            }
        }

        public void Draw()
        {     
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    aliens[i, j].Draw();
                }
            }
        }
    }
}

