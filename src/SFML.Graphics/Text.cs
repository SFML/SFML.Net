using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using SFML.System;

namespace SFML.Graphics;

////////////////////////////////////////////////////////////
/// <summary>
/// This class defines a graphical 2D text, that can be drawn on screen
/// </summary>
/// <remarks>
/// See also the note on coordinates and undistorted rendering in SFML.Graphics.Transformable.
/// </remarks>
////////////////////////////////////////////////////////////
public class Text : Transformable, IDrawable
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
    /// Line alignment of a multi-line text
    /// </summary>
    ////////////////////////////////////////////////////////////
    public enum Alignment
    {
        /// <summary> Automatically align lines by script direction, left-align left-to-right text and right-align right-to-left text </summary>
        Default,

        /// <summary> Force align all lines to the left, regardless of script direction </summary>
        Left,

        /// <summary> Force align all lines centrally </summary>
        Center,

        /// <summary> Force align lines to the right, regardless of script direction </summary>
        Right
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Line alignment of a multi-line text
    /// </summary>
    ////////////////////////////////////////////////////////////
    public enum TextClusterGrouping
    {
        /// <summary> Group clusters by grapheme </summary>
        Grapheme,

        /// <summary> Group clusters by character </summary>
        Character,

        /// <summary> Do not group clusters </summary>
        None
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Direction of the text a glyph belongs to
    /// </summary>
    ////////////////////////////////////////////////////////////
    public enum Direction
    {
        /// <summary> Unspecified </summary>
        Unspecified,

        /// <summary> Left-to-right </summary>
        LeftToRight,

        /// <summary> Right-to-left </summary>
        RightToLeft,

        /// <summary> Top-to-bottom </summary>
        TopToBottom,

        /// <summary> Bottom-to-top </summary>
        BottomToTop
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Text Orientation
    /// </summary>
    ////////////////////////////////////////////////////////////
    public enum Orientation
    {
        /// <summary> Default (left-to-right or right-to-left depending on detected script) </summary>
        Default,

        /// <summary> Top-to-bottom </summary>
        TopToBottom,

        /// <summary> Bottom-to-top </summary>
        BottomToTop
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Glyph that has been positioned by the shaper
    /// </summary>
    ////////////////////////////////////////////////////////////
    public struct ShapedGlyph
    {
        /// <summary> The glyph </summary>
        public Glyph Glyph;

        /// <summary> Position of the glyph within a text </summary>
        public Vector2f Position;

        /// <summary> Cluster ID </summary>
        public uint Cluster;

        /// <summary> Text direction </summary>
        public Direction TextDirection;

        /// <summary> The baseline position of the line this glyph is a part of </summary>
        public float Baseline;

        /// <summary> Starting offset of the vertex data belonging to this glyph </summary>
        public uint VertexOffset;

        /// <summary> Count of vertices belonging to this glyph </summary>
        public uint VertexCount;
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Callback that is provided with glyph data for pre-processing
    ///
    /// The callback is called once per glyph whenever the text
    /// geometry is regenerated, in the order in which the glyph
    /// geometry is generated. The style, fill color, outline color
    /// and outline thickness of the glyph can be modified through
    /// the given pointers.
    ///
    /// To map glyphs back to the input string, use the cluster
    /// value of the shaped glyph. See sf::Text::GlyphPreProcessor
    /// in the SFML documentation for a detailed explanation.
    ///
    /// Changing the style or outline thickness might lead to
    /// slight inconsistencies of the text bounds, changing the
    /// fill or outline color is always safe. It is not safe to
    /// query the text bounds from within the callback.
    /// </summary>
    /// <param name="shapedGlyph">The shaped glyph to pre-process</param>
    /// <param name="style">Style of the glyph (see sfTextStyle enum)</param>
    /// <param name="fillColor">Fill color of the glyph</param>
    /// <param name="outlineColor">Outline color of the glyph</param>
    /// <param name="outlineThickness">Outline thickness of the glyph</param>
    /// <param name="userData">User data passed to sfText_setGlyphPreProcessor</param>
    ////////////////////////////////////////////////////////////
    // I can use EffectProcessor as a blueprint
    public delegate void GlyphPreProcessorCallback(ref ShapedGlyph shapedGlyph, ref uint style, ref Color fillColor, ref Color outlineColor,
        ref float outlineThickness, object userData);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Construct the text from a <see cref="SFML.Graphics.Font"/>
    /// </summary>
    /// <param name="font">Font to use</param>
    ////////////////////////////////////////////////////////////
    public Text(Font font) :
        this(font, "", 30)
    {
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Construct the text from a string and a <see cref="SFML.Graphics.Font"/>
    /// </summary>
    /// <param name="font">Font to use</param>
    /// <param name="str">String to display</param>
    ////////////////////////////////////////////////////////////
    public Text(Font font, string str) :
        this(font, str, 30)
    {
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Construct the text from a string, <see cref="SFML.Graphics.Font"/> and size
    /// </summary>
    /// <param name="font">Font to use</param>
    /// <param name="str">String to display</param>
    /// <param name="characterSize">Font size</param>
    ////////////////////////////////////////////////////////////
    public Text(Font font, string str, uint characterSize) :
        base(sfText_create(font.CPointer))
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
        get => sfText_getFillColor(CPointer);
        set => sfText_setFillColor(CPointer, value);
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
        get => sfText_getOutlineColor(CPointer);
        set => sfText_setOutlineColor(CPointer, value);
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
        get => sfText_getOutlineThickness(CPointer);
        set => sfText_setOutlineThickness(CPointer, value);
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
            var source = sfText_getUnicodeString(CPointer);

            // Find its length (find the terminating 0)
            uint length = 0;
            unsafe
            {
                for (var ptr = (uint*)source.ToPointer(); *ptr != 0; ++ptr)
                {
                    length++;
                }
            }

            // Copy it to a byte array
            var sourceBytes = new byte[length * 4];
            Marshal.Copy(source, sourceBytes, 0, sourceBytes.Length);

            // Convert it to a C# string
            return Encoding.UTF32.GetString(sourceBytes);
        }

        set
        {
            // Copy the string to a null-terminated UTF-32 byte array
            var utf32 = Encoding.UTF32.GetBytes(value + '\0');

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
        get => _font;
        set { _font = value; sfText_setFont(CPointer, value?.CPointer ?? IntPtr.Zero); }
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Base size of characters
    /// </summary>
    ////////////////////////////////////////////////////////////
    public uint CharacterSize
    {
        get => sfText_getCharacterSize(CPointer);
        set => sfText_setCharacterSize(CPointer, value);
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Size of the letter spacing factor
    /// </summary>
    ////////////////////////////////////////////////////////////
    public float LetterSpacing
    {
        get => sfText_getLetterSpacing(CPointer);
        set => sfText_setLetterSpacing(CPointer, value);
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Size of the line spacing factor
    /// </summary>
    ////////////////////////////////////////////////////////////
    public float LineSpacing
    {
        get => sfText_getLineSpacing(CPointer);
        set => sfText_setLineSpacing(CPointer, value);
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// <see cref="Styles">Style</see> of the text
    /// </summary>
    ////////////////////////////////////////////////////////////
    public Styles Style
    {
        get => sfText_getStyle(CPointer);
        set => sfText_setStyle(CPointer, value);
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Line alignment for a multi-line text
    /// </summary>
    ////////////////////////////////////////////////////////////
    public Alignment LineAlignment
    {
        get => sfText_getLineAlignment(CPointer);
        set => sfText_setLineAlignment(CPointer, value);
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Text orientation
    ///
    /// Vertical text orientations require the font to provide
    /// vertical metrics, see <see cref="Font.Info"/>
    /// </summary>
    ////////////////////////////////////////////////////////////
    public Orientation TextOrientation
    {
        get => sfText_getTextOrientation(CPointer);
        set => sfText_setTextOrientation(CPointer, value);
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Cluster grouping algorithm in use
    /// </summary>
    ////////////////////////////////////////////////////////////
    public TextClusterGrouping ClusterGrouping
    {
        get => sfText_getClusterGrouping(CPointer);
        set => sfText_setClusterGrouping(CPointer, value);
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
    [Obsolete("Use GetShapedGlyphs instead")]
    public Vector2f FindCharacterPos(uint index) => sfText_findCharacterPos(CPointer, (UIntPtr)index);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Get the shaped glyphs that make up a text
    /// </summary>
    /// <returns>Array of shaped glyphs</returns>
    ////////////////////////////////////////////////////////////
    public ShapedGlyph[] GetShapedGlyphs()
    {
        unsafe
        {
            ulong length = 0;
            var rawGlyphs = sfText_getShapedGlyphs(CPointer, ref length);
            var glyphs = new ShapedGlyph[length];
            for (ulong i = 0; i < length; i++)
            {
                glyphs[i] = rawGlyphs[i];
            }
            return glyphs;
        }
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Set the glyph pre-processor to be called per glyph
    ///
    /// The glyph pre-processor is called with glyph data to be
    /// pre-processed whenever the text geometry is regenerated.
    /// </summary>
    /// <param name="callback">The glyph pre-processor to be called per glyph, pass null to disable pre-processing</param>
    /// <param name="userData">User data that will be passed to the glyph pre-processor</param>
    ////////////////////////////////////////////////////////////
    public void SetGlyphPreProcessor(GlyphPreProcessorCallback callback, object userData)
    {
        if (callback is null)
        {
            sfText_setGlyphPreProcessor(CPointer, IntPtr.Zero, IntPtr.Zero);
            return;
        }
        _internalGlyphPreProcessorCallback = (ref ShapedGlyph glyph, ref uint style, ref Color color, ref Color outlineColor, ref float thickness,
            IntPtr data) =>
        {
            callback(ref glyph, ref style, ref color, ref outlineColor, ref thickness, userData);
        };
        sfText_setGlyphPreProcessor(CPointer, Marshal.GetFunctionPointerForDelegate(_internalGlyphPreProcessorCallback), IntPtr.Zero);
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Get the vertex data of the text
    /// </summary>
    /// <returns></returns>
    ////////////////////////////////////////////////////////////
    public Vertex[] GetVertexData()
    {
        unsafe
        {
            ulong length = 0;
            var rawVerts = sfText_getVertexData(CPointer, ref length);
            var vertices = new Vertex[length];
            for (ulong i = 0; i < length; i++)
            {
                vertices[i] = rawVerts[i];
            }
            return vertices;
        }
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Get the outline vertex data of the text
    /// </summary>
    /// <returns></returns>
    ////////////////////////////////////////////////////////////
    public Vertex[] GetOutlineVertexData()
    {
        unsafe
        {
            ulong length = 0;
            var rawVerts = sfText_getOutlineVertexData(CPointer, ref length);
            var vertices = new Vertex[length];
            for (ulong i = 0; i < length; i++)
            {
                vertices[i] = rawVerts[i];
            }
            return vertices;
        }
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
    public FloatRect GetLocalBounds() => sfText_getLocalBounds(CPointer);

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
    public FloatRect GetGlobalBounds() => Transform.TransformRect(GetLocalBounds()); // we don't use the native getGlobalBounds function, because we override the object's transform

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
    /// Draw the text to a <see cref="IRenderTarget"/>
    /// </summary>
    /// <param name="target">Render target to draw to</param>
    /// <param name="states">Current render states</param>
    ////////////////////////////////////////////////////////////
    public void Draw(IRenderTarget target, RenderStates states)
    {
        states.Transform *= Transform;
        var marshaledStates = states.Marshal();

        if (target is RenderWindow window)
        {
            sfRenderWindow_drawText(window.CPointer, CPointer, ref marshaledStates);
        }
        else if (target is RenderTexture texture)
        {
            sfRenderTexture_drawText(texture.CPointer, CPointer, ref marshaledStates);
        }
    }

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Handle the destruction of the object
    /// </summary>
    /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
    ////////////////////////////////////////////////////////////
    protected override void Destroy(bool disposing) => sfText_destroy(CPointer);

    private Font _font;
    private InternalGlyphPreProcessorCallback _internalGlyphPreProcessorCallback;

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void InternalGlyphPreProcessorCallback(ref ShapedGlyph shapedGlyph, ref uint style, ref Color fillColor, ref Color outlineColor,
        ref float outlineThickness, IntPtr userData);


    #region Imports
    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern IntPtr sfText_create(IntPtr font);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern IntPtr sfText_copy(IntPtr text);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern void sfText_destroy(IntPtr cPointer);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern void sfText_setFillColor(IntPtr cPointer, Color color);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern void sfText_setOutlineColor(IntPtr cPointer, Color color);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern void sfText_setOutlineThickness(IntPtr cPointer, float thickness);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern Color sfText_getFillColor(IntPtr cPointer);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern Color sfText_getOutlineColor(IntPtr cPointer);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern float sfText_getOutlineThickness(IntPtr cPointer);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern void sfRenderWindow_drawText(IntPtr cPointer, IntPtr text, ref RenderStates.MarshalData states);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern void sfRenderTexture_drawText(IntPtr cPointer, IntPtr text, ref RenderStates.MarshalData states);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern void sfText_setUnicodeString(IntPtr cPointer, IntPtr text);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern void sfText_setFont(IntPtr cPointer, IntPtr font);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern void sfText_setCharacterSize(IntPtr cPointer, uint size);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern void sfText_setLineSpacing(IntPtr cPointer, float spacingFactor);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern void sfText_setLetterSpacing(IntPtr cPointer, float spacingFactor);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern void sfText_setStyle(IntPtr cPointer, Styles style);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern IntPtr sfText_getUnicodeString(IntPtr cPointer);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern uint sfText_getCharacterSize(IntPtr cPointer);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern float sfText_getLetterSpacing(IntPtr cPointer);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern float sfText_getLineSpacing(IntPtr cPointer);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern Styles sfText_getStyle(IntPtr cPointer);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern Vector2f sfText_findCharacterPos(IntPtr cPointer, UIntPtr index);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern FloatRect sfText_getLocalBounds(IntPtr cPointer);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern Alignment sfText_getLineAlignment(IntPtr cPointer);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern Orientation sfText_getTextOrientation(IntPtr cPointer);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern void sfText_setLineAlignment(IntPtr cPointer, Alignment lineAlignment);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern void sfText_setTextOrientation(IntPtr cPointer, Orientation textOrientation);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern unsafe ShapedGlyph* sfText_getShapedGlyphs(IntPtr cPointer, ref ulong count);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern TextClusterGrouping sfText_getClusterGrouping(IntPtr cPointer);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern void sfText_setClusterGrouping(IntPtr cPointer, TextClusterGrouping clusterGrouping);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern void sfText_setGlyphPreProcessor(IntPtr cPointer, IntPtr callback, IntPtr userData);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern unsafe Vertex* sfText_getVertexData(IntPtr cPointer, ref ulong count);

    [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern unsafe Vertex* sfText_getOutlineVertexData(IntPtr cPointer, ref ulong count);
    #endregion
}
