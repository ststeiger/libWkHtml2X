
namespace libWkHtml2X
{


    public class LibraryLoader
    {
        private static AbstractLibraryLoader s_Loader;

        static LibraryLoader()
        {
            s_Loader = AbstractLibraryLoader.CreateInstance();
        }

        public static System.IntPtr Load(string libraryFileName)
        {
            return s_Loader.LoadLibrary(libraryFileName);
        }

        public static System.IntPtr LoadSymbol(System.IntPtr hSO, string symbol)
        {
            return s_Loader.LoadSymbol(hSO, symbol);
        }

        public static bool Unload(System.IntPtr hSO)
        {
            return s_Loader.Unload(hSO);
        }


        public static void UnloadAllLoadedDlls()
        {
            s_Loader.UnloadAllLoadedDlls();
        }

    }


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


        public override System.IntPtr LoadLibrary(string libraryFileName)
        {
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
                    , new System.Exception(dlerror())
                );
            } // End if (hSO == IntPtr.Zero)

            this.m_dictionary.Add(libraryFileName, hSO);

            return hSO;
        }


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
        }


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
        }


    }


    public class WindowsLoader : AbstractLibraryLoader
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



        public override System.IntPtr LoadLibrary(string libraryFileName)
        {
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
                throw new System.InvalidOperationException("Cannot open libary \"" + libraryFileName + "\".");
            } // End if (hSO == IntPtr.Zero)

            this.m_dictionary.Add(libraryFileName, hSO);

            return hSO;
        }


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
        }


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
        }


    }


    public abstract class AbstractLibraryLoader
    {
        protected BiDictionary<string, System.IntPtr> m_dictionary;

        protected AbstractLibraryLoader()
        {
            this.m_dictionary = new BiDictionary<string, System.IntPtr>();
        }


        public abstract System.IntPtr LoadLibrary(string libraryFileName);
        public abstract bool Unload(System.IntPtr hSO);
        public abstract System.IntPtr LoadSymbol(System.IntPtr hModule, string symbol);


        public virtual void UnloadAllLoadedDlls()
        {
            System.Collections.Generic.List<System.Exception> ls = new System.Collections.Generic.List<System.Exception>();

            foreach (string strKey in this.m_dictionary.Keys)
            {
                try
                {
                    bool notUnloaded = Unload(this.m_dictionary[strKey]);
                    if (notUnloaded)
                        throw new System.InvalidOperationException("Couldn't unload \"" + strKey + "\".");
                }
                catch (System.Exception ex)
                {
                    ls.Add(ex);
                }

            } // Next strKey

            if (ls.Count > 0)
                throw new System.Exception("Unable to unload all dlls.", ls[0]);
            else
                ls = null;
        }


        public static AbstractLibraryLoader CreateInstance()
        {
#if NET_2_0
            if (System.Environment.OSVersion.Platform != System.PlatformID.Unix)
                return new WindowsLoader();
#else
            if (System.Runtime.InteropServices.RuntimeInformation
                .IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows)
            )
                return new WindowsLoader();
#endif

            return new PosixLoader();
        }


    }


}
