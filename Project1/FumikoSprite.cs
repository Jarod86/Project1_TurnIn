using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Project1.Collisions;

namespace Project1
{
    public enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,
        
    }

    /// <summary>
    /// class representing the Fumiko sprite
    /// </summary>
    public class FumikoSprite
    {
        
        private double animationTimer;
        private short animationFrame = 0;
        private GamePadState gamePadState;
        private KeyboardState keyboardState;
        private Texture2D texture;
        private bool idol = true;

        /// <summary>
        /// the color changes if the actor is hit by a ball
        /// </summary>
        public Color Color { get; set; } = Color.White;


        /// <summary>
        /// the direction of Fumiko
        /// </summary>
        public Direction Direction;

        
        /// <summary>
        /// position of the sprite
        /// </summary>
        public Vector2 position = new Vector2(10, 225);

        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(10-6, 225-8), 12, 16);

        /// <summary>
        /// the bounding volume in our sprite
        /// </summary>
        public BoundingRectangle Bounds => bounds;


        /// <summary>
        /// loads the Fumiko sprite texture
        /// </summary>
        /// <param name="content">contentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Fumiko");

        }

        /// <summary>
        /// updates the Fumiko sprite
        /// </summary>
        /// <param name="gameTime">the game time</param>
        public void Update(GameTime gameTime)
        {
            gamePadState = GamePad.GetState(0);
            keyboardState = Keyboard.GetState();

            // Apply the gamepad movement with inverted Y axis
            position += gamePadState.ThumbSticks.Left * new Vector2(1, -1);
     

            // Apply keyboard movement
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                position += new Vector2(0, -1);
                Direction = Direction.Up;
                idol = false;
            }
            if(keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                position += new Vector2(0, 1);
                Direction = Direction.Down;
                idol = false;
            }
            if(keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                position += new Vector2(-1, 0);
                Direction = Direction.Left;
                idol = false;
                //flipped = true;
            }
            if(keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                position += new Vector2(1, 0);
                Direction = Direction.Right;
                idol = false;
            }

            bounds.X = position.X-6;
            bounds.Y = position.Y-8;

            
        }


        /// <summary>
        /// draws the fumiko sprite
        /// </summary>
        /// <param name="gameTime">the game time</param>
        /// <param name="spriteBatch">the sprite batch</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //update animation timer
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            //Update Animation frame
            if (!idol)
            {
                if (animationTimer > 0.3)
                {
                    animationFrame++;
                    if (animationFrame > 2) animationFrame = 0;
                    animationTimer -= 0.3;
                    idol = true;
                }
            }
            else
            {
                animationFrame = 0;
            }

            var source = new Rectangle(animationFrame * 24, (int)Direction * 32, 24, 32);
            spriteBatch.Draw(texture, position, source, Color);

        }
    }
}
