using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft
{
    internal class Instance
    {
        public bool anchored = true;
        public Vector2 Pos;
        public Color Color;
        public float Size = 3; //160
        public float collider_size = 46;
        public float gravity = 20f;
        public float orientation = 0f;
        public Collider collider = new Collider();
        public Texture2D Texture;
        public Type type;

        public static void Instatitate(List<Instance> Workspace, Texture2D texture, Vector2 pos)
        {
            Instance instance = new Instance();
            instance.Texture = texture;
            instance.Pos = pos;
            Workspace.Add(instance);
            bool is_colliding = false;
        }


    }
    class Collider()
    {
        public bool sideA = false;
        public bool sideB = false;
        public bool sideC = false;
        public bool sideD = false;
    }
    class Collision(Instance obj)
    {
        Vector2 margin1 = new Vector2(obj.Pos.X, obj.Pos.Y);
        Vector2 margin2 = new Vector2(obj.Pos.X + obj.collider_size, obj.Pos.Y + obj.collider_size);
        

        public static bool Collide(Collision obj1, Collision obj2)
        {

            if (obj1.margin1.X <= obj2.margin2.X && obj1.margin2.X >= obj2.margin1.X && obj1.margin1.Y <= obj2.margin2.Y && obj1.margin2.Y >= obj2.margin1.Y)
            {

                return true;
            }
            else
            {
                if (obj1.margin1.Y == obj2.margin1.Y && obj1.margin1.X == obj2.margin1.X)
                {
                    return true;
                }
                if (obj1.margin2.Y == obj2.margin2.Y && obj1.margin2.X == obj2.margin2.X)
                {
                    return true;
                }
                return false;
            }
        }
        public static Collider CollideSide(Collision obj1, Collision obj2)
        {
            Collider collider = new Collider();
            
            if(obj1.margin1.X <= obj2.margin2.X)
            {
                collider.sideD = true;
            }
            if(obj1.margin2.X >= obj2.margin1.X)
            {
                collider.sideB = true;
            }
            if(obj1.margin1.Y <= obj2.margin2.Y)
            {
                collider.sideC = true;
            }
            if (obj1.margin2.Y >= obj2.margin1.Y)
            {

                collider.sideA = true;

            }

            //else
            //{
            //    if (obj1.margin1.Y == obj2.margin1.Y && obj1.margin1.X == obj2.margin1.X)
            //    {
                   
            //    }
            //    if (obj1.margin2.Y == obj2.margin2.Y && obj1.margin2.X == obj2.margin2.X)
            //    {
                    
            //    }
                
            //}
            return collider;
        }


    }
}
