using System;
using System.Runtime.InteropServices;

namespace SFML.System;

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
    /// Construct the vector from its coordinates
    /// <para/>
    /// Note that this constructor is lossy: calling Length and Angle
    /// may return values different to those provided in this constructor.
    /// <para/>
    /// In particular, these transforms can be applied:
    /// * Vector2(r, phi) == Vector2(-r, phi + 180_deg)
    /// * Vector2(r, phi) == Vector2(r, phi + n * 360_deg)
    /// </summary>
    /// <param name="r">Length of vector (can be negative)</param>
    /// <param name="phi">Angle from X axis</param>
    ////////////////////////////////////////////////////////////
    public Vector2f(float r, Angle phi)
    {
        X = (float)(r * Math.Cos(phi.Radians));
        Y = (float)(r * Math.Sin(phi.Radians));
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Length of the vector
    /// <para/>
    /// If you are not interested in the actual length, but only in comparisons, consider using <see cref="LengthSquared"/>
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly float Length => (float)Math.Sqrt(((double)X * X) + ((double)Y * Y));

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Square of vector's length
    /// <para/>
    /// Suitable for comparisons, more efficient than <see cref="Length"/>
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly float LengthSquared => Dot(this);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Vector with same direction but length 1
    /// <para/>
    /// <see langword="this"/> should not be a zero vector
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly Vector2f Normalized() => this / Length;

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Signed angle from <see langword="this"/> to <paramref name="rhs"/>
    /// <para/>
    /// Neither <see langword="this"/> nor <paramref name="rhs"/> should be a zero vector.
    /// </summary>
    /// <returns>
    /// The smallest angle which rotates `*this` in positive
    /// or negative direction, until it has the same direction as \a `rhs`.
    /// The result has a sign and lies in the range [-180, 180) degrees.
    /// </returns>
    ////////////////////////////////////////////////////////////
    public readonly Angle AngleTo(Vector2f rhs) => SFML.System.Angle.FromRadians((float)Math.Atan2(Cross(rhs), Dot(rhs)));

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Signed angle from +X or (1,0) vector
    /// <para/>
    /// For example, the vector (1,0) corresponds to 0 degrees, (0,1) corresponds to 90 degrees.
    /// <para/>
    /// <see langword="this"/> should not be a zero vector
    /// </summary>
    /// <returns>Angle in the range [-180, 180) degrees</returns>
    ////////////////////////////////////////////////////////////
    public readonly Angle Angle() => SFML.System.Angle.FromRadians((float)Math.Atan2(Y, X));

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Rotate by angle <paramref name="phi"/>
    /// <para/>
    /// Returns a vector with same length but different direction.
    /// <para/>
    /// In SFML's default coordinate system with +X right and +Y down,
    /// this amounts to a clockwise rotation by <paramref name="phi"/>.
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly Vector2f RotatedBy(Angle phi)
    {
        var cos = Math.Cos(phi.Radians);
        var sin = Math.Sin(phi.Radians);

        return new Vector2f((float)((cos * X) - (sin * Y)), (float)((sin * X) + (cos * Y)));
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Projection of this vector onto <paramref name="axis"/>
    /// <para/>
    /// <paramref name="axis"/> must not have length zero
    /// </summary>
    /// <param name="axis">Vector being projected onto. Need not be normalized</param>
    ////////////////////////////////////////////////////////////
    public readonly Vector2f ProjectedOnto(Vector2f axis) => Dot(axis) / axis.LengthSquared * axis;

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Returns a perpendicular vector.
    ///
    /// Returns <see langword="this"/> rotated by +90 degrees; (x,y) becomes (-y,x).
    /// For example, the vector (1,0) is transformed to (0,1).
    ///
    /// In SFML's default coordinate system with +X right and +Y down,
    /// this amounts to a clockwise rotation.
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly Vector2f Perpendicular() => new(-Y, X);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Dot product of two 2D vectors.
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly float Dot(Vector2f rhs) => (X * rhs.X) + (Y * rhs.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Z component of the cross product of two 2D vectors.
    /// <para/>
    /// Treats the operands as 3D vectors, computes their cross product
    /// and returns the result's Z component (X and Y components are always zero).
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly float Cross(Vector2f rhs) => (X * rhs.Y) - (Y * rhs.X);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Component-wise multiplication of <see langword="this"/> and <paramref name="rhs"/>
    /// <para/>
    /// Computes <code>(this.X * rhs.X, this.Y * rhs.Y)</code>
    /// <para/>
    /// Scaling is the most common use case for component-wise multiplication/division.
    /// This operation is also known as the Hadamard or Schur product.
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly Vector2f ComponentWiseMul(Vector2f rhs) => new(X * rhs.X, Y * rhs.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Component-wise division of <see langword="this"/> and <paramref name="rhs"/>
    /// <para/>
    /// Computes <code>(this.X / rhs.X, this.Y / rhs.Y)</code>
    /// <para/>
    /// Scaling is the most common use case for component-wise multiplication/division.
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly Vector2f ComponentWiseDiv(Vector2f rhs) => new(X / rhs.X, Y / rhs.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// The X unit vector (1, 0), usually facing right
    /// </summary>
    ////////////////////////////////////////////////////////////
    public static readonly Vector2f UnitX = new(1, 0);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// The Y unit vector (0, 1), usually facing down
    /// </summary>
    ////////////////////////////////////////////////////////////
    public static readonly Vector2f UnitY = new(0, 1);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Deconstructs a Vector2f into a tuple of floats
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    ////////////////////////////////////////////////////////////
    public readonly void Deconstruct(out float x, out float y)
    {
        x = X;
        y = Y;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Operator - overload ; returns the opposite of a vector
    /// </summary>
    /// <param name="v">Vector to negate</param>
    /// <returns>-v</returns>
    ////////////////////////////////////////////////////////////
    public static Vector2f operator -(Vector2f v) => new(-v.X, -v.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Operator - overload ; subtracts two vectors
    /// </summary>
    /// <param name="v1">First vector</param>
    /// <param name="v2">Second vector</param>
    /// <returns>v1 - v2</returns>
    ////////////////////////////////////////////////////////////
    public static Vector2f operator -(Vector2f v1, Vector2f v2) => new(v1.X - v2.X, v1.Y - v2.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Operator + overload ; add two vectors
    /// </summary>
    /// <param name="v1">First vector</param>
    /// <param name="v2">Second vector</param>
    /// <returns>v1 + v2</returns>
    ////////////////////////////////////////////////////////////
    public static Vector2f operator +(Vector2f v1, Vector2f v2) => new(v1.X + v2.X, v1.Y + v2.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Operator * overload ; multiply a vector by a scalar value
    /// </summary>
    /// <param name="v">Vector</param>
    /// <param name="x">Scalar value</param>
    /// <returns>v * x</returns>
    ////////////////////////////////////////////////////////////
    public static Vector2f operator *(Vector2f v, float x) => new(v.X * x, v.Y * x);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Operator * overload ; multiply a scalar value by a vector
    /// </summary>
    /// <param name="x">Scalar value</param>
    /// <param name="v">Vector</param>
    /// <returns>x * v</returns>
    ////////////////////////////////////////////////////////////
    public static Vector2f operator *(float x, Vector2f v) => new(v.X * x, v.Y * x);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Operator / overload ; divide a vector by a scalar value
    /// </summary>
    /// <param name="v">Vector</param>
    /// <param name="x">Scalar value</param>
    /// <returns>v / x</returns>
    ////////////////////////////////////////////////////////////
    public static Vector2f operator /(Vector2f v, float x) => new(v.X / x, v.Y / x);

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
    public override readonly string ToString() => $"[Vector2f] X({X}) Y({Y})";

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Compare vector and object and checks if they are equal
    /// </summary>
    /// <param name="obj">Object to check</param>
    /// <returns>Object and vector are equal</returns>
    ////////////////////////////////////////////////////////////
    public override readonly bool Equals(object obj) => obj is Vector2f vec && Equals(vec);

    ///////////////////////////////////////////////////////////
    /// <summary>
    /// Compare two vectors and checks if they are equal
    /// </summary>
    /// <param name="other">Vector to check</param>
    /// <returns>Vectors are equal</returns>
    ////////////////////////////////////////////////////////////
    public readonly bool Equals(Vector2f other) => X == other.X && Y == other.Y;

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
    public static explicit operator Vector2i(Vector2f v) => new((int)v.X, (int)v.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Explicit casting to another vector type
    /// </summary>
    /// <param name="v">Vector being casted</param>
    /// <returns>Casting result</returns>
    ////////////////////////////////////////////////////////////
    public static explicit operator Vector2u(Vector2f v) => new((uint)v.X, (uint)v.Y);

    /// <summary>
    /// Converts a tuple of floats to a Vector2f
    /// </summary>
    /// <param name="tuple">The tuple to convert</param>
    public static implicit operator Vector2f((float X, float Y) tuple) => new(tuple.X, tuple.Y);

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
    /// Square of vector's length
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly float LengthSquared => Dot(this);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Returns a perpendicular vector.
    ///
    /// Returns <see langword="this"/> rotated by +90 degrees; (x,y) becomes (-y,x).
    /// For example, the vector (1,0) is transformed to (0,1).
    ///
    /// In SFML's default coordinate system with +X right and +Y down,
    /// this amounts to a clockwise rotation.
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly Vector2i Perpendicular() => new(-Y, X);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Dot product of two 2D vectors.
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly float Dot(Vector2i rhs) => (X * rhs.X) + (Y * rhs.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Z component of the cross product of two 2D vectors.
    /// <para/>
    /// Treats the operands as 3D vectors, computes their cross product
    /// and returns the result's Z component (X and Y components are always zero).
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly float Cross(Vector2i rhs) => (X * rhs.Y) - (Y * rhs.X);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Component-wise multiplication of <see langword="this"/> and <paramref name="rhs"/>
    /// <para/>
    /// Computes <code>(this.X * rhs.X, this.Y * rhs.Y)</code>
    /// <para/>
    /// Scaling is the most common use case for component-wise multiplication/division.
    /// This operation is also known as the Hadamard or Schur product.
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly Vector2i ComponentWiseMul(Vector2i rhs) => new(X * rhs.X, Y * rhs.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Component-wise division of <see langword="this"/> and <paramref name="rhs"/>
    /// <para/>
    /// Computes <code>(this.X / rhs.X, this.Y / rhs.Y)</code>
    /// <para/>
    /// Scaling is the most common use case for component-wise multiplication/division.
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly Vector2i ComponentWiseDiv(Vector2i rhs) => new(X / rhs.X, Y / rhs.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// The X unit vector (1, 0), usually facing right
    /// </summary>
    ////////////////////////////////////////////////////////////
    public static readonly Vector2i UnitX = new(1, 0);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// The Y unit vector (0, 1), usually facing down
    /// </summary>
    ////////////////////////////////////////////////////////////
    public static readonly Vector2i UnitY = new(0, 1);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Deconstructs a Vector2i into a tuple of ints
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    ////////////////////////////////////////////////////////////
    public readonly void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Operator - overload ; returns the opposite of a vector
    /// </summary>
    /// <param name="v">Vector to negate</param>
    /// <returns>-v</returns>
    ////////////////////////////////////////////////////////////
    public static Vector2i operator -(Vector2i v) => new(-v.X, -v.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Operator - overload ; subtracts two vectors
    /// </summary>
    /// <param name="v1">First vector</param>
    /// <param name="v2">Second vector</param>
    /// <returns>v1 - v2</returns>
    ////////////////////////////////////////////////////////////
    public static Vector2i operator -(Vector2i v1, Vector2i v2) => new(v1.X - v2.X, v1.Y - v2.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Operator + overload ; add two vectors
    /// </summary>
    /// <param name="v1">First vector</param>
    /// <param name="v2">Second vector</param>
    /// <returns>v1 + v2</returns>
    ////////////////////////////////////////////////////////////
    public static Vector2i operator +(Vector2i v1, Vector2i v2) => new(v1.X + v2.X, v1.Y + v2.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Operator * overload ; multiply a vector by a scalar value
    /// </summary>
    /// <param name="v">Vector</param>
    /// <param name="x">Scalar value</param>
    /// <returns>v * x</returns>
    ////////////////////////////////////////////////////////////
    public static Vector2i operator *(Vector2i v, int x) => new(v.X * x, v.Y * x);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Operator * overload ; multiply a scalar value by a vector
    /// </summary>
    /// <param name="x">Scalar value</param>
    /// <param name="v">Vector</param>
    /// <returns>x * v</returns>
    ////////////////////////////////////////////////////////////
    public static Vector2i operator *(int x, Vector2i v) => new(v.X * x, v.Y * x);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Operator / overload ; divide a vector by a scalar value
    /// </summary>
    /// <param name="v">Vector</param>
    /// <param name="x">Scalar value</param>
    /// <returns>v / x</returns>
    ////////////////////////////////////////////////////////////
    public static Vector2i operator /(Vector2i v, int x) => new(v.X / x, v.Y / x);

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
    public override readonly string ToString() => $"[Vector2i] X({X}) Y({Y})";

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Compare vector and object and checks if they are equal
    /// </summary>
    /// <param name="obj">Object to check</param>
    /// <returns>Object and vector are equal</returns>
    ////////////////////////////////////////////////////////////
    public override readonly bool Equals(object obj) => obj is Vector2i vec && Equals(vec);

    ///////////////////////////////////////////////////////////
    /// <summary>
    /// Compare two vectors and checks if they are equal
    /// </summary>
    /// <param name="other">Vector to check</param>
    /// <returns>Vectors are equal</returns>
    ////////////////////////////////////////////////////////////
    public readonly bool Equals(Vector2i other) => X == other.X && Y == other.Y;

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
    public static explicit operator Vector2f(Vector2i v) => new(v.X, v.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Explicit casting to another vector type
    /// </summary>
    /// <param name="v">Vector being casted</param>
    /// <returns>Casting result</returns>
    ////////////////////////////////////////////////////////////
    public static explicit operator Vector2u(Vector2i v) => new((uint)v.X, (uint)v.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Converts a tuple of ints to a Vector2i
    /// </summary>
    /// <param name="tuple">The tuple to convert</param>
    ////////////////////////////////////////////////////////////
    public static implicit operator Vector2i((int X, int Y) tuple) => new(tuple.X, tuple.Y);

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
    /// Square of vector's length
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly float LengthSquared => Dot(this);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Dot product of two 2D vectors.
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly float Dot(Vector2u rhs) => (X * rhs.X) + (Y * rhs.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Z component of the cross product of two 2D vectors.
    /// <para/>
    /// Treats the operands as 3D vectors, computes their cross product
    /// and returns the result's Z component (X and Y components are always zero).
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly float Cross(Vector2u rhs) => (X * rhs.Y) - (Y * rhs.X);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Component-wise multiplication of <see langword="this"/> and <paramref name="rhs"/>
    /// <para/>
    /// Computes <code>(this.X * rhs.X, this.Y * rhs.Y)</code>
    /// <para/>
    /// Scaling is the most common use case for component-wise multiplication/division.
    /// This operation is also known as the Hadamard or Schur product.
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly Vector2u ComponentWiseMul(Vector2u rhs) => new(X * rhs.X, Y * rhs.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Component-wise division of <see langword="this"/> and <paramref name="rhs"/>
    /// <para/>
    /// Computes <code>(this.X / rhs.X, this.Y / rhs.Y)</code>
    /// <para/>
    /// Scaling is the most common use case for component-wise multiplication/division.
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly Vector2u ComponentWiseDiv(Vector2u rhs) => new(X / rhs.X, Y / rhs.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// The X unit vector (1, 0), usually facing right
    /// </summary>
    ////////////////////////////////////////////////////////////
    public static readonly Vector2u UnitX = new(1, 0);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// The Y unit vector (0, 1), usually facing down
    /// </summary>
    ////////////////////////////////////////////////////////////
    public static readonly Vector2u UnitY = new(0, 1);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Deconstructs a Vector2u into a tuple of uints
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    ////////////////////////////////////////////////////////////
    public readonly void Deconstruct(out uint x, out uint y)
    {
        x = X;
        y = Y;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Operator - overload ; subtracts two vectors
    /// </summary>
    /// <param name="v1">First vector</param>
    /// <param name="v2">Second vector</param>
    /// <returns>v1 - v2</returns>
    ////////////////////////////////////////////////////////////
    public static Vector2u operator -(Vector2u v1, Vector2u v2) => new(v1.X - v2.X, v1.Y - v2.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Operator + overload ; add two vectors
    /// </summary>
    /// <param name="v1">First vector</param>
    /// <param name="v2">Second vector</param>
    /// <returns>v1 + v2</returns>
    ////////////////////////////////////////////////////////////
    public static Vector2u operator +(Vector2u v1, Vector2u v2) => new(v1.X + v2.X, v1.Y + v2.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Operator * overload ; multiply a vector by a scalar value
    /// </summary>
    /// <param name="v">Vector</param>
    /// <param name="x">Scalar value</param>
    /// <returns>v * x</returns>
    ////////////////////////////////////////////////////////////
    public static Vector2u operator *(Vector2u v, uint x) => new(v.X * x, v.Y * x);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Operator * overload ; multiply a scalar value by a vector
    /// </summary>
    /// <param name="x">Scalar value</param>
    /// <param name="v">Vector</param>
    /// <returns>x * v</returns>
    ////////////////////////////////////////////////////////////
    public static Vector2u operator *(uint x, Vector2u v) => new(v.X * x, v.Y * x);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Operator / overload ; divide a vector by a scalar value
    /// </summary>
    /// <param name="v">Vector</param>
    /// <param name="x">Scalar value</param>
    /// <returns>v / x</returns>
    ////////////////////////////////////////////////////////////
    public static Vector2u operator /(Vector2u v, uint x) => new(v.X / x, v.Y / x);

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
    public override readonly string ToString() => $"[Vector2u] X({X}) Y({Y})";

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Compare vector and object and checks if they are equal
    /// </summary>
    /// <param name="obj">Object to check</param>
    /// <returns>Object and vector are equal</returns>
    ////////////////////////////////////////////////////////////
    public override readonly bool Equals(object obj) => obj is Vector2u vec && Equals(vec);

    ///////////////////////////////////////////////////////////
    /// <summary>
    /// Compare two vectors and checks if they are equal
    /// </summary>
    /// <param name="other">Vector to check</param>
    /// <returns>Vectors are equal</returns>
    ////////////////////////////////////////////////////////////
    public readonly bool Equals(Vector2u other) => X == other.X && Y == other.Y;

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
    public static explicit operator Vector2i(Vector2u v) => new((int)v.X, (int)v.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Explicit casting to another vector type
    /// </summary>
    /// <param name="v">Vector being casted</param>
    /// <returns>Casting result</returns>
    ////////////////////////////////////////////////////////////
    public static explicit operator Vector2f(Vector2u v) => new(v.X, v.Y);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Converts a tuple of uints to a Vector2u
    /// </summary>
    /// <param name="tuple">The tuple to convert</param>
    ////////////////////////////////////////////////////////////
    public static implicit operator Vector2u((uint X, uint Y) tuple) => new(tuple.X, tuple.Y);

    /// <summary>X (horizontal) component of the vector</summary>
    public uint X;

    /// <summary>Y (vertical) component of the vector</summary>
    public uint Y;
}
