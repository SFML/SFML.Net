using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using SFML.System;

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
        public Context()
        {
            myThis = sfContext_create();
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Finalizer
        /// </summary>
        ////////////////////////////////////////////////////////////
        ~Context()
        {
            sfContext_destroy(myThis);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Check whether a given OpenGL extension is available.
        /// </summary>
        /// <param name="name">Name of the extension to check for</param>
        /// <returns>True if available, false if unavailable</returns>
        ////////////////////////////////////////////////////////////
        public bool IsExtensionAvailable(string name)
        {
            return sfContext_isExtensionAvailable(myThis, name);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Activate or deactivate the context
        /// </summary>
        /// <param name="active">True to activate, false to deactivate</param>
        /// <returns>True on success, false on failure</returns>
        ////////////////////////////////////////////////////////////
        public bool SetActive(bool active)
        {
            return sfContext_setActive(myThis, active);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the address of an OpenGL function.
        /// </summary>
        /// <param name="name">Name of the function to get the address of</param>
        /// <returns>Address of the OpenGL function, <see cref="IntPtr.Zero"/> on failure</returns>
        ////////////////////////////////////////////////////////////
        public IntPtr GetFunction(string name)
        {
            return sfContext_getFunction(myThis, name);
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the settings of the context.
        /// </summary>
        ////////////////////////////////////////////////////////////
        public ContextSettings Settings
        {
            get { return sfContext_getSettings(myThis); }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Global helper context
        /// </summary>
        ////////////////////////////////////////////////////////////
        public static Context Global
        {
            get
            {
                if (ourGlobalContext == null)
                {
                    ourGlobalContext = new Context();
                }

                return ourGlobalContext;
            }
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Provide a string describing the object
        /// </summary>
        /// <returns>String description of the object</returns>
        ////////////////////////////////////////////////////////////
        public override string ToString()
        {
            return "[Context]";
        }

        private static Context ourGlobalContext = null;

        private readonly IntPtr myThis = IntPtr.Zero;

        #region Imports
        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfContext_create();

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern void sfContext_destroy(IntPtr View);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfContext_isExtensionAvailable(IntPtr View, string name);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern bool sfContext_setActive(IntPtr View, bool Active);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern IntPtr sfContext_getFunction(IntPtr View, string name);

        [DllImport(CSFML.window, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern ContextSettings sfContext_getSettings(IntPtr View);
        #endregion
    }
}
