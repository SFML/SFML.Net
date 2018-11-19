using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using SFML.System;
using SFML.Window;

namespace SFML.Graphics
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Image living on the graphics card that can be used for drawing
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class Texture : ObjectBase
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the texture
        /// </summary>
        /// <param name="width">Texture width</param>
        /// <param name="height">Texture height</param>
        /// <exception cref="LoadingFailedException" />
        ////////////////////////////////////////////////////////////
        public Texture(uint width, uint height) :
            base(sfTexture_create(width, height))
        {
            if (CPointer == IntPtr.Zero)
            {
                throw new LoadingFailedException("texture");
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the texture from a file
        /// </summary>
        /// <param name="filename">Path of the image file to load</param>
        /// <exception cref="LoadingFailedException" />
        ////////////////////////////////////////////////////////////
        public Texture(string filename) :
            this(filename, new IntRect(0, 0, 0, 0))
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the texture from a file
        /// </summary>
        /// <param name="filename">Path of the image file to load</param>
        /// <param name="area">Area of the image to load</param>
        /// <exception cref="LoadingFailedException" />
        ////////////////////////////////////////////////////////////
        public Texture(string filename, IntRect area) :
            base(sfTexture_createFromFile(filename, ref area))
        {
            if (CPointer == IntPtr.Zero)
            {
                throw new LoadingFailedException("texture", filename);
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the texture from a file in a stream
        /// </summary>
        /// <param name="stream">Stream containing the file contents</param>
        /// <exception cref="LoadingFailedException" />
        ////////////////////////////////////////////////////////////
        public Texture(Stream stream) :
            this(stream, new IntRect(0, 0, 0, 0))
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the texture from a file in a stream
        /// </summary>
        /// <param name="stream">Stream containing the file contents</param>
        /// <param name="area">Area of the image to load</param>
        /// <exception cref="LoadingFailedException" />
        ////////////////////////////////////////////////////////////
        public Texture(Stream stream, IntRect area) :
            base(IntPtr.Zero)
        {
            using (StreamAdaptor adaptor = new StreamAdaptor(stream))
            {
                CPointer = sfTexture_createFromStream(adaptor.InputStreamPtr, ref area);
            }

            if (CPointer == IntPtr.Zero)
            {
                throw new LoadingFailedException("texture");
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the texture from an image
        /// </summary>
        /// <param name="image">Image to load to the texture</param>
        /// <exception cref="LoadingFailedException" />
        ////////////////////////////////////////////////////////////
        public Texture(Image image) :
            this(image, new IntRect(0, 0, 0, 0))
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the texture from an image
        /// </summary>
        /// <param name="image">Image to load to the texture</param>
        /// <param name="area">Area of the image to load</param>
        /// <exception cref="LoadingFailedException" />
        ////////////////////////////////////////////////////////////
        public Texture(Image image, IntRect area) :
            base(sfTexture_createFromImage(image.CPointer, ref area))
        {
            if (CPointer == IntPtr.Zero)
            {
                throw new LoadingFailedException("texture");
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the texture from a file in memory
        /// </summary>
        /// <param name="bytes">Byte array containing the file contents</param>
        /// <exception cref="LoadingFailedException" />
        ////////////////////////////////////////////////////////////
        public Texture(byte[] bytes) :
            base(IntPtr.Zero)
        {
            GCHandle pin = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                IntRect rect = new IntRect(0, 0, 0, 0);
                CPointer = sfTexture_createFromMemory(pin.AddrOfPinnedObject(), Convert.ToUInt64(bytes.Length), ref rect);
            }
            finally
            {
                pin.Free();
            }
            if (CPointer == IntPtr.Zero)
            {
                throw new LoadingFailedException("texture");
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the texture from another texture
        /// </summary>
        /// <param name="copy">Texture to copy</param>
        ////////////////////////////////////////////////////////////
        public Texture(Texture copy) :
            base(sfTexture_copy(copy.CPointer))
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the underlying OpenGL handle of the texture.
        /// </summary>
        /// <remarks>
        /// You shouldn't need to use this handle, unless you have
        /// very specific stuff to implement that SFML doesn't support,
        /// or implement a temporary workaround until a bug is fixed.
        /// </remarks>
        ////////////////////////////////////////////////////////////
        public uint NativeHandle
        {
            get { return sfTexture_getNativeHandle(CPointer); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Copy a texture's pixels to an image
        /// </summary>
        /// <returns>Image containing the texture's pixels</returns>
        ////////////////////////////////////////////////////////////
        public Image CopyToImage()
        {
            return new Image(sfTexture_copyToImage(CPointer));
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Update a texture from an array of pixels
        /// </summary>
        /// <param name="pixels">Array of pixels to copy to the texture</param>
        ////////////////////////////////////////////////////////////
        public void Update(byte[] pixels)
        {
            Vector2u size = Size;
            Update(pixels, size.X, size.Y, 0, 0);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Update a texture from an array of pixels
        /// </summary>
        /// <param name="pixels">Array of pixels to copy to the texture</param>
        /// <param name="width">Width of the pixel region contained in pixels</param>
        /// <param name="height">Height of the pixel region contained in pixels</param>
        /// <param name="x">X offset in the texture where to copy the source pixels</param>
        /// <param name="y">Y offset in the texture where to copy the source pixels</param>
        ////////////////////////////////////////////////////////////
        public void Update(byte[] pixels, uint width, uint height, uint x, uint y)
        {
            unsafe
            {
                fixed (byte* ptr = pixels)
                {
                    sfTexture_updateFromPixels(CPointer, ptr, width, height, x, y);
                }
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Update a part of this texture from another texture
        /// </summary>
        /// <param name="texture">Source texture to copy to destination texture</param>
        /// <param name="x">X offset in this texture where to copy the source texture</param>
        /// <param name="y">Y offset in this texture where to copy the source texture</param>
        ////////////////////////////////////////////////////////////
        public void Update(Texture texture, uint x, uint y)
        {
            sfTexture_updateFromTexture(CPointer, texture.CPointer, x, y);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Update a texture from an image
        /// </summary>
        /// <param name="image">Image to copy to the texture</param>
        ////////////////////////////////////////////////////////////
        public void Update(Image image)
        {
            Update(image, 0, 0);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Update a texture from an image
        /// </summary>
        /// <param name="image">Image to copy to the texture</param>
        /// <param name="x">X offset in the texture where to copy the source pixels</param>
        /// <param name="y">Y offset in the texture where to copy the source pixels</param>
        ////////////////////////////////////////////////////////////
        public void Update(Image image, uint x, uint y)
        {
            sfTexture_updateFromImage(CPointer, image.CPointer, x, y);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Update a texture from the contents of a window
        /// </summary>
        /// <param name="window">Window to copy to the texture</param>
        ////////////////////////////////////////////////////////////
        public void Update(SFML.Window.Window window)
        {
            Update(window, 0, 0);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Update a texture from the contents of a window
        /// </summary>
        /// <param name="window">Window to copy to the texture</param>
        /// <param name="x">X offset in the texture where to copy the source pixels</param>
        /// <param name="y">Y offset in the texture where to copy the source pixels</param>
        ////////////////////////////////////////////////////////////
        public void Update(SFML.Window.Window window, uint x, uint y)
        {
            sfTexture_updateFromWindow(CPointer, window.CPointer, x, y);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Update a texture from the contents of a render-window
        /// </summary>
        /// <param name="window">Render-window to copy to the texture</param>
        ////////////////////////////////////////////////////////////
        public void Update(RenderWindow window)
        {
            Update(window, 0, 0);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Update a texture from the contents of a render-window
        /// </summary>
        /// <param name="window">Render-window to copy to the texture</param>
        /// <param name="x">X offset in the texture where to copy the source pixels</param>
        /// <param name="y">Y offset in the texture where to copy the source pixels</param>
        ////////////////////////////////////////////////////////////
        public void Update(RenderWindow window, uint x, uint y)
        {
            sfTexture_updateFromRenderWindow(CPointer, window.CPointer, x, y);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Generate a mipmap using the current texture data
        /// </summary>
        /// 
        /// <remarks>
        /// <para>Mipmaps are pre-computed chains of optimized textures. Each
        /// level of texture in a mipmap is generated by halving each of
        /// the previous level's dimensions. This is done until the final
        /// level has the size of 1x1. The textures generated in this process may
        /// make use of more advanced filters which might improve the visual quality
        /// of textures when they are applied to objects much smaller than they are.
        /// This is known as minification. Because fewer texels (texture elements)
        /// have to be sampled from when heavily minified, usage of mipmaps
        /// can also improve rendering performance in certain scenarios.</para>
        ///
        /// <para>Mipmap generation relies on the necessary OpenGL extension being
        /// available. If it is unavailable or generation fails due to another
        /// reason, this function will return false. Mipmap data is only valid from
        /// the time it is generated until the next time the base level image is
        /// modified, at which point this function will have to be called again to
        /// regenerate it.</para>
        /// </remarks>
        /// 
        /// <returns>True if mipmap generation was successful, false if unsuccessful</returns>
        ////////////////////////////////////////////////////////////
        public bool GenerateMipmap()
        {
            return sfTexture_generateMipmap(CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Swap the contents of this texture with those of another
        /// </summary>
        /// <param name="right">Instance to swap with</param>
        ////////////////////////////////////////////////////////////
        public void Swap(Texture right)
        {
            sfTexture_swap(CPointer, right.CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Control the smooth filter
        /// </summary>
        ////////////////////////////////////////////////////////////
        public bool Smooth
        {
            get { return sfTexture_isSmooth(CPointer); }
            set { sfTexture_setSmooth(CPointer, value); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Enable or disable conversion from sRGB
        /// </summary>
        /// 
        /// <remarks>
        /// <para>When providing texture data from an image file or memory, it can
        /// either be stored in a linear color space or an sRGB color space.
        /// Most digital images account for gamma correction already, so they
        /// would need to be "uncorrected" back to linear color space before
        /// being processed by the hardware. The hardware can automatically
        /// convert it from the sRGB color space to a linear color space when
        /// it gets sampled. When the rendered image gets output to the final
        /// framebuffer, it gets converted back to sRGB.</para>
        ///
        /// <para>After enabling or disabling sRGB conversion, make sure to reload
        /// the texture data in order for the setting to take effect.</para>
        ///
        /// <para>This option is only useful in conjunction with an sRGB capable
        /// framebuffer. This can be requested during window creation.</para>
        /// </remarks>
        ////////////////////////////////////////////////////////////
        public bool Srgb
        {
            get { return sfTexture_isSrgb(CPointer); }
            set { sfTexture_setSrgb(CPointer, value); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Control the repeat mode
        /// </summary>
        ////////////////////////////////////////////////////////////
        public bool Repeated
        {
            get { return sfTexture_isRepeated(CPointer); }
            set { sfTexture_setRepeated(CPointer, value); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Size of the texture, in pixels
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Vector2u Size
        {
            get { return sfTexture_getSize(CPointer); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Bind a texture for rendering
        /// </summary>
        /// <param name="texture">Shader to bind (can be null to use no texture)</param>
        ////////////////////////////////////////////////////////////
        public static void Bind(Texture texture)
        {
            sfTexture_bind(texture != null ? texture.CPointer : IntPtr.Zero);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Maximum texture size allowed
        /// </summary>
        ////////////////////////////////////////////////////////////
        public static uint MaximumSize
        {
            get { return sfTexture_getMaximumSize(); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString()
        {
            return "[Texture]" +
                   " Size(" + Size + ")" +
                   " Smooth(" + Smooth + ")" +
                   " Repeated(" + Repeated + ")";
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Internal constructor
        /// </summary>
        /// <param name="cPointer">Pointer to the object in C library</param>
        ////////////////////////////////////////////////////////////
        internal Texture(IntPtr cPointer) :
            base(cPointer)
        {
            myExternal = true;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
        ////////////////////////////////////////////////////////////
        protected override void Destroy(bool disposing)
        {
            if (!myExternal)
            {
                if (!disposing)
                {
                    Context.Global.SetActive(true);
                }

                sfTexture_destroy(CPointer);

                if (!disposing)
                {
                    Context.Global.SetActive(false);
                }
            }
        }

        private readonly bool myExternal = false;

        #region Imports
        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfTexture_create(uint width, uint height);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfTexture_createFromFile(string filename, ref IntRect area);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfTexture_createFromStream(IntPtr stream, ref IntRect area);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfTexture_createFromImage(IntPtr image, ref IntRect area);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfTexture_createFromMemory(IntPtr data, ulong size, ref IntRect area);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfTexture_copy(IntPtr texture);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfTexture_destroy(IntPtr texture);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Vector2u sfTexture_getSize(IntPtr texture);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfTexture_copyToImage(IntPtr texture);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        unsafe static extern void sfTexture_updateFromPixels(IntPtr texture, byte* pixels, uint width, uint height, uint x, uint y);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfTexture_updateFromTexture(IntPtr CPointer, IntPtr texture, uint x, uint y);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfTexture_updateFromImage(IntPtr texture, IntPtr image, uint x, uint y);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfTexture_updateFromWindow(IntPtr texture, IntPtr window, uint x, uint y);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfTexture_updateFromRenderWindow(IntPtr texture, IntPtr renderWindow, uint x, uint y);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfTexture_bind(IntPtr texture);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfTexture_setSmooth(IntPtr texture, bool smooth);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfTexture_isSmooth(IntPtr texture);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfTexture_setSrgb(IntPtr texture, bool sRgb);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfTexture_isSrgb(IntPtr texture);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfTexture_setRepeated(IntPtr texture, bool repeated);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfTexture_isRepeated(IntPtr texture);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfTexture_generateMipmap(IntPtr texture);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfTexture_swap(IntPtr CPointer, IntPtr right);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern uint sfTexture_getNativeHandle(IntPtr shader);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern FloatRect sfTexture_getTexCoords(IntPtr texture, IntRect rectangle);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern uint sfTexture_getMaximumSize();
        #endregion
    }
}
