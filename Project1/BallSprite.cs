using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Project1.Collisions;

namespace Project1
{

    public class BallSprite
    {
        /// <summary>
        /// the starting location of the ball
        /// </summary>
        public Vector2 BallPosition;

        /// <summary>
        /// vellocity of the sprite ball
        /// </summary>
        public Vector2 ballVelocity;

        private BoundingCircle bounds;

        /// <summary>
        /// the collision detection shape
        /// </summary>
        public BoundingCircle Bounds => bounds;

        private Texture2D texture;
        private System.Random random = new System.Random();

        /// <summary>
        /// loads the ball texture and sets the velocity
        /// </summary>
        /// <param name="content">the content manager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("spikedball");
            ballVelocity = new Vector2((float)random.NextDouble(), (float)random.NextDouble());
            ballVelocity.Normalize();
            ballVelocity *= 100;
            bounds = new BoundingCircle(BallPosition + new Vector2(16,16), 16);
        }

        /// <summary>
        /// updates the balls position
        /// </summary>
        /// <param name="gameTime">the game time</param>
        public void Update(GameTime gameTime)
        {
            BallPosition += ballVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            bounds.Center.X = BallPosition.X +4;
            bounds.Center.Y = BallPosition.Y +4;

        }

        /// <summary>
        /// draws the ball sprite
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, BallPosition, Color.White);
        }
    }
}
