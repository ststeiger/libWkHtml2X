
namespace libWkHtmlToX
{


    public class MacLoader : PosixLoader
    {

        public MacLoader()
            : base()
        { }


        public override System.IntPtr LoadLibrary(string libraryFileName, bool withExtension)
        {
            if (string.IsNullOrEmpty(libraryFileName))
                throw new System.ArgumentNullException(libraryFileName);

            if (!withExtension)
            {
                if (!libraryFileName.EndsWith(".dylib", System.StringComparison.OrdinalIgnoreCase))
                    libraryFileName += ".dylib";
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
                    , new System.Exception(dlerror())
                );
            } // End if (hSO == IntPtr.Zero)

            this.m_dictionary.Add(libraryFileName, hSO);

            return hSO;
        } // End Function LoadLibrary 


    } // End Class MacLoader 


} // End Namespace 
