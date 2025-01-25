using System;
using System.Runtime.InteropServices;
using System.Security;
using SFML.System;

namespace SFML.Graphics
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Base class for textured shapes with outline
    /// </summary>
    ////////////////////////////////////////////////////////////
    public abstract class Shape : Transformable, IDrawable
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Source texture of the shape
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Texture Texture
        {
            get => _texture;
            set { _texture = value; sfShape_setTexture(CPointer, value != null ? value.CPointer : IntPtr.Zero, false); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Sub-rectangle of the texture that the shape will display
        /// </summary>
        ////////////////////////////////////////////////////////////
        public IntRect TextureRect
        {
            get => sfShape_getTextureRect(CPointer);
            set => sfShape_setTextureRect(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Fill color of the shape
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Color FillColor
        {
            get => sfShape_getFillColor(CPointer);
            set => sfShape_setFillColor(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Outline color of the shape
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Color OutlineColor
        {
            get => sfShape_getOutlineColor(CPointer);
            set => sfShape_setOutlineColor(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Thickness of the shape's outline
        /// </summary>
        ////////////////////////////////////////////////////////////
        public float OutlineThickness
        {
            get => sfShape_getOutlineThickness(CPointer);
            set => sfShape_setOutlineThickness(CPointer, value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the total number of points of the shape
        /// </summary>
        /// <returns>The total point count</returns>
        ////////////////////////////////////////////////////////////
        public abstract uint GetPointCount();

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the position of a point
        ///
        /// The returned point is in local coordinates, that is,
        /// the shape's transforms (position, rotation, scale) are
        /// not taken into account.
        /// The result is undefined if index is out of the valid range.
        /// </summary>
        /// <param name="index">Index of the point to get, in range [0 .. PointCount - 1]</param>
        /// <returns>index-th point of the shape</returns>
        ////////////////////////////////////////////////////////////
        public abstract Vector2f GetPoint(uint index);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the geometric center of the shape
        ///
        /// The returned point is in local coordinates, that is,
        /// the shape's transforms (position, rotation, scale) are
        /// not taken into account.
        /// 
        /// </summary>
        /// <returns>The geometric center of the shape</returns>
        ////////////////////////////////////////////////////////////
        public virtual Vector2f GetGeometricCenter() => sfShape_getGeometricCenter(CPointer);

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
        public FloatRect GetLocalBounds() => sfShape_getLocalBounds(CPointer);

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
        public FloatRect GetGlobalBounds() =>
            // we don't use the native getGlobalBounds function,
            // because we override the object's transform
            Transform.TransformRect(GetLocalBounds());

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Draw the shape to a render target
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
                sfRenderWindow_drawShape(window.CPointer, CPointer, ref marshaledStates);
            }
            else if (target is RenderTexture texture)
            {
                sfRenderTexture_drawShape(texture.CPointer, CPointer, ref marshaledStates);
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Default constructor
        /// </summary>
        ////////////////////////////////////////////////////////////
        protected Shape() :
            base(IntPtr.Zero)
        {
            _getPointCountCallback = new GetPointCountCallbackType(InternalGetPointCount);
            _getPointCallback = new GetPointCallbackType(InternalGetPoint);
            CPointer = sfShape_create(_getPointCountCallback, _getPointCallback, IntPtr.Zero);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the shape from another shape
        /// </summary>
        /// <param name="copy">Shape to copy</param>
        ////////////////////////////////////////////////////////////
        public Shape(Shape copy) :
            base(IntPtr.Zero)
        {
            _getPointCountCallback = new GetPointCountCallbackType(InternalGetPointCount);
            _getPointCallback = new GetPointCallbackType(InternalGetPoint);
            CPointer = sfShape_create(_getPointCountCallback, _getPointCallback, IntPtr.Zero);

            Origin = copy.Origin;
            Position = copy.Position;
            Rotation = copy.Rotation;
            Scale = copy.Scale;

            Texture = copy.Texture;
            TextureRect = copy.TextureRect;
            FillColor = copy.FillColor;
            OutlineColor = copy.OutlineColor;
            OutlineThickness = copy.OutlineThickness;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Recompute the internal geometry of the shape.
        ///
        /// This function must be called by the derived class every time
        /// the shape's points change (ie. the result of either
        /// PointCount or GetPoint is different).
        /// </summary>
        ////////////////////////////////////////////////////////////
        protected void Update() => sfShape_update(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
        ////////////////////////////////////////////////////////////
        protected override void Destroy(bool disposing) => sfShape_destroy(CPointer);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Callback passed to the C API
        /// </summary>
        ////////////////////////////////////////////////////////////
        private UIntPtr InternalGetPointCount(IntPtr userData) => (UIntPtr)GetPointCount();

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Callback passed to the C API
        /// </summary>
        ////////////////////////////////////////////////////////////
        private Vector2f InternalGetPoint(UIntPtr index, IntPtr userData) => GetPoint((uint)index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate UIntPtr GetPointCountCallbackType(IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate Vector2f GetPointCallbackType(UIntPtr index, IntPtr userData);

        private readonly GetPointCountCallbackType _getPointCountCallback;
        private readonly GetPointCallbackType _getPointCallback;

        private Texture _texture;

        #region Imports
        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfShape_create(GetPointCountCallbackType getPointCount, GetPointCallbackType getPoint, IntPtr userData);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfShape_destroy(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfShape_setTexture(IntPtr cPointer, IntPtr texture, bool adjustToNewSize);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfShape_setTextureRect(IntPtr cPointer, IntRect rect);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntRect sfShape_getTextureRect(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfShape_setFillColor(IntPtr cPointer, Color color);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Color sfShape_getFillColor(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfShape_setOutlineColor(IntPtr cPointer, Color color);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Color sfShape_getOutlineColor(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfShape_setOutlineThickness(IntPtr cPointer, float thickness);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfShape_getOutlineThickness(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector2f sfShape_getGeometricCenter(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern FloatRect sfShape_getLocalBounds(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfShape_update(IntPtr cPointer);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderWindow_drawShape(IntPtr cPointer, IntPtr shape, ref RenderStates.MarshalData states);

        [DllImport(CSFML.Graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfRenderTexture_drawShape(IntPtr cPointer, IntPtr shape, ref RenderStates.MarshalData states);
        #endregion
    }
}
