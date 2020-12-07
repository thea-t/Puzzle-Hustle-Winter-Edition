using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_Hustle_Winter_Edition
{
    class MapLevel
    {
        //creating texture2D variable that will be used to load and draw the images later on
        Texture2D m_Texture;
        Texture2D m_LockedTexture;

        //creating vector2 that will be used to place the images in certain positions later on
        Vector2 m_Position;
        Vector2 m_Origin;
        
        //storing the index of the levels
        int m_LevelNumber;

        //creating a bool that will be used to store the information of the status of each level(if it is locked or not)
        bool m_IsLocked;

         //creating a constructor with different parameters
        public MapLevel(Texture2D texture, Texture2D lockedTexture, Vector2 position, int level, bool isLocked)
        {
            //assign variables to the parameters
            m_Texture = texture;
            m_LockedTexture = lockedTexture;
            m_Position = position;
            m_LevelNumber = level;
            m_IsLocked = isLocked;
            
            //calculating the origin of the image
            m_Origin = new Vector2(m_Texture.Width / 2, m_Texture.Height / 2);
        }

        //creating a public update function which is called by the ScenesContent 
        public void Update(MouseState mouseState, SceneChanger sceneChanger)
        {
            //detecting if the mouse click is withing the position of the map level image
            if (mouseState.X >= m_Position.X && mouseState.X <= m_Position.X + m_Texture.Width)
            {
                if (mouseState.Y >= m_Position.Y && mouseState.Y <= m_Position.Y + m_Texture.Height)
                {
                    //change the current scene to the game scene
                    sceneChanger.ChangeScene(SceneChanger.Scenes.Game);

                    //save the value of the current level in the sceneChanger
                    sceneChanger.currentLevel = m_LevelNumber;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {//drawing the button
            spriteBatch.Draw(m_Texture, m_Position, Color.White);
            //drawing the locked image if the level is locked
            if (m_IsLocked)
            {
                spriteBatch.Draw(m_LockedTexture, m_Position, Color.White);
            }

            //convert int to string http://zetcode.com/csharp/inttostring/
            //draw the levels
            spriteBatch.DrawString(font, m_LevelNumber.ToString(), new Vector2(m_Position.X + 60, m_Position.Y + 60), Color.White, 0, m_Origin, 0.5f, SpriteEffects.None, 1);
        }

        //unlocks the levels
        public void UnlockLevel()
        {
            m_IsLocked = false;
        }
    }
}
