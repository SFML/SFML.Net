using System;
using System.Security;
using System.Runtime.InteropServices;
using SFML.Window;

namespace SFML
{
    namespace Graphics
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// This class defines a graphical 2D text, that can be drawn on screen
        /// </summary>
        ////////////////////////////////////////////////////////////
        public class Text : Transformable, Drawable
        {
            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Enumerate the string drawing styles
            /// </summary>
            ////////////////////////////////////////////////////////////
            [Flags]
            public enum Styles
            {
                /// <summary>Regular characters, no style</summary>
                Regular = 0,

                /// <summary> Characters are bold</summary>
                Bold = 1 << 0,

                /// <summary>Characters are in italic</summary>
                Italic = 1 << 1,

                /// <summary>Characters are underlined</summary>
                Underlined = 1 << 2
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Default constructor
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Text() :
                this("")
            {
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the text from a string
            /// </summary>
            /// <param name="str">String to display</param>
            ////////////////////////////////////////////////////////////
            public Text(string str) :
                this(str, Font.DefaultFont)
            {
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the text from a string and a font
            /// </summary>
            /// <param name="str">String to display</param>
            /// <param name="font">Font to use</param>
            ////////////////////////////////////////////////////////////
            public Text(string str, Font font) :
                this(str, font, 30)
            {
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the text from a string, font and size
            /// </summary>
            /// <param name="str">String to display</param>
            /// <param name="font">Font to use</param>
            /// <param name="characterSize">Base characters size</param>
            ////////////////////////////////////////////////////////////
            public Text(string str, Font font, uint characterSize) :
                base(sfText_Create())
            {
                DisplayedString = str;
                Font = font;
                CharacterSize = characterSize;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the text from another text
            /// </summary>
            /// <param name="copy">Text to copy</param>
            ////////////////////////////////////////////////////////////
            public Text(Text copy) :
                base(sfText_Copy(copy.CPointer))
            {
                Origin = copy.Origin;
                Position = copy.Position;
                Rotation = copy.Rotation;
                Scale = copy.Scale;

                Font = copy.Font;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Global color of the object
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Color Color
            {
                get { return sfText_GetColor(CPointer); }
                set { sfText_SetColor(CPointer, value); }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// String which is displayed
            /// </summary>
            ////////////////////////////////////////////////////////////
            public string DisplayedString
            {
                get
                {
                    // Get the number of characters
                    // This is probably not the most optimized way; if anyone has a better solution...
                    int length = Marshal.PtrToStringAnsi(sfText_GetString(CPointer)).Length;

                    // Copy the characters
                    byte[] characters = new byte[length * 4];
                    Marshal.Copy(sfText_GetUnicodeString(CPointer), characters, 0, characters.Length);

                    // Convert from UTF-32 to String (UTF-16)
                    return System.Text.Encoding.UTF32.GetString(characters);
                }

                set
                {
                    // Convert from String (UTF-16) to UTF-32
                    int[] characters = new int[value.Length];
                    for (int i = 0; i < value.Length; ++i)
                        characters[i] = Char.ConvertToUtf32(value, i);

                    // Transform to raw and pass to the C API
                    GCHandle handle = GCHandle.Alloc(characters, GCHandleType.Pinned);
                    sfText_SetUnicodeString(CPointer, handle.AddrOfPinnedObject());
                    handle.Free();
                }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Font used to display the text
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Font Font
            {
                get {return myFont;}
                set {myFont = value; sfText_SetFont(CPointer, value != null ? value.CPointer : IntPtr.Zero);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Base size of characters
            /// </summary>
            ////////////////////////////////////////////////////////////
            public uint CharacterSize
            {
                get {return sfText_GetCharacterSize(CPointer);}
                set {sfText_SetCharacterSize(CPointer, value);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Style of the text (see Styles enum)
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Styles Style
            {
                get {return sfText_GetStyle(CPointer);}
                set {sfText_SetStyle(CPointer, value);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Return the visual position of the Index-th character of the text,
            /// in coordinates relative to the text
            /// (note : translation, origin, rotation and scale are not applied)
            /// </summary>
            /// <param name="index">Index of the character</param>
            /// <returns>Position of the Index-th character (end of text if Index is out of range)</returns>
            ////////////////////////////////////////////////////////////
            public Vector2f FindCharacterPos(uint index)
            {
                return sfText_FindCharacterPos(CPointer, index);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Get the local bounding rectangle of the entity.
            ///
            /// The returned rectangle is in local coordinates, which means
            /// that it ignores the transformations (translation, rotation,
            /// scale, ...) that are applied to the entity.
            /// In other words, this function returns the bounds of the
            /// entity in the entity's coordinate system.
            /// </summary>
            /// <returns>Local bounding rectangle of the entity</returns>
            ////////////////////////////////////////////////////////////
            public FloatRect GetLocalBounds()
            {
                return sfText_GetLocalBounds(CPointer);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Get the global bounding rectangle of the entity.
            ///
            /// The returned rectangle is in global coordinates, which means
            /// that it takes in account the transformations (translation,
            /// rotation, scale, ...) that are applied to the entity.
            /// In other words, this function returns the bounds of the
            /// sprite in the global 2D world's coordinate system.
            /// </summary>
            /// <returns>Global bounding rectangle of the entity</returns>
            ////////////////////////////////////////////////////////////
            public FloatRect GetGlobalBounds()
            {
                return sfText_GetGlobalBounds(CPointer);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Provide a string describing the object
            /// </summary>
            /// <returns>String description of the object</returns>
            ////////////////////////////////////////////////////////////
            public override string ToString()
            {
                return "[Text]" +
                       " Color(" + Color + ")" +
                       " String(" + DisplayedString + ")" +
                       " Font(" + Font + ")" +
                       " CharacterSize(" + CharacterSize + ")" +
                       " Style(" + Style + ")";
            }

            ////////////////////////////////////////////////////////////
            /// <summmary>
            /// Draw the object to a render target
            ///
            /// This is a pure virtual function that has to be implemented
            /// by the derived class to define how the drawable should be
            /// drawn.
            /// </summmary>
            /// <param name="target">Render target to draw to</param>
            /// <param name="states">Current render states</param>
            ////////////////////////////////////////////////////////////
            public void Draw(RenderTarget target, RenderStates states)
            {
                states.Transform *= Transform;
                RenderStates.MarshalData marshaledStates = states.Marshal();

                if (target is RenderWindow)
                {
                    sfRenderWindow_DrawText(((RenderWindow)target).CPointer, CPointer, ref marshaledStates);
                }
                else if (target is RenderTexture)
                {
                    sfRenderWindow_DrawText(((RenderTexture)target).CPointer, CPointer, ref marshaledStates);
                }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Handle the destruction of the object
            /// </summary>
            /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
            ////////////////////////////////////////////////////////////
            protected override void Destroy(bool disposing)
            {
                sfText_Destroy(CPointer);
            }

            private Font myFont = Font.DefaultFont;

            #region Imports

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfText_Create();

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfText_Copy(IntPtr Text);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfText_Destroy(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfText_SetColor(IntPtr CPointer, Color Color);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Color sfText_GetColor(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_DrawText(IntPtr CPointer, IntPtr Text, ref RenderStates.MarshalData states);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderTexture_DrawText(IntPtr CPointer, IntPtr Text, ref RenderStates.MarshalData states);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfText_SetUnicodeString(IntPtr CPointer, IntPtr Text);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfText_SetFont(IntPtr CPointer, IntPtr Font);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfText_SetCharacterSize(IntPtr CPointer, uint Size);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfText_SetStyle(IntPtr CPointer, Styles Style);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfText_GetString(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfText_GetUnicodeString(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern uint sfText_GetCharacterSize(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Styles sfText_GetStyle(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern FloatRect sfText_GetRect(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Vector2f sfText_FindCharacterPos(IntPtr CPointer, uint Index);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern FloatRect sfText_GetLocalBounds(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern FloatRect sfText_GetGlobalBounds(IntPtr CPointer);

            #endregion
        }
    }
}
