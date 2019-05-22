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
    class Shield
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public ShieldBlock[] ShieldBlocks;
        private Texture2D Texture { get; set; }

        public Shield(float x, float y, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            X = x;
            Y = y;
            ShieldBlocks = new ShieldBlock[10];
            for(int i = 0; i < 10; i++)
            {
                ShieldBlocks[i] = new ShieldBlock(x, y + i*10, spriteBatch, graphicsDevice);
            }
        }

        public void Rebuild()
        {
            for(int x = 9; x >= 0; x--)
            {
                if(!ShieldBlocks[x].Visible)
                {
                    ShieldBlocks[x].Visible = true;
                    break;
                }
            }
        }

        public void Draw()
        {
            for(int x = 0; x < 10; x++)
            {
                ShieldBlocks[x].Draw();
            }           
        }
    }
}