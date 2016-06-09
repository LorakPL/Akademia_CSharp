using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using System.Collections.Generic;

namespace Fenix
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private enum State
        {
            Menu,
            Playing,
            GameOver
        }

        State gameState = State.Menu;
        private Texture2D menuImage;
        private Texture2D gameOverImage;
        private Texture2D errorImage;

        Color backgroundColor = Color.TransparentBlack;

        Object enemy;
        Object enemy2;
        Object enemy3;
        Player player;

        MovingBackground lava;
        MovingBackground water;

        Text text;

        Random rand;
        private int randomValue;

        private KeyboardState currentKeyboardState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            menuImage = null;
            gameOverImage = null;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            enemy = new Object();
            enemy2 = new Object();
            enemy3 = new Object();
            player = new Player();
            rand = new Random();
            text = new Text();
            text.list = new List<string>();
            lava = new MovingBackground();
            water = new MovingBackground();

            this.graphics.PreferredBackBufferWidth = 900;
            this.graphics.PreferredBackBufferHeight = 800;
            this.Window.Position = new Point(200, 30);
            this.Window.Title = "Akademia C# - projekt";
            this.graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player.Initialize(Content.Load<Texture2D>("phoenix"), 10f, new Vector2(100, 300), false, 100);
            enemy.Initialize(Content.Load<Texture2D>("water1"), 9f, new Vector2(1000, 70));
            enemy2.Initialize(Content.Load<Texture2D>("water2"), 13f, new Vector2(1000, 250));
            enemy3.Initialize(Content.Load<Texture2D>("water3"), 5f, new Vector2(1000, 440));

            lava.Initialize(Content.Load<Texture2D>("lava"), new Vector2(-1, 730), new Vector2(-900, 730), 1, "lava");
            water.Initialize(Content.Load<Texture2D>("water"), new Vector2(-1, 0), new Vector2(-900, 0), 1, "water");

            menuImage = Content.Load<Texture2D>("menu");
            gameOverImage = Content.Load<Texture2D>("gameover");
            errorImage = Content.Load<Texture2D>("error");

            text.font = Content.Load<SpriteFont>("font");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (gameState)
            {
                case State.Menu:
                    {
                        KeyboardState keyState = Keyboard.GetState();

                        if (keyState.IsKeyDown(Keys.Space))
                        {
                            gameState = State.Playing;
                        }

                        break;
                    }

                case State.Playing:
                    {

                        if (player.Death == true)
                        {
                            Thread.Sleep(1000);

                            text.list.Add(text.timer.ToString("0.00"));
                            gameState = State.GameOver;
                        }

                        player.ObjectRectangle = new Rectangle((int)player.Position.X, (int)player.Position.Y, 50, 100);

                        enemy.ObjectRectangle = new Rectangle((int)enemy.Position.X, (int)enemy.Position.Y, 260, 90);
                        enemy2.ObjectRectangle = new Rectangle((int)enemy2.Position.X, (int)enemy2.Position.Y, 215, 100);
                        enemy3.ObjectRectangle = new Rectangle((int)enemy3.Position.X, (int)enemy3.Position.Y, 400, 140);

                        EnemyReset(gameTime);

                        Collisions(gameTime);

                        UpdatePlayer(gameTime);
                        player.playerHealth(gameTime);

                        lava.Update(gameTime);
                        water.Update(gameTime);

                        text.timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                        break;
                    }

                case State.GameOver:
                    {
                        KeyboardState keyState = Keyboard.GetState();

                        if (keyState.IsKeyDown(Keys.Space))
                        {
                            gameState = State.Playing;
                            player.Initialize(10f, new Vector2(100, 300), false, 100);
                            enemy.Initialize(9f, new Vector2(1000, enemy.Position.Y));
                            enemy2.Initialize(13f, new Vector2(1000, enemy2.Position.Y));
                            enemy3.Initialize(5f, new Vector2(1000, enemy3.Position.Y));
                            backgroundColor = Color.Navy;
                            text.timer = 0;
                        }

                        break;
                    }
            }
            base.Update(gameTime);
        }

        private void UpdatePlayer(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                player.Position -= new Vector2(0, player.Speed);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                player.Position += new Vector2(0, player.Speed);
            }
            player.Position = new Vector2(player.Position.X, MathHelper.Clamp(player.Position.Y, 0, 700));
        }

        private void EnemyReset(GameTime gameTime)
        {
            if (enemy.Position.X + enemy.Width < 0
                || enemy2.Position.X + enemy2.Width < 0
                || enemy3.Position.X + enemy3.Width < 0)
            {
                randomValue = rand.Next(3);

                if (randomValue == 0 && enemy.Position.X + enemy.Width < 0)
                {
                    enemy.Position = new Vector2(1000, rand.Next(70, 150));
                    enemy.Speed += .9f;
                }
                if (randomValue == 1 && enemy2.Position.X + enemy2.Width < 0)
                {
                    enemy2.Position = new Vector2(1000, rand.Next(250, 350));
                    enemy2.Speed += 1.2f;
                }
                if (randomValue == 2 && enemy3.Position.X + enemy3.Width < 0)
                {
                    enemy3.Position = new Vector2(1000, rand.Next(460, 590));
                    enemy3.Speed += 1.5f;
                }
            }
        }

        private void Collisions(GameTime gameTime)
        {
            if (player.ObjectRectangle.Intersects(enemy.ObjectRectangle)
                || player.ObjectRectangle.Intersects(enemy2.ObjectRectangle)
                || player.ObjectRectangle.Intersects(enemy3.ObjectRectangle))
            {
                backgroundColor = Color.Red;
                enemy.Speed = 0f;
                enemy2.Speed = 0f;
                enemy3.Speed = 0f;
                player.Speed = 0;
                player.Death = true;
            }
            else
            {
                backgroundColor = Color.Navy;
                enemy.Position -= new Vector2(enemy.Speed, 0);
                enemy2.Position -= new Vector2(enemy2.Speed, 0);
                enemy3.Position -= new Vector2(enemy3.Speed, 0);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            spriteBatch.Begin();

            switch (gameState)
            {
                case State.Menu:
                    {
                        spriteBatch.Draw(menuImage, new Vector2(0, 0), Color.White);
                        try
                        {
                            text.Read(spriteBatch);
                        }
                        catch
                        {
                            spriteBatch.Draw(errorImage, new Vector2(0, 0), Color.White);
                        }
                        break;
                    }

                case State.Playing:
                    {
                        lava.Draw(spriteBatch);
                        water.Draw(spriteBatch);
                        spriteBatch.DrawString(text.font, player.Health.ToString("0"), new Vector2(850, 0), Color.Red);
                        player.Draw(spriteBatch);
                        enemy.Draw(spriteBatch);
                        enemy2.Draw(spriteBatch);
                        enemy3.Draw(spriteBatch);
                        break;
                    }

                case State.GameOver:
                    {
                        spriteBatch.Draw(gameOverImage, new Vector2(0, 0), Color.White);
                        spriteBatch.DrawString(text.font, "Ostatnie wyniki:", new Vector2(10, 250), Color.Black);
                        text.Draw(spriteBatch);
                        break;
                    }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
