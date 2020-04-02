using System;
using System.Linq;
using System.Collections.Generic;

namespace RogueLike
{
    public class Level
    {
        int Rows { get; set; }
        int Cols { get; set; }
        private int NextLevel { get; set; }
        int[] playerPosition = new int[2];
        char[,] map;
        public Level() //no parameters? no problem
        {
            Rows = 8;
            Cols = 15;
            NextLevel = 1;
            playerPosition[0] = 1;
            playerPosition[1] = 1;
            map = new char[Rows, Cols];
            FillMap(Symbols.floor, Symbols.wall);
        }

        public Level(int rowsCount, int colsCount, int whichLevelAfterThis, int[] startingPlayerPos) //parameters? no problem
        {
            Rows = rowsCount;
            Cols = colsCount;
            NextLevel = whichLevelAfterThis;
            playerPosition = startingPlayerPos;
            map = new char[Rows, Cols];
            FillMap(Symbols.floor, Symbols.wall);
            PutSymbol(playerPosition, Symbols.playerSign);
        }

        void FillMap(char symbolTile, char symbolWall)
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    if ((col > 0 && col < Cols - 1) && (row > 0 && row < Rows - 1))
                    {
                        map[row, col] = symbolTile;
                    }
                    else
                    {
                        map[row, col] = symbolWall;
                    }
                }
            }
        }

        public void PutSymbol(int row, int col, char symbol)
        {
            map[outOfIndexProtection(row,0), outOfIndexProtection(col, 1)] = symbol;
        }
        public void PutSymbol(int[] coordinates, char symbol)
        {
            map[coordinates[0], coordinates[1]] = symbol;
        }

        public void AddVerticalWallOfSymbols(int row, int col, int size, char symbol)
        {
            for (int counter = 0; counter < size; counter++)
            {
                map[outOfIndexProtection(counter + row, 0), col] = symbol;
            }

        }

        public void AddHorizontalWallOfSymbols(int row, int col, int size, char symbol)
        {
            for (int counter = 0; counter < size; counter++)
            {
                map[row, outOfIndexProtection(counter + col, 1)] = symbol;
            }

        }
        int outOfIndexProtection(int check, int dimension)
        {
            if (check < 0)
            {
                check = 0;
            }
            else if (check >= map.GetLength(dimension))
            {
                check = dimension - 1;
            }
            return check;
        }

        public void ShowMap()
        {
            string[] mapInStringArr = new string[biggestDimensionSize()];
            string mapInString = "";
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    mapInStringArr[x] += map[x, y];
                }
                mapInString += mapInStringArr[x] + "\n";
            }
            Console.Write(mapInString);
        }

        int biggestDimensionSize()
        {
            int[] arrayOfSizes = new int[map.Rank];
            return map.GetLength(arrayOfSizes.Max());
        }
    }

}