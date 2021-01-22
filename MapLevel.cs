using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_Hustle_Winter_Edition
{
    class MapLevel
    {
        #region Variables
        public TileMap tileMap;

        Texture2D m_ButtonTexture;
        Texture2D m_LockedTexture;
        Texture2D[] m_PuzzleTextures;

        Vector2 m_Position;
        Vector2 m_Origin;
        
        int m_LevelNumber;

        //creating a bool that will be used to store the information of the status of each level(if it is locked or not)
        bool m_IsLocked;
        #endregion

        //creating a constructor with different parameters
        public MapLevel(Texture2D texture, Texture2D lockedTexture, Texture2D[] puzzleTextures, Vector2 position, int level, bool isLocked)
        {
            //assigning the received parameters from the constructor to variables
            m_ButtonTexture = texture;
            m_LockedTexture = lockedTexture;
            m_Position = position;
            m_LevelNumber = level;
            m_IsLocked = isLocked;
            m_PuzzleTextures = puzzleTextures;

            //calculating the origin of the image in order to draw the text in the center of the level buttons 
            m_Origin = new Vector2(m_ButtonTexture.Width / 2, m_ButtonTexture.Height / 2);
        }

        //creating a public Update function which is called by the ScenesContent 
        public void Update(MouseState mouseState, SceneChanger sceneChanger)
        {
            //first checking if the level is unlocked 
            if (m_IsLocked == false)
            {
                //then detecting if the mouse click is withing the position of the button image
                if (mouseState.X >= m_Position.X && mouseState.X <= m_Position.X + m_ButtonTexture.Width)
                {
                    if (mouseState.Y >= m_Position.Y && mouseState.Y <= m_Position.Y + m_ButtonTexture.Height)
                    {
                        //change the current scene to the Game scene and play the background music
                        sceneChanger.ChangeScene(SceneChanger.Scenes.Game);
                        sceneChanger.scenesContent.PuzzleBackgroundMusic();

                        //save the value of the current level in the sceneChanger
                        sceneChanger.currentLevel = m_LevelNumber;

                        //if the level is 1, it will create a TileMap with 3x3 grid. If the level is 5, the grid will be 7x7
                        tileMap = new TileMap(m_LevelNumber + 2, m_PuzzleTextures, sceneChanger);
                    }
                }
            }
        }
        //creating a public Draw function which is called by the ScenesContent 
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            //draw the buttons in the position that was sent from ScenesContent
            spriteBatch.Draw(m_ButtonTexture, m_Position, Color.White);

            //drawing the locked image if the level is locked
            if (m_IsLocked)
            {
                spriteBatch.Draw(m_LockedTexture, m_Position, Color.White);
            }

            //convert int to string http://zetcode.com/csharp/inttostring/
            //draw the level's number in the center of the level's button
            spriteBatch.DrawString(font, m_LevelNumber.ToString(), new Vector2(m_Position.X + 60, m_Position.Y + 60), Color.White, 0, m_Origin, 0.5f, SpriteEffects.None, 1);
        }

        //function that is being called in ScenesContent in order to restart the level when the restart button is pressed
        public void RestartLevel()
        {
            //on restart, shuffle the tiles and also restart the timer and the moves counter
            tileMap.ShuffleTiles();
            tileMap.scoreCalculator.movesCount = 0;
            tileMap.scoreCalculator.passedTime = 0;

        }

        //function that unlocks the levels when its called in ScenesContent
        public void UnlockLevel()
        {
            m_IsLocked = false;
        }
    }
}
