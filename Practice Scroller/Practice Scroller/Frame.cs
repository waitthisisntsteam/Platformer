using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Practice_Scroller
{
    class Frame
    {
        public Vector2 Origin;
        public Rectangle SourceRectangle;

        public Frame(Vector2 origin, Rectangle sourcerectangle)
        {
            Origin = origin;
            SourceRectangle = sourcerectangle;
        }
    }
}
