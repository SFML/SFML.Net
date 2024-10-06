namespace SFML.Graphics
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Types of primitives that a <see cref="VertexArray"/> or <see cref="VertexBuffer"/> can render.
    /// </summary>
    ///
    /// <remarks>
    /// Points and lines have no area, therefore their thickness
    /// will always be 1 pixel, regardless the current transform
    /// and view.
    /// </remarks>
    ////////////////////////////////////////////////////////////
    public enum PrimitiveType
    {
        /// <summary>Individual Points</summary>
        Points,

        /// <summary>Individual, Disconnected Lines; each pair of points forms a line</summary>
        Lines,

        /// <summary>Connected Lines; each point starts at the previous point to form a line</summary>
        LineStrip,

        /// <summary>Individual, Disconnected Triangles</summary>
        Triangles,

        /// <summary>Connected Triangles; each point uses the two previous points to form a triangle</summary>
        TriangleStrip,

        /// <summary>Connected Triangles; each point uses the first point and the previous point to form a triangle</summary>
        TriangleFan
    }
}
