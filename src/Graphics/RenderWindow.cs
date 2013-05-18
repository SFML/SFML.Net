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
                base(IntPtr.Zero, 0)
            {
                 // Copy the string to a null-terminated UTF-32 byte array
                byte[] titleAsUtf32 = System.Text.Encoding.UTF32.GetBytes(title + '\0');

                unsafe
                {
                    fixed (byte* titlePtr = titleAsUtf32)
                    {
                        SetThis(sfRenderWindow_createUnicode(mode, (IntPtr)titlePtr, style, ref settings));
                    }
                }
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
                base(sfRenderWindow_createFromHandle(handle, ref settings), 0)
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
                return sfRenderWindow_isOpen(CPointer);
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
                sfRenderWindow_close(CPointer);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Display the window on screen
            /// </summary>
            ////////////////////////////////////////////////////////////
            public override void Display()
            {
                sfRenderWindow_display(CPointer);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Creation settings of the window
            /// </summary>
            ////////////////////////////////////////////////////////////
            public override ContextSettings Settings
            {
                get {return sfRenderWindow_getSettings(CPointer);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Position of the window
            /// </summary>
            ////////////////////////////////////////////////////////////
            public override Vector2i Position
            {
                get { return sfRenderWindow_getPosition(CPointer); }
                set { sfRenderWindow_setPosition(CPointer, value); }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Size of the rendering region of the window
            /// </summary>
            ////////////////////////////////////////////////////////////
            public override Vector2u Size
            {
                get { return sfRenderWindow_getSize(CPointer); }
                set { sfRenderWindow_setSize(CPointer, value); }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Change the title of the window
            /// </summary>
            /// <param name="title">New title</param>
            ////////////////////////////////////////////////////////////
            public override void SetTitle(string title)
            {
                // Copy the title to a null-terminated UTF-32 byte array
                byte[] titleAsUtf32 = System.Text.Encoding.UTF32.GetBytes(title + '\0');

                unsafe
                {
                    fixed (byte* titlePtr = titleAsUtf32)
                    {
                        sfRenderWindow_setUnicodeTitle(CPointer, (IntPtr)titlePtr);
                    }
                }
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
                        sfRenderWindow_setIcon(CPointer, width, height, PixelsPtr);
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
                sfRenderWindow_setVisible(CPointer, visible);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Show or hide the mouse cursor
            /// </summary>
            /// <param name="visible">True to show, false to hide</param>
            ////////////////////////////////////////////////////////////
            public override void SetMouseCursorVisible(bool visible)
            {
                sfRenderWindow_setMouseCursorVisible(CPointer, visible);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Enable / disable vertical synchronization
            /// </summary>
            /// <param name="enable">True to enable v-sync, false to deactivate</param>
            ////////////////////////////////////////////////////////////
            public override void SetVerticalSyncEnabled(bool enable)
            {
                sfRenderWindow_setVerticalSyncEnabled(CPointer, enable);
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
                sfRenderWindow_setKeyRepeatEnabled(CPointer, enable);
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
                return sfRenderWindow_setActive(CPointer, active);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Limit the framerate to a maximum fixed frequency
            /// </summary>
            /// <param name="limit">Framerate limit, in frames per seconds (use 0 to disable limit)</param>
            ////////////////////////////////////////////////////////////
            public override void SetFramerateLimit(uint limit)
            {
                sfRenderWindow_setFramerateLimit(CPointer, limit);
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
                sfRenderWindow_setJoystickThreshold(CPointer, threshold);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// OS-specific handle of the window
            /// </summary>
            ////////////////////////////////////////////////////////////
            public override IntPtr SystemHandle
            {
                get {return sfRenderWindow_getSystemHandle(CPointer);}
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
                return new View(sfRenderWindow_getView(CPointer));
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Change the current active view
            /// </summary>
            /// <param name="view">New view</param>
            ////////////////////////////////////////////////////////////
            public void SetView(View view)
            {
                sfRenderWindow_setView(CPointer, view.CPointer);
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
                return sfRenderWindow_getViewport(CPointer, view.CPointer);
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
                return sfRenderWindow_mapPixelToCoords(CPointer, point, view != null ? view.CPointer : IntPtr.Zero);
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
                return sfRenderWindow_mapCoordsToPixel(CPointer, point, view != null ? view.CPointer : IntPtr.Zero);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Clear the entire window with black color
            /// </summary>
            ////////////////////////////////////////////////////////////
            public void Clear()
            {
                sfRenderWindow_clear(CPointer, Color.Black);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Clear the entire window with a single color
            /// </summary>
            /// <param name="color">Color to use to clear the window</param>
            ////////////////////////////////////////////////////////////
            public void Clear(Color color)
            {
                sfRenderWindow_clear(CPointer, color);
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
                        sfRenderWindow_drawPrimitives(CPointer, vertexPtr + start, count, type, ref marshaledStates);
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
                sfRenderWindow_pushGLStates(CPointer);
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
                sfRenderWindow_popGLStates(CPointer);
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
                sfRenderWindow_resetGLStates(CPointer);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Capture the current contents of the window into an image
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Image Capture()
            {
                return new Image(sfRenderWindow_capture(CPointer));
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
                return sfRenderWindow_pollEvent(CPointer, out eventToFill);
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
                return sfRenderWindow_waitEvent(CPointer, out eventToFill);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Internal function to get the mouse position relatively to the window.
            /// This function is public because it is called by another class,
            /// it is not meant to be called by users.
            /// </summary>
            /// <returns>Relative mouse position</returns>
            ////////////////////////////////////////////////////////////
            public override Vector2i InternalGetMousePosition()
            {
                return sfMouse_getPositionRenderWindow(CPointer);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Internal function to set the mouse position relatively to the window.
            /// This function is public because it is called by another class,
            /// it is not meant to be called by users.
            /// </summary>
            /// <param name="position">Relative mouse position</param>
            ////////////////////////////////////////////////////////////
            public override void InternalSetMousePosition(Vector2i position)
            {
                sfMouse_setPositionRenderWindow(position, CPointer);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Handle the destruction of the object
            /// </summary>
            /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
            ////////////////////////////////////////////////////////////
            protected override void Destroy(bool disposing)
            {
                sfRenderWindow_destroy(CPointer);

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
                myDefaultView = new View(sfRenderWindow_getDefaultView(CPointer));
                GC.SuppressFinalize(myDefaultView);
            }

            private View myDefaultView = null;

            #region Imports
            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfRenderWindow_create(VideoMode Mode, string Title, Styles Style, ref ContextSettings Params);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfRenderWindow_createUnicode(VideoMode Mode, IntPtr Title, Styles Style, ref ContextSettings Params);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfRenderWindow_createFromHandle(IntPtr Handle, ref ContextSettings Params);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_destroy(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfRenderWindow_isOpen(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_close(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfRenderWindow_pollEvent(IntPtr CPointer, out Event Evt);
            
            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfRenderWindow_waitEvent(IntPtr CPointer, out Event Evt);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_clear(IntPtr CPointer, Color ClearColor);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_display(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern ContextSettings sfRenderWindow_getSettings(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Vector2i sfRenderWindow_getPosition(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_setPosition(IntPtr CPointer, Vector2i position);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Vector2u sfRenderWindow_getSize(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_setSize(IntPtr CPointer, Vector2u size);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_setTitle(IntPtr CPointer, string title);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_setUnicodeTitle(IntPtr CPointer, IntPtr title);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            unsafe static extern void sfRenderWindow_setIcon(IntPtr CPointer, uint Width, uint Height, byte* Pixels);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_setVisible(IntPtr CPointer, bool visible);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_setMouseCursorVisible(IntPtr CPointer, bool visible);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_setVerticalSyncEnabled(IntPtr CPointer, bool Enable);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_setKeyRepeatEnabled(IntPtr CPointer, bool Enable);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfRenderWindow_setActive(IntPtr CPointer, bool Active);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfRenderWindow_saveGLStates(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern bool sfRenderWindow_restoreGLStates(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_setFramerateLimit(IntPtr CPointer, uint Limit);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern uint sfRenderWindow_getFrameTime(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_setJoystickThreshold(IntPtr CPointer, float Threshold);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_setView(IntPtr CPointer, IntPtr View);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfRenderWindow_getView(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfRenderWindow_getDefaultView(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntRect sfRenderWindow_getViewport(IntPtr CPointer, IntPtr TargetView);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Vector2i sfRenderWindow_mapCoordsToPixel(IntPtr CPointer, Vector2f point, IntPtr View);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Vector2f sfRenderWindow_mapPixelToCoords(IntPtr CPointer, Vector2i point, IntPtr View);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            unsafe static extern void sfRenderWindow_drawPrimitives(IntPtr CPointer, Vertex* vertexPtr, uint vertexCount, PrimitiveType type, ref RenderStates.MarshalData renderStates);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_pushGLStates(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_popGLStates(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_resetGLStates(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfRenderWindow_getSystemHandle(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfRenderWindow_capture(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Vector2i sfMouse_getPositionRenderWindow(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfMouse_setPositionRenderWindow(Vector2i position, IntPtr CPointer);

            #endregion
        }
    }
}
