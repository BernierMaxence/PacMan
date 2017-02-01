using System;

public class PacMan : Character 
{
    private int _life; 

	public PacMan(Position position, int life )
	{
        base(position);
        this._life = life; 
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
        --life; 
    }
}
