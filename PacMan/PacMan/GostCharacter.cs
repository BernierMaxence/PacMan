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
            List<Direction> allowedDirections = new List<Direction>();
            allowedDirections.Add(Direction.Up);
            allowedDirections.Add(Direction.Right);
            allowedDirections.Add(Direction.Left);
            allowedDirections.Add(Direction.Down);
            return allowedRandomDiretion(allowedDirections); 

        }

        public Direction allowedRandomDiretion(List<Direction> allowedDirections)
        {
            Random rand = new Random();
            int decision = rand.Next(0, 4);
            if (decision < allowedDirections.Count)
            {
                return allowedDirections.ElementAt(decision); 
            } else
            {
                allowedDirections.RemoveAt(decision);
                return allowedRandomDiretion(allowedDirections); 
            }
            
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
