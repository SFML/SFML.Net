using SFML;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SFML.Window
{
    /// <summary>
    /// Cursor defines the appearance of a system cursor. 
    /// </summary>
    public class Cursor : ObjectBase
    {
        /// <summary>
        /// Enumeration of possibly available native system cursor types
        /// </summary>
        public enum CursorType
        {
            /// <summary>
            /// Arrow cursor (default)
            /// Windows: Yes
            /// Mac OS:  Yes
            /// Linux:   Yes
            /// </summary>
            Arrow,
            /// <summary>
            /// Busy arrow cursor
            /// Windows: Yes
            /// Mac OS:  No
            /// Linux:   No
            /// </summary>
            ArrowWait,
            /// <summary>
            /// Busy cursor
            /// Windows: Yes
            /// Mac OS:  No
            /// Linux:   Yes
            /// </summary>
            Wait,
            /// <summary>
            /// I-beam, cursor when hovering over a field allowing text entry
            /// Windows: Yes
            /// Mac OS:  Yes
            /// Linux:   Yes
            /// </summary>
            Text,
            /// <summary>
            /// Pointing hand cursor
            /// Windows: Yes
            /// Mac OS:  Yes
            /// Linux:   Yes
            /// </summary>
            Hand,
            /// <summary>
            /// Horizontal double arrow cursor
            /// Windows: Yes
            /// Mac OS:  Yes
            /// Linux:   Yes
            /// </summary>
            SizeHorizontal,
            /// <summary>
            /// Vertical double arrow cursor
            /// Windows: Yes
            /// Mac OS:  Yes
            /// Linux:   Yes
            /// </summary>
            SizeVertical,
            /// <summary>
            /// Double arrow cursor going from top-left to bottom-right
            /// Windows: Yes
            /// Mac OS:  No
            /// Linux:   No
            /// </summary>
            SizeTopLeftBottomRight,
            /// <summary>
            /// Double arrow cursor going from bottom-left to top-right
            /// Windows: Yes
            /// Mac OS:  No
            /// Linux:   No
            /// </summary>
            SizeBottomLeftTopRight,
            /// <summary>
            /// Combination of SizeHorizontal and SizeVertical
            /// Windows: Yes
            /// Mac OS:  No
            /// Linux:   Yes
            /// </summary>
            SizeAll,
            /// <summary>
            /// Crosshair cursor
            /// Windows: Yes
            /// Mac OS:  Yes
            /// Linux:   Yes
            /// </summary>
            Cross,
            /// <summary>
            /// Help cursor
            /// Windows: Yes
            /// Mac OS:  No
            /// Linux:   Yes
            /// </summary>
            Help,
            /// <summary>
            /// Action not allowed cursor
            /// Windows: Yes
            /// Mac OS:  Yes
            /// Linux:   Yes
            /// </summary>
            NotAllowed
        }

        /// <summary>
        /// Create a native system cursor
        ///
        /// Refer to the list of cursor available on each system
        /// (see CursorType) to know whether a given cursor is
        /// expected to load successfully or is not supported by
        /// the operating system.
        /// </summary>
        /// <param name="type">System cursor type</param>
        public Cursor(CursorType type)
            : base(sfCursor_createFromSystem(type))
        {
        }

        /// <summary>
        /// Create a cursor with the provided image
        ///
        /// Pixels must be an array of width by height pixels
        /// in 32-bit RGBA format. If not, this will cause undefined behavior.
        ///
        /// If pixels is null or either width or height are 0,
        /// the current cursor is left unchanged and the function will
        /// return false.
        ///
        /// In addition to specifying the pixel data, you can also
        /// specify the location of the hotspot of the cursor. The
        /// hotspot is the pixel coordinate within the cursor image
        /// which will be located exactly where the mouse pointer
        /// position is. Any mouse actions that are performed will
        /// return the window/screen location of the hotspot.
        ///
        /// Warning: On Unix, the pixels are mapped into a monochrome
        ///          bitmap: pixels with an alpha channel to 0 are
        ///          transparent, black if the RGB channel are close
        ///          to zero, and white otherwise.
        /// </summary>
        /// <param name="pixels">Array of pixels of the image</param>
        /// <param name="size">Width and height of the image</param>
        /// <param name="hotspot">(x,y) location of the hotspot</param>
        public Cursor(byte[] pixels, SFML.System.Vector2u size, SFML.System.Vector2u hotspot)
            : base((IntPtr)0)
        {
            unsafe
            {
                fixed (byte* ptr = pixels)
                {
                    CPointer = sfCursor_createFromPixels((IntPtr)ptr, size, hotspot);
                }
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
        ////////////////////////////////////////////////////////////
        protected override void Destroy(bool disposing)
        {
            sfCursor_destroy(CPointer);
        }

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfCursor_createFromSystem(CursorType type);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfCursor_createFromPixels(IntPtr pixels, Vector2u size, Vector2u hotspot);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfCursor_destroy(IntPtr CPointer);
    }
}
