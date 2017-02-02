using System;

public class PacManCharacter : Character 
{
    private int _life;
    private Direction _direction; 
	public PacManCharacter(Position position, int life, Direction direction) : base(position, direction)
	{
       this._life = life;
        this._direction = direction; 
	}

    public int getLife()
    {
        return _life; 
    }
    public void setLife(int life)
    {
        this._life = life; 
    }

    public void looseLife()
    {
        --_life; 
    }
    /*
    public Direction getDirection()
    {
        return this._direction; 
    }
    public void setDirection(Direction direction)
    {
        this._direction = direction;
    }
    */

  
}
