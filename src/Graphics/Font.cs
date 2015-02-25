using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.IO;
using SFML.Window;

namespace SFML
{
    namespace Graphics
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Font is the low-level class for loading and
        /// manipulating character fonts. This class is meant to
        /// be used by String2D
        /// </summary>
        ////////////////////////////////////////////////////////////
        public class Font : ObjectBase
        {
            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the font from a file
            /// </summary>
            /// <param name="filename">Font file to load</param>
            /// <exception cref="LoadingFailedException" />
            ////////////////////////////////////////////////////////////
            public Font(string filename) :
                base(sfFont_createFromFile(filename))
            {
                if (CPointer == IntPtr.Zero)
                    throw new LoadingFailedException("font", filename);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the font from a custom stream
            /// </summary>
            /// <param name="stream">Source stream to read from</param>
            /// <exception cref="LoadingFailedException" />
            ////////////////////////////////////////////////////////////
            public Font(Stream stream) :
                base(IntPtr.Zero)
            {
                myStream = new StreamAdaptor(stream);
                CPointer = sfFont_createFromStream(myStream.InputStreamPtr);

                if (CPointer == IntPtr.Zero)
                    throw new LoadingFailedException("font");
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the font from a file in memory
            /// </summary>
            /// <param name="bytes">Byte array containing the file contents</param>
            /// <exception cref="LoadingFailedException" />
            ////////////////////////////////////////////////////////////
            public Font(byte[] bytes) : 
                base(IntPtr.Zero)
            {
                GCHandle pin = GCHandle.Alloc(bytes, GCHandleType.Pinned);
                try 
                {
                    CPointer = sfFont_createFromMemory(pin.AddrOfPinnedObject(), Convert.ToUInt64(bytes.Length));
                } 
                finally 
                {
                    pin.Free();
                }
                if (CPointer == IntPtr.Zero)
                    throw new LoadingFailedException("font");
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the font from another font
            /// </summary>
            /// <param name="copy">Font to copy</param>
            ////////////////////////////////////////////////////////////
            public Font(Font copy) :
                base(sfFont_copy(copy.CPointer))
            {
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Get a glyph in the font
            /// </summary>
            /// <param name="codePoint">Unicode code point of the character to get</param>
            /// <param name="characterSize">Character size</param>
            /// <param name="bold">Retrieve the bold version or the regular one?</param>
            /// <returns>The glyph corresponding to the character</returns>
            ////////////////////////////////////////////////////////////
            public Glyph GetGlyph(uint codePoint, uint characterSize, bool bold)
            {
                return sfFont_getGlyph(CPointer, codePoint, characterSize, bold);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Get the kerning offset between two glyphs
            /// </summary>
            /// <param name="first">Unicode code point of the first character</param>
            /// <param name="second">Unicode code point of the second character</param>
            /// <param name="characterSize">Character size</param>
            /// <returns>Kerning offset, in pixels</returns>
            ////////////////////////////////////////////////////////////
            public float GetKerning(uint first, uint second, uint characterSize)
            {
                return sfFont_getKerning(CPointer, first, second, characterSize);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Get spacing between two consecutive lines
            /// </summary>
            /// <param name="characterSize">Character size</param>
            /// <returns>Line spacing, in pixels</returns>
            ////////////////////////////////////////////////////////////
            public float GetLineSpacing(uint characterSize)
            {
                return sfFont_getLineSpacing(CPointer, characterSize);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Get the position of the underline
            /// </summary>
            /// <param name="characterSize">Character size</param>
            /// <returns>Underline position, in pixels</returns>
            ////////////////////////////////////////////////////////////
            public float GetUnderlinePosition(uint characterSize)
            {
                return sfFont_getUnderlinePosition(CPointer, characterSize);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Get the thickness of the underline
            /// </summary>
            /// <param name="characterSize">Character size</param>
            /// <returns>Underline thickness, in pixels</returns>
            ////////////////////////////////////////////////////////////
            public float GetUnderlineThickness(uint characterSize)
            {
                return sfFont_getUnderlineThickness(CPointer, characterSize);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Get the texture containing the glyphs of a given size
            /// </summary>
            /// <param name="characterSize">Character size</param>
            /// <returns>Texture storing the glyphs for the given size</returns>
            ////////////////////////////////////////////////////////////
            public Texture GetTexture(uint characterSize)
            {
                myTextures[characterSize] = new Texture(sfFont_getTexture(CPointer, characterSize));
                return myTextures[characterSize];
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Get the font information
            /// </summary>
            /// <returns>A structure that holds the font information</returns>
            ////////////////////////////////////////////////////////////
            public Info GetInfo()
            {
                InfoMarshalData data = sfFont_getInfo(CPointer);
                Info info = new Info();

                info.Family = Marshal.PtrToStringAnsi(data.Family);

                return info;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Provide a string describing the object
            /// </summary>
            /// <returns>String description of the object</returns>
            ////////////////////////////////////////////////////////////
            public override string ToString()
            {
                return "[Font]";
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Handle the destruction of the object
            /// </summary>
            /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
            ////////////////////////////////////////////////////////////
            protected override void Destroy(bool disposing)
            {
                if (!disposing)
                    Context.Global.SetActive(true);

                sfFont_destroy(CPointer);

                if (disposing)
                {
                    foreach (Texture texture in myTextures.Values)
                        texture.Dispose();

                    if (myStream != null)
                        myStream.Dispose();
                }

                if (!disposing)
                    Context.Global.SetActive(false);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Internal constructor
            /// </summary>
            /// <param name="cPointer">Pointer to the object in C library</param>
            ////////////////////////////////////////////////////////////
            private Font(IntPtr cPointer) :
                base(cPointer)
            {
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Info holds various information about a font
            /// </summary>
            ////////////////////////////////////////////////////////////
            public struct Info
            {
                /// <summary>The font family</summary>
                public string Family;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Internal struct used for marshaling the font info
            /// struct from unmanaged code.
            /// </summary>
            ////////////////////////////////////////////////////////////
            [StructLayout(LayoutKind.Sequential)]
            internal struct InfoMarshalData
            {
                public IntPtr Family;
            }

            private Dictionary<uint, Texture> myTextures = new Dictionary<uint, Texture>();
            private StreamAdaptor myStream = null;

            #region Imports
            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfFont_createFromFile(string Filename);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfFont_createFromStream(IntPtr stream);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfFont_createFromMemory(IntPtr data, ulong size);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfFont_copy(IntPtr Font);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfFont_destroy(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Glyph sfFont_getGlyph(IntPtr CPointer, uint codePoint, uint characterSize, bool bold);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern float sfFont_getKerning(IntPtr CPointer, uint first, uint second, uint characterSize);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern float sfFont_getLineSpacing(IntPtr CPointer, uint characterSize);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern float sfFont_getUnderlinePosition(IntPtr CPointer, uint characterSize);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern float sfFont_getUnderlineThickness(IntPtr CPointer, uint characterSize);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfFont_getTexture(IntPtr CPointer, uint characterSize);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern InfoMarshalData sfFont_getInfo(IntPtr CPointer);
            #endregion
        }
    }
}
