using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PacMan
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        // Characters 
        PacManCharacter pacManCharacter;
        GostCharacter gostCharacterRed; // TODO : create a list of all four gosts 
        // Animated characters
        AnimatedPacMan animatedPacMan;
        List<AnimatedGost> listAnimatedGosts;

        List<GostCharacter> listGostCharacters;  
        AnimatedGost animatedGostRed; 

        int timer = 0;
        int counterAnimation =0;

        int score = 0;
        int nbBeans = 0;

        byte[,] map;
        AnimatedObject wall;
        AnimatedObject bean;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            pacManCharacter = new PacManCharacter(new Position(1, 1), 3, Direction.Right);
            listGostCharacters = new List<GostCharacter>(); 
            for (int i = 0; i<1; i++)
            {
                listGostCharacters.Add(new GostCharacter(new Position(12+i, 14), Direction.Up)); 
            }
            gostCharacterRed = new GostCharacter(new Position(10, 10), Direction.Up); 



            AnimatedObject wall;
            AnimatedObject bean;

            map = new byte[31, 28]{
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 2, 2, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 2, 2, 2, 2, 2, 2, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 2, 2, 2, 2, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 2, 2, 2, 2, 2, 2, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
            {0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0},
            {0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0},
            {0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0},
            {0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0},
            {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
            {0, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        };
            for (int x = 0; x < 31; x++)
            {
                for (int y = 0; y < 28; y++)
                {
                    if (map[x, y] == 1)
                    {
                        nbBeans++;
                    }
                }
            }

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //  changing the back buffer size changes the window size (when in windowed mode)
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 660;
            graphics.ApplyChanges();
            // on charge un objet mur 
            wall = new AnimatedObject(Content.Load<Texture2D>("mur"), new Vector2(0f, 0f), new Vector2(20f, 20f));
            bean = new AnimatedObject(Content.Load<Texture2D>("bean"), new Vector2(0f, 0f), new Vector2(20f, 20f));
            animatedPacMan = new AnimatedPacMan(Content.Load<Texture2D>("pacmanDroite0"), new Vector2(0f, 0f), new Vector2(20f, 20f), pacManCharacter);
            listAnimatedGosts = new List<AnimatedGost>(); 
            for (int i = 0; i<1; i++)
            {
                listAnimatedGosts.Add(new AnimatedGost(Content.Load<Texture2D>("fantome"+i), new Vector2(0f, 0f), new Vector2(20f, 20f), listGostCharacters.ElementAt(i))); 
            }
            animatedGostRed = new AnimatedGost(Content.Load<Texture2D>("fantome0"), new Vector2(0f, 0f), new Vector2(20f, 20f), gostCharacterRed); 


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            ++timer;
            if (timer%5== 0) 
            {
                ++counterAnimation;
                if (pacManCharacter.Moving)
                {
                    move(pacManCharacter);
                    // move(gostCharacterRed); 
                    moveGosts();
                    animatePacMan();
                    getKeyboardInput();
                } 
                else
                {
                    PacManCharacterDies(); 
                }

            }

            base.Update(gameTime);

            if(nbBeans == 0) {
               // MessageBox.Show("Tout ramassé");
                Exit();
            }



        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            for (int x = 0; x < 31; x++)
            {
                for (int y = 0; y < 28; y++)
                {
                    if (map[x, y] == 0)
                    {
                        int xpos, ypos;
                        xpos = x * 20;
                        ypos = y * 20;
                        Vector2 pos = new Vector2(ypos, xpos);
                        spriteBatch.Draw(wall.Texture, pos, Color.White);
                    }
                    else if (map[x, y] == 1)
                    {
                        int xpos, ypos;
                        xpos = x * 20;
                        ypos = y * 20;
                        Vector2 pos = new Vector2(ypos, xpos);
                        spriteBatch.Draw(bean.Texture, pos, Color.White);
                    }
                }
            }
            spriteBatch.Draw(Content.Load<Texture2D>("barriereFantome"), new Vector2(13 * 20, 12 * 20), Color.White);

            spriteBatch.Draw(animatedPacMan.Texture, new Vector2(pacManCharacter.getPostion().X * 20, pacManCharacter.getPostion().Y * 20), Color.White);
            for (int i=0; i<1; i++)
            {
                spriteBatch.Draw(listAnimatedGosts.ElementAt(i).Texture, new Vector2(listGostCharacters.ElementAt(i).getPostion().X * 20, listGostCharacters.ElementAt(i).getPostion().Y * 20), Color.White);

            }
            //


            base.Draw(gameTime);
            spriteBatch.End();
        }
        public void animatePacMan()
        {
            switch (pacManCharacter.Direction)
            {
                case Direction.Down:
                    if (counterAnimation%2 == 0) animatedPacMan.Texture = Content.Load<Texture2D>("pacmanBas0");
                    else animatedPacMan.Texture = Content.Load<Texture2D>("pacmanBas1");

                    break;
                case Direction.Right:
                    if (counterAnimation % 2 == 0) animatedPacMan.Texture = Content.Load<Texture2D>("pacmanDroite0");
                    else animatedPacMan.Texture = Content.Load<Texture2D>("pacmanDroite1");

                    break;
                case Direction.Left:
                    if (counterAnimation % 2 == 0) animatedPacMan.Texture = Content.Load<Texture2D>("pacmanGauche0");
                    else animatedPacMan.Texture = Content.Load<Texture2D>("pacmanGauche1");

                    break;
                case Direction.Up:
                    if (counterAnimation % 2 == 0) animatedPacMan.Texture = Content.Load<Texture2D>("pacmanHaut0");
                    else animatedPacMan.Texture = Content.Load<Texture2D>("pacmanHaut1");

                    break;
                default:
                    break;
            }
        }
        public void getKeyboardInput()
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Right))
            {
                if (CheckNextCell(pacManCharacter.Position.X + 1, pacManCharacter.Position.Y)) pacManCharacter.Direction = Direction.Right;
              
            }
            else if (keyboard.IsKeyDown(Keys.Left))
            {
                if (CheckNextCell(pacManCharacter.Position.X - 1, pacManCharacter.Position.Y)) pacManCharacter.Direction = Direction.Left;
                
                //on vérifie s’il est possible de se déplacer
                // si oui on avance
            }
            else if (keyboard.IsKeyDown(Keys.Up))
            {
                if (CheckNextCell(pacManCharacter.Position.X, pacManCharacter.Position.Y-1)) pacManCharacter.Direction = Direction.Up;
                
                //on vérifie s’il est possible de se déplacer
                // si oui on avance
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                if (CheckNextCell(pacManCharacter.Position.X, pacManCharacter.Position.Y + 1)) pacManCharacter.Direction = Direction.Down;
                
                
                
                //on vérifie s’il est possible de se déplacer
                // si oui on avance
            }
        }

        public void move(Character character)
        {
            if (character.Moving)
            {
                switch (character.Direction)
                {
                    case Direction.Down:

                        if (CheckNextCell(character.Position.X, character.Position.Y + 1))
                        {
                            character.Position = new Position(character.Position.X, character.Position.Y + 1);
                        }

                        break;

                    case Direction.Right:
                        if (CheckNextCell(character.Position.X + 1, character.Position.Y))
                        {
                            character.Position = new Position(character.Position.X + 1, character.Position.Y);
                        }

                        break;

                    case Direction.Left:
                        if (CheckNextCell(character.Position.X - 1, character.Position.Y))
                        {
                            character.Position = new Position(character.Position.X - 1, character.Position.Y);
                        }
                        break;

                    case Direction.Up:
                        if (CheckNextCell(character.Position.X, character.Position.Y - 1))
                        {
                            character.Position = new Position(character.Position.X, character.Position.Y - 1);
                        }

                        break;

                    default:
                        break;

                }
                checkBeanEaten();
                //checkGost 

            }


        }
        public bool CheckNextCell (int x, int y)
        {

            if (y > 0 && y < 31 && x > 0 && x < 28)
            {
                if (map[y, x] == 0) return false;
                else return true;
            }
            else return false; 
            

           
        }

        public void checkBeanEaten()
        {

            int xPos = pacManCharacter.Position.X;
            int yPos = pacManCharacter.Position.Y;
            if (map[yPos, xPos ] == 1)
            {
                map[yPos, xPos] = 10;
                nbBeans--;
                score++;
            }
        }

        public Position getNextPosition(Character character, Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                    return new Position(character.Position.X, character.Position.Y + 1);

                case Direction.Right:
                    return new Position(character.Position.X + 1, character.Position.Y);

                case Direction.Left:
                    return new Position(character.Position.X - 1, character.Position.Y);

                case Direction.Up:
                    return new Position(character.Position.X, character.Position.Y - 1);


                default:
                    return null;
            }
        }
        public void moveGosts()
        {
            foreach (GostCharacter gostCharacter in listGostCharacters)
            {
                Position nextPosition = getNextPosition(gostCharacter, gostCharacter.Direction);
                bool forward = CheckNextCell(nextPosition.X, nextPosition.Y);

                if ((gostCharacter.Position.X == 13 || gostCharacter.Position.X == 14) && gostCharacter.Position.Y == 13)
                {
                    gostCharacter.InHouse = !gostCharacter.InHouse;
                    forward = true;
                    gostCharacter.Direction = Direction.Up; 
                  
                }

                if (forward)
                { 
                    move(gostCharacter);
                }
                else
                {
                    
                    bool directionAllowed = false;

                    while (!directionAllowed)
                    {
                        gostCharacter.Direction = gostCharacter.randomDirection();
                        nextPosition = getNextPosition(gostCharacter, gostCharacter.Direction); 
                        directionAllowed = CheckNextCell(nextPosition.X, nextPosition.Y);
                        
                        if ((nextPosition.X == 13|| nextPosition.X == 14) && nextPosition.Y == 13)
                        {
                            if (gostCharacter.Scared)
                            {
                                directionAllowed = true;  
                            } else
                            {
                                directionAllowed = false;

                            }
                        }


                    }
                    gostCharacter.Direction = gostCharacter.Direction;
                    move(gostCharacter);

                }
            }
           
        }

        public void PacManCharacterDies()
        {
           switch(animatedPacMan.Texture.Name)
            {
                case "":
                    var texture = Content.Load<Texture2D>("Mort0");
                    texture.Name = "Mort0";
                    animatedPacMan.Texture = texture;
                    break;
                case "Mort0":
                    texture = Content.Load<Texture2D>("Mort1");
                    texture.Name = "Mort1";
                    animatedPacMan.Texture = texture;
                    break;
                case "Mort1":
                    texture = Content.Load<Texture2D>("Mort2");
                    texture.Name = "Mort2";
                    animatedPacMan.Texture = texture;
                    break;
                case "Mort2":
                    texture = Content.Load<Texture2D>("Mort3");
                    texture.Name = "Mort3";
                    animatedPacMan.Texture = texture;
                    //partie.over(); 
                    break;
                default:
                    break; 


            }
        }


       
    }

    

}
