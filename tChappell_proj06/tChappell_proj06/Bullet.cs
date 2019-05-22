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
    class Bullet
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public bool Visible { get; set; }
        SpriteBatch spriteBatch;
        GameContent gameContent;
        private Texture2D imgPixel { get; set; }

        public Bullet(float x, float y, SpriteBatch spriteBatch, GameContent gameContent)
        {
            Visible = true;
            imgPixel = gameContent.imgPixel;
            this.spriteBatch = spriteBatch;
            Width = imgPixel.Width * 5;
            Height = imgPixel.Height * 5;
            X = x - Width/2;
            Y = y;
            this.gameContent = gameContent;
        }

        public void Fire()
        {
            Y -= 10;
        }

        public int ShootAliens(AlienFleet alienFleet)
        {
            int numHit = 0;
            Rectangle bulletRect = new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
            for (int i = 0; i < 11; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    Alien alien = alienFleet.aliens[i, j];
                    if (alien.Visible)
                    {
                        Rectangle alienRect = new Rectangle((int)alien.X, (int)alien.Y, (int)alien.Width, (int)alien.Height);
                        if (HitTest(bulletRect, alienRect))
                        {
                            numHit++;
                            Visible = false;
                            PlaySound(gameContent.alienShotSound);
                            alien.Visible = false;
                        }
                    }
                }
            }
            return numHit;
        }

        public bool ShootUFO(UFO ufo)
        {
            if (ufo.Visible)
            {
                Rectangle bulletRect = new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
                Rectangle ufoRect = new Rectangle((int)ufo.X, (int)ufo.Y, (int)ufo.Width, (int)ufo.Height);
                if (HitTest(bulletRect, ufoRect))
                {
                    Visible = false;
                    ufo.Visible = false;
                    ufo.ufoSoundInstance.Stop();
                    PlaySound(gameContent.ufoShotSound);
                    return true;
                }
            }
            return false;
        }

        public void ShootShield(Shield shield)
        {
            foreach(ShieldBlock shieldBlock in shield.ShieldBlocks)
            {
                if(shieldBlock.Visible)
                {
                    Rectangle bulletRect = new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
                    Rectangle shieldRect = new Rectangle((int)shieldBlock.X, (int)shieldBlock.Y, (int)shieldBlock.Width, (int)shieldBlock.Height);
                    if (HitTest(bulletRect, shieldRect))
                    {
                        Visible = false;
                    }
                }
            }
        }

        public static bool HitTest(Rectangle r1, Rectangle r2)
        {
            if (Rectangle.Intersect(r1, r2) != Rectangle.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Draw()
        {
            if(Visible)
            {
                spriteBatch.Draw(imgPixel, new Vector2(X, Y), null, Color.White, 0, new Vector2(0, 0), 5f, SpriteEffects.None, 0);
            }
        }

        private void PlaySound(SoundEffect sound)
        {
            float volume = 0.03f;
            float pitch = 0.0f;
            float pan = 0.0f;
            sound.Play(volume, pitch, pan);
        }
    }
}
