using System.Runtime.InteropServices;
using System.Security;
using SFML.System;

namespace SFML.Audio
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// The audio listener is the point in the scene
    /// from where all the sounds are heard
    /// </summary>
    ////////////////////////////////////////////////////////////
    public class Listener
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// The volume is a number between 0 and 100; it is combined with
        /// the individual volume of each sound / music.
        /// The default value for the volume is 100 (maximum).
        /// </summary>
        ////////////////////////////////////////////////////////////
        public static float GlobalVolume
        {
            get => sfListener_getGlobalVolume();
            set => sfListener_setGlobalVolume(value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 3D position of the listener (default is (0, 0, 0))
        /// </summary>
        ////////////////////////////////////////////////////////////
        public static Vector3f Position
        {
            get => sfListener_getPosition();
            set => sfListener_setPosition(value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// The direction (also called "at vector") is the vector
        /// pointing forward from the listener's perspective. Together
        /// with the up vector, it defines the 3D orientation of the
        /// listener in the scene. The direction vector doesn't
        /// have to be normalized.
        /// The default listener's direction is (0, 0, -1).
        /// </summary>
        ////////////////////////////////////////////////////////////
        public static Vector3f Direction
        {
            get => sfListener_getDirection();
            set => sfListener_setDirection(value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// The velocity of the listener in the scene (default is (0, 0, -1))
        /// </summary>
        ////////////////////////////////////////////////////////////
        public static Vector3f Velocity
        {
            get => sfListener_getVelocity();
            set => sfListener_setVelocity(value);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// The cone defines how directional attenuation is applied.
        /// The default cone of a sound is {2 * PI, 2 * PI, 1}.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public static Cone Cone
        {
            get => new Cone(sfListener_getCone());
            set => sfListener_setCone(value.Marshal());
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// The up vector is the vector that points upward from the
        /// listener's perspective. Together with the direction, it
        /// defines the 3D orientation of the listener in the scene.
        /// The up vector doesn't have to be normalized.
        /// The default listener's up vector is (0, 1, 0). It is usually
        /// not necessary to change it, especially in 2D scenarios.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public static Vector3f UpVector
        {
            get => sfListener_getUpVector();
            set => sfListener_setUpVector(value);
        }

        #region Imports
        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfListener_setGlobalVolume(float volume);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern float sfListener_getGlobalVolume();

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfListener_setPosition(Vector3f position);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector3f sfListener_getPosition();

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfListener_setDirection(Vector3f direction);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector3f sfListener_getDirection();

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfListener_setVelocity(Vector3f direction);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector3f sfListener_getVelocity();

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfListener_setCone(Cone.MarshalData cone);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Cone.MarshalData sfListener_getCone();

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfListener_setUpVector(Vector3f upVector);

        [DllImport(CSFML.Audio, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern Vector3f sfListener_getUpVector();
        #endregion
    }
}
