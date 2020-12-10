using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Puzzle_Hustle_Winter_Edition
{
    //how to draw textures from other classes https://community.monogame.net/t/drawing-from-a-new-class-is-not-working/11878/5
    class ScenesContent : DrawableGameComponent
    {


        //creating texture2D variable that will be used to load and draw the images later on
        Texture2D m_BackgroundImage;
        Texture2D m_Background2Image;
        Texture2D m_Background3Image;
        Texture2D m_Background4Image;
        Texture2D m_Background5Image;
        Texture2D m_PlayButtonImage;
        Texture2D m_MapButtonImage;
        Texture2D m_LockedImage;

        //MapLevel m_MapLevel1;
        //MapLevel m_MapLevel2;
        //MapLevel m_MapLevel3;
        //MapLevel m_MapLevel4;
        //MapLevel m_MapLevel5;

        //creating an array of map levels https://stackoverflow.com/questions/3301678/how-to-declare-an-array-of-objects-in-c-sharp
        MapLevel[] m_Levels = new MapLevel[5];

        //creating a variable of the sceneChanger class in order to be able to use it in this class
        SceneChanger m_SceneChanger = new SceneChanger();

        SpriteBatch m_SpriteBatch;
        SpriteFont m_Font;

        Vector2 m_PlayButtonPosition;

        // storing the previous mouse state to make sure that the button was released before, and pressed now
        private MouseState oldState;

        public ScenesContent(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            m_Font = Game.Content.Load<SpriteFont>("Font");

            //loading the textures before drawing them
            m_BackgroundImage = Game.Content.Load<Texture2D>("art/Background1");
            m_Background2Image = Game.Content.Load<Texture2D>("art/Background2");
            m_Background3Image = Game.Content.Load<Texture2D>("art/Background3");
            m_Background4Image = Game.Content.Load<Texture2D>("art/Background4");
            m_Background5Image = Game.Content.Load<Texture2D>("art/Background5");
            m_PlayButtonImage = Game.Content.Load<Texture2D>("art/PlayButton");
            m_MapButtonImage = Game.Content.Load<Texture2D>("art/Blue");
            m_LockedImage = Game.Content.Load<Texture2D>("art/LockInChains");

            m_PlayButtonPosition = new Vector2(600 - (m_PlayButtonImage.Width / 2), 350);

        // m_Level1 = new MapLevel(m_MapButtonImage, m_LockedImage, new Vector2(100,100), 1, false);
        //m_Level2 = new MapLevel(m_MapButtonImage, m_LockedImage, new Vector2(200,100), 2, true);
        //m_Level3 = new MapLevel(m_MapButtonImage, m_LockedImage, new Vector2(300,100), 3, true);
        //m_Level4 = new MapLevel(m_MapButtonImage, m_LockedImage, new Vector2(400,100), 4, true);
        //m_Level5 = new MapLevel(m_MapButtonImage, m_LockedImage, new Vector2(500,100), 5, true);

        //creating instances of MapLevel and calling their constuctors 
        m_Levels[0] = new MapLevel(m_MapButtonImage, m_LockedImage, new Vector2(100, 100), 1, false);
        m_Levels[1] = new MapLevel(m_MapButtonImage, m_LockedImage, new Vector2(200, 100), 2, true);
        m_Levels[2] = new MapLevel(m_MapButtonImage, m_LockedImage, new Vector2(300, 100), 3, true);
        m_Levels[3] = new MapLevel(m_MapButtonImage, m_LockedImage, new Vector2(400, 100), 4, true);
        m_Levels[4] = new MapLevel(m_MapButtonImage, m_LockedImage, new Vector2(500, 100), 5, true);
    }

        //drawing different textures and font in specific positions, depending on the currently active scene
        public override void Draw(GameTime gameTime)
        {
           
            m_SpriteBatch.Begin();
            //menu scene
            if (m_SceneChanger.currentScene == SceneChanger.Scenes.Menu)
            {
                m_SpriteBatch.Draw(m_BackgroundImage, new Vector2(0, 0));
                m_SpriteBatch.DrawString(m_Font, "Puzzle", new Vector2(200, 100), Color.DarkRed);
                m_SpriteBatch.DrawString(m_Font, "Hustle", new Vector2(400, 200), Color.DarkRed);
                m_SpriteBatch.Draw(m_PlayButtonImage, m_PlayButtonPosition);
            }
            //map scene
            else if (m_SceneChanger.currentScene == SceneChanger.Scenes.Map)
            {
                m_SpriteBatch.Draw(m_Background2Image, new Vector2(0, 0));

                //m_Level1.Draw(m_SpriteBatch, m_Font);
                //m_Level2.Draw(m_SpriteBatch, m_Font);
                //m_Level3.Draw(m_SpriteBatch, m_Font);
                //m_Level4.Draw(m_SpriteBatch, m_Font);
                //m_Level5.Draw(m_SpriteBatch, m_Font);

                //drawing all the map level buttons 
                for (int i = 0; i < m_Levels.Length; i++)
                {
                    m_Levels[i].Draw(m_SpriteBatch, m_Font);

                }

            }
            //game scene
            else if (m_SceneChanger.currentScene == SceneChanger.Scenes.Game)
            {
                m_SpriteBatch.Draw(m_Background3Image, new Vector2(0, 0));

                Vector2 levelTextPosition = new Vector2(100, 100);
                //convert int to string http://zetcode.com/csharp/inttostring/
                m_SpriteBatch.DrawString(m_Font, "Level: " + m_SceneChanger.currentLevel.ToString(), levelTextPosition, Color.White, 0, levelTextPosition, 0.3f, SpriteEffects.None, 1);
            }
            //result scene
            else if (m_SceneChanger.currentScene == SceneChanger.Scenes.Result)
            {
                m_SpriteBatch.Draw(m_Background5Image, new Vector2(0, 0));
            }

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
                //cheking which is the currently active scene (if it is menu)
                if(m_SceneChanger.currentScene == SceneChanger.Scenes.Menu)
                {
                    //checking if the mouse click's position is inside the button position
                    if(currentState.X >= m_PlayButtonPosition.X && currentState.X <= m_PlayButtonPosition.X+ m_PlayButtonImage.Width)
                    {
                        if (currentState.Y >= m_PlayButtonPosition.Y && currentState.Y <= m_PlayButtonPosition.Y + m_PlayButtonImage.Height)
                        {
                            //if all the conditions are met, go to a different scene (map)
                            m_SceneChanger.ChangeScene(SceneChanger.Scenes.Map);
                        }
                    }
                }
                //cheking which is the currently active scene (if it is map)
                else if (m_SceneChanger.currentScene == SceneChanger.Scenes.Map)
                {
                    //calling the update function on each level
                    for (int i = 0; i < m_Levels.Length; i++)
                    {
                        m_Levels[i].Update(currentState, m_SceneChanger);
                    }
                }

            }

            oldState = currentState; // this reassigns the old state so that it is ready for next time
        }
    }
}
