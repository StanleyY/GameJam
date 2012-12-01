using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Racer
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Car Player;
        startMenu Menu;
        Wall brick;
        Rectangle screenRectangle;
        Boolean start;
        GG gameOver;
        Boolean lost;
        powerUp redPow;
        powerUp greenPow;
        powerUp bluePow;

        SpriteFont font;
        string PlayerTime = "Time: ";
        TimeSpan startScreen;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            screenRectangle = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            start = false;
            lost = false;
            Console.WriteLine("Game1() ran");
            Console.WriteLine(graphics.PreferredBackBufferHeight);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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

            // TODO: use this.Content to load your game content here
            Texture2D menuTexture = Content.Load<Texture2D>("startMenu");
            Texture2D ggTexture = Content.Load<Texture2D>("ggscreen");

            font = Content.Load<SpriteFont>("myFont");

            Menu = new startMenu(menuTexture);
            gameOver = new GG(ggTexture, lost);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            if (!start)
            {
                start = Menu.Update();
                //Console.WriteLine(start);
                Texture2D tempTexture = Content.Load<Texture2D>("ball");
                Player = new Car(tempTexture, screenRectangle);
                Texture2D tempWallTexture = Content.Load<Texture2D>("ball");
                brick = new Wall(tempWallTexture, screenRectangle);
                Texture2D redTexture = Content.Load<Texture2D>("red");
                redPow = new powerUp(redTexture, screenRectangle);
                Texture2D greenTexture = Content.Load<Texture2D>("green");
                greenPow = new powerUp(greenTexture, screenRectangle);
                Texture2D blueTexture = Content.Load<Texture2D>("blue");
                bluePow = new powerUp(blueTexture, screenRectangle);
                startScreen = gameTime.TotalGameTime;
            }
            TimeSpan timePlaying = gameTime.TotalGameTime.Subtract(startScreen);
            if (start)
            {
                Player.Update();
                //brick.Update();
                //redPow.Update();
                greenPow.Update();
                //bluePow.Update();
                //draw gg
            }
            if (brick.checkCollision(Player.getRectangle()))
            {
                Console.WriteLine("touche");
                Player.takeDamage();
                brick.hitPlayer = true;
                if (Player.getShields() <= 0)
                {
                    gameOver.setLost(true);
                    lost = gameOver.getLost();
                }
            }
            if (redPow.checkCollision(Player.getRectangle()))
            {
                Console.WriteLine("redPOW");
                Player.addShields();
                redPow.hitPlayer = true;
                Console.WriteLine(Player.getShields());
            }
            if (greenPow.checkCollision(Player.getRectangle()))//is permanent for now
            {
                Console.WriteLine("greenPOW");
                TimeSpan timeToStop = timePlaying.Add(TimeSpan.Parse("0:0:10"));
                Player.buffMultiplier(2);//timespan of 10s
                greenPow.hitPlayer = true;
                Console.WriteLine("You got a green power up");
            }
            if (bluePow.checkCollision(Player.getRectangle()))
            {
                Console.WriteLine("bluePOW");
                bluePow.hitPlayer = true;
                Console.WriteLine("You got a blue power up");
            }
            //Player.updateScore(gameTime.TotalGameTime);
            //TimeSpan timePlaying = gameTime.TotalGameTime.Subtract(startScreen);
            if (!lost && start)
                PlayerTime = "Time: " + timePlaying.ToString();
            base.Update(gameTime);
        }

        private void DrawText()
        {
            spriteBatch.DrawString(font, PlayerTime, new Vector2(10, 10), Color.White);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            
            Player.Draw(spriteBatch);
            brick.Draw(spriteBatch);
            redPow.Draw(spriteBatch);
            greenPow.Draw(spriteBatch);
            bluePow.Draw(spriteBatch);
            Menu.Draw(spriteBatch);
            gameOver.Draw(spriteBatch);
            DrawText();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
