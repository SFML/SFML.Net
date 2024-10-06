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
        /// 
        /// The enumerators refer to the "localized" key; i.e. depending
        /// on the layout set by the operating system, a key can be mapped
        /// to `Y` or `Z`.
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
            /// <summary>The left OS specific key: window (Windows and Linux), apple (macOS), ...</summary>
            LSystem,
            /// <summary>The right Control key</summary>
            RControl,
            /// <summary>The right Shift key</summary>
            RShift,
            /// <summary>The right Alt key</summary>
            RAlt,
            /// <summary>The right OS specific key: window (Windows and Linux), apple (macOS), ...</summary>
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
            Apostrophe,
            /// <summary>The / key</summary>
            Slash,
            /// <summary>The \ key</summary>
            Backslash,
            /// <summary>The ~ key</summary>
            Grave,
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
            Pause
        };

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// The total number of keyboard keys, ignoring <see cref="Key.Unknown"/>
        /// </summary>
        ////////////////////////////////////////////////////////////
        public static readonly uint KeyCount = (uint)Key.Pause + 1;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Scancodes
        /// 
        /// The enumerators are bound to a physical key and do not depend on
        /// the keyboard layout used by the operating system. Usually, the AT-101
        /// keyboard can be used as reference for the physical position of the keys.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public enum Scancode
        {
            /// <summary>Represents any scancode not present in this enum</summary>
            Unknown = -1,
            /// <summary>Keyboard a and A key</summary>
            A = 0,
            /// <summary>Keyboard b and B key</summary>
            B,
            /// <summary>Keyboard c and C key</summary>
            C,
            /// <summary>Keyboard d and D key</summary>
            D,
            /// <summary>Keyboard e and E key</summary>
            E,
            /// <summary>Keyboard f and F key</summary>
            F,
            /// <summary>Keyboard g and G key</summary>
            G,
            /// <summary>Keyboard h and H key</summary>
            H,
            /// <summary>Keyboard i and I key</summary>
            I,
            /// <summary>Keyboard j and J key</summary>
            J,
            /// <summary>Keyboard k and K key</summary>
            K,
            /// <summary>Keyboard l and L key</summary>
            L,
            /// <summary>Keyboard m and M key</summary>
            M,
            /// <summary>Keyboard n and N key</summary>
            N,
            /// <summary>Keyboard o and O key</summary>
            O,
            /// <summary>Keyboard p and P key</summary>
            P,
            /// <summary>Keyboard q and Q key</summary>
            Q,
            /// <summary>Keyboard r and R key</summary>
            R,
            /// <summary>Keyboard s and S key</summary>
            S,
            /// <summary>Keyboard t and T key</summary>
            T,
            /// <summary>Keyboard u and U key</summary>
            U,
            /// <summary>Keyboard v and V key</summary>
            V,
            /// <summary>Keyboard w and W key</summary>
            W,
            /// <summary>Keyboard x and X key</summary>
            X,
            /// <summary>Keyboard y and Y key</summary>
            Y,
            /// <summary>Keyboard z and Z key</summary>
            Z,
            /// <summary>Keyboard 1 and ! key</summary>
            Num1,
            /// <summary>Keyboard 2 and @ key</summary>
            Num2,
            /// <summary>Keyboard 3 and # key</summary>
            Num3,
            /// <summary>Keyboard 4 and $ key</summary>
            Num4,
            /// <summary>Keyboard 5 and % key</summary>
            Num5,
            /// <summary>Keyboard 6 and ^ key</summary>
            Num6,
            /// <summary>Keyboard 7 and &amp; key</summary>
            Num7,
            /// <summary>Keyboard 8 and * key</summary>
            Num8,
            /// <summary>Keyboard 9 and ) key</summary>
            Num9,
            /// <summary>Keyboard 0 and ) key</summary>
            Num0,
            /// <summary>Keyboard Enter/Return key</summary>
            Enter,
            /// <summary>Keyboard Escape key</summary>
            Escape,
            /// <summary>Keyboard Backspace key</summary>
            Backspace,
            /// <summary>Keyboard Tab key</summary>
            Tab,
            /// <summary>Keyboard Space key</summary>
            Space,
            /// <summary>Keyboard - and _ key</summary>
            Hyphen,
            /// <summary>Keyboard = and +</summary>
            Equal,
            /// <summary>Keyboard [ and { key</summary>
            LBracket,
            /// <summary>Keyboard ] and } key</summary>
            RBracket,
            // For US keyboards mapped to key 29 (Microsoft Keyboard Scan Code Specification)
            // For Non-US keyboards mapped to key 42 (Microsoft Keyboard Scan Code Specification)
            // Typical language mappings: Belg:£µ` FrCa:<>} Dan:*' Dutch:`´ Fren:µ* Ger:'# Ital:§ù LatAm:[}` Nor:*@ Span:ç} Swed:*' Swiss:$£} UK:~# Brazil:}]
            /// <summary>Keyboard \ and | key OR various keys for Non-US keyboards</summary>
            Backslash,
            /// <summary>Keyboard ; and : key</summary>
            Semicolon,
            /// <summary>Keyboard ' and " key</summary>
            Apostrophe,
            /// <summary>Keyboard ` and ~ key</summary>
            Grave,
            /// <summary>Keyboard , and &lt; key</summary>
            Comma,
            /// <summary>Keyboard . and > key</summary>
            Period,
            /// <summary>Keyboard / and ? key</summary>
            Slash,
            /// <summary>Keyboard F1 key</summary>
            F1,
            /// <summary>Keyboard F2 key</summary>
            F2,
            /// <summary>Keyboard F3 key</summary>
            F3,
            /// <summary>Keyboard F5 key</summary>
            F4,
            /// <summary>Keyboard F4 key</summary>
            F5,
            /// <summary>Keyboard F6 key</summary>
            F6,
            /// <summary>Keyboard F7 key</summary>
            F7,
            /// <summary>Keyboard F8 key</summary>
            F8,
            /// <summary>Keyboard F9 key</summary>
            F9,
            /// <summary>Keyboard F10 key</summary>
            F10,
            /// <summary>Keyboard F11 key</summary>
            F11,
            /// <summary>Keyboard F12 key</summary>
            F12,
            /// <summary>Keyboard F13 key</summary>
            F13,
            /// <summary>Keyboard F14 key</summary>
            F14,
            /// <summary>Keyboard F15 key</summary>
            F15,
            /// <summary>Keyboard F16 key</summary>
            F16,
            /// <summary>Keyboard F17 key</summary>
            F17,
            /// <summary>Keyboard F18 key</summary>
            F18,
            /// <summary>Keyboard F19 key</summary>
            F19,
            /// <summary>Keyboard F20 key</summary>
            F20,
            /// <summary>Keyboard F21 key</summary>
            F21,
            /// <summary>Keyboard F22 key</summary>
            F22,
            /// <summary>Keyboard F23 key</summary>
            F23,
            /// <summary>Keyboard F24 key</summary>
            F24,
            /// <summary>Keyboard Caps %Lock key</summary>
            CapsLock,
            /// <summary>Keyboard Print Screen key</summary>
            PrintScreen,
            /// <summary>Keyboard Scroll %Lock key</summary>
            ScrollLock,
            /// <summary>Keyboard Pause key</summary>
            Pause,
            /// <summary>Keyboard Insert key</summary>
            Insert,
            /// <summary>Keyboard Home key</summary>
            Home,
            /// <summary>Keyboard Page Up key</summary>
            PageUp,
            /// <summary>Keyboard Delete Forward key</summary>
            Delete,
            /// <summary>Keyboard End key</summary>
            End,
            /// <summary>Keyboard Page Down key</summary>
            PageDown,
            /// <summary>Keyboard Right Arrow key</summary>
            Right,
            /// <summary>Keyboard Left Arrow key</summary>
            Left,
            /// <summary>Keyboard Down Arrow key</summary>
            Down,
            /// <summary>Keyboard Up Arrow key</summary>
            Up,
            /// <summary>Keypad Num %Lock and Clear key</summary>
            NumLock,
            /// <summary>Keypad / key</summary>
            NumpadDivide,
            /// <summary>Keypad * key</summary>
            NumpadMultiply,
            /// <summary>Keypad - key</summary>
            NumpadMinus,
            /// <summary>Keypad + key</summary>
            NumpadPlus,
            /// <summary>keypad = key</summary>
            NumpadEqual,
            /// <summary>Keypad Enter/Return key</summary>
            NumpadEnter,
            /// <summary>Keypad . and Delete key</summary>
            NumpadDecimal,
            /// <summary>Keypad 1 and End key</summary>
            Numpad1,
            /// <summary>Keypad 2 and Down Arrow key</summary>
            Numpad2,
            /// <summary>Keypad 3 and Page Down key</summary>
            Numpad3,
            /// <summary>Keypad 4 and Left Arrow key</summary>
            Numpad4,
            /// <summary>Keypad 5 key</summary>
            Numpad5,
            /// <summary>Keypad 6 and Right Arrow key</summary>
            Numpad6,
            /// <summary>Keypad 7 and Home key</summary>
            Numpad7,
            /// <summary>Keypad 8 and Up Arrow key</summary>
            Numpad8,
            /// <summary>Keypad 9 and Page Up key</summary>
            Numpad9,
            /// <summary>Keypad 0 and Insert key</summary>
            Numpad0,
            // For US keyboards doesn't exist
            // For Non-US keyboards mapped to key 45 (Microsoft Keyboard Scan Code Specification)
            // Typical language mappings: Belg:<\> FrCa:«°» Dan:<\> Dutch:]|[ Fren:<> Ger:<|> Ital:<> LatAm:<> Nor:<> Span:<> Swed:<|> Swiss:<\> UK:\| Brazil: \|.
            /// <summary>Keyboard Non-US \ and | key</summary>
            NonUsBackslash,
            /// <summary>Keyboard Application key</summary>
            Application,
            /// <summary>Keyboard Execute key</summary>
            Execute,
            /// <summary>Keyboard Mode Change key</summary>
            ModeChange,
            /// <summary>Keyboard Help key</summary>
            Help,
            /// <summary>Keyboard Menu key</summary>
            Menu,
            /// <summary>Keyboard Select key</summary>
            Select,
            /// <summary>Keyboard Redo key</summary>
            Redo,
            /// <summary>Keyboard Undo key</summary>
            Undo,
            /// <summary>Keyboard Cut key</summary>
            Cut,
            /// <summary>Keyboard Copy key</summary>
            Copy,
            /// <summary>Keyboard Paste key</summary>
            Paste,
            /// <summary>Keyboard Volume Mute key</summary>
            VolumeMute,
            /// <summary>Keyboard Volume Up key</summary>
            VolumeUp,
            /// <summary>Keyboard Volume Down key</summary>
            VolumeDown,
            /// <summary>Keyboard Media Play Pause key</summary>
            MediaPlayPause,
            /// <summary>Keyboard Media Stop key</summary>
            MediaStop,
            /// <summary>Keyboard Media Next Track key</summary>
            MediaNextTrack,
            /// <summary>Keyboard Media Previous Track key</summary>
            MediaPreviousTrack,
            /// <summary>Keyboard Left Control key</summary>
            LControl,
            /// <summary>Keyboard Left Shift key</summary>
            LShift,
            /// <summary>Keyboard Left Alt key</summary>
            LAlt,
            /// <summary>Keyboard Left System key</summary>
            LSystem,
            /// <summary>Keyboard Right Control key</summary>
            RControl,
            /// <summary>Keyboard Right Shift key</summary>
            RShift,
            /// <summary>Keyboard Right Alt key</summary>
            RAlt,
            /// <summary>Keyboard Right System key</summary>
            RSystem,
            /// <summary>Keyboard Back key</summary>
            Back,
            /// <summary>Keyboard Forward key</summary>
            Forward,
            /// <summary>Keyboard Refresh key</summary>
            Refresh,
            /// <summary>Keyboard Stop key</summary>
            Stop,
            /// <summary>Keyboard Search key</summary>
            Search,
            /// <summary>Keyboard Favorites key</summary>
            Favorites,
            /// <summary>Keyboard Home Page key</summary>
            HomePage,
            /// <summary>Keyboard Launch Application 1 key</summary>
            LaunchApplication1,
            /// <summary>Keyboard Launch Application 2 key</summary>
            LaunchApplication2,
            /// <summary>Keyboard Launch Mail key</summary>
            LaunchMail,
            /// <summary>Keyboard Launch Media Select key</summary>
            LaunchMediaSelect,
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// The total number of scancodes, ignoring <see cref="Scancode.Unknown"/>
        /// </summary>
        ////////////////////////////////////////////////////////////
        public static readonly uint ScancodeCount = (uint)Scancode.LaunchMediaSelect + 1;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Check if a key is pressed
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True if the key is pressed, false otherwise</returns>
        ////////////////////////////////////////////////////////////
        public static bool IsKeyPressed(Key key) => sfKeyboard_isKeyPressed(key);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Check if a key is pressed
        /// </summary>
        /// <param name="code">Scancode to check</param>
        /// <returns>True if the physical key is pressed, false otherwise</returns>
        ////////////////////////////////////////////////////////////
        public static bool IsScancodePressed(Scancode code) => sfKeyboard_isScancodePressed(code);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Localize a physical key to a logical one
        /// </summary>
        /// <param name="code">Scancode to localize</param>
        /// <returns>
        /// The key corresponding to the scancode under the current
        /// keyboard layout used by the operating system, or
        /// <see cref="Key.Unknown"/> when the scancode cannot be mapped
        /// to a <see cref="Key"/>.
        /// </returns>
        ////////////////////////////////////////////////////////////
        public static Key Localize(Scancode code) => sfKeyboard_localize(code);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Identify the physical key corresponding to a logical one
        /// </summary>
        /// <param name="key">Key to "delocalize"</param>
        /// <returns>
        /// The scancode corresponding to the key under the current
        /// keyboard layout used by the operating system, or
        /// <see cref="Scancode.Unknown"/> when the key cannot be mapped
        /// to a <see cref="Scancode"/>.
        /// </returns>
        ////////////////////////////////////////////////////////////
        public static Scancode Delocalize(Key key) => sfKeyboard_delocalize(key);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string representation for a given scancode
        /// <para/>
        /// The returned string is a short, non-technical description of
        /// the key represented with the given scancode. Most effectively
        /// used in user interfaces, as the description for the key takes
        /// the users keyboard layout into consideration.
        /// <para/>
        /// The current keyboard layout set by the operating system is used to
        /// interpret the scancode: for example, <see cref="Scancode.Semicolon"/> is
        /// mapped to ";" for layout and to "é" for others.
        /// </summary>
        /// <remarks>
        /// The result is OS-dependent: for example,  <see cref="Scancode.LSystem"/>
        /// is "Left Meta" on Linux, "Left Windows" on Windows and
        /// "Left Command" on macOS.
        /// </remarks>
        /// <param name="code">Scancode to describe</param>
        /// <returns>The localized description of the code</returns>
        ////////////////////////////////////////////////////////////
        public static string GetDescription(Scancode code)
        {
            // this returns an owning C pointer
            var ptr = sfKeyboard_getDescription(code);
            var description = Marshal.PtrToStringAnsi(ptr);
            Allocation.Free(ptr);

            return description;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Show or hide the virtual keyboard.
        /// </summary>
        /// <remarks>Applicable only on Android and iOS</remarks>
        /// <param name="visible">Whether to make the virtual keyboard visible (true) or not (false)</param>
        ////////////////////////////////////////////////////////////
        public static void SetVirtualKeyboardVisible(bool visible) => sfKeyboard_setVirtualKeyboardVisible(visible);

        #region Imports
        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfKeyboard_isKeyPressed(Key key);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfKeyboard_isScancodePressed(Scancode code);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Key sfKeyboard_localize(Scancode code);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Scancode sfKeyboard_delocalize(Key key);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfKeyboard_getDescription(Scancode code);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfKeyboard_setVirtualKeyboardVisible(bool visible);
        #endregion
    }
}
