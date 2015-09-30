using System;
using System.Runtime.InteropServices;

namespace SFML
{
    namespace System
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Vector2 is an generic utility class for manipulating
        /// 2-dimensional vectors
        /// </summary>
        ////////////////////////////////////////////////////////////
        [StructLayout(LayoutKind.Sequential)]
        public struct Vector2<T> : IEquatable<Vector2<T>>
        {
            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the vector from its coordinates
            /// </summary>
            /// <param name="x">X coordinate</param>
            /// <param name="y">Y coordinate</param>
            ////////////////////////////////////////////////////////////
            public Vector2(T x, T y)
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
            public static Vector2<T> operator -(Vector2<T> v)
            {
                return new Vector2<T>(Operator<T>.Negate(v.X), Operator<T>.Negate(v.Y));
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator - overload ; subtracts two vectors
            /// </summary>
            /// <param name="v1">First vector</param>
            /// <param name="v2">Second vector</param>
            /// <returns>v1 - v2</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2<T> operator -(Vector2<T> v1, Vector2<T> v2)
            {
                return new Vector2<T>(Operator<T>.Subtract(v1.X, v2.X), Operator<T>.Subtract(v1.Y, v2.Y));
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator + overload ; add two vectors
            /// </summary>
            /// <param name="v1">First vector</param>
            /// <param name="v2">Second vector</param>
            /// <returns>v1 + v2</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2<T> operator +(Vector2<T> v1, Vector2<T> v2)
            {
                return new Vector2<T>(Operator<T>.Add(v1.X, v2.X), Operator<T>.Add(v1.Y, v2.Y));
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator * overload ; multiply a vector by a scalar value
            /// </summary>
            /// <param name="v">Vector</param>
            /// <param name="x">Scalar value</param>
            /// <returns>v * x</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2<T> operator *(Vector2<T> v, T x)
            {
                return new Vector2<T>(Operator<T>.Multiply(v.X, x), Operator<T>.Multiply(v.Y, x));
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator * overload ; multiply a scalar value by a vector
            /// </summary>
            /// <param name="x">Scalar value</param>
            /// <param name="v">Vector</param>
            /// <returns>x * v</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2<T> operator *(T x, Vector2<T> v)
            {
                return new Vector2<T>(Operator<T>.Multiply(v.X, x), Operator<T>.Multiply(v.Y, x));
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator / overload ; divide a vector by a scalar value
            /// </summary>
            /// <param name="v">Vector</param>
            /// <param name="x">Scalar value</param>
            /// <returns>v / x</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2<T> operator /(Vector2<T> v, T x)
            {
                return new Vector2<T>(Operator<T>.Divide(v.X, x), Operator<T>.Divide(v.Y, x));
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator == overload ; check vector equality
            /// </summary>
            /// <param name="v1">First vector</param>
            /// <param name="v2">Second vector</param>
            /// <returns>v1 == v2</returns>
            ////////////////////////////////////////////////////////////
            public static bool operator ==(Vector2<T> v1, Vector2<T> v2)
            {
                return v1.Equals(v2);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator != overload ; check vector inequality
            /// </summary>
            /// <param name="v1">First vector</param>
            /// <param name="v2">Second vector</param>
            /// <returns>v1 != v2</returns>
            ////////////////////////////////////////////////////////////
            public static bool operator !=(Vector2<T> v1, Vector2<T> v2)
            {
                return !v1.Equals(v2);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Provide a string describing the object
            /// </summary>
            /// <returns>String description of the object</returns>
            ////////////////////////////////////////////////////////////
            public override string ToString()
            {
                return "[Vector2<" + typeof(T).Name + ">]" +
                       " X(" + X + ")" +
                       " Y(" + Y + ")";
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Compare vector and object and checks if they are equal
            /// </summary>
            /// <param name="obj">Object to check</param>
            /// <returns>Object and vector are equal</returns>
            ////////////////////////////////////////////////////////////
            public override bool Equals(object obj)
            {
                return (obj is Vector2<T>) && Equals((Vector2<T>)obj);
            }

            ///////////////////////////////////////////////////////////
            /// <summary>
            /// Compare two vectors and checks if they are equal
            /// </summary>
            /// <param name="other">Vector to check</param>
            /// <returns>Vectors are equal</returns>
            ////////////////////////////////////////////////////////////
            public bool Equals(Vector2<T> other)
            {
                return (Operator<T>.Equal(X, other.X) &&
                        Operator<T>.Equal(Y, other.Y));
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Provide a integer describing the object
            /// </summary>
            /// <returns>Integer description of the object</returns>
            ////////////////////////////////////////////////////////////
            public override int GetHashCode()
            {
                return X.GetHashCode() ^
                       Y.GetHashCode();
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Explicit casting to another generic vector type
            /// </summary>
            /// <returns>Casting result</returns>
            ////////////////////////////////////////////////////////////
            public Vector2<K> Cast<K>()
            {
                return new Vector2<K>((K)Convert.ChangeType(X, typeof(K)),
                                      (K)Convert.ChangeType(Y, typeof(K)));
            }

            /// <summary>X (horizontal) component of the vector</summary>
            public T X;

            /// <summary>Y (vertical) component of the vector</summary>
            public T Y;
        }
    }
}
