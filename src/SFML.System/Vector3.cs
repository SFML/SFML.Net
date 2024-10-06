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
        /// Length of the vector
        /// <para/>
        /// If you are not interested in the actual length, but only in comparisons, consider using <see cref="LengthSquared"/>.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float Length => (float)Math.Sqrt((X * X) + (Y * Y) + (Z * Z));

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Square of vector's length
        /// <para/>
        /// Suitable for comparisons, more efficient than <see cref="Length"/>
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float LengthSquared => Dot(this);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Vector with same direction but length 1
        /// <para/>
        /// <see langword="this"/> should not be a zero vector
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Vector3f Normalized() => this / Length;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Dot product of two 3D vectors.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float Dot(Vector3f rhs) => (X * rhs.X) + (Y * rhs.Y) + (Z * rhs.Z);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Cross product of two 3D vectors
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Vector3f Cross(Vector3f rhs) => new Vector3f((Y * rhs.Z) - (Z * rhs.Y), (Z * rhs.X) - (X * rhs.Z), (X * rhs.Y) - (Y * rhs.X));

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Component-wise multiplication of <see langword="this"/> and <paramref name="rhs"/>
        /// <para/>
        /// Computes <code>(this.X * rhs.X, this.Y * rhs.Y, this.Z * rhs.Z)</code>
        /// <para/>
        /// Scaling is the most common use case for component-wise multiplication/division.
        /// This operation is also known as the Hadamard or Schur product.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Vector3f ComponentWiseMul(Vector3f rhs) => new Vector3f(X * rhs.X, Y * rhs.Y, Z * rhs.Z);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Component-wise division of <see langword="this"/> and <paramref name="rhs"/>
        /// <para/>
        /// Computes <code>(this.X / rhs.X, this.Y / rhs.Y, this.Z * rhs.Z)</code>
        /// <para/>
        /// Scaling is the most common use case for component-wise multiplication/division.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Vector3f ComponentWiseDiv(Vector3f rhs) => new Vector3f(X / rhs.X, Y / rhs.Y, Z / rhs.Z);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Deconstructs a Vector3f into a tuple of floats
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        ////////////////////////////////////////////////////////////
        public void Deconstruct(out float x, out float y, out float z)
        {
            x = X;
            y = Y;
            z = Z;
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
        public override bool Equals(object obj) => (obj is Vector3f vec) && Equals(vec);

        ///////////////////////////////////////////////////////////
        /// <summary>
        /// Compare two vectors and checks if they are equal
        /// </summary>
        /// <param name="other">Vector to check</param>
        /// <returns>Vectors are equal</returns>
        ////////////////////////////////////////////////////////////
        public bool Equals(Vector3f other) => (X == other.X) && (Y == other.Y) && (Z == other.Z);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Converts a tuple of floats to a Vector3f
        /// </summary>
        /// <param name="tuple">The tuple to convert</param>
        ////////////////////////////////////////////////////////////
        public static implicit operator Vector3f((float X, float Y, float Z) tuple) => new Vector3f(tuple.X, tuple.Y, tuple.Z);

        /// <summary>X (horizontal) component of the vector</summary>
        public float X;

        /// <summary>Y (vertical) component of the vector</summary>
        public float Y;

        /// <summary>Z (depth) component of the vector</summary>
        public float Z;
    }
}
