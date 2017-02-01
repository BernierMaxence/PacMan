using System;

public enum Direction { Up, Down ,Left, Right };
public class PacManCharacter : Character 
{
    private int _life;
    private Direction _direction; 
	public PacManCharacter(Position position, int life, Direction direction) : base(position)
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

    public Direction Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }
}
