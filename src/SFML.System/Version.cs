using System;
using System.Runtime.InteropServices;
using System.Security;

namespace SFML.System;

////////////////////////////////////////////////////////////
/// <summary>
/// Version information of the SFML library
/// </summary>
////////////////////////////////////////////////////////////
[StructLayout(LayoutKind.Sequential)]
public readonly struct Version
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// SFML Major Version
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly sbyte Major;

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// SFML Minor Version
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly sbyte Minor;

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// SFML Patch Version
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly sbyte Patch;

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// true if this is a release version, false if this is a development version
    /// </summary>
    ////////////////////////////////////////////////////////////
    public readonly bool IsRelease;

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// String representation of the SFML version, e.g. 3.1.0 or 3.1.0-dev
    /// </summary>
    ////////////////////////////////////////////////////////////
    public string String => Marshal.PtrToStringAnsi(_versionPointer);

    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Get the version of the SFML library loaded at runtime
    /// </summary>
    /// <returns>Version information of the SFML library</returns>
    ////////////////////////////////////////////////////////////
    public static Version GetVersion() => sfGetVersion();

    private readonly IntPtr _versionPointer;

    #region Imports
    [DllImport(CSFML.System, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
    private static extern Version sfGetVersion();
    #endregion
}