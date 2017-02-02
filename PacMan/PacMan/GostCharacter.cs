using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan
{
    class GostCharacter : Character
    {
        public GostCharacter(Position position, Direction direction) : base(position, direction)
        {

        }

        public Direction randomDirection()
        {
            Random rand = new Random();
            return (Direction)rand.Next(1, 5);

        }
    }
    
}
