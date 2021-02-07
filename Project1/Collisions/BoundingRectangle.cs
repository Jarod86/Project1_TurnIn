using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Project1.Collisions
{
    public class BoundingRectangle
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;
        public float Left => X;
        public float Right => X + Height;
        public float Top => Y;
        public float Bottom => Y + Height;

        /// <summary>
        /// initializes the rectangle 
        /// </summary>
        /// <param name="position">the starting position</param>
        /// <param name="width">the width of the rectangle</param>
        /// <param name="height">the height of the rectangle</param>
        public BoundingRectangle(Vector2 position, float width, float height)
        {
            X = position.X;
            Y = position.Y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// checks to see if it collides with a circle
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool CollidesWith(BoundingCircle other)
        {
            return CollisionHelper.Collides(other, this);
        }
    }
}
