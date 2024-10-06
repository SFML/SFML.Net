using System;
using System.Runtime.InteropServices;
using System.Security;
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
        public RectangleShape(Vector2f size) => Size = size;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the shape from another shape
        /// </summary>
        /// <param name="copy">Shape to copy</param>
        ////////////////////////////////////////////////////////////
        public RectangleShape(RectangleShape copy) :
            base(copy) => Size = copy.Size;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// The size of the rectangle
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Vector2f Size
        {
            get => _size;
            set
            {
                _size = value;
                Update();
            }
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
                    return new Vector2f(_size.X, 0);
                case 2:
                    return new Vector2f(_size.X, _size.Y);
                case 3:
                    return new Vector2f(0, _size.Y);
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the geometric center of the rectangle
        ///
        /// The returned point is in local coordinates, that is,
        /// the shape's transforms (position, rotation, scale) are
        /// not taken into account.
        /// 
        /// </summary>
        /// <returns>The geometric center of the shape</returns>
        ////////////////////////////////////////////////////////////
        public override Vector2f GetGeometricCenter() => sfRectangleShape_getGeometricCenter(CPointer);

        private Vector2f _size;

        #region Imports
        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f sfRectangleShape_getGeometricCenter(IntPtr cPointer);
        #endregion
    }
}
