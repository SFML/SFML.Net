using System;
using System.Diagnostics;

namespace SFML.System
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Represents an angle value
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly struct Angle
    {
        private const float Tau = (float)(2 * Math.PI);
        private const float Pi = (float)Math.PI;
        private const double DegreesInRadian = 180.0 / Math.PI;
        private const double RadiansInDegree = Math.PI / 180.0;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct an angle value from a number of radians
        /// </summary>
        /// <param name="radians">Number of radians</param>
        /// <returns>Angle value constructed from the number of radians</returns>
        ////////////////////////////////////////////////////////////
        public static Angle FromRadians(float radians) => new Angle(radians);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct an angle value from a number of degrees
        /// </summary>
        /// <param name="degrees">Number of degrees</param>
        /// <returns>Angle value constructed from the number of degrees</returns>
        ////////////////////////////////////////////////////////////
        public static Angle FromDegrees(float degrees) => new Angle((float)(degrees * RadiansInDegree));

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Predefined 0 degree angle value
        /// </summary>
        ////////////////////////////////////////////////////////////
        public static readonly Angle Zero;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets the angle's value in radians
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float Radians { get; }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets the angle's value in degrees
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float Degrees => (float)(Radians * DegreesInRadian);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Wrap to a range such that -180° &lt;= angle &lt; 180°
        /// <para/>
        /// Similar to a modulo operation, this returns a copy of the angle
        /// constrained to the range [-180°, 180°) == [-Pi, Pi).
        /// The resulting angle represents a rotation which is equivalent to *this.
        /// <para/>
        /// The name "signed" originates from the similarity to signed integers:
        /// <table>
        /// <tr>
        ///   <th></th>
        ///   <th>signed</th>
        ///   <th>unsigned</th>
        /// </tr>
        /// <tr>
        ///   <td>char</td>
        ///   <td>[-128, 128)</td>
        ///   <td>[0, 256)</td>
        /// </tr>
        /// <tr>
        ///   <td>Angle</td>
        ///   <td>[-180°, 180°)</td>
        ///   <td>[0°, 360°)</td>
        /// </tr>
        /// </table>
        /// </summary>
        /// <returns>Signed angle, wrapped to [-180°, 180°)</returns>
        ////////////////////////////////////////////////////////////
        public Angle WrapSigned() => FromRadians(PositiveRemainder(Radians + Pi, Tau) - Pi);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Wrap to a range such that 0° &lt;= angle &lt; 360°
        /// <para/>
        /// Similar to a modulo operation, this returns a copy of the angle
        /// constrained to the range [0°, 360°) == [0, Tau) == [0, 2*Pi).
        /// The resulting angle represents a rotation which is equivalent to this
        /// <para/>
        /// The name "unsigned" originates from the similarity to unsigned integers:
        /// <table>
        /// <tr>
        ///   <th></th>
        ///   <th>signed</th>
        ///   <th>unsigned</th>
        /// </tr>
        /// <tr>
        ///   <td>char</td>
        ///   <td>[-128, 128)</td>
        ///   <td>[0, 256)</td>
        /// </tr>
        /// <tr>
        ///   <td>Angle</td>
        ///   <td>[-180°, 180°)</td>
        ///   <td>[0°, 360°)</td>
        /// </tr>
        /// </table>
        /// </summary>
        /// <returns>Unsigned angle, wrapped to [0°, 360°)</returns>
        ////////////////////////////////////////////////////////////
        public Angle WrapUnsigned() => FromRadians((float)PositiveRemainder(Radians, Tau));

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of == operator to compare two angle values
        /// </summary>
        /// <remarks>Does not automatically wrap the angle value</remarks>
        /// <param name="left">Left operand (an angle)</param>
        /// <param name="right">Right operand (an angle)</param>
        /// <returns>True if both angle values are equal</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator ==(Angle left, Angle right) => left.Radians == right.Radians;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of != operator to compare two angle values
        /// </summary>
        /// <remarks>Does not automatically wrap the angle value</remarks>
        /// <param name="left">Left operand (an angle)</param>
        /// <param name="right">Right operand (an angle)</param>
        /// <returns>True if both angle values are different</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator !=(Angle left, Angle right) => left.Radians != right.Radians;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of &lt; operator to compare two angle values
        /// </summary>
        /// <remarks>Does not automatically wrap the angle value</remarks>
        /// <param name="left">Left operand (an angle)</param>
        /// <param name="right">Right operand (an angle)</param>
        /// <returns>True if <paramref name="left"/> is less than <paramref name="right"/></returns>
        ////////////////////////////////////////////////////////////
        public static bool operator <(Angle left, Angle right) => left.Radians < right.Radians;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of &lt;= operator to compare two angle values
        /// </summary>
        /// <remarks>Does not automatically wrap the angle value</remarks>
        /// <param name="left">Left operand (an angle)</param>
        /// <param name="right">Right operand (an angle)</param>
        /// <returns>True if <paramref name="left"/> is less than or equal to <paramref name="right"/></returns>
        ////////////////////////////////////////////////////////////
        public static bool operator <=(Angle left, Angle right) => left.Radians <= right.Radians;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of &gt; operator to compare two angle values
        /// </summary>
        /// <remarks>Does not automatically wrap the angle value</remarks>
        /// <param name="left">Left operand (an angle)</param>
        /// <param name="right">Right operand (an angle)</param>
        /// <returns>True if <paramref name="left"/> is greater than <paramref name="right"/></returns>
        ////////////////////////////////////////////////////////////
        public static bool operator >(Angle left, Angle right) => left.Radians > right.Radians;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of &gt;= operator to compare two angle values
        /// </summary>
        /// <remarks>Does not automatically wrap the angle value</remarks>
        /// <param name="left">Left operand (an angle)</param>
        /// <param name="right">Right operand (an angle)</param>
        /// <returns>True if <paramref name="left"/> is greater than or equal to <paramref name="right"/></returns>
        ////////////////////////////////////////////////////////////
        public static bool operator >=(Angle left, Angle right) => left.Radians >= right.Radians;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of binary + operator to add two angle values
        /// </summary>
        /// <param name="left">Left operand (an angle)</param>
        /// <param name="right">Right operand (an angle)</param>
        /// <returns>Sum of the two angle values</returns>
        ////////////////////////////////////////////////////////////
        public static Angle operator +(Angle left, Angle right) => FromRadians(left.Radians + right.Radians);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of unary - operator to negate an angle value
        /// <para/>
        /// Represents a rotation in the opposite direction.
        /// </summary>
        /// <param name="right">Right operand (an angle)</param>
        /// <returns>Negative of the angle value</returns>
        ////////////////////////////////////////////////////////////
        public static Angle operator -(Angle right) => FromRadians(-right.Radians);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of binary - operator to substract two angle values
        /// </summary>
        /// <param name="left">Left operand (an angle)</param>
        /// <param name="right">Right operand (an angle)</param>
        /// <returns>Difference of the two angle values</returns>
        ////////////////////////////////////////////////////////////
        public static Angle operator -(Angle left, Angle right) => FromRadians(left.Radians - right.Radians);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of binary * operator to scale an angle value
        /// </summary>
        /// <param name="left">Left operand (an angle)</param>
        /// <param name="right">Right operand (a number)</param>
        /// <returns><paramref name="left"/> multiplied by <paramref name="right"/></returns>
        ////////////////////////////////////////////////////////////
        public static Angle operator *(Angle left, float right) => FromRadians(left.Radians * right);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of binary * operator to scale an angle value
        /// </summary>
        /// <param name="left">Left operand (a number)</param>
        /// <param name="right">Right operand (an angle)</param>
        /// <returns><paramref name="left"/> multiplied by <paramref name="right"/></returns>
        ////////////////////////////////////////////////////////////
        public static Angle operator *(float left, Angle right) => FromRadians(left * right.Radians);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of binary / operator to compute the ratio of two angle values
        /// </summary>
        /// <param name="left">Left operand (an angle)</param>
        /// <param name="right">Right operand (an angle)</param>
        /// <returns><paramref name="left"/> divided by <paramref name="right"/></returns>
        ////////////////////////////////////////////////////////////
        public static float operator /(Angle left, Angle right)
        {
            Debug.Assert(right.Radians != 0f, "Angle.operator/ cannot divide by 0");

            return left.Radians / right.Radians;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of binary / operator to scale an angle value
        /// </summary>
        /// <param name="left">Left operand (an angle)</param>
        /// <param name="right">Right operand (a number)</param>
        /// <returns><paramref name="left"/> divided by <paramref name="right"/></returns>
        ////////////////////////////////////////////////////////////
        public static Angle operator /(Angle left, float right)
        {
            Debug.Assert(right != 0f, "Angle.operator/ cannot divide by 0");

            return FromRadians(left.Radians / right);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of binary % operator to compute modulo of an angle value
        /// <para/>
        /// Right hand angle must be greater than zero.
        /// <para/>
        /// Examples:
        /// <code>
        /// sf::degrees(90) % sf::degrees(40)  // 10 degrees
        /// sf::degrees(-90) % sf::degrees(40) // 30 degrees (not -10)
        /// </code>
        /// </summary>
        /// <param name="left">Left operand (an angle)</param>
        /// <param name="right">Right operand (an angle)</param>
        /// <returns><paramref name="left"/> modulo <paramref name="right"/></returns>
        ////////////////////////////////////////////////////////////
        public static Angle operator %(Angle left, Angle right)
        {
            Debug.Assert(right.Radians != 0f, "Angle.operator% cannot modulus by 0");

            return FromRadians((float)PositiveRemainder(left.Radians, right.Radians));
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Compare angle and object and checks if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>Object and angle are equal</returns>
        ////////////////////////////////////////////////////////////
        public override bool Equals(object obj) => (obj is Angle angle) && Equals(angle);

        ///////////////////////////////////////////////////////////
        /// <summary>
        /// Compare two angles and checks if they are equal
        /// </summary>
        /// <param name="other">Angle to check</param>
        /// <returns>Angles are equal</returns>
        ////////////////////////////////////////////////////////////
        public bool Equals(Angle other) => Radians == other.Radians;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override int GetHashCode() => Radians.GetHashCode();

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => "[Angle]" +
                   " Radians(" + Radians + ")" +
                   " Degrees(" + Degrees + ")";

        private Angle(float radians) => Radians = radians;

        private static float PositiveRemainder(float a, float b)
        {
            Debug.Assert(b > 0, "Cannot calculate remainder with non-positive divisor");

            var val = a - ((int)(a / b) * b);
            return val >= 0.0 ? val : val + b;
        }
    }
}
