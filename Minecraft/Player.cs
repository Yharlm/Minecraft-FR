using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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
        public float size = 5; //160

        

    }

    class Cordinates(int x,int y)
    {
        public int X = x;
        public int Y = y;
    }

    class Map
    {
        public int[,] grid;
        public static void MapChanges() 
        {
            grid[1, 1] = 1;
        }
    }
}
