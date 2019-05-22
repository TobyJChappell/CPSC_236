using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace tChappell_proj06
{
    class GameContent
    {
        public Texture2D imgSpaceShip { get; set; }
        public Texture2D imgDestroyedSpaceShip { get; set; }
        public Texture2D imgAlien { get; set; }
        public Texture2D imgUFO { get; set; }
        public Texture2D imgPixel { get; set; }
        public Texture2D imgBlaster { get; set; }
        public SoundEffect shootSound { get; set; }
        public SoundEffect explodeSound { get; set; }
        public SoundEffect ufoSound { get; set; }
        public SoundEffect ufoShotSound { get; set; }
        public SoundEffect alienShotSound { get; set; }
        public SoundEffect speed1Sound { get; set; }
        public SoundEffect speed2Sound { get; set; }
        public SoundEffect speed3Sound { get; set; }
        public SoundEffect speed4Sound { get; set; }
        public SpriteFont labelFont { get; set; }

        public GameContent(ContentManager Content)
        {
            //load images
            imgSpaceShip = Content.Load<Texture2D>("SpaceShip");
            imgDestroyedSpaceShip = Content.Load<Texture2D>("DestroyedSpaceShip");
            imgAlien = Content.Load<Texture2D>("Alien");
            imgUFO = Content.Load<Texture2D>("UFO");
            imgPixel = Content.Load<Texture2D>("Pixel");
            imgBlaster = Content.Load<Texture2D>("Blaster");

            //load sounds
            shootSound = Content.Load<SoundEffect>("shoot");
            explodeSound = Content.Load<SoundEffect>("explosion");
            ufoSound = Content.Load<SoundEffect>("ufo_lowpitch");
            ufoShotSound = Content.Load<SoundEffect>("ufo_highpitch");
            alienShotSound = Content.Load<SoundEffect>("invaderKilled");
            speed1Sound = Content.Load<SoundEffect>("fastinvader1");
            speed2Sound = Content.Load<SoundEffect>("fastinvader2");
            speed3Sound = Content.Load<SoundEffect>("fastinvader3");
            speed4Sound = Content.Load<SoundEffect>("fastinvader4");
            //load fonts
            labelFont = Content.Load<SpriteFont>("Arial20");

        }
    }
}
