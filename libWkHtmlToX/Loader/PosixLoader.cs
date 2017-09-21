
namespace libWkHtmlToX
{


    public class PosixLoader : AbstractLibraryLoader
    {

        public PosixLoader() : base()
        { }


        // See http://mpi4py.googlecode.com/svn/trunk/src/dynload.h
        protected const int RTLD_LAZY = 1; // for dlopen's flags
        protected const int RTLD_NOW = 2; // for dlopen's flags

        [System.Runtime.InteropServices.DllImport("libdl", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        protected static extern System.IntPtr dlopen(string filename, int flags);

        [System.Runtime.InteropServices.DllImport("libdl", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        protected static extern System.IntPtr dlsym(System.IntPtr handle, string symbol);

        [System.Runtime.InteropServices.DllImport("libdl", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        protected static extern int dlclose(System.IntPtr handle);

        [System.Runtime.InteropServices.DllImport("libdl", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        protected static extern string dlerror();


        public override System.IntPtr LoadLibrary(string libraryFileName, bool withExtension)
        {
            if (string.IsNullOrEmpty(libraryFileName))
                throw new System.ArgumentNullException(libraryFileName);

            if (!withExtension)
            {
                if (!libraryFileName.EndsWith(".so", System.StringComparison.OrdinalIgnoreCase))
                    libraryFileName += ".so";
            }

            System.IntPtr hSO = System.IntPtr.Zero;

            try
            {
                hSO = dlopen(libraryFileName, RTLD_NOW);
            } // End Try
            catch (System.Exception ex)
            {
                throw new System.InvalidOperationException("Cannot open " + libraryFileName, ex);
            } // End Catch

            if (hSO == System.IntPtr.Zero)
            {
                throw new System.InvalidOperationException("Cannot open library \"" 
                    + libraryFileName + "\"."
                    , new System.Exception( dlerror() )
                );
            } // End if (hSO == IntPtr.Zero)

            this.m_dictionary.Add(libraryFileName, hSO);

            return hSO;
        } // End Function LoadLibrary 


        public override System.IntPtr LoadSymbol(System.IntPtr hModule, string symbol)
        {
            System.IntPtr ptr = System.IntPtr.Zero;
            try
            {
                ptr = dlsym(hModule, symbol);

                if (ptr == System.IntPtr.Zero)
                    throw new System.Exception(dlerror());
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
                // If the referenced object was successfully closed, dlclose() shall return 0. 
                // If the object could not be closed, or if handle does not refer to an open object, 
                // dlclose() shall return a non-zero value. 
                // More detailed diagnostic information shall be available through dlerror().

                // http://stackoverflow.com/questions/956640/linux-c-error-undefined-reference-to-dlopen
                if (dlclose(hSO) == 0)
                    bError = false;

                if (bError)
                    throw new System.Exception(dlerror());
            }
            catch (System.Exception ex)
            {
                throw new System.InvalidOperationException("Cannot unload handle '" + hSO.ToInt64().ToString() + "'", ex);
            }         

            return bError;
        } // End Function Unload 


    } // End Class PosixLoader 


} // End Namespace 
