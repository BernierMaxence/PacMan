using System;

public class Position
{
    private int _x;
    private int _y;

    public Position(int x, int y)
	{
        this._x = x;
        this._y = y;        

	}
    /*
    public int getX()
    {
        return _x;
    }
    public int getY()
    {
        return _y;
    }

    public void setY(int y)
    {
        this._y = y;
    }
    public void setX(int x)
    {
        this._x = x;
    }
    */
    public int X
    {
        get { return _x; }
        set { _x = value; }
    }

    public int Y
    {
        get { return _y; }
        set { _y = value; }
    }
}
