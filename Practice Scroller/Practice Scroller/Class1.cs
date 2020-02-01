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
    class Sprite
    {
        public Vector2 Position;
        public Color Tint;
        public Texture2D Image;

        public Sprite(Vector2 position, Color tint, Texture2D image)
        {
            Position = position;
            Tint = tint;
            Image = image;
        }
    }
}