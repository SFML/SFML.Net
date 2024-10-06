using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using SFML.System;
using SFML.Window;

namespace SFML.Graphics
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Simple wrapper for Window that allows easy
    /// 2D rendering
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class RenderWindow : Window.Window, IRenderTarget
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Create the window with default style, state and creation settings
        /// </summary>
        /// <param name="mode">Video mode to use</param>
        /// <param name="title">Title of the window</param>
        ////////////////////////////////////////////////////////////
        public RenderWindow(VideoMode mode, string title) :
            this(mode, title, Styles.Default, State.Windowed, new ContextSettings(0, 0))
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Create the window with default creation settings
        /// </summary>
        /// <param name="mode">Video mode to use</param>
        /// <param name="title">Title of the window</param>
        /// <param name="style">Window style (Resize | Close by default)</param>
        /// <param name="state">Window state</param>
        ////////////////////////////////////////////////////////////
        public RenderWindow(VideoMode mode, string title, Styles style, State state) :
            this(mode, title, style, state, new ContextSettings(0, 0))
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Create the window
        /// </summary>
        /// <param name="mode">Video mode to use</param>
        /// <param name="title">Title of the window</param>
        /// <param name="style">Window style (Resize | Close by default)</param>
        /// <param name="state">Window state</param>
        /// <param name="settings">Creation parameters</param>
        ////////////////////////////////////////////////////////////
        public RenderWindow(VideoMode mode, string title, Styles style, State state, ContextSettings settings) :
            base(IntPtr.Zero, 0)
        {
            // Copy the string to a null-terminated UTF-32 byte array
            var titleAsUtf32 = Encoding.UTF32.GetBytes(title + '\0');

            unsafe
            {
                fixed (byte* titlePtr = titleAsUtf32)
                {
                    CPointer = sfRenderWindow_createUnicode(mode, (IntPtr)titlePtr, style, state, ref settings);
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
            base(sfRenderWindow_createFromHandle(handle, ref settings), 0) => Initialize();

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Close (destroy) the window.
        /// The Window instance remains valid and you can call
        /// Create to recreate the window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public override void Close() => sfRenderWindow_close(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Tell whether or not the window is opened (ie. has been created).
        /// Note that a hidden window (Show(false))
        /// will still return true
        /// </summary>
        /// <returns>True if the window is opened</returns>
        ////////////////////////////////////////////////////////////
        public override bool IsOpen => sfRenderWindow_isOpen(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Creation settings of the window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public override ContextSettings Settings => sfRenderWindow_getSettings(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Position of the window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public override Vector2i Position
        {
            get => sfRenderWindow_getPosition(CPointer);
            set => sfRenderWindow_setPosition(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Size of the rendering region of the window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public override Vector2u Size
        {
            get => sfRenderWindow_getSize(CPointer);
            set => sfRenderWindow_setSize(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Tell if the render window will use sRGB encoding when drawing on it
        /// </summary>
        ////////////////////////////////////////////////////////////
        public bool IsSrgb => sfRenderWindow_isSrgb(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Change the title of the window
        /// </summary>
        /// <param name="title">New title</param>
        ////////////////////////////////////////////////////////////
        public override void SetTitle(string title)
        {
            // Copy the title to a null-terminated UTF-32 byte array
            var titleAsUtf32 = Encoding.UTF32.GetBytes(title + '\0');

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
        /// <param name="size">Icon's width and height, in pixels</param>
        /// <param name="pixels">Array of pixels, format must be RGBA 32 bits</param>
        ////////////////////////////////////////////////////////////
        public override void SetIcon(Vector2u size, byte[] pixels)
        {
            unsafe
            {
                fixed (byte* pixelsPtr = pixels)
                {
                    sfRenderWindow_setIcon(CPointer, size, pixelsPtr);
                }
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Show or hide the window
        /// </summary>
        /// <param name="visible">True to show the window, false to hide it</param>
        ////////////////////////////////////////////////////////////
        public override void SetVisible(bool visible) => sfRenderWindow_setVisible(CPointer, visible);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Enable / disable vertical synchronization
        /// </summary>
        /// <param name="enable">True to enable v-sync, false to deactivate</param>
        ////////////////////////////////////////////////////////////
        public override void SetVerticalSyncEnabled(bool enable) => sfRenderWindow_setVerticalSyncEnabled(CPointer, enable);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Show or hide the mouse cursor
        /// </summary>
        /// <param name="visible">True to show, false to hide</param>
        ////////////////////////////////////////////////////////////
        public override void SetMouseCursorVisible(bool visible) => sfRenderWindow_setMouseCursorVisible(CPointer, visible);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Grab or release the mouse cursor
        /// </summary>
        /// <param name="grabbed">True to grab, false to release</param>
        /// 
        /// <remarks>
        /// If set, grabs the mouse cursor inside this window's client
        /// area so it may no longer be moved outside its bounds.
        /// Note that grabbing is only active while the window has
        /// focus and calling this function for fullscreen windows
        /// won't have any effect (fullscreen windows always grab the
        /// cursor).
        /// </remarks>
        ////////////////////////////////////////////////////////////
        public override void SetMouseCursorGrabbed(bool grabbed) => sfRenderWindow_setMouseCursorGrabbed(CPointer, grabbed);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Set the displayed cursor to a native system cursor
        /// </summary>
        /// <param name="cursor">Native system cursor type to display</param>
        ////////////////////////////////////////////////////////////
        public override void SetMouseCursor(Cursor cursor) => sfRenderWindow_setMouseCursor(CPointer, cursor.CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Enable or disable automatic key-repeat.
        /// Automatic key-repeat is enabled by default
        /// </summary>
        /// <param name="enable">True to enable, false to disable</param>
        ////////////////////////////////////////////////////////////
        public override void SetKeyRepeatEnabled(bool enable) => sfRenderWindow_setKeyRepeatEnabled(CPointer, enable);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Limit the framerate to a maximum fixed frequency
        /// </summary>
        /// <param name="limit">Framerate limit, in frames per seconds (use 0 to disable limit)</param>
        ////////////////////////////////////////////////////////////
        public override void SetFramerateLimit(uint limit) => sfRenderWindow_setFramerateLimit(CPointer, limit);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Change the joystick threshold, ie. the value below which
        /// no move event will be generated
        /// </summary>
        /// <param name="threshold">New threshold, in range [0, 100]</param>
        ////////////////////////////////////////////////////////////
        public override void SetJoystickThreshold(float threshold) => sfRenderWindow_setJoystickThreshold(CPointer, threshold);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Activate of deactivate the window as the current target
        /// for rendering
        /// </summary>
        /// <param name="active">True to activate, false to deactivate (true by default)</param>
        /// <returns>True if operation was successful, false otherwise</returns>
        ////////////////////////////////////////////////////////////
        public override bool SetActive(bool active) => sfRenderWindow_setActive(CPointer, active);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Request the current window to be made the active
        /// foreground window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public override void RequestFocus() => sfRenderWindow_requestFocus(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Check whether the window has the input focus
        /// </summary>
        /// <returns>True if the window has focus, false otherwise</returns>
        ////////////////////////////////////////////////////////////
        public override bool HasFocus() => sfRenderWindow_hasFocus(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Create a Vulkan rendering surface
        /// </summary>
        /// <param name="vkInstance">Vulkan instance</param>
        /// <param name="vkSurface">Created surface</param>
        /// <param name="vkAllocator">Allocator to use</param>
        /// <returns>True if surface creation was successful, false otherwise</returns>
        ////////////////////////////////////////////////////////////
        public override bool CreateVulkanSurface(IntPtr vkInstance, out IntPtr vkSurface, IntPtr vkAllocator) => sfRenderWindow_createVulkanSurface(CPointer, vkInstance, out vkSurface, vkAllocator);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Display the window on screen
        /// </summary>
        ////////////////////////////////////////////////////////////
        public override void Display() => sfRenderWindow_display(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// OS-specific handle of the window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public override IntPtr NativeHandle => sfRenderWindow_getNativeHandle(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Clear the entire window with black color
        /// </summary>
        ////////////////////////////////////////////////////////////
        public void Clear() => sfRenderWindow_clear(CPointer, Color.Black);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Clear the entire window with a single color
        /// </summary>
        /// <param name="color">Color to use to clear the window</param>
        ////////////////////////////////////////////////////////////
        public void Clear(Color color) => sfRenderWindow_clear(CPointer, color);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Clear the entire target with a single color and stencil value
        /// <para/>
        /// The specified stencil value is truncated to the bit
        /// width of the current stencil buffer.
        /// </summary>
        /// <param name="color">Fill color to use to clear the render target</param> 
        /// <param name="stencilValue">Stencil value to clear to</param>
        ////////////////////////////////////////////////////////////
        public void Clear(Color color, StencilValue stencilValue) => sfRenderWindow_clearColorAndStencil(CPointer, color, stencilValue);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Clear the stencil buffer to a specific value
        /// <para/>
        /// The specified value is truncated to the bit width of
        /// the current stencil buffer.
        /// </summary>
        /// <param name="stencilValue">Stencil value to clear to</param>
        ////////////////////////////////////////////////////////////
        public void ClearStencil(StencilValue stencilValue) => sfRenderWindow_clearStencil(CPointer, stencilValue);


        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Change the current active view
        /// </summary>
        /// <param name="view">New view</param>
        ////////////////////////////////////////////////////////////
        public void SetView(View view) => sfRenderWindow_setView(CPointer, view.CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Return the current active view
        /// </summary>
        /// <returns>The current view</returns>
        ////////////////////////////////////////////////////////////
        public View GetView() => new View(sfRenderWindow_getView(CPointer));

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Default view of the window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public View DefaultView => new View(_defaultView);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the viewport of a view applied to this target
        /// </summary>
        /// <param name="view">Target view</param>
        /// <returns>Viewport rectangle, expressed in pixels in the current target</returns>
        ////////////////////////////////////////////////////////////
        public IntRect GetViewport(View view) => sfRenderWindow_getViewport(CPointer, view.CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the scissor rectangle of a view, applied to this render target
        /// <para/>
        /// The scissor rectangle is defined in the view as a ratio. This
        /// function simply applies this ratio to the current dimensions
        /// of the render target to calculate the pixels rectangle
        /// that the scissor rectangle actually covers in the target.
        /// </summary>
        /// <param name="view">The view for which we want to compute the scissor rectangle</param>
        /// <returns>Scissor rectangle, expressed in pixels</returns>
        ////////////////////////////////////////////////////////////
        public IntRect GetScissor(View view) => sfRenderWindow_getScissor(CPointer, view.CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Convert a point from target coordinates to world
        /// coordinates, using the current view
        ///
        /// This function is an overload of the MapPixelToCoords
        /// function that implicitly uses the current view.
        /// It is equivalent to:
        /// target.MapPixelToCoords(point, target.GetView());
        /// </summary>
        /// <param name="point">Pixel to convert</param>
        /// <returns>The converted point, in "world" coordinates</returns>
        ////////////////////////////////////////////////////////////
        public Vector2f MapPixelToCoords(Vector2i point) => MapPixelToCoords(point, GetView());

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
        public Vector2f MapPixelToCoords(Vector2i point, View view) => sfRenderWindow_mapPixelToCoords(CPointer, point, view != null ? view.CPointer : IntPtr.Zero);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Convert a point from world coordinates to target
        /// coordinates, using the current view
        ///
        /// This function is an overload of the mapCoordsToPixel
        /// function that implicitly uses the current view.
        /// It is equivalent to:
        /// target.MapCoordsToPixel(point, target.GetView());
        /// </summary>
        /// <param name="point">Point to convert</param>
        /// <returns>The converted point, in target coordinates (pixels)</returns>
        ////////////////////////////////////////////////////////////
        public Vector2i MapCoordsToPixel(Vector2f point) => MapCoordsToPixel(point, GetView());

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
        public Vector2i MapCoordsToPixel(Vector2f point, View view) => sfRenderWindow_mapCoordsToPixel(CPointer, point, view != null ? view.CPointer : IntPtr.Zero);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Draw a drawable object to the render-target, with default render states
        /// </summary>
        /// <param name="drawable">Object to draw</param>
        ////////////////////////////////////////////////////////////
        public void Draw(IDrawable drawable) => Draw(drawable, RenderStates.Default);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Draw a drawable object to the render-target
        /// </summary>
        /// <param name="drawable">Object to draw</param>
        /// <param name="states">Render states to use for drawing</param>
        ////////////////////////////////////////////////////////////
        public void Draw(IDrawable drawable, RenderStates states) => drawable.Draw(this, states);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Draw primitives defined by an array of vertices, with default render states
        /// </summary>
        /// <param name="vertices">Pointer to the vertices</param>
        /// <param name="type">Type of primitives to draw</param>
        ////////////////////////////////////////////////////////////
        public void Draw(Vertex[] vertices, PrimitiveType type) => Draw(vertices, type, RenderStates.Default);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Draw primitives defined by an array of vertices
        /// </summary>
        /// <param name="vertices">Pointer to the vertices</param>
        /// <param name="type">Type of primitives to draw</param>
        /// <param name="states">Render states to use for drawing</param>
        ////////////////////////////////////////////////////////////
        public void Draw(Vertex[] vertices, PrimitiveType type, RenderStates states) => Draw(vertices, 0, (uint)vertices.Length, type, states);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Draw primitives defined by a sub-array of vertices, with default render states
        /// </summary>
        /// <param name="vertices">Array of vertices to draw</param>
        /// <param name="start">Index of the first vertex to draw in the array</param>
        /// <param name="count">Number of vertices to draw</param>
        /// <param name="type">Type of primitives to draw</param>
        ////////////////////////////////////////////////////////////
        public void Draw(Vertex[] vertices, uint start, uint count, PrimitiveType type) => Draw(vertices, start, count, type, RenderStates.Default);

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
            var marshaledStates = states.Marshal();

            unsafe
            {
                fixed (Vertex* vertexPtr = vertices)
                {
                    sfRenderWindow_drawPrimitives(CPointer, vertexPtr + start, (UIntPtr)count, type, ref marshaledStates);
                }
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Save the current OpenGL render states and matrices.
        /// </summary>
        /// 
        /// <example>
        /// // OpenGL code here...
        /// window.PushGLStates();
        /// window.Draw(...);
        /// window.Draw(...);
        /// window.PopGLStates();
        /// // OpenGL code here...
        /// </example>
        ///
        /// <remarks>
        /// <para>This function can be used when you mix SFML drawing
        /// and direct OpenGL rendering. Combined with PopGLStates,
        /// it ensures that:</para>
        /// <para>SFML's internal states are not messed up by your OpenGL code</para>
        /// <para>Your OpenGL states are not modified by a call to a SFML function</para>
        ///
        /// <para>More specifically, it must be used around code that
        /// calls Draw functions.</para>
        ///
        /// <para>Note that this function is quite expensive: it saves all the
        /// possible OpenGL states and matrices, even the ones you
        /// don't care about. Therefore it should be used wisely.
        /// It is provided for convenience, but the best results will
        /// be achieved if you handle OpenGL states yourself (because
        /// you know which states have really changed, and need to be
        /// saved and restored). Take a look at the <seealso cref="ResetGLStates"/>
        /// function if you do so.</para>
        /// </remarks>
        ////////////////////////////////////////////////////////////
        public void PushGLStates() => sfRenderWindow_pushGLStates(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Restore the previously saved OpenGL render states and matrices.
        ///
        /// See the description of <seealso cref="PushGLStates"/> to get a detailed
        /// description of these functions.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public void PopGLStates() => sfRenderWindow_popGLStates(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Reset the internal OpenGL states so that the target is ready for drawing.
        /// </summary>
        ///
        /// <remarks>
        /// This function can be used when you mix SFML drawing
        /// and direct OpenGL rendering, if you choose not to use
        /// PushGLStates/PopGLStates. It makes sure that all OpenGL
        /// states needed by SFML are set, so that subsequent Draw()
        /// calls will work as expected.
        /// </remarks>
        /// 
        /// <example>
        /// // OpenGL code here...
        /// glPushAttrib(...);
        /// window.ResetGLStates();
        /// window.Draw(...);
        /// window.Draw(...);
        /// glPopAttrib(...);
        /// // OpenGL code here...
        /// </example>
        ////////////////////////////////////////////////////////////
        public void ResetGLStates() => sfRenderWindow_resetGLStates(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString()
        {
            if (IsInvalid)
            {
                return MakeDisposedObjectString();
            }

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
        protected override bool PollEvent(out Event eventToFill) => sfRenderWindow_pollEvent(CPointer, out eventToFill);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Internal function to get the next event (blocking)
        /// </summary>
        /// <param name="timeout">Maximum time to wait (<see cref="Time.Zero"/> for infinite)</param>
        /// <param name="eventToFill">Variable to fill with the raw pointer to the event structure</param>
        /// <returns>False if any error occurred</returns>
        ////////////////////////////////////////////////////////////
        protected override bool WaitEvent(Time timeout, out Event eventToFill) => sfRenderWindow_waitEvent(CPointer, timeout, out eventToFill);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Internal function to get the mouse position relative to the window.
        /// This function is protected because it is called by another class,
        /// it is not meant to be called by users.
        /// </summary>
        /// <returns>Relative mouse position</returns>
        ////////////////////////////////////////////////////////////
        protected override Vector2i InternalGetMousePosition() => sfMouse_getPositionRenderWindow(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Internal function to set the mouse position relative to the window.
        /// This function is protected because it is called by another class,
        /// it is not meant to be called by users.
        /// </summary>
        /// <param name="position">Relative mouse position</param>
        ////////////////////////////////////////////////////////////
        protected override void InternalSetMousePosition(Vector2i position) => sfMouse_setPositionRenderWindow(position, CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Internal function to get the touch position relative to the window.
        /// This function is protected because it is called by another class of
        /// another module, it is not meant to be called by users.
        /// </summary>
        /// <param name="finger">Finger index</param>
        /// <returns>Relative touch position</returns>
        ////////////////////////////////////////////////////////////
        protected override Vector2i InternalGetTouchPosition(uint finger) => sfTouch_getPositionRenderWindow(finger, CPointer);

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
            {
                _defaultView.Dispose();
            }

            _defaultView = null;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Do common initializations
        /// </summary>
        ////////////////////////////////////////////////////////////
        private void Initialize()
        {
            _defaultView = new View(sfRenderWindow_getDefaultView(CPointer));
            GC.SuppressFinalize(_defaultView);
        }

        private View _defaultView;

        #region Imports
        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfRenderWindow_createUnicode(VideoMode mode, IntPtr title, Styles style, State state, ref ContextSettings settings);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfRenderWindow_createFromHandle(IntPtr handle, ref ContextSettings settings);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_destroy(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_close(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfRenderWindow_isOpen(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ContextSettings sfRenderWindow_getSettings(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfRenderWindow_pollEvent(IntPtr cPointer, out Event evt);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfRenderWindow_waitEvent(IntPtr cPointer, Time timeout, out Event evt);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2i sfRenderWindow_getPosition(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_setPosition(IntPtr cPointer, Vector2i position);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2u sfRenderWindow_getSize(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfRenderWindow_isSrgb(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_setSize(IntPtr cPointer, Vector2u size);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_setUnicodeTitle(IntPtr cPointer, IntPtr title);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void sfRenderWindow_setIcon(IntPtr cPointer, Vector2u size, byte* pixels);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_setVisible(IntPtr cPointer, bool visible);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_setVerticalSyncEnabled(IntPtr cPointer, bool enable);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_setMouseCursorVisible(IntPtr cPointer, bool visible);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_setMouseCursorGrabbed(IntPtr cPointer, bool grabbed);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_setMouseCursor(IntPtr window, IntPtr cursor);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_setKeyRepeatEnabled(IntPtr cPointer, bool enable);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_setFramerateLimit(IntPtr cPointer, uint limit);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_setJoystickThreshold(IntPtr cPointer, float threshold);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfRenderWindow_setActive(IntPtr cPointer, bool active);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_requestFocus(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfRenderWindow_hasFocus(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_display(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfRenderWindow_getNativeHandle(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_clear(IntPtr cPointer, Color clearColor);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_clearStencil(IntPtr cPointer, StencilValue stencilValue);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_clearColorAndStencil(IntPtr cPointer, Color clearColor, StencilValue stencilValue);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_setView(IntPtr cPointer, IntPtr view);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfRenderWindow_getView(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfRenderWindow_getDefaultView(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntRect sfRenderWindow_getViewport(IntPtr cPointer, IntPtr targetView);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntRect sfRenderWindow_getScissor(IntPtr cPointer, IntPtr targetView);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f sfRenderWindow_mapPixelToCoords(IntPtr cPointer, Vector2i point, IntPtr view);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2i sfRenderWindow_mapCoordsToPixel(IntPtr cPointer, Vector2f point, IntPtr view);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void sfRenderWindow_drawPrimitives(IntPtr cPointer, Vertex* vertexPtr, UIntPtr vertexCount, PrimitiveType type, ref RenderStates.MarshalData renderStates);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_pushGLStates(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_popGLStates(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_resetGLStates(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2i sfMouse_getPositionRenderWindow(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfMouse_setPositionRenderWindow(Vector2i position, IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2i sfTouch_getPositionRenderWindow(uint finger, IntPtr relativeTo);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfRenderWindow_createVulkanSurface(IntPtr cPointer, IntPtr vkInstance, out IntPtr surface, IntPtr vkAllocator);
        #endregion
    }
}
