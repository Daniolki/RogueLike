using System;

namespace RogueLike
{
    class Program
    {

        static char playerSign = 'X'; //our character or other players - not walkable
        static char floor = '.'; //walkable, safe - done
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
            int height = 25;
            int width = 20;
            int nextLevel = 1;
            int[] playerPosition = new int[] { 3, 3 }; //starting player position
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
                AddVerticalWallOfSymbols(firstLevel, 1, 1, 1, playerSign);
                AddVerticalWallOfSymbols(firstLevel, 2, 2, 3, wall);
                AddHorizontalWallOfSymbols(firstLevel, 5, 5, 2, wall);
                AddHorizontalWallOfSymbols(firstLevel, 6, 5, 3, door);
                AddHorizontalWallOfSymbols(firstLevel, 7, 5, 2, obstacle);
                AddHorizontalWallOfSymbols(firstLevel, 8, 5, 2, closedDoor);
                AddHorizontalWallOfSymbols(firstLevel, 9, 5, 2, stairUp);
                AddHorizontalWallOfSymbols(firstLevel, 10, 5, 2, stairDown);

                //Put player sign to the map
                PutSymbol(firstLevel, playerPosition[0], playerPosition[1], playerSign);
                //
                //Building Stage End
                //

                //Show map to player - After building stage
                ShowMap(firstLevel);

                //Move player
                var key = Console.ReadKey(true);
                int[] newPos = new int[] { 0, 0 };
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
                else if (key.Key == ConsoleKey.Escape) //dodać nextLevel
                {
                    break;
                }
                if (CanMove(firstLevel, newPos, walkable))
                {
                    playerPosition = newPos;
                }
                Console.Clear();
            }
            return nextLevel;
        }

        static bool CanMove(char[,] map, int[] pos, char[] forbiddenSymbols)
        {
            //bool isChecked = false;
            for (int x = 0; x < forbiddenSymbols.Length; x++)
            {
                if (map[pos[0], pos[1]] == forbiddenSymbols[x])
                {
                    Console.WriteLine(forbiddenSymbols[x]);
                    return true;
                }
            }
            return false;
        }

        static void PutSymbol(char[,] map, int x, int y, char symbol)
        {
            map[x, y] = symbol;
        }

        static void FillMap(char[,] map, char symbolTile, char symbolWall)
        {

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if ((y > 0 && y < map.GetLength(1) - 1) && (x > 0 && x < map.GetLength(0) - 1))
                    {
                        map[x, y] = symbolTile;
                    }
                    else
                    {
                        map[x, y] = symbolWall;
                    }
                }
            }
        }
        static void AddVerticalWallOfSymbols(char[,] map, int startY, int startX, int length, char symbol)
        {

            for (int counter = 1; counter < length + 1; counter++)
            {
                map[counter + startY, startX] = symbol;
            }
        }
        static void AddHorizontalWallOfSymbols(char[,] map, int startY, int startX, int length, char symbol)
        {

            for (int counter = 1; counter < length + 1; counter++)
            {
                map[startY, counter + startX] = symbol;
            }
        }
        static void ShowMap(char[,] mapInChar)
        {
            string[] mapInStringArr = new string[whichDimensionIsBigger(mapInChar)];
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
        static int whichDimensionIsBigger(char[,] map)
        {
            if (map.GetLength(0) <= map.GetLength(1))
            {
                return map.GetLength(1);
            }
            else
            {
                return map.GetLength(0);
            }
        }

    }

}
