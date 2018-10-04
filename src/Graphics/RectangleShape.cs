using SFML.System;

namespace SFML.Graphics
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Specialized shape representing a rectangle
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class RectangleShape : Shape
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Default constructor
        /// </summary>
        ////////////////////////////////////////////////////////////
        public RectangleShape() :
            this(new Vector2f(0, 0))
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the shape with an initial size
        /// </summary>
        /// <param name="size">Size of the shape</param>
        ////////////////////////////////////////////////////////////
        public RectangleShape(Vector2f size)
        {
            Size = size;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the shape from another shape
        /// </summary>
        /// <param name="copy">Shape to copy</param>
        ////////////////////////////////////////////////////////////
        public RectangleShape(RectangleShape copy) :
            base(copy)
        {
            Size = copy.Size;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// The size of the rectangle
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Vector2f Size
        {
            get { return mySize; }
            set { mySize = value; Update(); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the total number of points of the rectangle.
        /// </summary>
        /// <returns>The total point count. For rectangle shapes,
        /// this number is always 4.</returns>
        ////////////////////////////////////////////////////////////
        public override uint GetPointCount() => 4;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the position of a point
        ///
        /// The returned point is in local coordinates, that is,
        /// the shape's transforms (position, rotation, scale) are
        /// not taken into account.
        /// The result is undefined if index is out of the valid range.
        /// </summary>
        /// <param name="index">Index of the point to get, in range [0 .. 3]</param>
        /// <returns>index-th point of the shape</returns>
        ////////////////////////////////////////////////////////////
        public override Vector2f GetPoint(uint index)
        {
            switch (index)
            {
                default:
                case 0:
                    return new Vector2f(0, 0);
                case 1:
                    return new Vector2f(mySize.X, 0);
                case 2:
                    return new Vector2f(mySize.X, mySize.Y);
                case 3:
                    return new Vector2f(0, mySize.Y);
            }
        }

        private Vector2f mySize;
    }
}
