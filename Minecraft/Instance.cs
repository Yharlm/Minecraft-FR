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
        public Vector2 Pos;
        public Color Color;
        public float Size = 3; //160
        public float orientation = 0f;
        public Texture2D Texture;
        public Type type;

        public static void Instatitate(List<Instance> Workspace, Texture2D texture, Vector2 pos)
        {
            Instance instance = new Instance();
            instance.Texture = texture;
            instance.Pos = pos;
            Workspace.Add(instance);
        }
    }

    class Collision(Instance obj)
    { 
        Vector2 margin1 = new Vector2(obj.Pos.X-obj.Size/2, obj.Pos.Y - obj.Size / 2);
        Vector2 margin2 = new Vector2(obj.Pos.X + obj.Size / 2, obj.Pos.Y + obj.Size / 2);

        public bool Collide(Collision obj1,Collision obj2)
        {
            
            if(obj1.margin1.X < obj2.margin2.X && obj1.margin2.X > obj2.margin1.X && obj1.margin1.Y < obj2.margin2.Y && obj1.margin2.Y > obj2.margin1.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
