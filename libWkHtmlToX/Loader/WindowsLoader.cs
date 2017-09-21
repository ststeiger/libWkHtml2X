
namespace libWkHtmlToX
{


    public class WindowsLoader: AbstractLibraryLoader
    {

        public WindowsLoader() : base()
        { }

        [System.Runtime.InteropServices.DllImport("kernel32", EntryPoint = "LoadLibrary", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        protected static extern System.IntPtr dlopen(string lpFileName);

        [System.Runtime.InteropServices.DllImport("kernel32", EntryPoint = "GetProcAddress", CharSet = System.Runtime.InteropServices.CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        protected static extern System.IntPtr GetProcAddress(System.IntPtr hModule, string procName);

        [System.Runtime.InteropServices.DllImport("kernel32", EntryPoint = "FreeLibrary", SetLastError = true)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        protected static extern bool FreeLibrary(System.IntPtr hModule);



        public override System.IntPtr LoadLibrary(string libraryFileName, bool withExtension)
        {
            if (string.IsNullOrEmpty(libraryFileName))
                throw new System.ArgumentNullException(libraryFileName);

            if (!withExtension)
            {
                if (!libraryFileName.EndsWith(".dll", System.StringComparison.OrdinalIgnoreCase))
                    libraryFileName += ".dll";
            }

            System.IntPtr hSO = System.IntPtr.Zero;

            try
            {
                hSO = dlopen(libraryFileName);
            } // End Try
            catch (System.Exception ex)
            {
                throw new System.InvalidOperationException("Cannot open " + libraryFileName, ex);
            } // End Catch

            if (hSO == System.IntPtr.Zero)
            {
                System.Exception ex = new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
                
                throw new System.InvalidOperationException("Cannot open libary \"" + libraryFileName + "\".", ex);
            } // End if (hSO == IntPtr.Zero)

            this.m_dictionary.Add(libraryFileName, hSO);
            
            return hSO;
        } // End Function LoadLibrary 


        public override System.IntPtr LoadSymbol(System.IntPtr hModule, string symbol)
        {
            System.IntPtr ptr = System.IntPtr.Zero;
            try
            {
                ptr = GetProcAddress(hModule, symbol);

                if (ptr == System.IntPtr.Zero)
                    throw new System.ComponentModel.Win32Exception(
                        System.Runtime.InteropServices.Marshal.GetLastWin32Error());
            }
            catch (System.Exception ex)
            {
                throw new System.InvalidOperationException("Unable to load symbol '" + symbol + "'.", ex);
            }

            return ptr;
        } // End Function LoadSymbol 


        public override bool Unload(System.IntPtr hSO)
        {
            bool bError = true;

            if (hSO == System.IntPtr.Zero)
            {
                throw new System.ArgumentNullException("hSO");
            } // End if (hSO == IntPtr.Zero)

            try
            {
                // FreeLibrary: If the function succeeds, the return value is nonzero.
                // If the function fails, the return value is zero. 
                // To get extended error information, call the GetLastError function.
                bError = !FreeLibrary(hSO);

                if (bError)
                    throw new System.ComponentModel.Win32Exception(
                        System.Runtime.InteropServices.Marshal.GetLastWin32Error());
            } // End Try
            catch (System.Exception ex)
            {
                throw new System.InvalidOperationException("Cannot unload handle '" + hSO.ToInt64().ToString() + "'.", ex);
            } // End Catch

            return bError;
        } // End Function Unload 


    } // End Class WindowsLoader 


} // End Namespace 
