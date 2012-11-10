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
                base(sfRenderTexture_create(width, height, depthBuffer))
            {
                myDefaultView = new View(sfRenderTexture_getDefaultView(CPointer));
                myTexture = new Texture(sfRenderTexture_getTexture(CPointer));
                GC.SuppressFinalize(myDefaultView);
                GC.SuppressFinalize(myTexture);
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
                return sfRenderTexture_setActive(CPointer, active);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Size of the rendering region of the render texture
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Vector2u Size
            {
                get { return sfRenderTexture_getSize(CPointer); }
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
                return new View(sfRenderTexture_getView(CPointer));
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Change the current active view
            /// </summary>
            /// <param name="view">New view</param>
            ////////////////////////////////////////////////////////////
            public void SetView(View view)
            {
                sfRenderTexture_setView(CPointer, view.CPointer);
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
                return sfRenderTexture_getViewport(CPointer, view.CPointer);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Convert a point from target coordinates to world
            /// coordinates, using the current view
            ///
            /// This function is an overload of the MapPixelToCoords
            /// function that implicitely uses the current view.
            /// It is equivalent to:
            /// target.MapPixelToCoords(point, target.GetView());
            /// </summary>
            /// <param name="point">Pixel to convert</param>
            /// <returns>The converted point, in "world" coordinates</returns>
            ////////////////////////////////////////////////////////////
            public Vector2f MapPixelToCoords(Vector2i point)
            {
                return MapPixelToCoords(point, GetView());
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Convert a point from target coordinates to world coordinates
            ///
            /// This function finds the 2D position that matches the
            /// given pixel of the render-target. In other words, it does
            /// the inverse of what the graphics card does, to find the
            /// initial position of a rendered pixel.
            ///
            /// Initially, both coordinate systems (world units and target pixels)
            /// match perfectly. But if you define a custom view or resize your
            /// render-target, this assertion is not true anymore, ie. a point
            /// located at (10, 50) in your render-target may map to the point
            /// (150, 75) in your 2D world -- if the view is translated by (140, 25).
            ///
            /// For render-windows, this function is typically used to find
            /// which point (or object) is located below the mouse cursor.
            ///
            /// This version uses a custom view for calculations, see the other
            /// overload of the function if you want to use the current view of the
            /// render-target.
            /// </summary>
            /// <param name="point">Pixel to convert</param>
            /// <param name="view">The view to use for converting the point</param>
            /// <returns>The converted point, in "world" coordinates</returns>
            ////////////////////////////////////////////////////////////
            public Vector2f MapPixelToCoords(Vector2i point, View view)
            {
                return sfRenderTexture_mapPixelToCoords(CPointer, point, view != null ? view.CPointer : IntPtr.Zero);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Convert a point from world coordinates to target
            /// coordinates, using the current view
            ///
            /// This function is an overload of the mapCoordsToPixel
            /// function that implicitely uses the current view.
            /// It is equivalent to:
            /// target.MapCoordsToPixel(point, target.GetView());
            /// </summary>
            /// <param name="point">Point to convert</param>
            /// <returns>The converted point, in target coordinates (pixels)</returns>
            ////////////////////////////////////////////////////////////
            public Vector2i MapCoordsToPixel(Vector2f point)
            {
                return MapCoordsToPixel(point, GetView());
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Convert a point from world coordinates to target coordinates
            ///
            /// This function finds the pixel of the render-target that matches
            /// the given 2D point. In other words, it goes through the same process
            /// as the graphics card, to compute the final position of a rendered point.
            ///
            /// Initially, both coordinate systems (world units and target pixels)
            /// match perfectly. But if you define a custom view or resize your
            /// render-target, this assertion is not true anymore, ie. a point
            /// located at (150, 75) in your 2D world may map to the pixel
            /// (10, 50) of your render-target -- if the view is translated by (140, 25).
            ///
            /// This version uses a custom view for calculations, see the other
            /// overload of the function if you want to use the current view of the
            /// render-target.
            /// </summary>
            /// <param name="point">Point to convert</param>
            /// <param name="view">The view to use for converting the point</param>
            /// <returns>The converted point, in target coordinates (pixels)</returns>
            ////////////////////////////////////////////////////////////
            public Vector2i MapCoordsToPixel(Vector2f point, View view)
            {
                return sfRenderTexture_mapCoordsToPixel(CPointer, point, view != null ? view.CPointer : IntPtr.Zero);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Clear the entire render texture with black color
            /// </summary>
            ////////////////////////////////////////////////////////////
            public void Clear()
            {
                sfRenderTexture_clear(CPointer, Color.Black);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Clear the entire render texture with a single color
            /// </summary>
            /// <param name="color">Color to use to clear the texture</param>
            ////////////////////////////////////////////////////////////
            public void Clear(Color color)
            {
                sfRenderTexture_clear(CPointer, color);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Update the contents of the target texture
            /// </summary>
            ////////////////////////////////////////////////////////////
            public void Display()
            {
                sfRenderTexture_display(CPointer);
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
            /// Control the smooth filter
            /// </summary>
            ////////////////////////////////////////////////////////////
            public bool Smooth
            {
                get { return sfRenderTexture_isSmooth(CPointer); }
                set { sfRenderTexture_setSmooth(CPointer, value); }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Draw a drawable object to the render-target, with default render states
            /// </summary>
            /// <param name="drawable">Object to draw</param>
            ////////////////////////////////////////////////////////////
            public void Draw(Drawable drawable)
            {
                Draw(drawable, RenderStates.Default);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Draw a drawable object to the render-target
            /// </summary>
            /// <param name="drawable">Object to draw</param>
            /// <param name="states">Render states to use for drawing</param>
            ////////////////////////////////////////////////////////////
            public void Draw(Drawable drawable, RenderStates states)
            {
                drawable.Draw(this, states);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Draw primitives defined by an array of vertices, with default render states
            /// </summary>
            /// <param name="vertices">Pointer to the vertices</param>
            /// <param name="type">Type of primitives to draw</param>
            ////////////////////////////////////////////////////////////
            public void Draw(Vertex[] vertices, PrimitiveType type)
            {
                Draw(vertices, type, RenderStates.Default);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Draw primitives defined by an array of vertices
            /// </summary>
            /// <param name="vertices">Pointer to the vertices</param>
            /// <param name="type">Type of primitives to draw</param>
            /// <param name="states">Render states to use for drawing</param>
            ////////////////////////////////////////////////////////////
            public void Draw(Vertex[] vertices, PrimitiveType type, RenderStates states)
            {
                Draw(vertices, 0, (uint)vertices.Length, type, states);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Draw primitives defined by a sub-array of vertices, with default render states
            /// </summary>
            /// <param name="vertices">Array of vertices to draw</param>
            /// <param name="start">Index of the first vertex to draw in the array</param>
            /// <param name="count">Number of vertices to draw</param>
            /// <param name="type">Type of primitives to draw</param>
            ////////////////////////////////////////////////////////////
            public void Draw(Vertex[] vertices, uint start, uint count, PrimitiveType type)
            {
                Draw(vertices, start, count, type, RenderStates.Default);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Draw primitives defined by a sub-array of vertices
            /// </summary>
            /// <param name="vertices">Pointer to the vertices</param>
            /// <param name="start">Index of the first vertex to use in the array</param>
            /// <param name="count">Number of vertices to draw</param>
            /// <param name="type">Type of primitives to draw</param>
            /// <param name="states">Render states to use for drawing</param>
            ////////////////////////////////////////////////////////////
            public void Draw(Vertex[] vertices, uint start, uint count, PrimitiveType type, RenderStates states)
            {
                RenderStates.MarshalData marshaledStates = states.Marshal();

                unsafe
                {
                    fixed (Vertex* vertexPtr = vertices)
                    {
                        sfRenderTexture_drawPrimitives(CPointer, vertexPtr + start, count, type, ref marshaledStates);
                    }
                }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Save the current OpenGL render states and matrices.
            ///
            /// This function can be used when you mix SFML drawing
            /// and direct OpenGL rendering. Combined with PopGLStates,
            /// it ensures that:
            /// \li SFML's internal states are not messed up by your OpenGL code
            /// \li your OpenGL states are not modified by a call to a SFML function
            ///
            /// More specifically, it must be used around code that
            /// calls Draw functions. Example:
            ///
            /// // OpenGL code here...
            /// window.PushGLStates();
            /// window.Draw(...);
            /// window.Draw(...);
            /// window.PopGLStates();
            /// // OpenGL code here...
            ///
            /// Note that this function is quite expensive: it saves all the
            /// possible OpenGL states and matrices, even the ones you
            /// don't care about. Therefore it should be used wisely.
            /// It is provided for convenience, but the best results will
            /// be achieved if you handle OpenGL states yourself (because
            /// you know which states have really changed, and need to be
            /// saved and restored). Take a look at the ResetGLStates
            /// function if you do so.
            /// </summary>
            ////////////////////////////////////////////////////////////
            public void PushGLStates()
            {
                sfRenderTexture_pushGLStates(CPointer);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Restore the previously saved OpenGL render states and matrices.
            ///
            /// See the description of PushGLStates to get a detailed
            /// description of these functions.
            /// </summary>
            ////////////////////////////////////////////////////////////
            public void PopGLStates()
            {
                sfRenderTexture_popGLStates(CPointer);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Reset the internal OpenGL states so that the target is ready for drawing.
            ///
            /// This function can be used when you mix SFML drawing
            /// and direct OpenGL rendering, if you choose not to use
            /// PushGLStates/PopGLStates. It makes sure that all OpenGL
            /// states needed by SFML are set, so that subsequent Draw()
            /// calls will work as expected.
            ///
            /// Example:
            ///
            /// // OpenGL code here...
            /// glPushAttrib(...);
            /// window.ResetGLStates();
            /// window.Draw(...);
            /// window.Draw(...);
            /// glPopAttrib(...);
            /// // OpenGL code here...
            /// </summary>
            ////////////////////////////////////////////////////////////
            public void ResetGLStates()
            {
                sfRenderTexture_resetGLStates(CPointer);
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
                       " Size(" + Size + ")" +
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

                sfRenderTexture_destroy(CPointer);

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
            static extern IntPtr sfRenderTexture_create(uint Width, uint Height, bool DepthBuffer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderTexture_destroy(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderTexture_clear(IntPtr CPointer, Color ClearColor);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Vector2u sfRenderTexture_getSize(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfRenderTexture_setActive(IntPtr CPointer, bool Active);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfRenderTexture_saveGLStates(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfRenderTexture_restoreGLStates(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfRenderTexture_display(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderTexture_setView(IntPtr CPointer, IntPtr View);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfRenderTexture_getView(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfRenderTexture_getDefaultView(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntRect sfRenderTexture_getViewport(IntPtr CPointer, IntPtr TargetView);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Vector2i sfRenderTexture_mapCoordsToPixel(IntPtr CPointer, Vector2f point, IntPtr View);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Vector2f sfRenderTexture_mapPixelToCoords(IntPtr CPointer, Vector2i point, IntPtr View);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfRenderTexture_getTexture(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderTexture_setSmooth(IntPtr texture, bool smooth);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfRenderTexture_isSmooth(IntPtr texture);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            unsafe static extern void sfRenderTexture_drawPrimitives(IntPtr CPointer, Vertex* vertexPtr, uint vertexCount, PrimitiveType type, ref RenderStates.MarshalData renderStates);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderTexture_pushGLStates(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderTexture_popGLStates(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderTexture_resetGLStates(IntPtr CPointer);

            #endregion
        }
    }
}
