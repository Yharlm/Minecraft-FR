using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft
{
    class Player
    {
        public Texture2D Texture;
        public Vector2 Pos;
        public Cordinates Cordinates;
        public Vector2 Grid_pos;
        public float size = 3; //160

        

    }

    class Cordinates(int x,int y)
    {
        public int X = x;
        public int Y = y;
    }

    class Map
    {
        public int[,] grid;
        public static void MapChanges(int[,] grid) 
        {
            int counter = 0;
            while (counter <10)
            {
                grid[9, counter] = 1;
                grid[counter, 0] = 1;
                grid[0, counter] = 1;
                grid[counter, 9] = 1;
                counter++;
            }
        }
    }
}
