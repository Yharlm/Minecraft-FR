using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Minecraft
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Player Player = new Player();
        private Instance player = new Instance();
        bool is_colliding = false;
        private List<Instance> Workspace = new List<Instance>();
        private List<Instance> Items = new List<Instance>();
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
            Workspace[0].collider_size +=1;
            CreateMap(Workspace, Items);
            


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
        static void CreateMap(List<Instance> WS,List<Instance> Items)
        {
            for (int j = 4; j < 9; j++)
            {
                Instance.Instatitate(WS, Items[0].Texture, new System.Numerics.Vector2(j * 48 + 120, 48));
            }

                for (int i = 5; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    //if (Grid_pos(4, 0, i, j))
                    //{
                    //    continue;
                    //}
                    //if (i == 0 || j == 0 || i == 9 || j == 9)
                    {
                        Instance.Instatitate(WS, Items[0].Texture, new System.Numerics.Vector2(j * 48 + 120, i * 48));
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


            float mouseposX = Mouse.GetState().X;
            float mouseposY = Mouse.GetState().Y;
            Get_input(Workspace[0], Player);
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !is_pressed)
            {
                is_pressed = true;
                Instance.Instatitate(Workspace, Content.Load<Texture2D>("dirt"), new System.Numerics.Vector2(mouseposX - Workspace[0].collider_size/2, mouseposY - Workspace[0].collider_size / 2));
                
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                is_pressed = false;
            }
            if
                (Keyboard.GetState().IsKeyDown(Keys.E))
            {

                Workspace[0].Color = System.Drawing.Color.Red;
            }
            base.Update(gameTime);
            foreach (Instance instance in Workspace)
            {

                if (instance != Workspace[0])
                {
                    System.Numerics.Vector2 margin2 = new System.Numerics.Vector2(Workspace[0].Pos.X + Workspace[0].collider_size, Workspace[0].Pos.Y + Workspace[0].collider_size);
                    System.Numerics.Vector2 margin1 = new System.Numerics.Vector2(instance.Pos.X + instance.collider_size, instance.Pos.Y + instance.collider_size);

                    if (Collision.Collide(new Collision(Workspace[0]), new Collision(instance)) || Keyboard.GetState().IsKeyDown(Keys.E))
                    {
                        float speed = 3;
                        is_colliding = true;
                        if (Workspace[0].Pos.X < instance.Pos.X)
                        {
                            Workspace[0].Pos.X -= speed;
                        }
                        if (Workspace[0].Pos.X > instance.Pos.X)
                        {
                            Workspace[0].Pos.X += speed;
                        }
                        if (Workspace[0].Pos.Y < instance.Pos.Y)
                        {
                            Workspace[0].Pos.Y -= speed;
                        }
                        if (Workspace[0].Pos.Y > instance.Pos.Y)
                        {
                            Workspace[0].Pos.Y += speed;
                        }

                    }
                    else
                    {
                        is_colliding = false;
                    }

                }
            }
        }

        static void Get_input(Instance plr, Player player)
        {
            if(player.is_colliding == true)
            {
                player.speed = 0;
            }
            else
            {
                player.speed = 3;
            }
            float speed = player.speed;
            plr.Pos.Y += speed;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                plr.Pos.Y -= speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                plr.Pos.Y += speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                plr.Pos.X -= speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                plr.Pos.X += speed;
            }
            



        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.DrawString(Content.Load<SpriteFont>("File"), Mouse.GetState().Position.ToString(),Vector2.Zero,Color.Wheat);
            _spriteBatch.End();
            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(Workspace[0].Texture, Workspace[0].Pos, null, Color.White, 0f, Vector2.Zero, Workspace[0].Size, SpriteEffects.None, 0f);
            _spriteBatch.End();
            foreach (Instance instance in Workspace)
            {

                _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                _spriteBatch.Draw(instance.Texture, instance.Pos, null, Color.White, instance.orientation, Vector2.Zero, instance.Size, SpriteEffects.None, 0f);
                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
