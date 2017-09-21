
namespace libWkHtmlToX
{


    public abstract class AbstractLibraryLoader
    {
        protected BiDiDictionary<string, System.IntPtr> m_dictionary;

        protected AbstractLibraryLoader()
        {
            this.m_dictionary = new BiDiDictionary<string, System.IntPtr>();
        }

        public abstract System.IntPtr LoadLibrary(string libraryFileName, bool withExtension);

        public virtual System.IntPtr LoadLibrary(string libraryFileName)
        {
            return this.LoadLibrary(libraryFileName, false);
        }


        public abstract bool Unload(System.IntPtr hSO);
        public abstract System.IntPtr LoadSymbol(System.IntPtr hModule, string symbol);


        public virtual System.Delegate LoadSymbol(System.IntPtr module, System.IntPtr symbol, System.Type type)
        {
            if (!System.Reflection.IntrospectionExtensions.GetTypeInfo(type)
                .IsSubclassOf(typeof(System.Delegate)))
            {
                throw new System.InvalidOperationException(type.Name + " is not a delegate type");
            }

            System.Delegate delegateInstance = System.Runtime.InteropServices.Marshal.GetDelegateForFunctionPointer(symbol, type);
            return delegateInstance;
        } // End Function LoadSymbol 

        public virtual System.Delegate LoadSymbol(System.IntPtr module, string symbol, System.Type type)
        {
            System.IntPtr ptrSymbol = this.LoadSymbol(module, symbol);
            return LoadSymbol(module, ptrSymbol, type);
        } // End Function LoadSymbol 

        // LoadSymbol<T>(...).DynamicInvoke("a", "b", "c");
        public virtual T LoadSymbol<T>(System.IntPtr module, System.IntPtr symbol)
        // where T : System.Delegate
        {
            if (!System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(T))
                .IsSubclassOf(typeof(System.Delegate)))
            {
                throw new System.InvalidOperationException(typeof(T).Name + " is not a delegate type");
            }

#if NET_2_0
            T delegateInstance = (T)(object)this.LoadSymbol(module, symbol, typeof(T));
            return delegateInstance;
#else
            T delegateInstance = System.Runtime.InteropServices
                .Marshal.GetDelegateForFunctionPointer<T>(symbol);
            return delegateInstance;
#endif
        } // End Function LoadSymbol 

        public virtual T LoadSymbol<T>(System.IntPtr module, string symbol)
        // where T : System.Delegate
        {
            System.IntPtr ptrSymbol = this.LoadSymbol(module, symbol);
            return LoadSymbol<T>(module, ptrSymbol);
        } // End Function LoadSymbol 




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
        } // End Sub UnloadAllLoadedDlls 


        public static AbstractLibraryLoader CreateInstance()
        {
            if (OsHelper.IsWindows)
                return new WindowsLoader();

            if (OsHelper.IsLinux)
                return new PosixLoader();

            if (OsHelper.IsMac)
                return new MacLoader();

            return new PosixLoader();
        } // End Function CreateInstance 


    } // End Class AbstractLibraryLoader 


}
