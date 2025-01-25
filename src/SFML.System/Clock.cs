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
        /// 
        /// The clock starts automatically after being constructed.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Clock() : base(sfClock_create()) { }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
        ////////////////////////////////////////////////////////////
        protected override void Destroy(bool disposing) => sfClock_destroy(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets the time elapsed since the last call to Restart
        /// (or the construction of the instance if Restart
        /// has not been called).
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Time ElapsedTime => sfClock_getElapsedTime(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Check whether the clock is running
        /// </summary>
        ////////////////////////////////////////////////////////////
        public bool IsRunning => sfClock_isRunning(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Start the clock
        /// </summary>
        ////////////////////////////////////////////////////////////
        public void Start() => sfClock_start(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Stop the clock
        /// </summary>
        ////////////////////////////////////////////////////////////
        public void Stop() => sfClock_stop(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// This function puts the time counter back to zero.
        /// </summary>
        /// <returns>Time elapsed since the clock was started.</returns>
        ////////////////////////////////////////////////////////////
        public Time Restart() => sfClock_restart(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Reset the clock
        /// 
        /// This function puts the time counter back to zero, returns
        /// the elapsed time, and leaves the clock in a paused state.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Time Reset() => sfClock_reset(CPointer);

        #region Imports
        [DllImport(CSFML.System, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfClock_create();

        [DllImport(CSFML.System, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfClock_destroy(IntPtr cPointer);

        [DllImport(CSFML.System, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Time sfClock_getElapsedTime(IntPtr clock);

        [DllImport(CSFML.System, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfClock_isRunning(IntPtr clock);

        [DllImport(CSFML.System, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfClock_start(IntPtr clock);

        [DllImport(CSFML.System, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfClock_stop(IntPtr clock);

        [DllImport(CSFML.System, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Time sfClock_restart(IntPtr clock);

        [DllImport(CSFML.System, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Time sfClock_reset(IntPtr clock);
        #endregion
    }
}
