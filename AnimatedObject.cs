using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;   //   for Texture2D
using Microsoft.Xna.Framework;  //  for Vector2
namespace PacMan
{
    private Texture2D _texture;    //  sprite texture 
    private Vector2 _position;     //  sprite position on screen
    private Vector2 _size;         //  sprite size in pixels



    public class AnimatedObject
    {
        public AnimatedObject(Texture2D texture, Vector2 position, Vector2 size)
        {
            this._texture = texture;
            this._position = position;
            this._size = size;
        }

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }


        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        public Vector2 Size
        {
            get { return _size; }
            set { _size = value; }
        }

        
    }

}
