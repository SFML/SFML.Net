using System;
using System.Runtime.InteropServices;

namespace SFML.System
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Vector3f is an utility class for manipulating 3 dimensional
    /// vectors with float components
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3f : IEquatable<Vector3f>
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the vector from its coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        ////////////////////////////////////////////////////////////
        public Vector3f(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator - overload ; returns the opposite of a vector
        /// </summary>
        /// <param name="v">Vector to negate</param>
        /// <returns>-v</returns>
        ////////////////////////////////////////////////////////////
        public static Vector3f operator -(Vector3f v) => new Vector3f(-v.X, -v.Y, -v.Z);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator - overload ; subtracts two vectors
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>v1 - v2</returns>
        ////////////////////////////////////////////////////////////
        public static Vector3f operator -(Vector3f v1, Vector3f v2) => new Vector3f(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator + overload ; add two vectors
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>v1 + v2</returns>
        ////////////////////////////////////////////////////////////
        public static Vector3f operator +(Vector3f v1, Vector3f v2) => new Vector3f(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator * overload ; multiply a vector by a scalar value
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="x">Scalar value</param>
        /// <returns>v * x</returns>
        ////////////////////////////////////////////////////////////
        public static Vector3f operator *(Vector3f v, float x) => new Vector3f(v.X * x, v.Y * x, v.Z * x);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator * overload ; multiply a scalar value by a vector
        /// </summary>
        /// <param name="x">Scalar value</param>
        /// <param name="v">Vector</param>
        /// <returns>x * v</returns>
        ////////////////////////////////////////////////////////////
        public static Vector3f operator *(float x, Vector3f v) => new Vector3f(v.X * x, v.Y * x, v.Z * x);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator / overload ; divide a vector by a scalar value
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="x">Scalar value</param>
        /// <returns>v / x</returns>
        ////////////////////////////////////////////////////////////
        public static Vector3f operator /(Vector3f v, float x) => new Vector3f(v.X / x, v.Y / x, v.Z / x);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator == overload ; check vector equality
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>v1 == v2</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator ==(Vector3f v1, Vector3f v2) => v1.Equals(v2);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Operator != overload ; check vector inequality
        /// </summary>
        /// <param name="v1">First vector</param>
        /// <param name="v2">Second vector</param>
        /// <returns>v1 != v2</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator !=(Vector3f v1, Vector3f v2) => !v1.Equals(v2);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => $"[Vector3f] X({X}) Y({Y}) Z({Z})";

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Compare vector and object and checks if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>Object and vector are equal</returns>
        ////////////////////////////////////////////////////////////
        public override bool Equals(object obj) => ( obj is Vector3f ) && Equals((Vector3f)obj);

        ///////////////////////////////////////////////////////////
        /// <summary>
        /// Compare two vectors and checks if they are equal
        /// </summary>
        /// <param name="other">Vector to check</param>
        /// <returns>Vectors are equal</returns>
        ////////////////////////////////////////////////////////////
        public bool Equals(Vector3f other) => ( X == other.X ) && ( Y == other.Y ) && ( Z == other.Z );

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();

        /// <summary>X (horizontal) component of the vector</summary>
        public float X;

        /// <summary>Y (vertical) component of the vector</summary>
        public float Y;

        /// <summary>Z (depth) component of the vector</summary>
        public float Z;
    }
}
