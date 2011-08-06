using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using SFML.Window;

namespace SFML
{
    namespace Graphics
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Target for off-screen 2D rendering into an texture
        /// </summary>
        ////////////////////////////////////////////////////////////
        public class RenderTexture : ObjectBase, RenderTarget
        {
            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Create the render-texture with the given dimensions
            /// </summary>
            /// <param name="width">Width of the render-texture</param>
            /// <param name="height">Height of the render-texture</param>
            ////////////////////////////////////////////////////////////
            public RenderTexture(uint width, uint height) :
                this(width, height, false)
            {
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Create the render-texture with the given dimensions and
            /// an optional depth-buffer attached
            /// </summary>
            /// <param name="width">Width of the render-texture</param>
            /// <param name="height">Height of the render-texture</param>
            /// <param name="depthBuffer">Do you want a depth-buffer attached?</param>
            ////////////////////////////////////////////////////////////
            public RenderTexture(uint width, uint height, bool depthBuffer) :
                base(sfRenderTexture_Create(width, height, depthBuffer))
            {
                myDefaultView = new View(sfRenderTexture_GetDefaultView(This));
                myTexture = new Texture(sfRenderTexture_GetTexture(This));
                GC.SuppressFinalize(myDefaultView);
                GC.SuppressFinalize(myTexture);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Width of the rendering region of the texture
            /// </summary>
            ////////////////////////////////////////////////////////////
            public uint Width
            {
                get {return sfRenderTexture_GetWidth(This);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Height of the rendering region of the texture
            /// </summary>
            ////////////////////////////////////////////////////////////
            public uint Height
            {
                get {return sfRenderTexture_GetHeight(This);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Activate of deactivate the render texture as the current target
            /// for rendering
            /// </summary>
            /// <param name="active">True to activate, false to deactivate (true by default)</param>
            /// <returns>True if operation was successful, false otherwise</returns>
            ////////////////////////////////////////////////////////////
            public bool SetActive(bool active)
            {
                return sfRenderTexture_SetActive(This, active);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Default view of the render texture
            /// </summary>
            ////////////////////////////////////////////////////////////
            public View DefaultView
            {
                get {return myDefaultView;}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Return the current active view
            /// </summary>
            /// <returns>The current view</returns>
            ////////////////////////////////////////////////////////////
            public View GetView()
            {
                return new View(sfRenderTexture_GetView(This));
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Change the current active view
            /// </summary>
            /// <param name="view">New view</param>
            ////////////////////////////////////////////////////////////
            public void SetView(View view)
            {
                sfRenderTexture_SetView(This, view.This);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Get the viewport of a view applied to this target
            /// </summary>
            /// <param name="view">Target view</param>
            /// <returns>Viewport rectangle, expressed in pixels in the current target</returns>
            ////////////////////////////////////////////////////////////
            public IntRect GetViewport(View view)
            {
                return sfRenderTexture_GetViewport(This, view.This);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Convert a point in target coordinates into view coordinates
            /// This version uses the current view of the window
            /// </summary>
            /// <param name="x">X coordinate of the point to convert, relative to the target</param>
            /// <param name="y">Y coordinate of the point to convert, relative to the target</param>
            /// <returns>Converted point</returns>
            ///
            ////////////////////////////////////////////////////////////
            public Vector2f ConvertCoords(uint x, uint y)
            {
                return ConvertCoords(x, y, GetView());
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Convert a point in target coordinates into view coordinates
            /// This version uses the given view
            /// </summary>
            /// <param name="x">X coordinate of the point to convert, relative to the target</param>
            /// <param name="y">Y coordinate of the point to convert, relative to the target</param>
            /// <param name="view">Target view to convert the point to</param>
            /// <returns>Converted point</returns>
            ///
            ////////////////////////////////////////////////////////////
            public Vector2f ConvertCoords(uint x, uint y, View view)
            {
                Vector2f point;
                sfRenderTexture_ConvertCoords(This, x, y, out point.X, out point.Y, view.This);

                return point;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Clear the entire render texture with black color
            /// </summary>
            ////////////////////////////////////////////////////////////
            public void Clear()
            {
                sfRenderTexture_Clear(This, Color.Black);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Clear the entire render texture with a single color
            /// </summary>
            /// <param name="color">Color to use to clear the texture</param>
            ////////////////////////////////////////////////////////////
            public void Clear(Color color)
            {
                sfRenderTexture_Clear(This, color);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Draw something into the render texture
            /// </summary>
            /// <param name="objectToDraw">Object to draw</param>
            ////////////////////////////////////////////////////////////
            public void Draw(Drawable objectToDraw)
            {
                objectToDraw.Render(this, null);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Draw something into the render texture with a shader
            /// </summary>
            /// <param name="objectToDraw">Object to draw</param>
            /// <param name="shader">Shader to apply</param>
            ////////////////////////////////////////////////////////////
            public void Draw(Drawable objectToDraw, Shader shader)
            {
                objectToDraw.Render(this, shader);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Save the current OpenGL render states and matrices
            /// </summary>
            ////////////////////////////////////////////////////////////
            public void SaveGLStates()
            {
                sfRenderTexture_SaveGLStates(This);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Restore the previously saved OpenGL render states and matrices
            /// </summary>
            ////////////////////////////////////////////////////////////
            public void RestoreGLStates()
            {
                sfRenderTexture_RestoreGLStates(This);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Update the contents of the target texture
            /// </summary>
            ////////////////////////////////////////////////////////////
            public void Display()
            {
                sfRenderTexture_Display(This);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Target texture of the render texture
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Texture Texture
            {
                get { return myTexture; }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Provide a string describing the object
            /// </summary>
            /// <returns>String description of the object</returns>
            ////////////////////////////////////////////////////////////
            public override string ToString()
            {
                return "[RenderTexture]" +
                       " Width(" + Width + ")" +
                       " Height(" + Height + ")" +
                       " Texture(" + Texture + ")" +
                       " DefaultView(" + DefaultView + ")" +
                       " View(" + GetView() + ")";
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

                sfRenderTexture_Destroy(This);

                if (disposing)
                {
                    myDefaultView.Dispose();
                    myTexture.Dispose();
                }

                if (!disposing)
                    Context.Global.SetActive(false);
            }

            private View myDefaultView = null;
            private Texture myTexture = null;

            #region Imports
            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfRenderTexture_Create(uint Width, uint Height, bool DepthBuffer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderTexture_Destroy(IntPtr This);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderTexture_Clear(IntPtr This, Color ClearColor);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern uint sfRenderTexture_GetWidth(IntPtr This);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern uint sfRenderTexture_GetHeight(IntPtr This);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfRenderTexture_SetActive(IntPtr This, bool Active);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfRenderTexture_SaveGLStates(IntPtr This);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfRenderTexture_RestoreGLStates(IntPtr This);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfRenderTexture_Display(IntPtr This);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderTexture_SetView(IntPtr This, IntPtr View);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfRenderTexture_GetView(IntPtr This);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfRenderTexture_GetDefaultView(IntPtr This);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntRect sfRenderTexture_GetViewport(IntPtr This, IntPtr TargetView);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderTexture_ConvertCoords(IntPtr This, uint WindowX, uint WindowY, out float ViewX, out float ViewY, IntPtr TargetView);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfRenderTexture_GetTexture(IntPtr This);

            #endregion
        }
    }
}
