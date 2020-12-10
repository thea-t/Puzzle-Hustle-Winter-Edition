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
        //https://github.com/CartBlanche/MonoGame-Samples/blob/master/BookSourceCode/XNAGameDevelopmentbyExampleCode/RobotRampage/TileMap.cs

        private int m_Rows;
        private int m_Colums;
        private Texture2D m_PuzzleImage;



        private int[,] m_Tiles;
        private Texture2D[,] m_TileTextures;

        public TileMap(int rows, int columns, Texture2D puzzleImage)
        {
            m_Rows = rows;
            m_Colums = columns;
            m_PuzzleImage = puzzleImage;

            m_Tiles = new int[m_Rows, m_Colums];
            m_TileTextures = new Texture2D[m_Rows, m_Colums];

            Color[,] colors = TextureTo2DArray(m_PuzzleImage);

            for (int i = 0; i < colors; i++)
            {
                for (int j = 0; j < m_TileTextures.Length; j++)
                {
                    m_TileTextures[i, j].SetData<Color>(TextureTo2DArray(m_PuzzleImage));
                }
            }
        }


        //How to convert texture to 2D array: https://gamedev.stackexchange.com/questions/46775/xna-why-is-texture-getdata-one-dimensional
        Color[,] TextureTo2DArray(Texture2D texture)
        {
            Color[] colorsOne = new Color[texture.Width * texture.Height]; //The hard to read,1D array
            texture.GetData(colorsOne); //Get the colors and add them to the array

            Color[,] colorsTwo = new Color[texture.Width, texture.Height]; //The new, easy to read 2D array
            for (int x = 0; x < texture.Width; x++) //Convert!
                for (int y = 0; y < texture.Height; y++)
                    colorsTwo[x, y] = colorsOne[x + y * texture.Width];

            return colorsTwo; //Done!
        }

    }
}
