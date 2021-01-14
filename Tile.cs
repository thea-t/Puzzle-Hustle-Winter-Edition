using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Puzzle_Hustle_Winter_Edition
{
    class Tile
    {
        Texture2D m_TileTexture;
        Vector2 m_CorrectPosition;
        public Vector2 position;
        public bool isLastTile;

        public Tile(Texture2D texture, Vector2 pos)
        {
            m_TileTexture = texture;
            position = pos;
            m_CorrectPosition = pos;
        }
    }
}
