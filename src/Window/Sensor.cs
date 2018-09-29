using System.Runtime.InteropServices;
using System.Security;
using SFML.System;

namespace SFML.Window
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Give access to the real-time state of sensors
    /// </summary>
    ////////////////////////////////////////////////////////////
    public static class Sensor
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Sensor types
        /// </summary>
        ////////////////////////////////////////////////////////////
        public enum Type
        {
            /// <summary>Measures the raw acceleration (m/s^2)</summary>
            Accelerometer,

            /// <summary>Measures the raw rotation rates (degrees/s)</summary>
            Gyroscope,

            /// <summary>Measures the ambient magnetic field (micro-teslas)</summary>
            Magnetometer,

            /// <summary>Measures the direction and intensity of gravity, independent of device acceleration (m/s^2)</summary>
            Gravity,

            /// <summary>Measures the direction and intensity of device acceleration, independent of the gravity (m/s^2)</summary>
            UserAcceleration,

            /// <summary>Measures the absolute 3D orientation (degrees)</summary>
            Orientation,

            /// <summary>Keep last -- the total number of sensor types</summary>
            TypeCount
        };

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Check if a sensor is available on the underlying platform
        /// </summary>
        /// <param name="Sensor">Sensor to check</param>
        /// <returns>True if the sensor is available, false otherwise</returns>
        ////////////////////////////////////////////////////////////
        public static bool IsAvailable(Type Sensor)
        {
            return sfSensor_isAvailable(Sensor);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Enable or disable a sensor
        /// </summary>
        /// <param name="Sensor">Sensor to check</param>
        /// <param name="Enabled">True to enable, false to disable</param>
        ////////////////////////////////////////////////////////////
        public static void SetEnabled(Type Sensor, bool Enabled)
        {
            sfSensor_setEnabled(Sensor, Enabled);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the current sensor value
        /// </summary>
        /// <param name="Sensor">Sensor to check</param>
        /// <returns>The current sensor value</returns>
        ////////////////////////////////////////////////////////////
        public static Vector3f GetValue(Type Sensor)
        {
            return sfSensor_getValue(Sensor);
        }

        #region Imports
        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern bool sfSensor_isAvailable(Type Sensor);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern void sfSensor_setEnabled(Type Sensor, bool Enabled);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        static extern Vector3f sfSensor_getValue(Type Sensor);
        #endregion
    }
}
