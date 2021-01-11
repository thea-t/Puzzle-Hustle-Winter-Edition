using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_Hustle_Winter_Edition
{
    class TileMap
    {
        int m_TileCount;
        float m_TileSize;
        Texture2D[] m_TileTextures;
        List<Tile> m_Tiles = new List<Tile>();
        Vector2 m_StartOffset = new Vector2(100, 200);
        int m_DistanceBetweenTiles = 4;

        int m_CurrentlyDraggedTileIndex;
        int m_CurrentlyReleasedTileIndex;
        public TileMap(int tileCount, Texture2D[] tileTextures)
        {
            m_TileCount = tileCount;
            m_TileTextures = tileTextures;

            //500 is the resolution used in all puzzle images
            m_TileSize = 500 / m_TileCount;

            //initializing the list with all the tiles so that their positions can be swapped later on
            int tileNumber = 0;


            for (int i = 0; i < m_TileCount; i++)
            {
                for (int j = 0; j < m_TileCount; j++)
                {
                    m_Tiles.Add(new Tile(m_TileTextures[tileNumber], new Vector2(m_StartOffset.X + (m_TileSize + m_DistanceBetweenTiles) * j, m_StartOffset.Y + (m_TileSize + m_DistanceBetweenTiles) * i)));
                    tileNumber++;
                }
            }
        }


        public void Update(MouseState currentState, MouseState oldState)
        {
            if(oldState.LeftButton == ButtonState.Released && currentState.LeftButton == ButtonState.Pressed)
            {
                for (int i = 0; i < m_Tiles.Count; i++)
                {
                    if(currentState.Position.X > m_Tiles[i].position.X && currentState.Position.X < m_Tiles[i].position.X + m_TileSize)
                    {
                        if (currentState.Position.Y > m_Tiles[i].position.Y && currentState.Position.Y < m_Tiles[i].position.Y + m_TileSize)
                        {
                            m_CurrentlyDraggedTileIndex = i;
                        }
                    }
                }
            }
            else if(oldState.LeftButton == ButtonState.Pressed && currentState.LeftButton == ButtonState.Released)
            {
                for (int i = 0; i < m_Tiles.Count; i++)
                {
                    if (currentState.Position.X > m_Tiles[i].position.X && currentState.Position.X < m_Tiles[i].position.X + m_TileSize)
                    {
                        if (currentState.Position.Y > m_Tiles[i].position.Y && currentState.Position.Y < m_Tiles[i].position.Y + m_TileSize)
                        {
                            m_CurrentlyReleasedTileIndex = i;
                            
                            // Check distance between two points: https://stackoverflow.com/questions/21870101/c-sharp-xna-calculate-distance-between-rectangles-rotation-friendly
                            if (Vector2.Distance(m_Tiles[m_CurrentlyDraggedTileIndex].position, m_Tiles[m_CurrentlyReleasedTileIndex].position) <= m_TileSize + m_DistanceBetweenTiles)
                            {
                                SwapTiles();
                            }
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int tileNumber = 0;

            for (int i = 0; i < m_TileCount; i++)
            {
                for (int j = 0; j < m_TileCount; j++)
                {
                    spriteBatch.Draw(m_TileTextures[tileNumber], m_Tiles[tileNumber].position, Color.White);
                    tileNumber++;
                }
            }
        }

        private void ShuffleTiles()
        {

        }

        private Tile[] GetNeighborTiles(Tile tile)
        {
            return null;
        }

        private void SwapTiles()
        {
            // How to swap elements in a list: https://stackoverflow.com/questions/11283028/how-do-i-swap-2-elements-in-a-list
            Tile empty;

            empty = m_Tiles[m_CurrentlyDraggedTileIndex];
            m_Tiles[m_CurrentlyDraggedTileIndex] = m_Tiles[m_CurrentlyReleasedTileIndex];
            m_Tiles[m_CurrentlyReleasedTileIndex] = empty;
        }
    }
}
