using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SFML.System
{
    public class CSFML
    {
        private static string decorate(String libname)
        {

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return $"{libname}-2.dll";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return $"lib{libname}.so";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return $"lib{libname}.dylib";
            }
            else
            {
                throw new Exception("Unknown OS cannot match Shared Library");
            }

        }

#if   _WINDOWS_
        public const string audio       = "csfml-audio-2.dll";
        public const string graphics    = "csfml-graphics-2.dll";
        public const string system      = "csfml-system-2.dll";
        public const string window      = "csfml-window-2.dll";
#elif _OSX_
        public const string audio       = "libcsfml-audio.dylib";
        public const string graphics    = "libcsfml-graphics.dylib";
        public const string system      = "libcsfml-system.dylib";
        public const string window      = "libcsfml-window.dylib";
#elif _LINUX_
        public const string audio       = "libcsfml-audio.so";
        public const string graphics    = "libcsfml-graphics.so";
        public const string system      = "libcsfml-system.so";
        public const string window      = "libcsfml-window.so";
#endif


    }
}
