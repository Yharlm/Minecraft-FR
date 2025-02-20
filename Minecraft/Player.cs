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
        public float size = 5; //160
        public bool is_moving = true;
        public float gravity = 2f;
        Collision collision;
        public bool is_colliding = false;
        public float speed = 5f;
        

    }

    class Cordinates(int x,int y)
    {
        public int X = x;
        public int Y = y;
    }

    
}
