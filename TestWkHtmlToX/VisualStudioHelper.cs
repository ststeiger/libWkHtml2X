
namespace TestWkHtmlToX
{

    public class VisualStudioHelper
    {



        public static string MapSolutionPath(string file)
        {
            file = file.Replace("/", System.IO.Path.DirectorySeparatorChar.ToString()).Replace(@"\", System.IO.Path.DirectorySeparatorChar.ToString());
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
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
