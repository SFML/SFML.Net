using System;
using SFML.System;

namespace SFML.Graphics
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Specialized shape representing a circle
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class CircleShape : Shape
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Default constructor
        /// </summary>
        ////////////////////////////////////////////////////////////
        public CircleShape() : this(0) { }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the shape with an initial radius
        /// </summary>
        /// <param name="radius">Radius of the shape</param>
        ////////////////////////////////////////////////////////////
        public CircleShape(float radius) : this(radius, 30) { }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the shape with an initial radius and point count
        /// </summary>
        /// <param name="radius">Radius of the shape</param>
        /// <param name="pointCount">Number of points of the shape</param>
        ////////////////////////////////////////////////////////////
        public CircleShape(float radius, uint pointCount)
        {
            Radius = radius;
            SetPointCount(pointCount);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the shape from another shape
        /// </summary>
        /// <param name="copy">Shape to copy</param>
        ////////////////////////////////////////////////////////////
        public CircleShape(CircleShape copy) : base(copy)
        {
            Radius = copy.Radius;
            SetPointCount(copy.GetPointCount());
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// The radius of the shape
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float Radius
        {
            get { return myRadius; }
            set { myRadius = value; Update(); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the total number of points of the circle
        /// </summary>
        /// <returns>The total point count</returns>
        ////////////////////////////////////////////////////////////
        public override uint GetPointCount() => myPointCount;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Set the number of points of the circle.
        /// The count must be greater than 2 to define a valid shape.
        /// </summary>
        /// <param name="count">New number of points of the circle</param>
        ////////////////////////////////////////////////////////////
        public void SetPointCount(uint count)
        {
            myPointCount = count;
            Update();
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the position of a point
        ///
        /// The returned point is in local coordinates, that is,
        /// the shape's transforms (position, rotation, scale) are
        /// not taken into account.
        /// The result is undefined if index is out of the valid range.
        /// </summary>
        /// <param name="index">Index of the point to get, in range [0 .. PointCount - 1]</param>
        /// <returns>index-th point of the shape</returns>
        ////////////////////////////////////////////////////////////
        public override Vector2f GetPoint(uint index)
        {
            float angle = (float)( index * 2 * Math.PI / myPointCount - Math.PI / 2 );
            float x = (float)Math.Cos(angle) * myRadius;
            float y = (float)Math.Sin(angle) * myRadius;

            return new Vector2f(myRadius + x, myRadius + y);
        }

        private float myRadius;
        private uint myPointCount;
    }
}
