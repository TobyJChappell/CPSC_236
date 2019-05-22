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
    class Blaster
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public bool Visible { get; set; }
        public SpriteBatch spriteBatch;
        private Texture2D imgBlaster { get; set; }
        public GameContent gameContent;

        public Blaster(float x, float y, SpriteBatch spriteBatch, GameContent gameContent)
        {
            this.gameContent = gameContent;
            Visible = true;
            imgBlaster = gameContent.imgBlaster;
            Width = imgBlaster.Width/3;
            Height = imgBlaster.Height/3;
            this.spriteBatch = spriteBatch;
            X = x - Width / 2;
            Y = y;
        }

        public void Fire()
        {
            Y += 10;
        }

        public void ShootSpaceShip(SpaceShip spaceShip)
        {
            Rectangle blasterRect = new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
            if (spaceShip.Visible && Visible)
            {
                Rectangle spaceShipRect = new Rectangle((int)spaceShip.X, (int)spaceShip.Y, (int)spaceShip.Width, (int)spaceShip.Height);
                if (HitTest(blasterRect, spaceShipRect))
                {
                    PlaySound(gameContent.explodeSound);
                    Visible = false;
                    spaceShip.ExplodeSpaceShip();
                }
            }
        }

        public void ShootShield(Shield shield)
        {
            foreach (ShieldBlock shieldBlock in shield.ShieldBlocks)
            {
                if(shieldBlock.Visible && Visible)
                {
                    Rectangle blasterRect = new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
                    Rectangle shieldRect = new Rectangle((int)shieldBlock.X, (int)shieldBlock.Y, (int)shieldBlock.Width, (int)shieldBlock.Height);
                    if (HitTest(blasterRect, shieldRect))
                    {
                        Visible = false;
                        shieldBlock.Visible = false;
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
                spriteBatch.Draw(imgBlaster, new Vector2(X, Y), null, Color.White, 0, new Vector2(0, 0), 0.3333f, SpriteEffects.None, 0);
            }
        }

        private void PlaySound(SoundEffect sound)
        {
            float volume = 0.25f;
            float pitch = 0.0f;
            float pan = 0.0f;
            sound.Play(volume, pitch, pan);
        }
    }
}