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
        #region Variables
        public ScoreCalculator scoreCalculator;
        SceneChanger m_SceneChanger;

        float m_TileSize;
        int m_TileCount;
        int m_DistanceBetweenTiles = 4;
        int m_CurrentlyDraggedTileIndex;
        int m_CurrentlyReleasedTileIndex;

        Vector2 m_StartOffset = new Vector2(145, 190);
        Texture2D[] m_TileTextures;

        List<Tile> m_Tiles = new List<Tile>();
        List<Tile> m_OrderedTiles = new List<Tile>();
        #endregion

        public TileMap(int tileCount, Texture2D[] tileTextures, SceneChanger sceneChanger)
        {
            //creating an instance of ScoreCalculator and assigning it to a variable
            scoreCalculator = new ScoreCalculator();

            //assigning the received parameters from the constructor to variables
            m_SceneChanger = sceneChanger;
            m_TileCount = tileCount;
            m_TileTextures = tileTextures;

            //dividing 500 by the tile count in order to get the size of each tile for each level (500 is the resolution used in all puzzle images)
            m_TileSize = 500 / m_TileCount;

            //adding the positions of each tile to the list of tiles 
            int tileNumber = 0;

            for (int i = 0; i < m_TileCount; i++)
            {
                for (int j = 0; j < m_TileCount; j++)
                {
                    m_Tiles.Add(new Tile(new Vector2(m_StartOffset.X + (m_TileSize + m_DistanceBetweenTiles) * j, m_StartOffset.Y + (m_TileSize + m_DistanceBetweenTiles) * i)));
                  
                    tileNumber++;

                }
            }
            //assigning the positions of the main list of tiles(m_Tiles) to an another list of tiles(m_OrderedTiles) before shuffling the main list(m_Tiles).
        //the ordered tiles list is used later on in order to determine if the puzzle was solved https://stackoverflow.com/questions/1952185/how-do-i-copy-items-from-list-to-list-without-foreach
            m_OrderedTiles = new List<Tile>(m_Tiles);

            ShuffleTiles();
        }


        //creating a public Update function which is called by the ScenesContent 
        public void Update(MouseState currentState, MouseState oldState)
        {
            //while pressed, check if the position of the mouse is on top of a tile and if so, save its index
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

            //while released, check if the position of the mouse is on top of a tile and if so, save its index
            else if (oldState.LeftButton == ButtonState.Pressed && currentState.LeftButton == ButtonState.Released)
            {
                for (int i = 0; i < m_Tiles.Count; i++)
                {
                    if (currentState.Position.X > m_Tiles[i].position.X && currentState.Position.X < m_Tiles[i].position.X + m_TileSize)
                    {
                        if (currentState.Position.Y > m_Tiles[i].position.Y && currentState.Position.Y < m_Tiles[i].position.Y + m_TileSize)
                        {
                            m_CurrentlyReleasedTileIndex = i;

                            //the empty tile is always carrying the index of the last tile in the list (for example if the grid is 3x3 the empty tile's index is 8)
                            //if the index of the currently released tile = the empty tile's index and 
                            //if the distance between the currently dragged tile and the last tile < or = to the size of one tile, then swap the tiles
                            if (m_CurrentlyReleasedTileIndex == m_TileCount * m_TileCount - 1)
                            {
                                //check distance between two points: https://stackoverflow.com/questions/21870101/c-sharp-xna-calculate-distance-between-rectangles-rotation-friendly
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
        //creating a public Draw function which is called by the ScenesContent 
        public void Draw(SpriteBatch spriteBatch)
        {
            //drawing the tile textures in their positions
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

        //how to shuffle lists: https://answers.unity.com/questions/486626/how-can-i-shuffle-alist.html
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

        //how to swap elements in a list: https://stackoverflow.com/questions/11283028/how-do-i-swap-2-elements-in-a-list
        //after swapping 2 tiles, check if the puzzle is solved by calling the CheckTilePositions function
        private void SwapTiles()
        {
            Tile empty;

            empty = m_Tiles[m_CurrentlyDraggedTileIndex];
            m_Tiles[m_CurrentlyDraggedTileIndex] = m_Tiles[m_CurrentlyReleasedTileIndex];
            m_Tiles[m_CurrentlyReleasedTileIndex] = empty;
            scoreCalculator.movesCount++;
            CheckTilePositions();


        }

        //checking if the puzzle is solved
        private void CheckTilePositions()
        {
            //for each tile in the list that is in its correct position, increase the amount of correctCount
            int correctCount = 0;
            for (int i = 0; i < m_Tiles.Count; i++)
            {
                if (m_OrderedTiles[i] == m_Tiles[i])
                {
                    correctCount++;
                };
            }
            //if all the tiles are in their correct position, call the display function from the ScoreCalculator class and 
            //send the SceneChanger as a parameter in order to be able to access it from ScoreCalculator and change the current scene later on 
            if (correctCount == m_OrderedTiles.Count)
            {
                scoreCalculator.DisplayScore(m_SceneChanger);

            }

        }


    }
}
