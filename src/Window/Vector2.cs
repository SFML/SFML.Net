using System;
using System.Runtime.InteropServices;

namespace SFML
{
    namespace Window
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Vector2f is an utility class for manipulating 2 dimensional
        /// vectors with float components
        /// </summary>
        ////////////////////////////////////////////////////////////
        [StructLayout(LayoutKind.Sequential)]
        public struct Vector2f
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
            public static Vector2f operator -(Vector2f v)
            {
                return new Vector2f(-v.X, -v.Y);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator - overload ; subtracts two vectors
            /// </summary>
            /// <param name="v1">First vector</param>
            /// <param name="v2">Second vector</param>
            /// <returns>v1 - v2</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2f operator -(Vector2f v1, Vector2f v2)
            {
                return new Vector2f(v1.X - v2.X, v1.Y - v2.Y);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator + overload ; add two vectors
            /// </summary>
            /// <param name="v1">First vector</param>
            /// <param name="v2">Second vector</param>
            /// <returns>v1 + v2</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2f operator +(Vector2f v1, Vector2f v2)
            {
                return new Vector2f(v1.X + v2.X, v1.Y + v2.Y);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator * overload ; multiply a vector by a scalar value
            /// </summary>
            /// <param name="v">Vector</param>
            /// <param name="x">Scalar value</param>
            /// <returns>v * x</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2f operator *(Vector2f v, float x)
            {
                return new Vector2f(v.X * x, v.Y * x);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator * overload ; multiply a scalar value by a vector
            /// </summary>
            /// <param name="x">Scalar value</param>
            /// <param name="v">Vector</param>
            /// <returns>x * v</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2f operator *(float x, Vector2f v)
            {
                return new Vector2f(v.X * x, v.Y * x);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator / overload ; divide a vector by a scalar value
            /// </summary>
            /// <param name="v">Vector</param>
            /// <param name="x">Scalar value</param>
            /// <returns>v / x</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2f operator /(Vector2f v, float x)
            {
                return new Vector2f(v.X / x, v.Y / x);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Provide a string describing the object
            /// </summary>
            /// <returns>String description of the object</returns>
            ////////////////////////////////////////////////////////////
            public override string ToString()
            {
                return "[Vector2f]" +
                       " X(" + X + ")" +
                       " Y(" + Y + ")";
            }

            /// <summary>X (horizontal) component of the vector</summary>
            public float X;
 
            /// <summary>Y (vertical) component of the vector</summary>
            public float Y;

            /// <summary>Vector2f with X and Y set to 0</summary>
            private static Vector2f zero = new Vector2f(0, 0);

            /// <summary>Vector2f with X and Y set to 0</summary>
            public static Vector2f Zero { get { return zero; } }

            /// <summary>
            /// Checks if the passed obj's value equals this objects's value
            /// </summary>
            /// <param name="obj"></param>
            /// <returns>true if equal, false if not equal</returns>
            public override bool Equals(object obj)
            {
                if (obj is Vector2f)
                {
                    return ((Vector2f)obj).X == X && ((Vector2f)obj).Y == Y;
                }
                else
                {
                    return false;
                }
            }

            /// <summary>
            /// Returns this objects hash code
            /// </summary>
            /// <returns>int hash code</returns>
            public override int GetHashCode()
            {
                int hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Vector2i is an utility class for manipulating 2 dimensional
        /// vectors with integer components
        /// </summary>
        ////////////////////////////////////////////////////////////
        [StructLayout(LayoutKind.Sequential)]
        public struct Vector2i
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
            public static Vector2i operator -(Vector2i v)
            {
                return new Vector2i(-v.X, -v.Y);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator - overload ; subtracts two vectors
            /// </summary>
            /// <param name="v1">First vector</param>
            /// <param name="v2">Second vector</param>
            /// <returns>v1 - v2</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2i operator -(Vector2i v1, Vector2i v2)
            {
                return new Vector2i(v1.X - v2.X, v1.Y - v2.Y);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator + overload ; add two vectors
            /// </summary>
            /// <param name="v1">First vector</param>
            /// <param name="v2">Second vector</param>
            /// <returns>v1 + v2</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2i operator +(Vector2i v1, Vector2i v2)
            {
                return new Vector2i(v1.X + v2.X, v1.Y + v2.Y);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator * overload ; multiply a vector by a scalar value
            /// </summary>
            /// <param name="v">Vector</param>
            /// <param name="x">Scalar value</param>
            /// <returns>v * x</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2i operator *(Vector2i v, int x)
            {
                return new Vector2i(v.X * x, v.Y * x);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator * overload ; multiply a scalar value by a vector
            /// </summary>
            /// <param name="x">Scalar value</param>
            /// <param name="v">Vector</param>
            /// <returns>x * v</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2i operator *(int x, Vector2i v)
            {
                return new Vector2i(v.X * x, v.Y * x);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator / overload ; divide a vector by a scalar value
            /// </summary>
            /// <param name="v">Vector</param>
            /// <param name="x">Scalar value</param>
            /// <returns>v / x</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2i operator /(Vector2i v, int x)
            {
                return new Vector2i(v.X / x, v.Y / x);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Provide a string describing the object
            /// </summary>
            /// <returns>String description of the object</returns>
            ////////////////////////////////////////////////////////////
            public override string ToString()
            {
                return "[Vector2i]" +
                       " X(" + X + ")" +
                       " Y(" + Y + ")";
            }

            /// <summary>X (horizontal) component of the vector</summary>
            public int X;
 
            /// <summary>Y (vertical) component of the vector</summary>
            public int Y;

            /// <summary>Vector2i with X and Y set to 0</summary>
            private static Vector2i zero = new Vector2i(0, 0);

            /// <summary>Vector2i with X and Y set to 0</summary>
            public static Vector2i Zero { get { return zero; } }

            /// <summary>
            /// Checks if the passed obj's value equals this objects's value
            /// </summary>
            /// <param name="obj"></param>
            /// <returns>true if equal, false if not equal</returns>
            public override bool Equals(object obj)
            {
                if (obj is Vector2i)
                {
                    return ((Vector2i)obj).X == X && ((Vector2i)obj).Y == Y;
                }
                else
                {
                    return false;
                }
            }

            /// <summary>
            /// Returns this objects hash code
            /// </summary>
            /// <returns>int hash code</returns>
            public override int GetHashCode()
            {
                int hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Vector2u is an utility class for manipulating 2 dimensional
        /// vectors with unsigned integer components
        /// </summary>
        ////////////////////////////////////////////////////////////
        [StructLayout(LayoutKind.Sequential)]
        public struct Vector2u
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
            public static Vector2u operator -(Vector2u v1, Vector2u v2)
            {
                return new Vector2u(v1.X - v2.X, v1.Y - v2.Y);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator + overload ; add two vectors
            /// </summary>
            /// <param name="v1">First vector</param>
            /// <param name="v2">Second vector</param>
            /// <returns>v1 + v2</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2u operator +(Vector2u v1, Vector2u v2)
            {
                return new Vector2u(v1.X + v2.X, v1.Y + v2.Y);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator * overload ; multiply a vector by a scalar value
            /// </summary>
            /// <param name="v">Vector</param>
            /// <param name="x">Scalar value</param>
            /// <returns>v * x</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2u operator *(Vector2u v, uint x)
            {
                return new Vector2u(v.X * x, v.Y * x);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator * overload ; multiply a scalar value by a vector
            /// </summary>
            /// <param name="x">Scalar value</param>
            /// <param name="v">Vector</param>
            /// <returns>x * v</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2u operator *(uint x, Vector2u v)
            {
                return new Vector2u(v.X * x, v.Y * x);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Operator / overload ; divide a vector by a scalar value
            /// </summary>
            /// <param name="v">Vector</param>
            /// <param name="x">Scalar value</param>
            /// <returns>v / x</returns>
            ////////////////////////////////////////////////////////////
            public static Vector2u operator /(Vector2u v, uint x)
            {
                return new Vector2u(v.X / x, v.Y / x);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Provide a string describing the object
            /// </summary>
            /// <returns>String description of the object</returns>
            ////////////////////////////////////////////////////////////
            public override string ToString()
            {
                return "[Vector2u]" +
                       " X(" + X + ")" +
                       " Y(" + Y + ")";
            }

            /// <summary>X (horizontal) component of the vector</summary>
            public uint X;

            /// <summary>Y (vertical) component of the vector</summary>
            public uint Y;

            /// <summary>Vector2u with X and Y set to 0</summary>
            private static Vector2u zero = new Vector2u(0, 0);

            /// <summary>Vector2u with X and Y set to 0</summary>
            public static Vector2u Zero { get { return zero; } }

            /// <summary>
            /// Checks if the passed obj's value equals this objects's value
            /// </summary>
            /// <param name="obj"></param>
            /// <returns>true if equal, false if not equal</returns>
            public override bool Equals(object obj)
            {
                if (obj is Vector2u)
                {
                    return ((Vector2u)obj).X == X && ((Vector2u)obj).Y == Y;
                }
                else
                {
                    return false;
                }
            }

            /// <summary>
            /// Returns this objects hash code
            /// </summary>
            /// <returns>int hash code</returns>
            public override int GetHashCode()
            {
                int hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }
        }
    }
}
