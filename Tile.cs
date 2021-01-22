using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Puzzle_Hustle_Winter_Edition
{
    //creating a tile class that acts as an individual tile
    //storing its position as a public variable so that it can be used my the TileMap class while creating the grid for each level, swapping the tiles, and shuffling their positions
    class Tile
    {
        public Vector2 position;

        public Tile(Vector2 pos)
        {
            position = pos;
        }


    }

}
