using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using SFML.System;

// TODO getActiveContext
// TODO getActiveContextId

namespace SFML.Window
{
    //////////////////////////////////////////////////////////////////
    /// <summary>
    /// This class defines a .NET interface to an SFML OpenGL Context
    /// </summary>
    //////////////////////////////////////////////////////////////////
    public class Context : CriticalFinalizerObject
    {
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Default constructor
        /// </summary>
        ////////////////////////////////////////////////////////////
        public Context() => _this = sfContext_create();

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Finalizer
        /// </summary>
        ////////////////////////////////////////////////////////////
        ~Context()
        {
            sfContext_destroy(_this);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Check whether a given OpenGL extension is available.
        /// </summary>
        /// <param name="name">Name of the extension to check for</param>
        /// <returns>True if available, false if unavailable</returns>
        ////////////////////////////////////////////////////////////
        public static bool IsExtensionAvailable(string name) => sfContext_isExtensionAvailable(name);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Activate or deactivate the context
        /// </summary>
        /// <param name="active">True to activate, false to deactivate</param>
        /// <returns>True on success, false on failure</returns>
        ////////////////////////////////////////////////////////////
        public bool SetActive(bool active) => sfContext_setActive(_this, active);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the address of an OpenGL function.
        /// </summary>
        /// <param name="name">Name of the function to get the address of</param>
        /// <returns>Address of the OpenGL function, <see cref="IntPtr.Zero"/> on failure</returns>
        ////////////////////////////////////////////////////////////
        public static IntPtr GetFunction(string name) => sfContext_getFunction(name);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the settings of the context.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public ContextSettings Settings => sfContext_getSettings(_this);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Global helper context
        /// </summary>
        ////////////////////////////////////////////////////////////
        public static Context Global
        {
            get
            {
                if (_globalContext == null)
                {
                    _globalContext = new Context();
                }

                return _globalContext;
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString() => "[Context]";

        private static Context _globalContext;

        private readonly IntPtr _this = IntPtr.Zero;

        #region Imports
        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfContext_create();

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfContext_destroy(IntPtr view);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfContext_isExtensionAvailable(string name);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool sfContext_setActive(IntPtr view, bool active);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfContext_getFunction(string name);

        [DllImport(CSFML.Window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ContextSettings sfContext_getSettings(IntPtr view);
        #endregion
    }
}
