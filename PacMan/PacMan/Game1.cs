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
        List<GhostCharacter> listGhostCharacters;
        PacManCharacter pacManCharacter;
        // Animated characters
        AnimatedPacMan animatedPacMan;
        List<AnimatedGhost> listAnimatedGhosts;
        //Animated objects
        AnimatedObject wall;
        AnimatedObject bean;
        AnimatedObject bigBean;

        bool restart =true; 
        int timer = 0;
        int counterAnimation = 0;
        int beginPower=0;
        int beginPause=0; 
        int score = 0;
        int nbBeans = 0;
        byte[,] map;

        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // We initialize our characters with their initial poisitions. 
            pacManCharacter = new PacManCharacter(new Position(13, 17), new Position(15, 17),  3, Direction.Right);
            listGhostCharacters = new List<GhostCharacter>();
            for (int i = 0; i < 4; i++)
            {
                listGhostCharacters.Add(new GhostCharacter(new Position(12 + i, 14), new Position(12 + i, 14), Direction.Up));
            }

            // We initilize the map
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
            // We count the number of bean present on the map
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

      
        protected override void Initialize()
        {
            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //  changing the back buffer size changes the window size (when in windowed mode)
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 660;
            graphics.ApplyChanges();

            // Loding wall, bean and bigBean objects
            wall = new AnimatedObject(Content.Load<Texture2D>("mur"), new Vector2(0f, 0f), new Vector2(20f, 20f));
            bean = new AnimatedObject(Content.Load<Texture2D>("bean"), new Vector2(0f, 0f), new Vector2(20f, 20f));
            bigBean = new AnimatedObject(Content.Load<Texture2D>("gros_bean"), new Vector2(0f, 0f), new Vector2(20f, 20f));

            //Loding animated characters 
            animatedPacMan = new AnimatedPacMan(Content.Load<Texture2D>("pacmanDroite0"), new Vector2(0f, 0f), new Vector2(20f, 20f), pacManCharacter);
            listAnimatedGhosts = new List<AnimatedGhost>();
            for (int i = 0; i < 4; i++)
            {
                listAnimatedGhosts.Add(new AnimatedGhost(Content.Load<Texture2D>("fantome" + i), new Vector2(0f, 0f), new Vector2(20f, 20f), listGhostCharacters.ElementAt(i)));
            }
        }

       
        protected override void UnloadContent()
        {
        }

       
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Update game timer
            ++timer;
            if (!pacManCharacter.Dead) // If the pacMan is not dead, we can start the game. 
            {
                if (timer - beginPause > 200) // If the pause started at beginPause is over 200 ms, beginPause is unset.
                {
                    beginPause = -1;

                }
                if (beginPause == -1) //If beginPause is unset (game not paused)
                {
                    restart = false; // Stop displaying the "ready" image

                    if (timer % 5 == 0) // Modulo 5 to slow down the display 
                    {
                        ++counterAnimation;
                        if (pacManCharacter.Moving) // If pacMan is allowed to move, we update it's position and the ghosts'
                        {
                            movePacMan();
                            moveghosts();
                            animatePacMan(); // Allows PacMan to be animated 
                            getKeyboardInput();
                        }
                        else
                        {
                            //If pacman isn't allowed to move, that means it has been eaten and is now dead 
                            PacManCharacterDies(); 
                        }
                    }

                    base.Update(gameTime);
                    if (nbBeans == 0) //if there are no more beans on the map, the game stops
                    {
                        Exit();
                    }
                }
            }
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // Drawing wall and beans 
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
                    else if (map[x, y] == 3)
                    {
                        int xpos, ypos;
                        xpos = x * 20;
                        ypos = y * 20;
                        Vector2 pos = new Vector2(ypos, xpos);
                        spriteBatch.Draw(bigBean.Texture, pos, Color.White);
                    }
                }
            }
            // Drawing ghost fence
            spriteBatch.Draw(Content.Load<Texture2D>("barriereFantome"), new Vector2(13 * 20, 12 * 20), Color.White);

            //Drawing animated objects according to the associated character
            spriteBatch.Draw(animatedPacMan.Texture, new Vector2(pacManCharacter.getPostion().X * 20, pacManCharacter.getPostion().Y * 20), Color.White);
            for (int i = 0; i < 4; i++)
            {
                if (pacManCharacter.Moving) // If the pacMan is dead, ghosts disapear before the pacman reapears 
                {
                    spriteBatch.Draw(listAnimatedGhosts.ElementAt(i).Texture, new Vector2(listGhostCharacters.ElementAt(i).getPostion().X * 20, listGhostCharacters.ElementAt(i).getPostion().Y * 20), Color.White);
                }

            }
            //If we restart the game, the "ready !" image displays
            if (restart)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("ready"), new Vector2(5 * 20, 5 * 20), Color.White);
            }
            //If we restart the game, the "game over" and the "press enter" image display

            if (pacManCharacter.Dead)
            {
                //"game over"
                spriteBatch.Draw(Content.Load<Texture2D>("game_over"), new Vector2(7 * 20, 5 * 20), Color.White);
                //"press enter"
                spriteBatch.Draw(Content.Load<Texture2D>("enter"), new Vector2(3 * 20, 7 * 20), Color.White);
                KeyboardState keyboard = Keyboard.GetState();

                if (keyboard.IsKeyDown(Keys.Enter))
                {
                    //We reinitialize the game on the enter key pressed
                    pacManCharacter.Dead = false;
                    pacManCharacter.Moving = true;
                    pacManCharacter.Direction = Direction.Right;
                    pacManCharacter.Position = pacManCharacter.InitialPosition; 
                    pacManCharacter.Life = 3;
                    animatedPacMan = new AnimatedPacMan(Content.Load<Texture2D>("pacmanDroite0"), new Vector2(0f, 0f), new Vector2(20f, 20f), pacManCharacter);

                    foreach (GhostCharacter ghostCharacter in listGhostCharacters )
                    {
                        ghostCharacter.Position = ghostCharacter.InitialPosition; 
                    }

                    restart = true;
                    beginPause = timer; 
                }
            }
            
            base.Draw(gameTime);
            spriteBatch.End();
        }


        public void animatePacMan() // Allows pacMan to open and close his mouth, and face the right way
        {
            switch (pacManCharacter.Direction)
            {
                case Direction.Down:
                    if (counterAnimation % 2 == 0) animatedPacMan.Texture = Content.Load<Texture2D>("pacmanBas0");
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

        


        public void getKeyboardInput() //reads keybord and set new direction accordingly if possible
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Right))
            {
                if (CheckNextCellForPacMan(pacManCharacter.Position.X + 1, pacManCharacter.Position.Y)) pacManCharacter.Direction = Direction.Right;
            }
            else if (keyboard.IsKeyDown(Keys.Left))
            {
                if (CheckNextCellForPacMan(pacManCharacter.Position.X - 1, pacManCharacter.Position.Y)) pacManCharacter.Direction = Direction.Left;
            }
            else if (keyboard.IsKeyDown(Keys.Up))
            {
                if (CheckNextCellForPacMan(pacManCharacter.Position.X, pacManCharacter.Position.Y - 1)) pacManCharacter.Direction = Direction.Up;
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                if (CheckNextCellForPacMan(pacManCharacter.Position.X, pacManCharacter.Position.Y + 1)) pacManCharacter.Direction = Direction.Down;
            }
        }


        public void moveghost(Character character) // Moves ghosts according to their direction.
        {
            if (character.Moving)
            {
                switch (character.Direction)
                {
                    case Direction.Down:

                        if (CheckNextCellForghost(character.Position.X, character.Position.Y + 1))
                        {
                            character.Position = new Position(character.Position.X, character.Position.Y + 1);
                        }

                        break;

                    case Direction.Right:
                        if (CheckNextCellForghost(character.Position.X + 1, character.Position.Y))
                        {
                            character.Position = new Position(character.Position.X + 1, character.Position.Y);
                        }

                        break;

                    case Direction.Left:
                        if (CheckNextCellForghost(character.Position.X - 1, character.Position.Y))
                        {
                            character.Position = new Position(character.Position.X - 1, character.Position.Y);
                        }
                        break;

                    case Direction.Up:
                        if (CheckNextCellForghost(character.Position.X, character.Position.Y - 1))
                        {
                            character.Position = new Position(character.Position.X, character.Position.Y - 1);
                        }

                        break;

                    default:
                        break;

                }
                detectEnemy(pacManCharacter, (GhostCharacter)character); 
            }
        }


        public void movePacMan() //moves pacMan according to it's direction
        {
            if (pacManCharacter.Moving)
            {
                //teleports pacMan from one side of the map to the other
                if (pacManCharacter.Position.X == 26 && pacManCharacter.Position.Y == 14 && pacManCharacter.Direction ==Direction.Right)
                {
                    pacManCharacter.Position = new Position(1, 14);
                } else if (pacManCharacter.Position.X == 1 && pacManCharacter.Position.Y == 14 && pacManCharacter.Direction == Direction.Left)
                {
                    pacManCharacter.Position = new Position(26, 14);
                }
                else
                {
                    //moves pacMan according to it's direction   
                    switch (pacManCharacter.Direction)
                    {
                        case Direction.Down:

                            if (CheckNextCellForPacMan(pacManCharacter.Position.X, pacManCharacter.Position.Y + 1))
                                pacManCharacter.Position = new Position(pacManCharacter.Position.X, pacManCharacter.Position.Y + 1);
                            break;

                        case Direction.Right:
                            if (CheckNextCellForPacMan(pacManCharacter.Position.X + 1, pacManCharacter.Position.Y))
                                pacManCharacter.Position = new Position(pacManCharacter.Position.X + 1, pacManCharacter.Position.Y);
                            break;

                        case Direction.Left:
                            if (CheckNextCellForPacMan(pacManCharacter.Position.X - 1, pacManCharacter.Position.Y))
                                pacManCharacter.Position = new Position(pacManCharacter.Position.X - 1, pacManCharacter.Position.Y);
                            break;

                        case Direction.Up:
                            if (CheckNextCellForPacMan(pacManCharacter.Position.X, pacManCharacter.Position.Y - 1))
                                pacManCharacter.Position = new Position(pacManCharacter.Position.X, pacManCharacter.Position.Y - 1);
                            break;

                        default:
                            break;

                    }

                    checkBeanEaten(); 

                    if (beginPower != 0) // If time where power began is set, we check if pacMan should still be powerfull 
                    {
                        pacManPower(timer - beginPower);
                    }

                    foreach (GhostCharacter gostCharacter in listGhostCharacters) // Every time pacman moves, we detect if someone should die 
                    {
                        detectEnemy(pacManCharacter, gostCharacter);
                    } 
                }
            }
        }


        public void moveghosts() //moves ghosts 
        {
            foreach (GhostCharacter ghostCharacter in listGhostCharacters)
            {
                Position nextPosition = getNextPosition(ghostCharacter, ghostCharacter.Direction);
                bool forward = CheckNextCellForghost(nextPosition.X, nextPosition.Y);

                if ((ghostCharacter.Position.X == 13 || ghostCharacter.Position.X == 14) && ghostCharacter.Position.Y == 13) //cells corresponding to the gate
                {
                    ghostCharacter.InHouse = !ghostCharacter.InHouse;
                    forward = true;
                    ghostCharacter.Direction = Direction.Up;
                }

                if (forward)
                {
                    moveghost(ghostCharacter);
                }
                else
                {
                    bool directionAllowed = false;
                    while (!directionAllowed)
                    {
                        ghostCharacter.Direction = ghostCharacter.randomDirection();
                        nextPosition = getNextPosition(ghostCharacter, ghostCharacter.Direction);
                        directionAllowed = CheckNextCellForghost(nextPosition.X, nextPosition.Y);

                        if ((nextPosition.X == 13 || nextPosition.X == 14) && nextPosition.Y == 13)
                        {
                            if (ghostCharacter.Scared)
                            {
                                directionAllowed = true;
                            }
                            else
                            {
                                directionAllowed = false;
                            }
                        }
                    }
                    ghostCharacter.Direction = ghostCharacter.Direction;
                    moveghost(ghostCharacter);
                }
            }
        }

        public bool CheckNextCellForghost(int x, int y) // Checks if the next cell the ghost wants to go is allowed
        {
            if (y > 0 && y < 31 && x > 0 && x < 28)
            {
                if (map[y, x] == 0) return false;
                else return true;
            }
            else return false;
        }

        public bool CheckNextCellForPacMan(int x, int y) //Checks if the next cell the pacMan wants to go is allowed
        {
            if (y > 0 && y < 31 && x > 0 && x < 28)
            {
                if (map[y, x] == 0 || map[y, x] == 2) return false;
                else return true;
            }
            else return false;
        }

        public void checkBeanEaten() // Change the cell in the map when the bean is eaten 
        {

            int xPos = pacManCharacter.Position.X;
            int yPos = pacManCharacter.Position.Y;
            int content = map[yPos, xPos];
            if (content == 1)
            {
                map[yPos, xPos] = 10;
                nbBeans--;
                score = score+10; 
            }
            else if (content == 3)
            {
                map[yPos, xPos] = 10;
                score = score + 10;
                beginPower = timer; 
                pacManPower(0); 
            }
        }

        public void pacManPower(int time) // Animates the ghost so that they look scared. 
        {
            if (time == 0)
            {
                pacManCharacter.Power = true;
                foreach (GhostCharacter ghostCharacter in listGhostCharacters)
                {
                    ghostCharacter.Scared = true;
                }
            } else { 
                for (int i = 0; i < 4; i++)
                {
                    if (listAnimatedGhosts.ElementAt(i).GhostCharacter.Scared)
                    {
                        if (time < 401)
                        {
                            listAnimatedGhosts.ElementAt(i).Texture = Content.Load<Texture2D>("fantomePeur0");
                        }
                        else if (time > 400 && time <501)
                        {
                            
                            if (counterAnimation % 2 == 0) listAnimatedGhosts.ElementAt(i).Texture = Content.Load<Texture2D>("fantomePeur0");
                            else listAnimatedGhosts.ElementAt(i).Texture = Content.Load<Texture2D>("fantomePeur1");
                            
                        }
                        else if (time > 500)
                        {
                            listAnimatedGhosts.ElementAt(i).Texture = Content.Load<Texture2D>("fantome" + i);
                            listGhostCharacters.ElementAt(i).Scared = false;
                            pacManCharacter.Power = false;
                            beginPower = 0;
                        }
                    }
                    else
                    {
                        listAnimatedGhosts.ElementAt(i).Texture = Content.Load<Texture2D>("fantome" + i);
                        listGhostCharacters.ElementAt(i).Scared = false;
                    }
                }
            }
        }
        

        public Position getNextPosition(Character character, Direction direction) // returns next position according to direction 
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


       

        public void PacManCharacterDies() //animation of pacMan death + reinisializes for the restart 
        {
            switch (animatedPacMan.Texture.Name)
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
                    break;
                default:
                    if (pacManCharacter.Life == 0)
                    {
                        pacManCharacter.Dead = true;
                    }
                    else
                    {
                        pacManCharacter.Position = pacManCharacter.InitialPosition;

                        foreach (GhostCharacter ghostCharacter in listGhostCharacters)
                        {
                            ghostCharacter.Position = ghostCharacter.InitialPosition;
                        }
                        animatedPacMan.Texture = Content.Load<Texture2D>("pacmanDroite0");
                        pacManCharacter.Direction = Direction.Right;
                        pacManCharacter.Moving = true;
                        restart = true;
                        beginPause = timer;
                    }
                    break;
                }
            }

        public void ghostDies(GhostCharacter ghostCharacter) // Replaces the ghosts when they die. They aren't scared anymore
        {
            ghostCharacter.Position = ghostCharacter.InitialPosition;
            ghostCharacter.Scared = false; 
            ghostCharacter.InHouse = true; 
        }
        
        public void detectEnemy(PacManCharacter pacManCharcter, GhostCharacter ghostCharacter) //Detects collision betwen pacman and ghosts 
        {
            
            if (pacManCharcter.Position.X == ghostCharacter.Position.X && pacManCharcter.Position.Y == ghostCharacter.Position.Y )
            {
                if (ghostCharacter.Scared) // If the ghost is vulnerable, it dies and pacMan wins points.
                {
                    ghostDies(ghostCharacter);
                    score = score + 200; 
                }
                else // otherwise, pacMan dies. 
                {
                    if (pacManCharacter.Life > 0)
                    {
                        pacManCharacter.looseLife();


                        pacManCharacter.Moving = false;

                    }
                }
            }
        }
    }
}
