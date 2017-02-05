using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan
{
    class Animatedghost : AnimatedObject
    {
        private GhostCharacter _ghostCharacter;
         
        public Animatedghost(Texture2D texture, Vector2 position, Vector2 size, GhostCharacter ghostCharacter) : base(texture, position, size)
        {
            this._ghostCharacter = ghostCharacter; 

        }

        public GhostCharacter GhostCharacter
        {
            get { return _ghostCharacter;  }
            set { _ghostCharacter = value;  }
        }
    }
}
