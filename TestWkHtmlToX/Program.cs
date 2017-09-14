
namespace TestWkHtmlToX
{


    static class Program
    {


        private delegate System.IntPtr wkhtmltopdf_version_t();


        public static string MapSolutionPath(string file)
        {
            file = file.Replace("/", System.IO.Path.DirectorySeparatorChar.ToString()).Replace(@"\", System.IO.Path.DirectorySeparatorChar.ToString());
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            dir = System.IO.Path.Combine(System.IO.Path.Combine(dir, ".."), "..");
            dir = System.IO.Path.GetFullPath(dir);

            if (file.StartsWith("~"))
            {
                file = file.Substring(1);

                if (file.StartsWith(System.IO.Path.DirectorySeparatorChar.ToString()) )
                    file = file.Substring(1);

                file = System.IO.Path.Combine(dir, file);
                return file;
            } // End if (file.StartsWith("~")) 

            return file;
        } // End Function MapSolutionPath 
        

        // F*** Win 8.1
        private static System.Globalization.NumberFormatInfo CreateWebNumberFormat()
        {
            //System.Globalization.NumberFormatInfo nfi = (System.Globalization.NumberFormatInfo)System.Globalization.CultureInfo.InvariantCulture.NumberFormat.Clone();
            System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();
            nfi.NumberGroupSeparator = "";
            nfi.NumberDecimalSeparator = ".";

            nfi.CurrencyGroupSeparator = "";
            nfi.CurrencyDecimalSeparator = ".";
            nfi.CurrencySymbol = "";

            return nfi;
        } // End Function SetupNumberFormatInfo






        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [System.STAThread]
        static void Main()
        {
            System.Globalization.NumberFormatInfo nfi = CreateWebNumberFormat();

            // TestAsyncMethod.EntryPoint();
#if false
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new Form1());
#endif

            // libWkHtml2X.TestScheduler.Test();

            libWkHtml2X.PdfGlobalSettings gs = new libWkHtml2X.PdfGlobalSettings();
            libWkHtml2X.PdfObjectSettings os = new libWkHtml2X.PdfObjectSettings();

            
            gs.DocumentTitle = "Legende";
            gs.Orientation = libWkHtml2X.Orientation.Portrait;
            // gs.PageSize = "width height";
            gs.OutputFormat = libWkHtml2X.OutputFormat_t.pdf;
            
            //  A4: 210 × 297 millimeters 
            gs.Width = "21.0cm";
            gs.Height = "29.7cm";


            gs.Width = "1380px";
            gs.Height = "999px";

            // viewbox
            // gs.Width = "2099px";
            // gs.Height = "1941px";

            // frame
            // gs.Width = "1655px";
            // gs.Height = "1005px";

            // screen 
            // gs.Width = "1920px";
            // gs.Height = "1160";



            //gs.Width = "800px";
            //gs.Height = "726px";

            //gs.Width = "1392px";
            //gs.Height = "1015px";


            gs.Width = "1380px";
            gs.Height = "950px";
            


            // 2098.0400390625,1940.68994140625


            gs.MarginBottom = "0px";
            gs.MarginTop = "0px";
            gs.MarginLeft = "0px";
            gs.MarginRight = "0px";

            gs.ImageQuality = 100;
            gs.UseCompression = false;
            gs.ImageDPI = 15200;
            gs.DPI = 14200;


            gs.ImageDPI = 300;
            gs.DPI = 300;

            gs.ImageDPI = 96;
            gs.DPI = 96;

            gs.ImageDPI = 300;
            gs.DPI = 119;


            gs.Outline = true;
            gs.Copies = 1;
            







            os.Web.DefaultEncoding = System.Text.Encoding.UTF8.WebName;
            os.Web.PrintBackground = true;
            os.Web.EnableIntelligentShrinking = false;
            




            //libWkHtml2X.NativeMethods.Init();
            //System.IntPtr hSO = libWkHtml2X.LibraryLoader.Load("wkhtmltox");
            //wkhtmltopdf_version_t getVersion = libWkHtml2X.LibraryLoader.LoadSymbol<wkhtmltopdf_version_t>(hSO, "wkhtmltopdf_version");

            //System.IntPtr ptrVersion = getVersion();
            //string ver = libWkHtml2X.ConstUtf8Marshaler._staticInstance.MarshalNativeToManaged(ptrVersion);
            //System.Console.WriteLine(ver);
            //libWkHtml2X.LibraryLoader.Unload(hSO);



            string htmlData = @"<!doctype html>
<html>
<head>
<title>Test</title>
<script type=""text/javascript"">
</script>

<style type=""text/css"" media=""all"">

div{
background-color: red !important;
}

</style>
</head>
<body>

<div style=""display: block; width: 2000p; height: 2000px; background-color: hotpink;""></div>

</body>
</html>
";

            
            string inputSvg = MapSolutionPath(@"~/TestFiles/1503497977772.svg");
            inputSvg = MapSolutionPath(@"~/TestFiles/1503647812149.svg");
            // inputSvg = MapSolutionPath(@"~/TestFiles/1503666084152.svg");
            // inputSvg = MapSolutionPath(@"~/TestFiles/1503666154395.svg");
            // inputSvg = MapSolutionPath(@"~/TestFiles/TestBug.svg");
            // inputSvg = MapSolutionPath(@"~/TestFiles/TestFixed.svg");
            
            // wkhtmltopdf --page-size Letter -B 0 -L 0 -R 0 -T 0 input.html output.pdf
            // https://stackoverflow.com/questions/6394905/wkhtmltopdf-what-paper-sizes-are-valid
            // 96dpi/2.54cmpi = 37.7952755906 dpcm
            

            htmlData = System.IO.File.ReadAllText(inputSvg, System.Text.Encoding.UTF8);
            //htmlData = @"<?xml version=""1.0"" encoding=""utf-8""?>" + System.Environment.NewLine + htmlData;
            
            // A document must not contain both a meta element with an http-equiv attribute in the encoding declaration state and a meta element with the charset attribute present.
            // XHTML: <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
            // XHTML5: <meta charset="utf-8" />

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

            doc.XmlResolver = null; // https://stackoverflow.com/questions/4445348/net-prevent-xmldocument-loadxml-from-retrieving-dtd
            doc.PreserveWhitespace = true;

            doc.LoadXml(htmlData);



            System.Xml.XmlNamespaceManager nsmgr = libWkHtml2X.XmlHelper.GetNamespaceManager(doc);
            string realDefaultNamespace = nsmgr.LookupNamespace("dft");

            // if (viewbox_width.EndsWith("px", System.StringComparison.InvariantCultureIgnoreCase))
                // viewbox_width = viewbox_width.Substring(0, viewbox_width.Length - 2);

            /*
            System.Xml.XmlAttribute awidth = doc.DocumentElement.Attributes["width"];
            double dw = -1;
            double.TryParse(awidth.Value, out dw);
            int w = (int)System.Math.Ceiling(dw);

            System.Xml.XmlAttribute aheight = doc.DocumentElement.Attributes["height"];
            double dh = -1;
            double.TryParse(aheight.Value, out dh);
            int h = (int)System.Math.Ceiling(dh);

            System.Xml.XmlAttribute aviewBox = doc.DocumentElement.Attributes["viewBox"];
            string[] sv = aviewBox.Value.Split( new char[]{' ', ','}, System.StringSplitOptions.RemoveEmptyEntries);
            double[] dv = new double[sv.Length];
            int[] v = new int[sv.Length];
            for (int i = 0; i < sv.Length; ++i) double.TryParse(sv[i], out dv[i]);
            for (int i = 0; i < sv.Length; ++i) v[i] = (int) System.Math.Ceiling(dv[i]);
             
            System.Console.WriteLine("w: " + w + " ,h: " + h + " ,v: " + string.Join(" ", sv) + v.ToString());
            */



            System.Xml.XmlAttribute awidth = doc.DocumentElement.Attributes["width"];
            System.Xml.XmlAttribute aheight = doc.DocumentElement.Attributes["height"];
            System.Xml.XmlAttribute aviewBox = doc.DocumentElement.Attributes["viewBox"];

            double width = -1;
            double.TryParse(awidth.Value, out width);

            double height = -1;
            double.TryParse(aheight.Value, out height);

            string[] sv = aviewBox.Value.Split(new char[] { ' ', ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            double[] dv = new double[sv.Length];
            int[] v = new int[sv.Length];
            for (int i = 0; i < sv.Length; ++i) double.TryParse(sv[i], out dv[i]);

            double viewbox_width = dv[2];
            double viewbox_height = dv[3];


            double r1 = width / viewbox_width;
            double r2 = height / viewbox_height;
            double factor = System.Math.Min(r1, r2);

            // Adjust width & height so it fits viewbox by aspect-ratio 
            double newWidth = factor * viewbox_width;
            double newHeight = factor * viewbox_height;

            double f1 = 21.0 / newWidth;
            double f2 = 29.7 / newHeight;
            double f = System.Math.Min(f1, f2);

            // Adjust width & height so it fits onto a A4/custom page 
            double ff1 = newWidth * f;
            double ff2 = newHeight * f;

#if DO_IMAGE
            // For Image
            double factorSize = 2;
            awidth.Value = ((int)System.Math.Ceiling(factorSize * viewbox_width)).ToString("N2", nfi) + "px";
            aheight.Value = ((int)System.Math.Ceiling(factorSize * viewbox_height)).ToString("N2", nfi) + "px";
#else
            // For PDF
            awidth.Value = ff1.ToString("N2", System.Globalization.CultureInfo.InvariantCulture) + "cm";
            aheight.Value = ff2.ToString("N2", System.Globalization.CultureInfo.InvariantCulture) + "cm";
#endif



            gs.Width = "21.0cm";
            gs.Height = "29.7cm";


            gs.Width = awidth.Value;
            gs.Height = aheight.Value;
            // gs.Height = "23.79cm"; 
            // gs.Height = "23.9cm"; // 23.66cm

            //gs.Width = aheight.Value;
            //gs.Height = awidth.Value;

            //gs.Width = "1cm";
            //gs.Height = "5cm";



            System.Console.WriteLine(gs.PageSize);
            // gs.PageSize = gs.Width + " " + gs.Height;



            /*

            gs.DPI = 1500;
            gs.ImageDPI = gs.DPI;
            // 96/2.54 - 37.7952755906

            // doc.DocumentElement.Attributes.Remove(doc.DocumentElement.Attributes["viewBox"]);
            // doc.DocumentElement.Attributes["viewBox"].Value = w.ToString() + "0cm 0cm 2098.04cm 2363.85cm";
            // doc.DocumentElement.Attributes["width"].Value = w.ToString() + "cm";
            // doc.DocumentElement.Attributes["height"].Value = h.ToString() + "cm";

            // gs.Width = doc.DocumentElement.Attributes["width"].Value + "px";
            // gs.Height = doc.DocumentElement.Attributes["height"].Value + "px";


            // gs.ImageDPI = 150;
            gs.DPI = 1300;
            gs.ImageDPI = gs.DPI;

            // gs.Resolution = "1024x768";
            // gs.Resolution = "1280x1024";

            // double ddd = 1.0 / 39.37; System.Console.WriteLine(ddd); = 25.400050800101603


            gs.Width = (1380 * 2).ToString() + "px";
            gs.Height = (950 * 2).ToString() + "px";

            // 2098.04 2363.85"
            gs.Width = (1380.0 / (gs.DPI / 25.4)).ToString() + "mm";
            gs.Height = (950.0 / (gs.DPI / 25.4)).ToString() + "mm";
            */
            
            


            string xml = doc.OuterXml;

            string simplePage2 = MapSolutionPath(@"~/TestFiles/simplePage2.htm");
            simplePage2 = System.IO.File.ReadAllText(simplePage2, System.Text.Encoding.UTF8);
            xml = string.Format(simplePage2, xml);

            System.IO.File.WriteAllText(@"d:\test_lines.svg", xml, new System.Text.UTF8Encoding(false));
            System.IO.File.WriteAllText(@"d:\test_lines.htm", xml, new System.Text.UTF8Encoding(false));

            // System.Console.WriteLine(xml);

            /*
            inputSvg = MapSolutionPath(@"~/TestFiles/simplePage.htm");
            xml = System.IO.File.ReadAllText(inputSvg, System.Text.Encoding.UTF8);

            gs = new libWkHtml2X.PdfGlobalSettings();

            gs.Orientation = libWkHtml2X.Orientation.Portrait;
            gs.Width = "20cm";
            gs.Height = "30cm";

            gs.MarginBottom = "0px";
            gs.MarginTop = "0px";
            gs.MarginLeft = "0px";
            gs.MarginRight = "0px";
            */

            gs.ImageDPI = 600;
            gs.DPI = 96;

            // gs.PageSize = "20cm 10cm";
            // gs.Resolution = "2000x1000";

            // os.Header.Spacing = 0;
            // os.Footer.Spacing = 0;



            os = new libWkHtml2X.PdfObjectSettings();
            os.Web.EnableIntelligentShrinking = false;
            //os.Web.PrintMediaType = true;
            // os.Load.ZoomFactor = 1.0;

            // https://github.com/wkhtmltopdf/wkhtmltopdf/blob/master/docs/usage/wkhtmltopdf.txt
            // https://stackoverflow.com/questions/6057781/wkhtmltopdf-with-full-page-background
            // https://stackoverflow.com/questions/4550612/wkhtmltopdf-cannot-convert-local-file
            // https://github.com/wkhtmltopdf/wkhtmltopdf/issues/2590
            // https://stackoverflow.com/questions/37454957/wkhtmltopdf-fit-output-to-whole-page-width
            // https://stackoverflow.com/questions/33528780/any-way-to-reduce-file-size-using-wkhtmltopdf

            wkHtmlToXCore.TestPDF.CreatePdfFile(xml, gs, os, @"D:\Test_Lines.pdf");
#if DO_IMAGE 

            htmlData = xml;

            libWkHtml2X.ImageSettings imageSettings = new libWkHtml2X.ImageSettings();

            imageSettings.SupportedFormat = libWkHtml2X.SupportedFormat.PNG;
            imageSettings.ScreenWidth = (int)System.Math.Ceiling(factorSize * viewbox_width);
            imageSettings.SmartWidth = false;
            imageSettings.Quality = 50;
            
            imageSettings.Web.DefaultEncoding = System.Text.Encoding.UTF8.WebName;
            imageSettings.Web.EnableIntelligentShrinking = false;
            imageSettings.Web.PrintBackground = true;

            
            // imageSettings.ScreenWidth = 5000;
            /*
            double stretch_factor = 5.5;
            htmlData = htmlData
                .Replace(@"width=""1380""", @"width=""" + ((int) System.Math.Ceiling(1380 * stretch_factor)).ToString() 
                + @"""")
                .Replace(@"height=""950""", @"height=""" + ((int)System.Math.Ceiling((950 * stretch_factor))).ToString() 
                + @"""");
            */
            
            /*
            double stretch_factor = 1.0;
            htmlData = htmlData
                .Replace(@"width=""21.00cm""", @"width=""" + ((int)System.Math.Ceiling(2100 * stretch_factor)).ToString()
                + @"""")
                .Replace(@"height=""23.66cm""", @"height=""" + ((int)System.Math.Ceiling((2366 * stretch_factor))).ToString()
                + @"""");
            */


            // https://stackoverflow.com/questions/20577991/wkhtmltoimage-mention-size-when-taking-screenshot
            // wkhtmltoimage.exe"  --width 1024 --height 768 http://www.google.com/ D:\example.jpg 
            wkHtmlToXCore.TestImage.CreateImageFile(htmlData, imageSettings, @"D:\wktest.png");
#endif

            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        } // End Sub Main 


    } // End Class Program 


} // End Namespace TestWkHtmlToX 
