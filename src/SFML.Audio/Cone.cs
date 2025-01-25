using System.Runtime.InteropServices;
using SFML.System;

namespace SFML.Audio
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Structure defining the properties of a directional cone
    /// <para/>
    /// When set on the listener, sounds will play at gain 1 when
    /// they are positioned within the inner angle of the cone.
    /// Sounds will play at outerGain when they are positioned
    /// outside the outer angle of the cone.
    /// The gain declines linearly from 1 to outerGain as the
    /// sound moves from the inner angle to the outer angle.
    /// <para/>
    /// When set on sound sources, they will play at gain 1 when the
    /// listener is positioned within the inner angle of the cone.
    /// Sounds will play at `outerGain` when the listener is
    /// positioned outside the outer angle of the cone.
    /// The gain declines linearly from 1 to outerGain as the
    /// listener moves from the inner angle to the outer angle.
    /// </summary>
    ////////////////////////////////////////////////////////////
    public struct Cone
    {
        /// <summary>Inner angle</summary>
        public Angle InnerAngle;

        /// <summary>Outer angle</summary>
        public Angle OuterAngle;

        /// <summary>Outer angle</summary>
        public float OuterGain;

        [StructLayout(LayoutKind.Sequential)]
        internal struct MarshalData
        {
            public float InnerAngleDegrees;
            public float OuterAngleDegrees;
            public float OuterGain;
        }

        internal Cone(MarshalData data)
        {
            InnerAngle = Angle.FromDegrees(data.InnerAngleDegrees);
            OuterAngle = Angle.FromDegrees(data.OuterAngleDegrees);
            OuterGain = data.OuterGain;
        }

        // Return a marshalled version of the instance, that can directly be passed to the C API
        internal MarshalData Marshal()
        {
            var data = new MarshalData
            {
                InnerAngleDegrees = InnerAngle.Degrees,
                OuterAngleDegrees = OuterAngle.Degrees,
                OuterGain = OuterGain
            };

            return data;
        }
    }
}
