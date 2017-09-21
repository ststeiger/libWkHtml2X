
namespace libWkHtmlToX
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

        public static System.IntPtr Load(string libraryFileName, bool withExtension)
        {
            return s_Loader.LoadLibrary(libraryFileName, withExtension);
        }

        public static System.IntPtr LoadSymbol(System.IntPtr hSO, string symbol)
        {
            return s_Loader.LoadSymbol(hSO, symbol);
        }

        public static T LoadSymbol<T>(System.IntPtr module, string symbol)
        // where T : System.Delegate
        {
            return s_Loader.LoadSymbol<T>(module, symbol);
        }

        public static T LoadSymbol<T>(System.IntPtr module, System.IntPtr symbol)
        // where T : System.Delegate
        {
            return s_Loader.LoadSymbol<T>(module, symbol);
        }

        public static System.Delegate LoadSymbol(System.IntPtr module, string symbol, System.Type type)
        {
            return s_Loader.LoadSymbol(module, symbol, type);
        }

        public static System.Delegate LoadSymbol(System.IntPtr module, System.IntPtr symbol, System.Type type)
        {
            return s_Loader.LoadSymbol(module, symbol, type);
        }

        public static bool Unload(System.IntPtr hSO)
        {
            return s_Loader.Unload(hSO);
        }

        public static void UnloadAllLoadedDlls()
        {
            s_Loader.UnloadAllLoadedDlls();
        }

    } // End Static Class LibraryLoader 


} // End Namespace 
