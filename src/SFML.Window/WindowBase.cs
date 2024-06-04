using System;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using SFML.System;

namespace SFML.Window
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Enumeration of window creation styles
    /// </summary>
    ////////////////////////////////////////////////////////////
    [Flags]
    public enum Styles
    {
        /// <summary>No border / title bar (this flag and all others are mutually exclusive)</summary>
        None = 0,

        /// <summary>Title bar + fixed border</summary>
        Titlebar = 1 << 0,

        /// <summary>Titlebar + resizable border + maximize button</summary>
        Resize = 1 << 1,

        /// <summary>Titlebar + close button</summary>
        Close = 1 << 2,

        /// <summary>Fullscreen mode (this flag and all others are mutually exclusive))</summary>
        Fullscreen = 1 << 3,

        /// <summary>Default window style (titlebar + resize + close)</summary>
        Default = Titlebar | Resize | Close
    }


    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Window that serves as a base for other windows
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class WindowBase : ObjectBase
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// The event management for this window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public readonly IEventMan SfmlEventManager;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Create the window with default creation settings
        /// </summary>
        /// <param name="mode">Video mode to use</param>
        /// <param name="title">Title of the window</param>
        /// <param name="style">Window style (Resize | Close by default)</param>
        /// <param name="manager">A custom event manager. By default, a SubscribeManager object is created</param>
        ////////////////////////////////////////////////////////////
        public WindowBase(VideoMode mode, string title, Styles style = Styles.Default, IEventMan manager = null) :
            base(IntPtr.Zero)
        {
            // Copy the title to a null-terminated UTF-32 byte array
            var titleAsUtf32 = Encoding.UTF32.GetBytes(title + '\0');

            unsafe
            {
                fixed (byte* titlePtr = titleAsUtf32)
                {
                    CPointer = sfWindowBase_createUnicode(mode, (IntPtr)titlePtr, style);
                }
            }
            if (manager == null)
            {
                manager = new SubscribeManager() { Parent = this };
            }
            SfmlEventManager = manager;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Create the window from an existing control
        /// </summary>
        /// <param name="Handle">Platform-specific handle of the control</param>
        /// <param name="manager">A custom event manager. By default, a SubscribeManager object is created</param>
        ////////////////////////////////////////////////////////////
        public WindowBase(IntPtr Handle, IEventMan manager = null) :
            base(sfWindowBase_createFromHandle(Handle))
        {
            if (manager == null)
            {
                manager = new SubscribeManager() { Parent = this };
            }
            SfmlEventManager = manager;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Tell whether or not the window is opened (ie. has been created).
        /// Note that a hidden window (Show(false))
        /// will still return true
        /// </summary>
        /// <returns>True if the window is opened</returns>
        ////////////////////////////////////////////////////////////
        public virtual bool IsOpen
        {
            get { return sfWindowBase_isOpen(CPointer); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Close (destroy) the window.
        /// The Window instance remains valid and you can call
        /// Create to recreate the window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public virtual void Close()
        {
            sfWindowBase_close(CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Position of the window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public virtual Vector2i Position
        {
            get { return sfWindowBase_getPosition(CPointer); }
            set { sfWindowBase_setPosition(CPointer, value); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Size of the rendering region of the window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public virtual Vector2u Size
        {
            get { return sfWindowBase_getSize(CPointer); }
            set { sfWindowBase_setSize(CPointer, value); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Change the title of the window
        /// </summary>
        /// <param name="title">New title</param>
        ////////////////////////////////////////////////////////////
        public virtual void SetTitle(string title)
        {
            // Copy the title to a null-terminated UTF-32 byte array
            var titleAsUtf32 = Encoding.UTF32.GetBytes(title + '\0');

            unsafe
            {
                fixed (byte* titlePtr = titleAsUtf32)
                {
                    sfWindowBase_setUnicodeTitle(CPointer, (IntPtr)titlePtr);
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
        public virtual void SetIcon(uint width, uint height, byte[] pixels)
        {
            unsafe
            {
                fixed (byte* PixelsPtr = pixels)
                {
                    sfWindowBase_setIcon(CPointer, width, height, PixelsPtr);
                }
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Show or hide the window
        /// </summary>
        /// <param name="visible">True to show the window, false to hide it</param>
        ////////////////////////////////////////////////////////////
        public virtual void SetVisible(bool visible)
        {
            sfWindowBase_setVisible(CPointer, visible);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Show or hide the mouse cursor
        /// </summary>
        /// <param name="show">True to show, false to hide</param>
        ////////////////////////////////////////////////////////////
        public virtual void SetMouseCursorVisible(bool show)
        {
            sfWindowBase_setMouseCursorVisible(CPointer, show);
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
        public virtual void SetMouseCursorGrabbed(bool grabbed)
        {
            sfWindowBase_setMouseCursorGrabbed(CPointer, grabbed);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Set the displayed cursor to a native system cursor
        ///
        /// Upon window creation, the arrow cursor is used by default.
        /// </summary>
        /// <param name="cursor">Native system cursor type to display</param>
        ////////////////////////////////////////////////////////////
        public virtual void SetMouseCursor(Cursor cursor)
        {
            sfWindowBase_setMouseCursor(CPointer, cursor.CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Enable or disable automatic key-repeat.
        ///
        /// If key repeat is enabled, you will receive repeated
        /// <see cref="KeyPressed"/> events while keeping a key pressed. If it is
        /// disabled, you will only get a single event when the key is pressed.
        ///
        /// Automatic key-repeat is enabled by default
        /// </summary>
        /// <param name="enable">True to enable, false to disable</param>
        ////////////////////////////////////////////////////////////
        public virtual void SetKeyRepeatEnabled(bool enable)
        {
            sfWindowBase_setKeyRepeatEnabled(CPointer, enable);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Change the joystick threshold, ie. the value below which
        /// no move event will be generated
        /// </summary>
        /// <param name="threshold">New threshold, in range [0, 100]</param>
        ////////////////////////////////////////////////////////////
        public virtual void SetJoystickThreshold(float threshold)
        {
            sfWindowBase_setJoystickThreshold(CPointer, threshold);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// OS-specific handle of the window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public virtual IntPtr SystemHandle
        {
            get { return sfWindowBase_getSystemHandle(CPointer); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Wait for a new event and dispatch it to the corresponding
        /// event handler
        /// </summary>
        ////////////////////////////////////////////////////////////
        public void WaitAndDispatchEvents()
        {
            SfmlEventManager.PrepareFrame();
            if (WaitEvent(out Event e))
            {
                SfmlEventManager.HandleEvent(e);
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Call the event handlers for each pending event
        /// </summary>
        ////////////////////////////////////////////////////////////
        public void DispatchEvents()
        {
            SfmlEventManager.PrepareFrame();
            while (PollEvent(out Event e))
            {
                SfmlEventManager.HandleEvent(e);
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Request the current window to be made the active
        /// foreground window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public virtual void RequestFocus()
        {
            sfWindowBase_requestFocus(CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Check whether the window has the input focus
        /// </summary>
        /// <returns>True if the window has focus, false otherwise</returns>
        ////////////////////////////////////////////////////////////
        public virtual bool HasFocus()
        {
            return sfWindowBase_hasFocus(CPointer);
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
        public virtual bool CreateVulkanSurface(IntPtr vkInstance, out IntPtr vkSurface, IntPtr vkAllocator)
        {
            return sfWindowBase_createVulkanSurface(CPointer, vkInstance, out vkSurface, vkAllocator);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString()
        {
            return "[WindowBase]" +
                   " Size(" + Size + ")" +
                   " Position(" + Position + ")";
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor for derived classes
        /// </summary>
        /// <param name="manager">A custom event manager. By default, a SubscribeManager object is created</param>
        /// <param name="cPointer">Pointer to the internal object in the C API</param>
        ////////////////////////////////////////////////////////////
        protected WindowBase(IEventMan manager, IntPtr cPointer) :
            base(cPointer)
        {
            if (manager == null)
            {
                manager = new SubscribeManager() { Parent = this };
            }
            SfmlEventManager = manager;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Internal function to get the next event (non-blocking)
        /// </summary>
        /// <param name="eventToFill">Variable to fill with the raw pointer to the event structure</param>
        /// <returns>True if there was an event, false otherwise</returns>
        ////////////////////////////////////////////////////////////
        protected virtual bool PollEvent(out Event eventToFill)
        {
            return sfWindowBase_pollEvent(CPointer, out eventToFill);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Internal function to get the next event (blocking)
        /// </summary>
        /// <param name="eventToFill">Variable to fill with the raw pointer to the event structure</param>
        /// <returns>False if any error occured</returns>
        ////////////////////////////////////////////////////////////
        protected virtual bool WaitEvent(out Event eventToFill)
        {
            return sfWindowBase_waitEvent(CPointer, out eventToFill);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Internal function to get the mouse position relative to the window.
        /// This function is protected because it is called by another class of
        /// another module, it is not meant to be called by users.
        /// </summary>
        /// <returns>Relative mouse position</returns>
        ////////////////////////////////////////////////////////////
        protected internal virtual Vector2i InternalGetMousePosition()
        {
            return sfMouse_getPositionWindowBase(CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Internal function to set the mouse position relative to the window.
        /// This function is protected because it is called by another class of
        /// another module, it is not meant to be called by users.
        /// </summary>
        /// <param name="position">Relative mouse position</param>
        ////////////////////////////////////////////////////////////
        protected internal virtual void InternalSetMousePosition(Vector2i position)
        {
            sfMouse_setPositionWindowBase(position, CPointer);
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
        protected internal virtual Vector2i InternalGetTouchPosition(uint Finger)
        {
            return sfTouch_getPositionWindowBase(Finger, CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
        ////////////////////////////////////////////////////////////
        protected override void Destroy(bool disposing)
        {
            sfWindowBase_destroy(CPointer);
        }
        #region Imports
        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfWindowBase_create(VideoMode Mode, string Title, Styles Style);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfWindowBase_createUnicode(VideoMode Mode, IntPtr Title, Styles Style);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfWindowBase_createFromHandle(IntPtr Handle);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_destroy(IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_close(IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfWindowBase_isOpen(IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfWindowBase_pollEvent(IntPtr CPointer, out Event Evt);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfWindowBase_waitEvent(IntPtr CPointer, out Event Evt);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2i sfWindowBase_getPosition(IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_setPosition(IntPtr CPointer, Vector2i position);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2u sfWindowBase_getSize(IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_setSize(IntPtr CPointer, Vector2u size);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_setTitle(IntPtr CPointer, string title);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_setUnicodeTitle(IntPtr CPointer, IntPtr title);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private unsafe static extern void sfWindowBase_setIcon(IntPtr CPointer, uint Width, uint Height, byte* Pixels);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_setVisible(IntPtr CPointer, bool visible);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_setMouseCursorVisible(IntPtr CPointer, bool Show);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_setMouseCursorGrabbed(IntPtr CPointer, bool grabbed);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_setMouseCursor(IntPtr CPointer, IntPtr cursor);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_setKeyRepeatEnabled(IntPtr CPointer, bool Enable);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_setJoystickThreshold(IntPtr CPointer, float Threshold);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_requestFocus(IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfWindowBase_hasFocus(IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfWindowBase_getSystemHandle(IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfWindowBase_createVulkanSurface(IntPtr CPointer, IntPtr vkInstance, out IntPtr surface, IntPtr vkAllocator);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2i sfMouse_getPositionWindowBase(IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfMouse_setPositionWindowBase(Vector2i position, IntPtr CPointer);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2i sfTouch_getPositionWindowBase(uint Finger, IntPtr RelativeTo);
        #endregion
    }
}
