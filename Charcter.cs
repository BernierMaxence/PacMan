using System;

public class Charcter
{
    private Position _position; 


	public Charcter(Position position)
	{
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
}
