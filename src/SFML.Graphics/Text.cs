using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using SFML.System;

namespace SFML.Graphics
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// This class defines a graphical 2D text, that can be drawn on screen
    /// </summary>
    /// <remarks>
    /// See also the note on coordinates and undistorted rendering in SFML.Graphics.Transformable.
    /// </remarks>
    ////////////////////////////////////////////////////////////
    public class Text : Transformable, Drawable
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Flags for styles that can be applied to the <see cref="Text"/>
        /// </summary>
        ////////////////////////////////////////////////////////////
        [Flags]
        public enum Styles
        {
            /// <summary>No Style</summary>
            Regular = 0,

            /// <summary>Bold</summary>
            Bold = 1 << 0,

            /// <summary>Italic</summary>
            Italic = 1 << 1,

            /// <summary>Underlined</summary>
            Underlined = 1 << 2,

            /// <summary>Strikethrough</summary>
            StrikeThrough = 1 << 3
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Default constructor
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Text() :
            this("", null)
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the text from a string and a <see cref="SFML.Graphics.Font"/>
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
        /// Construct the text from a string, <see cref="SFML.Graphics.Font"/> and size
        /// </summary>
        /// <param name="str">String to display</param>
        /// <param name="font">Font to use</param>
        /// <param name="characterSize">Font size</param>
        ////////////////////////////////////////////////////////////
        public Text(string str, Font font, uint characterSize) :
            base(sfText_create())
        {
            DisplayedString = str;
            Font = font;
            CharacterSize = characterSize;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the text from another <see cref="SFML.Graphics.Text"/>
        /// </summary>
        /// <param name="copy">Text to copy</param>
        ////////////////////////////////////////////////////////////
        public Text(Text copy) :
            base(sfText_copy(copy.CPointer))
        {
            Origin = copy.Origin;
            Position = copy.Position;
            Rotation = copy.Rotation;
            Scale = copy.Scale;

            Font = copy.Font;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// DEPRECATED Fill <see cref="SFML.Graphics.Color"/> of the <see cref="Text"/>
        /// </summary>
        /// 
        /// <remarks>
        /// Use <see cref="FillColor"/> instead.
        /// 
        /// By default, the text's fill <see cref="SFML.Graphics.Color"/> is <see cref="SFML.Graphics.Color.White">opaque White</see>.
        /// <para>
        /// Setting the fill color to a transparent <see cref="SFML.Graphics.Color"/> with an outline
        /// will cause the outline to be displayed in the fill area of the text.
        /// </para>
        /// </remarks>
        ////////////////////////////////////////////////////////////
        [Obsolete("Use FillColor and OutlineColor")]
        public Color Color
        {
            get { return sfText_getFillColor(CPointer); }
            set { sfText_setFillColor(CPointer, value); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Fill <see cref="SFML.Graphics.Color"/> of the <see cref="Text"/>
        /// </summary>
        /// 
        /// <remarks>
        /// By default, the text's fill <see cref="SFML.Graphics.Color"/> is <see cref="SFML.Graphics.Color.White">opaque White</see>.
        /// <para>
        /// Setting the fill color to a transparent <see cref="SFML.Graphics.Color"/> with an outline
        /// will cause the outline to be displayed in the fill area of the text.
        /// </para>
        /// </remarks>
        ////////////////////////////////////////////////////////////
        public Color FillColor
        {
            get { return sfText_getFillColor(CPointer); }
            set { sfText_setFillColor(CPointer, value); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Outline <see cref="SFML.Graphics.Color"/> of the <see cref="Text"/>
        /// </summary>
        /// 
        /// <remarks>
        /// By default, the text's outline <see cref="SFML.Graphics.Color"/> is <see cref="SFML.Graphics.Color.Black">opaque Black</see>.
        /// </remarks>
        ////////////////////////////////////////////////////////////
        public Color OutlineColor
        {
            get { return sfText_getOutlineColor(CPointer); }
            set { sfText_setOutlineColor(CPointer, value); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Thickness of the object's outline
        /// </summary>
        /// 
        /// <remarks>
        /// <para>By default, the outline thickness is 0.</para>
        /// <para>Be aware that using a negative value for the outline
        /// thickness will cause distorted rendering.</para>
        /// </remarks>
        ////////////////////////////////////////////////////////////
        public float OutlineThickness
        {
            get { return sfText_getOutlineThickness(CPointer); }
            set { sfText_setOutlineThickness(CPointer, value); }
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
                // Get a pointer to the source string (UTF-32)
                IntPtr source = sfText_getUnicodeString(CPointer);

                // Find its length (find the terminating 0)
                uint length = 0;
                unsafe
                {
                    for (uint* ptr = (uint*)source.ToPointer(); *ptr != 0; ++ptr)
                    {
                        length++;
                    }
                }

                // Convert it to a C# string
                unsafe
                {
                    return Encoding.UTF32.GetString((byte*)source, (int)( length * 4 ));
                }
            }

            set
            {
                // Copy the string to a null-terminated UTF-32 byte array
                byte[] utf32 = Encoding.UTF32.GetBytes(value + '\0');

                // Pass it to the C API
                unsafe
                {
                    fixed (byte* ptr = utf32)
                    {
                        sfText_setUnicodeString(CPointer, (IntPtr)ptr);
                    }
                }
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// <see cref="SFML.Graphics.Font"/> used to display the text
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Font Font
        {
            get { return myFont; }
            set { myFont = value; sfText_setFont(CPointer, value != null ? value.CPointer : IntPtr.Zero); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Base size of characters
        /// </summary>
        ////////////////////////////////////////////////////////////
        public uint CharacterSize
        {
            get { return sfText_getCharacterSize(CPointer); }
            set { sfText_setCharacterSize(CPointer, value); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Size of the letter spacing factor
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float LetterSpacing
        {
            get { return sfText_getLetterSpacing(CPointer); }
            set { sfText_setLetterSpacing(CPointer, value); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Size of the line spacing factor
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float LineSpacing
        {
            get { return sfText_getLineSpacing(CPointer); }
            set { sfText_setLineSpacing(CPointer, value); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// <see cref="Styles">Style</see> of the text
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Styles Style
        {
            get { return sfText_getStyle(CPointer); }
            set { sfText_setStyle(CPointer, value); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Return the visual position of the Index-th character of the text,
        /// in coordinates relative to the text
        /// </summary>
        /// <remarks>
        /// Translation, origin, rotation and scale are not applied.
        /// </remarks>
        /// <param name="index">Index of the character</param>
        /// <returns>Position of the Index-th character (end of text if Index is out of range)</returns>
        ////////////////////////////////////////////////////////////
        public Vector2f FindCharacterPos(uint index)
        {
            return sfText_findCharacterPos(CPointer, (UIntPtr)index);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the local bounding <see cref="FloatRect"/> of the text.
        /// </summary>
        /// <remarks>
        /// The returned <see cref="FloatRect"/> is in local coordinates.
        /// <para>Transformations (Translation, Rotation, Scale) are not applied to the entity.</para>
        /// <para>In other words, this function returns the bounds of the
        /// entity in the entity's coordinate system.</para>
        /// </remarks>
        /// <returns>Local bounding rectangle of the entity</returns>
        ////////////////////////////////////////////////////////////
        public FloatRect GetLocalBounds()
        {
            return sfText_getLocalBounds(CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the global bounding rectangle of the text.
        /// </summary>
        /// <remarks>
        /// The returned <see cref="FloatRect"/> is in global coordinates.
        /// <para>Transformations (Translation, Rotation, Scale) are applied to the entity.</para>
        /// <para>In other words, this function returns the bounds of the
        /// sprite in the global 2D world's coordinate system.</para>
        /// </remarks>
        /// <returns>Global bounding rectangle of the entity</returns>
        ////////////////////////////////////////////////////////////
        public FloatRect GetGlobalBounds()
        {
            // we don't use the native getGlobalBounds function,
            // because we override the object's transform
            return Transform.TransformRect(GetLocalBounds());
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
                   " FillColor(" + FillColor + ")" +
                   " OutlineColor(" + OutlineColor + ")" +
                   " String(" + DisplayedString + ")" +
                   " Font(" + Font + ")" +
                   " CharacterSize(" + CharacterSize + ")" +
                   " OutlineThickness(" + OutlineThickness + ")" +
                   " Style(" + Style + ")";
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Draw the text to a <see cref="RenderTarget"/>
        /// </summary>
        /// <param name="target">Render target to draw to</param>
        /// <param name="states">Current render states</param>
        ////////////////////////////////////////////////////////////
        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            RenderStates.MarshalData marshaledStates = states.Marshal();

            if (target is RenderWindow)
            {
                sfRenderWindow_drawText(( (RenderWindow)target ).CPointer, CPointer, ref marshaledStates);
            }
            else if (target is RenderTexture)
            {
                sfRenderTexture_drawText(( (RenderTexture)target ).CPointer, CPointer, ref marshaledStates);
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
            sfText_destroy(CPointer);
        }

        private Font myFont = null;

        #region Imports
        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfText_create();

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfText_copy(IntPtr Text);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfText_destroy(IntPtr CPointer);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfText_setFillColor(IntPtr CPointer, Color Color);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfText_setOutlineColor(IntPtr CPointer, Color Color);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfText_setOutlineThickness(IntPtr CPointer, float thickness);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Color sfText_getFillColor(IntPtr CPointer);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Color sfText_getOutlineColor(IntPtr CPointer);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfText_getOutlineThickness(IntPtr CPointer);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_drawText(IntPtr CPointer, IntPtr Text, ref RenderStates.MarshalData states);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderTexture_drawText(IntPtr CPointer, IntPtr Text, ref RenderStates.MarshalData states);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfText_setUnicodeString(IntPtr CPointer, IntPtr Text);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfText_setFont(IntPtr CPointer, IntPtr Font);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfText_setCharacterSize(IntPtr CPointer, uint Size);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfText_setLineSpacing(IntPtr CPointer, float spacingFactor);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfText_setLetterSpacing(IntPtr CPointer, float spacingFactor);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfText_setStyle(IntPtr CPointer, Styles Style);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfText_getUnicodeString(IntPtr CPointer);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint sfText_getCharacterSize(IntPtr CPointer);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfText_getLetterSpacing(IntPtr CPointer);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfText_getLineSpacing(IntPtr CPointer);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Styles sfText_getStyle(IntPtr CPointer);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f sfText_findCharacterPos(IntPtr CPointer, UIntPtr Index);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern FloatRect sfText_getLocalBounds(IntPtr CPointer);
        #endregion
    }
}
