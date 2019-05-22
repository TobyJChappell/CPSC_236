using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tChappell_projFinal
{
    class Tile
    {
        public float X;
        public float Y;
        public float SideLength;
        public bool hasMine;
        public bool hasFlag;
        public bool hasQuestionMark;
        public bool Visible;
        public bool firstMine;
        public int numNeighbors;
        public SpriteBatch spriteBatch;
        public GameContent gameContent;

         /**
         * Tile Constructor
         * @param x X position
         * @param y Y position
         * @param spriteBatch SpriteBatch
         * @param gameContent GameContent
         */
        public Tile(float x, float y, SpriteBatch spriteBatch, GameContent gameContent)
        {
            X = x;
            Y = y;
            SideLength = gameContent.imgBlankTile.Width*3;
            hasMine = false;
            hasFlag = false;
            hasQuestionMark = false;
            Visible = false;
            firstMine = false;
            this.spriteBatch = spriteBatch;
            this.gameContent = gameContent;
        }

        /**
         * Draws a tile based on its properties
         */ 
        public void Draw()
        {
            if(!Visible)
            {
                if(hasFlag)
                {
                    spriteBatch.Draw(gameContent.imgFlag, new Vector2(X, Y), null, Color.White, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);
                }
                else if(hasQuestionMark)
                {
                    spriteBatch.Draw(gameContent.imgQuestionMark, new Vector2(X, Y), null, Color.White, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);

                }
                else
                {
                    spriteBatch.Draw(gameContent.imgBlankTile, new Vector2(X, Y), null, Color.White, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);
                }
            }
            else
            {
                if(firstMine)
                {
                    spriteBatch.Draw(gameContent.imgMine, new Vector2(X, Y), null, Color.White, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);
                }
                else if(hasFlag && !hasMine)
                {
                    spriteBatch.Draw(gameContent.imgIncorrectMine, new Vector2(X, Y), null, Color.White, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);
                }
                else if(hasMine)
                {
                    spriteBatch.Draw(gameContent.imgAllMines, new Vector2(X, Y), null, Color.White, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(gameContent.GetNumberImg(numNeighbors), new Vector2(X, Y), null, Color.White, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);
                }
            }
        }

        /**
         * Determines if a tile has been clicked
         * @param x X location of the mouse
         * @param y Y location of the mouse
         * @return bool True if tile has been clicked
         */ 
        public bool contains(float x, float y)
        {
            if(X < x && (X + SideLength) > x && Y < y && (Y + SideLength) > y)
            {
                return true;
            }
            return false;
        }
    }
}
