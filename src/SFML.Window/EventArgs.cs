using System;
using SFML.System;

namespace SFML.Window
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Keyboard event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class KeyEventArgs : EventArgs
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the key arguments from a key event
        /// </summary>
        /// <param name="e">Key event</param>
        ////////////////////////////////////////////////////////////
        public KeyEventArgs(KeyEvent e)
        {
            Code = e.Code;
            Scancode = e.Scancode;
            Alt = e.Alt;
            Control = e.Control;
            Shift = e.Shift;
            System = e.System;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => "[KeyEventArgs]" +
                   " Code(" + Code + ")" +
                   " Scancode(" + Scancode + ")" +
                   " Alt(" + Alt + ")" +
                   " Control(" + Control + ")" +
                   " Shift(" + Shift + ")" +
                   " System(" + System + ")";

        /// <summary>Code of the key (see <see cref="Keyboard.Key"/>)</summary>
        public Keyboard.Key Code;

        /// <summary>Physical code of the key (see <see cref="Keyboard.Scancode"/>)</summary>
        public Keyboard.Scancode Scancode;

        /// <summary>Is the Alt modifier pressed?</summary>
        public bool Alt;

        /// <summary>Is the Control modifier pressed?</summary>
        public bool Control;

        /// <summary>Is the Shift modifier pressed?</summary>
        public bool Shift;

        /// <summary>Is the System modifier pressed?</summary>
        public bool System;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Text event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class TextEventArgs : EventArgs
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the text arguments from a text event
        /// </summary>
        /// <param name="e">Text event</param>
        ////////////////////////////////////////////////////////////
        public TextEventArgs(TextEvent e) => Unicode = char.ConvertFromUtf32((int)e.Unicode);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => "[TextEventArgs]" +
                   " Unicode(" + Unicode + ")";

        /// <summary>UTF-16 value of the character</summary>
        public string Unicode;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Mouse move event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class MouseMoveEventArgs : EventArgs
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the mouse move arguments from a mouse move event
        /// </summary>
        /// <param name="e">Mouse move event</param>
        ////////////////////////////////////////////////////////////
        public MouseMoveEventArgs(MouseMoveEvent e) => Position = e.Position;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => "[MouseMoveEventArgs]" +
                   $" Position({Position.X}, {Position.Y})";

        /// <summary>Coordinates of the mouse cursor</summary>
        public Vector2i Position;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Mouse move raw event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class MouseMoveRawEventArgs : EventArgs
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the mouse move raw arguments from a mouse move raw event
        /// </summary>
        /// <param name="e">Mouse move event</param>
        ////////////////////////////////////////////////////////////
        public MouseMoveRawEventArgs(MouseMoveRawEvent e) => Delta = e.Delta;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => "[MouseMoveRawEventArgs]" +
                   $" Delta({Delta.X}, {Delta.Y})";

        /// <summary>Delta movement of the mouse since last event</summary>
        public Vector2i Delta;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Mouse buttons event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class MouseButtonEventArgs : EventArgs
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the mouse button arguments from a mouse button event
        /// </summary>
        /// <param name="e">Mouse button event</param>
        ////////////////////////////////////////////////////////////
        public MouseButtonEventArgs(MouseButtonEvent e)
        {
            Button = e.Button;
            Position = e.Position;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => "[MouseButtonEventArgs]" +
                   $" Button({Button})" +
                   $" Position({Position.X}, {Position.Y})";

        /// <summary>Code of the button (see MouseButton enum)</summary>
        public Mouse.Button Button;

        /// <summary>Coordinates of the mouse cursor</summary>
        public Vector2i Position;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Mouse wheel scroll event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class MouseWheelScrollEventArgs : EventArgs
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the mouse wheel scroll arguments from a mouse wheel scroll event
        /// </summary>
        /// <param name="e">Mouse wheel scroll event</param>
        ////////////////////////////////////////////////////////////
        public MouseWheelScrollEventArgs(MouseWheelScrollEvent e)
        {
            Delta = e.Delta;
            Wheel = e.Wheel;
            Position = e.Position;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => "[MouseWheelScrollEventArgs]" +
                   $" Wheel({Wheel})" +
                   $" Delta({Delta})" +
                   $" Position(${Position.X}, {Position.Y})";

        /// <summary>Mouse Wheel which triggered the event</summary>
        public Mouse.Wheel Wheel;

        /// <summary>Scroll amount</summary>
        public float Delta;

        /// <summary>Coordinate of the mouse cursor</summary>
        public Vector2i Position;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Joystick axis move event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class JoystickMoveEventArgs : EventArgs
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the joystick move arguments from a joystick move event
        /// </summary>
        /// <param name="e">Joystick move event</param>
        ////////////////////////////////////////////////////////////
        public JoystickMoveEventArgs(JoystickMoveEvent e)
        {
            JoystickId = e.JoystickId;
            Axis = e.Axis;
            Position = e.Position;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => "[JoystickMoveEventArgs]" +
                   $" JoystickId({JoystickId})" +
                   $" Axis({Axis})" +
                   $" Position({Position})";

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
    public class JoystickButtonEventArgs : EventArgs
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the joystick button arguments from a joystick button event
        /// </summary>
        /// <param name="e">Joystick button event</param>
        ////////////////////////////////////////////////////////////
        public JoystickButtonEventArgs(JoystickButtonEvent e)
        {
            JoystickId = e.JoystickId;
            Button = e.Button;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => "[JoystickButtonEventArgs]" +
                   $" JoystickId({JoystickId})" +
                   $" Button({Button})";

        /// <summary>Index of the joystick which triggered the event</summary>
        public uint JoystickId;

        /// <summary>Index of the button</summary>
        public uint Button;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Joystick connection/disconnection event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class JoystickConnectEventArgs : EventArgs
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the joystick connect arguments from a joystick connect event
        /// </summary>
        /// <param name="e">Joystick button event</param>
        ////////////////////////////////////////////////////////////
        public JoystickConnectEventArgs(JoystickConnectEvent e) => JoystickId = e.JoystickId;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => "[JoystickConnectEventArgs]" +
                   $" JoystickId({JoystickId})";

        /// <summary>Index of the joystick which triggered the event</summary>
        public uint JoystickId;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Size event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class SizeEventArgs : EventArgs
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the size arguments from a size event
        /// </summary>
        /// <param name="e">Size event</param>
        ////////////////////////////////////////////////////////////
        public SizeEventArgs(SizeEvent e) => Size = e.Size;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => "[SizeEventArgs]" +
                   $" Size({Size.X}, {Size.Y})";

        /// <summary>New size of the window</summary>
        public Vector2u Size;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Touch event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class TouchEventArgs : EventArgs
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the touch arguments from a touch event
        /// </summary>
        /// <param name="e">Touch event</param>
        ////////////////////////////////////////////////////////////
        public TouchEventArgs(TouchEvent e)
        {
            Finger = e.Finger;
            Position = e.Position;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => "[TouchEventArgs]" +
                   $" Finger({Finger})" +
                   $" Position({Position.X}, {Position.Y})";

        /// <summary>Index of the finger in case of multi-touch events</summary>
        public uint Finger;

        /// <summary>Position of the touch, relative to the left of the owner window</summary>
        public Vector2i Position;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Sensor event parameters
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class SensorEventArgs : EventArgs
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the sensor arguments from a sensor event
        /// </summary>
        /// <param name="e">Sensor event</param>
        ////////////////////////////////////////////////////////////
        public SensorEventArgs(SensorEvent e)
        {
            Type = e.Type;
            Value = e.Value;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => "[SensorEventArgs]" +
                   $" Type({Type})" +
                   $" Value({Value.X}, {Value.Y}, {Value.Z})";

        /// <summary>Type of the sensor</summary>
        public Sensor.Type Type;

        /// <summary>Current value of the sensor on X, Y and Z axes</summary>
        public Vector3f Value;
    }
}
