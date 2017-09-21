
namespace libWkHtml2X
{


    public class VisualStudioHelper
    {


        public static string GetDllDirectory()
        {
            // string dllDirectory = @"C:\PortableApps\wkhtmltopdf\x" + (System.IntPtr.Size * 8).ToString() + @"\bin";
            string dllDirectory = System.IO.Path.GetFullPath(
                    System.IO.Path.Combine(
                        System.IO.Path.Combine(
                            System.IO.Path.GetDirectoryName(
                                System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(VisualStudioHelper)).Assembly.Location
                            )
                            
                            , (OsHelper.IsNetCore ? "../../../../" : "../../../")
                            + "TestWkHtmlToX/Libs/" +

#if false
                             "0.13.0-alpha"

#elif false
                             "0.12.1.2"
#else
                             "0.12.4"
#endif
                             + OsHelper.GetOsDirectory()
                             )

                        , "x86-" + (System.IntPtr.Size * 8).ToString()
                    )
            );

            return dllDirectory;
        } // End Function GetDllDirectory 


        public static string MapSolutionPath(string file)
        {
            if (file == null)
                return null;

            string dir = System.IO.Path.GetFullPath(
                        System.IO.Path.Combine(
                            System.IO.Path.GetDirectoryName(
                                System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(VisualStudioHelper)).Assembly.Location
                            )
                            , ( OsHelper.IsNetCore ? "../../../../" : "../../../") + "TestWkHtmlToX"
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
        } // End Function MapSolutionPath 


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


    } // End Class VisualStudioHelper 


} // End Namespace libWkHtml2X 
