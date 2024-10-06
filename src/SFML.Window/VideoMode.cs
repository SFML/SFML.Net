using System;
using System.Runtime.InteropServices;
using System.Security;
using SFML.System;

namespace SFML.Window
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// VideoMode defines a video mode (width, height, bpp, frequency)
    /// and provides static functions for getting modes supported
    /// by the display device
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct VideoMode
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the video mode with its width and height
        /// </summary>
        /// <param name="size">Video mode size</param>
        ////////////////////////////////////////////////////////////
        public VideoMode(Vector2u size) :
            this(size, 32)
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the video mode with its width, height and depth
        /// </summary>
        /// <param name="size">Video mode size</param>
        /// <param name="bpp">Video mode depth (bits per pixel)</param>
        ////////////////////////////////////////////////////////////
        public VideoMode(Vector2u size, uint bpp)
        {
            Size = size;
            BitsPerPixel = bpp;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Tell whether or not the video mode is supported
        /// </summary>
        /// <returns>True if the video mode is valid, false otherwise</returns>
        ////////////////////////////////////////////////////////////
        public bool IsValid() => sfVideoMode_isValid(this);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the list of all the supported fullscreen video modes
        /// </summary>
        ////////////////////////////////////////////////////////////
        public static VideoMode[] FullscreenModes
        {
            get
            {
                unsafe
                {
                    var modesPtr = sfVideoMode_getFullscreenModes(out var count);
                    var modes = new VideoMode[(int)count];
                    for (var i = 0; i < (int)count; ++i)
                    {
                        modes[i] = modesPtr[i];
                    }

                    return modes;
                }
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the current desktop video mode
        /// </summary>
        ////////////////////////////////////////////////////////////
        public static VideoMode DesktopMode => sfVideoMode_getDesktopMode();

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => "[VideoMode]" +
                   $" Size({Size})" +
                   $" BitsPerPixel({BitsPerPixel})";

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Compare video mode and object and checks if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>Object and video mode are equal</returns>
        ////////////////////////////////////////////////////////////
        public override bool Equals(object obj) => (obj is VideoMode mode) && Equals(mode);

        ///////////////////////////////////////////////////////////
        /// <summary>
        /// Compare two video modes and checks if they are equal
        /// </summary>
        /// <param name="other">Video modes to check</param>
        /// <returns>Video modes are equal</returns>
        ////////////////////////////////////////////////////////////
        public bool Equals(VideoMode other) => (Size == other.Size) &&
                   (BitsPerPixel == other.BitsPerPixel);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override int GetHashCode() => Size.GetHashCode() ^ BitsPerPixel.GetHashCode();

        /// <summary>Video mode width and height, in pixels</summary>
        public Vector2u Size;

        /// <summary>Video mode depth, in bits per pixel</summary>
        public uint BitsPerPixel;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of == operator to compare two video modes
        /// </summary>
        /// <param name="left">Left operand (a video mode)</param>
        /// <param name="right">Right operand (a video mode)</param>
        /// <returns>True if modes are equal</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator ==(VideoMode left, VideoMode right) => left.Equals(right);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of != operator to compare two video modes
        /// </summary>
        /// <param name="left">Left operand (a video mode)</param>
        /// <param name="right">Right operand (a video mode)</param>
        /// <returns>True if modes are different</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator !=(VideoMode left, VideoMode right) => !left.Equals(right);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of &lt; operator to compare two video modes
        /// </summary>
        /// <param name="left">Left operand (a video mode)</param>
        /// <param name="right">Right operand (a video mode)</param>
        /// <returns>True if <paramref name="left"/> is less than <paramref name="right"/></returns>
        ////////////////////////////////////////////////////////////
        public static bool operator <(VideoMode left, VideoMode right)
        {
            if (left.BitsPerPixel == right.BitsPerPixel)
            {
                if (left.Size.X == right.Size.X)
                {
                    return left.Size.Y < right.Size.Y;
                }

                return left.Size.X < right.Size.X;
            }

            return left.BitsPerPixel < right.BitsPerPixel;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of &lt;= operator to compare two video modes
        /// </summary>
        /// <param name="left">Left operand (a video mode)</param>
        /// <param name="right">Right operand (a video mode)</param>
        /// <returns>True if <paramref name="left"/> is less than or equal to <paramref name="right"/></returns>
        ////////////////////////////////////////////////////////////
        public static bool operator <=(VideoMode left, VideoMode right) => !(right < left);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of &gt; operator to compare two video modes
        /// </summary>
        /// <param name="left">Left operand (a video mode)</param>
        /// <param name="right">Right operand (a video mode)</param>
        /// <returns>True if <paramref name="left"/> is greater than <paramref name="right"/></returns>
        ////////////////////////////////////////////////////////////
        public static bool operator >(VideoMode left, VideoMode right) => right < left;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Overload of &gt;= operator to compare two video modes
        /// </summary>
        /// <param name="left">Left operand (a video mode)</param>
        /// <param name="right">Right operand (a video mode)</param>
        /// <returns>True if <paramref name="left"/> is greater than or equal to <paramref name="right"/></returns>
        ////////////////////////////////////////////////////////////
        public static bool operator >=(VideoMode left, VideoMode right) => !(left < right);

        #region Imports
        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern VideoMode sfVideoMode_getDesktopMode();

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe VideoMode* sfVideoMode_getFullscreenModes(out UIntPtr count);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfVideoMode_isValid(VideoMode mode);
        #endregion
    }
}
