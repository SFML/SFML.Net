using System;
using System.Runtime.InteropServices;
using System.Security;
using SFML.System;

namespace SFML.Window
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Give access to the real-time state of the keyboard
    /// </summary>
    ////////////////////////////////////////////////////////////
    public static class Keyboard
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Key codes
        /// </summary>
        ////////////////////////////////////////////////////////////
        public enum Key
        {
            /// <summary>Unhandled key</summary>
            Unknown = -1,
            /// <summary>The A key</summary>
            A = 0,
            /// <summary>The B key</summary>
            B,
            /// <summary>The C key</summary>
            C,
            /// <summary>The D key</summary>
            D,
            /// <summary>The E key</summary>
            E,
            /// <summary>The F key</summary>
            F,
            /// <summary>The G key</summary>
            G,
            /// <summary>The H key</summary>
            H,
            /// <summary>The I key</summary>
            I,
            /// <summary>The J key</summary>
            J,
            /// <summary>The K key</summary>
            K,
            /// <summary>The L key</summary>
            L,
            /// <summary>The M key</summary>
            M,
            /// <summary>The N key</summary>
            N,
            /// <summary>The O key</summary>
            O,
            /// <summary>The P key</summary>
            P,
            /// <summary>The Q key</summary>
            Q,
            /// <summary>The R key</summary>
            R,
            /// <summary>The S key</summary>
            S,
            /// <summary>The T key</summary>
            T,
            /// <summary>The U key</summary>
            U,
            /// <summary>The V key</summary>
            V,
            /// <summary>The W key</summary>
            W,
            /// <summary>The X key</summary>
            X,
            /// <summary>The Y key</summary>
            Y,
            /// <summary>The Z key</summary>
            Z,
            /// <summary>The 0 key</summary>
            Num0,
            /// <summary>The 1 key</summary>
            Num1,
            /// <summary>The 2 key</summary>
            Num2,
            /// <summary>The 3 key</summary>
            Num3,
            /// <summary>The 4 key</summary>
            Num4,
            /// <summary>The 5 key</summary>
            Num5,
            /// <summary>The 6 key</summary>
            Num6,
            /// <summary>The 7 key</summary>
            Num7,
            /// <summary>The 8 key</summary>
            Num8,
            /// <summary>The 9 key</summary>
            Num9,
            /// <summary>The Escape key</summary>
            Escape,
            /// <summary>The left Control key</summary>
            LControl,
            /// <summary>The left Shift key</summary>
            LShift,
            /// <summary>The left Alt key</summary>
            LAlt,
            /// <summary>The left OS specific key: window (Windows and Linux), apple (MacOS X), ...</summary>
            LSystem,
            /// <summary>The right Control key</summary>
            RControl,
            /// <summary>The right Shift key</summary>
            RShift,
            /// <summary>The right Alt key</summary>
            RAlt,
            /// <summary>The right OS specific key: window (Windows and Linux), apple (MacOS X), ...</summary>
            RSystem,
            /// <summary>The Menu key</summary>
            Menu,
            /// <summary>The [ key</summary>
            LBracket,
            /// <summary>The ] key</summary>
            RBracket,
            /// <summary>The ; key</summary>
            Semicolon,
            /// <summary>The , key</summary>
            Comma,
            /// <summary>The . key</summary>
            Period,
            /// <summary>The ' key</summary>
            Quote,
            /// <summary>The / key</summary>
            Slash,
            /// <summary>The \ key</summary>
            Backslash,
            /// <summary>The ~ key</summary>
            Tilde,
            /// <summary>The = key</summary>
            Equal,
            /// <summary>The - key</summary>
            Hyphen,
            /// <summary>The Space key</summary>
            Space,
            /// <summary>The Return key</summary>
            Enter,
            /// <summary>The Backspace key</summary>
            Backspace,
            /// <summary>The Tabulation key</summary>
            Tab,
            /// <summary>The Page up key</summary>
            PageUp,
            /// <summary>The Page down key</summary>
            PageDown,
            /// <summary>The End key</summary>
            End,
            /// <summary>The Home key</summary>
            Home,
            /// <summary>The Insert key</summary>
            Insert,
            /// <summary>The Delete key</summary>
            Delete,
            /// <summary>The + key</summary>
            Add,
            /// <summary>The - key</summary>
            Subtract,
            /// <summary>The * key</summary>
            Multiply,
            /// <summary>The / key</summary>
            Divide,
            /// <summary>Left arrow</summary>
            Left,
            /// <summary>Right arrow</summary>
            Right,
            /// <summary>Up arrow</summary>
            Up,
            /// <summary>Down arrow</summary>
            Down,
            /// <summary>The numpad 0 key</summary>
            Numpad0,
            /// <summary>The numpad 1 key</summary>
            Numpad1,
            /// <summary>The numpad 2 key</summary>
            Numpad2,
            /// <summary>The numpad 3 key</summary>
            Numpad3,
            /// <summary>The numpad 4 key</summary>
            Numpad4,
            /// <summary>The numpad 5 key</summary>
            Numpad5,
            /// <summary>The numpad 6 key</summary>
            Numpad6,
            /// <summary>The numpad 7 key</summary>
            Numpad7,
            /// <summary>The numpad 8 key</summary>
            Numpad8,
            /// <summary>The numpad 9 key</summary>
            Numpad9,
            /// <summary>The F1 key</summary>
            F1,
            /// <summary>The F2 key</summary>
            F2,
            /// <summary>The F3 key</summary>
            F3,
            /// <summary>The F4 key</summary>
            F4,
            /// <summary>The F5 key</summary>
            F5,
            /// <summary>The F6 key</summary>
            F6,
            /// <summary>The F7 key</summary>
            F7,
            /// <summary>The F8 key</summary>
            F8,
            /// <summary>The F9 key</summary>
            F9,
            /// <summary>The F10 key</summary>
            F10,
            /// <summary>The F11 key</summary>
            F11,
            /// <summary>The F12 key</summary>
            F12,
            /// <summary>The F13 key</summary>
            F13,
            /// <summary>The F14 key</summary>
            F14,
            /// <summary>The F15 key</summary>
            F15,
            /// <summary>The Pause key</summary>
            Pause,

            /// <summary>The total number of keyboard keys</summary>
            KeyCount, // Keep last

            // Deprecated backwards compatible stuff
            /// <summary>DEPRECATED: Use Hyphen</summary>
            [Obsolete("Deprecated: Use Hyphen instead.")]
            Dash = Hyphen,
            /// <summary>DEPRECATED: Use Backspace</summary>
            [Obsolete("Deprecated: Use Backspace instead.")]
            BackSpace = Backspace,
            /// <summary>DEPRECATED: Use Enter</summary>
            [Obsolete("Deprecated: Use Enter instead.")]
            Return = Enter,
            /// <summary>DEPRECATED: Use Backslash</summary>
            [Obsolete("Deprecated: Use Backslash instead.")]
            BackSlash = Backslash,
            /// <summary>DEPRECATED: Use Semicolon</summary>
            [Obsolete("Deprecated: Use Semicolon instead.")]
            SemiColon = Semicolon
        };

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Check if a key is pressed
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True if the key is pressed, false otherwise</returns>
        ////////////////////////////////////////////////////////////
        public static bool IsKeyPressed(Key key)
        {
            return sfKeyboard_isKeyPressed(key);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Enable/Disable visibility of the virtual keyboard
        /// </summary>
        /// <remarks>Applicable only on Android and iOS</remarks>
        /// <param name="visible">Whether to make the virtual keyboard visible (true) or not (false)</param>
        ////////////////////////////////////////////////////////////
        public static void SetVirtualKeyboardVisible(bool visible)
        {
            sfKeyboard_setVirtualKeyboardVisible(visible);
        }

        #region Imports
        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfKeyboard_isKeyPressed(Key Key);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfKeyboard_setVirtualKeyboardVisible(bool visible);
        #endregion
    }
}
