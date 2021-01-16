using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Puzzle_Hustle_Winter_Edition
{
    //how to draw textures from other classes https://community.monogame.net/t/drawing-from-a-new-class-is-not-working/11878/5
    class ScenesContent : DrawableGameComponent
    {
        #region Variables
        //creating Texture2D variables that will be used to load and draw the images later on
        Texture2D m_BackgroundImage;
        Texture2D m_Background2Image;
        Texture2D m_Background3Image;
        Texture2D m_Background4Image;
        Texture2D m_Background5Image;
        Texture2D m_PlayButtonImage;
        Texture2D m_ButtonImage;
        Texture2D m_ScorePannel;
        Texture2D m_Timer;
        Texture2D m_PuzzleBackground;
        Texture2D m_MapButtonImage;
        Texture2D m_LockedImage;

        //arrays of textures, that will be used to load and draw the puzzle pieces for each level
        Texture2D[] m_Level1Textures = new Texture2D[9];
        Texture2D[] m_Level2Textures = new Texture2D[16];
        Texture2D[] m_Level3Textures = new Texture2D[25];
        Texture2D[] m_Level4Textures = new Texture2D[36];
        Texture2D[] m_Level5Textures = new Texture2D[49];

     
        //creating an array of map levels 
        MapLevel[] m_Levels = new MapLevel[5];

        //creating a new instance of the SceneChanger class
        SceneChanger m_SceneChanger = new SceneChanger();


        SpriteBatch m_SpriteBatch;
        SpriteFont m_Font;

        Vector2 m_PlayButtonPosition;
        Vector2 m_ScorePosition;
        Vector2 m_ShuffleButtonPosition = new Vector2(840, 600);

        // storing the previous mouse state to make sure that the button was released before, and pressed now
        private MouseState oldState;
        #endregion

        public ScenesContent(Game game) : base(game)
        {
        }


        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);

            #region Loading the font and the background images


            //loading the textures before drawing them
            m_BackgroundImage = Game.Content.Load<Texture2D>("art/Background1");
            m_Background2Image = Game.Content.Load<Texture2D>("art/Background2");
            m_Background3Image = Game.Content.Load<Texture2D>("art/Background3");
            m_Background4Image = Game.Content.Load<Texture2D>("art/Background4");
            m_Background5Image = Game.Content.Load<Texture2D>("art/Background5");
            m_PlayButtonImage = Game.Content.Load<Texture2D>("art/PlayButton");
            m_ButtonImage = Game.Content.Load<Texture2D>("art/Button");
            m_PuzzleBackground = Game.Content.Load<Texture2D>("art/puzzle-background");
            m_ScorePannel = Game.Content.Load<Texture2D>("art/Moves");
            m_Timer = Game.Content.Load<Texture2D>("art/Clock");
            m_MapButtonImage = Game.Content.Load<Texture2D>("art/Blue");
            m_LockedImage = Game.Content.Load<Texture2D>("art/LockInChains");


            //loading the font
            m_Font = Game.Content.Load<SpriteFont>("Font");
            #endregion

            #region Loading the tiles textures in each level
            //loading multiple textures into an array: https://stackoverflow.com/questions/10705563/xna-storing-lots-of-texture2d-in-an-array
            for (int i = 0; i < m_Level1Textures.Length; i++)
            {
                m_Level1Textures[i] = Game.Content.Load<Texture2D>("art/tiles/level1/" + (i + 1));
            }

            for (int i = 0; i < m_Level2Textures.Length; i++)
            {
                m_Level2Textures[i] = Game.Content.Load<Texture2D>("art/tiles/level2/" + (i + 1));
            }
            for (int i = 0; i < m_Level3Textures.Length; i++)
            {
                m_Level3Textures[i] = Game.Content.Load<Texture2D>("art/tiles/level3/" + (i + 1));
            }

            for (int i = 0; i < m_Level4Textures.Length; i++)
            {
                m_Level4Textures[i] = Game.Content.Load<Texture2D>("art/tiles/level4/" + (i + 1));
            }
            for (int i = 0; i < m_Level5Textures.Length; i++)
            {
                m_Level5Textures[i] = Game.Content.Load<Texture2D>("art/tiles/level5/" + (i + 1));
            }


            #endregion

            #region Loading button images
            m_PlayButtonPosition = new Vector2(600 - (m_PlayButtonImage.Width / 2), 350);
            m_ScorePosition = new Vector2(980, 280);

            //creating instances of MapLevel and calling their constuctors 
            m_Levels[0] = new MapLevel(m_MapButtonImage, m_LockedImage, m_Level1Textures, new Vector2(980, 270), 1, false);
            m_Levels[1] = new MapLevel(m_MapButtonImage, m_LockedImage, m_Level2Textures, new Vector2(870, 490), 2, true);
            m_Levels[2] = new MapLevel(m_MapButtonImage, m_LockedImage, m_Level3Textures, new Vector2(550, 600), 3, true);
            m_Levels[3] = new MapLevel(m_MapButtonImage, m_LockedImage, m_Level4Textures, new Vector2(250, 570), 4, true);
            m_Levels[4] = new MapLevel(m_MapButtonImage, m_LockedImage, m_Level5Textures, new Vector2(550, 170), 5, true);
            #endregion
        }



        public override void Draw(GameTime gameTime)
        {
            m_SpriteBatch.Begin();

            #region Drawing elements, depending on the currently active scene
            //switch statements: https://stackskills.com/courses/590152/lectures/10608229

            switch (m_SceneChanger.currentScene)
            {
                //if the currently active scene is the Menu scene, then draw the background images, buttons and text
                //color: https://www.c-sharpcorner.com/UploadFile/iersoy/how-to-use-colors-in-xna/
                case SceneChanger.Scenes.Menu:
                    m_SpriteBatch.Draw(m_BackgroundImage, new Vector2(0, 0));
                    m_SpriteBatch.DrawString(m_Font, "Puzzle", new Vector2(120, 100), new Color(230, 37, 53));
                    m_SpriteBatch.DrawString(m_Font, "Hustle", new Vector2(620, 100), new Color(230, 37, 53));
                    m_SpriteBatch.Draw(m_PlayButtonImage, m_PlayButtonPosition);
                    break;

                //if the currently active scene is the Map scene, then draw the background and all the level buttons 
                case SceneChanger.Scenes.Map:
                    m_SpriteBatch.Draw(m_Background2Image, new Vector2(0, 0));

                    for (int i = 0; i < m_Levels.Length; i++)
                    {
                        m_Levels[i].Draw(m_SpriteBatch, m_Font);

                    }
                    break;

                //if the currently active scene is the Game scene, then draw the background and check the curent level 
                case SceneChanger.Scenes.Game:
                    m_SpriteBatch.Draw(m_Background3Image, new Vector2(0, 0));
                    m_SpriteBatch.Draw(m_ButtonImage, m_ShuffleButtonPosition);
                    m_SpriteBatch.Draw(m_ScorePannel, new Vector2(800, 160));
                    m_SpriteBatch.Draw(m_Timer, new Vector2(830, 470));
                    m_SpriteBatch.Draw(m_PuzzleBackground, new Vector2(100, 140));

                //convert int to string http://zetcode.com/csharp/inttostring/
                    m_SpriteBatch.DrawString(m_Font, "Level: " + m_SceneChanger.currentLevel.ToString(), new Vector2(200, 100), Color.White, 0, new Vector2(200, 100), 0.3f, SpriteEffects.None, 1);
                    m_SpriteBatch.DrawString(m_Font, "Shuffle", new Vector2(920, 690), Color.White, 0, new Vector2(100, 100), 0.3f, SpriteEffects.None, 1);
                    m_SpriteBatch.DrawString(m_Font, "moves", new Vector2(940, 330), Color.White, 0, new Vector2(100, 100), 0.3f, SpriteEffects.None, 1);
                    m_SpriteBatch.DrawString(m_Font, m_Levels[m_SceneChanger.currentLevel - 1].tileMap.scoreCalculator.movesCount.ToString() , m_ScorePosition, Color.White, 0, new Vector2(100, 100), 0.8f, SpriteEffects.None, 1);
                   
                    //Leave only two decimal places after the dot: https://stackoverflow.com/questions/1291483/leave-only-two-decimal-places-after-the-dot
                    m_SpriteBatch.DrawString(m_Font, String.Format("{0:0.00}", m_Levels[m_SceneChanger.currentLevel - 1].tileMap.scoreCalculator.passedTime), new Vector2(980, 530), Color.White, 0, new Vector2(100, 100), 0.3f, SpriteEffects.None, 1);


                   

                    //draw different textures, depending on the curent level
                    m_Levels[m_SceneChanger.currentLevel - 1].tileMap.Draw(m_SpriteBatch);
                    break;

                //if the currently active scene is the Result scene
                case SceneChanger.Scenes.Result:
                    m_SpriteBatch.Draw(m_Background5Image, new Vector2(0, 0));
                    break;
            }
            #endregion

            m_SpriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // getting the current state of the mouse http://rbwhitaker.wikidot.com/mouse-input
            MouseState currentState = Mouse.GetState();

            // checking if the button was pressed in the current state and if it was released in the old state
            if (currentState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                //cheking which is the currently active scene
                switch (m_SceneChanger.currentScene)
                {
                    case SceneChanger.Scenes.Menu:
                        if (currentState.X >= m_PlayButtonPosition.X && currentState.X <= m_PlayButtonPosition.X + m_PlayButtonImage.Width)
                        {
                            if (currentState.Y >= m_PlayButtonPosition.Y && currentState.Y <= m_PlayButtonPosition.Y + m_PlayButtonImage.Height)
                            {
                                //if all the conditions are met, go to a different scene (map)
                                m_SceneChanger.ChangeScene(SceneChanger.Scenes.Map);
                            }
                        }
                        break;
                    case SceneChanger.Scenes.Map:
                        //calling the update function on each level
                        for (int i = 0; i < m_Levels.Length; i++)
                        {
                            m_Levels[i].Update(currentState, m_SceneChanger);
                        }
                        break;
                    case SceneChanger.Scenes.Game:
                        //checking if the mouse click's position is inside the button position
                        if (currentState.X >= m_ShuffleButtonPosition.X && currentState.X <= m_ShuffleButtonPosition.X + m_ButtonImage.Width)
                        {
                            if (currentState.Y >= m_ShuffleButtonPosition.Y && currentState.Y <= m_ShuffleButtonPosition.Y + m_ButtonImage.Height)
                            {

                                m_Levels[m_SceneChanger.currentLevel - 1].tileMap.ShuffleTiles();

                            }
                        }
                        break;
                }
            }

            if (m_SceneChanger.currentScene == SceneChanger.Scenes.Game)
            {
                m_Levels[m_SceneChanger.currentLevel - 1].tileMap.Update(currentState, oldState);
                m_Levels[m_SceneChanger.currentLevel - 1].tileMap.scoreCalculator.Update(gameTime);
                
            }
                        
            oldState = currentState; // this reassigns the old state so that it is ready for next time
        }
    }
}
