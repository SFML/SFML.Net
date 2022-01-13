using System;
using System.Runtime.InteropServices;
using System.Security;
using SFML.System;

namespace SFML.Window
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Give access to the real-time state of the touches
    /// </summary>
    ////////////////////////////////////////////////////////////
    public static class Touch
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Check if a touch event is currently down
        /// </summary>
        /// <param name="Finger">Finger index</param>
        /// <returns>True if the finger is currently touching the screen, false otherwise</returns>
        ////////////////////////////////////////////////////////////
        public static bool IsDown(uint Finger)
        {
            return sfTouch_isDown(Finger);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// This function returns the current touch position
        /// </summary>
        /// <param name="Finger">Finger index</param>
        /// <returns>Current position of the finger</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2i GetPosition(uint Finger)
        {
            return GetPosition(Finger, null);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// This function returns the current touch position
        /// relative to the given window
        /// </summary>
        /// <param name="Finger">Finger index</param>
        /// <param name="RelativeTo">Reference window</param>
        /// <returns>Current position of the finger</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2i GetPosition(uint Finger, Window RelativeTo)
        {
            if (RelativeTo != null)
            {
                return RelativeTo.InternalGetTouchPosition(Finger);
            }
            else
            {
                return sfTouch_getPosition(Finger, IntPtr.Zero);
            }
        }

        #region Imports
        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfTouch_isDown(uint Finger);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2i sfTouch_getPosition(uint Finger, IntPtr RelativeTo);
        #endregion
    }
}
