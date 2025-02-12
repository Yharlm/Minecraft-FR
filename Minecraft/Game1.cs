using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Minecraft
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Player player = new Player();
        Map map = new Map();


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            player.Cordinates = new Cordinates(1, 1);
            map.grid = new int[10, 10];
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D Dirt = Content.Load<Texture2D>("Grass_block");
            player.Texture = Dirt;
            Map.MapChanges(map.grid);

            // TODO: use this.Content to load your game content here
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (map.grid[player.Cordinates.Y+1,player.Cordinates.X] == 0)
            {
                player.Pos.Y += player.gravity;
                if(player.Pos.Y % (16f * player.size) == 0f)
                {
                    player.is_moving = true;
                    player.Cordinates.Y = int.Parse((player.Pos.Y / (16f * player.size)).ToString());

                }
                else
                {
                    player.is_moving = false;
                }
            }
            
            if(player.is_moving == false)
            {
                player.Grid_pos.Y = 16f * player.size * player.Cordinates.Y;
                player.Grid_pos.X = 16f * player.size * player.Cordinates.X;
                player.Pos.Y = player.Cordinates.Y* 16* player.size;
                player.Pos.X = player.Cordinates.X * 16 * player.size;
            }
            
            // TODO: Add your update logic here

            Get_input(player, map);
            base.Update(gameTime);
        }

        static void Get_input(Player plr, Map map)
        {
            float speed = 4;
            float Float_To_int = 16f * plr.size;



            float Grid_size = 16f * plr.size;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                
                if (map.grid[plr.Cordinates.Y - 1, plr.Cordinates.X] == 0)
                {
                    plr.Pos.Y -= speed;
                    if (plr.Pos.Y % Float_To_int == 0f)
                    {
                        plr.is_moving = true;
                        plr.Cordinates.Y = int.Parse((plr.Pos.Y / Grid_size).ToString());
                    }
                }
            }
            else
            {
                   plr.is_moving = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (map.grid[plr.Cordinates.Y + 1, plr.Cordinates.X] == 0)
                {
                    plr.Pos.Y += speed;
                    if (plr.Pos.Y % Float_To_int == 0f)
                    {
                        plr.is_moving = true;
                        plr.Cordinates.Y = int.Parse((plr.Pos.Y / Grid_size).ToString());
                    }
                }
            }
            else
            {
                plr.is_moving = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (map.grid[plr.Cordinates.Y, plr.Cordinates.X - 1] == 0)
                {
                    plr.Pos.X -= speed;
                    if (plr.Pos.X % Float_To_int == 0f)
                    {
                        plr.is_moving = true;
                        plr.Cordinates.X = int.Parse((plr.Pos.X / Grid_size).ToString());
                    }
                }
            }
            else
            {
                plr.is_moving = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (map.grid[plr.Cordinates.Y, plr.Cordinates.X + 1] == 0)
                {
                    plr.Pos.X += speed;
                    if (plr.Pos.X % Float_To_int == 0f)
                    {
                        plr.is_moving = true;
                        plr.Cordinates.X = int.Parse((plr.Pos.X / Grid_size).ToString());
                    }
                }
            }
            else
            {
                plr.is_moving = false;
            }
            


            






        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (map.grid[i, j] == 1)
                    {
                        _spriteBatch.Draw(Content.Load<Texture2D>("dirt"), new Vector2(j * player.size * 16f, i * player.size * 16f), null, Color.White, 0f, Vector2.Zero, player.size, SpriteEffects.None, 0f);
                    }
                }
            }
            _spriteBatch.End();

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(player.Texture, player.Pos, null, Color.White, 0f, Vector2.Zero, player.size, SpriteEffects.None, 0f);
            _spriteBatch.End();
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(player.Texture, player.Grid_pos, null, Color.Red, 0f, Vector2.Zero, player.size, SpriteEffects.None, 0f);
            _spriteBatch.End();

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.DrawString(Content.Load<SpriteFont>("File"), "X: " + player.Pos.X + " Y: " + player.Pos.Y, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(Content.Load<SpriteFont>("File"), "X: " + player.Cordinates.X + " Y: " + player.Cordinates.Y, new Vector2(0, 20), Color.White);
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
