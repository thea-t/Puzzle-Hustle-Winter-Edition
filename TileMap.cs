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
        public ScoreCalculator scoreCalculator;



        float m_TileSize;
        int m_TileCount;
        int m_DistanceBetweenTiles = 4;
        int m_CurrentlyDraggedTileIndex;
        int m_CurrentlyReleasedTileIndex;

        Vector2 m_StartOffset = new Vector2(145, 190);
        Texture2D[] m_TileTextures;

        List<Tile> m_Tiles = new List<Tile>();
        List<Tile> m_OrderedTiles = new List<Tile>();


        public TileMap(int tileCount, Texture2D[] tileTextures)
        {

            scoreCalculator = new ScoreCalculator();

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
                    m_Tiles.Add(new Tile(new Vector2(m_StartOffset.X + (m_TileSize + m_DistanceBetweenTiles) * j, m_StartOffset.Y + (m_TileSize + m_DistanceBetweenTiles) * i)));
                    
                    
                    tileNumber++;

                }
            }
            // https://stackoverflow.com/questions/1952185/how-do-i-copy-items-from-list-to-list-without-foreach
            m_OrderedTiles = new List<Tile>(m_Tiles);


            ShuffleTiles();
        }


        public void Update(MouseState currentState, MouseState oldState)
        {
            if (oldState.LeftButton == ButtonState.Released && currentState.LeftButton == ButtonState.Pressed)
            {
                for (int i = 0; i < m_Tiles.Count; i++)
                {
                    if (currentState.Position.X > m_Tiles[i].position.X && currentState.Position.X < m_Tiles[i].position.X + m_TileSize)
                    {
                        if (currentState.Position.Y > m_Tiles[i].position.Y && currentState.Position.Y < m_Tiles[i].position.Y + m_TileSize)
                        {
                            m_CurrentlyDraggedTileIndex = i;
                        }
                    }
                }
            }
            else if (oldState.LeftButton == ButtonState.Pressed && currentState.LeftButton == ButtonState.Released)
            {
                for (int i = 0; i < m_Tiles.Count; i++)
                {
                    if (currentState.Position.X > m_Tiles[i].position.X && currentState.Position.X < m_Tiles[i].position.X + m_TileSize)
                    {
                        if (currentState.Position.Y > m_Tiles[i].position.Y && currentState.Position.Y < m_Tiles[i].position.Y + m_TileSize)
                        {
                            m_CurrentlyReleasedTileIndex = i;

                            if (m_CurrentlyReleasedTileIndex == m_TileCount * m_TileCount - 1)
                            {
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

        //How to shuffle lists: https://answers.unity.com/questions/486626/how-can-i-shuffle-alist.html
        public void ShuffleTiles()
        {
            Random rnd = new Random();
            for (int i = 0; i < m_Tiles.Count; i++)
            {
                Tile temp = m_Tiles[i];
                int randomIndex = rnd.Next(i, m_Tiles.Count);
                m_Tiles[i] = m_Tiles[randomIndex];
                m_Tiles[randomIndex] = temp;
            }
        }

        private void SwapTiles()
        {
            // How to swap elements in a list: https://stackoverflow.com/questions/11283028/how-do-i-swap-2-elements-in-a-list
            Tile empty;

            empty = m_Tiles[m_CurrentlyDraggedTileIndex];
            m_Tiles[m_CurrentlyDraggedTileIndex] = m_Tiles[m_CurrentlyReleasedTileIndex];
            m_Tiles[m_CurrentlyReleasedTileIndex] = empty;
            scoreCalculator.movesCount++;
            CheckTilePositions();


        }

        private void CheckTilePositions()
        {
            int correctCount = 0;
            for (int i = 0; i < m_Tiles.Count; i++)
            {
                if (m_OrderedTiles[i] == m_Tiles[i])
                {
                    correctCount++;
                };
            }
            Console.WriteLine(correctCount);
            if (correctCount == m_OrderedTiles.Count)
            {
                scoreCalculator.CheckScore();
            }
            //issue: identifying when the puzzle is solved. Solution: on top
            //// how to check if two lists have same items https://stackoverflow.com/questions/22173762/check-if-two-lists-are-equal
            //if (m_Tiles.Equals(m_OrderedTiles))
            //{
            //    scoreCalculator.CheckScore();
            //}
        }


    }
}
