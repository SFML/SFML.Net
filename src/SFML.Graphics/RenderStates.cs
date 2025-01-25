using System;
using System.Runtime.InteropServices;

namespace SFML.Graphics
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Define the states used for drawing to a RenderTarget
    /// </summary>
    ////////////////////////////////////////////////////////////
    public struct RenderStates
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct a default set of render states with a custom blend mode
        /// </summary>
        /// <param name="blendMode">Blend mode to use</param>
        ////////////////////////////////////////////////////////////
        public RenderStates(BlendMode blendMode) :
            this(blendMode, StencilMode.Default, Transform.Identity, CoordinateType.Pixels, null, null)
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct a default set of render states with a custom stencil mode
        /// </summary>
        /// <param name="stencilMode">Stencil mode to use</param>
        ////////////////////////////////////////////////////////////
        public RenderStates(StencilMode stencilMode) :
            this(BlendMode.Alpha, stencilMode, Transform.Identity, CoordinateType.Pixels, null, null)
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct a default set of render states with a custom transform
        /// </summary>
        /// <param name="transform">Transform to use</param>
        ////////////////////////////////////////////////////////////
        public RenderStates(Transform transform) :
            this(BlendMode.Alpha, StencilMode.Default, transform, CoordinateType.Pixels, null, null)
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct a default set of render states with a custom texture
        /// </summary>
        /// <param name="texture">Texture to use</param>
        ////////////////////////////////////////////////////////////
        public RenderStates(Texture texture) :
            this(BlendMode.Alpha, StencilMode.Default, Transform.Identity, CoordinateType.Pixels, texture, null)
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct a default set of render states with a custom shader
        /// </summary>
        /// <param name="shader">Shader to use</param>
        ////////////////////////////////////////////////////////////
        public RenderStates(Shader shader) :
            this(BlendMode.Alpha, StencilMode.Default, Transform.Identity, CoordinateType.Pixels, null, shader)
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct a set of render states with all its attributes
        /// </summary>
        /// <param name="blendMode">Blend mode to use</param>
        /// <param name="stencilMode">Stencil mode to use</param>
        /// <param name="transform">Transform to use</param>
        /// <param name="coordinateType">Coordinate type to use</param>
        /// <param name="texture">Texture to use</param>
        /// <param name="shader">Shader to use</param>
        ////////////////////////////////////////////////////////////
        public RenderStates(BlendMode blendMode, StencilMode stencilMode, Transform transform, CoordinateType coordinateType, Texture texture, Shader shader)
        {
            BlendMode = blendMode;
            StencilMode = stencilMode;
            Transform = transform;
            CoordinateType = coordinateType;
            Texture = texture;
            Shader = shader;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="copy">States to copy</param>
        ////////////////////////////////////////////////////////////
        public RenderStates(RenderStates copy)
        {
            BlendMode = copy.BlendMode;
            StencilMode = copy.StencilMode;
            Transform = copy.Transform;
            CoordinateType = copy.CoordinateType;
            Texture = copy.Texture;
            Shader = copy.Shader;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>Special instance holding the default render states</summary>
        ////////////////////////////////////////////////////////////
        public static RenderStates Default => new RenderStates(BlendMode.Alpha, StencilMode.Default, Transform.Identity, CoordinateType.Pixels, null, null);

        /// <summary>Blending mode</summary>
        public BlendMode BlendMode;

        /// <summary>Stencil mode</summary>
        public StencilMode StencilMode;

        /// <summary>Transform</summary>
        public Transform Transform;

        /// <summary>Texture coordinate type</summary>
        public CoordinateType CoordinateType;

        /// <summary>Texture</summary>
        public Texture Texture;

        /// <summary>Shader</summary>
        public Shader Shader;

        // Return a marshalled version of the instance, that can directly be passed to the C API
        internal MarshalData Marshal()
        {
            var data = new MarshalData
            {
                BlendMode = BlendMode,
                StencilMode = StencilMode,
                Transform = Transform,
                CoordinateType = CoordinateType,
                Texture = Texture != null ? Texture.CPointer : IntPtr.Zero,
                Shader = Shader != null ? Shader.CPointer : IntPtr.Zero
            };

            return data;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct MarshalData
        {
            public BlendMode BlendMode;
            public StencilMode StencilMode;
            public Transform Transform;
            public CoordinateType CoordinateType;
            public IntPtr Texture;
            public IntPtr Shader;
        }
    }
}
