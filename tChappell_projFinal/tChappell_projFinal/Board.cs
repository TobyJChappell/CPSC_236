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
    class Board
    {
        public int Rows;
        public int Columns;
        public int DistanceFromTop;
        public float SideLength;
        public int NumMines;
        public Tile[,] Tiles;

        /**
         * Board Constructor
         * @param rows Number of rows
         * @param columns Number of columns
         * @param distanceFromTop Number of pixels from the top of the screen the board is being drawn
         * @param numMines Total number of mines on the board
         * @param spriteBatch SpriteBatch
         * @param gameContent GameContent
         */
        public Board(int rows, int columns, int distanceFromTop, int numMines, SpriteBatch spriteBatch, GameContent gameContent)
        {
            Rows = rows;
            Columns = columns;
            DistanceFromTop = distanceFromTop;
            SideLength = gameContent.imgBlankTile.Width*3;
            NumMines = numMines;
            Tiles = new Tile[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Tiles[i, j] = new Tile(i * SideLength, j * SideLength + DistanceFromTop, spriteBatch, gameContent);
                }
            }
            Random random = new Random();
            int x;
            int y;
            while (numMines > 0)
            {
                x = random.Next(Rows);
                y = random.Next(Columns);
                if (!Tiles[x, y].hasMine)
                {
                    Tiles[x, y].hasMine = true;
                    numMines--;
                }
            }
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Tiles[i, j].numNeighbors = CheckNeighbors(i, j);
                }
            }
        }

        /**
         * Draws the board to the screen
         */
        public void Draw()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Tiles[i, j].Draw();
                }
            }
        }

        /**
         * Counts the number of neighbors with a mine a tile has
         * @param x Row
         * @param y Column
         * @return int Number of neighbors with mines
         */
        public int CheckNeighbors(int x, int y)
        {
            int numNeighbors = 0;
            for (int i = -1 + x; i <= 1 + x; i++)
            {
                for (int j = -1 + y; j <= 1 + y; j++)
                {
                    if (i >= 0 && j >= 0 && i < Rows && j < Columns)
                    {
                        if (Tiles[i, j].hasMine)
                        {
                            numNeighbors++;
                        }
                    }
                }
            }
            return numNeighbors;
        }

        /**
         * Flag, ?, or unflags a tile based on where the user clicks
         * @param x X location of the mouse
         * @param y Y location of the mouse
         * @return int +1 if tile is unflagged and -1 if tile is flagged
         */
        public int FlagTile(float x, float y)
        {
            int flagged = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (Tiles[i, j].contains(x, y))
                    {
                        if(!Tiles[i, j].Visible)
                        {
                            if (Tiles[i, j].hasQuestionMark)
                            {
                                Tiles[i, j].hasQuestionMark = false;
                            }
                            else if (Tiles[i, j].hasFlag)
                            {
                                Tiles[i, j].hasFlag = false;
                                Tiles[i, j].hasQuestionMark = true;
                                flagged = 1;
                            }
                            else
                            {
                                Tiles[i, j].hasFlag = true;
                                flagged = -1;
                            }
                        }
                        return flagged;
                    }
                }
            }
            return flagged;
        }

        /**
         * Reveals a tile based on where the user clicks
         * @param x X location of the mouse
         * @param y Y location of the mouse
         * @return bool True if the user clicked on a tile
         */
        public bool ChooseTile(float x, float y)
        {
            for(int i = 0; i < Rows; i++)
            {
                for(int j = 0; j < Columns; j++)
                {
                    if (Tiles[i, j].contains(x, y))
                    {
                        RevealTile(i, j);
                        return true;
                    }
                }
            }
            return false;
        }

        /**
         * Reveals all of the mines as well as incorrect flagging on the board (called if player loses)
         */
        public void RevealBoard()
        {
            foreach(Tile tile in Tiles)
            {
                if(tile.hasMine || tile.hasFlag)
                {
                    tile.Visible = true;
                } 
            }
        }

        /**
         * Reveals a tile on the board according to the following rules:
         *      If tile has a flag, nothing happens
         *      Else the tile is made visible:
         *          If a tile has a mine, the entire board is revealed and the player loses
         *          If a the tile has 0 neighbors, RevealTile is recursively called for each of its neighbors
         * @param x Row
         * @param y Column
         */
        public void RevealTile(int x, int y)
        {
            if(Tiles[x, y].hasFlag)
            {
                return;
            }
            Tiles[x, y].Visible = true;
            if (Tiles[x, y].hasMine)
            {
                Tiles[x, y].firstMine = true;
                RevealBoard();
            }
            else if(Tiles[x, y].numNeighbors == 0)
            {
                for (int i = -1 + x; i <= 1 + x; i++)
                {
                    for (int j = -1 + y; j <= 1 + y; j++)
                    {
                        if (i >= 0 && j >= 0 && i < Rows && j < Columns && !Tiles[i, j].Visible)
                        {
                            RevealTile(i, j);
                        }
                    }
                }
            }
        }
    }		
}