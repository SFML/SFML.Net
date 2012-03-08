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
        /// Simple wrapper for Window that allows easy
        /// 2D rendering
        /// </summary>
        ////////////////////////////////////////////////////////////
        public class RenderWindow : SFML.Window.Window, RenderTarget
        {
            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Create the window with default style and creation settings
            /// </summary>
            /// <param name="mode">Video mode to use</param>
            /// <param name="title">Title of the window</param>
            ////////////////////////////////////////////////////////////
            public RenderWindow(VideoMode mode, string title) :
                this(mode, title, Styles.Default, new ContextSettings(0, 0))
            {
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Create the window with default creation settings
            /// </summary>
            /// <param name="mode">Video mode to use</param>
            /// <param name="title">Title of the window</param>
            /// <param name="style">Window style (Resize | Close by default)</param>
            ////////////////////////////////////////////////////////////
            public RenderWindow(VideoMode mode, string title, Styles style) :
                this(mode, title, style, new ContextSettings(0, 0))
            {
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Create the window
            /// </summary>
            /// <param name="mode">Video mode to use</param>
            /// <param name="title">Title of the window</param>
            /// <param name="style">Window style (Resize | Close by default)</param>
            /// <param name="settings">Creation parameters</param>
            ////////////////////////////////////////////////////////////
            public RenderWindow(VideoMode mode, string title, Styles style, ContextSettings settings) :
                base(sfRenderWindow_Create(mode, title, style, ref settings), 0)
            {
                Initialize();
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Create the window from an existing control with default creation settings
            /// </summary>
            /// <param name="handle">Platform-specific handle of the control</param>
            ////////////////////////////////////////////////////////////
            public RenderWindow(IntPtr handle) :
                this(handle, new ContextSettings(0, 0))
            {
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Create the window from an existing control
            /// </summary>
            /// <param name="handle">Platform-specific handle of the control</param>
            /// <param name="settings">Creation parameters</param>
            ////////////////////////////////////////////////////////////
            public RenderWindow(IntPtr handle, ContextSettings settings) :
                base(sfRenderWindow_CreateFromHandle(handle, ref settings), 0)
            {
                Initialize();
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Tell whether or not the window is opened (ie. has been created).
            /// Note that a hidden window (Show(false))
            /// will still return true
            /// </summary>
            /// <returns>True if the window is opened</returns>
            ////////////////////////////////////////////////////////////
            public override bool IsOpen()
            {
                return sfRenderWindow_IsOpen(CPointer);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Close (destroy) the window.
            /// The Window instance remains valid and you can call
            /// Create to recreate the window
            /// </summary>
            ////////////////////////////////////////////////////////////
            public override void Close()
            {
                sfRenderWindow_Close(CPointer);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Display the window on screen
            /// </summary>
            ////////////////////////////////////////////////////////////
            public override void Display()
            {
                sfRenderWindow_Display(CPointer);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Creation settings of the window
            /// </summary>
            ////////////////////////////////////////////////////////////
            public override ContextSettings Settings
            {
                get {return sfRenderWindow_GetSettings(CPointer);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Position of the window
            /// </summary>
            ////////////////////////////////////////////////////////////
            public override Vector2i Position
            {
                get { return sfRenderWindow_GetPosition(CPointer); }
                set { sfRenderWindow_SetPosition(CPointer, value); }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Size of the rendering region of the window
            /// </summary>
            ////////////////////////////////////////////////////////////
            public override Vector2u Size
            {
                get { return sfRenderWindow_GetSize(CPointer); }
                set { sfRenderWindow_SetSize(CPointer, value); }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Change the title of the window
            /// </summary>
            /// <param name="title">New title</param>
            ////////////////////////////////////////////////////////////
            public override void SetTitle(string title)
            {
                sfRenderWindow_SetTitle(CPointer, title);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Change the window's icon
            /// </summary>
            /// <param name="width">Icon's width, in pixels</param>
            /// <param name="height">Icon's height, in pixels</param>
            /// <param name="pixels">Array of pixels, format must be RGBA 32 bits</param>
            ////////////////////////////////////////////////////////////
            public override void SetIcon(uint width, uint height, byte[] pixels)
            {
                unsafe
                {
                    fixed (byte* PixelsPtr = pixels)
                    {
                        sfRenderWindow_SetIcon(CPointer, width, height, PixelsPtr);
                    }
                }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Show or hide the window
            /// </summary>
            /// <param name="visible">True to show the window, false to hide it</param>
            ////////////////////////////////////////////////////////////
            public override void SetVisible(bool visible)
            {
                sfRenderWindow_SetVisible(CPointer, visible);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Show or hide the mouse cursor
            /// </summary>
            /// <param name="visible">True to show, false to hide</param>
            ////////////////////////////////////////////////////////////
            public override void SetMouseCursorVisible(bool visible)
            {
                sfRenderWindow_SetMouseCursorVisible(CPointer, visible);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Enable / disable vertical synchronization
            /// </summary>
            /// <param name="enable">True to enable v-sync, false to deactivate</param>
            ////////////////////////////////////////////////////////////
            public override void SetVerticalSyncEnabled(bool enable)
            {
                sfRenderWindow_SetVerticalSyncEnabled(CPointer, enable);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Enable or disable automatic key-repeat.
            /// Automatic key-repeat is enabled by default
            /// </summary>
            /// <param name="enable">True to enable, false to disable</param>
            ////////////////////////////////////////////////////////////
            public override void SetKeyRepeatEnabled(bool enable)
            {
                sfRenderWindow_SetKeyRepeatEnabled(CPointer, enable);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Activate of deactivate the window as the current target
            /// for rendering
            /// </summary>
            /// <param name="active">True to activate, false to deactivate (true by default)</param>
            /// <returns>True if operation was successful, false otherwise</returns>
            ////////////////////////////////////////////////////////////
            public override bool SetActive(bool active)
            {
                return sfRenderWindow_SetActive(CPointer, active);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Limit the framerate to a maximum fixed frequency
            /// </summary>
            /// <param name="limit">Framerate limit, in frames per seconds (use 0 to disable limit)</param>
            ////////////////////////////////////////////////////////////
            public override void SetFramerateLimit(uint limit)
            {
                sfRenderWindow_SetFramerateLimit(CPointer, limit);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Change the joystick threshold, ie. the value below which
            /// no move event will be generated
            /// </summary>
            /// <param name="threshold">New threshold, in range [0, 100]</param>
            ////////////////////////////////////////////////////////////
            public override void SetJoystickThreshold(float threshold)
            {
                sfRenderWindow_SetJoystickThreshold(CPointer, threshold);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// OS-specific handle of the window
            /// </summary>
            ////////////////////////////////////////////////////////////
            public override IntPtr SystemHandle
            {
                get {return sfRenderWindow_GetSystemHandle(CPointer);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Default view of the window
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
                return new View(sfRenderWindow_GetView(CPointer));
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Change the current active view
            /// </summary>
            /// <param name="view">New view</param>
            ////////////////////////////////////////////////////////////
            public void SetView(View view)
            {
                sfRenderWindow_SetView(CPointer, view.CPointer);
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
                return sfRenderWindow_GetViewport(CPointer, view.CPointer);
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
                sfRenderWindow_ConvertCoords(CPointer, x, y, out point.X, out point.Y, view.CPointer);

                return point;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Clear the entire window with black color
            /// </summary>
            ////////////////////////////////////////////////////////////
            public void Clear()
            {
                sfRenderWindow_Clear(CPointer, Color.Black);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Clear the entire window with a single color
            /// </summary>
            /// <param name="color">Color to use to clear the window</param>
            ////////////////////////////////////////////////////////////
            public void Clear(Color color)
            {
                sfRenderWindow_Clear(CPointer, color);
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
                RenderStates.MarshalData marshaledStates = states.Marshal();

                unsafe
                {
                    fixed (Vertex* vertexPtr = vertices)
                    {
                        sfRenderWindow_DrawPrimitives(CPointer, vertexPtr, (uint)vertices.Length, type, ref marshaledStates);
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
                sfRenderWindow_PushGLStates(CPointer);
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
                sfRenderWindow_PopGLStates(CPointer);
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
                sfRenderWindow_ResetGLStates(CPointer);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Capture the current contents of the window into an image
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Image Capture()
            {
                return new Image(sfRenderWindow_Capture(CPointer));
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Provide a string describing the object
            /// </summary>
            /// <returns>String description of the object</returns>
            ////////////////////////////////////////////////////////////
            public override string ToString()
            {
                return "[RenderWindow]" +
                       " Size(" + Size + ")" +
                       " Position(" + Position + ")" +
                       " Settings(" + Settings + ")" +
                       " DefaultView(" + DefaultView + ")" +
                       " View(" + GetView() + ")";
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Internal function to get the next event
            /// </summary>
            /// <param name="eventToFill">Variable to fill with the raw pointer to the event structure</param>
            /// <returns>True if there was an event, false otherwise</returns>
            ////////////////////////////////////////////////////////////
            protected override bool PollEvent(out Event eventToFill)
            {
                return sfRenderWindow_PollEvent(CPointer, out eventToFill);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Internal function to get the next event (blocking)
            /// </summary>
            /// <param name="eventToFill">Variable to fill with the raw pointer to the event structure</param>
            /// <returns>False if any error occured</returns>
            ////////////////////////////////////////////////////////////
            protected override bool WaitEvent(out Event eventToFill)
            {
                return sfRenderWindow_WaitEvent(CPointer, out eventToFill);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Handle the destruction of the object
            /// </summary>
            /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
            ////////////////////////////////////////////////////////////
            protected override void Destroy(bool disposing)
            {
                sfRenderWindow_Destroy(CPointer);

                if (disposing)
                    myDefaultView.Dispose();
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Do common initializations
            /// </summary>
            ////////////////////////////////////////////////////////////
            private void Initialize()
            {
                myDefaultView = new View(sfRenderWindow_GetDefaultView(CPointer));
                GC.SuppressFinalize(myDefaultView);
            }

            private View myDefaultView = null;

            #region Imports
            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfRenderWindow_Create(VideoMode Mode, string Title, Styles Style, ref ContextSettings Params);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfRenderWindow_CreateFromHandle(IntPtr Handle, ref ContextSettings Params);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_Destroy(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfRenderWindow_IsOpen(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_Close(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfRenderWindow_PollEvent(IntPtr CPointer, out Event Evt);
            
            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfRenderWindow_WaitEvent(IntPtr CPointer, out Event Evt);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_Clear(IntPtr CPointer, Color ClearColor);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_Display(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern ContextSettings sfRenderWindow_GetSettings(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Vector2i sfRenderWindow_GetPosition(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_SetPosition(IntPtr CPointer, Vector2i position);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Vector2u sfRenderWindow_GetSize(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_SetSize(IntPtr CPointer, Vector2u size);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_SetTitle(IntPtr CPointer, string title);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            unsafe static extern void sfRenderWindow_SetIcon(IntPtr CPointer, uint Width, uint Height, byte* Pixels);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_SetVisible(IntPtr CPointer, bool visible);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_SetMouseCursorVisible(IntPtr CPointer, bool visible);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_SetVerticalSyncEnabled(IntPtr CPointer, bool Enable);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_SetKeyRepeatEnabled(IntPtr CPointer, bool Enable);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfRenderWindow_SetActive(IntPtr CPointer, bool Active);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfRenderWindow_SaveGLStates(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfRenderWindow_RestoreGLStates(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_SetFramerateLimit(IntPtr CPointer, uint Limit);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern uint sfRenderWindow_GetFrameTime(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_SetJoystickThreshold(IntPtr CPointer, float Threshold);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_SetView(IntPtr CPointer, IntPtr View);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfRenderWindow_GetView(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfRenderWindow_GetDefaultView(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntRect sfRenderWindow_GetViewport(IntPtr CPointer, IntPtr TargetView);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_ConvertCoords(IntPtr CPointer, uint WindowX, uint WindowY, out float ViewX, out float ViewY, IntPtr TargetView);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            unsafe static extern void sfRenderWindow_DrawPrimitives(IntPtr CPointer, Vertex* vertexPtr, uint vertexCount, PrimitiveType type, ref RenderStates.MarshalData renderStates);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_PushGLStates(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_PopGLStates(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_ResetGLStates(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfRenderWindow_GetSystemHandle(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfRenderWindow_Capture(IntPtr CPointer);

            #endregion
        }
    }
}
