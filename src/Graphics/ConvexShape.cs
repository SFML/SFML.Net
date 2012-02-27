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
        /// Specialized shape representing a convex polygon
        /// </summary>
        ////////////////////////////////////////////////////////////
        public class ConvexShape : Transformable, Drawable
        {
            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Default constructor
            /// </summary>
            ////////////////////////////////////////////////////////////
            public ConvexShape() :
                base(sfConvexShape_Create())
            {
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the shape with an initial point count
            /// </summary>
            /// <param name="pointCount">Number of points of the shape</param>
            ////////////////////////////////////////////////////////////
            public ConvexShape(uint pointCount) :
                base(sfConvexShape_Create())
            {
                PointCount = PointCount;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the shape from another shape
            /// </summary>
            /// <param name="copy">Shape to copy</param>
            ////////////////////////////////////////////////////////////
            public ConvexShape(ConvexShape copy) :
                base(sfConvexShape_Copy(copy.CPointer))
            {
                Texture = copy.Texture;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Source texture of the shape
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Texture Texture
            {
                get { return myTexture; }
                set { myTexture = value; sfConvexShape_SetTexture(CPointer, value != null ? value.CPointer : IntPtr.Zero, false); }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Sub-rectangle of the texture that the shape will display
            /// </summary>
            ////////////////////////////////////////////////////////////
            public IntRect TextureRect
            {
                get { return sfConvexShape_GetTextureRect(CPointer); }
                set { sfConvexShape_SetTextureRect(CPointer, value); }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Fill color of the shape
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Color FillColor
            {
                get { return sfConvexShape_GetFillColor(CPointer); }
                set { sfConvexShape_SetFillColor(CPointer, value); }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Outline color of the shape
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Color OutlineColor
            {
                get { return sfConvexShape_GetOutlineColor(CPointer); }
                set { sfConvexShape_SetOutlineColor(CPointer, value); }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Thickness of the shape's outline
            /// </summary>
            ////////////////////////////////////////////////////////////
            public float OutlineThickness
            {
                get { return sfConvexShape_GetOutlineThickness(CPointer); }
                set { sfConvexShape_SetOutlineThickness(CPointer, value); }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// The total number of points of the shape
            /// </summary>
            ////////////////////////////////////////////////////////////
            public uint PointCount
            {
                get { return sfConvexShape_GetPointCount(CPointer); }
                set { sfConvexShape_SetPointCount(CPointer, value); }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Get a point of the shape.
            ///
            /// The result is undefined if index is out of the valid range.
            /// </summary>
            /// <param name="index">Index of the point to get, in range [0 .. PointCount - 1]</param>
            /// <returns>Index-th point of the shape</returns>
            ////////////////////////////////////////////////////////////
            public Vector2f GetPoint(uint index)
            {
                return sfConvexShape_GetPoint(CPointer, index);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Set the position of a point.
            ///
            /// Don't forget that the polygon must remain convex, and
            /// the points need to stay ordered!
            /// PointCount must be set first in order to set the total
            /// number of points. The result is undefined if index is out
            /// of the valid range.
            /// </summary>
            /// <param name="index">Index of the point to change, in range [0 .. PointCount - 1]</param>
            /// <param name="point">New position of the point</param>
            ////////////////////////////////////////////////////////////
            public void SetPoint(uint index, Vector2f point)
            {
                sfConvexShape_SetPoint(CPointer, index, point);
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
                return sfConvexShape_GetLocalBounds(CPointer);
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
                return sfConvexShape_GetGlobalBounds(CPointer);
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
                    sfRenderWindow_DrawConvexShape(((RenderWindow)target).CPointer, CPointer, ref marshaledStates);
                }
                else if (target is RenderTexture)
                {
                    sfRenderTexture_DrawConvexShape(((RenderTexture)target).CPointer, CPointer, ref marshaledStates);
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
                sfConvexShape_Destroy(CPointer);
            }

            private Texture myTexture = null;

            #region Imports

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfConvexShape_Create();

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfConvexShape_Copy(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfConvexShape_Destroy(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfConvexShape_SetTexture(IntPtr CPointer, IntPtr Texture, bool AdjustToNewSize);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfConvexShape_SetTextureRect(IntPtr CPointer, IntRect Rect);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntRect sfConvexShape_GetTextureRect(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfConvexShape_SetFillColor(IntPtr CPointer, Color Color);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Color sfConvexShape_GetFillColor(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfConvexShape_SetOutlineColor(IntPtr CPointer, Color Color);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Color sfConvexShape_GetOutlineColor(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfConvexShape_SetOutlineThickness(IntPtr CPointer, float Thickness);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern float sfConvexShape_GetOutlineThickness(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfConvexShape_SetPointCount(IntPtr CPointer, uint PointCount);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern uint sfConvexShape_GetPointCount(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfConvexShape_SetPoint(IntPtr CPointer, uint Index, Vector2f Point);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern Vector2f sfConvexShape_GetPoint(IntPtr CPointer, uint Index);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern FloatRect sfConvexShape_GetLocalBounds(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern FloatRect sfConvexShape_GetGlobalBounds(IntPtr CPointer);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderWindow_DrawConvexShape(IntPtr CPointer, IntPtr Shape, ref RenderStates.MarshalData states);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfRenderTexture_DrawConvexShape(IntPtr CPointer, IntPtr Shape, ref RenderStates.MarshalData states);

            #endregion
        }
    }
}
