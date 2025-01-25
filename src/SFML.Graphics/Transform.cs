using System.Runtime.InteropServices;
using System.Security;
using SFML.System;

// TODO REIMPLEMENT WITH 4x4 MATRIX

namespace SFML.Graphics
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Define a 3x3 transform matrix
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct Transform
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct a transform from a 3x3 matrix
        /// </summary>
        /// <param name="a00">Element (0, 0) of the matrix</param>
        /// <param name="a01">Element (0, 1) of the matrix</param>
        /// <param name="a02">Element (0, 2) of the matrix</param>
        /// <param name="a10">Element (1, 0) of the matrix</param>
        /// <param name="a11">Element (1, 1) of the matrix</param>
        /// <param name="a12">Element (1, 2) of the matrix</param>
        /// <param name="a20">Element (2, 0) of the matrix</param>
        /// <param name="a21">Element (2, 1) of the matrix</param>
        /// <param name="a22">Element (2, 2) of the matrix</param>
        ////////////////////////////////////////////////////////////
        public Transform(float a00, float a01, float a02,
                         float a10, float a11, float a12,
                         float a20, float a21, float a22)
        {
            M00 = a00;
            M01 = a01;
            M02 = a02;
            M10 = a10;
            M11 = a11;
            M12 = a12;
            M20 = a20;
            M21 = a21;
            M22 = a22;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Return the inverse of the transform.
        /// 
        /// If the inverse cannot be computed, an identity transform
        /// is returned.
        /// </summary>
        /// <returns>A new transform which is the inverse of self</returns>
        ////////////////////////////////////////////////////////////
        public Transform GetInverse() => sfTransform_getInverse(ref this);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Transform a 2D point.
        /// </summary>
        /// <param name="point">Point to transform</param>
        /// <returns>Transformed point</returns>
        ////////////////////////////////////////////////////////////
        public Vector2f TransformPoint(Vector2f point) => sfTransform_transformPoint(ref this, point);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Transform a rectangle.
        /// 
        /// Since SFML doesn't provide support for oriented rectangles,
        /// the result of this function is always an axis-aligned
        /// rectangle. Which means that if the transform contains a
        /// rotation, the bounding rectangle of the transformed rectangle
        /// is returned.
        /// </summary>
        /// <param name="rectangle">Rectangle to transform</param>
        /// <returns>Transformed rectangle</returns>
        ////////////////////////////////////////////////////////////
        public FloatRect TransformRect(FloatRect rectangle) => sfTransform_transformRect(ref this, rectangle);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Combine the current transform with another one.
        /// 
        /// The result is a transform that is equivalent to applying
        /// this followed by transform. Mathematically, it is
        /// equivalent to a matrix multiplication.
        /// </summary>
        /// <param name="transform">Transform to combine to this transform</param>
        ////////////////////////////////////////////////////////////
        public void Combine(Transform transform) => sfTransform_combine(ref this, ref transform);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Combine the current transform with a translation.
        /// </summary>
        /// <param name="offset">Translation offset to apply</param>
        ////////////////////////////////////////////////////////////
        public void Translate(Vector2f offset) => sfTransform_translate(ref this, offset);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Combine the current transform with a rotation.
        /// </summary>
        /// <param name="angle">Rotation angle</param>
        ////////////////////////////////////////////////////////////
        public void Rotate(Angle angle) => sfTransform_rotate(ref this, angle.Degrees);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Combine the current transform with a rotation.
        /// 
        /// The center of rotation is provided for convenience as a second
        /// argument, so that you can build rotations around arbitrary points
        /// more easily (and efficiently) than the usual
        /// Translate(-center); Rotate(angle); Translate(center).
        /// </summary>
        /// <param name="angle">Rotation angle</param>
        /// <param name="center">Center of rotation</param>
        ////////////////////////////////////////////////////////////
        public void Rotate(Angle angle, Vector2f center) => sfTransform_rotateWithCenter(ref this, angle.Degrees, center);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Combine the current transform with a scaling.
        /// </summary>
        /// <param name="factors">Scaling factors</param>
        ////////////////////////////////////////////////////////////
        public void Scale(Vector2f factors) => sfTransform_scale(ref this, factors);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Combine the current transform with a scaling.
        /// 
        /// The center of scaling is provided for convenience as a second
        /// argument, so that you can build scaling around arbitrary points
        /// more easily (and efficiently) than the usual
        /// Translate(-center); Scale(factors); Translate(center).
        /// </summary>
        /// <param name="factors">Scaling factors</param>
        /// <param name="center">Center of scaling</param>
        ////////////////////////////////////////////////////////////
        public void Scale(Vector2f factors, Vector2f center) => sfTransform_scaleWithCenter(ref this, factors, center);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Compare Transform and object and checks if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>Object and transform are equal</returns>
        ////////////////////////////////////////////////////////////
        public override bool Equals(object obj) => (obj is Transform transform) && Equals(transform);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Compare two transforms for equality
        ///
        /// Performs an element-wise comparison of the elements of this
        /// transform with the elements of the right transform.
        /// </summary>
        /// <param name="transform">Transform to check</param>
        /// <returns>Transforms are equal</returns>
        ////////////////////////////////////////////////////////////
        public bool Equals(Transform transform) => sfTransform_equal(ref this, ref transform);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Generates a hash code by XORing together the internal 3x3 matrix.
        /// </summary>
        /// <returns>XOR'd Hash of floats contained.</returns>
        ////////////////////////////////////////////////////////////
        public override int GetHashCode()
        {
            var hash0 = M00.GetHashCode() ^ M01.GetHashCode() ^ M02.GetHashCode();
            var hash1 = M10.GetHashCode() ^ M11.GetHashCode() ^ M12.GetHashCode();
            var hash2 = M20.GetHashCode() ^ M21.GetHashCode() ^ M22.GetHashCode();
            return hash0 ^ hash1 ^ hash2;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of binary operator * to combine two transforms.
        /// This call is equivalent to calling new Transform(left).Combine(right).
        /// </summary>
        /// <param name="left">Left operand (the first transform)</param>
        /// <param name="right">Right operand (the second transform)</param>
        /// <returns>New combined transform</returns>
        ////////////////////////////////////////////////////////////
        public static Transform operator *(Transform left, Transform right)
        {
            left.Combine(right);
            return left;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of binary operator * to transform a point.
        /// This call is equivalent to calling left.TransformPoint(right).
        /// </summary>
        /// <param name="left">Left operand (the transform)</param>
        /// <param name="right">Right operand (the point to transform)</param>
        /// <returns>New transformed point</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2f operator *(Transform left, Vector2f right) => left.TransformPoint(right);

        ////////////////////////////////////////////////////////////
        /// <summary>The identity transform (does nothing)</summary>
        ////////////////////////////////////////////////////////////
        public static Transform Identity => new Transform(1, 0, 0,
                                     0, 1, 0,
                                     0, 0, 1);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => string.Format("[Transform]" +
                   " Matrix(" +
                   "{0}, {1}, {2}," +
                   "{3}, {4}, {5}," +
                   "{6}, {7}, {8}, )",
                   M00, M01, M02,
                   M10, M11, M12,
                   M20, M21, M22);

        internal float M00, M01, M02;
        internal float M10, M11, M12;
        internal float M20, M21, M22;

        #region Imports
        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Transform sfTransform_getInverse(ref Transform transform);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f sfTransform_transformPoint(ref Transform transform, Vector2f point);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern FloatRect sfTransform_transformRect(ref Transform transform, FloatRect rectangle);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfTransform_combine(ref Transform transform, ref Transform other);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfTransform_translate(ref Transform transform, Vector2f offset);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfTransform_rotate(ref Transform transform, float angle);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfTransform_rotateWithCenter(ref Transform transform, float angle, Vector2f center);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfTransform_scale(ref Transform transform, Vector2f scale);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfTransform_scaleWithCenter(ref Transform transform, Vector2f scale, Vector2f center);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfTransform_equal(ref Transform left, ref Transform right);
        #endregion
    }
}
