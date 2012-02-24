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
        /// This class defines a view (position, size, etc.) ;
        /// you can consider it as a 2D camera
        /// </summary>
        ////////////////////////////////////////////////////////////
        public class View : ObjectBase
        {
            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Create a default view (1000x1000)
            /// </summary>
            ////////////////////////////////////////////////////////////
            public View() :
                base(sfView_Create())
            {
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the view from a rectangle
            /// </summary>
            /// <param name="viewRect">Rectangle defining the position and size of the view</param>
            ////////////////////////////////////////////////////////////
            public View(FloatRect viewRect) :
                base(sfView_CreateFromRect(viewRect))
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
                base(sfView_Create())
            {
                this.Center = center;
                this.Size   = size;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Construct the view from another view
            /// </summary>
            /// <param name="copy">View to copy</param>
            ////////////////////////////////////////////////////////////
            public View(View copy) :
                base(sfView_Copy(copy.CPointer))
            {
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Center of the view
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Vector2f Center
            {
                get {return new Vector2f(sfView_GetCenterX(CPointer), sfView_GetCenterY(CPointer));}
                set {sfView_SetCenter(CPointer, value.X, value.Y);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Half-size of the view
            /// </summary>
            ////////////////////////////////////////////////////////////
            public Vector2f Size
            {
                get {return new Vector2f(sfView_GetWidth(CPointer), sfView_GetHeight(CPointer));}
                set {sfView_SetSize(CPointer, value.X, value.Y);}
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Rotation of the view, in degrees
            /// </summary>
            ////////////////////////////////////////////////////////////
            public float Rotation
            {
                get { return sfView_GetRotation(CPointer); }
                set { sfView_SetRotation(CPointer, value); }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Target viewport of the view, defined as a factor of the
            /// size of the target to which the view is applied
            /// </summary>
            ////////////////////////////////////////////////////////////
            public FloatRect Viewport
            {
                get { return sfView_GetViewport(CPointer); }
                set { sfView_SetViewport(CPointer, value); }
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Rebuild the view from a rectangle
            /// </summary>
            /// <param name="rectangle">Rectangle defining the position and size of the view</param>
            ////////////////////////////////////////////////////////////
            public void Reset(FloatRect rectangle)
            {
                sfView_Reset(CPointer, rectangle);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Move the view
            /// </summary>
            /// <param name="offset">Offset to move the view</param>
            ////////////////////////////////////////////////////////////
            public void Move(Vector2f offset)
            {
                sfView_Move(CPointer, offset.X, offset.Y);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Rotate the view
            /// </summary>
            /// <param name="angle">Angle of rotation, in degrees</param>
            ////////////////////////////////////////////////////////////
            public void Rotate(float angle)
            {
                sfView_Rotate(CPointer, angle);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Resize the view rectangle to simulate a zoom / unzoom effect
            /// </summary>
            /// <param name="factor">Zoom factor to apply, relative to the current zoom</param>
            ////////////////////////////////////////////////////////////
            public void Zoom(float factor)
            {
                sfView_Zoom(CPointer, factor);
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Provide a string describing the object
            /// </summary>
            /// <returns>String description of the object</returns>
            ////////////////////////////////////////////////////////////
            public override string ToString()
            {
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
                base(cPointer)
            {
                myExternal = true;
            }

            ////////////////////////////////////////////////////////////
            /// <summary>
            /// Handle the destruction of the object
            /// </summary>
            /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
            ////////////////////////////////////////////////////////////
            protected override void Destroy(bool disposing)
            {
                if (!myExternal)
                    sfView_Destroy(CPointer);
            }

            private bool myExternal = false;
            #region Imports
            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfView_Create();

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfView_CreateFromRect(FloatRect Rect);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern IntPtr sfView_Copy(IntPtr View);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfView_Destroy(IntPtr View);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfView_SetCenter(IntPtr View, float X, float Y);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfView_SetSize(IntPtr View, float Width, float Height);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfView_SetRotation(IntPtr View, float Angle);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfView_SetViewport(IntPtr View, FloatRect Viewport);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfView_Reset(IntPtr View, FloatRect Rectangle);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern float sfView_GetCenterX(IntPtr View);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern float sfView_GetCenterY(IntPtr View);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern float sfView_GetWidth(IntPtr View);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern float sfView_GetHeight(IntPtr View);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern float sfView_GetRotation(IntPtr View);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern FloatRect sfView_GetViewport(IntPtr View);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfView_Move(IntPtr View, float OffsetX, float OffsetY);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfView_Rotate(IntPtr View, float Angle);

            [DllImport("csfml-graphics-2", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
            static extern void sfView_Zoom(IntPtr View, float Factor);

            #endregion
        }
    }
}
