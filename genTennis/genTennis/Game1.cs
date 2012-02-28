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

namespace genTennis
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D leftRacquet;
        Texture2D rightRacquet;
        Texture2D ball;

        Rectangle left = new Rectangle(0, 0, 20, 80);
        Rectangle right = new Rectangle( 780, 0, 20, 80);
        Rectangle theBall = new Rectangle(400, 224, 10, 10);

        double velocity = 6;

        double velocityX;
        double velocityY;

        double theta = 1;
        Boolean lost = false;

        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            leftRacquet = this.Content.Load<Texture2D>("theRacquet");
            rightRacquet = this.Content.Load<Texture2D>("theRacquet");
            ball = this.Content.Load<Texture2D>("theBall");
            font = this.Content.Load<SpriteFont>("SpriteFont1");
            // TODO: use this.Content to load your game content here
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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) == true) this.Exit();
            MouseState mouseState = Mouse.GetState();
            // TODO: Add your update logic here
            //the right racquet movement
            right.Y = theBall.Y - 10;



            // the movement of the ball is completely specified here 
            velocityX = velocity * Math.Sin(theta);
            velocityY = velocity * Math.Cos(theta);


            theBall.X += (int)velocityX;          
            theBall.Y += (int)velocityY;
            //left racquet
            if (theBall.X <= left.Width && theBall.Y - left.Y <= left.Height && theBall.Y - left.Y >= 0) Rebound(1);
            //if the ball touches the top, it should rebound
            if (theBall.Y <= 0) Rebound(2);
            //right racquet
            if (theBall.X >= GraphicsDevice.Viewport.Width - 20 && theBall.Y - right.Y <= right.Height)
            {
                Rebound(0);
                velocity += 0.4;
            }
            //bottom
            if (theBall.Y >= GraphicsDevice.Viewport.Height)
            {
                Rebound(3);
                velocity += 0.4;
            }

            //losing condition
            if (theBall.X < 0)
            {
                lost = true;
            }
            




            if(left.Y >= 0 || mouseState.Y >=0) left.Y = mouseState.Y;
            


            base.Update(gameTime);
        }
        protected void Rebound(int n)
        /*
         @params - 0 - rebounding after colliding on the right
         *         1 - after collision with the left   
         *         2 - with the top
         *         3 - with the bottom
         */
        
        {
            if (n == 0 || n == 1)
                theta = 2 * Math.PI - theta;
            //if (n == 1)
            //    theta = -2*Math.PI - Math.Atan(-velocityY / velocityX);
            if (n == 2 || n == 3)
                theta = Math.PI - theta;
            //if (n == 3)
            //    theta = -2*Math.PI + Math.Atan(-velocityY / velocityX);
            while (theta - 2 * Math.PI > 0)
                theta = theta - 2 * Math.PI;
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(leftRacquet, left, Color.White);
            spriteBatch.Draw(rightRacquet, right, Color.White);
            spriteBatch.Draw(ball, theBall, Color.White);


            if (lost == true)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                Vector2 textVector = new Vector2(400, 400);
               spriteBatch.DrawString(font, "YOU LOST!",textVector, Color.Red); 
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
