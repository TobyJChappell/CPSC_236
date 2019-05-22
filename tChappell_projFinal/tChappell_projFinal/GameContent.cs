using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace tChappell_projFinal
{
    class GameContent
    {
        public Texture2D imgMine;
        public Texture2D imgAllMines;
        public Texture2D imgIncorrectMine;
        public Texture2D imgFlag;
        public Texture2D imgQuestionMark;
        public Texture2D imgBlankTile;
        public Texture2D imgNumber0;
        public Texture2D imgNumber1;
        public Texture2D imgNumber2;
        public Texture2D imgNumber3;
        public Texture2D imgNumber4;
        public Texture2D imgNumber5;
        public Texture2D imgNumber6;
        public Texture2D imgNumber7;
        public Texture2D imgNumber8;
        public SpriteFont text;
        public SpriteFont menuText;

        /**
         * GameContent Constructor
         * @param Content ContentManager
         */ 
        public GameContent(ContentManager Content)
        {
            imgMine = Content.Load<Texture2D>("Bomb");
            imgAllMines = Content.Load<Texture2D>("AllBombs");
            imgIncorrectMine = Content.Load<Texture2D>("IncorrectBomb");
            imgFlag = Content.Load<Texture2D>("Flag");
            imgQuestionMark = Content.Load<Texture2D>("QuestionMark");
            imgBlankTile = Content.Load<Texture2D>("BlankTile");
            imgNumber0 = Content.Load<Texture2D>("Number0");
            imgNumber1 = Content.Load<Texture2D>("Number1");
            imgNumber2 = Content.Load<Texture2D>("Number2");
            imgNumber3 = Content.Load<Texture2D>("Number3");
            imgNumber4 = Content.Load<Texture2D>("Number4");
            imgNumber5 = Content.Load<Texture2D>("Number5");
            imgNumber6 = Content.Load<Texture2D>("Number6");
            imgNumber7 = Content.Load<Texture2D>("Number7");
            imgNumber8 = Content.Load<Texture2D>("Number8");

            text = Content.Load<SpriteFont>("Text");
            menuText = Content.Load<SpriteFont>("MenuText");
        }

        /**
         * Returns the correct number image a tile is based on how many neighbors that tile has
         * @param numNeighbors Neighbors with mines
         * @return Texture2D The correct image
         */ 
        public Texture2D GetNumberImg(int numNeighbors)
        {
            Texture2D imgNumber = imgBlankTile;
            switch(numNeighbors)
            {
                case 0:
                    imgNumber = imgNumber0;
                    break;
                case 1:
                    imgNumber = imgNumber1;
                    break;
                case 2:
                    imgNumber = imgNumber2;
                    break;
                case 3:
                    imgNumber = imgNumber3;
                    break;
                case 4:
                    imgNumber = imgNumber4;
                    break;
                case 5:
                    imgNumber = imgNumber5;
                    break;
                case 6:
                    imgNumber = imgNumber6;
                    break;
                case 7:
                    imgNumber = imgNumber7;
                    break;
                case 8:
                    imgNumber = imgNumber8;
                    break;
            }
            return imgNumber;
        }
    }
}
