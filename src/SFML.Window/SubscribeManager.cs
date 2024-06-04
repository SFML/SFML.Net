using System;

namespace SFML.Window
{
    /// <summary>
    /// A simple Event manager that uses C# events to invoke
    /// the required event
    /// </summary>
    public class SubscribeManager : IEventMan
    {
        public WindowBase Parent { get; set; }

        ///<inheritdoc/>
        public void PrepareFrame() { } // Nothing to do
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Call the event handler for the given event
        /// </summary>
        /// <param name="e">Event to dispatch</param>
        ////////////////////////////////////////////////////////////
        public void HandleEvent(Event e)
        {
            switch (e.Type)
            {
                case EventType.Closed:
                    Closed?.Invoke(Parent, EventArgs.Empty);

                    break;

                case EventType.GainedFocus:
                    GainedFocus?.Invoke(Parent, EventArgs.Empty);

                    break;

                case EventType.JoystickButtonPressed:
                    JoystickButtonPressed?.Invoke(Parent, new JoystickButtonEventArgs(e.JoystickButton));

                    break;

                case EventType.JoystickButtonReleased:
                    JoystickButtonReleased?.Invoke(Parent, new JoystickButtonEventArgs(e.JoystickButton));

                    break;

                case EventType.JoystickMoved:
                    JoystickMoved?.Invoke(Parent, new JoystickMoveEventArgs(e.JoystickMove));

                    break;

                case EventType.JoystickConnected:
                    JoystickConnected?.Invoke(Parent, new JoystickConnectEventArgs(e.JoystickConnect));

                    break;

                case EventType.JoystickDisconnected:
                    JoystickDisconnected?.Invoke(Parent, new JoystickConnectEventArgs(e.JoystickConnect));

                    break;

                case EventType.KeyPressed:
                    KeyPressed?.Invoke(Parent, new KeyEventArgs(e.Key));

                    break;

                case EventType.KeyReleased:
                    KeyReleased?.Invoke(Parent, new KeyEventArgs(e.Key));

                    break;

                case EventType.LostFocus:
                    LostFocus?.Invoke(Parent, EventArgs.Empty);

                    break;

                case EventType.MouseButtonPressed:
                    MouseButtonPressed?.Invoke(Parent, new MouseButtonEventArgs(e.MouseButton));

                    break;

                case EventType.MouseButtonReleased:
                    MouseButtonReleased?.Invoke(Parent, new MouseButtonEventArgs(e.MouseButton));

                    break;

                case EventType.MouseEntered:
                    MouseEntered?.Invoke(Parent, EventArgs.Empty);

                    break;

                case EventType.MouseLeft:
                    MouseLeft?.Invoke(Parent, EventArgs.Empty);

                    break;

                case EventType.MouseMoved:
                    MouseMoved?.Invoke(Parent, new MouseMoveEventArgs(e.MouseMove));

                    break;

                // Disable CS0618 (Obselete Warning).  This Event will be removed in SFML.NET 3.0, but should remain supported until then.
#pragma warning disable CS0618
                case EventType.MouseWheelMoved:
                    MouseWheelMoved?.Invoke(Parent, new MouseWheelEventArgs(e.MouseWheel));

                    break;
                // restore CS0618
#pragma warning restore CS0618

                case EventType.MouseWheelScrolled:
                    MouseWheelScrolled?.Invoke(Parent, new MouseWheelScrollEventArgs(e.MouseWheelScroll));

                    break;

                case EventType.Resized:
                    Resized?.Invoke(Parent, new SizeEventArgs(e.Size));

                    break;

                case EventType.TextEntered:
                    TextEntered?.Invoke(Parent, new TextEventArgs(e.Text));

                    break;

                case EventType.TouchBegan:
                    TouchBegan?.Invoke(Parent, new TouchEventArgs(e.Touch));

                    break;

                case EventType.TouchMoved:
                    TouchMoved?.Invoke(Parent, new TouchEventArgs(e.Touch));

                    break;

                case EventType.TouchEnded:
                    TouchEnded?.Invoke(Parent, new TouchEventArgs(e.Touch));

                    break;

                case EventType.SensorChanged:
                    SensorChanged?.Invoke(Parent, new SensorEventArgs(e.Sensor));

                    break;
                default:
                    break;
            }
        }

        /// <summary>Event handler for the Closed event</summary>
        public event EventHandler Closed = null;

        /// <summary>Event handler for the Resized event</summary>
        public event EventHandler<SizeEventArgs> Resized = null;

        /// <summary>Event handler for the LostFocus event</summary>
        public event EventHandler LostFocus = null;

        /// <summary>Event handler for the GainedFocus event</summary>
        public event EventHandler GainedFocus = null;

        /// <summary>Event handler for the TextEntered event</summary>
        public event EventHandler<TextEventArgs> TextEntered = null;

        /// <summary>Event handler for the KeyPressed event</summary>
        public event EventHandler<KeyEventArgs> KeyPressed = null;

        /// <summary>Event handler for the KeyReleased event</summary>
        public event EventHandler<KeyEventArgs> KeyReleased = null;

        /// <summary>Event handler for the MouseWheelMoved event</summary>
        [Obsolete("MouseWheelMoved is deprecated, please use MouseWheelScrolled instead")]
        public event EventHandler<MouseWheelEventArgs> MouseWheelMoved = null;

        /// <summary>Event handler for the MouseWheelScrolled event</summary>
        public event EventHandler<MouseWheelScrollEventArgs> MouseWheelScrolled = null;

        /// <summary>Event handler for the MouseButtonPressed event</summary>
        public event EventHandler<MouseButtonEventArgs> MouseButtonPressed = null;

        /// <summary>Event handler for the MouseButtonReleased event</summary>
        public event EventHandler<MouseButtonEventArgs> MouseButtonReleased = null;

        /// <summary>Event handler for the MouseMoved event</summary>
        public event EventHandler<MouseMoveEventArgs> MouseMoved = null;

        /// <summary>Event handler for the MouseEntered event</summary>
        public event EventHandler MouseEntered = null;

        /// <summary>Event handler for the MouseLeft event</summary>
        public event EventHandler MouseLeft = null;

        /// <summary>Event handler for the JoystickButtonPressed event</summary>
        public event EventHandler<JoystickButtonEventArgs> JoystickButtonPressed = null;

        /// <summary>Event handler for the JoystickButtonReleased event</summary>
        public event EventHandler<JoystickButtonEventArgs> JoystickButtonReleased = null;

        /// <summary>Event handler for the JoystickMoved event</summary>
        public event EventHandler<JoystickMoveEventArgs> JoystickMoved = null;

        /// <summary>Event handler for the JoystickConnected event</summary>
        public event EventHandler<JoystickConnectEventArgs> JoystickConnected = null;

        /// <summary>Event handler for the JoystickDisconnected event</summary>
        public event EventHandler<JoystickConnectEventArgs> JoystickDisconnected = null;

        /// <summary>Event handler for the TouchBegan event</summary>
        public event EventHandler<TouchEventArgs> TouchBegan = null;

        /// <summary>Event handler for the TouchMoved event</summary>
        public event EventHandler<TouchEventArgs> TouchMoved = null;

        /// <summary>Event handler for the TouchEnded event</summary>
        public event EventHandler<TouchEventArgs> TouchEnded = null;

        /// <summary>Event handler for the SensorChanged event</summary>
        public event EventHandler<SensorEventArgs> SensorChanged = null;
    }
}