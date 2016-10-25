using System;
using System.Runtime.InteropServices;

namespace SFML.Window
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Enumeration of the different types of events
    /// </summary>
    ////////////////////////////////////////////////////////////
    public enum EventType
    {
        /// <summary>Event triggered when a window is manually closed</summary>
        Closed,

        /// <summary>Event triggered when a window is resized</summary>
        Resized,

        /// <summary>Event triggered when a window loses the focus</summary>
        LostFocus,

        /// <summary>Event triggered when a window gains the focus</summary>
        GainedFocus,

        /// <summary>Event triggered when a valid character is entered</summary>
        TextEntered,

        /// <summary>Event triggered when a keyboard key is pressed</summary>
        KeyPressed,

        /// <summary>Event triggered when a keyboard key is released</summary>
        KeyReleased,

        /// <summary>Event triggered when the mouse wheel is scrolled (deprecated)</summary>
        [Obsolete("MouseWheelMoved is deprecated, please use MouseWheelScrolled instead")]
        MouseWheelMoved,

        /// <summary>Event triggered when a mouse wheel is scrolled</summary>
        MouseWheelScrolled,

        /// <summary>Event triggered when a mouse button is pressed</summary>
        MouseButtonPressed,

        /// <summary>Event triggered when a mouse button is released</summary>
        MouseButtonReleased,

        /// <summary>Event triggered when the mouse moves within the area of a window</summary>
        MouseMoved,

        /// <summary>Event triggered when the mouse enters the area of a window</summary>
        MouseEntered,

        /// <summary>Event triggered when the mouse leaves the area of a window</summary>
        MouseLeft,

        /// <summary>Event triggered when a joystick button is pressed</summary>
        JoystickButtonPressed,

        /// <summary>Event triggered when a joystick button is released</summary>
        JoystickButtonReleased,

        /// <summary>Event triggered when a joystick axis moves</summary>
        JoystickMoved,

        /// <summary>Event triggered when a joystick is connected</summary>
        JoystickConnected,

        /// <summary>Event triggered when a joystick is disconnected</summary>
        JoystickDisconnected,

        /// <summary>Event triggered when a touch begins</summary>
        TouchBegan,

        /// <summary>Event triggered when a touch is moved</summary>
        TouchMoved,

        /// <summary>Event triggered when a touch is ended</summary>
        TouchEnded,

        /// <summary>Event triggered when a sensor is changed</summary>
        SensorChanged
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Keyboard event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyEvent
    {
        /// <summary>Code of the key (see KeyCode enum)</summary>
        public Keyboard.Key Code;

        /// <summary>Is the Alt modifier pressed?</summary>
        public int Alt;

        /// <summary>Is the Control modifier pressed?</summary>
        public int Control;

        /// <summary>Is the Shift modifier pressed?</summary>
        public int Shift;

        /// <summary>Is the System modifier pressed?</summary>
        public int System;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Text event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct TextEvent
    {
        /// <summary>UTF-32 value of the character</summary>
        public uint Unicode;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Mouse move event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseMoveEvent
    {
        /// <summary>X coordinate of the mouse cursor</summary>
        public int X;

        /// <summary>Y coordinate of the mouse cursor</summary>
        public int Y;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Mouse buttons event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseButtonEvent
    {
        /// <summary>Code of the button (see MouseButton enum)</summary>
        public Mouse.Button Button;

        /// <summary>X coordinate of the mouse cursor</summary>
        public int X;

        /// <summary>Y coordinate of the mouse cursor</summary>
        public int Y;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Mouse wheel move event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    [Obsolete("MouseWheelEvent is deprecated, please use MouseWheelScrollEvent instead")]
    public struct MouseWheelEvent
    {
        /// <summary>Scroll amount</summary>
        public int Delta;

        /// <summary>X coordinate of the mouse cursor</summary>
        public int X;

        /// <summary>Y coordinate of the mouse cursor</summary>
        public int Y;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Mouse wheel scroll event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseWheelScrollEvent
    {
        /// <summary>Mouse Wheel which triggered the event</summary>
        public Mouse.Wheel Wheel;

        /// <summary>Scroll amount</summary>
        public float Delta;

        /// <summary>X coordinate of the mouse cursor</summary>
        public int X;

        /// <summary>Y coordinate of the mouse cursor</summary>
        public int Y;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Joystick axis move event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct JoystickMoveEvent
    {
        /// <summary>Index of the joystick which triggered the event</summary>
        public uint JoystickId;

        /// <summary>Joystick axis (see JoyAxis enum)</summary>
        public Joystick.Axis Axis;

        /// <summary>Current position of the axis</summary>
        public float Position;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Joystick buttons event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct JoystickButtonEvent
    {
        /// <summary>Index of the joystick which triggered the event</summary>
        public uint JoystickId;

        /// <summary>Index of the button</summary>
        public uint Button;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Joystick connect event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct JoystickConnectEvent
    {
        /// <summary>Index of the joystick which triggered the event</summary>
        public uint JoystickId;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Size event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct SizeEvent
    {
        /// <summary>New width of the window</summary>
        public uint Width;

        /// <summary>New height of the window</summary>
        public uint Height;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Touch event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct TouchEvent
    {
        /// <summary>Index of the finger in case of multi-touch events</summary>
        public uint Finger;

        /// <summary>X position of the touch, relative to the left of the owner window</summary>
        public int X;

        /// <summary>Y position of the touch, relative to the top of the owner window</summary>
        public int Y;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Sensor event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct SensorEvent
    {
        /// <summary>Type of the sensor</summary>
        public Sensor.Type Type;

        /// <summary>Current value of the sensor on X axis</summary>
        public float X;

        /// <summary>Current value of the sensor on Y axis</summary>
        public float Y;

        /// <summary>Current value of the sensor on Z axis</summary>
        public float Z;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Event defines a system event and its parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Explicit, Size = 20)]
    public struct Event
    {
        /// <summary>Type of event (see EventType enum)</summary>
        [FieldOffset(0)]
        public EventType Type;

        /// <summary>Arguments for size events (Resized)</summary>
        [FieldOffset(4)]
        public SizeEvent Size;

        /// <summary>Arguments for key events (KeyPressed, KeyReleased)</summary>
        [FieldOffset(4)]
        public KeyEvent Key;

        /// <summary>Arguments for text events (TextEntered)</summary>
        [FieldOffset(4)]
        public TextEvent Text;

        /// <summary>Arguments for mouse move events (MouseMoved)</summary>
        [FieldOffset(4)]
        public MouseMoveEvent MouseMove;

        /// <summary>Arguments for mouse button events (MouseButtonPressed, MouseButtonReleased)</summary>
        [FieldOffset(4)]
        public MouseButtonEvent MouseButton;

        /// <summary>Arguments for mouse wheel events (MouseWheelMoved)</summary>
        [FieldOffset(4)]
        [Obsolete("MouseWheel is deprecated, please use MouseWheelScroll instead")]
        public MouseWheelEvent MouseWheel;

        /// <summary>Arguments for mouse wheel scroll events (MouseWheelScrolled)</summary>
        [FieldOffset(4)]
        public MouseWheelScrollEvent MouseWheelScroll;

        /// <summary>Arguments for joystick axis events (JoystickMoved)</summary>
        [FieldOffset(4)]
        public JoystickMoveEvent JoystickMove;

        /// <summary>Arguments for joystick button events (JoystickButtonPressed, JoystickButtonReleased)</summary>
        [FieldOffset(4)]
        public JoystickButtonEvent JoystickButton;

        /// <summary>Arguments for joystick connect events (JoystickConnected, JoystickDisconnected)</summary>
        [FieldOffset(4)]
        public JoystickConnectEvent JoystickConnect;

        /// <summary>Arguments for touch events (TouchBegan, TouchMoved, TouchEnded)</summary>
        [FieldOffset(4)]
        public TouchEvent Touch;

        /// <summary>Arguments for sensor events (SensorChanged)</summary>
        [FieldOffset(4)]
        public SensorEvent Sensor;
    }
}
