using SFML.System;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace SFML.Graphics
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Vertex buffer storage for one or more 2D primitives.
    ///
    /// VertexBuffer is a simple wrapper around a dynamic buffer of vertices and a primitives type.
    ///
    /// Unlike SFML.VertexArray, the vertex data is stored in graphics memory.
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class VertexBuffer : ObjectBase, IDrawable
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// <see cref="VertexBuffer"/> Usage Specifiers
        /// </summary>
        /// <remarks>
        /// <para>If data is going to be updated once or more every frame, use <see cref="Stream"/>.</para>
        /// <para>If data is going to be set once and used for a long time without being modified, use <see cref="Static"/>.</para>
        /// <para>For everything else, <see cref="Dynamic"/> should be a good compromise.</para>
        /// </remarks>
        ////////////////////////////////////////////////////////////
        public enum UsageSpecifier
        {
            /// <summary>Constantly changing data.</summary>
            /// <remarks>Use when the <see cref="VertexBuffer"/> will be updated once or more every frame.</remarks>
            Stream,
            /// <summary>Occasionally changing data.</summary>
            /// <remarks>Use when the <see cref="VertexBuffer"/> will change infrequently.</remarks>
            Dynamic,
            /// <summary>Rarely changing data.</summary>
            /// <remarks>Use when the <see cref="VertexBuffer"/> will rarely of never be changed.</remarks>
            Static
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Whether or not the system supports <see cref="VertexBuffer"/>s
        /// </summary>
        /// <remarks>
        /// This function should always be called before using
        /// the vertex buffer features. If it returns false, then
        /// any attempt to use <see cref="VertexBuffer"/> will fail.
        /// </remarks>
        ///////////////////////////////////////////////////////////
        public static bool Available => sfVertexBuffer_isAvailable();

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Create a new <see cref="VertexBuffer"/> with a specific
        /// <see cref="SFML.Graphics.PrimitiveType"/> and <see cref="UsageSpecifier"/>
        /// </summary>
        /// <remarks>
        /// Creates the vertex buffer, allocating enough graphics
        /// memory to hold <paramref name="vertexCount"/> vertices, and sets its
        /// <see cref="SFML.Graphics.PrimitiveType"/> to primitiveType and <see cref="UsageSpecifier"/> to usageType.
        /// </remarks>
        /// <param name="vertexCount">Amount of vertices</param>
        /// <param name="primitiveType">Type of primitives</param>
        /// <param name="usageType">Usage specifier</param>
        ////////////////////////////////////////////////////////////
        public VertexBuffer(uint vertexCount, PrimitiveType primitiveType, UsageSpecifier usageType)
            : base(sfVertexBuffer_create(vertexCount, primitiveType, usageType))
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the vertex buffer from another <see cref="VertexBuffer"/>
        /// </summary>
        /// <param name="copy">VertexBuffer to copy</param>
        ////////////////////////////////////////////////////////////
        public VertexBuffer(VertexBuffer copy)
            : base(sfVertexBuffer_copy(copy.CPointer))
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Total vertex count
        /// </summary>
        ////////////////////////////////////////////////////////////
        public uint VertexCount => sfVertexBuffer_getVertexCount(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// OpenGL handle of the vertex buffer or 0 if not yet created
        /// </summary>
        /// <remarks>
        /// You shouldn't need to use this property, unless you have
        /// very specific stuff to implement that SFML doesn't support,
        /// or implement a temporary workaround until a bug is fixed.
        /// </remarks>
        ////////////////////////////////////////////////////////////
        public uint NativeHandle => sfVertexBuffer_getNativeHandle(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// The <see cref="SFML.Graphics.PrimitiveType"/> drawn by the <see cref="VertexBuffer"/>
        /// </summary>
        ////////////////////////////////////////////////////////////
        public PrimitiveType PrimitiveType
        {
            get => sfVertexBuffer_getPrimitiveType(CPointer);
            set => sfVertexBuffer_setPrimitiveType(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// The <see cref="UsageSpecifier"/> for the <see cref="VertexBuffer"/>
        /// </summary>
        ////////////////////////////////////////////////////////////
        public UsageSpecifier Usage
        {
            get => sfVertexBuffer_getUsage(CPointer);
            set => sfVertexBuffer_setUsage(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Bind a <see cref="VertexBuffer"/> for rendering.
        /// </summary>
        /// <remarks>
        /// <para>This function is not part of the graphics API.</para>
        /// <para>It must not be used when drawing SFML entities.</para>
        /// <para>It must be used only if you mix VertexBuffer with OpenGL code.</para>
        /// </remarks>
        /// <param name="vertexBuffer">The vertex buffer to bind; can be null to use no vertex buffer</param>
        ////////////////////////////////////////////////////////////
        public static void Bind(VertexBuffer vertexBuffer) => sfVertexBuffer_bind(vertexBuffer?.CPointer ?? IntPtr.Zero);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Update the <see cref="VertexBuffer"/> from a <see cref="Vertex"/>[]
        /// </summary>
        /// <remarks>
        /// <para>
        /// <pararef name="offset" /> is specified as the number of vertices to skip
        /// from the beginning of the buffer.
        /// </para>
        /// <para>
        /// If <pararef name="offset" /> is 0 and <paramref name="vertexCount" /> is equal to the size of
        /// the currently created buffer, its whole contents are replaced.
        /// </para>
        /// <para>
        /// If <pararef name="offset" /> is 0 and <paramref name="vertexCount" /> is greater than the
        /// size of the currently created buffer, a new buffer is created
        /// containing the <see cref="Vertex"/> data.
        /// </para>
        /// <para>
        /// If <pararef name="offset" /> is 0 and <paramref name="vertexCount" /> is less than the size of
        /// the currently created buffer, only the corresponding region
        /// is updated.
        /// </para>
        /// <para>
        /// If <pararef name="offset" /> is NOT 0 and <pararef name="offset" /> +
        /// <paramref name="vertexCount" /> is greater than the size of the currently
        /// created buffer, the update fails.
        /// </para>
        /// <para>
        /// No additional checks are performed on the size of the <see cref="VertexBuffer"/>.
        /// Passing invalid arguments will lead to undefined behavior.
        /// </para>
        /// </remarks>
        /// <param name="vertices">Array of vertices to copy from</param>
        /// <param name="vertexCount">Number of vertices to copy</param>
        /// <param name="offset">Offset in the buffer to copy to</param>
        ////////////////////////////////////////////////////////////
        public bool Update(Vertex[] vertices, uint vertexCount, uint offset)
        {
            unsafe
            {
                fixed (Vertex* verts = vertices)
                {
                    return sfVertexBuffer_update(CPointer, verts, vertexCount, offset);
                }
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Update a part of the buffer from a <see cref="Vertex"/>[]
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the length of <paramref name="vertices" /> is equal
        /// to the current size of the buffer, the entire buffer is replaced.
        /// </para>
        /// <para>
        /// If the length of <paramref name="vertices" /> is greater than the
        /// current size of the buffer, a new buffer is created containing
        /// the <see cref="Vertex"/> data.
        /// </para>
        /// <para>
        /// If the length of <paramref name="vertices" /> is less than the
        /// current size of the buffer, only the first <paramref name="vertices" />.Length
        /// vertices are replaced.
        /// </para>
        /// <para>
        /// No additional checks are performed on <paramref name="vertices" />.Length.
        /// Passing invalid arguments will lead to undefined behavior.
        /// </para>
        /// </remarks>
        /// <param name="vertices">Array of vertices to copy to the buffer</param>
        ////////////////////////////////////////////////////////////
        public bool Update(Vertex[] vertices) => Update(vertices, (uint)vertices.Length, 0);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Update a part of the buffer from a <see cref="Vertex"/>[]
        /// </summary>
        /// <remarks>
        /// <para>
        /// <pararef name="offset" /> is specified as the number of vertices to skip
        /// from the beginning of the buffer.
        /// </para>
        /// <para>
        /// If <pararef name="offset" /> is 0 and <paramref name="vertices" />.Length
        /// is equal to the size of the currently created buffer, its whole contents are replaced.
        /// </para>
        /// <para>
        /// If <pararef name="offset" /> is 0 and <paramref name="vertices" />.Length
        /// is greater than the size of the currently created buffer, a new buffer is created
        /// containing the <see cref="Vertex"/> data.
        /// </para>
        /// <para>
        /// If <pararef name="offset" /> is 0 and <paramref name="vertices" />.Length
        /// is less than the size of the currently created buffer, only the first
        /// <paramref name="vertices" />.Length vertices are replaced.
        /// </para>
        /// <para>
        /// If <pararef name="offset" /> is NOT 0 and <pararef name="offset" /> +
        /// <paramref name="vertices" />.Length is greater than the size of the currently
        /// created buffer, the update fails.
        /// </para>
        /// <para>
        /// No additional checks are performed on the size of the <see cref="VertexBuffer"/>.
        /// Passing invalid arguments will lead to undefined behavior.
        /// </para>
        /// </remarks>
        /// <param name="vertices">Array of vertices to copy to the buffer</param>
        /// <param name="offset">Offset in the buffer to copy to</param>
        ////////////////////////////////////////////////////////////
        public bool Update(Vertex[] vertices, uint offset) => Update(vertices, (uint)vertices.Length, offset);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Copy the contents of another <see cref="VertexBuffer"/> into this buffer
        /// </summary>
        /// <param name="other">VertexBuffer whose contents to copy from</param>
        ////////////////////////////////////////////////////////////
        public bool Update(VertexBuffer other) => sfVertexBuffer_updateFromVertexBuffer(CPointer, other.CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Swap the contents of another <see cref="VertexBuffer"/> into this buffer
        /// </summary>
        /// <param name="other">VertexBuffer whose contents to swap with</param>
        ////////////////////////////////////////////////////////////
        public void Swap(VertexBuffer other) => sfVertexBuffer_swap(CPointer, other.CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
        ////////////////////////////////////////////////////////////
        protected override void Destroy(bool disposing) => sfVertexBuffer_destroy(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Draw the <see cref="VertexBuffer"/> to a <see cref="IRenderTarget"/>
        /// </summary>
        /// <param name="target">Render target to draw to</param>
        /// <param name="states">Current render states</param>
        ////////////////////////////////////////////////////////////
        public void Draw(IRenderTarget target, RenderStates states)
        {
            var marshaledStates = states.Marshal();

            if (target is RenderWindow window)
            {
                sfRenderWindow_drawVertexBuffer(window.CPointer, CPointer, ref marshaledStates);
            }
            else if (target is RenderTexture texture)
            {
                sfRenderTexture_drawVertexBuffer(texture.CPointer, CPointer, ref marshaledStates);
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Draw the vertex buffer to a render target
        /// </summary>
        /// <param name="target">Render target to draw to</param>
        /// <param name="firstVertex">Index of the first vertex to render</param>
        /// <param name="vertexCount">Number of vertices to render</param>
        /// <param name="states">Current render states</param>
        ////////////////////////////////////////////////////////////
        public void Draw(IRenderTarget target, uint firstVertex, uint vertexCount, RenderStates states)
        {
            var marshaledStates = states.Marshal();

            if (target is RenderWindow window)
            {
                sfRenderWindow_drawVertexBufferRange(window.CPointer, CPointer, (UIntPtr)firstVertex, (UIntPtr)vertexCount, ref marshaledStates);
            }
            else if (target is RenderTexture texture)
            {
                sfRenderTexture_drawVertexBufferRange(texture.CPointer, CPointer, (UIntPtr)firstVertex, (UIntPtr)vertexCount, ref marshaledStates);
            }
        }

        #region Imports
        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfVertexBuffer_create(uint vertexCount, PrimitiveType type, UsageSpecifier usage);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfVertexBuffer_copy(IntPtr copy);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfVertexBuffer_destroy(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint sfVertexBuffer_getVertexCount(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern unsafe bool sfVertexBuffer_update(IntPtr cPointer, Vertex* vertices, uint vertexCount, uint offset);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfVertexBuffer_updateFromVertexBuffer(IntPtr cPointer, IntPtr other);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfVertexBuffer_swap(IntPtr cPointer, IntPtr other);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern uint sfVertexBuffer_getNativeHandle(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfVertexBuffer_setPrimitiveType(IntPtr cPointer, PrimitiveType primitiveType);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern PrimitiveType sfVertexBuffer_getPrimitiveType(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfVertexBuffer_setUsage(IntPtr cPointer, UsageSpecifier usageType);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern UsageSpecifier sfVertexBuffer_getUsage(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfVertexBuffer_bind(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfVertexBuffer_isAvailable();

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_drawVertexBuffer(IntPtr cPointer, IntPtr vertexArray, ref RenderStates.MarshalData states);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_drawVertexBufferRange(IntPtr cPointer, IntPtr vertexBuffer, UIntPtr firstVertex, UIntPtr vertexCount, ref RenderStates.MarshalData states);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderTexture_drawVertexBuffer(IntPtr cPointer, IntPtr vertexBuffer, ref RenderStates.MarshalData states);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderTexture_drawVertexBufferRange(IntPtr cPointer, IntPtr vertexBuffer, UIntPtr firstVertex, UIntPtr vertexCount, ref RenderStates.MarshalData states);
        #endregion
    }
}
