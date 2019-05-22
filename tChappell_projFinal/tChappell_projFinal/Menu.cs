using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tChappell_projFinal
{
    class Menu
    {
        public GameContent gameContent;
        public SpriteBatch spriteBatch;
        private int screenWidth;
        private int screenHeight;
        public Vector2 easy;
        public Vector2 medium;
        public Vector2 hard;

        /**
         * Menu Constructor
         * @param screenWidth Width of the screen
         * @param screenHeight Height of the screen
         * @param spriteBatch SpriteBatch
         * @param gameContent GameContent
         */
        public Menu(int screenWidth, int screenHeight, SpriteBatch spriteBatch, GameContent gameContent)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.spriteBatch = spriteBatch;
            this.gameContent = gameContent;
            easy = gameContent.menuText.MeasureString("Easy");
            medium = gameContent.menuText.MeasureString("Medium");
            hard = gameContent.menuText.MeasureString("Hard");
        }

        /**
         * Draws the menu to the screen
         */ 
        public void Draw()
        {
            Vector2 title = gameContent.menuText.MeasureString("Minesweeper");
            spriteBatch.DrawString(gameContent.menuText, "Minesweeper", new Vector2((screenWidth - title.X) / 2, (screenHeight - title.Y) / 16), Color.Black);

            spriteBatch.DrawString(gameContent.menuText, "Easy", new Vector2((screenWidth - easy.X) / 2, (screenHeight - easy.Y) / 4), Color.Green);
            Vector2 easyDescription = gameContent.text.MeasureString("9 x 9 field, 10 mines");
            spriteBatch.DrawString(gameContent.text, "9 x 9 field, 10 mines", new Vector2((screenWidth - easyDescription.X) / 2, (screenHeight - easy.Y) / 4 + easy.Y), Color.Black);
            spriteBatch.DrawString(gameContent.menuText, "Medium", new Vector2((screenWidth - medium.X) / 2, (screenHeight - medium.Y) / 2), Color.Yellow);
            Vector2 mediumDescription = gameContent.text.MeasureString("16 x 16 field, 40 mines");
            spriteBatch.DrawString(gameContent.text, "16 x 16 field, 40 mines", new Vector2((screenWidth - mediumDescription.X) / 2, (screenHeight - medium.Y) / 2 + medium.Y), Color.Black);
            spriteBatch.DrawString(gameContent.menuText, "Hard", new Vector2((screenWidth - hard.X) / 2, 3 * (screenHeight - hard.Y) / 4), Color.Red);
            Vector2 hardDescription = gameContent.text.MeasureString("30 x 16 field, 99 mines");
            spriteBatch.DrawString(gameContent.text, "30 x 16 field, 99 mines", new Vector2((screenWidth - hardDescription.X) / 2, 3 * (screenHeight - hard.Y) / 4 + hard.Y), Color.Black);
        }

        /**
         * Determines if the user has clicked on "Easy", "Medium", or "Hard"
         * @param x X location of the mouse
         * @param y Y location of the mouse
         * @return int Number corresponding to which label the user chose
         */ 
        public int contains(float x, float y)
        {
            if ((screenWidth - easy.X) / 2 < x && ((screenWidth - easy.X) / 2 + easy.X) > x && (screenHeight - easy.Y) / 4 < y && (screenHeight - easy.Y) / 4 + easy.Y > y)
            {
                return 1;
            }
            else if((screenWidth - medium.X) / 2 < x && ((screenWidth - medium.X) / 2 + medium.X) > x && (screenHeight - medium.Y) / 2 < y && ((screenHeight - medium.Y) / 2 + medium.Y) > y)
            {
                return 2;
            }
            else if ((screenWidth - hard.X) / 2 < x && ((screenWidth - hard.X) / 2 + hard.X) > x && (3 * (screenHeight - hard.Y) / 4) < y && (3 * (screenHeight - hard.Y) / 4 + hard.Y)  > y)
            {
                return 3;
            }
            return 0;
        }
    }
}
