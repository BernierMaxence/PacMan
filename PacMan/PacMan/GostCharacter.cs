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
            Direction dir = (Direction)rand.Next(0, 4);
            Console.WriteLine("dir " + dir);
            return dir; 
        }
        public Direction nextDirection()
        {
            switch(this.Direction)
            {
                case Direction.Down:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Up;
                case Direction.Up:
                    return Direction.Left;
                case Direction.Left:
                    return Direction.Down;
                default:
                    return Direction.Down; 
            }
        }
    }
    
}
