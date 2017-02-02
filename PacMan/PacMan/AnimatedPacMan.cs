using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan
{
    class AnimatedPacMan : AnimatedObject
    {
        PacManCharacter _pacManCharacter; 

        public AnimatedPacMan(Texture2D texture, Vector2 position, Vector2 size, PacManCharacter pacMan) : base(texture, position, size)
        {
            this._pacManCharacter = pacMan; 
        }
        public PacManCharacter pacManCharacter
        {
            get { return _pacManCharacter; }
            set { _pacManCharacter = value; }
        }

    }
}
