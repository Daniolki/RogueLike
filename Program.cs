using System;
using System.Linq;

namespace RogueLike
{
    class Program
    {

       static char playerSign = 'X'; //our character or other players - not walkable
        static char floor = ' '; //walkable, safe - done
        static char wall = '#'; //not walkable - done
        static char obstacle = '^'; //walkable, does dmg
        static char door = '>'; //walkable, state open
        static char closedDoor = '|'; //not walkable, needs key to open - half done, key interaction to add
        static char stairUp = '/'; //walkable, safe
        static char stairDown = '\\'; //walkable, sometimes our character can trip down the stairs
        static char[] walkable = new char[] { floor, obstacle, door, stairDown, stairUp }; //array of walkable objects
        

        static void Main(string[] args)
        {
            LevelZero();

        }
        static int LevelZero()
        {
            //Level Config Start
            int height = 14;
            int width = 30;
            int nextLevel = 1;
            int[] playerPosition = new int[] { 1, 0 }; //starting player position
            char[,] firstLevel = new char[height, width];
            //Level Config End

            while (true)
            {
                //
                //Building Stage Start
                //
                //Fill map with # and .
                FillMap(firstLevel, floor, wall);

                //Add things to map
                //AddVerticalWallOfSymbols(map, row, col, size, symbol)
                AddVerticalWallOfSymbols(firstLevel, 1, 7, 4, wall);
                PutSymbol(firstLevel, 5, 7, door);
                AddVerticalWallOfSymbols(firstLevel, 6, 7, 4, wall);

                AddHorizontalWallOfSymbols(firstLevel, 4, 8, 18, wall);

                AddHorizontalWallOfSymbols(firstLevel, 10, 1, 7, wall);

                AddVerticalWallOfSymbols(firstLevel, 1, 26, 4, wall);

                //Put player sign to the map
                PutSymbol(firstLevel, playerPosition[0], playerPosition[1], playerSign);
                //
                //Building Stage End
                //

                //Show map to player - After building stage
                ShowMap(firstLevel);
                Console.WriteLine(playerPosition[0] + " " + playerPosition[1]);
                //Move player
                int[] newPos = Movement(playerPosition);
                Console.WriteLine(CanMove(firstLevel, newPos, walkable));
                if (CanMove(firstLevel, newPos, walkable))
                {

                    playerPosition = newPos;
                }
                Console.Clear();
            }
            return nextLevel;
        }
        static int[] Movement(int[] playerPosition) {
            var key = Console.ReadKey(true);
            int[] newPos = new int[2];
            if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow)
            {
                newPos[0] = playerPosition[0] + 1;
                newPos[1] = playerPosition[1];
            }
            else if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow)
            {
                newPos[0] = playerPosition[0] - 1;
                newPos[1] = playerPosition[1];
            }
            else if (key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow)
            {
                newPos[0] = playerPosition[0];
                newPos[1] = playerPosition[1] + 1;
            }
            else if (key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow)
            {
                newPos[0] = playerPosition[0];
                newPos[1] = playerPosition[1] - 1;
            }
            return newPos;
        }
        static bool CanMove(char[,] map, int[] pos, char[] allowedSymbols)
        {
            if ((pos[0] < 0 || pos[0] > map.GetLength(0) - 1) || (pos[1] < 0 || pos[1] > map.GetLength(1) - 1))
            {
                return false;
            }
            if (allowedSymbols.Contains(map[pos[0], pos[1]]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static char[,] PutSymbol(char[,] map, int row, int col, char symbol)
        {
            map[row, col] = symbol;
            return map;
        }
        
        static void FillMap(char[,] map, char symbolTile, char symbolWall)
        {

            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    if ((col > 0 && col < map.GetLength(1) - 1) && (row > 0 && row < map.GetLength(0) - 1))
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

        static char[,] AddVerticalWallOfSymbols(char[,] map, int row, int col, int length, char symbol)
        {

            for (int counter = 0; counter < length; counter++)
            {
                map[counter + row, col] = symbol;
            }
            return map;
        }
        static char[,] AddHorizontalWallOfSymbols(char[,] map, int row, int col, int length, char symbol)
        {

            for (int counter = 0; counter < length; counter++)
            {
                map[row, counter + col] = symbol;
            }
            return map;
        }
        static void ShowMap(char[,] mapInChar)
        {
            string[] mapInStringArr = new string[biggestDimensionSize(mapInChar)];
            string mapInString = "";
            for (int x = 0; x < mapInChar.GetLength(0); x++)
            {
                for (int y = 0; y < mapInChar.GetLength(1); y++)
                {
                    mapInStringArr[x] += mapInChar[x, y];
                }
                mapInString += mapInStringArr[x] + "\n";
            }
            Console.Write(mapInString);
        }
        static int biggestDimensionSize(char[,] map)
        {
            int[] arrayOfSizes = new int[map.Rank];
            return map.GetLength(arrayOfSizes.Max());
        }

    }

}
