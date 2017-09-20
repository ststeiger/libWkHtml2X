
namespace libWkHtml2X
{


    public class VisualStudioHelper
    {


#if NET_2_0

        [System.Runtime.InteropServices.DllImport("libc", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        private static extern int uname(System.IntPtr buf);


        private static string GetOsNameInternal()
        {
            string s_osName = null;

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


        private static bool IsWindows()
        {
            return (System.Environment.OSVersion.Platform != System.PlatformID.Unix);
        }


        private static bool IsMac()
        {
            string s_osName = GetOsNameInternal();

            if (s_osName != null)
            {
                if (System.Environment.OSVersion.Platform == System.PlatformID.Unix) ;
                {
                    return s_osName.IndexOf("mac", System.StringComparison.OrdinalIgnoreCase) != -1;
                }
            }

            return false;
        }


        private static bool IsLinux()
        {
            string s_osName = GetOsNameInternal();

            if (s_osName != null)
            {
                if (System.Environment.OSVersion.Platform == System.PlatformID.Unix) ;
                {
                    return s_osName.IndexOf("Linux", System.StringComparison.OrdinalIgnoreCase) != -1;
                }
            }

            return false;
        }


        private static bool IsUnix()
        {
            string s_osName = GetOsNameInternal();

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

        private static bool IsWindows()
        {
            return System.Runtime.InteropServices.RuntimeInformation
                .IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows);
        }

        private static bool IsMac()
        {
            return System.Runtime.InteropServices.RuntimeInformation
                .IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX);
        }

        private static bool IsLinux()
        {
            return System.Runtime.InteropServices.RuntimeInformation
                .IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux);
        }

        private static bool IsUnix()
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


        private static bool IsNetCore()
        {
            System.Type tIntroSpec = System.Type.GetType("System.Reflection.IntrospectionExtensions, System.Private.CoreLib", false);
            return (tIntroSpec != null);
        }


        private static string GetOsDirectory()
        {
            if (IsWindows())
                return "/Win";

            if (IsLinux())
                return "/Linux";

            if (IsMac())
                return "/Mac";

            if (IsUnix())
                return "/Unix";

            return "/Win";
        }


        public static string GetDllDirectory()
        {
            // string dllDirectory = @"C:\PortableApps\wkhtmltopdf\x" + (System.IntPtr.Size * 8).ToString() + @"\bin";
            string dllDirectory = System.IO.Path.GetFullPath(
                    System.IO.Path.Combine(
                        System.IO.Path.Combine(
                            System.IO.Path.GetDirectoryName(
                                System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(VisualStudioHelper)).Assembly.Location
                            )
                            //#if VER_NEU

                            , (IsNetCore() ? "../../../../" : "../../../")
                            + "TestWkHtmlToX/Libs/" +

#if false
                             "0.13.0-alpha"

#elif false
                             "0.12.1.2"
#else
                             "0.12.4"
#endif
                             + GetOsDirectory()
                             )

                        , "x86-" + (System.IntPtr.Size * 8).ToString()
                    )
            );

            return dllDirectory;
        }


        public static string MapSolutionPath(string file)
        {
            if (file == null)
                return null;

            string dir = System.IO.Path.GetFullPath(
                        System.IO.Path.Combine(
                            System.IO.Path.GetDirectoryName(
                                System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(VisualStudioHelper)).Assembly.Location
                            )
                            , (IsNetCore() ? "../../../../" : "../../../") + "TestWkHtmlToX" 
                             )
            );

            // System.Console.WriteLine(dir);

            file = file.Replace("/", System.IO.Path.DirectorySeparatorChar.ToString()).Replace(@"\", System.IO.Path.DirectorySeparatorChar.ToString());
            if (file.StartsWith("~"))
            {
                file = file.Substring(1);

                if (file.StartsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
                    file = file.Substring(1);

                file = System.IO.Path.Combine(dir, file);
                return file;
            } // End if (file.StartsWith("~")) 

            return file;
        }


        public static string MapSolutionPath_old(string file)
        {
            file = file.Replace("/", System.IO.Path.DirectorySeparatorChar.ToString()).Replace(@"\", System.IO.Path.DirectorySeparatorChar.ToString());
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(VisualStudioHelper)).Assembly.Location);
            
            dir = System.IO.Path.Combine(System.IO.Path.Combine(dir, ".."), "..");
            dir = System.IO.Path.GetFullPath(dir);

            if (file.StartsWith("~"))
            {
                file = file.Substring(1);

                if (file.StartsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
                    file = file.Substring(1);

                file = System.IO.Path.Combine(dir, file);
                return file;
            } // End if (file.StartsWith("~")) 

            return file;
        } // End Function MapSolutionPath 


    }


}
