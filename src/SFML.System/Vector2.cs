using System;
using System.Runtime.InteropServices;

namespace SFML.System
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Vector2f is an utility class for manipulating 2 dimensional
    /// vectors with float components
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2f : IEquatable<Vector2f>
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the vector from its coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        ////////////////////////////////////////////////////////////
        public Vector2f(float x, float y)
        {
            X = x;
            Y = y;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator - overload ; returns the opposite of a vector
        /// </summary>
        /// <param name="v">Vector to negate</param>
        /// <returns>-v</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2f operator -(Vector2f v) => new Vector2f(-v.X, -v.Y);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator - overload ; subtracts two vectors
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>v1 - v2</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2f operator -(Vector2f v1, Vector2f v2) => new Vector2f(v1.X - v2.X, v1.Y - v2.Y);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator + overload ; add two vectors
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>v1 + v2</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2f operator +(Vector2f v1, Vector2f v2) => new Vector2f(v1.X + v2.X, v1.Y + v2.Y);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator * overload ; multiply a vector by a scalar value
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="x">Scalar value</param>
        /// <returns>v * x</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2f operator *(Vector2f v, float x) => new Vector2f(v.X * x, v.Y * x);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator * overload ; multiply a scalar value by a vector
        /// </summary>
        /// <param name="x">Scalar value</param>
        /// <param name="v">Vector</param>
        /// <returns>x * v</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2f operator *(float x, Vector2f v) => new Vector2f(v.X * x, v.Y * x);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator / overload ; divide a vector by a scalar value
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="x">Scalar value</param>
        /// <returns>v / x</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2f operator /(Vector2f v, float x) => new Vector2f(v.X / x, v.Y / x);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator == overload ; check vector equality
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>v1 == v2</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator ==(Vector2f v1, Vector2f v2) => v1.Equals(v2);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator != overload ; check vector inequality
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>v1 != v2</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator !=(Vector2f v1, Vector2f v2) => !v1.Equals(v2);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => $"[Vector2f] X({X}) Y({Y})";

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Compare vector and object and checks if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>Object and vector are equal</returns>
        ////////////////////////////////////////////////////////////
        public override bool Equals(object obj) => ( obj is Vector2f ) && Equals((Vector2f)obj);

        ///////////////////////////////////////////////////////////
        /// <summary>
        /// Compare two vectors and checks if they are equal
        /// </summary>
        /// <param name="other">Vector to check</param>
        /// <returns>Vectors are equal</returns>
        ////////////////////////////////////////////////////////////
        public bool Equals(Vector2f other) => ( X == other.X ) && ( Y == other.Y );

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Explicit casting to another vector type
        /// </summary>
        /// <param name="v">Vector being casted</param>
        /// <returns>Casting result</returns>
        ////////////////////////////////////////////////////////////
        public static explicit operator Vector2i(Vector2f v) => new Vector2i((int)v.X, (int)v.Y);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Explicit casting to another vector type
        /// </summary>
        /// <param name="v">Vector being casted</param>
        /// <returns>Casting result</returns>
        ////////////////////////////////////////////////////////////
        public static explicit operator Vector2u(Vector2f v) => new Vector2u((uint)v.X, (uint)v.Y);

        /// <summary>X (horizontal) component of the vector</summary>
        public float X;

        /// <summary>Y (vertical) component of the vector</summary>
        public float Y;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Vector2i is an utility class for manipulating 2 dimensional
    /// vectors with integer components
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2i : IEquatable<Vector2i>
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the vector from its coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        ////////////////////////////////////////////////////////////
        public Vector2i(int x, int y)
        {
            X = x;
            Y = y;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator - overload ; returns the opposite of a vector
        /// </summary>
        /// <param name="v">Vector to negate</param>
        /// <returns>-v</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2i operator -(Vector2i v) => new Vector2i(-v.X, -v.Y);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator - overload ; subtracts two vectors
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>v1 - v2</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2i operator -(Vector2i v1, Vector2i v2) => new Vector2i(v1.X - v2.X, v1.Y - v2.Y);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator + overload ; add two vectors
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>v1 + v2</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2i operator +(Vector2i v1, Vector2i v2) => new Vector2i(v1.X + v2.X, v1.Y + v2.Y);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator * overload ; multiply a vector by a scalar value
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="x">Scalar value</param>
        /// <returns>v * x</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2i operator *(Vector2i v, int x) => new Vector2i(v.X * x, v.Y * x);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator * overload ; multiply a scalar value by a vector
        /// </summary>
        /// <param name="x">Scalar value</param>
        /// <param name="v">Vector</param>
        /// <returns>x * v</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2i operator *(int x, Vector2i v) => new Vector2i(v.X * x, v.Y * x);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator / overload ; divide a vector by a scalar value
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="x">Scalar value</param>
        /// <returns>v / x</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2i operator /(Vector2i v, int x) => new Vector2i(v.X / x, v.Y / x);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator == overload ; check vector equality
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>v1 == v2</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator ==(Vector2i v1, Vector2i v2) => v1.Equals(v2);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator != overload ; check vector inequality
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>v1 != v2</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator !=(Vector2i v1, Vector2i v2) => !v1.Equals(v2);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => $"[Vector2i] X({X}) Y({Y})";

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Compare vector and object and checks if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>Object and vector are equal</returns>
        ////////////////////////////////////////////////////////////
        public override bool Equals(object obj) => ( obj is Vector2i ) && Equals((Vector2i)obj);

        ///////////////////////////////////////////////////////////
        /// <summary>
        /// Compare two vectors and checks if they are equal
        /// </summary>
        /// <param name="other">Vector to check</param>
        /// <returns>Vectors are equal</returns>
        ////////////////////////////////////////////////////////////
        public bool Equals(Vector2i other) => ( X == other.X ) && ( Y == other.Y );

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Explicit casting to another vector type
        /// </summary>
        /// <param name="v">Vector being casted</param>
        /// <returns>Casting result</returns>
        ////////////////////////////////////////////////////////////
        public static explicit operator Vector2f(Vector2i v) => new Vector2f(v.X, v.Y);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Explicit casting to another vector type
        /// </summary>
        /// <param name="v">Vector being casted</param>
        /// <returns>Casting result</returns>
        ////////////////////////////////////////////////////////////
        public static explicit operator Vector2u(Vector2i v) => new Vector2u((uint)v.X, (uint)v.Y);

        /// <summary>X (horizontal) component of the vector</summary>
        public int X;

        /// <summary>Y (vertical) component of the vector</summary>
        public int Y;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Vector2u is an utility class for manipulating 2 dimensional
    /// vectors with unsigned integer components
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2u : IEquatable<Vector2u>
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the vector from its coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        ////////////////////////////////////////////////////////////
        public Vector2u(uint x, uint y)
        {
            X = x;
            Y = y;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator - overload ; subtracts two vectors
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>v1 - v2</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2u operator -(Vector2u v1, Vector2u v2) => new Vector2u(v1.X - v2.X, v1.Y - v2.Y);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator + overload ; add two vectors
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>v1 + v2</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2u operator +(Vector2u v1, Vector2u v2) => new Vector2u(v1.X + v2.X, v1.Y + v2.Y);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator * overload ; multiply a vector by a scalar value
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="x">Scalar value</param>
        /// <returns>v * x</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2u operator *(Vector2u v, uint x) => new Vector2u(v.X * x, v.Y * x);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator * overload ; multiply a scalar value by a vector
        /// </summary>
        /// <param name="x">Scalar value</param>
        /// <param name="v">Vector</param>
        /// <returns>x * v</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2u operator *(uint x, Vector2u v) => new Vector2u(v.X * x, v.Y * x);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator / overload ; divide a vector by a scalar value
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="x">Scalar value</param>
        /// <returns>v / x</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2u operator /(Vector2u v, uint x) => new Vector2u(v.X / x, v.Y / x);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator == overload ; check vector equality
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>v1 == v2</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator ==(Vector2u v1, Vector2u v2) => v1.Equals(v2);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator != overload ; check vector inequality
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>v1 != v2</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator !=(Vector2u v1, Vector2u v2) => !v1.Equals(v2);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => $"[Vector2u] X({X}) Y({Y})";

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Compare vector and object and checks if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>Object and vector are equal</returns>
        ////////////////////////////////////////////////////////////
        public override bool Equals(object obj) => ( obj is Vector2u ) && Equals((Vector2u)obj);

        ///////////////////////////////////////////////////////////
        /// <summary>
        /// Compare two vectors and checks if they are equal
        /// </summary>
        /// <param name="other">Vector to check</param>
        /// <returns>Vectors are equal</returns>
        ////////////////////////////////////////////////////////////
        public bool Equals(Vector2u other) => ( X == other.X ) && ( Y == other.Y );

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Explicit casting to another vector type
        /// </summary>
        /// <param name="v">Vector being casted</param>
        /// <returns>Casting result</returns>
        ////////////////////////////////////////////////////////////
        public static explicit operator Vector2i(Vector2u v) => new Vector2i((int)v.X, (int)v.Y);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Explicit casting to another vector type
        /// </summary>
        /// <param name="v">Vector being casted</param>
        /// <returns>Casting result</returns>
        ////////////////////////////////////////////////////////////
        public static explicit operator Vector2f(Vector2u v) => new Vector2f(v.X, v.Y);

        /// <summary>X (horizontal) component of the vector</summary>
        public uint X;

        /// <summary>Y (vertical) component of the vector</summary>
        public uint Y;
    }
}
