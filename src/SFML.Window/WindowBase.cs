using System;
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

        /// <summary>Default window style (titlebar + resize + close)</summary>
        Default = Titlebar | Resize | Close
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Enumeration of window states
    /// </summary>
    ////////////////////////////////////////////////////////////
    public enum State
    {
        /// <summary>Floating window</summary>
        Windowed,

        /// <summary>Fullscreen window</summary>
        Fullscreen
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
        /// Create the window with default style and creation settings
        /// </summary>
        /// <param name="mode">Video mode to use</param>
        /// <param name="title">Title of the window</param>
        ////////////////////////////////////////////////////////////
        public WindowBase(VideoMode mode, string title) :
            this(mode, title, Styles.Default, State.Windowed)
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
        public WindowBase(VideoMode mode, string title, Styles style, State state) :
            base(IntPtr.Zero)
        {
            // Copy the title to a null-terminated UTF-32 byte array
            var titleAsUtf32 = Encoding.UTF32.GetBytes(title + '\0');

            unsafe
            {
                fixed (byte* titlePtr = titleAsUtf32)
                {
                    CPointer = sfWindowBase_createUnicode(mode, (IntPtr)titlePtr, style, state);
                }
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Create the window from an existing control
        /// </summary>
        /// <param name="handle">Platform-specific handle of the control</param>
        ////////////////////////////////////////////////////////////
        public WindowBase(IntPtr handle) :
            base(sfWindowBase_createFromHandle(handle))
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
        public virtual bool IsOpen => sfWindowBase_isOpen(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Close (destroy) the window.
        /// The Window instance remains valid and you can call
        /// Create to recreate the window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public virtual void Close() => sfWindowBase_close(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Position of the window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public virtual Vector2i Position
        {
            get => sfWindowBase_getPosition(CPointer);
            set => sfWindowBase_setPosition(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Size of the rendering region of the window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public virtual Vector2u Size
        {
            get => sfWindowBase_getSize(CPointer);
            set => sfWindowBase_setSize(CPointer, value);
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
        /// <param name="size">Icon's width and height, in pixels</param>
        /// <param name="pixels">Array of pixels, format must be RGBA 32 bits</param>
        ////////////////////////////////////////////////////////////
        public virtual void SetIcon(Vector2u size, byte[] pixels)
        {
            unsafe
            {
                fixed (byte* pixelsPtr = pixels)
                {
                    sfWindowBase_setIcon(CPointer, size, pixelsPtr);
                }
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Show or hide the window
        /// </summary>
        /// <param name="visible">True to show the window, false to hide it</param>
        ////////////////////////////////////////////////////////////
        public virtual void SetVisible(bool visible) => sfWindowBase_setVisible(CPointer, visible);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Show or hide the mouse cursor
        /// </summary>
        /// <param name="show">True to show, false to hide</param>
        ////////////////////////////////////////////////////////////
        public virtual void SetMouseCursorVisible(bool show) => sfWindowBase_setMouseCursorVisible(CPointer, show);

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
        public virtual void SetMouseCursorGrabbed(bool grabbed) => sfWindowBase_setMouseCursorGrabbed(CPointer, grabbed);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Set the displayed cursor to a native system cursor
        ///
        /// Upon window creation, the arrow cursor is used by default.
        /// </summary>
        /// <param name="cursor">Native system cursor type to display</param>
        ////////////////////////////////////////////////////////////
        public virtual void SetMouseCursor(Cursor cursor) => sfWindowBase_setMouseCursor(CPointer, cursor.CPointer);

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
        public virtual void SetKeyRepeatEnabled(bool enable) => sfWindowBase_setKeyRepeatEnabled(CPointer, enable);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Change the joystick threshold, ie. the value below which
        /// no move event will be generated
        /// </summary>
        /// <param name="threshold">New threshold, in range [0, 100]</param>
        ////////////////////////////////////////////////////////////
        public virtual void SetJoystickThreshold(float threshold) => sfWindowBase_setJoystickThreshold(CPointer, threshold);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// OS-specific handle of the window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public virtual IntPtr NativeHandle => sfWindowBase_getNativeHandle(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Wait for a new event and dispatch it to the corresponding
        /// event handler
        /// </summary>
        ////////////////////////////////////////////////////////////
        public void WaitAndDispatchEvents() => WaitAndDispatchEvents(Time.Zero);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Wait for a new event and dispatch it to the corresponding
        /// event handler
        /// </summary>
        /// <param name="timeout">Maximum time to wait (<see cref="Time.Zero"/> for infinite)</param>
        ////////////////////////////////////////////////////////////
        public void WaitAndDispatchEvents(Time timeout)
        {
            if (WaitEvent(timeout, out var e))
            {
                CallEventHandler(e);
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Call the event handlers for each pending event
        /// </summary>
        ////////////////////////////////////////////////////////////
        public void DispatchEvents()
        {
            while (PollEvent(out var e))
            {
                CallEventHandler(e);
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Request the current window to be made the active
        /// foreground window
        /// </summary>
        ////////////////////////////////////////////////////////////
        public virtual void RequestFocus() => sfWindowBase_requestFocus(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Check whether the window has the input focus
        /// </summary>
        /// <returns>True if the window has focus, false otherwise</returns>
        ////////////////////////////////////////////////////////////
        public virtual bool HasFocus() => sfWindowBase_hasFocus(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Create a Vulkan rendering surface
        /// </summary>
        /// <param name="vkInstance">Vulkan instance</param>
        /// <param name="vkSurface">Created surface</param>
        /// <param name="vkAllocator">Allocator to use</param>
        /// <returns>True if surface creation was successful, false otherwise</returns>
        ////////////////////////////////////////////////////////////
        public virtual bool CreateVulkanSurface(IntPtr vkInstance, out IntPtr vkSurface, IntPtr vkAllocator) => sfWindowBase_createVulkanSurface(CPointer, vkInstance, out vkSurface, vkAllocator);

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

            return "[WindowBase]" +
                   " Size(" + Size + ")" +
                   " Position(" + Position + ")";
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor for derived classes
        /// </summary>
        /// <param name="cPointer">Pointer to the internal object in the C API</param>
        /// <param name="dummy">Internal hack :)</param>
        ////////////////////////////////////////////////////////////
        protected WindowBase(IntPtr cPointer, int dummy) :
            base(cPointer)
        {
            // TODO : find a cleaner way of separating this constructor from WindowBase(IntPtr handle)
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Internal function to get the next event (non-blocking)
        /// </summary>
        /// <param name="eventToFill">Variable to fill with the raw pointer to the event structure</param>
        /// <returns>True if there was an event, false otherwise</returns>
        ////////////////////////////////////////////////////////////
        protected virtual bool PollEvent(out Event eventToFill) => sfWindowBase_pollEvent(CPointer, out eventToFill);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Internal function to get the next event (blocking)
        /// </summary>
        /// <param name="timeout">Maximum time to wait (<see cref="Time.Zero"/> for infinite)</param>
        /// <param name="eventToFill">Variable to fill with the raw pointer to the event structure</param>
        /// <returns>False if any error occurred</returns>
        ////////////////////////////////////////////////////////////
        protected virtual bool WaitEvent(Time timeout, out Event eventToFill) => sfWindowBase_waitEvent(CPointer, timeout, out eventToFill);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Internal function to get the mouse position relative to the window.
        /// This function is protected because it is called by another class of
        /// another module, it is not meant to be called by users.
        /// </summary>
        /// <returns>Relative mouse position</returns>
        ////////////////////////////////////////////////////////////
        protected internal virtual Vector2i InternalGetMousePosition() => sfMouse_getPositionWindowBase(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Internal function to set the mouse position relative to the window.
        /// This function is protected because it is called by another class of
        /// another module, it is not meant to be called by users.
        /// </summary>
        /// <param name="position">Relative mouse position</param>
        ////////////////////////////////////////////////////////////
        protected internal virtual void InternalSetMousePosition(Vector2i position) => sfMouse_setPositionWindowBase(position, CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Internal function to get the touch position relative to the window.
        /// This function is protected because it is called by another class of
        /// another module, it is not meant to be called by users.
        /// </summary>
        /// <param name="finger">Finger index</param>
        /// <returns>Relative touch position</returns>
        ////////////////////////////////////////////////////////////
        protected internal virtual Vector2i InternalGetTouchPosition(uint finger) => sfTouch_getPositionWindowBase(finger, CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
        ////////////////////////////////////////////////////////////
        protected override void Destroy(bool disposing) => sfWindowBase_destroy(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Call the event handler for the given event
        /// </summary>
        /// <param name="e">Event to dispatch</param>
        ////////////////////////////////////////////////////////////
        private void CallEventHandler(Event e)
        {
            switch (e.Type)
            {
                case EventType.Closed:
                    Closed?.Invoke(this, EventArgs.Empty);
                    break;

                case EventType.GainedFocus:
                    GainedFocus?.Invoke(this, EventArgs.Empty);
                    break;

                case EventType.JoystickButtonPressed:
                    JoystickButtonPressed?.Invoke(this, new JoystickButtonEventArgs(e.JoystickButton));
                    break;

                case EventType.JoystickButtonReleased:
                    JoystickButtonReleased?.Invoke(this, new JoystickButtonEventArgs(e.JoystickButton));
                    break;

                case EventType.JoystickMoved:
                    JoystickMoved?.Invoke(this, new JoystickMoveEventArgs(e.JoystickMove));
                    break;

                case EventType.JoystickConnected:
                    JoystickConnected?.Invoke(this, new JoystickConnectEventArgs(e.JoystickConnect));
                    break;

                case EventType.JoystickDisconnected:
                    JoystickDisconnected?.Invoke(this, new JoystickConnectEventArgs(e.JoystickConnect));
                    break;

                case EventType.KeyPressed:
                    KeyPressed?.Invoke(this, new KeyEventArgs(e.Key));
                    break;

                case EventType.KeyReleased:
                    KeyReleased?.Invoke(this, new KeyEventArgs(e.Key));
                    break;

                case EventType.LostFocus:
                    LostFocus?.Invoke(this, EventArgs.Empty);
                    break;

                case EventType.MouseButtonPressed:
                    MouseButtonPressed?.Invoke(this, new MouseButtonEventArgs(e.MouseButton));
                    break;

                case EventType.MouseButtonReleased:
                    MouseButtonReleased?.Invoke(this, new MouseButtonEventArgs(e.MouseButton));
                    break;

                case EventType.MouseEntered:
                    MouseEntered?.Invoke(this, EventArgs.Empty);
                    break;

                case EventType.MouseLeft:
                    MouseLeft?.Invoke(this, EventArgs.Empty);
                    break;

                case EventType.MouseMoved:
                    MouseMoved?.Invoke(this, new MouseMoveEventArgs(e.MouseMove));
                    break;

                case EventType.MouseMovedRaw:
                    MouseMovedRaw?.Invoke(this, new MouseMoveRawEventArgs(e.MouseMoveRaw));
                    break;

                case EventType.MouseWheelScrolled:
                    MouseWheelScrolled?.Invoke(this, new MouseWheelScrollEventArgs(e.MouseWheelScroll));
                    break;

                case EventType.Resized:
                    Resized?.Invoke(this, new SizeEventArgs(e.Size));
                    break;

                case EventType.TextEntered:
                    TextEntered?.Invoke(this, new TextEventArgs(e.Text));
                    break;

                case EventType.TouchBegan:
                    TouchBegan?.Invoke(this, new TouchEventArgs(e.Touch));
                    break;

                case EventType.TouchMoved:
                    TouchMoved?.Invoke(this, new TouchEventArgs(e.Touch));
                    break;

                case EventType.TouchEnded:
                    TouchEnded?.Invoke(this, new TouchEventArgs(e.Touch));
                    break;

                case EventType.SensorChanged:
                    SensorChanged?.Invoke(this, new SensorEventArgs(e.Sensor));
                    break;

                default:
                    break;
            }
        }

        /// <summary>Event handler for the Closed event</summary>
        public event EventHandler Closed;

        /// <summary>Event handler for the Resized event</summary>
        public event EventHandler<SizeEventArgs> Resized;

        /// <summary>Event handler for the LostFocus event</summary>
        public event EventHandler LostFocus;

        /// <summary>Event handler for the GainedFocus event</summary>
        public event EventHandler GainedFocus;

        /// <summary>Event handler for the TextEntered event</summary>
        public event EventHandler<TextEventArgs> TextEntered;

        /// <summary>Event handler for the KeyPressed event</summary>
        public event EventHandler<KeyEventArgs> KeyPressed;

        /// <summary>Event handler for the KeyReleased event</summary>
        public event EventHandler<KeyEventArgs> KeyReleased;

        /// <summary>Event handler for the MouseWheelScrolled event</summary>
        public event EventHandler<MouseWheelScrollEventArgs> MouseWheelScrolled;

        /// <summary>Event handler for the MouseButtonPressed event</summary>
        public event EventHandler<MouseButtonEventArgs> MouseButtonPressed;

        /// <summary>Event handler for the MouseButtonReleased event</summary>
        public event EventHandler<MouseButtonEventArgs> MouseButtonReleased;

        /// <summary>Event handler for the MouseMoved event</summary>
        public event EventHandler<MouseMoveEventArgs> MouseMoved;

        /// <summary>Event handler for the MouseMovedRaw event</summary>
        public event EventHandler<MouseMoveRawEventArgs> MouseMovedRaw;

        /// <summary>Event handler for the MouseEntered event</summary>
        public event EventHandler MouseEntered;

        /// <summary>Event handler for the MouseLeft event</summary>
        public event EventHandler MouseLeft;

        /// <summary>Event handler for the JoystickButtonPressed event</summary>
        public event EventHandler<JoystickButtonEventArgs> JoystickButtonPressed;

        /// <summary>Event handler for the JoystickButtonReleased event</summary>
        public event EventHandler<JoystickButtonEventArgs> JoystickButtonReleased;

        /// <summary>Event handler for the JoystickMoved event</summary>
        public event EventHandler<JoystickMoveEventArgs> JoystickMoved;

        /// <summary>Event handler for the JoystickConnected event</summary>
        public event EventHandler<JoystickConnectEventArgs> JoystickConnected;

        /// <summary>Event handler for the JoystickDisconnected event</summary>
        public event EventHandler<JoystickConnectEventArgs> JoystickDisconnected;

        /// <summary>Event handler for the TouchBegan event</summary>
        public event EventHandler<TouchEventArgs> TouchBegan;

        /// <summary>Event handler for the TouchMoved event</summary>
        public event EventHandler<TouchEventArgs> TouchMoved;

        /// <summary>Event handler for the TouchEnded event</summary>
        public event EventHandler<TouchEventArgs> TouchEnded;

        /// <summary>Event handler for the SensorChanged event</summary>
        public event EventHandler<SensorEventArgs> SensorChanged;

        #region Imports
        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfWindowBase_createUnicode(VideoMode mode, IntPtr title, Styles style, State state);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfWindowBase_createFromHandle(IntPtr handle);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_destroy(IntPtr cPointer);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_close(IntPtr cPointer);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfWindowBase_isOpen(IntPtr cPointer);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfWindowBase_pollEvent(IntPtr cPointer, out Event evt);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfWindowBase_waitEvent(IntPtr cPointer, Time timeout, out Event evt);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2i sfWindowBase_getPosition(IntPtr cPointer);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_setPosition(IntPtr cPointer, Vector2i position);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2u sfWindowBase_getSize(IntPtr cPointer);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_setSize(IntPtr cPointer, Vector2u size);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_setUnicodeTitle(IntPtr cPointer, IntPtr title);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern unsafe void sfWindowBase_setIcon(IntPtr cPointer, Vector2u size, byte* pixels);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_setVisible(IntPtr cPointer, bool visible);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_setMouseCursorVisible(IntPtr cPointer, bool show);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_setMouseCursorGrabbed(IntPtr cPointer, bool grabbed);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_setMouseCursor(IntPtr cPointer, IntPtr cursor);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_setKeyRepeatEnabled(IntPtr cPointer, bool enable);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_setJoystickThreshold(IntPtr cPointer, float threshold);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfWindowBase_requestFocus(IntPtr cPointer);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfWindowBase_hasFocus(IntPtr cPointer);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfWindowBase_getNativeHandle(IntPtr cPointer);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfWindowBase_createVulkanSurface(IntPtr cPointer, IntPtr vkInstance, out IntPtr surface, IntPtr vkAllocator);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2i sfMouse_getPositionWindowBase(IntPtr cPointer);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfMouse_setPositionWindowBase(Vector2i position, IntPtr cPointer);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2i sfTouch_getPositionWindowBase(uint finger, IntPtr relativeTo);
        #endregion
    }
}
