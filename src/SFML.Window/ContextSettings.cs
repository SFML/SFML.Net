using System;
using System.Runtime.InteropServices;

namespace SFML.Window
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Structure defining the creation settings of OpenGL contexts
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct ContextSettings
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Enumeration of the context attribute flags
        /// </summary>
        ////////////////////////////////////////////////////////////
        [Flags]
        public enum Attribute
        {
            /// <summary>Non-debug, compatibility context (this and the core attribute are mutually exclusive)</summary>
            Default = 0,

            /// <summary>Core attribute</summary>
            Core = 1 << 0,

            /// <summary>Debug attribute</summary>
            Debug = 1 << 2
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the settings from depth / stencil bits
        /// </summary>
        /// <param name="depthBits">Depth buffer bits</param>
        /// <param name="stencilBits">Stencil buffer bits</param>
        ////////////////////////////////////////////////////////////
        public ContextSettings(uint depthBits, uint stencilBits) :
            this(depthBits, stencilBits, 0)
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the settings from depth / stencil bits and antialiasing level
        /// </summary>
        /// <param name="depthBits">Depth buffer bits</param>
        /// <param name="stencilBits">Stencil buffer bits</param>
        /// <param name="antialiasingLevel">Antialiasing level</param>
        ////////////////////////////////////////////////////////////
        public ContextSettings(uint depthBits, uint stencilBits, uint antialiasingLevel) :
            this(depthBits, stencilBits, antialiasingLevel, 2, 0, Attribute.Default, false)
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the settings from depth / stencil bits and antialiasing level
        /// </summary>
        /// <param name="depthBits">Depth buffer bits</param>
        /// <param name="stencilBits">Stencil buffer bits</param>
        /// <param name="antialiasingLevel">Antialiasing level</param>
        /// <param name="majorVersion">Major number of the context version</param>
        /// <param name="minorVersion">Minor number of the context version</param>
        /// <param name="attributes">Attribute flags of the context</param>
        /// <param name="sRgbCapable">sRGB capability of the context</param>
        ////////////////////////////////////////////////////////////
        public ContextSettings(uint depthBits, uint stencilBits, uint antialiasingLevel, uint majorVersion, uint minorVersion, Attribute attributes, bool sRgbCapable)
        {
            DepthBits = depthBits;
            StencilBits = stencilBits;
            AntialiasingLevel = antialiasingLevel;
            MajorVersion = majorVersion;
            MinorVersion = minorVersion;
            AttributeFlags = attributes;
            _SRgbCapable = sRgbCapable ? 1 : 0;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString()
        {
            return "[ContextSettings]" +
                   " DepthBits(" + DepthBits + ")" +
                   " StencilBits(" + StencilBits + ")" +
                   " AntialiasingLevel(" + AntialiasingLevel + ")" +
                   " MajorVersion(" + MajorVersion + ")" +
                   " MinorVersion(" + MinorVersion + ")" +
                   " AttributeFlags(" + AttributeFlags + ")" +
                   " SRgbCapable(" + SRgbCapable + ")";
        }

        /// <summary>Depth buffer bits (0 is disabled)</summary>
        public uint DepthBits;

        /// <summary>Stencil buffer bits (0 is disabled)</summary>
        public uint StencilBits;

        /// <summary>Antialiasing level (0 is disabled)</summary>
        public uint AntialiasingLevel;

        /// <summary>Major number of the context version</summary>
        public uint MajorVersion;

        /// <summary>Minor number of the context version</summary>
        public uint MinorVersion;

        /// <summary>The attribute flags to create the context with</summary>
        public Attribute AttributeFlags;

        /// <summary>Internal Representation</summary>
        private int _SRgbCapable;

        /// <summary>Whether the context framebuffer is sRGB capable</summary>
        public bool SRgbCapable
        {
            get => _SRgbCapable == 1;
            set { _SRgbCapable = value ? 1 : 0; }
        }
    }
}
