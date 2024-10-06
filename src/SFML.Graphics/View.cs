using System;
using System.Runtime.InteropServices;
using System.Security;
using SFML.System;

namespace SFML.Graphics
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// This class defines a view (position, size, etc.) ;
    /// you can consider it as a 2D camera
    /// </summary>
    /// <remarks>
    /// See also the note on coordinates and undistorted rendering in SFML.Graphics.Transformable.
    /// </remarks>
    ////////////////////////////////////////////////////////////
    public class View : ObjectBase
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Create a default view (1000x1000)
        /// </summary>
        ////////////////////////////////////////////////////////////
        public View() :
            base(sfView_create())
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the view from a rectangle
        /// </summary>
        /// <param name="viewRect">Rectangle defining the position and size of the view</param>
        ////////////////////////////////////////////////////////////
        public View(FloatRect viewRect) :
            base(sfView_createFromRect(viewRect))
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the view from its center and size
        /// </summary>
        /// <param name="center">Center of the view</param>
        /// <param name="size">Size of the view</param>
        ////////////////////////////////////////////////////////////
        public View(Vector2f center, Vector2f size) :
            base(sfView_create())
        {
            Center = center;
            Size = size;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the view from another view
        /// </summary>
        /// <param name="copy">View to copy</param>
        ////////////////////////////////////////////////////////////
        public View(View copy) :
            base(sfView_copy(copy.CPointer))
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Center of the view
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Vector2f Center
        {
            get => sfView_getCenter(CPointer);
            set => sfView_setCenter(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Half-size of the view
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Vector2f Size
        {
            get => sfView_getSize(CPointer);
            set => sfView_setSize(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Rotation of the view, in degrees
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Angle Rotation
        {
            get => Angle.FromDegrees(sfView_getRotation(CPointer));
            set => sfView_setRotation(CPointer, value.Degrees);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Target viewport of the view, defined as a factor of the
        /// size of the target to which the view is applied
        /// </summary>
        ////////////////////////////////////////////////////////////
        public FloatRect Viewport
        {
            get => sfView_getViewport(CPointer);
            set => sfView_setViewport(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// The scissor rectangle, expressed as a factor (between 0 and 1) of
        /// the RenderTarget, specifies the region of the RenderTarget whose
        /// pixels are able to be modified by draw or clear operations.
        /// Any pixels which lie outside of the scissor rectangle will
        /// not be modified by draw or clear operations.
        /// For example, a scissor rectangle which only allows modifications
        /// to the right side of the target would be defined
        /// with View.setScissor(sf::FloatRect({0.5f, 0.f}, {0.5f, 1.f})).
        /// By default, a view has a scissor rectangle which allows
        /// modifications to the entire target. This is equivalent to
        /// disabling the scissor test entirely. Passing the default
        /// scissor rectangle to this function will also disable
        /// scissor testing.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public FloatRect Scissor
        {
            get => sfView_getScissor(CPointer);
            set => sfView_setScissor(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Move the view
        /// </summary>
        /// <param name="offset">Offset to move the view</param>
        ////////////////////////////////////////////////////////////
        public void Move(Vector2f offset) => sfView_move(CPointer, offset);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Rotate the view
        /// </summary>
        /// <param name="angle">Angle of rotation, in degrees</param>
        ////////////////////////////////////////////////////////////
        public void Rotate(float angle) => sfView_rotate(CPointer, angle);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Resize the view rectangle to simulate a zoom / unzoom effect
        /// </summary>
        /// <param name="factor">Zoom factor to apply, relative to the current zoom</param>
        ////////////////////////////////////////////////////////////
        public void Zoom(float factor) => sfView_zoom(CPointer, factor);

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

            return "[View]" +
                   " Center(" + Center + ")" +
                   " Size(" + Size + ")" +
                   " Rotation(" + Rotation + ")" +
                   " Viewport(" + Viewport + ")";
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Internal constructor for other classes which need to manipulate raw views
        /// </summary>
        /// <param name="cPointer">Direct pointer to the view object in the C library</param>
        ////////////////////////////////////////////////////////////
        internal View(IntPtr cPointer) :
            base(cPointer) => _external = true;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
        ////////////////////////////////////////////////////////////
        protected override void Destroy(bool disposing)
        {
            if (!_external)
            {
                sfView_destroy(CPointer);
            }
        }

        private readonly bool _external;

        #region Imports
        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfView_create();

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfView_createFromRect(FloatRect rect);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfView_copy(IntPtr view);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfView_destroy(IntPtr view);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfView_setCenter(IntPtr view, Vector2f center);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfView_setSize(IntPtr view, Vector2f size);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfView_setRotation(IntPtr view, float angle);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfView_setViewport(IntPtr view, FloatRect viewport);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfView_setScissor(IntPtr view, FloatRect viewport);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f sfView_getCenter(IntPtr view);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f sfView_getSize(IntPtr view);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfView_getRotation(IntPtr view);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern FloatRect sfView_getViewport(IntPtr view);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern FloatRect sfView_getScissor(IntPtr view);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfView_move(IntPtr view, Vector2f offset);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfView_rotate(IntPtr view, float angle);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfView_zoom(IntPtr view, float factor);
        #endregion
    }
}
