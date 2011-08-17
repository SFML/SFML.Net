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
        /// Structure describing a glyph (a visual character)
        /// </summary>
        ////////////////////////////////////////////////////////////
        [StructLayout(LayoutKind.Sequential)]
        public struct Glyph
        {
            /// <summary>Offset to move horizontically to the next character</summary>
            public int Advance;

            /// <summary>Bounding rectangle of the glyph, in coordinates relative to the baseline</summary>
            public IntRect Rectangle;

            /// <summary>Texture coordinates of the glyph inside the font's image</summary>
            public FloatRect TexCoords;
        }

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
                base(sfFont_CreateFromFile(filename))
            {
                if (This == IntPtr.Zero)
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
                SetThis(sfFont_CreateFromStream(myStream.InputStreamPtr));

                if (This == IntPtr.Zero)
                    throw new LoadingFailedException("font");
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the font from another font
            /// </summary>
            /// <param name="copy">Font to copy</param>
            ////////////////////////////////////////////////////////////
            public Font(Font copy) :
                base(sfFont_Copy(copy.This))
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
                return sfFont_GetGlyph(This, codePoint, characterSize, bold);
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
            public int GetKerning(uint first, uint second, uint characterSize)
            {
                return sfFont_GetKerning(This, first, second, characterSize);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Get spacing between two consecutive lines
            /// </summary>
            /// <param name="characterSize">Character size</param>
            /// <returns>Line spacing, in pixels</returns>
            ////////////////////////////////////////////////////////////
            public int GetLineSpacing(uint characterSize)
            {
                return sfFont_GetLineSpacing(This, characterSize);
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
                myTextures[characterSize] = new Texture(sfFont_GetTexture(This, characterSize));
                return myTextures[characterSize];
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Default built-in font
            /// </summary>
            ////////////////////////////////////////////////////////////
            public static Font DefaultFont
            {
                get
                {
                    if (ourDefaultFont == null)
                        ourDefaultFont = new Font(sfFont_GetDefaultFont());

                    return ourDefaultFont;
                }
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
                if (this != ourDefaultFont)
                {
                    if (!disposing)
                        Context.Global.SetActive(true);

                    sfFont_Destroy(This);

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
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Internal constructor
            /// </summary>
            /// <param name="thisPtr">Pointer to the object in C library</param>
            ////////////////////////////////////////////////////////////
            private Font(IntPtr thisPtr) :
                base(thisPtr)
            {
            }

            private Dictionary<uint, Texture> myTextures = new Dictionary<uint, Texture>();
            private StreamAdaptor myStream = null;
            private static Font ourDefaultFont = null;

            #region Imports
            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfFont_CreateFromFile(string Filename);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfFont_CreateFromStream(IntPtr stream);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfFont_Copy(IntPtr Font);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfFont_Destroy(IntPtr This);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Glyph sfFont_GetGlyph(IntPtr This, uint codePoint, uint characterSize, bool bold);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern int sfFont_GetKerning(IntPtr This, uint first, uint second, uint characterSize);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern int sfFont_GetLineSpacing(IntPtr This, uint characterSize);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfFont_GetTexture(IntPtr This, uint characterSize);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfFont_GetDefaultFont();
            #endregion
        }
    }
}
