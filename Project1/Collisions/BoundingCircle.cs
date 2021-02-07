using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Project1.Collisions
{
    public class BoundingCircle
    {
        /// <summary>
        /// center of the circle
        /// </summary>
        public Vector2 Center;

        /// <summary>
        /// Radius of the circle
        /// </summary>
        public float Radius;

        /// <summary>
        /// initalize the circle
        /// </summary>
        /// <param name="center">the center</param>
        /// <param name="radius">the radius</param>
        public BoundingCircle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// check to see if the circle collides with a rectangle
        /// </summary>
        /// <param name="other">the Bounding rectangle</param>
        /// <returns></returns>
        public bool CollidesWith(BoundingRectangle other)
        {
            return CollisionHelper.Collides(this, other);
        }
    }
}
