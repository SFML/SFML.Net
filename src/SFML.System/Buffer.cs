using System;
using System.Runtime.InteropServices;
using System.Security;

namespace SFML.System
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Internal helper class for CSFML's sfBuffer
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class Buffer : ObjectBase
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the buffer
        /// </summary>
        /// <exception cref="LoadingFailedException" />
        ////////////////////////////////////////////////////////////
        public Buffer() :
            base(sfBuffer_create())
        {
            if (IsInvalid)
            {
                throw new LoadingFailedException("buffer");
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get a copy of the buffer data
        /// </summary>
        /// <returns>A byte array containing the buffer data</returns>
        ////////////////////////////////////////////////////////////
        public byte[] GetData()
        {
            var size = sfBuffer_getSize(CPointer);
            var ptr = sfBuffer_getData(CPointer);

            if (ptr == IntPtr.Zero)
            {
                return Array.Empty<byte>();
            }

            var data = new byte[(int)size];
            Marshal.Copy(ptr, data, 0, (int)size);

            return data;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Internal constructor
        /// </summary>
        /// <param name="cPointer">Pointer to the object in C library</param>
        ////////////////////////////////////////////////////////////
        internal Buffer(IntPtr cPointer) :
            base(cPointer)
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
        ////////////////////////////////////////////////////////////
        protected override void Destroy(bool disposing) => sfBuffer_destroy(CPointer);

        #region Imports
        [DllImport(CSFML.System, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfBuffer_create();

        [DllImport(CSFML.System, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfBuffer_destroy(IntPtr buffer);

        [DllImport(CSFML.System, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UIntPtr sfBuffer_getSize(IntPtr buffer);

        [DllImport(CSFML.System, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfBuffer_getData(IntPtr buffer);
        #endregion
    }
}
