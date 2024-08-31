using System;
using System.Runtime.InteropServices;

namespace SFML.Graphics
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Blending modes for drawing
    /// </summary>
    ////////////////////////////////////////////////////////////
    [StructLayout(LayoutKind.Sequential)]
    public struct BlendMode : IEquatable<BlendMode>
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Enumeration of the blending factors
        /// </summary>
        ////////////////////////////////////////////////////////////
        public enum Factor
        {
            /// <summary>(0, 0, 0, 0)</summary>
            Zero,

            /// <summary>(1, 1, 1, 1)</summary>
            One,

            /// <summary>(src.r, src.g, src.b, src.a)</summary>
            SrcColor,

            /// <summary>(1, 1, 1, 1) - (src.r, src.g, src.b, src.a)</summary>
            OneMinusSrcColor,

            /// <summary>(dst.r, dst.g, dst.b, dst.a)</summary>
            DstColor,

            /// <summary>(1, 1, 1, 1) - (dst.r, dst.g, dst.b, dst.a)</summary>
            OneMinusDstColor,

            /// <summary>(src.a, src.a, src.a, src.a)</summary>
            SrcAlpha,

            /// <summary>(1, 1, 1, 1) - (src.a, src.a, src.a, src.a)</summary>
            OneMinusSrcAlpha,

            /// <summary>(dst.a, dst.a, dst.a, dst.a)</summary>
            DstAlpha,

            /// <summary>(1, 1, 1, 1) - (dst.a, dst.a, dst.a, dst.a)</summary>
            OneMinusDstAlpha
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Enumeration of the blending equations
        /// </summary>
        ////////////////////////////////////////////////////////////
        public enum Equation
        {
            /// <summary>Pixel = Src * SrcFactor + Dst * DstFactor</summary>
            Add,

            /// <summary>Pixel = Src * SrcFactor - Dst * DstFactor</summary>
            Subtract,

            /// <summary>Pixel = Dst * DstFactor - Src * SrcFactor</summary>
            ReverseSubtract,

            /// <summary>Pixel = min(Dst, Src)</summary>
            Min,

            /// <summary>Pixel = max(Dst, Src)</summary>
            Max
        }

        /// <summary>Blend source and dest according to dest alpha</summary>
        public static readonly BlendMode Alpha = new BlendMode(Factor.SrcAlpha, Factor.OneMinusSrcAlpha, Equation.Add,
                                                               Factor.One, Factor.OneMinusSrcAlpha, Equation.Add);

        /// <summary>Add source to dest</summary>
        public static readonly BlendMode Add = new BlendMode(Factor.SrcAlpha, Factor.One, Equation.Add,
                                                             Factor.One, Factor.One, Equation.Add);

        /// <summary>Multiply source and dest</summary>
        public static readonly BlendMode Multiply = new BlendMode(Factor.DstColor, Factor.Zero);

        /// <summary>Take minimum between source and dest</summary>
        public static readonly BlendMode Min = new BlendMode(Factor.One, Factor.One, Equation.Min);

        /// <summary>Take maximum between source and dest</summary>
        public static readonly BlendMode Max = new BlendMode(Factor.One, Factor.One, Equation.Max);

        /// <summary>Overwrite dest with source</summary>
        public static readonly BlendMode None = new BlendMode(Factor.One, Factor.Zero);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the blend mode given the factors and equation
        /// </summary>
        /// <param name="sourceFactor">Specifies how to compute the source factor for the color and alpha channels.</param>
        /// <param name="destinationFactor">Specifies how to compute the destination factor for the color and alpha channels.</param>
        ////////////////////////////////////////////////////////////
        public BlendMode(Factor sourceFactor, Factor destinationFactor)
            : this(sourceFactor, destinationFactor, Equation.Add)
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the blend mode given the factors and equation
        /// </summary>
        /// <param name="sourceFactor">Specifies how to compute the source factor for the color and alpha channels.</param>
        /// <param name="destinationFactor">Specifies how to compute the destination factor for the color and alpha channels.</param>
        /// <param name="blendEquation">Specifies how to combine the source and destination colors and alpha.</param>
        ////////////////////////////////////////////////////////////
        public BlendMode(Factor sourceFactor, Factor destinationFactor, Equation blendEquation)
            : this(sourceFactor, destinationFactor, blendEquation, sourceFactor, destinationFactor, blendEquation)
        {
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Construct the blend mode given the factors and equation
        /// </summary>
        /// <param name="colorSourceFactor">Specifies how to compute the source factor for the color channels.</param>
        /// <param name="colorDestinationFactor">Specifies how to compute the destination factor for the color channels.</param>
        /// <param name="colorBlendEquation">Specifies how to combine the source and destination colors.</param>
        /// <param name="alphaSourceFactor">Specifies how to compute the source factor.</param>
        /// <param name="alphaDestinationFactor">Specifies how to compute the destination factor.</param>
        /// <param name="alphaBlendEquation">Specifies how to combine the source and destination alphas.</param>
        ////////////////////////////////////////////////////////////
        public BlendMode(Factor colorSourceFactor, Factor colorDestinationFactor, Equation colorBlendEquation, Factor alphaSourceFactor, Factor alphaDestinationFactor, Equation alphaBlendEquation)
        {
            ColorSrcFactor = colorSourceFactor;
            ColorDstFactor = colorDestinationFactor;
            ColorEquation = colorBlendEquation;
            AlphaSrcFactor = alphaSourceFactor;
            AlphaDstFactor = alphaDestinationFactor;
            AlphaEquation = alphaBlendEquation;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Compare two blend modes and checks if they are equal
        /// </summary>
        /// <returns>Blend Modes are equal</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator ==(BlendMode left, BlendMode right) => left.Equals(right);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Compare two blend modes and checks if they are not equal
        /// </summary>
        /// <returns>Blend Modes are not equal</returns>
        ////////////////////////////////////////////////////////////
        public static bool operator !=(BlendMode left, BlendMode right) => !left.Equals(right);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Compare blend mode and object and checks if they are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>Object and blend mode are equal</returns>
        ////////////////////////////////////////////////////////////
        public override bool Equals(object obj) => ( obj is BlendMode mode ) && Equals(mode);

        ///////////////////////////////////////////////////////////
        /// <summary>
        /// Compare two blend modes and checks if they are equal
        /// </summary>
        /// <param name="other">Blend Mode to check</param>
        /// <returns>blend modes are equal</returns>
        ////////////////////////////////////////////////////////////
        public bool Equals(BlendMode other) => ( ColorSrcFactor == other.ColorSrcFactor ) &&
                   ( ColorDstFactor == other.ColorDstFactor ) &&
                   ( ColorEquation == other.ColorEquation ) &&
                   ( AlphaSrcFactor == other.AlphaSrcFactor ) &&
                   ( AlphaDstFactor == other.AlphaDstFactor ) &&
                   ( AlphaEquation == other.AlphaEquation );

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a integer describing the object
        /// </summary>
        /// <returns>Integer description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override int GetHashCode() => ColorSrcFactor.GetHashCode() ^
                   ColorDstFactor.GetHashCode() ^
                   ColorEquation.GetHashCode() ^
                   AlphaSrcFactor.GetHashCode() ^
                   AlphaDstFactor.GetHashCode() ^
                   AlphaEquation.GetHashCode();

        /// <summary>Source blending factor for the color channels</summary>
        public Factor ColorSrcFactor;

        /// <summary>Destination blending factor for the color channels</summary>
        public Factor ColorDstFactor;

        /// <summary>Blending equation for the color channels</summary>
        public Equation ColorEquation;

        /// <summary>Source blending factor for the alpha channel</summary>
        public Factor AlphaSrcFactor;

        /// <summary>Destination blending factor for the alpha channel</summary>
        public Factor AlphaDstFactor;

        /// <summary>Blending equation for the alpha channel</summary>
        public Equation AlphaEquation;
    }
}
