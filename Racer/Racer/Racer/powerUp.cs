using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Racer
{
    class powerUp : Wall //subclassing Wall
    {

        Boolean testbool;
        Texture2D texture;
        Rectangle screenBounds;

        //public powerUp(Texture2D texture, Rectangle screenBounds) : base(Texture2D texture, Rectangle screenBounds)
        public powerUp(Texture2D texture, Rectangle screenBounds) : base(texture, screenBounds)
        {
            this.testbool = true;
            this.texture = texture;
            this.screenBounds = screenBounds;
        }

        public void addShields(Car Player)//increases number of shields by 1
        {
            Player.addShields();
        }
    }
}