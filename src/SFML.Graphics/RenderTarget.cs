using SFML.System;

namespace SFML.Graphics
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Abstract base class for render targets (renderwindow, renderimage)
    /// </summary>
    ////////////////////////////////////////////////////////////
    public interface RenderTarget
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Size of the rendering region of the target
        /// </summary>
        ////////////////////////////////////////////////////////////
        Vector2u Size { get; }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Default view of the target
        /// </summary>
        ////////////////////////////////////////////////////////////
        View DefaultView { get; }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Return the current active view
        /// </summary>
        /// <returns>The current view</returns>
        ////////////////////////////////////////////////////////////
        View GetView();

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Change the current active view
        /// </summary>
        /// <param name="view">New view</param>
        ////////////////////////////////////////////////////////////
        void SetView(View view);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the viewport of a view applied to this target
        /// </summary>
        /// <param name="view">Target view</param>
        /// <returns>Viewport rectangle, expressed in pixels in the current target</returns>
        ////////////////////////////////////////////////////////////
        IntRect GetViewport(View view);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Convert a point from target coordinates to world
        /// coordinates, using the current view
        ///
        /// This function is an overload of the MapPixelToCoords
        /// function that implicitly uses the current view.
        /// It is equivalent to:
        /// target.MapPixelToCoords(point, target.GetView());
        /// </summary>
        /// <param name="point">Pixel to convert</param>
        /// <returns>The converted point, in "world" coordinates</returns>
        ////////////////////////////////////////////////////////////
        Vector2f MapPixelToCoords(Vector2i point);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Convert a point from target coordinates to world coordinates
        ///
        /// This function finds the 2D position that matches the
        /// given pixel of the render-target. In other words, it does
        /// the inverse of what the graphics card does, to find the
        /// initial position of a rendered pixel.
        ///
        /// Initially, both coordinate systems (world units and target pixels)
        /// match perfectly. But if you define a custom view or resize your
        /// render-target, this assertion is not true anymore, ie. a point
        /// located at (10, 50) in your render-target may map to the point
        /// (150, 75) in your 2D world -- if the view is translated by (140, 25).
        ///
        /// For render-windows, this function is typically used to find
        /// which point (or object) is located below the mouse cursor.
        ///
        /// This version uses a custom view for calculations, see the other
        /// overload of the function if you want to use the current view of the
        /// render-target.
        /// </summary>
        /// <param name="point">Pixel to convert</param>
        /// <param name="view">The view to use for converting the point</param>
        /// <returns>The converted point, in "world" coordinates</returns>
        ////////////////////////////////////////////////////////////
        Vector2f MapPixelToCoords(Vector2i point, View view);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Convert a point from world coordinates to target
        /// coordinates, using the current view
        ///
        /// This function is an overload of the mapCoordsToPixel
        /// function that implicitly uses the current view.
        /// It is equivalent to:
        /// target.MapCoordsToPixel(point, target.GetView());
        /// </summary>
        /// <param name="point">Point to convert</param>
        /// <returns>The converted point, in target coordinates (pixels)</returns>
        ////////////////////////////////////////////////////////////
        Vector2i MapCoordsToPixel(Vector2f point);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Convert a point from world coordinates to target coordinates
        ///
        /// This function finds the pixel of the render-target that matches
        /// the given 2D point. In other words, it goes through the same process
        /// as the graphics card, to compute the final position of a rendered point.
        ///
        /// Initially, both coordinate systems (world units and target pixels)
        /// match perfectly. But if you define a custom view or resize your
        /// render-target, this assertion is not true anymore, ie. a point
        /// located at (150, 75) in your 2D world may map to the pixel
        /// (10, 50) of your render-target -- if the view is translated by (140, 25).
        ///
        /// This version uses a custom view for calculations, see the other
        /// overload of the function if you want to use the current view of the
        /// render-target.
        /// </summary>
        /// <param name="point">Point to convert</param>
        /// <param name="view">The view to use for converting the point</param>
        /// <returns>The converted point, in target coordinates (pixels)</returns>
        ////////////////////////////////////////////////////////////
        Vector2i MapCoordsToPixel(Vector2f point, View view);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Clear the entire target with black color
        /// </summary>
        ////////////////////////////////////////////////////////////
        void Clear();

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Clear the entire target with a single color
        /// </summary>
        /// <param name="color">Color to use to clear the window</param>
        ////////////////////////////////////////////////////////////
        void Clear(Color color);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Draw a drawable object to the render-target, with default render states
        /// </summary>
        /// <param name="drawable">Object to draw</param>
        ////////////////////////////////////////////////////////////
        void Draw(Drawable drawable);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Draw a drawable object to the render-target
        /// </summary>
        /// <param name="drawable">Object to draw</param>
        /// <param name="states">Render states to use for drawing</param>
        ////////////////////////////////////////////////////////////
        void Draw(Drawable drawable, RenderStates states);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Draw primitives defined by an array of vertices, with default render states
        /// </summary>
        /// <param name="vertices">Array of vertices to draw</param>
        /// <param name="type">Type of primitives to draw</param>
        ////////////////////////////////////////////////////////////
        void Draw(Vertex[] vertices, PrimitiveType type);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Draw primitives defined by an array of vertices
        /// </summary>
        /// <param name="vertices">Array of vertices to draw</param>
        /// <param name="type">Type of primitives to draw</param>
        /// <param name="states">Render states to use for drawing</param>
        ////////////////////////////////////////////////////////////
        void Draw(Vertex[] vertices, PrimitiveType type, RenderStates states);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Draw primitives defined by a sub-array of vertices, with default render states
        /// </summary>
        /// <param name="vertices">Array of vertices to draw</param>
        /// <param name="start">Index of the first vertex to draw in the array</param>
        /// <param name="count">Number of vertices to draw</param>
        /// <param name="type">Type of primitives to draw</param>
        ////////////////////////////////////////////////////////////
        void Draw(Vertex[] vertices, uint start, uint count, PrimitiveType type);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Draw primitives defined by a sub-array of vertices
        /// </summary>
        /// <param name="vertices">Pointer to the vertices</param>
        /// <param name="start">Index of the first vertex to use in the array</param>
        /// <param name="count">Number of vertices to draw</param>
        /// <param name="type">Type of primitives to draw</param>
        /// <param name="states">Render states to use for drawing</param>
        ////////////////////////////////////////////////////////////
        void Draw(Vertex[] vertices, uint start, uint count, PrimitiveType type, RenderStates states);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Save the current OpenGL render states and matrices.
        ///
        /// This function can be used when you mix SFML drawing
        /// and direct OpenGL rendering. Combined with PopGLStates,
        /// it ensures that:
        /// \li SFML's internal states are not messed up by your OpenGL code
        /// \li your OpenGL states are not modified by a call to a SFML function
        ///
        /// More specifically, it must be used around code that
        /// calls Draw functions. Example:
        ///
        /// // OpenGL code here...
        /// window.PushGLStates();
        /// window.Draw(...);
        /// window.Draw(...);
        /// window.PopGLStates();
        /// // OpenGL code here...
        ///
        /// Note that this function is quite expensive: it saves all the
        /// possible OpenGL states and matrices, even the ones you
        /// don't care about. Therefore it should be used wisely.
        /// It is provided for convenience, but the best results will
        /// be achieved if you handle OpenGL states yourself (because
        /// you know which states have really changed, and need to be
        /// saved and restored). Take a look at the ResetGLStates
        /// function if you do so.
        /// </summary>
        ////////////////////////////////////////////////////////////
        void PushGLStates();

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Restore the previously saved OpenGL render states and matrices.
        ///
        /// See the description of PushGLStates to get a detailed
        /// description of these functions.
        /// </summary>
        ////////////////////////////////////////////////////////////
        void PopGLStates();

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Reset the internal OpenGL states so that the target is ready for drawing.
        ///
        /// This function can be used when you mix SFML drawing
        /// and direct OpenGL rendering, if you choose not to use
        /// PushGLStates/PopGLStates. It makes sure that all OpenGL
        /// states needed by SFML are set, so that subsequent Draw()
        /// calls will work as expected.
        ///
        /// Example:
        ///
        /// // OpenGL code here...
        /// glPushAttrib(...);
        /// window.ResetGLStates();
        /// window.Draw(...);
        /// window.Draw(...);
        /// glPopAttrib(...);
        /// // OpenGL code here...
        /// </summary>
        ////////////////////////////////////////////////////////////
        void ResetGLStates();
    }
}
