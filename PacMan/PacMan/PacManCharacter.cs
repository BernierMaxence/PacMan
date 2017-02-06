using System;

public class PacManCharacter : Character 
{
    private int _life;
    private bool _power; 
    private Direction _direction;
    private bool _dead; 
	public PacManCharacter(Position position, Position initialPosition,  int life, Direction direction) : base(position, initialPosition, direction)
	{
       this._life = life;
        this._power = false; 
        this._direction = direction; 
	}

    public bool Dead
    {
        get { return _dead; }
        set { _dead = value; }
    }

    public bool Power
    {
        get { return _power ; }
        set { _power = value; }
    }

    public int Life
    {
        get { return _life; }
        set { _life = value; }
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
