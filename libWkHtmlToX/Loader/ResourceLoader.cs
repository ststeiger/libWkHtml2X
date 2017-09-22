
namespace libWkHtmlToX
{


    public class ResourceLoader
    {


        public static string GetResourceName(System.Reflection.Assembly asm, string resourceName)
        {
            if (resourceName == null)
                return null;

#if EXACT
            foreach (string thisRessourceName in asm.GetManifestResourceNames())
            {
                
                if (System.StringComparer.OrdinalIgnoreCase.Equals(thisRessourceName, resourceName))
                {
                    return thisRessourceName;
                } // End if (thisRessourceName != null && thisRessourceName.EndsWith(resourceName, System.StringComparison.OrdinalIgnoreCase)) 

            } // Next thisRessourceName 
#endif 

            foreach (string thisRessourceName in asm.GetManifestResourceNames())
            {
                if (thisRessourceName.EndsWith(resourceName, System.StringComparison.OrdinalIgnoreCase))
                {
                    return thisRessourceName;
                } // End if (thisRessourceName != null && thisRessourceName.EndsWith(resourceName, System.StringComparison.OrdinalIgnoreCase)) 

            } // Next thisRessourceName 

            return resourceName;
        } // End Function GetResourceName 


        public static string GetResourceName(string resourceName)
        {
            System.Reflection.Assembly asm = System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(ResourceLoader)).Assembly;

            return GetResourceName(asm, resourceName);
        } // End Function GetResourceName 


        public static string ReadEmbeddedResource(System.Reflection.Assembly asm, string resourceName)
        {
            string retValue = null; 
            
            if (resourceName != null)
            {
                resourceName = GetResourceName(asm, resourceName);

                using (System.IO.Stream resourceStream = asm.GetManifestResourceStream(resourceName))
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(resourceStream, System.Text.Encoding.UTF8))
                    {
                        retValue = sr.ReadToEnd();
                    } // End Using sr 

                } // End Using resourceStream

            } // End if (resourceName != null)

            return retValue;
        } // End Sub ReadEmbeddedResource 


        public static string ReadEmbeddedResource(string resourceName)
        {
            System.Reflection.Assembly asm = System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(ResourceLoader)).Assembly;
            return ReadEmbeddedResource(asm, resourceName);
        } // End Sub ReadEmbeddedResource


        public static void ReadEmbeddedResource(System.Reflection.Assembly asm, string resourceName, System.IO.Stream output)
        {
            if (resourceName != null)
            {
                resourceName = GetResourceName(asm, resourceName);

                using (System.IO.Stream resourceStream = asm.GetManifestResourceStream(resourceName))
                {
                    byte[] buffer = new byte[8 * 1024];
                    int len;
                    while ((len = resourceStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        output.Write(buffer, 0, len);
                    } // Whend 

                    buffer = null;
                } // End Using resourceStream

            } // End if (resourceName != null)

        } // End Sub ReadEmbeddedResource 


        public static void ReadEmbeddedResource(string resourceName, System.IO.Stream output)
        {
            System.Reflection.Assembly asm = System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(ResourceLoader)).Assembly;
            ReadEmbeddedResource(asm, resourceName, output);
        } // End Sub ReadEmbeddedResource
        

        public static void EmbeddedResourceToFile(System.Reflection.Assembly asm, string resourceName, string targetFile)
        {
            if (resourceName != null)
            {
                resourceName = GetResourceName(asm, resourceName);

                using (System.IO.FileStream fs = new System.IO.FileStream(targetFile, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None))
                {
                    using (System.IO.Stream resourceStream = asm.GetManifestResourceStream(resourceName))
                    {
                        byte[] buffer = new byte[8 * 1024];
                        int len;
                        while ((len = resourceStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fs.Write(buffer, 0, len);
                        } // Whend 

                        buffer = null;
                    } // End Using resourceStream

                } // End Using fs 

            } // End if (resourceName != null)

        } // End Sub EmbeddedResourceToFile  


        public static void EmbeddedResourceToFile(string resourceName, string targetFile)
        {
            System.Reflection.Assembly asm = System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(ResourceLoader)).Assembly;
            EmbeddedResourceToFile(resourceName, targetFile);
        } // End Sub EmbeddedResourceToFile 


    } // End Class ResourceLoader 


} // End Namespace libWkHtmlToX 
