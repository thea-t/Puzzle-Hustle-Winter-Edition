using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace Puzzle_Hustle_Winter_Edition
{
    //how to draw textures from other classes https://community.monogame.net/t/drawing-from-a-new-class-is-not-working/11878/5
    class ScenesContent : DrawableGameComponent
    {
        #region Variables
        Texture2D m_BackgroundImage;
        Texture2D m_Background2Image;
        Texture2D m_Background3Image;
        Texture2D m_Background4Image;
        Texture2D m_Background5Image;
        Texture2D m_PlayButtonImage;
        Texture2D m_PauseButtonImage;
        Texture2D m_ActiveSoundButtonImage;
        Texture2D m_InactiveSoundButtonImage;
        Texture2D m_RepeatButtonImage;
        Texture2D m_MenuButtonImage;
        Texture2D m_ButtonImage;
        Texture2D m_ScorePannel;
        Texture2D m_Timer;
        Texture2D m_Snowflake;
        Texture2D m_PuzzleBackground;
        Texture2D m_MapButtonImage;
        Texture2D m_LockedImage;

        //arrays of textures, that will be used to load and draw the puzzle pieces for each level
        Texture2D[] m_Level1Textures = new Texture2D[9];
        Texture2D[] m_Level2Textures = new Texture2D[16];
        Texture2D[] m_Level3Textures = new Texture2D[25];
        Texture2D[] m_Level4Textures = new Texture2D[36];
        Texture2D[] m_Level5Textures = new Texture2D[49];

        //array of map levels 
        MapLevel[] m_Levels = new MapLevel[5];

        Vector2 m_PlayButtonPosition;
        Vector2 m_PauseButtonPosition;
        Vector2 m_SoundButtonPosition;
        Vector2 m_ResumeButtonPosition;
        Vector2 m_RestartButtonPosition;
        Vector2 m_QuitButtonPosition;
        Vector2 m_RepeatButtonPosition;
        Vector2 m_MenuButtonPosition;
        Vector2 m_ScorePosition;
        Vector2 m_ShuffleButtonPosition;

        SpriteBatch m_SpriteBatch;
        SpriteFont m_Font;
        Song m_PuzzleMusic;
        Song m_MenuMusic;

        SceneChanger m_SceneChanger = new SceneChanger();

        // storing the previous mouse state to make sure that the button was released before, and pressed now
        private MouseState oldState;
        #endregion

        //creating an empty constructor that will receive the main Game class in order to be able to run the Draw and Update functions from this class
        public ScenesContent(Game game) : base(game)
        {
        }


        protected override void LoadContent()
        {
            //storing this ScenesContent in SceneChanger so that other classes can access ScenesContent
            m_SceneChanger.scenesContent = this;
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);

            

            #region Loading the font, music and the background images


            //loading the textures before drawing them
            m_BackgroundImage = Game.Content.Load<Texture2D>("art/Background1");
            m_Background2Image = Game.Content.Load<Texture2D>("art/Background2");
            m_Background3Image = Game.Content.Load<Texture2D>("art/Background3");
            m_Background4Image = Game.Content.Load<Texture2D>("art/Background4");
            m_Background5Image = Game.Content.Load<Texture2D>("art/Background5");
            m_PlayButtonImage = Game.Content.Load<Texture2D>("art/PlayButton");
            m_RepeatButtonImage = Game.Content.Load<Texture2D>("art/RepeatButton");
            m_MenuButtonImage = Game.Content.Load<Texture2D>("art/MenuButton");
            m_PauseButtonImage = Game.Content.Load<Texture2D>("art/PauseButton");
            m_ActiveSoundButtonImage = Game.Content.Load<Texture2D>("art/ActiveSound");
            m_InactiveSoundButtonImage = Game.Content.Load<Texture2D>("art/InactiveSound");
            m_ButtonImage = Game.Content.Load<Texture2D>("art/Button");
            m_PuzzleBackground = Game.Content.Load<Texture2D>("art/puzzle-background");
            m_ScorePannel = Game.Content.Load<Texture2D>("art/Moves");
            m_Timer = Game.Content.Load<Texture2D>("art/Clock");
            m_Snowflake = Game.Content.Load<Texture2D>("art/Star");
            m_MapButtonImage = Game.Content.Load<Texture2D>("art/Blue");
            m_LockedImage = Game.Content.Load<Texture2D>("art/LockInChains");


            //loading the font
            //loading the music: http://rbwhitaker.wikidot.com/playing-background-music
            m_Font = Game.Content.Load<SpriteFont>("Font"); 
            m_PuzzleMusic = Game.Content.Load<Song>("Music"); 
            m_MenuMusic = Game.Content.Load<Song>("MenuMusic");  




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
            m_PauseButtonPosition = new Vector2(1030,20);
            m_SoundButtonPosition = new Vector2(1100,20);
            m_ResumeButtonPosition = new Vector2(480,250);
            m_RestartButtonPosition = new Vector2(480,400);
            m_QuitButtonPosition = new Vector2(480,550);
            m_RepeatButtonPosition = new Vector2(610, 500);
            m_MenuButtonPosition = new Vector2(430, 500);
            m_ScorePosition = new Vector2(980, 280);
            m_ShuffleButtonPosition = new Vector2(840, 600);

            //creating 5 MapLevels and giving them their parameters  
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

            //drawing background images, buttons and text, depending on the currently active scene
            //switch statements: https://stackskills.com/courses/590152/lectures/10608229
            //color: https://www.c-sharpcorner.com/UploadFile/iersoy/how-to-use-colors-in-xna/

            switch (m_SceneChanger.currentScene)
            {
                case SceneChanger.Scenes.Menu:

                    //draw textures and font 
                    m_SpriteBatch.Draw(m_BackgroundImage, new Vector2(0, 0));
                    m_SpriteBatch.DrawString(m_Font, "Puzzle", new Vector2(120, 100), new Color(230, 37, 53));
                    m_SpriteBatch.DrawString(m_Font, "Hustle", new Vector2(620, 100), new Color(230, 37, 53));
                    m_SpriteBatch.Draw(m_PlayButtonImage, m_PlayButtonPosition);
                    break;

                case SceneChanger.Scenes.Map:
                    
                    //draw the background image
                    m_SpriteBatch.Draw(m_Background2Image, new Vector2(0, 0));
                    
                    //change the texture of the sound button, depending on if the music is running or not
                    if (MediaPlayer.IsMuted == false)
                    {
                        m_SpriteBatch.Draw(m_ActiveSoundButtonImage, m_SoundButtonPosition);
                    }
                    else
                    {
                        m_SpriteBatch.Draw(m_InactiveSoundButtonImage, m_SoundButtonPosition);
                    }

                    //for each level, call the Draw function that was created in the MapLevel class 
                    for (int i = 0; i < m_Levels.Length; i++)
                    {
                        m_Levels[i].Draw(m_SpriteBatch, m_Font);

                    }
                    break;

                case SceneChanger.Scenes.Game:
                    //draw textures
                    m_SpriteBatch.Draw(m_Background3Image, new Vector2(0, 0));
                    m_SpriteBatch.Draw(m_ButtonImage, m_ShuffleButtonPosition);
                    m_SpriteBatch.Draw(m_PauseButtonImage, m_PauseButtonPosition);
                    m_SpriteBatch.Draw(m_ScorePannel, new Vector2(800, 160));
                    m_SpriteBatch.Draw(m_Timer, new Vector2(830, 470));
                    m_SpriteBatch.Draw(m_PuzzleBackground, new Vector2(100, 140));

                    //change the texture of the sound button, depending on if the music is running or not
                    if (MediaPlayer.IsMuted == false)
                    {
                        m_SpriteBatch.Draw(m_ActiveSoundButtonImage, m_SoundButtonPosition);
                    }
                    else
                    {
                        m_SpriteBatch.Draw(m_InactiveSoundButtonImage, m_SoundButtonPosition);
                    }

                    //draw font, give it a position and color
                    //convert int to string http://zetcode.com/csharp/inttostring/
                    m_SpriteBatch.DrawString(m_Font, "Level: " + m_SceneChanger.currentLevel.ToString(), new Vector2(200, 100), Color.White, 0, new Vector2(200, 100), 0.3f, SpriteEffects.None, 1);
                    m_SpriteBatch.DrawString(m_Font, "Shuffle", m_ShuffleButtonPosition + new Vector2(80, 90), Color.White, 0, new Vector2(100, 100), 0.3f, SpriteEffects.None, 1);
                    m_SpriteBatch.DrawString(m_Font, "moves", new Vector2(940, 330), Color.White, 0, new Vector2(100, 100), 0.3f, SpriteEffects.None, 1);
                    m_SpriteBatch.DrawString(m_Font, m_Levels[m_SceneChanger.currentLevel - 1].tileMap.scoreCalculator.movesCount.ToString(), m_ScorePosition, Color.White, 0, new Vector2(100, 100), 0.8f, SpriteEffects.None, 1);

                    //drawing the currently passed time
                    //leave only two decimal places after the dot: https://stackoverflow.com/questions/1291483/leave-only-two-decimal-places-after-the-dot
                    m_SpriteBatch.DrawString(m_Font, String.Format("{0:0.00}", m_Levels[m_SceneChanger.currentLevel - 1].tileMap.scoreCalculator.passedTime), new Vector2(980, 530), Color.White, 0, new Vector2(100, 100), 0.3f, SpriteEffects.None, 1);

                    //drawing the current level's tile map by calling its draw function
                    m_Levels[m_SceneChanger.currentLevel - 1].tileMap.Draw(m_SpriteBatch);
                    break;

                case SceneChanger.Scenes.Result:
                    //drawing textures and font
                    m_SpriteBatch.Draw(m_Background5Image, new Vector2(0, 0));
                    m_SpriteBatch.Draw(m_Snowflake, new Vector2(710, 120));
                    m_SpriteBatch.Draw(m_Snowflake, new Vector2(520, 100));
                    m_SpriteBatch.Draw(m_Snowflake, new Vector2(330, 120));
                    m_SpriteBatch.Draw(m_MenuButtonImage, m_MenuButtonPosition);
                    m_SpriteBatch.Draw(m_RepeatButtonImage, m_RepeatButtonPosition);
                    m_SpriteBatch.DrawString(m_Font, "score:  " + m_Levels[m_SceneChanger.currentLevel - 1].tileMap.scoreCalculator.score, new Vector2(500, 440), Color.White, 0, new Vector2(100, 100), 0.3f, SpriteEffects.None, 1);
                    m_SpriteBatch.DrawString(m_Font, "Level Complete!", new Vector2(390, 350), Color.White, 0, new Vector2(100, 100), 0.5f, SpriteEffects.None, 1);
                    break;

                case SceneChanger.Scenes.Paused:
                    //drawing textures and font
                    m_SpriteBatch.Draw(m_Background4Image, new Vector2(0, 0));
                    m_SpriteBatch.Draw(m_ButtonImage, m_ResumeButtonPosition);
                    m_SpriteBatch.Draw(m_ButtonImage, m_RestartButtonPosition);
                    m_SpriteBatch.Draw(m_ButtonImage, m_QuitButtonPosition);
                    m_SpriteBatch.DrawString(m_Font, "Game Paused..", new Vector2(390, 200), Color.White, 0, new Vector2(100, 100), 0.7f, SpriteEffects.None, 1);
                    m_SpriteBatch.DrawString(m_Font, "Restart", m_RestartButtonPosition + new Vector2 (75, 90), Color.White, 0, new Vector2(100, 100), 0.3f, SpriteEffects.None, 1);
                    m_SpriteBatch.DrawString(m_Font, "Resume", m_ResumeButtonPosition + new Vector2(84, 90), Color.White, 0, new Vector2(100, 100), 0.3f, SpriteEffects.None, 1);
                    m_SpriteBatch.DrawString(m_Font, "Quit", m_QuitButtonPosition + new Vector2(105, 90), Color.White, 0, new Vector2(100, 100), 0.3f, SpriteEffects.None, 1);

                    break;
            }

            m_SpriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //mouse states:  http://rbwhitaker.wikidot.com/mouse-input
            MouseState currentState = Mouse.GetState();

            if (currentState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                //cheking which is the currently active scene
                switch (m_SceneChanger.currentScene)
                {
                    
                    case SceneChanger.Scenes.Menu:
                        //if the current scene is menu and the play button was pressed, go to the Map scene, play its music and loop it
                        if (currentState.X >= m_PlayButtonPosition.X && currentState.X <= m_PlayButtonPosition.X + m_PlayButtonImage.Width)
                        {
                            if (currentState.Y >= m_PlayButtonPosition.Y && currentState.Y <= m_PlayButtonPosition.Y + m_PlayButtonImage.Height)
                            {
                                m_SceneChanger.ChangeScene(SceneChanger.Scenes.Map);
                                MapSceneMusic();

                                MediaPlayer.Play(m_MenuMusic);
                                MediaPlayer.IsRepeating = true;

                            }
                        }
                        break;
                   
                    case SceneChanger.Scenes.Map:
                        //if the current scene is map and the sound button was pressed, mute the music. If the music is already muted - play it
                        if (currentState.X >= m_SoundButtonPosition.X && currentState.X <= m_SoundButtonPosition.X + m_ActiveSoundButtonImage.Width)
                        {
                            if (currentState.Y >= m_SoundButtonPosition.Y && currentState.Y <= m_SoundButtonPosition.Y + m_ActiveSoundButtonImage.Height)
                            {

                                if (MediaPlayer.IsMuted == true)
                                {
                                    MediaPlayer.IsMuted = false;
                                }
                                else
                                {
                                    MediaPlayer.IsMuted = true;
                                }

                            }
                        }
                        //calling the Update function for each Map level
                        for (int i = 0; i < m_Levels.Length; i++)
                        {
                            m_Levels[i].Update(currentState, m_SceneChanger);
                        }
                        break;
                    case SceneChanger.Scenes.Game:
                        //if the current scene is game and the sound button was pressed, mute the music. If the music is already muted - play it
                        if (currentState.X >= m_SoundButtonPosition.X && currentState.X <= m_SoundButtonPosition.X + m_ActiveSoundButtonImage.Width)
                        {
                            if (currentState.Y >= m_SoundButtonPosition.Y && currentState.Y <= m_SoundButtonPosition.Y + m_ActiveSoundButtonImage.Height)
                            {

                                if (MediaPlayer.IsMuted == true)
                                {
                                    MediaPlayer.IsMuted = false;
                                }
                                else
                                {
                                    MediaPlayer.IsMuted = true;
                                }

                            }
                        }
                        //checking if the shuffle button was pressed
                        if (currentState.X >= m_ShuffleButtonPosition.X && currentState.X <= m_ShuffleButtonPosition.X + m_ButtonImage.Width)
                        {
                            if (currentState.Y >= m_ShuffleButtonPosition.Y && currentState.Y <= m_ShuffleButtonPosition.Y + m_ButtonImage.Height)
                            {
                                //shuffling the tiles in the current level's TileMap
                                m_Levels[m_SceneChanger.currentLevel - 1].tileMap.ShuffleTiles();

                            }
                        }
                        // if the pause button is pressed, go to the Pause scene
                        if (currentState.X >= m_PauseButtonPosition.X && currentState.X <= m_PauseButtonPosition.X + m_PauseButtonImage.Width)
                        {
                            if (currentState.Y >= m_PauseButtonPosition.Y && currentState.Y <= m_PauseButtonPosition.Y + m_PauseButtonImage.Height)
                            {
                                m_SceneChanger.ChangeScene(SceneChanger.Scenes.Paused);
                            }
                        }
                        break;

                    case SceneChanger.Scenes.Result:
                        //if the current scene is result and the menu button was pressed, go to the Map Scene and play the music for that scene
                        if (currentState.X >= m_MenuButtonPosition.X && currentState.X <= m_MenuButtonPosition.X + m_MenuButtonImage.Width)
                        {
                            if (currentState.Y >= m_MenuButtonPosition.Y && currentState.Y <= m_MenuButtonPosition.Y + m_MenuButtonImage.Height)
                            {

                                m_SceneChanger.ChangeScene(SceneChanger.Scenes.Map);
                                MapSceneMusic();
                            }
                        }
                        //if the repeat button was pressed, go to the Game scene and call the RestartLevel function for the current level
                        if (currentState.X >= m_RepeatButtonPosition.X && currentState.X <= m_RepeatButtonPosition.X + m_RepeatButtonImage.Width)
                        {
                            if (currentState.Y >= m_RepeatButtonPosition.Y && currentState.Y <= m_RepeatButtonPosition.Y + m_RepeatButtonImage.Height)
                            {

                                m_SceneChanger.ChangeScene(SceneChanger.Scenes.Game);
                                m_Levels[m_SceneChanger.currentLevel - 1].RestartLevel();

                            }
                        }
                        break;

                    case SceneChanger.Scenes.Paused:
                        //if the current scene is Pause and restart button was pressed, go to the Game scene and call the RestartLevel function for the current level
                        if (currentState.X >= m_RestartButtonPosition.X && currentState.X <= m_RestartButtonPosition.X + m_ButtonImage.Width)
                        {
                            if (currentState.Y >= m_RestartButtonPosition.Y && currentState.Y <= m_RestartButtonPosition.Y + m_ButtonImage.Height)
                            {

                                m_SceneChanger.ChangeScene(SceneChanger.Scenes.Game);
                                m_Levels[m_SceneChanger.currentLevel - 1].RestartLevel();

                            }
                        }
                        //if the resume button was pressed, go back to the Game scene
                        if (currentState.X >= m_ResumeButtonPosition.X && currentState.X <= m_ResumeButtonPosition.X + m_ButtonImage.Width)
                        {
                            if (currentState.Y >= m_ResumeButtonPosition.Y && currentState.Y <= m_ResumeButtonPosition.Y + m_ButtonImage.Height)
                            {

                                m_SceneChanger.ChangeScene(SceneChanger.Scenes.Game);

                            }
                        }
                        //if the quit button was pressed, go to the Map Scene and play the music for that scene
                        if (currentState.X >= m_QuitButtonPosition.X && currentState.X <= m_QuitButtonPosition.X + m_ButtonImage.Width)
                        {
                            if (currentState.Y >= m_QuitButtonPosition.Y && currentState.Y <= m_QuitButtonPosition.Y + m_ButtonImage.Height)
                            {

                                m_SceneChanger.ChangeScene(SceneChanger.Scenes.Map);
                                MapSceneMusic();
                            }
                        }
                        break;
                }
            }
            // //if the current scene is the Game scene, call the Update fuctions that were created in the TileMap and the ScoreCalculator classes, for the current level
            if (m_SceneChanger.currentScene == SceneChanger.Scenes.Game)
            {

                m_Levels[m_SceneChanger.currentLevel - 1].tileMap.Update(currentState, oldState);
                m_Levels[m_SceneChanger.currentLevel - 1].tileMap.scoreCalculator.Update(gameTime);



            }

            oldState = currentState; // this reassigns the old state so that it is ready for next time

            
        }

        //unlocks the next level by calling the UnlockLevel function from the MapLevel class
        //the ScoreCalculator class is calling this function when the score is being displayed
        public void UnlockNextLevel()
        {
            m_Levels[m_SceneChanger.currentLevel].UnlockLevel();
        }
        
        //plays and loops the music from Game scene
        public void PuzzleBackgroundMusic()
        {
            MediaPlayer.Play(m_PuzzleMusic);
            MediaPlayer.IsRepeating = true;
        }

        //plays and loops the music from Map scene
        private void MapSceneMusic()
        {
            MediaPlayer.Play(m_MenuMusic);
            MediaPlayer.IsRepeating = true;
        }

       


    }
}
