using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using SFML.System;
using SFML.Window;

namespace SFML.Graphics
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Wrapper for pixel shaders
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class Shader : ObjectBase
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Special type that can be passed to SetParameter,
        /// and that represents the texture of the object being drawn
        /// </summary>
        ////////////////////////////////////////////////////////////
        public class CurrentTextureType { }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Special value that can be passed to SetParameter,
        /// and that represents the texture of the object being drawn
        /// </summary>
        ////////////////////////////////////////////////////////////
        public static readonly CurrentTextureType CurrentTexture = null;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Load the vertex, geometry and fragment shaders from files
        /// </summary>
        /// <remarks>
        /// This function loads the vertex, geometry and fragment
        /// shaders. Pass NULL if you don't want to load
        /// a specific shader.
        /// The sources must be text files containing valid shaders
        /// in GLSL language. GLSL is a C-like language dedicated to
        /// OpenGL shaders; you'll probably need to read a good documentation
        /// for it before writing your own shaders.
        /// </remarks>
        /// <param name="vertexShaderFilename">Path of the vertex shader file to load, or null to skip this shader</param>
        /// <param name="geometryShaderFilename">Path of the geometry shader file to load, or null to skip this shader</param>
        /// <param name="fragmentShaderFilename">Path of the fragment shader file to load, or null to skip this shader</param>
        /// <exception cref="LoadingFailedException" />
        ////////////////////////////////////////////////////////////
        public Shader(string vertexShaderFilename, string geometryShaderFilename, string fragmentShaderFilename) :
            base(sfShader_createFromFile(vertexShaderFilename, geometryShaderFilename, fragmentShaderFilename))
        {
            if (CPointer == IntPtr.Zero)
            {
                throw new LoadingFailedException("shader", vertexShaderFilename + " " + fragmentShaderFilename);
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Load the vertex, geometry and fragment shaders from custom streams
        /// </summary>
        ///
        /// <remarks>
        /// This function loads the vertex, geometry and fragment
        /// shaders. Pass NULL if you don't want to load
        /// a specific shader.
        /// The sources must be valid shaders in GLSL language. GLSL is
        /// a C-like language dedicated to OpenGL shaders; you'll
        /// probably need to read a good documentation for it before
        /// writing your own shaders.
        /// </remarks>
        /// <param name="vertexShaderStream">Source stream to read the vertex shader from, or null to skip this shader</param>
        /// <param name="geometryShaderStream">Source stream to read the geometry shader from, or null to skip this shader</param>
        /// <param name="fragmentShaderStream">Source stream to read the fragment shader from, or null to skip this shader</param>
        /// <exception cref="LoadingFailedException" />
        ////////////////////////////////////////////////////////////
        public Shader(Stream vertexShaderStream, Stream geometryShaderStream, Stream fragmentShaderStream) :
            base(IntPtr.Zero)
        {
            // using these funky conditional operators because StreamAdaptor doesn't have some method for dealing with
            // its constructor argument being null
            using (StreamAdaptor vertexAdaptor = vertexShaderStream != null ? new StreamAdaptor(vertexShaderStream) : null,
                                 geometryAdaptor = geometryShaderStream != null ? new StreamAdaptor(geometryShaderStream) : null,
                                 fragmentAdaptor = fragmentShaderStream != null ? new StreamAdaptor(fragmentShaderStream) : null)
            {
                CPointer = sfShader_createFromStream(vertexAdaptor != null ? vertexAdaptor.InputStreamPtr : IntPtr.Zero,
                                                     geometryAdaptor != null ? geometryAdaptor.InputStreamPtr : IntPtr.Zero,
                                                     fragmentAdaptor != null ? fragmentAdaptor.InputStreamPtr : IntPtr.Zero);
            }

            if (CPointer == IntPtr.Zero)
            {
                throw new LoadingFailedException("shader");
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the underlying OpenGL handle of the shader.
        /// </summary>
        /// <remarks>
        /// You shouldn't need to use this handle, unless you have
        /// very specific stuff to implement that SFML doesn't support,
        /// or implement a temporary workaround until a bug is fixed.
        /// </remarks>
        ////////////////////////////////////////////////////////////
        public uint NativeHandle
        {
            get { return sfShader_getNativeHandle(CPointer); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Load the vertex, geometry and fragment shaders from source code in memory
        /// </summary>
        /// <remarks>
        /// This function loads the vertex, geometry and fragment
        /// shaders. Pass NULL if you don't want to load
        /// a specific shader.
        /// The sources must be valid shaders in GLSL language. GLSL is
        /// a C-like language dedicated to OpenGL shaders; you'll
        /// probably need to read a good documentation for it before
        /// writing your own shaders.
        /// </remarks>
        /// <param name="vertexShader">String containing the source code of the vertex shader</param>
        /// <param name="geometryShader">String containing the source code of the geometry shader</param>
        /// <param name="fragmentShader">String containing the source code of the fragment shader</param>
        /// <returns>New shader instance</returns>
        /// <exception cref="LoadingFailedException" />
        ////////////////////////////////////////////////////////////
        public static Shader FromString(string vertexShader, string geometryShader, string fragmentShader)
        {
            IntPtr ptr = sfShader_createFromMemory(vertexShader, geometryShader, fragmentShader);
            if (ptr == IntPtr.Zero)
            {
                throw new LoadingFailedException("shader");
            }

            return new Shader(ptr);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify value for <c>float</c> uniform
        /// </summary>
        /// <param name="name">Name of the uniform variable in GLSL</param>
        /// <param name="x">Value of the float scalar</param>
        ////////////////////////////////////////////////////////////
        public void SetUniform(string name, float x)
        {
            sfShader_setFloatUniform(CPointer, name, x);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify value for <c>vec2</c> uniform
        /// </summary>
        /// <param name="name">Name of the uniform variable in GLSL</param>
        /// <param name="vector">Value of the vec2 vector</param>
        ////////////////////////////////////////////////////////////
        public void SetUniform(string name, Glsl.Vec2 vector)
        {
            sfShader_setVec2Uniform(CPointer, name, vector);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify value for <c>vec3</c> uniform
        /// </summary>
        /// <param name="name">Name of the uniform variable in GLSL</param>
        /// <param name="vector">Value of the vec3 vector</param>
        ////////////////////////////////////////////////////////////
        public void SetUniform(string name, Glsl.Vec3 vector)
        {
            sfShader_setVec3Uniform(CPointer, name, vector);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify value for <c>vec4</c> uniform
        /// </summary>
        /// <param name="name">Name of the uniform variable in GLSL</param>
        /// <param name="vector">Value of the vec4 vector</param>
        ////////////////////////////////////////////////////////////
        public void SetUniform(string name, Glsl.Vec4 vector)
        {
            sfShader_setVec4Uniform(CPointer, name, vector);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify value for <c>int</c> uniform
        /// </summary>
        /// <param name="name">Name of the uniform variable in GLSL</param>
        /// <param name="x">Value of the int scalar</param>
        ////////////////////////////////////////////////////////////
        public void SetUniform(string name, int x)
        {
            sfShader_setIntUniform(CPointer, name, x);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify value for <c>ivec2</c> uniform
        /// </summary>
        /// <param name="name">Name of the uniform variable in GLSL</param>
        /// <param name="vector">Value of the ivec2 vector</param>
        ////////////////////////////////////////////////////////////
        public void SetUniform(string name, Glsl.Ivec2 vector)
        {
            sfShader_setIvec2Uniform(CPointer, name, vector);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify value for <c>ivec3</c> uniform
        /// </summary>
        /// <param name="name">Name of the uniform variable in GLSL</param>
        /// <param name="vector">Value of the ivec3 vector</param>
        ////////////////////////////////////////////////////////////
        public void SetUniform(string name, Glsl.Ivec3 vector)
        {
            sfShader_setIvec3Uniform(CPointer, name, vector);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify value for <c>ivec4</c> uniform
        /// </summary>
        /// <param name="name">Name of the uniform variable in GLSL</param>
        /// <param name="vector">Value of the ivec4 vector</param>
        ////////////////////////////////////////////////////////////
        public void SetUniform(string name, Glsl.Ivec4 vector)
        {
            sfShader_setIvec4Uniform(CPointer, name, vector);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify value for <c>bool</c> uniform
        /// </summary>
        /// <param name="name">Name of the uniform variable in GLSL</param>
        /// <param name="x">Value of the bool scalar</param>
        ////////////////////////////////////////////////////////////
        public void SetUniform(string name, bool x)
        {
            sfShader_setBoolUniform(CPointer, name, x);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify value for <c>bvec2</c> uniform
        /// </summary>
        /// <param name="name">Name of the uniform variable in GLSL</param>
        /// <param name="vector">Value of the bvec2 vector</param>
        ////////////////////////////////////////////////////////////
        public void SetUniform(string name, Glsl.Bvec2 vector)
        {
            sfShader_setBvec2Uniform(CPointer, name, vector);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify value for <c>bvec3</c> uniform
        /// </summary>
        /// <param name="name">Name of the uniform variable in GLSL</param>
        /// <param name="vector">Value of the bvec3 vector</param>
        ////////////////////////////////////////////////////////////
        public void SetUniform(string name, Glsl.Bvec3 vector)
        {
            sfShader_setBvec3Uniform(CPointer, name, vector);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify value for <c>bvec4</c> uniform
        /// </summary>
        /// <param name="name">Name of the uniform variable in GLSL</param>
        /// <param name="vector">Value of the bvec4 vector</param>
        ////////////////////////////////////////////////////////////
        public void SetUniform(string name, Glsl.Bvec4 vector)
        {
            sfShader_setBvec4Uniform(CPointer, name, vector);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify value for <c>mat3</c> uniform
        /// </summary>
        /// <param name="name">Name of the uniform variable in GLSL</param>
        /// <param name="matrix">Value of the mat3 matrix</param>
        ////////////////////////////////////////////////////////////
        public void SetUniform(string name, Glsl.Mat3 matrix)
        {
            sfShader_setMat3Uniform(CPointer, name, matrix);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify value for <c>mat4</c> uniform
        /// </summary>
        /// <param name="name">Name of the uniform variable in GLSL</param>
        /// <param name="matrix">Value of the mat4 matrix</param>
        ////////////////////////////////////////////////////////////
        public void SetUniform(string name, Glsl.Mat4 matrix)
        {
            sfShader_setMat4Uniform(CPointer, name, matrix);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify a texture as <c>sampler2D</c> uniform
        /// </summary>
        ///
        /// <remarks>
        /// <para>name is the name of the variable to change in the shader.
        /// The corresponding parameter in the shader must be a 2D texture
        /// (<c>sampler2D</c> GLSL type).</para>
        ///
        /// <para>Example:
        /// <code>
        /// uniform sampler2D the_texture; // this is the variable in the shader
        /// </code>
        /// <code>
        /// sf::Texture texture;
        /// ...
        /// shader.setUniform("the_texture", texture);
        /// </code>
        /// It is important to note that <paramref name="texture"/> must remain alive as long
        /// as the shader uses it, no copy is made internally.</para>
        ///
        /// <para>To use the texture of the object being drawn, which cannot be
        /// known in advance, you can pass the special value
        /// Shader.CurrentTexture:
        /// <code>
        /// shader.setUniform("the_texture", Shader.CurrentTexture);
        /// </code>
        /// </para>
        /// </remarks>
        /// 
        /// <param name="name">Name of the texture in the shader</param>
        /// <param name="texture">Texture to assign</param>
        ///
        ////////////////////////////////////////////////////////////
        public void SetUniform(string name, Texture texture)
        {
            // Keep a reference to the Texture so it doesn't get GC'd
            myTextures[name] = texture;
            sfShader_setTextureUniform(CPointer, name, texture.CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify current texture as \p sampler2D uniform
        /// </summary>
        ///
        /// <remarks>
        /// <para>This overload maps a shader texture variable to the
        /// texture of the object being drawn, which cannot be
        /// known in advance. The second argument must be
        /// <see cref="CurrentTexture"/>.
        /// The corresponding parameter in the shader must be a 2D texture
        /// (<c>sampler2D</c> GLSL type).</para>
        ///
        /// <para>Example:
        /// <code>
        /// uniform sampler2D current; // this is the variable in the shader
        /// </code>
        /// <code>
        /// shader.setUniform("current", Shader.CurrentTexture);
        /// </code>
        /// </para>
        /// </remarks>
        /// 
        /// <param name="name">Name of the texture in the shader</param>
        /// <param name="current"/>
        ////////////////////////////////////////////////////////////
        public void SetUniform(string name, CurrentTextureType current)
        {
            sfShader_setCurrentTextureUniform(CPointer, name);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify values for <c>float[]</c> array uniforms
        /// </summary>
        /// <param name="name">Name of the uniform variable in GLSL</param>
        /// <param name="array">array of <c>float</c> values</param>
        ////////////////////////////////////////////////////////////
        public unsafe void SetUniformArray(string name, float[] array)
        {
            fixed (float* data = array)
            {
                sfShader_setFloatUniformArray(CPointer, name, data, (uint)array.Length);
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify values for <c>vec2[]</c> array uniforms
        /// </summary>
        /// <param name="name">Name of the uniform variable in GLSL</param>
        /// <param name="array">array of <c>vec2</c> values</param>
        ////////////////////////////////////////////////////////////
        public unsafe void SetUniformArray(string name, Glsl.Vec2[] array)
        {
            fixed (Glsl.Vec2* data = array)
            {
                sfShader_setVec2UniformArray(CPointer, name, data, (uint)array.Length);
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify values for <c>vec3[]</c> array uniforms
        /// </summary>
        /// <param name="name">Name of the uniform variable in GLSL</param>
        /// <param name="array">array of <c>vec3</c> values</param>
        ////////////////////////////////////////////////////////////
        public unsafe void SetUniformArray(string name, Glsl.Vec3[] array)
        {
            fixed (Glsl.Vec3* data = array)
            {
                sfShader_setVec3UniformArray(CPointer, name, data, (uint)array.Length);
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify values for <c>vec4[]</c> array uniforms
        /// </summary>
        /// <param name="name">Name of the uniform variable in GLSL</param>
        /// <param name="array">array of <c>vec4</c> values</param>
        ////////////////////////////////////////////////////////////
        public unsafe void SetUniformArray(string name, Glsl.Vec4[] array)
        {
            fixed (Glsl.Vec4* data = array)
            {
                sfShader_setVec4UniformArray(CPointer, name, data, (uint)array.Length);
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify values for <c>mat3[]</c> array uniforms
        /// </summary>
        /// <param name="name">Name of the uniform variable in GLSL</param>
        /// <param name="array">array of <c>mat3</c> values</param>
        ////////////////////////////////////////////////////////////
        public unsafe void SetUniformArray(string name, Glsl.Mat3[] array)
        {
            fixed (Glsl.Mat3* data = array)
            {
                sfShader_setMat3UniformArray(CPointer, name, data, (uint)array.Length);
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Specify values for <c>mat4[]</c> array uniforms
        /// </summary>
        /// <param name="name">Name of the uniform variable in GLSL</param>
        /// <param name="array">array of <c>mat4</c> values</param>
        ////////////////////////////////////////////////////////////
        public unsafe void SetUniformArray(string name, Glsl.Mat4[] array)
        {
            fixed (Glsl.Mat4* data = array)
            {
                sfShader_setMat4UniformArray(CPointer, name, data, (uint)array.Length);
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Change a float parameter of the shader
        ///
        /// "name" is the name of the variable to change in the shader.
        /// The corresponding parameter in the shader must be a float
        /// (float GLSL type).
        /// </summary>
        ///
        /// <param name="name">Name of the parameter in the shader</param>
        /// <param name="x">Value to assign</param>
        ///
        ////////////////////////////////////////////////////////////
        [Obsolete("SetParameter is deprecated, please use the corresponding SetUniform")]
        public void SetParameter(string name, float x)
        {
            sfShader_setFloatParameter(CPointer, name, x);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Change a 2-components vector parameter of the shader
        ///
        /// "name" is the name of the variable to change in the shader.
        /// The corresponding parameter in the shader must be a 2x1 vector
        /// (vec2 GLSL type).
        /// </summary>
        /// <param name="name">Name of the parameter in the shader</param>
        /// <param name="x">First component of the value to assign</param>
        /// <param name="y">Second component of the value to assign</param>
        ////////////////////////////////////////////////////////////
        [Obsolete("SetParameter is deprecated, please use the corresponding SetUniform")]
        public void SetParameter(string name, float x, float y)
        {
            sfShader_setFloat2Parameter(CPointer, name, x, y);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Change a 3-components vector parameter of the shader
        ///
        /// "name" is the name of the variable to change in the shader.
        /// The corresponding parameter in the shader must be a 3x1 vector
        /// (vec3 GLSL type).
        /// </summary>
        /// <param name="name">Name of the parameter in the shader</param>
        /// <param name="x">First component of the value to assign</param>
        /// <param name="y">Second component of the value to assign</param>
        /// <param name="z">Third component of the value to assign</param>
        ////////////////////////////////////////////////////////////
        [Obsolete("SetParameter is deprecated, please use the corresponding SetUniform")]
        public void SetParameter(string name, float x, float y, float z)
        {
            sfShader_setFloat3Parameter(CPointer, name, x, y, z);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Change a 4-components vector parameter of the shader
        ///
        /// "name" is the name of the variable to change in the shader.
        /// The corresponding parameter in the shader must be a 4x1 vector
        /// (vec4 GLSL type).
        /// </summary>
        /// <param name="name">Name of the parameter in the shader</param>
        /// <param name="x">First component of the value to assign</param>
        /// <param name="y">Second component of the value to assign</param>
        /// <param name="z">Third component of the value to assign</param>
        /// <param name="w">Fourth component of the value to assign</param>
        ////////////////////////////////////////////////////////////
        [Obsolete("SetParameter is deprecated, please use the corresponding SetUniform")]
        public void SetParameter(string name, float x, float y, float z, float w)
        {
            sfShader_setFloat4Parameter(CPointer, name, x, y, z, w);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Change a 2-components vector parameter of the shader
        ///
        /// "name" is the name of the variable to change in the shader.
        /// The corresponding parameter in the shader must be a 2x1 vector
        /// (vec2 GLSL type).
        /// </summary>
        /// <param name="name">Name of the parameter in the shader</param>
        /// <param name="vector">Vector to assign</param>
        ////////////////////////////////////////////////////////////
        [Obsolete("SetParameter is deprecated, please use the corresponding SetUniform")]
        public void SetParameter(string name, Vector2f vector)
        {
            SetParameter(name, vector.X, vector.Y);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Change a color parameter of the shader
        ///
        /// "name" is the name of the variable to change in the shader.
        /// The corresponding parameter in the shader must be a 4x1 vector
        /// (vec4 GLSL type).
        /// </summary>
        /// <param name="name">Name of the parameter in the shader</param>
        /// <param name="color">Color to assign</param>
        ////////////////////////////////////////////////////////////
        [Obsolete("SetParameter is deprecated, please use the corresponding SetUniform")]
        public void SetParameter(string name, Color color)
        {
            sfShader_setColorParameter(CPointer, name, color);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Change a matrix parameter of the shader
        ///
        /// "name" is the name of the variable to change in the shader.
        /// The corresponding parameter in the shader must be a 4x4 matrix
        /// (mat4 GLSL type).
        /// </summary>
        /// <param name="name">Name of the parameter in the shader</param>
        /// <param name="transform">Transform to assign</param>
        ////////////////////////////////////////////////////////////
        [Obsolete("SetParameter is deprecated, please use the corresponding SetUniform")]
        public void SetParameter(string name, Transform transform)
        {
            sfShader_setTransformParameter(CPointer, name, transform);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Change a texture parameter of the shader
        ///
        /// "name" is the name of the variable to change in the shader.
        /// The corresponding parameter in the shader must be a 2D texture
        /// (sampler2D GLSL type).
        ///
        /// It is important to note that \a texture must remain alive as long
        /// as the shader uses it, no copy is made internally.
        ///
        /// To use the texture of the object being draw, which cannot be
        /// known in advance, you can pass the special value
        /// Shader.CurrentTexture.
        /// </summary>
        /// <param name="name">Name of the texture in the shader</param>
        /// <param name="texture">Texture to assign</param>
        ////////////////////////////////////////////////////////////
        [Obsolete("SetParameter is deprecated, please use the corresponding SetUniform")]
        public void SetParameter(string name, Texture texture)
        {
            // Keep a reference to the Texture so it doesn't get GC'd
            myTextures[name] = texture;
            sfShader_setTextureParameter(CPointer, name, texture.CPointer);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Change a texture parameter of the shader
        ///
        /// This overload maps a shader texture variable to the
        /// texture of the object being drawn, which cannot be
        /// known in advance. The second argument must be
        /// sf::Shader::CurrentTexture.
        /// The corresponding parameter in the shader must be a 2D texture
        /// (sampler2D GLSL type).
        /// </summary>
        /// <param name="name">Name of the texture in the shader</param>
        /// <param name="current">Always pass the spacial value Shader.CurrentTexture</param>
        ////////////////////////////////////////////////////////////
        [Obsolete("SetParameter is deprecated, please use the corresponding SetUniform")]
        public void SetParameter(string name, CurrentTextureType current)
        {
            sfShader_setCurrentTextureParameter(CPointer, name);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Bind a shader for rendering
        /// </summary>
        /// <param name="shader">Shader to bind (can be null to use no shader)</param>
        ////////////////////////////////////////////////////////////
        public static void Bind(Shader shader)
        {
            sfShader_bind(shader != null ? shader.CPointer : IntPtr.Zero);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Tell whether or not the system supports shaders.
        /// </summary>
        ///
        /// <remarks>
        /// This property should always be checked before using
        /// the shader features. If it returns false, then
        /// any attempt to use Shader will fail.
        /// </remarks>
        ////////////////////////////////////////////////////////////
        public static bool IsAvailable
        {
            get { return sfShader_isAvailable(); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Tell whether or not the system supports geometry shaders.
        /// </summary>
        /// 
        /// <remarks>
        /// <para>This property should always be checked before using
        /// the geometry shader features. If it returns false, then
        /// any attempt to use geometry shader features will fail.</para>
        /// 
        /// <para>Note: The first call to this function, whether by your
        /// code or SFML will result in a context switch.</para>
        /// </remarks>
        ////////////////////////////////////////////////////////////
        public static bool IsGeometryAvailable
        {
            get { return sfShader_isGeometryAvailable(); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString()
        {
            return "[Shader]";
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Handle the destruction of the object
        /// </summary>
        /// <param name="disposing">Is the GC disposing the object, or is it an explicit call ?</param>
        ////////////////////////////////////////////////////////////
        protected override void Destroy(bool disposing)
        {
            if (!disposing)
            {
                Context.Global.SetActive(true);
            }

            myTextures.Clear();
            sfShader_destroy(CPointer);

            if (!disposing)
            {
                Context.Global.SetActive(false);
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the shader from a pointer
        /// </summary>
        /// <param name="ptr">Pointer to the shader instance</param>
        ////////////////////////////////////////////////////////////
        public Shader(IntPtr ptr) :
            base(ptr)
        {
        }

        // Keeps references to used Textures for GC prevention during use
        private Dictionary<string, Texture> myTextures = new Dictionary<string, Texture>();

        #region Imports
        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfShader_createFromFile(string vertexShaderFilename, string geometryShaderFilename, string fragmentShaderFilename);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfShader_createFromMemory(string vertexShader, string geometryShader, string fragmentShader);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern IntPtr sfShader_createFromStream(IntPtr vertexShaderStream, IntPtr geometryShaderStream, IntPtr fragmentShaderStream);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfShader_destroy(IntPtr shader);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfShader_setFloatUniform(IntPtr shader, string name, float x);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfShader_setVec2Uniform(IntPtr shader, string name, Glsl.Vec2 vector);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfShader_setVec3Uniform(IntPtr shader, string name, Glsl.Vec3 vector);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfShader_setVec4Uniform(IntPtr shader, string name, Glsl.Vec4 vector);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfShader_setIntUniform(IntPtr shader, string name, int x);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfShader_setIvec2Uniform(IntPtr shader, string name, Glsl.Ivec2 vector);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfShader_setIvec3Uniform(IntPtr shader, string name, Glsl.Ivec3 vector);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfShader_setIvec4Uniform(IntPtr shader, string name, Glsl.Ivec4 vector);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfShader_setBoolUniform(IntPtr shader, string name, bool x);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfShader_setBvec2Uniform(IntPtr shader, string name, Glsl.Bvec2 vector);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfShader_setBvec3Uniform(IntPtr shader, string name, Glsl.Bvec3 vector);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfShader_setBvec4Uniform(IntPtr shader, string name, Glsl.Bvec4 vector);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfShader_setMat3Uniform(IntPtr shader, string name, Glsl.Mat3 matrix);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfShader_setMat4Uniform(IntPtr shader, string name, Glsl.Mat4 matrix);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfShader_setTextureUniform(IntPtr shader, string name, IntPtr texture);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfShader_setCurrentTextureUniform(IntPtr shader, string name);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern unsafe void sfShader_setFloatUniformArray(IntPtr shader, string name, float* data, uint length);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern unsafe void sfShader_setVec2UniformArray(IntPtr shader, string name, Glsl.Vec2* data, uint length);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern unsafe void sfShader_setVec3UniformArray(IntPtr shader, string name, Glsl.Vec3* data, uint length);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern unsafe void sfShader_setVec4UniformArray(IntPtr shader, string name, Glsl.Vec4* data, uint length);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern unsafe void sfShader_setMat3UniformArray(IntPtr shader, string name, Glsl.Mat3* data, uint length);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern unsafe void sfShader_setMat4UniformArray(IntPtr shader, string name, Glsl.Mat4* data, uint length);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, Obsolete]
        static extern void sfShader_setFloatParameter(IntPtr shader, string name, float x);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, Obsolete]
        static extern void sfShader_setFloat2Parameter(IntPtr shader, string name, float x, float y);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, Obsolete]
        static extern void sfShader_setFloat3Parameter(IntPtr shader, string name, float x, float y, float z);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, Obsolete]
        static extern void sfShader_setFloat4Parameter(IntPtr shader, string name, float x, float y, float z, float w);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, Obsolete]
        static extern void sfShader_setColorParameter(IntPtr shader, string name, Color color);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, Obsolete]
        static extern void sfShader_setTransformParameter(IntPtr shader, string name, Transform transform);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, Obsolete]
        static extern void sfShader_setTextureParameter(IntPtr shader, string name, IntPtr texture);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, Obsolete]
        static extern void sfShader_setCurrentTextureParameter(IntPtr shader, string name);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern uint sfShader_getNativeHandle(IntPtr shader);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfShader_bind(IntPtr shader);

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfShader_isAvailable();

        [DllImport(CSFML.graphics, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfShader_isGeometryAvailable();
        #endregion
    }
}
