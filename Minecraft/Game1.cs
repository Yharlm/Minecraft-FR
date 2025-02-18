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

        Instance player = new Instance();

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

        static void CreateMap(List<Instance> WS)
        {
           for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Instance.Instatitate(WS, WS[0].Texture, new System.Numerics.Vector2(i * 16, j * 16));
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
                Instance.Instatitate(Workspace, Content.Load<Texture2D>("dirt"), player.Pos);

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
                foreach( Instance instance1 in Workspace)
                {
                    if(instance == instance1)
                    {
                        continue;
                    }
                    if (Collision.Collide(new Collision(instance1), new Collision(instance)))
                    {

                        player.Pos.X = 0f ;
                        player.Pos.Y = 0f;
                    }
                }
            }
        }

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
