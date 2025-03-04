using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
                        Instance.Instatitate(WS, Items[id].Texture, new System.Numerics.Vector2(j * 48 + 120, i * 48));
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
            bool collison = false;
            foreach (Instance instance in Workspace)
            {

                if (instance != Workspace[0])
                {
                    System.Numerics.Vector2 margin2 = new System.Numerics.Vector2(Workspace[0].Pos.X + Workspace[0].collider_size / 2, Workspace[0].Pos.Y + Workspace[0].collider_size / 2);
                    System.Numerics.Vector2 margin1 = new System.Numerics.Vector2(instance.Pos.X + instance.collider_size / 2, instance.Pos.Y + instance.collider_size / 2);
                    //while (true)
                    //{
                    Collider collider = new Collider();
                    if(Collision.Collide(new Collision(Workspace[0]), new Collision(instance)))
                    {
                        
                        Workspace[0].collider = Collision.CollideSide(new Collision(Workspace[0]), new Collision(instance));
                    }
                    



                    //}

                }
            }
            Get_input(Workspace[0], Player, collison);
        }

        static void Get_input(Instance plr, Player player, bool col)
        {

            



            float speed = player.speed;

            if (Keyboard.GetState().IsKeyDown(Keys.W) && !plr.collider.sideC)
            {
                plr.Pos.Y -= speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && !plr.collider.sideA)
            {
                plr.Pos.Y += speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) && !plr.collider.sideD)
            {
                plr.Pos.X -= speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) && !plr.collider.sideB)
            {
                plr.Pos.X += speed;
            }
            



        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            System.Numerics.Vector2 camera_pos = new System.Numerics.Vector2(player.Pos.X, player.Pos.Y);
            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(Workspace[0].Texture, Workspace[0].Pos, null, Color.White, 0f, Vector2.Zero, Workspace[0].Size, SpriteEffects.None, 0f);
            _spriteBatch.End();
            foreach (Instance instance in Workspace)
            {
                if (instance == Workspace[0]) continue;
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                _spriteBatch.Draw(instance.Texture, instance.Pos + camera_pos * 0, null, Color.White, instance.orientation, Vector2.Zero, instance.Size, SpriteEffects.None, 0f);
                _spriteBatch.End();
            }
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.DrawString(Content.Load<SpriteFont>("File"), "X: " + Workspace[0].Pos.X + " Y: " + Workspace[0].Pos.Y, new Vector2(0, 0), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
