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
    class ShieldBlock
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public bool Visible { get; set; }
        public SpriteBatch spriteBatch;
        private Texture2D Texture { get; set; }

        public ShieldBlock(float x, float y, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            this.spriteBatch = spriteBatch;
            Visible = true;
            Texture = new Texture2D(graphicsDevice, 200, 10);
            Width = 200;
            Height = 10;
            X = x;
            Y = y;
            Color[] colorData = new Color[200 * 10];
            for (int i = 0; i < 200*10; i++)
            {
                colorData[i] = Color.GreenYellow;
            }
            Texture.SetData<Color>(colorData);
        }

        public void Draw()
        {
            if(Visible)
            {
                spriteBatch.Draw(Texture, new Vector2(X, Y), null, Color.GreenYellow, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            }
        }
    }
}