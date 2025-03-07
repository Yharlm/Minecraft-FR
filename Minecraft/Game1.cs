﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minecraft
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Player Player = new Player();
        private Instance player = new Instance();
        public bool is_colliding = false;
        private List<Instance> Workspace = new List<Instance>();
        private List<Instance> Items = new List<Instance>();
        int[,] grid = new int[20, 20];
        public System.Numerics.Vector2 camera_pos = new System.Numerics.Vector2(0,0);
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            Instance Dirt = new Instance();
            Dirt.Texture = Content.Load<Texture2D>("dirt");
            Items.Add(Dirt);
            Workspace.Add(player);
            Instance Grass = new Instance();
            Grass.Texture = Content.Load<Texture2D>("Grass_block");
            Items.Add(Grass);
            Workspace.Add(player);

            CreateMap(Workspace, Items, grid);



        }
        static bool Grid_pos(int y, int x, int i, int j)
        {
            if (y == i && x == j)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void CreateMap(List<Instance> WS, List<Instance> Items, int[,] grid)
        {
            //for (int j = 4; j < 9; j++)
            //{
            //    Instance.Instatitate(WS, Items[1].Texture, new System.Numerics.Vector2(j * 48 + 120, 48));
            //}

            for (int i = 5; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    int id = grid[i, j];
                    {
                        Vector2 offset = new Vector2(0,0);
                        Instance.Instatitate(WS, Items[id].Texture, new System.Numerics.Vector2(j * 48 , i * 48));
                    }

                }
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D Dirt = Content.Load<Texture2D>("Grass_block");
            player.Texture = Dirt;



            // TODO: use this.Content to load your game content here
        }

        bool is_pressed = false;
        protected override void Update(GameTime gameTime)
        {


            
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                float mouseposX = Mouse.GetState().Position.X;
                float mouseposY = Mouse.GetState().Position.Y;
                is_pressed = true;
                Instance.Instatitate(Workspace, Content.Load<Texture2D>("dirt"), new System.Numerics.Vector2(mouseposX - Workspace[0].collider_size/2, mouseposY - Workspace[0].collider_size / 2) + camera_pos);
                
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                is_pressed = false;
            }
            if
                (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                float mouseX = Mouse.GetState().X;
                float mouseY = Mouse.GetState().Y;  
                var item = Workspace.Find(x => x.Pos.X < mouseX && x.Pos.X + x.collider_size > mouseX &&
                                               x.Pos.Y < mouseY && x.Pos.Y + x.collider_size > mouseY);
                Workspace.Remove(item);
            }
            base.Update(gameTime);
            bool collison = false;
            foreach (Instance instance in Workspace)
            {

                if (instance != Workspace[0])
                {
                    System.Numerics.Vector2 margin2 = new System.Numerics.Vector2(Workspace[0].Pos.X + Workspace[0].collider_size / 2, Workspace[0].Pos.Y + Workspace[0].collider_size / 2);
                    System.Numerics.Vector2 margin1 = new System.Numerics.Vector2(instance.Pos.X + instance.collider_size / 2, instance.Pos.Y + instance.collider_size / 2);
                    
                    //Collider collider = new Collider();
                    //if(Collision.Collide(new Collision(Workspace[0]), new Collision(instance)))
                    //{
                    //    Workspace[0].collider = Collision.CollideSide(new Collision(Workspace[0]), new Collision(instance));
                    //}
                    while (true)
                    {
                        if (Collision.Collide(new Collision(Workspace[0]), new Collision(instance)))
                        {
                            //float speed = 3;
                            is_colliding = true;
                            Workspace[0].is_colliding = true;
                        }
                        else if(is_colliding)
                        {
                            if (!Collision.Collide(new Collision(Workspace[0]), new Collision(instance)))
                            {
                                //float speed = 3;
                                is_colliding = false;
                                Workspace[0].is_colliding = false;
                            }
                            //is_colliding = false;
                            //Workspace[0].is_colliding = false;
                            break;
                        }
                        float densityX =float.Abs(margin1.X - margin2.X)/122;
                        float densityY = float.Abs(margin1.Y - margin2.Y)/122;

                        if (margin1.X < margin2.X)
                        {
                            Workspace[0].Pos.X += densityX;
                        }
                        if (margin1.X > margin2.X)
                        {
                            Workspace[0].Pos.X -= densityX;
                        }
                        if (margin1.Y < margin2.Y)
                        {
                            Workspace[0].Pos.Y += densityY;
                        }
                        if (margin1.Y > margin2.Y)
                        {
                            Workspace[0].Pos.Y -= densityY;
                        }
                       

                    }

                }
            }
            
            Get_input(Workspace[0], Player, collison,camera_pos);
        }

        static void Get_input(Instance plr, Player player, bool col,Vector2 camera)
        {
            float gravity = 5;
            if(player.player_jumped == true)
            {
                plr.Pos.Y += gravity;
            }
            
            
            if (plr.is_colliding)
            {
                player.Pos = System.Numerics.Vector2.Zero;
                player.player_jumped = false;
                player.jump_time = 100;
            }


            float speed = player.speed;
            
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                
                plr.Pos.Y -= speed;
                camera.X -= speed;
                player.jump_time--;
            }
            if(player.jump_time <= 0)
            {
                player.player_jumped = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                
                plr.Pos.Y+= speed * 2;
                camera.X += speed * 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                plr.Pos.X -= speed;
                camera.Y -= speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                plr.Pos.X += speed;
                camera.Y -= speed;
            }
            



        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            if (Workspace[0].is_colliding)
            {
                _spriteBatch.Draw(Workspace[0].Texture, player.Pos, null, Color.Red, 0f, Vector2.One, Workspace[0].Size, SpriteEffects.None, 0f);

            }
            else
            {
                _spriteBatch.Draw(Workspace[0].Texture, player.Pos, null, Color.White, 0f, Vector2.One, Workspace[0].Size, SpriteEffects.None, 0f);

            }
            _spriteBatch.End();
            foreach (Instance instance in Workspace)
            {
                
                if (instance == Workspace[0]) continue;
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                _spriteBatch.Draw(instance.Texture, instance.Pos + camera_pos, null, Color.White, instance.orientation, Vector2.One, instance.Size, SpriteEffects.None, 0f);
                _spriteBatch.End();
            }
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.DrawString(Content.Load<SpriteFont>("File"), "X: " + Workspace[0].Pos.X + " Y: " + Workspace[0].Pos.Y, new Vector2(0, 0), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
