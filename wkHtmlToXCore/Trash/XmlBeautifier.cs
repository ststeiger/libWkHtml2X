
namespace wkHtmlToXCore.Trash
{


    public class XmlBeautifier
    {


        public static void Beautify()
        {

            string origSvgName = "1506416356672";
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            // doc.XmlResolver = null; // https://github.com/dotnet/corefx/issues/10322
            // doc.PreserveWhitespace = true; // no, false, as this interferes with indented...

            using (System.IO.Stream strm = System.IO.File.Open(@"D:\Test\Log\" + origSvgName + ".svg", System.IO.FileMode.Open))
            {
                doc.Load(strm);
            }

            //using (System.Xml.XmlTextWriter xwrite = new System.Xml.XmlTextWriter(beautifiedFileName, null))
            //{
            //    xwrite.Formatting = System.Xml.Formatting.Indented;
            //    doc.Save(xwrite);
            //}

            string beautifiedFileName = @"D:\Test\Log\" + origSvgName + "_pretty.svg";
            using (System.IO.FileStream fs = System.IO.File.OpenWrite(beautifiedFileName))
            {
                System.Xml.XmlWriterSettings xws = new System.Xml.XmlWriterSettings();
                xws.Indent = true;
                xws.Encoding = System.Text.Encoding.UTF8;
                xws.NewLineChars = System.Environment.NewLine;

                using (System.Xml.XmlWriter xw = System.Xml.XmlWriter.Create(fs, xws))
                {
                    doc.Save(xw);
                } // End Using xw 

            } // End Using fs 

        } // End Sub void Beautify 


    } // End Class XmlBeautifier 


} // End Namespace wkHtmlToXCore.Trash 
