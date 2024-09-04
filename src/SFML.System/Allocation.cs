using System.Runtime.InteropServices;
using System.Security;
using System;

namespace SFML.System
{
    /// <summary>
    /// Contains functions related to memory allocation.
    /// For internal use only.
    /// </summary>
    public static class Allocation
    {
        /// <summary>
        /// This function deallocates the memory being pointed to
        /// using the free function from the C standard library.
        ///
        /// The memory must have been previously allocated using a call
        /// to malloc.
        /// </summary>
        /// <param name="ptr">Pointer to the memory to deallocate</param>
        public static void Free(IntPtr ptr) => sfFree(ptr);

        [DllImport(CSFML.System, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfFree(IntPtr ptr);
    }
}
