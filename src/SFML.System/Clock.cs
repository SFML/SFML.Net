using System;
using System.Runtime.InteropServices;
using System.Security;

namespace SFML.System
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Utility class that measures the elapsed time
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class Clock : ObjectBase
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Default Constructor
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Clock() : base(sfClock_create()) { }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
        ////////////////////////////////////////////////////////////
        protected override void Destroy(bool disposing)
        {
            sfClock_destroy(CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets the time elapsed since the last call to Restart
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Time ElapsedTime => sfClock_getElapsedTime(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// This function puts the time counter back to zero.
        /// </summary>
        /// <returns>Time elapsed since the clock was started.</returns>
        ////////////////////////////////////////////////////////////
        public Time Restart() => sfClock_restart(CPointer);

        #region Imports
        [DllImport(CSFML.system, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfClock_create();

        [DllImport(CSFML.system, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfClock_destroy(IntPtr CPointer);

        [DllImport(CSFML.system, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Time sfClock_getElapsedTime(IntPtr Clock);

        [DllImport(CSFML.system, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Time sfClock_restart(IntPtr Clock);
        #endregion
    }
}
