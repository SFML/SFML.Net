using System;
using System.Runtime.InteropServices;
using System.Security;
using SFML.Window;

namespace SFML
{
    namespace Graphics
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Define a 3x3 transform matrix
        /// </summary>
        ////////////////////////////////////////////////////////////
        public class Transform : ObjectBase
        {
            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Default constructor.
            ///
            /// Creates an identity transform (a transform that does nothing).
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Transform() :
                base(sfTransform_create())
            {
            }

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
                             float a20, float a21, float a22) :
                base(sfTransform_createFromMatrix(a00, a01, a02,
                                                  a10, a11, a12,
                                                  a20, a21, a22))
            {
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the transform from another
            /// </summary>
            /// <param name="transform">Transform to copy</param>
            ////////////////////////////////////////////////////////////
            public Transform(Transform transform) :
                this(sfTransform_copy(transform.CPointer))
            {
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Return the transform as a 4x4 matrix.
            /// 
            /// This function returns an array of 16 floats containing
            /// the transform elements as a 4x4 matrix, which
            /// is directly compatible with OpenGL functions.
            /// </summary>
            /// <returns>Array containing the 4x4 matrix</returns>
            ////////////////////////////////////////////////////////////
            public float[] GetMatrix()
            {
                float[] matrix = new float[4 * 4];
                Marshal.Copy(sfTransform_getMatrix(CPointer), matrix, 0, matrix.Length);
                return matrix;
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
            public Transform GetInverse()
            {
                IntPtr cPointer = IntPtr.Zero;
                sfTransform_getInverse(CPointer, out cPointer);
                return new Transform(cPointer);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Transform a 2D point.
            /// </summary>
            /// <param name="x">X coordinate of the point to transform</param>
            /// <param name="y">Y coordinate of the point to transform</param>
            /// <returns>Transformed point</returns>
            ////////////////////////////////////////////////////////////
            public Vector2f TransformPoint(float x, float y)
            {
                return TransformPoint(new Vector2f(x, y));
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Transform a 2D point.
            /// </summary>
            /// <param name="point">Point to transform</param>
            /// <returns>Transformed point</returns>
            ////////////////////////////////////////////////////////////
            public Vector2f TransformPoint(Vector2f point)
            {
                return sfTransform_transformPoint(CPointer, point);
            }

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
            public FloatRect TransformRect(FloatRect rectangle)
            {
                return sfTransform_transformRect(CPointer, rectangle);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Combine the current transform with another one.
            /// 
            /// The result is a transform that is equivalent to applying
            /// this followed by transform. Mathematically, it is
            /// equivalent to a matrix multiplication.
            /// This function returns a reference to self, so that calls
            /// can be chained.
            /// </summary>
            /// <param name="transform">Transform to combine to this transform</param>
            /// <returns>Reference to self</returns>
            ////////////////////////////////////////////////////////////
            public Transform Combine(Transform transform)
            {
                sfTransform_combine(CPointer, transform.CPointer);
                return this;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Combine the current transform with a translation.
            /// 
            /// This function returns a reference to self, so that calls
            /// can be chained.
            /// </summary>
            /// <param name="x">Offset to apply on X axis</param>
            /// <param name="y">Offset to apply on Y axis</param>
            /// <returns>Reference to self</returns>
            ////////////////////////////////////////////////////////////
            public Transform Translate(float x, float y)
            {
                sfTransform_translate(CPointer, x, y);
                return this;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Combine the current transform with a translation.
            /// 
            /// This function returns a reference to self, so that calls
            /// can be chained.
            /// </summary>
            /// <param name="offset">Translation offset to apply</param>
            /// <returns>Reference to self</returns>
            ////////////////////////////////////////////////////////////
            public Transform Translate(Vector2f offset)
            {
                return Translate(offset.X, offset.Y);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Combine the current transform with a rotation.
            /// 
            /// This function returns a reference to self, so that calls
            /// can be chained.
            /// </summary>
            /// <param name="angle">Rotation angle, in degrees</param>
            /// <returns>Reference to self</returns>
            ////////////////////////////////////////////////////////////
            public Transform Rotate(float angle)
            {
                sfTransform_rotate(CPointer, angle);
                return this;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Combine the current transform with a rotation.
            /// 
            /// The center of rotation is provided for convenience as a second
            /// argument, so that you can build rotations around arbitrary points
            /// more easily (and efficiently) than the usual
            /// Translate(-center).Rotate(angle).Translate(center).
            /// This function returns a reference to self, so that calls
            /// can be chained.
            /// </summary>
            /// <param name="angle">Rotation angle, in degrees</param>
            /// <param name="centerX">X coordinate of the center of rotation</param>
            /// <param name="centerY">Y coordinate of the center of rotation</param>
            /// <returns>Reference to self</returns>
            ////////////////////////////////////////////////////////////
            public Transform Rotate(float angle, float centerX, float centerY)
            {
                sfTransform_rotateWithCenter(CPointer, angle, centerX, centerY);
                return this;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Combine the current transform with a rotation.
            /// 
            /// The center of rotation is provided for convenience as a second
            /// argument, so that you can build rotations around arbitrary points
            /// more easily (and efficiently) than the usual
            /// Translate(-center).Rotate(angle).Translate(center).
            /// This function returns a reference to self, so that calls
            /// can be chained.
            /// </summary>
            /// <param name="angle">Rotation angle, in degrees</param>
            /// <param name="center">Center of rotation</param>
            /// <returns>Reference to self</returns>
            ////////////////////////////////////////////////////////////
            public Transform Rotate(float angle, Vector2f center)
            {
                return Rotate(angle, center.X, center.Y);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Combine the current transform with a scaling.
            /// 
            /// This function returns a reference to self, so that calls
            /// can be chained.
            /// </summary>
            /// <param name="scaleX">Scaling factor on the X axis</param>
            /// <param name="scaleY">Scaling factor on the Y axis</param>
            /// <returns>Reference to self</returns>
            ////////////////////////////////////////////////////////////
            public Transform Scale(float scaleX, float scaleY)
            {
                sfTransform_scale(CPointer, scaleX, scaleY);
                return this;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Combine the current transform with a scaling.
            /// 
            /// The center of scaling is provided for convenience as a second
            /// argument, so that you can build scaling around arbitrary points
            /// more easily (and efficiently) than the usual
            /// Translate(-center).Scale(factors).Translate(center).
            /// This function returns a reference to self, so that calls
            /// can be chained.
            /// </summary>
            /// <param name="scaleX">Scaling factor on X axis</param>
            /// <param name="scaleY">Scaling factor on Y axis</param>
            /// <param name="centerX">X coordinate of the center of scaling</param>
            /// <param name="centerY">Y coordinate of the center of scaling</param>
            /// <returns>Reference to self</returns>
            ////////////////////////////////////////////////////////////
            public Transform Scale(float scaleX, float scaleY, float centerX, float centerY)
            {
                sfTransform_scaleWithCenter(CPointer, scaleX, scaleY, centerX, centerY);
                return this;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Combine the current transform with a scaling.
            /// 
            /// This function returns a reference to self, so that calls
            /// can be chained.
            /// </summary>
            /// <param name="factors">Scaling factors</param>
            /// <returns>Reference to self</returns>
            ////////////////////////////////////////////////////////////
            public Transform Scale(Vector2f factors)
            {
                return Scale(factors.X, factors.Y);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Combine the current transform with a scaling.
            /// 
            /// The center of scaling is provided for convenience as a second
            /// argument, so that you can build scaling around arbitrary points
            /// more easily (and efficiently) than the usual
            /// Translate(-center).Scale(factors).Translate(center).
            /// This function returns a reference to self, so that calls
            /// can be chained.
            /// </summary>
            /// <param name="factors">Scaling factors</param>
            /// <param name="center">Center of scaling</param>
            /// <returns>Reference to self</returns>
            ////////////////////////////////////////////////////////////
            public Transform Scale(Vector2f factors, Vector2f center)
            {
                return Scale(factors.X, factors.Y, center.X, center.Y);
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
                return new Transform(left).Combine(right);
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
            public static Vector2f operator *(Transform left, Vector2f right)
            {
                return left.TransformPoint(right);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>The identity transform (does nothing)</summary>
            ////////////////////////////////////////////////////////////
            public static Transform Identity
            {
                get { return new Transform(); }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Provide a string describing the object
            /// </summary>
            /// <returns>String description of the object</returns>
            ////////////////////////////////////////////////////////////
            public override string ToString()
            {
                return "[Transform]" +
                       " Matrix(" + GetMatrix() + ")";
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Internal constructor
            /// </summary>
            /// <param name="cPointer">Pointer to the object in C library</param>
            ////////////////////////////////////////////////////////////
            internal Transform(IntPtr cPointer) :
                base(cPointer)
            {
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Handle the destruction of the object
            /// </summary>
            /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
            ////////////////////////////////////////////////////////////
            protected override void Destroy(bool disposing)
            {
                sfTransform_destroy(CPointer);
            }

            #region Imports
            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfTransform_create();

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfTransform_createFromMatrix(float a00, float a01, float a02,
                                                              float a10, float a11, float a12,
                                                              float a20, float a21, float a22);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfTransform_copy(IntPtr transform);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfTransform_destroy(IntPtr transform);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfTransform_getMatrix(IntPtr transform);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfTransform_getInverse(IntPtr transform, out IntPtr inverse);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Vector2f sfTransform_transformPoint(IntPtr transform, Vector2f point);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern FloatRect sfTransform_transformRect(IntPtr transform, FloatRect rectangle);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfTransform_combine(IntPtr transform, IntPtr other);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfTransform_translate(IntPtr transform, float x, float y);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfTransform_rotate(IntPtr transform, float angle);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfTransform_rotateWithCenter(IntPtr transform, float angle, float centerX, float centerY);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfTransform_scale(IntPtr transform, float scaleX, float scaleY);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfTransform_scaleWithCenter( IntPtr transform, float scaleX, float scaleY, float centerX, float centerY);

            #endregion
        }
    }
}
