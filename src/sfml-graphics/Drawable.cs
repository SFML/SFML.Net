namespace SFML.Graphics
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Interface for every object that can be drawn to a render window
    /// </summary>
    ////////////////////////////////////////////////////////////
    public interface Drawable
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Draw the object to a render target
        ///
        /// This is a function that has to be implemented by the
        /// derived class to define how the drawable should be drawn.
        /// </summary>
        /// <param name="target">Render target to draw to</param>
        /// <param name="states">Current render states</param>
        ////////////////////////////////////////////////////////////
        void Draw(RenderTarget target, RenderStates states);
    }
}
