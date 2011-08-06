using System;
using System.Runtime.InteropServices;
using System.Security;
using System.IO;
using System.Runtime.ConstrainedExecution;
using SFML.Window;

namespace SFML
{
    namespace Graphics
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
                base(sfTexture_Create(width, height))
            {
                if (This == IntPtr.Zero)
                    throw new LoadingFailedException("texture");
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
                base(sfTexture_CreateFromFile(filename, ref area))
            {
                if (This == IntPtr.Zero)
                    throw new LoadingFailedException("texture", filename);
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
                    SetThis(sfTexture_CreateFromStream(adaptor.InputStreamPtr, ref area));
                }

                if (This == IntPtr.Zero)
                    throw new LoadingFailedException("texture");
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
                base(sfTexture_CreateFromImage(image.This, ref area))
            {
                if (This == IntPtr.Zero)
                    throw new LoadingFailedException("texture");
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the texture from another texture
            /// </summary>
            /// <param name="copy">Texture to copy</param>
            ////////////////////////////////////////////////////////////
            public Texture(Texture copy) :
                base(sfTexture_Copy(copy.This))
            {
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Copy a texture's pixels to an image
            /// </summary>
            /// <returns>Image containing the texture's pixels</returns>
            ////////////////////////////////////////////////////////////
            public Image CopyToImage()
            {
                return new Image(sfTexture_CopyToImage(This));
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Update a texture from an array of pixels
            /// </summary>
            /// <param name="pixels">Array of pixels to copy to the texture</param>
            ////////////////////////////////////////////////////////////
            public void Update(byte[] pixels)
            {
                Update(pixels, Width, Height, 0, 0);
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
                        sfTexture_UpdateFromPixels(This, ptr, width, height, x, y);
                    }
                }
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
                sfTexture_UpdateFromImage(This, image.This, x, y);
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
                sfTexture_UpdateFromWindow(This, window.This, x, y);
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
                sfTexture_UpdateFromRenderWindow(This, window.This, x, y);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Bind the texture for rendering
            /// </summary>
            ////////////////////////////////////////////////////////////
            public void Bind()
            {
                sfTexture_Bind(This);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Control the smooth filter
            /// </summary>
            ////////////////////////////////////////////////////////////
            public bool Smooth
            {
                get {return sfTexture_IsSmooth(This);}
                set {sfTexture_SetSmooth(This, value);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Width of the texture, in pixels
            /// </summary>
            ////////////////////////////////////////////////////////////
            public uint Width
            {
                get {return sfTexture_GetWidth(This);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Height of the texture, in pixels
            /// </summary>
            ////////////////////////////////////////////////////////////
            public uint Height
            {
                get {return sfTexture_GetHeight(This);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Convert a rectangle of pixels into texture coordinates
            /// </summary>
            /// <param name="rectangle">Rectangle to convert</param>
            /// <returns>Texture coordinates corresponding to the rectangle</returns>
            ////////////////////////////////////////////////////////////
            public FloatRect GetTexCoords(IntRect rectangle)
            {
                return sfTexture_GetTexCoords(This, rectangle);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Maximum texture size allowed
            /// </summary>
            ////////////////////////////////////////////////////////////
            public static uint MaximumSize
            {
                get {return sfTexture_GetMaximumSize();}
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
                       " Width(" + Width + ")" +
                       " Height(" + Height + ")" +
                       " Smooth(" + Smooth + ")";
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Internal constructor
            /// </summary>
            /// <param name="thisPtr">Pointer to the object in C library</param>
            ////////////////////////////////////////////////////////////
            internal Texture(IntPtr thisPtr) :
                base(thisPtr)
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
                        Context.Global.SetActive(true);

                    sfTexture_Destroy(This);

                    if (!disposing)
                        Context.Global.SetActive(false);
                }
            }

            bool myExternal = false;

            #region Imports
            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfTexture_Create(uint width, uint height);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfTexture_CreateFromFile(string filename, ref IntRect area);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfTexture_CreateFromStream(IntPtr stream, ref IntRect area);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfTexture_CreateFromImage(IntPtr image, ref IntRect area);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfTexture_Copy(IntPtr texture);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfTexture_Destroy(IntPtr texture);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern uint sfTexture_GetWidth(IntPtr texture);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern uint sfTexture_GetHeight(IntPtr texture);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfTexture_CopyToImage(IntPtr texture);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            unsafe static extern void sfTexture_UpdateFromPixels(IntPtr texture, byte* pixels, uint width, uint height, uint x, uint y);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfTexture_UpdateFromImage(IntPtr texture, IntPtr image, uint x, uint y);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfTexture_UpdateFromWindow(IntPtr texture, IntPtr window, uint x, uint y);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfTexture_UpdateFromRenderWindow(IntPtr texture, IntPtr renderWindow, uint x, uint y);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfTexture_Bind(IntPtr texture);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfTexture_SetSmooth(IntPtr texture, bool smooth);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfTexture_IsSmooth(IntPtr texture);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern FloatRect sfTexture_GetTexCoords(IntPtr texture, IntRect rectangle);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern uint sfTexture_GetMaximumSize();

            #endregion
        }
    }
}
