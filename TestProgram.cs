using System;
using System.Collections.Generic;
using System.Text;

namespace RogueLike
{
    class TestProgram
    {
        static void Main(string[] args)
        {
            Level zero = new Level();
            zero.ShowMap();

            int[] levelOneStartingPosition = { 1, 1 };
            Level one = new Level(14, 30, 2, levelOneStartingPosition);
            one.ShowMap();

        }
    }
}
