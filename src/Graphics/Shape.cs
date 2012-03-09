using System;
using System.Runtime.InteropServices;
using System.Security;
using SFML.Window;

namespace SFML
{
    namespace Graphics
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Base class for textured shapes with outline
        /// </summary>
        ////////////////////////////////////////////////////////////
        public abstract class Shape : Transformable, Drawable
        {
            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Source texture of the shape
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Texture Texture
            {
                get { return myTexture; }
                set { myTexture = value; sfShape_SetTexture(CPointer, value != null ? value.CPointer : IntPtr.Zero, false); }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Sub-rectangle of the texture that the shape will display
            /// </summary>
            ////////////////////////////////////////////////////////////
            public IntRect TextureRect
            {
                get { return sfShape_GetTextureRect(CPointer); }
                set { sfShape_SetTextureRect(CPointer, value); }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Fill color of the shape
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Color FillColor
            {
                get { return sfShape_GetFillColor(CPointer); }
                set { sfShape_SetFillColor(CPointer, value); }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Outline color of the shape
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Color OutlineColor
            {
                get { return sfShape_GetOutlineColor(CPointer); }
                set { sfShape_SetOutlineColor(CPointer, value); }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Thickness of the shape's outline
            /// </summary>
            ////////////////////////////////////////////////////////////
            public float OutlineThickness
            {
                get { return sfShape_GetOutlineThickness(CPointer); }
                set { sfShape_SetOutlineThickness(CPointer, value); }
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
            /// Get a point of the shape.
            ///
            /// The result is undefined if index is out of the valid range.
            /// </summary>
            /// <param name="index">Index of the point to get, in range [0 .. PointCount - 1]</param>
            /// <returns>Index-th point of the shape</returns>
            ////////////////////////////////////////////////////////////
            public abstract Vector2f GetPoint(uint index);

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
                return sfShape_GetLocalBounds(CPointer);
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
                return sfShape_GetGlobalBounds(CPointer);
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
                    sfRenderWindow_DrawShape(((RenderWindow)target).CPointer, CPointer, ref marshaledStates);
                }
                else if (target is RenderTexture)
                {
                    sfRenderTexture_DrawShape(((RenderTexture)target).CPointer, CPointer, ref marshaledStates);
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
                myGetPointCountCallback = new GetPointCountCallbackType(InternalGetPointCount);
                myGetPointCallback = new GetPointCallbackType(InternalGetPoint);
                SetThis(sfShape_Create(myGetPointCountCallback, myGetPointCallback, IntPtr.Zero));
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
                myGetPointCountCallback = new GetPointCountCallbackType(InternalGetPointCount);
                myGetPointCallback = new GetPointCallbackType(InternalGetPoint);
                SetThis(sfShape_Create(myGetPointCountCallback, myGetPointCallback, IntPtr.Zero));

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
            /// This function must be called by the derived class everytime
            /// the shape's points change (ie. the result of either
            /// PointCount or GetPoint is different).
            /// </summary>
            ////////////////////////////////////////////////////////////
            protected void Update()
            {
                sfShape_Update(CPointer);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Handle the destruction of the object
            /// </summary>
            /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
            ////////////////////////////////////////////////////////////
            protected override void Destroy(bool disposing)
            {
                sfShape_Destroy(CPointer);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Callback passed to the C API
            /// </summary>
            ////////////////////////////////////////////////////////////
            private uint InternalGetPointCount(IntPtr userData)
            {
                return GetPointCount();
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Callback passed to the C API
            /// </summary>
            ////////////////////////////////////////////////////////////
            private Vector2f InternalGetPoint(uint index, IntPtr userData)
            {
                return GetPoint(index);
            }

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            private delegate uint GetPointCountCallbackType(IntPtr UserData);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            private delegate Vector2f GetPointCallbackType(uint index, IntPtr UserData);

            private GetPointCountCallbackType myGetPointCountCallback;
            private GetPointCallbackType myGetPointCallback;

            private Texture myTexture = null;

            #region Imports

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfShape_Create(GetPointCountCallbackType getPointCount, GetPointCallbackType getPoint, IntPtr userData);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfShape_Copy(IntPtr Shape);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfShape_Destroy(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfShape_SetTexture(IntPtr CPointer, IntPtr Texture, bool AdjustToNewSize);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfShape_SetTextureRect(IntPtr CPointer, IntRect Rect);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntRect sfShape_GetTextureRect(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfShape_SetFillColor(IntPtr CPointer, Color Color);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Color sfShape_GetFillColor(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfShape_SetOutlineColor(IntPtr CPointer, Color Color);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Color sfShape_GetOutlineColor(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfShape_SetOutlineThickness(IntPtr CPointer, float Thickness);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern float sfShape_GetOutlineThickness(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern FloatRect sfShape_GetLocalBounds(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern FloatRect sfShape_GetGlobalBounds(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfShape_Update(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_DrawShape(IntPtr CPointer, IntPtr Shape, ref RenderStates.MarshalData states);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderTexture_DrawShape(IntPtr CPointer, IntPtr Shape, ref RenderStates.MarshalData states);

            #endregion
        }
    }
}
