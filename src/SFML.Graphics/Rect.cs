using System;
using System.Runtime.InteropServices;
using SFML.System;

namespace SFML.Graphics
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// IntRect is an utility class for manipulating 2D rectangles
    /// with integer coordinates
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct IntRect : IEquatable<IntRect>
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the rectangle from position and size
        /// </summary>
        /// <param name="position">Position of the top-left corner of the rectangle</param>
        /// <param name="size">Size of the rectangle</param>
        ////////////////////////////////////////////////////////////
        public IntRect(Vector2i position, Vector2i size)
        {
            Position = position;
            Size = size;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Check if a point is inside the rectangle's area
        /// </summary>
        /// <param name="point">Point to test</param>
        /// <returns>True if the point is inside</returns>
        ////////////////////////////////////////////////////////////
        public bool Contains(Vector2i point)
        {
            var minX = Math.Min(Left, Left + Width);
            var maxX = Math.Max(Left, Left + Width);
            var minY = Math.Min(Top, Top + Height);
            var maxY = Math.Max(Top, Top + Height);

            return
                (point.X >= minX) && (point.X < maxX) &&
                (point.Y >= minY) && (point.Y < maxY);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Check if a point is inside the rectangle's area
        /// </summary>
        /// <param name="point">Vector2 position of the point to test</param>
        /// <returns>True if the point is inside</returns>
        ////////////////////////////////////////////////////////////
        public bool Contains(Vector2u point) => Contains((Vector2i)point);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Check if a point is inside the rectangle's area
        /// </summary>
        /// <param name="point">Vector2 position of the point to test</param>
        /// <returns>True if the point is inside</returns>
        ////////////////////////////////////////////////////////////
        public bool Contains(Vector2f point) => Contains((Vector2i)point);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Check the intersection between two rectangles
        /// </summary>
        /// <param name="rect"> Rectangle to test</param>
        /// <returns>Intersection rectangle if intersecting, null otherwise</returns>
        ////////////////////////////////////////////////////////////
        public IntRect? FindIntersection(IntRect rect)
        {
            // Rectangles with negative dimensions are allowed, so we must handle them correctly

            // Compute the min and max of the first rectangle on both axes
            var r1MinX = Math.Min(Left, Left + Width);
            var r1MaxX = Math.Max(Left, Left + Width);
            var r1MinY = Math.Min(Top, Top + Height);
            var r1MaxY = Math.Max(Top, Top + Height);

            // Compute the min and max of the second rectangle on both axes
            var r2MinX = Math.Min(rect.Left, rect.Left + rect.Width);
            var r2MaxX = Math.Max(rect.Left, rect.Left + rect.Width);
            var r2MinY = Math.Min(rect.Top, rect.Top + rect.Height);
            var r2MaxY = Math.Max(rect.Top, rect.Top + rect.Height);

            // Compute the intersection boundaries
            var interLeft = Math.Max(r1MinX, r2MinX);
            var interTop = Math.Max(r1MinY, r2MinY);
            var interRight = Math.Min(r1MaxX, r2MaxX);
            var interBottom = Math.Min(r1MaxY, r2MaxY);

            // If the intersection is valid (positive non zero area), then there is an intersection
            if ((interLeft < interRight) && (interTop < interBottom))
            {
                return new IntRect((interLeft, interTop), (interRight - interLeft, interBottom - interTop));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Deconstructs an IntRect into a tuple of ints
        /// </summary>
        /// <param name="left">Left coordinate of the rectangle</param>
        /// <param name="top">Top coordinate of the rectangle</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        public void Deconstruct(out int left, out int top, out int width, out int height)
        {
            left = Position.X;
            top = Position.Y;
            width = Size.X;
            height = Size.Y;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => $"[IntRect] Position({Left}, {Top}) Size({Width}, {Height})";

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Compare rectangle and object and checks if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>Object and rectangle are equal</returns>
        ////////////////////////////////////////////////////////////
        public override bool Equals(object obj) => (obj is IntRect rect) && Equals(rect);

        ///////////////////////////////////////////////////////////
        /// <summary>
        /// Compare two rectangles and checks if they are equal
        /// </summary>
        /// <param name="other">Rectangle to check</param>
        /// <returns>Rectangles are equal</returns>
        ////////////////////////////////////////////////////////////
        public bool Equals(IntRect other) => (Left == other.Left) &&
                   (Top == other.Top) &&
                   (Width == other.Width) &&
                   (Height == other.Height);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override int GetHashCode() => unchecked((int)((uint)Left ^
                   (((uint)Top << 13) | ((uint)Top >> 19)) ^
                   (((uint)Width << 26) | ((uint)Width >> 6)) ^
                   (((uint)Height << 7) | ((uint)Height >> 25))));

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator == overload ; check rect equality
        /// </summary>
        /// <param name="r1">First rect</param>
        /// <param name="r2">Second rect</param>
        /// <returns>r1 == r2</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator ==(IntRect r1, IntRect r2) => r1.Equals(r2);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator != overload ; check rect inequality
        /// </summary>
        /// <param name="r1">First rect</param>
        /// <param name="r2">Second rect</param>
        /// <returns>r1 != r2</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator !=(IntRect r1, IntRect r2) => !r1.Equals(r2);

        /// <summary>
        /// Converts a tuple of ints to an IntRect
        /// </summary>
        /// <param name="tuple">The tuple to convert</param>
        public static implicit operator IntRect((int Left, int Top, int Width, int Height) tuple) => new IntRect((tuple.Left, tuple.Top), (tuple.Width, tuple.Height));

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Explicit casting to another rectangle type
        /// </summary>
        /// <param name="r">Rectangle being casted</param>
        /// <returns>Casting result</returns>
        ////////////////////////////////////////////////////////////
        public static explicit operator FloatRect(IntRect r) => new FloatRect(
            (r.Left, r.Top),
            (r.Width, r.Height));

        /// <summary>Left coordinate of the rectangle</summary>
        public int Left => Position.X;

        /// <summary>Top coordinate of the rectangle</summary>
        public int Top => Position.Y;

        /// <summary>Width of the rectangle</summary>
        public int Width => Size.X;

        /// <summary>Height of the rectangle</summary>
        public int Height => Size.Y;

        /// <summary>Position of the center of the rectangle</summary>
        public Vector2i Center => Position + (Size / 2);

        /// <summary>Position of the top-left corner of the rectangle</summary>
        public Vector2i Position;

        /// <summary>Size of the rectangle</summary>
        public Vector2i Size;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// FloatRect is an utility class for manipulating 2D rectangles
    /// with float coordinates
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct FloatRect : IEquatable<FloatRect>
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the rectangle from position and size
        /// </summary>
        /// <param name="position">Position of the top-left corner of the rectangle</param>
        /// <param name="size">Size of the rectangle</param>
        ////////////////////////////////////////////////////////////
        public FloatRect(Vector2f position, Vector2f size)
        {
            Position = position;
            Size = size;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Check if a point is inside the rectangle's area
        /// </summary>
        /// <param name="point">Point to test</param>
        /// <returns>True if the point is inside</returns>
        ////////////////////////////////////////////////////////////
        public bool Contains(Vector2f point)
        {
            var minX = Math.Min(Left, Left + Width);
            var maxX = Math.Max(Left, Left + Width);
            var minY = Math.Min(Top, Top + Height);
            var maxY = Math.Max(Top, Top + Height);

            return (point.X >= minX) && (point.X < maxX) && (point.Y >= minY) && (point.Y < maxY);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Check if a point is inside the rectangle's area
        /// </summary>
        /// <param name="point">Vector2 position of the point to test</param>
        /// <returns>True if the point is inside</returns>
        ////////////////////////////////////////////////////////////
        public bool Contains(Vector2i point) => Contains((Vector2f)point);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Check if a point is inside the rectangle's area
        /// </summary>
        /// <param name="point">Vector2 position of the point to test</param>
        /// <returns>True if the point is inside</returns>
        ////////////////////////////////////////////////////////////
        public bool Contains(Vector2u point) => Contains((Vector2f)point);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Check intersection between two rectangles
        /// </summary>
        /// <param name="rect"> Rectangle to test</param>
        /// <returns>True if rectangles overlap</returns>
        ////////////////////////////////////////////////////////////
        public FloatRect? FindIntersection(FloatRect rect)
        {
            // Rectangles with negative dimensions are allowed, so we must handle them correctly

            // Compute the min and max of the first rectangle on both axes
            var r1MinX = Math.Min(Left, Left + Width);
            var r1MaxX = Math.Max(Left, Left + Width);
            var r1MinY = Math.Min(Top, Top + Height);
            var r1MaxY = Math.Max(Top, Top + Height);

            // Compute the min and max of the second rectangle on both axes
            var r2MinX = Math.Min(rect.Left, rect.Left + rect.Width);
            var r2MaxX = Math.Max(rect.Left, rect.Left + rect.Width);
            var r2MinY = Math.Min(rect.Top, rect.Top + rect.Height);
            var r2MaxY = Math.Max(rect.Top, rect.Top + rect.Height);

            // Compute the intersection boundaries
            var interLeft = Math.Max(r1MinX, r2MinX);
            var interTop = Math.Max(r1MinY, r2MinY);
            var interRight = Math.Min(r1MaxX, r2MaxX);
            var interBottom = Math.Min(r1MaxY, r2MaxY);

            // If the intersection is valid (positive non zero area), then there is an intersection
            if ((interLeft < interRight) && (interTop < interBottom))
            {
                return new FloatRect((interLeft, interTop), (interRight - interLeft, interBottom - interTop));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Deconstructs a FloatRect into a tuple of floats
        /// </summary>
        /// <param name="left">Left coordinate of the rectangle</param>
        /// <param name="top">Top coordinate of the rectangle</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        public void Deconstruct(out float left, out float top, out float width, out float height)
        {
            left = Left;
            top = Top;
            width = Width;
            height = Height;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => $"[FloatRect] Position({Left}, {Top})  Size({Width}, {Height})";

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Compare rectangle and object and checks if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>Object and rectangle are equal</returns>
        ////////////////////////////////////////////////////////////
        public override bool Equals(object obj) => (obj is FloatRect rect) && Equals(rect);

        ///////////////////////////////////////////////////////////
        /// <summary>
        /// Compare two rectangles and checks if they are equal
        /// </summary>
        /// <param name="other">Rectangle to check</param>
        /// <returns>Rectangles are equal</returns>
        ////////////////////////////////////////////////////////////
        public bool Equals(FloatRect other) =>
            Position == other.Position &&
            Size == other.Size;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override int GetHashCode() => unchecked((int)((uint)Left ^
                   (((uint)Top << 13) | ((uint)Top >> 19)) ^
                   (((uint)Width << 26) | ((uint)Width >> 6)) ^
                   (((uint)Height << 7) | ((uint)Height >> 25))));

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator == overload ; check rect equality
        /// </summary>
        /// <param name="r1">First rect</param>
        /// <param name="r2">Second rect</param>
        /// <returns>r1 == r2</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator ==(FloatRect r1, FloatRect r2) => r1.Equals(r2);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator != overload ; check rect inequality
        /// </summary>
        /// <param name="r1">First rect</param>
        /// <param name="r2">Second rect</param>
        /// <returns>r1 != r2</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator !=(FloatRect r1, FloatRect r2) => !r1.Equals(r2);

        /// <summary>
        /// Converts a tuple of floats to a FloatRect
        /// </summary>
        /// <param name="tuple">The tuple to convert</param>
        public static implicit operator FloatRect((float Left, float Top, float Width, float Height) tuple) => new FloatRect((tuple.Left, tuple.Top), (tuple.Width, tuple.Height));

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Explicit casting to another rectangle type
        /// </summary>
        /// <param name="r">Rectangle being casted</param>
        /// <returns>Casting result</returns>
        ////////////////////////////////////////////////////////////
        public static explicit operator IntRect(FloatRect r) => new IntRect(
            ((int)r.Left, (int)r.Top),
            ((int)r.Width, (int)r.Height));

        /// <summary>Left coordinate of the rectangle</summary>
        public float Left => Position.X;

        /// <summary>Top coordinate of the rectangle</summary>
        public float Top => Position.Y;

        /// <summary>Width of the rectangle</summary>
        public float Width => Size.X;

        /// <summary>Height of the rectangle</summary>
        public float Height => Size.Y;

        /// <summary>Position of the center of the rectangle</summary>
        public Vector2f Center => Position + (Size / 2f);

        /// <summary>Position of the top-left corner of the rectangle</summary>
        public Vector2f Position;

        /// <summary>Size of the rectangle</summary>
        public Vector2f Size;
    }
}
