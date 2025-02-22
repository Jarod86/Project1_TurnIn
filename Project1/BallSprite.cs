﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using tainicom.Aether.Physics2D.Dynamics;
using tainicom.Aether.Physics2D.Dynamics.Contacts;

namespace Project1
{
    public class BallSprite
    {
        Texture2D texture;
        float radius;
        float scale;
        Vector2 origin;
        Body body;

        /// <summary>
        /// A boolean indicating if this ball is colliding with another
        /// </summary>
        public bool Colliding { get; protected set; }

        public BallSprite(float radius, Body body)
        {
            this.body = body;
            this.radius = radius;
            scale = 1;
            origin = new Vector2(16, 16);
            this.body.OnCollision += CollisionHandler;
        }

        /// <summary>
        /// Loads the ball's texture
        /// </summary>
        /// <param name="contentManager">The content manager to use</param>
        public void LoadContent(ContentManager contentManager)
        {
            texture = contentManager.Load<Texture2D>("spikedball");
        }

        /// <summary>
        /// Updates the ball
        /// </summary>
        /// <param name="gameTime">An object representing time in the game</param>
        public void Update(GameTime gameTime)
        {
            // Clear the colliding flag 
            Colliding = false;
        }

        /// <summary>
        /// Draws the ball using the provided spritebatch
        /// </summary>
        /// <param name="gameTime">an object representing time in the game</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Use Green for visual collision indication
            Color color = (Colliding) ? Color.Green : Color.White;
            spriteBatch.Draw(texture, body.Position, null, color, body.Rotation, origin, scale, SpriteEffects.None, 0);
        }

        bool CollisionHandler(Fixture fixture, Fixture other, Contact contact)
        {
            Colliding = true;
            return true;
        }

    }
}
