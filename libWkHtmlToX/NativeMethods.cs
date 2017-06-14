
namespace libWkHtml2X
{


    internal static class NativeMethods
    {
        internal const string DLL_NAME = "wkhtmltox";

        //[System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        //static extern bool SetDllDirectory(string lpPathName);


        private static object s_initLock = new object();
        private static string s_osName = null;
        private static bool? s_isWindows;
        private static bool? s_isMac;
        private static bool? s_isLinux;
        private static bool? s_isUnix;
        private static bool? s_isNetCore;
        private static bool? s_CoInitialized;

#if NET_2_0

        [System.Runtime.InteropServices.DllImport("libc", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern int uname(System.IntPtr buf);


        private static string GetOsNameInternal()
        {
            if (System.Environment.OSVersion.Platform != System.PlatformID.Unix
                && System.Environment.OSVersion.Platform != System.PlatformID.MacOSX)
            {
                return System.Environment.OSVersion.Platform.ToString();
            }

            System.IntPtr buf = System.IntPtr.Zero;
            try
            {
                buf = System.Runtime.InteropServices.Marshal.AllocHGlobal(8192);
                // This is a hacktastic way of getting sysname from uname ()
                if (uname(buf) == 0)
                {
                    s_osName = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(buf);
                }
            }
            catch { }
            finally
            {
                if (buf != System.IntPtr.Zero)
                    System.Runtime.InteropServices.Marshal.FreeHGlobal(buf);
            }

            return s_osName;
        }


        private static bool IsWindowsInternal()
        {
            return (System.Environment.OSVersion.Platform != System.PlatformID.Unix);
        }


        private static bool IsMacInternal()
        {
            if (s_osName != null)
            {
                if (System.Environment.OSVersion.Platform == System.PlatformID.Unix) ;
                {
                    s_isMac = s_osName.IndexOf("mac", System.StringComparison.OrdinalIgnoreCase) != -1;
                }
            }

            return false;
        }


        private static bool IsLinuxInternal()
        {
            if (s_osName != null)
            {
                if (System.Environment.OSVersion.Platform == System.PlatformID.Unix) ;
                {
                    s_isMac = s_osName.IndexOf("Linux", System.StringComparison.OrdinalIgnoreCase) != -1;
                }
            }

            return false;
        }


        private static bool IsUnixInternal()
        {
            if (s_osName != null)
            {
                if (System.Environment.OSVersion.Platform == System.PlatformID.Unix) ;
                {
                    if (s_osName.IndexOf("Linux", System.StringComparison.OrdinalIgnoreCase) != -1)
                        return false;

                    if (s_osName.IndexOf("Mac", System.StringComparison.OrdinalIgnoreCase) != -1)
                        return false;

                    return true;
                }
            }

            return false;
        }

#else

        // https://stackoverflow.com/questions/38790802/determine-operating-system-in-net-core
        private static string GetOsNameInternal()
        {
            return System.Runtime.InteropServices.RuntimeInformation.OSDescription;
        }

        private static bool IsWindowsInternal()
        {
            return System.Runtime.InteropServices.RuntimeInformation
                .IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows);
        }

        private static bool IsMacInternal()
        {
            return System.Runtime.InteropServices.RuntimeInformation
                .IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX);
        }

        private static bool IsLinuxInternal()
        {
            return System.Runtime.InteropServices.RuntimeInformation
                .IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux);
        }

        private static bool IsUnixInternal()
        {
            if(System.Runtime.InteropServices.RuntimeInformation
                .IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
                return false;

            if(System.Runtime.InteropServices.RuntimeInformation
                .IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
                return false;

            if(System.Runtime.InteropServices.RuntimeInformation
                .IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
                return false;

            return true;
        }

#endif
        
        static NativeMethods()
        {
            lock (s_initLock)
            {
                if (s_osName == null)
                    s_osName = GetOsNameInternal();

                if (!s_isWindows.HasValue)
                {
                    s_isWindows = IsWindowsInternal();
                }

                if (!s_isMac.HasValue)
                {
                    s_isMac = IsMacInternal();
                }

                if (!s_isLinux.HasValue)
                {
                    s_isLinux = IsLinuxInternal();
                }

                if (!s_isUnix.HasValue)
                {
                    s_isUnix = IsUnixInternal();
                }

                if (!s_isNetCore.HasValue)
                {
                    System.Type tIntroSpec = System.Type.GetType("System.Reflection.IntrospectionExtensions, System.Private.CoreLib", false);
                    s_isNetCore = tIntroSpec != null;
                }

                if (!s_CoInitialized.HasValue)
                {
                    if (IsWindows && IsNetCore)
                        CoInitHelper.CoInitialize();

                    s_CoInitialized = true;
                }

            } // End lock (s_initLock)

        } // End Constructor 


        public static bool IsWindows
        {
            get { return s_isWindows.Value; }
        }

        public static bool IsMac
        {
            get { return s_isMac.Value; }
        }

        public static bool IsLinux
        {
            get { return s_isLinux.Value; }
        }

        public static bool IsUnix
        {
            get { return s_isUnix.Value; }
        }

        public static bool IsNetCore
        {
            get { return s_isNetCore.Value; }
        }

        internal static void Init(string dllDirectory)
        {
            System.Environment.SetEnvironmentVariable("PATH", System.Environment.GetEnvironmentVariable("PATH") + ";" + dllDirectory);

            ConstUtf8Marshaler.GetInstance();
            Utf8Marshaler.GetInstance();
        }


        internal static void Init()
        {
            string dllDirectory = @"C:\PortableApps\wkhtmltopdf\x64\bin";
            if (System.IntPtr.Size * 8 == 32)
                dllDirectory = @"C:\PortableApps\wkhtmltopdf\x86\bin";

            Init(dllDirectory);
        }


        public static string OS_Name
        {
            get
            {
                return s_osName;
            }
        }


    } // End Class NativeMethods 


} // End Namespace libWkHtml2X 
