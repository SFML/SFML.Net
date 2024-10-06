using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using SFML.System;
using SFML.Window;

namespace SFML.Graphics
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
        public Font(string filename) : base(sfFont_createFromFile(filename))
        {
            if (IsInvalid)
            {
                throw new LoadingFailedException("font", filename);
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the font from a custom stream
        /// </summary>
        /// <param name="stream">Source stream to read from</param>
        /// <exception cref="LoadingFailedException" />
        ////////////////////////////////////////////////////////////
        public Font(Stream stream) : base(IntPtr.Zero)
        {
            using (var adaptor = new StreamAdaptor(stream))
            {
                CPointer = sfFont_createFromStream(adaptor.InputStreamPtr);
            }

            if (IsInvalid)
            {
                throw new LoadingFailedException("font");
            }
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
            unsafe
            {
                fixed (void* ptr = bytes)
                {
                    CPointer = sfFont_createFromMemory((IntPtr)ptr, (UIntPtr)bytes.Length);
                }
            }

            if (IsInvalid)
            {
                throw new LoadingFailedException("font");
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the font from another font
        /// </summary>
        /// <param name="copy">Font to copy</param>
        ////////////////////////////////////////////////////////////
        public Font(Font copy) : base(sfFont_copy(copy.CPointer)) { }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get a glyph in the font
        /// </summary>
        /// <param name="codePoint">Unicode code point of the character to get</param>
        /// <param name="characterSize">Character size</param>
        /// <param name="bold">Retrieve the bold version or the regular one?</param>
        /// <param name="outlineThickness">Thickness of outline (when != 0 the glyph will not be filled)</param>
        /// <returns>The glyph corresponding to the character</returns>
        ////////////////////////////////////////////////////////////
        public Glyph GetGlyph(uint codePoint, uint characterSize, bool bold, float outlineThickness) => sfFont_getGlyph(CPointer, codePoint, characterSize, bold, outlineThickness);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Determine if this font has a glyph representing the requested code point
        /// <para/>
        /// Most fonts only include a very limited selection of glyphs from
        /// specific Unicode subsets, like Latin, Cyrillic, or Asian characters.
        /// <para/>
        /// While code points without representation will return a font specific
        /// default character, it might be useful to verify whether specific
        /// code points are included to determine whether a font is suited
        /// to display text in a specific language.
        /// </summary>
        /// <param name="codePoint">Unicode code point to check</param>
        /// <returns>True if the codepoint has a glyph representation, false otherwise</returns>
        ////////////////////////////////////////////////////////////
        public bool HasGlyph(uint codePoint) => sfFont_hasGlyph(CPointer, codePoint);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the kerning value corresponding to a given pair of
        /// characters in a font
        /// </summary>
        /// <param name="first">Unicode code point of the first character</param>
        /// <param name="second">Unicode code point of the second character</param>
        /// <param name="characterSize">Character size, in pixels</param>
        /// <returns>Kerning offset, in pixels</returns>
        ////////////////////////////////////////////////////////////
        public float GetKerning(uint first, uint second, uint characterSize) => sfFont_getKerning(CPointer, first, second, characterSize);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the bold kerning value corresponding to a given pair
        /// of characters in a font
        /// </summary>
        /// <param name="first">Unicode code point of the first character</param>
        /// <param name="second">Unicode code point of the second character</param>
        /// <param name="characterSize">Character size, in pixels</param>
        /// <returns>Kerning offset, in pixels</returns>
        ////////////////////////////////////////////////////////////
        public float GetBoldKerning(uint first, uint second, uint characterSize) => sfFont_getBoldKerning(CPointer, first, second, characterSize);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get spacing between two consecutive lines
        /// </summary>
        /// <param name="characterSize">Character size</param>
        /// <returns>Line spacing, in pixels</returns>
        ////////////////////////////////////////////////////////////
        public float GetLineSpacing(uint characterSize) => sfFont_getLineSpacing(CPointer, characterSize);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the position of the underline
        /// </summary>
        /// <param name="characterSize">Character size</param>
        /// <returns>Underline position, in pixels</returns>
        ////////////////////////////////////////////////////////////
        public float GetUnderlinePosition(uint characterSize) => sfFont_getUnderlinePosition(CPointer, characterSize);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the thickness of the underline
        /// </summary>
        /// <param name="characterSize">Character size</param>
        /// <returns>Underline thickness, in pixels</returns>
        ////////////////////////////////////////////////////////////
        public float GetUnderlineThickness(uint characterSize) => sfFont_getUnderlineThickness(CPointer, characterSize);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the texture containing the glyphs of a given size
        /// </summary>
        /// <param name="characterSize">Character size</param>
        /// <returns>Texture storing the glyphs for the given size</returns>
        ////////////////////////////////////////////////////////////
        public Texture GetTexture(uint characterSize)
        {
            _textures[characterSize] = new Texture(sfFont_getTexture(CPointer, characterSize));
            return _textures[characterSize];
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Enable or disable the smooth filter
        ///
        /// When the filter is activated, the font appears smoother
        /// so that pixels are less noticeable. However if you want
        /// the font to look exactly the same as its source file,
        /// you should disable it.
        /// The smooth filter is enabled by default.
        /// </summary>
        /// <param name="smooth">True to enable smoothing, false to disable it</param>
        ////////////////////////////////////////////////////////////
        public void SetSmooth(bool smooth) => sfFont_setSmooth(CPointer, smooth);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Tell whether the smooth filter is enabled or disabled
        /// </summary>
        /// <returns>True if smoothing is enabled, false if it is disabled</returns>
        ////////////////////////////////////////////////////////////
        public bool IsSmooth() => sfFont_isSmooth(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the font information
        /// </summary>
        /// <returns>A structure that holds the font information</returns>
        ////////////////////////////////////////////////////////////
        public Info GetInfo()
        {
            var data = sfFont_getInfo(CPointer);
            var info = new Info
            {
                Family = Marshal.PtrToStringAnsi(data.Family)
            };

            return info;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => nameof(Font);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call?</param>
        ////////////////////////////////////////////////////////////
        protected override void Destroy(bool disposing)
        {
            if (!disposing)
            {
                _ = Context.Global.SetActive(true);
            }

            sfFont_destroy(CPointer);

            if (disposing)
            {
                foreach (var texture in _textures.Values)
                {
                    texture.Dispose();
                }
            }

            if (!disposing)
            {
                _ = Context.Global.SetActive(false);
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Internal constructor
        /// </summary>
        /// <param name="cPointer">Pointer to the object in C library</param>
        ////////////////////////////////////////////////////////////
        private Font(IntPtr cPointer) : base(cPointer) { }

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

        private readonly Dictionary<uint, Texture> _textures = new Dictionary<uint, Texture>();

        #region Imports
        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfFont_createFromFile(string filename);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfFont_createFromStream(IntPtr stream);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfFont_createFromMemory(IntPtr data, UIntPtr size);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfFont_copy(IntPtr font);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfFont_destroy(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Glyph sfFont_getGlyph(IntPtr cPointer, uint codePoint, uint characterSize, bool bold, float outlineThickness);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfFont_hasGlyph(IntPtr font, uint codePoint);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfFont_getKerning(IntPtr cPointer, uint first, uint second, uint characterSize);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfFont_getBoldKerning(IntPtr cPointer, uint first, uint second, uint characterSize);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfFont_getLineSpacing(IntPtr cPointer, uint characterSize);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfFont_getUnderlinePosition(IntPtr cPointer, uint characterSize);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfFont_getUnderlineThickness(IntPtr cPointer, uint characterSize);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfFont_getTexture(IntPtr cPointer, uint characterSize);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfFont_setSmooth(IntPtr cPointer, bool smooth);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfFont_isSmooth(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern InfoMarshalData sfFont_getInfo(IntPtr cPointer);
        #endregion
    }
}
