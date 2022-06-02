using System.Runtime.InteropServices;
using SFML.System;

namespace SFML.Graphics.Glsl
{
    #region 2D Vectors
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// <see cref="Vec2"/> is a struct represent a glsl vec2 value
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct Vec2
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Implicit cast from <see cref="Vector2f"/> to <see cref="Vec2"/>
        /// </summary>
        public static implicit operator Vec2(Vector2f vec) => new Vec2(vec);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the <see cref="Vec2"/> from its coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        ////////////////////////////////////////////////////////////
        public Vec2(float x, float y)
        {
            X = x;
            Y = y;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the <see cref="Vec2"/> from a standard SFML <see cref="Vector2f"/>
        /// </summary>
        /// <param name="vec">A standard SFML 2D vector</param>
        ////////////////////////////////////////////////////////////
        public Vec2(Vector2f vec)
        {
            X = vec.X;
            Y = vec.Y;
        }

        /// <summary>Horizontal component of the vector</summary>
        public float X;

        /// <summary>Vertical component of the vector</summary>
        public float Y;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// <see cref="Ivec2"/> is a struct represent a glsl ivec2 value
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct Ivec2
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Implicit cast from <see cref="Vector2i"/> to <see cref="Ivec2"/>
        /// </summary>
        public static implicit operator Ivec2(Vector2i vec) => new Ivec2(vec);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the <see cref="Ivec2"/> from its coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        ////////////////////////////////////////////////////////////
        public Ivec2(int x, int y)
        {
            X = x;
            Y = y;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the <see cref="Ivec2"/> from a standard SFML <see cref="Vector2i"/>
        /// </summary>
        /// <param name="vec">A standard SFML 2D integer vector</param>
        ////////////////////////////////////////////////////////////
        public Ivec2(Vector2i vec)
        {
            X = vec.X;
            Y = vec.Y;
        }

        /// <summary>Horizontal component of the vector</summary>
        public int X;

        /// <summary>Vertical component of the vector</summary>
        public int Y;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// <see cref="Bvec2"/> is a struct represent a glsl bvec2 value
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct Bvec2
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the <see cref="Bvec2"/> from its coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        ////////////////////////////////////////////////////////////
        public Bvec2(bool x, bool y)
        {
            X = x;
            Y = y;
        }

        /// <summary>Horizontal component of the vector</summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool X;

        /// <summary>Vertical component of the vector</summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool Y;
    }
    #endregion

    #region 3D Vectors
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// <see cref="Vec3"/> is a struct represent a glsl vec3 value
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct Vec3
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Implicit cast from <see cref="Vector3f"/> to <see cref="Vec3"/>
        /// </summary>
        public static implicit operator Vec3(Vector3f vec) => new Vec3(vec);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the <see cref="Vec3"/> from its coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        ////////////////////////////////////////////////////////////
        public Vec3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the <see cref="Vec3"/> from a standard SFML <see cref="Vector3f"/>
        /// </summary>
        /// <param name="vec">A standard SFML 3D vector</param>
        ////////////////////////////////////////////////////////////
        public Vec3(Vector3f vec)
        {
            X = vec.X;
            Y = vec.Y;
            Z = vec.Z;
        }

        /// <summary>Horizontal component of the vector</summary>
        public float X;

        /// <summary>Vertical component of the vector</summary>
        public float Y;

        /// <summary>Depth component of the vector</summary>
        public float Z;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// <see cref="Ivec3"/> is a struct represent a glsl ivec3 value
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct Ivec3
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the <see cref="Ivec3"/> from its coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        ////////////////////////////////////////////////////////////
        public Ivec3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>Horizontal component of the vector</summary>
        public int X;

        /// <summary>Vertical component of the vector</summary>
        public int Y;

        /// <summary>Depth component of the vector</summary>
        public int Z;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// <see cref="Bvec3"/> is a struct represent a glsl bvec3 value
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct Bvec3
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the <see cref="Bvec3"/> from its coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        ////////////////////////////////////////////////////////////
        public Bvec3(bool x, bool y, bool z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>Horizontal component of the vector</summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool X;

        /// <summary>Vertical component of the vector</summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool Y;

        /// <summary>Depth component of the vector</summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool Z;
    }
    #endregion

    #region 4D Vectors
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// <see cref="Vec4"/> is a struct represent a glsl vec4 value
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct Vec4
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the <see cref="Vec4"/> from its coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        /// <param name="w">W coordinate</param>
        ////////////////////////////////////////////////////////////
        public Vec4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Construct the <see cref="Vec4"/> from a <see cref="Color"/>
        /// </summary>
        /// <remarks>
        /// The <see cref="Color"/>'s values will be normalized from 0..255 to 0..1
        /// </remarks>
        /// <param name="color">A SFML <see cref="Color"/> to be translated to a 4D floating-point vector</param>
        public Vec4(Color color)
        {
            X = color.R / 255.0f;
            Y = color.G / 255.0f;
            Z = color.B / 255.0f;
            W = color.A / 255.0f;
        }

        /// <summary>Horizontal component of the vector</summary>
        public float X;

        /// <summary>Vertical component of the vector</summary>
        public float Y;

        /// <summary>Depth component of the vector</summary>
        public float Z;

        /// <summary>Projective/Homogenous component of the vector</summary>
        public float W;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// <see cref="Ivec4"/> is a struct represent a glsl ivec4 value
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct Ivec4
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the <see cref="Ivec4"/> from its coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        /// <param name="w">W coordinate</param>
        ////////////////////////////////////////////////////////////
        public Ivec4(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Construct the <see cref="Ivec4"/> from a <see cref="Color"/>
        /// </summary>
        /// <param name="color">A SFML <see cref="Color"/> to be translated to a 4D integer vector</param>
        public Ivec4(Color color)
        {
            X = color.R;
            Y = color.G;
            Z = color.B;
            W = color.A;
        }

        /// <summary>Horizontal component of the vector</summary>
        public int X;

        /// <summary>Vertical component of the vector</summary>
        public int Y;

        /// <summary>Depth component of the vector</summary>
        public int Z;

        /// <summary>Projective/Homogenous component of the vector</summary>
        public int W;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// <see cref="Bvec4"/> is a struct represent a glsl bvec4 value
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct Bvec4
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the <see cref="Bvec4"/> from its coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="z">Z coordinate</param>
        /// <param name="w">W coordinate</param>
        ////////////////////////////////////////////////////////////
        public Bvec4(bool x, bool y, bool z, bool w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>Horizontal component of the vector</summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool X;

        /// <summary>Vertical component of the vector</summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool Y;

        /// <summary>Depth component of the vector</summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool Z;

        /// <summary>Projective/Homogenous component of the vector</summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool W;
    }
    #endregion

    #region Matrices
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// <see cref="Mat3"/> is a struct representing a glsl mat3 value
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Mat3
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the <see cref="Mat3"/> from its components
        /// </summary>
        ///
        /// <remarks>
        /// Arguments are in row-major order
        /// </remarks>
        ////////////////////////////////////////////////////////////
        public Mat3(float a00, float a01, float a02,
                    float a10, float a11, float a12,
                    float a20, float a21, float a22)
        {
            fixed (float* m = array)
            {
                m[0] = a00;
                m[3] = a01;
                m[6] = a02;
                m[1] = a10;
                m[4] = a11;
                m[7] = a12;
                m[2] = a20;
                m[5] = a21;
                m[8] = a22;
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the <see cref="Mat3"/> from a SFML <see cref="Transform"/>
        /// </summary>
        /// <param name="transform">A SFML <see cref="Transform"/></param>
        ////////////////////////////////////////////////////////////
        public Mat3(Transform transform)
        {
            fixed (float* m = array)
            {
                m[0] = transform.m00;
                m[3] = transform.m01;
                m[6] = transform.m02;
                m[1] = transform.m10;
                m[4] = transform.m11;
                m[7] = transform.m12;
                m[2] = transform.m20;
                m[5] = transform.m21;
                m[8] = transform.m22;
            }
        }

        // column-major!
        private fixed float array[3 * 3];
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// <see cref="Mat4"/> is a struct representing a glsl mat4 value
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Mat4
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provides easy-access to an identity matrix
        /// </summary>
        ///
        /// <remarks>
        /// Keep in mind that a Mat4 cannot be modified after construction
        /// </remarks>
        ////////////////////////////////////////////////////////////
        public static Mat4 Identity
        {
            get
            {
                return new Mat4(1, 0, 0, 0,
                                0, 1, 0, 0,
                                0, 0, 1, 0,
                                0, 0, 0, 1);
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the <see cref="Mat4"/> from its components
        /// </summary>
        ///
        /// <remarks>
        /// Arguments are in row-major order
        /// </remarks>
        ////////////////////////////////////////////////////////////
        public Mat4(float a00, float a01, float a02, float a03,
                    float a10, float a11, float a12, float a13,
                    float a20, float a21, float a22, float a23,
                    float a30, float a31, float a32, float a33)
        {
            fixed (float* m = array)
            {
                // transpose to column major
                m[0] = a00;
                m[4] = a01;
                m[8] = a02;
                m[12] = a03;
                m[1] = a10;
                m[5] = a11;
                m[9] = a12;
                m[13] = a13;
                m[2] = a20;
                m[6] = a21;
                m[10] = a22;
                m[14] = a23;
                m[3] = a30;
                m[7] = a31;
                m[11] = a32;
                m[15] = a33;
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the <see cref="Mat3"/> from a SFML <see cref="Transform"/> and expand it to a 4x4 matrix
        /// </summary>
        /// <param name="transform">A SFML <see cref="Transform"/></param>
        ////////////////////////////////////////////////////////////
        public Mat4(Transform transform)
        {
            fixed (float* m = array)
            {
                // swapping to column-major (OpenGL) from row-major (SFML) order
                // in addition, filling in the blanks (from expanding to a mat4) with values from
                // an identity matrix
                m[0] = transform.m00;
                m[4] = transform.m01;
                m[8] = 0;
                m[12] = transform.m02;
                m[1] = transform.m10;
                m[5] = transform.m11;
                m[9] = 0;
                m[13] = transform.m12;
                m[2] = 0;
                m[6] = 0;
                m[10] = 1;
                m[14] = 0;
                m[3] = transform.m20;
                m[7] = transform.m21;
                m[11] = 0;
                m[15] = transform.m22;
            }
        }

        // column major!
        private fixed float array[4 * 4];
    }
    #endregion
}
