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
        Texture2D m_Texture;
        Texture2D m_LockedTexture;
        Vector2 m_Position;
        Vector2 m_Origin;
        int m_LevelNumber;
        bool m_IsLocked;

        public MapLevel(Texture2D texture, Texture2D lockedTexture, Vector2 position, int level, bool isLocked)
        {
            m_Texture = texture;
            m_LockedTexture = lockedTexture;
            m_Position = position;
            m_LevelNumber = level;
            m_IsLocked = isLocked;

            m_Origin = new Vector2(m_Texture.Width / 2, m_Texture.Height / 2);
        }

        public void Update(MouseState mouseState, SceneChanger sceneChanger)
        {
            if (mouseState.X >= m_Position.X && mouseState.X <= m_Position.X + m_Texture.Width)
            {
                if (mouseState.Y >= m_Position.Y && mouseState.Y <= m_Position.Y + m_Texture.Height)
                {
                    sceneChanger.ChangeScene(SceneChanger.Scenes.Game);
                    sceneChanger.currentLevel = m_LevelNumber;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.Draw(m_Texture, m_Position, Color.White);

            if (m_IsLocked)
            {
                spriteBatch.Draw(m_LockedTexture, m_Position, Color.White);
            }

            //convert int to string http://zetcode.com/csharp/inttostring/
            spriteBatch.DrawString(font, m_LevelNumber.ToString(), new Vector2(m_Position.X + 60, m_Position.Y + 60), Color.White, 0, m_Origin, 0.5f, SpriteEffects.None, 1);
        }

        public void UnlockLevel()
        {
            m_IsLocked = false;
        }
    }
}
