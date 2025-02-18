using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Minecraft
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Instance player = new Instance();
        bool is_colliding = false;
        private List<Instance> Workspace = new List<Instance>();

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

            Workspace.Add(player);
            CreateMap(Workspace);

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
        static void CreateMap(List<Instance> WS)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Grid_pos(4, 0, i, j))
                    {
                        continue;
                    }
                    if (i == 0 || j == 0 || i == 9 || j == 9)
                    {
                        Instance.Instatitate(WS, WS[0].Texture, new System.Numerics.Vector2(i * 48 + 120, j * 48));
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


            
            Get_input(Workspace[0]);
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !is_pressed)
            {
                is_pressed = true;
                Instance.Instatitate(Workspace, Content.Load<Texture2D>("dirt"), System.Numerics.Vector2.Add(player.Pos,new System.Numerics.Vector2(0,1)));

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
                    if (Collision.Collide(new Collision(Workspace[0]), new Collision(instance)))
                    {

                        System.Numerics.Vector2 direction = instance.Pos - Workspace[0].Pos;
                        direction= System.Numerics.Vector2.Normalize(direction);
                        Workspace[0].Pos = System.Numerics.Vector2.Subtract(Workspace[0].Pos, direction);
                        

                    }
            }
        }
        float speed = 2;
        static void Get_input(Instance plr)
        {
            float speed = 5;
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
