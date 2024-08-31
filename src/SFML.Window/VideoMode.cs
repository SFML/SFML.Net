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
        /// <param name="width">Video mode width</param>
        /// <param name="height">Video mode height</param>
        ////////////////////////////////////////////////////////////
        public VideoMode(uint width, uint height) :
            this(width, height, 32)
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the video mode with its width, height and depth
        /// </summary>
        /// <param name="width">Video mode width</param>
        /// <param name="height">Video mode height</param>
        /// <param name="bpp">Video mode depth (bits per pixel)</param>
        ////////////////////////////////////////////////////////////
        public VideoMode(uint width, uint height, uint bpp)
        {
            Width = width;
            Height = height;
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
                    var ModesPtr = sfVideoMode_getFullscreenModes(out var Count);
                    var Modes = new VideoMode[(int)Count];
                    for (var i = 0; i < (int)Count; ++i)
                    {
                        Modes[i] = ModesPtr[i];
                    }

                    return Modes;
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
                   " Width(" + Width + ")" +
                   " Height(" + Height + ")" +
                   " BitsPerPixel(" + BitsPerPixel + ")";

        /// <summary>Video mode width, in pixels</summary>
        public uint Width;

        /// <summary>Video mode height, in pixels</summary>
        public uint Height;

        /// <summary>Video mode depth, in bits per pixel</summary>
        public uint BitsPerPixel;

        #region Imports
        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern VideoMode sfVideoMode_getDesktopMode();

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe VideoMode* sfVideoMode_getFullscreenModes(out UIntPtr count);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfVideoMode_isValid(VideoMode mode);
        #endregion
    }
}
