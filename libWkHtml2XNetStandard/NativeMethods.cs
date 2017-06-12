
using System.Runtime.InteropServices;


namespace libWkHtml2XNetStandard
{


    internal class NativeMethods
    {

        internal const string DLL_NAME = "wkhtmltox";

        internal static void Init()
        {
            ConstUtf8Marshaler.GetInstance();
            Utf8Marshaler.GetInstance();

            string dllDirectory = @"C:\PortableApps\wkhtmltopdf\x64\bin"; 
            if(System.IntPtr.Size * 8 == 32)
                dllDirectory = @"C:\PortableApps\wkhtmltopdf\x86\bin";

            System.Environment.SetEnvironmentVariable("PATH", System.Environment.GetEnvironmentVariable("PATH") + ";" + dllDirectory);
        }


        /*
        [DllImport("libc")]
        public static extern int uname(System.IntPtr buf);

        private static object _OsNameLock = new object();
        private static string _OsName = null;

        private static string GetOsNameInternal()
        {
            if (!string.IsNullOrEmpty(_OsName))
                return _OsName;

            if (System.Environment.OSVersion.Platform != System.PlatformID.Unix
                && System.Environment.OSVersion.Platform != System.PlatformID.MacOSX)
            {
                return System.Environment.OSVersion.Platform.ToString();
            }

            System.IntPtr buf = System.IntPtr.Zero;
            try
            {
                buf = Marshal.AllocHGlobal(8192);
                // This is a hacktastic way of getting sysname from uname ()
                if (uname(buf) == 0)
                {
                    return Marshal.PtrToStringAnsi(buf);
                }
            }
            catch { }
            finally
            {
                if (buf != System.IntPtr.Zero) Marshal.FreeHGlobal(buf);
            }

            return null;
        }

        public static string GetOsName()
        {
            lock (_OsNameLock)
            {
                if (_OsName == null)
                    _OsName = GetOsNameInternal();

                return _OsName;
            }
        }
        */
        

    }


}
