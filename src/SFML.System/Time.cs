using System;
using System.Runtime.InteropServices;
using System.Security;

namespace SFML.System
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// A Time value for interfacing with SFML
    /// </summary>
    /// <remarks>
    /// <para>Using <see cref="TimeSpan"/> while working within managed code is best.</para>
    /// <para>In most cases, <see cref="Time"/> is only needed when calling into unmanaged code.</para>
    /// <para>Implicit casts from <see cref="TimeSpan"/> to <see cref="Time"/>
    /// allow <see cref="TimeSpan"/>s to be used for most SFML methods.</para>
    /// </remarks>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Time : IEquatable<Time>
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Predefined "zero" time value
        /// </summary>
        ////////////////////////////////////////////////////////////
        public static readonly Time Zero = FromMicroseconds(0);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct a <see cref="Time"/> from a number of seconds
        /// </summary>
        /// <param name="seconds">Number of seconds</param>
        /// <returns>Time constructed from the amount of seconds</returns>
        ////////////////////////////////////////////////////////////
        public static Time FromSeconds(float seconds) => sfSeconds(seconds);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct a <see cref="Time"/> from a number of milliseconds
        /// </summary>
        /// <param name="milliseconds">Number of milliseconds</param>
        /// <returns>Time constructed from the amount of milliseconds</returns>
        ////////////////////////////////////////////////////////////
        public static Time FromMilliseconds(int milliseconds) => sfMilliseconds(milliseconds);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct a <see cref="Time"/> from a number of microseconds
        /// </summary>
        /// <param name="microseconds">Number of microseconds</param>
        /// <returns>Time constructed from the amount of microseconds</returns>
        ////////////////////////////////////////////////////////////
        public static Time FromMicroseconds(long microseconds) => sfMicroseconds(microseconds);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct a <see cref="Time"/> from a <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="timeSpan">A TimeSpan representing the amount of time to represent</param>
        /// <returns>Time constructed from an existing TimeSpan</returns>
        ////////////////////////////////////////////////////////////
        public static Time FromTimeSpan(TimeSpan timeSpan) => sfMicroseconds(timeSpan.Ticks * 1000 / TimeSpan.TicksPerMillisecond);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the <see cref="Time"/> as a number of seconds
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float AsSeconds() => _microseconds / 1000000f;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the <see cref="Time"/> as a number of milliseconds
        /// </summary>
        ////////////////////////////////////////////////////////////
        public int AsMilliseconds() => (int)(_microseconds / 1000);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the <see cref="Time"/> as a number of microseconds
        /// </summary>
        ////////////////////////////////////////////////////////////
        public long AsMicroseconds() => _microseconds;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns the <see cref="Time"/> as a TimeSpan
        /// </summary>
        ////////////////////////////////////////////////////////////
        public TimeSpan ToTimeSpan() => TimeSpan.FromTicks(_microseconds * (TimeSpan.TicksPerMillisecond / 1000));

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Implicit conversion from <see cref="TimeSpan"/> to <see cref="Time"/>, allowing intuitive use
        /// </summary>
        ////////////////////////////////////////////////////////////
        public static implicit operator Time(TimeSpan timeSpan) => FromTimeSpan(timeSpan);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Compare two times and checks if they are equal
        /// </summary>
        /// <returns>Times are equal</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator ==(Time left, Time right) => left.Equals(right);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Compare two times and checks if they are not equal
        /// </summary>
        /// <returns>Times are not equal</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator !=(Time left, Time right) => !left.Equals(right);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Compare time and object and checks if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>Object and time are equal</returns>
        ////////////////////////////////////////////////////////////
        public override bool Equals(object obj) => (obj is Time time) && Equals(time);

        ///////////////////////////////////////////////////////////
        /// <summary>
        /// Compare two times and checks if they are equal
        /// </summary>
        /// <param name="other">Time to check</param>
        /// <returns>times are equal</returns>
        ////////////////////////////////////////////////////////////
        public bool Equals(Time other) => _microseconds == other._microseconds;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of &lt; operator to compare two time values
        /// </summary>
        /// <returns>True if left is lesser than right</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator <(Time left, Time right) => left.AsMicroseconds() < right.AsMicroseconds();

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of &lt;= operator to compare two time values
        /// </summary>
        /// <returns>True if left is lesser or equal than right</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator <=(Time left, Time right) => left.AsMicroseconds() <= right.AsMicroseconds();

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of &gt; operator to compare two time values
        /// </summary>
        /// <returns>True if left is greater than right</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator >(Time left, Time right) => left.AsMicroseconds() > right.AsMicroseconds();

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of &gt;= operator to compare two time values
        /// </summary>
        /// <returns>True if left is greater or equal than right</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator >=(Time left, Time right) => left.AsMicroseconds() >= right.AsMicroseconds();

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of binary - operator to subtract two time values
        /// </summary>
        /// <returns>Difference of the two times values</returns>
        ////////////////////////////////////////////////////////////
        public static Time operator -(Time left, Time right) => FromMicroseconds(left.AsMicroseconds() - right.AsMicroseconds());

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of binary + operator to add two time values
        /// </summary>
        /// <returns>Sum of the two times values</returns>
        ////////////////////////////////////////////////////////////
        public static Time operator +(Time left, Time right) => FromMicroseconds(left.AsMicroseconds() + right.AsMicroseconds());

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of binary * operator to scale a time value
        /// </summary>
        /// <returns>left multiplied by the right</returns>
        ////////////////////////////////////////////////////////////
        public static Time operator *(Time left, float right) => FromSeconds(left.AsSeconds() * right);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of binary * operator to scale a time value
        /// </summary>
        /// <returns>left multiplied by the right</returns>
        ////////////////////////////////////////////////////////////
        public static Time operator *(Time left, long right) => FromMicroseconds(left.AsMicroseconds() * right);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of binary * operator to scale a time value
        /// </summary>
        /// <returns>left multiplied by the right</returns>
        ////////////////////////////////////////////////////////////
        public static Time operator *(float left, Time right) => FromSeconds(left * right.AsSeconds());

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of binary * operator to scale a time value
        /// </summary>
        /// <returns>left multiplied by the right</returns>
        ////////////////////////////////////////////////////////////
        public static Time operator *(long left, Time right) => FromMicroseconds(left * right.AsMicroseconds());

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of binary / operator to scale a time value
        /// </summary>
        /// <returns>left divided by the right</returns>
        ////////////////////////////////////////////////////////////
        public static float operator /(Time left, Time right) => left.AsSeconds() / right.AsSeconds();

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of binary / operator to scale a time value
        /// </summary>
        /// <returns>left divided by the right</returns>
        ////////////////////////////////////////////////////////////
        public static Time operator /(Time left, float right) => FromSeconds(left.AsSeconds() / right);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of binary / operator to scale a time value
        /// </summary>
        /// <returns>left divided by the right</returns>
        ////////////////////////////////////////////////////////////
        public static Time operator /(Time left, long right) => FromMicroseconds(left.AsMicroseconds() / right);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of binary % operator to compute remainder of a time value
        /// </summary>
        /// <returns>left modulo of right</returns>
        ////////////////////////////////////////////////////////////
        public static Time operator %(Time left, Time right) => FromMicroseconds(left.AsMicroseconds() % right.AsMicroseconds());

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override int GetHashCode() => _microseconds.GetHashCode();

        private readonly long _microseconds;

        #region Imports
        [DllImport(CSFML.System, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Time sfSeconds(float amount);

        [DllImport(CSFML.System, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Time sfMilliseconds(int amount);

        [DllImport(CSFML.System, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Time sfMicroseconds(long amount);
        #endregion
    }
}
