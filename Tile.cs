using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Puzzle_Hustle_Winter_Edition
{
    class Tile
    {
        Vector2 m_CorrectPosition;
        public Vector2 position;

        public Tile(Vector2 pos)
        {
            position = pos;
            m_CorrectPosition = pos;
        }
    }
}
