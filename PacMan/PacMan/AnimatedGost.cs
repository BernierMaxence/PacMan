using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan
{
    class AnimatedGost : AnimatedObject
    {
        private GostCharacter _gostCharacter;
         
        public AnimatedGost(Texture2D texture, Vector2 position, Vector2 size, GostCharacter gostCharacter) : base(texture, position, size)
        {
            this._gostCharacter = gostCharacter; 

        }

        public GostCharacter gostCharacter
        {
            get { return _gostCharacter;  }
            set { _gostCharacter = value;  }
        }
    }
}
