using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_Hustle_Winter_Edition
{
    class TileMap
    {
        int m_TileSize;
        List<Texture2D> m_Tiles = new List<Texture2D>();

        public TileMap(int tileSize)
        {
            m_TileSize = tileSize;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D[] tileTextures)
        {
            for (int s = 0; s < length; s++)
            {

            }
            spriteBatch.Draw();
        }
    }
}
