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
                var source = sfClipboard_getUnicodeString();

                uint length = 0;
                unsafe
                {
                    for (var ptr = (uint*)source.ToPointer(); *ptr != 0; ++ptr)
                    {
                        length++;
                    }
                }

                // Convert it to a C# string
                unsafe
                {
                    return Encoding.UTF32.GetString((byte*)source, (int)(length * 4));
                }
            }
            set
            {
                var utf32 = Encoding.UTF32.GetBytes(value + '\0');

                unsafe
                {
                    fixed (byte* ptr = utf32)
                    {
                        sfClipboard_setUnicodeString((IntPtr)ptr);
                    }
                }
            }
        }

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfClipboard_getUnicodeString();

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfClipboard_setUnicodeString(IntPtr ptr);
    }
}
