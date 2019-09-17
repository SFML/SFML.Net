using SFML.System;
using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace SFML.Window
{
    /// <summary>
    /// Clipboard provides an interface for getting and setting the contents of the system clipboard.
    /// </summary>
    public class Clipboard
    {
        /// <summary>
        /// The contents of the Clipboard as a UTF-32 string
        /// </summary>
        public static string Contents
        {
            get
            {
                IntPtr source = sfClipboard_getUnicodeString();

                uint length = 0;
                unsafe
                {
                    for(uint* ptr = (uint*)source.ToPointer(); *ptr != 0; ++ptr)
                    {
                        length++;
                    }
                }
                
                byte[] sourceBytes = new byte[length * 4];
                Marshal.Copy(source, sourceBytes, 0, sourceBytes.Length);
                
                return Encoding.UTF32.GetString(sourceBytes);
            }
            set
            {
                byte[] utf32 = Encoding.UTF32.GetBytes(value + '\0');

                unsafe
                {
                    fixed (byte* ptr = utf32)
                    {
                        sfClipboard_setUnicodeString((IntPtr)ptr);
                    }
                }
            }
        }

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfClipboard_getUnicodeString();

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfClipboard_setUnicodeString(IntPtr ptr);
    }
}
