using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacMan
{
    public class GhostCharacter : Character
    {
        bool _inHouse;
        bool _scared; 
        public GhostCharacter(Position position, Direction direction) : base(position, direction)
        {
            this._inHouse = true;
            this._inHouse = false;
        }
        public bool InHouse
        {
            get { return _inHouse; }
            set { _inHouse = value; }
        }
        
        public bool Scared
        {
            get { return _scared; }
            set { _scared = value; }
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

        /*
        public Direction nextDirection()
        {
            List<Direction> allowedDirections = new List<Direction>();
            allowedDirections.Add(Direction.Up);
            allowedDirections.Add(Direction.Right);
            allowedDirections.Add(Direction.Left);
            allowedDirections.Add(Direction.Down);
            return allowedNextDirection(allowedDirections);
        }

        public Direction allowedNextDirection(List<Direction> allowedDirections)
        {
            switch (this.Direction)
            {
                case Direction.Down:
                    if (allowedDirections.Contains(Direction.Right))
                    {
                        return Direction.Right; 
                    }
                    else
                    {
                        allowedDirections.Remove(Direction.Right);
                        return allowedNextDirection(allowedDirections); 
                    }
                case Direction.Right:
                    if (allowedDirections.Contains(Direction.Up))
                    {
                        return Direction.Up;
                    }
                    else
                    {
                        allowedDirections.Remove(Direction.Up);
                        return allowedNextDirection(allowedDirections);
                    }
                case Direction.Up:
                    if (allowedDirections.Contains(Direction.Left))
                    {
                        return Direction.Left;
                    }
                    else
                    {
                        allowedDirections.Remove(Direction.Left);
                        return allowedNextDirection(allowedDirections);
                    }
                case Direction.Left:
                    if (allowedDirections.Contains(Direction.Down))
                    {
                        return Direction.Down;
                    }
                    else
                    {
                        allowedDirections.Remove(Direction.Down);
                        return allowedNextDirection(allowedDirections);
                    }
                default:
                    return Direction.Down;
            }
        }
        */
       
    }
    
}
