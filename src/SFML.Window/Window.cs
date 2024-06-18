using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using SFML.System;

namespace SFML.Window
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Window that serves as a target for OpenGL rendering
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class Window : WindowBase
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Create the window with default style and creation settings
        /// </summary>
        /// <param name="mode">Video mode to use</param>
        /// <param name="title">Title of the window</param>
        ////////////////////////////////////////////////////////////
        public Window(VideoMode mode, string title) :
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
        public Window(VideoMode mode, string title, Styles style) :
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
        public Window(VideoMode mode, string title, Styles style, ContextSettings settings) :
            base(IntPtr.Zero)
        {
            // Copy the title to a null-terminated UTF-32 byte array
            byte[] titleAsUtf32 = Encoding.UTF32.GetBytes(title + '\0');

            unsafe
            {
                fixed (byte* titlePtr = titleAsUtf32)
                {
                    CPointer = sfWindow_createUnicode(mode, (IntPtr)titlePtr, style, ref settings);
                }
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Create the window from an existing control with default creation settings
        /// </summary>
        /// <param name="handle">Platform-specific handle of the control</param>
        ////////////////////////////////////////////////////////////
        public Window(IntPtr handle) :
            this(handle, new ContextSettings(0, 0))
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Create the window from an existing control
        /// </summary>
        /// <param name="Handle">Platform-specific handle of the control</param>
        /// <param name="settings">Creation parameters</param>
        ////////////////////////////////////////////////////////////
        public Window(IntPtr Handle, ContextSettings settings) :
            base(sfWindow_createFromHandle(Handle, ref settings))
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Tell whether or not the window is opened (ie. has been created).
        /// Note that a hidden window (Show(false))
        /// will still return true
        /// </summary>
        /// <returns>True if the window is opened</returns>
        ////////////////////////////////////////////////////////////
        public override bool IsOpen
        {
            get { return sfWindow_isOpen(CPointer); }
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
            sfWindow_close(CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Display the window on screen
        /// </summary>
        ////////////////////////////////////////////////////////////
        public virtual void Display()
        {
            sfWindow_display(CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Creation settings of the window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public virtual ContextSettings Settings
        {
            get { return sfWindow_getSettings(CPointer); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Position of the window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public override Vector2i Position
        {
            get { return sfWindow_getPosition(CPointer); }
            set { sfWindow_setPosition(CPointer, value); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Size of the rendering region of the window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public override Vector2u Size
        {
            get { return sfWindow_getSize(CPointer); }
            set { sfWindow_setSize(CPointer, value); }
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
            byte[] titleAsUtf32 = Encoding.UTF32.GetBytes(title + '\0');

            unsafe
            {
                fixed (byte* titlePtr = titleAsUtf32)
                {
                    sfWindow_setUnicodeTitle(CPointer, (IntPtr)titlePtr);
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
        public override void SetIcon(uint width, uint height, Span<byte> pixels)
        {
            unsafe
            {
                fixed (byte* ptr = pixels)
                {
                    sfWindow_setIcon(CPointer, width, height, ptr);
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
            sfWindow_setVisible(CPointer, visible);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Show or hide the mouse cursor
        /// </summary>
        /// <param name="show">True to show, false to hide</param>
        ////////////////////////////////////////////////////////////
        public override void SetMouseCursorVisible(bool show)
        {
            sfWindow_setMouseCursorVisible(CPointer, show);
        }

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
        public override void SetMouseCursorGrabbed(bool grabbed)
        {
            sfWindow_setMouseCursorGrabbed(CPointer, grabbed);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Set the displayed cursor to a native system cursor
        /// </summary>
        /// <param name="cursor">Native system cursor type to display</param>
        ////////////////////////////////////////////////////////////
        public override void SetMouseCursor(Cursor cursor)
        {
            sfWindow_setMouseCursor(CPointer, cursor.CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Enable / disable vertical synchronization
        /// </summary>
        /// <param name="enable">True to enable v-sync, false to deactivate</param>
        ////////////////////////////////////////////////////////////
        public virtual void SetVerticalSyncEnabled(bool enable)
        {
            sfWindow_setVerticalSyncEnabled(CPointer, enable);
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
            sfWindow_setKeyRepeatEnabled(CPointer, enable);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Activate the window as the current target
        /// for rendering
        /// </summary>
        /// <returns>True if operation was successful, false otherwise</returns>
        ////////////////////////////////////////////////////////////
        public virtual bool SetActive()
        {
            return SetActive(true);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Activate of deactivate the window as the current target
        /// for rendering
        /// </summary>
        /// <param name="active">True to activate, false to deactivate (true by default)</param>
        /// <returns>True if operation was successful, false otherwise</returns>
        ////////////////////////////////////////////////////////////
        public virtual bool SetActive(bool active)
        {
            return sfWindow_setActive(CPointer, active);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Limit the framerate to a maximum fixed frequency
        /// </summary>
        /// <param name="limit">Framerate limit, in frames per seconds (use 0 to disable limit)</param>
        ////////////////////////////////////////////////////////////
        public virtual void SetFramerateLimit(uint limit)
        {
            sfWindow_setFramerateLimit(CPointer, limit);
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
            sfWindow_setJoystickThreshold(CPointer, threshold);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// OS-specific handle of the window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public override IntPtr SystemHandle
        {
            get { return sfWindow_getSystemHandle(CPointer); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Request the current window to be made the active
        /// foreground window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public override void RequestFocus()
        {
            sfWindow_requestFocus(CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Check whether the window has the input focus
        /// </summary>
        /// <returns>True if the window has focus, false otherwise</returns>
        ////////////////////////////////////////////////////////////
        public override bool HasFocus()
        {
            return sfWindow_hasFocus(CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Create a Vulkan rendering surface
        /// </summary>
        /// <param name="vkInstance">Vulkan instance</param>
        /// <param name="vkSurface">Created surface</param>
        /// <param name="vkAllocator">Allocator to use</param>
        /// <returns>True if surface creation was successful, false otherwise</returns>
        ////////////////////////////////////////////////////////////
        public override bool CreateVulkanSurface(IntPtr vkInstance, out IntPtr vkSurface, IntPtr vkAllocator)
        {
            return sfWindow_createVulkanSurface(CPointer, vkInstance, out vkSurface, vkAllocator);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString()
        {
            return "[Window]" +
                   " Size(" + Size + ")" +
                   " Position(" + Position + ")" +
                   " Settings(" + Settings + ")";
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor for derived classes
        /// </summary>
        /// <param name="cPointer">Pointer to the internal object in the C API</param>
        /// <param name="dummy">Internal hack :)</param>
        ////////////////////////////////////////////////////////////
        protected Window(IntPtr cPointer, int dummy) :
            base(cPointer, 0)
        {
            // TODO : find a cleaner way of separating this constructor from Window(IntPtr handle)
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Internal function to get the mouse position relative to the window.
        /// This function is protected because it is called by another class of
        /// another module, it is not meant to be called by users.
        /// </summary>
        /// <returns>Relative mouse position</returns>
        ////////////////////////////////////////////////////////////
        protected internal override Vector2i InternalGetMousePosition()
        {
            return sfMouse_getPosition(CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Internal function to set the mouse position relative to the window.
        /// This function is protected because it is called by another class of
        /// another module, it is not meant to be called by users.
        /// </summary>
        /// <param name="position">Relative mouse position</param>
        ////////////////////////////////////////////////////////////
        protected internal override void InternalSetMousePosition(Vector2i position)
        {
            sfMouse_setPosition(position, CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Internal function to get the touch position relative to the window.
        /// This function is protected because it is called by another class of
        /// another module, it is not meant to be called by users.
        /// </summary>
        /// <param name="Finger">Finger index</param>
        /// <returns>Relative touch position</returns>
        ////////////////////////////////////////////////////////////
        protected internal override Vector2i InternalGetTouchPosition(uint Finger)
        {
            return sfTouch_getPosition(Finger, CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Internal function to get the next event (non-blocking)
        /// </summary>
        /// <param name="eventToFill">Variable to fill with the raw pointer to the event structure</param>
        /// <returns>True if there was an event, false otherwise</returns>
        ////////////////////////////////////////////////////////////
        protected override bool PollEvent(out Event eventToFill)
        {
            return sfWindow_pollEvent(CPointer, out eventToFill);
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
            return sfWindow_waitEvent(CPointer, out eventToFill);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
        ////////////////////////////////////////////////////////////
        protected override void Destroy(bool disposing)
        {
            sfWindow_destroy(CPointer);
        }

        #region Imports
        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfWindow_createUnicode(VideoMode Mode, IntPtr Title, Styles Style, ref ContextSettings Params);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfWindow_createFromHandle(IntPtr Handle, ref ContextSettings Params);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindow_destroy(IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfWindow_isOpen(IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindow_close(IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfWindow_pollEvent(IntPtr CPointer, out Event Evt);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfWindow_waitEvent(IntPtr CPointer, out Event Evt);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindow_display(IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ContextSettings sfWindow_getSettings(IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2i sfWindow_getPosition(IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindow_setPosition(IntPtr CPointer, Vector2i position);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2u sfWindow_getSize(IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindow_setSize(IntPtr CPointer, Vector2u size);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindow_setUnicodeTitle(IntPtr CPointer, IntPtr title);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private unsafe static extern void sfWindow_setIcon(IntPtr CPointer, uint Width, uint Height, byte* Pixels);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindow_setVisible(IntPtr CPointer, bool visible);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindow_setMouseCursorVisible(IntPtr CPointer, bool Show);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindow_setMouseCursorGrabbed(IntPtr CPointer, bool grabbed);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindow_setMouseCursor(IntPtr CPointer, IntPtr cursor);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindow_setVerticalSyncEnabled(IntPtr CPointer, bool Enable);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindow_setKeyRepeatEnabled(IntPtr CPointer, bool Enable);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfWindow_setActive(IntPtr CPointer, bool Active);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindow_setFramerateLimit(IntPtr CPointer, uint Limit);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindow_setJoystickThreshold(IntPtr CPointer, float Threshold);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfWindow_getSystemHandle(IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindow_requestFocus(IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfWindow_hasFocus(IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2i sfMouse_getPosition(IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfMouse_setPosition(Vector2i position, IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2i sfTouch_getPosition(uint Finger, IntPtr RelativeTo);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfWindow_createVulkanSurface(IntPtr CPointer, IntPtr vkInstance, out IntPtr surface, IntPtr vkAllocator);
        #endregion
    }
}
