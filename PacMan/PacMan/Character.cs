﻿using System;

public class Character
{
    private Position _position; 


	public Character(Position position)
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

    public Position Position
    {
        get { return _position; }
        set { _position = value; }
    }

  
}
