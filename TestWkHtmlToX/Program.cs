
namespace TestWkHtmlToX
{


    static class Program
    {


        private delegate System.IntPtr wkhtmltopdf_version_t();


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [System.STAThread]
        static void Main()
        {

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

            string inputSvg = System.IO.Path.GetFullPath(
                System.IO.Path.Combine(
                    System.IO.Path.Combine(
                        System.IO.Path.Combine(
                            System.IO.Path.GetDirectoryName(typeof(Program).Assembly.Location)
                            , "../..")
                    , "TestFiles"
                )
                //, "TestBug.svg")
                , "TestFixed.svg")
            );


            inputSvg = "D:/Stefan.Steiger/Documents/Downloads/1503492416014.svg";
            inputSvg = @"D:/Stefan.Steiger/Documents/Downloads/1503497977772.svg";


            

            htmlData = System.IO.File.ReadAllText(inputSvg, System.Text.Encoding.UTF8);
            htmlData = @"<?xml version=""1.0"" encoding=""utf-8""?>" + System.Environment.NewLine + htmlData;
           
            // wkHtmlToXCore.TestPDF.CreatePdf(htmlData, gs, os);

            
            libWkHtml2X.ImageSettings imageSettings = new libWkHtml2X.ImageSettings();

            imageSettings.Quality = 50;
            imageSettings.Web.PrintBackground = true;
            imageSettings.Web.EnableIntelligentShrinking = false;
            imageSettings.Web.DefaultEncoding = System.Text.Encoding.UTF8.WebName;
            imageSettings.SupportedFormat = libWkHtml2X.SupportedFormat.PNG;
            


            // imageSettings.ScreenWidth = 5000;


            double factor = 5.5;
            htmlData = htmlData
                .Replace(@"width=""1380""", @"width=""" + ((int) System.Math.Ceiling(1380 * factor)).ToString() 
                + @"""")
                .Replace(@"height=""950""", @"height=""" + ((int)System.Math.Ceiling((950 * factor))).ToString() 
                + @"""");

            wkHtmlToXCore.TestImage.CreateImg(htmlData, imageSettings);
            /**/ 


            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        } // End Sub Main 


    } // End Class Program 


} // End Namespace TestWkHtmlToX 
