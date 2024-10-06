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
        /// <param name="finger">Finger index</param>
        /// <returns>True if the finger is currently touching the screen, false otherwise</returns>
        ////////////////////////////////////////////////////////////
        public static bool IsDown(uint finger) => sfTouch_isDown(finger);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// This function returns the current touch position
        /// </summary>
        /// <param name="finger">Finger index</param>
        /// <returns>Current position of the finger</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2i GetPosition(uint finger) => GetPosition(finger, null);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// This function returns the current touch position
        /// relative to the given window
        /// </summary>
        /// <param name="finger">Finger index</param>
        /// <param name="relativeTo">Reference window</param>
        /// <returns>Current position of the finger</returns>
        ////////////////////////////////////////////////////////////
        public static Vector2i GetPosition(uint finger, WindowBase relativeTo)
        {
            if (relativeTo != null)
            {
                return relativeTo.InternalGetTouchPosition(finger);
            }
            else
            {
                return sfTouch_getPosition(finger, IntPtr.Zero);
            }
        }

        #region Imports
        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfTouch_isDown(uint finger);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2i sfTouch_getPosition(uint finger, IntPtr relativeTo);
        #endregion
    }
}
