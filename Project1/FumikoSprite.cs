using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;
using Project1.Screens;


namespace Project1
{
    public enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,

    }
    public class FumikoSprite
    {
        private double animationTimer;
        private short animationFrame = 0;
        private GamePadState gamePadState;
        private KeyboardState keyboardState;
        private Texture2D texture;
        private bool idol = true;
        public Body body;

        /// <summary>
        /// the color changes if the actor is hit by a ball
        /// </summary>
        public Color Color { get; set; } = Color.White;


        /// <summary>
        /// the direction of Fumiko
        /// </summary>
        public Direction Direction;






        public bool Colliding { get; protected set; }




        public FumikoSprite(Body body)
        {
            this.body = body;
            this.body.OnCollision += CollisionHandler;
        }

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
            Colliding = false;
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;



            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {

                body.Position += new Vector2(-1, 0);
                Direction = Direction.Left;
                idol = false;
                //flipped = true;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                body.Position += new Vector2(1, 0);
                Direction = Direction.Right;
                idol = false;
            }




            //checks if the actor got to the other side of the viewport






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
                if (animationTimer > .3)
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
            spriteBatch.Draw(texture, body.Position, source, Color);

        }
        bool CollisionHandler(Fixture fixture, Fixture other, Contact contact)
        {
            Colliding = true;


            return true;
        }
    }
}

