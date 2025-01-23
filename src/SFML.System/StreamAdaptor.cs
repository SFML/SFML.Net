using System;
using System.IO;
using System.Runtime.InteropServices;

namespace SFML.System
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Structure that contains InputStream callbacks
    /// (directly maps to a CSFML sfInputStream)
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct InputStream
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Type of callback to read data from the current stream
        /// </summary>
        ////////////////////////////////////////////////////////////
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long ReadCallbackType(IntPtr data, UIntPtr size, IntPtr userData);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Type of callback to seek the current stream's position
        /// </summary>
        ////////////////////////////////////////////////////////////
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long SeekCallbackType(UIntPtr position, IntPtr userData);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Type of callback to return the current stream's position
        /// </summary>
        ////////////////////////////////////////////////////////////
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long TellCallbackType(IntPtr userData);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Type of callback to return the current stream's size
        /// </summary>
        ////////////////////////////////////////////////////////////
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long GetSizeCallbackType(IntPtr userData);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Function that is called to read data from the stream
        /// </summary>
        ////////////////////////////////////////////////////////////
        public ReadCallbackType Read;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Function that is called to seek the stream
        /// </summary>
        ////////////////////////////////////////////////////////////
        public SeekCallbackType Seek;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Function that is called to return the position
        /// </summary>
        ////////////////////////////////////////////////////////////
        public TellCallbackType Tell;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Function that is called to return the size
        /// </summary>
        ////////////////////////////////////////////////////////////
        public GetSizeCallbackType GetSize;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Adapts a System.IO.Stream to be usable as a SFML InputStream
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class StreamAdaptor : IDisposable
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct from a System.IO.Stream
        /// </summary>
        /// <param name="stream">Stream to adapt</param>
        ////////////////////////////////////////////////////////////
        public StreamAdaptor(Stream stream)
        {
            _stream = stream;

            _inputStream = new InputStream
            {
                Read = new InputStream.ReadCallbackType(Read),
                Seek = new InputStream.SeekCallbackType(Seek),
                Tell = new InputStream.TellCallbackType(Tell),
                GetSize = new InputStream.GetSizeCallbackType(GetSize)
            };

            InputStreamPtr = Marshal.AllocHGlobal(Marshal.SizeOf(_inputStream));
            Marshal.StructureToPtr(_inputStream, InputStreamPtr, false);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Dispose the object
        /// </summary>
        ////////////////////////////////////////////////////////////
        ~StreamAdaptor()
        {
            Dispose(false);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// The pointer to the CSFML InputStream structure
        /// </summary>
        ////////////////////////////////////////////////////////////
        public IntPtr InputStreamPtr { get; }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Explicitly dispose the object
        /// </summary>
        ////////////////////////////////////////////////////////////
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Destroy the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
        ////////////////////////////////////////////////////////////
        private void Dispose(bool disposing) => Marshal.FreeHGlobal(InputStreamPtr);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Called to read from the stream
        /// </summary>
        /// <param name="data">Where to copy the read bytes</param>
        /// <param name="size">Size to read, in bytes</param>
        /// <param name="userData">User data -- unused</param>
        /// <returns>Number of bytes read</returns>
        ////////////////////////////////////////////////////////////
        private long Read(IntPtr data, UIntPtr size, IntPtr userData)
        {
            var buffer = new byte[(int)size];
            var count = _stream.Read(buffer, 0, (int)size);
            Marshal.Copy(buffer, 0, data, count);
            return count;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Called to set the read position in the stream
        /// </summary>
        /// <param name="position">New read position</param>
        /// <param name="userData">User data -- unused</param>
        /// <returns>Actual position</returns>
        ////////////////////////////////////////////////////////////
        private long Seek(UIntPtr position, IntPtr userData) => _stream.Seek((long)position, SeekOrigin.Begin);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the current read position in the stream
        /// </summary>
        /// <param name="userData">User data -- unused</param>
        /// <returns>Current position in the stream</returns>
        ////////////////////////////////////////////////////////////
        private long Tell(IntPtr userData) => _stream.Position;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Called to get the total size of the stream
        /// </summary>
        /// <param name="userData">User data -- unused</param>
        /// <returns>Number of bytes in the stream</returns>
        ////////////////////////////////////////////////////////////
        private long GetSize(IntPtr userData) => _stream.Length;

        private readonly Stream _stream;
        private InputStream _inputStream;
    }
}
