using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Collisions;

namespace Project1
{
    public class Ranger86Game : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private FumikoSprite actor;

        private BallSprite[] balls;

        /// <summary>
        /// A game where you have to avoid the spikedballs and get to the other end
        /// </summary>
        public Ranger86Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.Title = "Ranger86";
        }

        /// <summary>
        /// Constructs the game
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            actor = new FumikoSprite() { Direction = Direction.Right};
            balls = new BallSprite[]
            {
                new BallSprite(){ BallPosition = new Vector2(100,100)},
                new BallSprite(){ BallPosition = new Vector2(400,400)},
                new BallSprite(){ BallPosition = new Vector2(300,300)},
                new BallSprite(){ BallPosition = new Vector2(200,200)},
                new BallSprite(){ BallPosition = new Vector2(100,200)},
                new BallSprite(){ BallPosition = new Vector2(50,50)},
                new BallSprite(){ BallPosition = new Vector2(500,300)},
                new BallSprite(){ BallPosition = new Vector2(350,225)}
            };
            base.Initialize();
        }

        /// <summary>
        /// loads game content
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);


            // TODO: use this.Content to load your game content here
            actor.LoadContent(Content);
            foreach (var ball in balls) ball.LoadContent(Content);
        }

        /// <summary>
        /// Updates the game world
        /// </summary>
        /// <param name="gameTime">the game time</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            actor.Update(gameTime);
            //bounces the ball off the edges of the viewport
            foreach (var ball in balls)
            {
                ball.Update(gameTime);
                if (ball.BallPosition.X < GraphicsDevice.Viewport.X || ball.BallPosition.X > GraphicsDevice.Viewport.Width - 32)
                {
                    ball.ballVelocity.X *= -1;
                }
                if (ball.BallPosition.Y < GraphicsDevice.Viewport.Y || ball.BallPosition.Y > GraphicsDevice.Viewport.Height - 32)
                {
                    ball.ballVelocity.Y *= -1;
                }
            }

            //checks if a ball collides with the actor
            actor.Color = Color.White;
            foreach(var ball in balls)
            {
                if(ball.Bounds.CollidesWith(actor.Bounds))
                {
                    actor.Color = Color.Red;
                    System.Windows.Forms.MessageBox.Show("You have Lost :(");
                    Exit();
                    
                }
            }
            //checks if the actor got to the other side of the viewport
            if(actor.position.X > GraphicsDevice.Viewport.Width - 24)
            {
                System.Windows.Forms.MessageBox.Show("You have Won :)");
                Exit();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game world
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            actor.Draw(gameTime, spriteBatch);
            foreach (var ball in balls) ball.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
