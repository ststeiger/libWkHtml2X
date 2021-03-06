﻿
namespace libWkHtmlToX
{


    public static class CallsInitializer 
    {

        // [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        // static extern bool SetDllDirectory(string lpPathName);

        
        internal static void AddToPathEnvironmentVariable(string dllDirectory)
        {
            System.Environment.SetEnvironmentVariable("PATH", System.Environment.GetEnvironmentVariable("PATH") + ";" + dllDirectory);
        }


        public static void InitWkhtmlToX(string dllDir)
        {
            libWkHtmlToX.CoInitHelper.CoInitialize();
            AddToPathEnvironmentVariable(dllDir);

            ConstUtf8Marshaler.GetInstance();
            Utf8Marshaler.GetInstance();

            CallsImage.wkhtmltoimage_init(false);
            CallsPDF.wkhtmltopdf_init(false);
        }


        internal static void InitWkhtmlToX()
        {
            string dllDir = VisualStudioHelper.GetDllDirectory();
            InitWkhtmlToX(dllDir);
        }
        

    } // End Class NativeMethods 


} // End namespace libWkHtmlToX 
