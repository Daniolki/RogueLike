using System.Collections.Generic;

namespace RogueLike
{
    public class Symbols
    {
        public static char playerSign = 'X'; //our character or other players - not walkable
        public static char wall = '#'; //not walkable - done
        public static char floor = ' '; //walkable, safe - done
        public static char obstacle = '^'; //walkable, does dmg
        public static char door = '>'; //walkable, state open
        public static char closedDoor = '|'; //not walkable, needs key to open - half done, key interaction to add
        public static char stairUp = '/'; //walkable, safe
        public static char stairDown = '\\'; //walkable, sometimes our character can trip down the stairs
        //char[] walkable; //array of walkable objects
        public static List<char> walkable = new List<char> { floor, obstacle, door, stairUp, stairDown };
        //   walkable = new char[] { floor, obstacle, door, stairDown, stairUp };
    }
}

