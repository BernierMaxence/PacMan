using System;

public enum Direction { Up, Down, Left, Right };

public class Character
{
    private bool _moving; 
    private Direction _direction; 
    private Position _position; 


	public Character(Position position, Direction direction)
	{
        this._moving = true;

        this._direction = direction; 
        this._position = position; 

	}

    public void setPosition(Position position)
    {
        this._position = position; 
    }
    public Position getPostion()
    {
        return _position; 
    }

    public Position Position
    {
        get { return _position; }
        set { _position = value; }
    }

    public Direction Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    public bool Moving
    {
        get { return _moving; }
        set { _moving = value; }
    }


}
