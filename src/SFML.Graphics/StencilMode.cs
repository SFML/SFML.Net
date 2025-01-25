using System.Runtime.InteropServices;

namespace SFML.Graphics
{
    ////////////////////////////////////////////////////////
    /// <summary>
    /// Enumeration of the stencil test comparisons that can be performed
    /// <para/>
    /// The comparisons are mapped directly to their OpenGL equivalents,
    /// specified by glStencilFunc().
    /// </summary>
    ////////////////////////////////////////////////////////
    public enum StencilComparison
    {
        /// <summary>The stencil test never passes</summary>
        Never,

        /// <summary>The stencil test passes if the new value is less than the value in the stencil buffer</summary>
        Less,

        /// <summary>The stencil test passes if the new value is less than or equal to the value in the stencil buffer</summary>
        LessEqual,

        /// <summary>The stencil test passes if the new value is greater than the value in the stencil buffer</summary>
        Greater,

        /// <summary>The stencil test passes if the new value is greater than or equal to the value in the stencil buffer</summary>
        GreaterEqual,

        /// <summary>The stencil test passes if the new value is strictly equal to the value in the stencil buffer</summary>
        Equal,

        /// <summary>The stencil test passes if the new value is strictly inequal to the value in the stencil buffer</summary>
        NotEqual,

        /// <summary>The stencil test always passes</summary>
        Always
    }

    ////////////////////////////////////////////////////////
    /// <summary>
    /// Enumeration of the stencil buffer update operations
    /// <para/>
    /// The update operations are mapped directly to their OpenGL equivalents,
    /// specified by glStencilOp().
    /// </summary>
    ////////////////////////////////////////////////////////
    public enum StencilUpdateOperation
    {
        /// <summary>If the stencil test passes, the value in the stencil buffer is not modified</summary>
        Keep,

        /// <summary>If the stencil test passes, the value in the stencil buffer is set to zero</summary>
        Zero,

        /// <summary>If the stencil test passes, the value in the stencil buffer is set to the new value</summary>
        Replace,

        /// <summary>If the stencil test passes, the value in the stencil buffer is incremented and if required clamped</summary>
        Increment,

        /// <summary>If the stencil test passes, the value in the stencil buffer is decremented and if required clamped</summary>
        Decrement,

        /// <summary>If the stencil test passes, the value in the stencil buffer is bitwise inverted</summary>
        Invert
    }

    ////////////////////////////////////////////////////////
    /// <summary>
    /// Stencil value type (also used as a mask)
    /// </summary>
    ////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct StencilValue
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// The stored stencil value
        /// </summary>
        ////////////////////////////////////////////////////////////
        public uint Value;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Convert a signed integer to a stencil value
        /// </summary>
        ////////////////////////////////////////////////////////////
        public static explicit operator StencilValue(int value) => new StencilValue() { Value = (uint)value };

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Convert an unsigned integer to a stencil value
        /// </summary>
        ////////////////////////////////////////////////////////////
        public static explicit operator StencilValue(uint value) => new StencilValue() { Value = value };
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Stencil modes for drawing
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct StencilMode
    {
        /// <summary>
        /// The comparison we're performing the stencil test with
        /// </summary>
        public StencilComparison StencilComparison;

        /// <summary>
        /// The update operation to perform if the stencil test passes
        /// </summary>
        public StencilUpdateOperation StencilUpdateOperation;

        /// <summary>
        /// The reference value we're performing the stencil test with
        /// </summary>
        public uint StencilReference;

        /// <summary>
        /// The mask to apply to both the reference value and the value in the stencil buffer
        /// </summary>
        public uint StencilMask;

        /// <summary>
        /// Whether we should update the color buffer in addition to the stencil buffer
        /// </summary>
        [MarshalAs(UnmanagedType.I1)]
        public bool StencilOnly;

        /// <summary>
        /// Default values for stencil mode
        /// </summary>
        public static readonly StencilMode Default = new StencilMode()
        {
            StencilComparison = StencilComparison.Always,
            StencilUpdateOperation = StencilUpdateOperation.Keep,
            StencilReference = 0,
            StencilMask = ~0u,
            StencilOnly = false
        };

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Compare two stencil modes and checks if they are equal
        /// </summary>
        /// <returns>Stencil Modes are equal</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator ==(StencilMode left, StencilMode right) => left.Equals(right);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Compare two stencil modes and checks if they are not equal
        /// </summary>
        /// <returns>Stencil Modes are not equal</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator !=(StencilMode left, StencilMode right) => !left.Equals(right);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Compare stencil mode and object and checks if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>Object and stencil mode are equal</returns>
        ////////////////////////////////////////////////////////////
        public override bool Equals(object obj) => obj is StencilMode mode && Equals(mode);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override int GetHashCode() =>
            StencilComparison.GetHashCode() ^
            StencilUpdateOperation.GetHashCode() ^
            StencilReference.GetHashCode() ^
            StencilMask.GetHashCode() ^
            StencilOnly.GetHashCode();

        ///////////////////////////////////////////////////////////
        /// <summary>
        /// Compare two stencil modes and checks if they are equal
        /// </summary>
        /// <param name="other">Stencil mode to check</param>
        /// <returns>Stencil modes are equal</returns>
        ////////////////////////////////////////////////////////////
        public bool Equals(StencilMode other) =>
            StencilComparison == other.StencilComparison &&
            StencilUpdateOperation == other.StencilUpdateOperation &&
            StencilReference == other.StencilReference &&
            StencilMask == other.StencilMask &&
            StencilOnly == other.StencilOnly;
    }
}
