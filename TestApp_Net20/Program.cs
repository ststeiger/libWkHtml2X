
namespace TestApp_Net20
{


    static class Program
    {


        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [System.STAThread]
        static void Main(string[] args)
        {

#if false
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new Form1());
#endif 

            System.Threading.Thread th = libWkHtmlToX.Scheduler.Init(libWkHtmlToX.VisualStudioHelper.GetDllDirectory());


            string htmlData = @"<!doctype html>
<html>
<head>
<title>Test</title>
<script type=""text/javascript"">
</script>

<style type=""text/css"" media=""all"">

div
{
    background-color: red !important;
}

</style>
</head>
<body>

<div style=""display: block; width: 2000p; height: 2000px; background-color: hotpink;""></div>

</body>
</html>
";

            ////////////////////
            libWkHtmlToX.PdfGlobalSettings gs = new libWkHtmlToX.PdfGlobalSettings();


            gs.DocumentTitle = "Legende";
            // gs.PageSize = "width height";
            gs.Orientation = libWkHtmlToX.Orientation.Portrait;
            gs.OutputFormat = libWkHtmlToX.OutputFormat_t.pdf;

            gs.MarginBottom = "0px";
            gs.MarginTop = "0px";
            gs.MarginLeft = "0px";
            gs.MarginRight = "0px";

            gs.ImageQuality = 100;
            gs.UseCompression = false;

            gs.Outline = true;
            gs.Copies = 1;

            gs.ImageDPI = 600;
            gs.DPI = 96;

            gs.Width = "21cm";  // awidth.Value;
            gs.Height = "29.7cm"; //  aheight.Value;

            ////////////////////

            libWkHtmlToX.PdfObjectSettings os = new libWkHtmlToX.PdfObjectSettings();
            os.Web.DefaultEncoding = System.Text.Encoding.UTF8.WebName;
            os.Web.PrintBackground = true;
            os.Web.EnableIntelligentShrinking = false;
            // os.Web.PrintMediaType = true;
            // os.Load.ZoomFactor = 1.0;

            ////////////////////
            

            byte[] data = libWkHtmlToX.Scheduler.ConvertFile(
                delegate (ulong queueId)
                {
                    //return libWkHtmlToX.Converter.CreatePdf(htmlData, gs, os);
                    return libWkHtmlToX.Converter.CreatePdf(htmlData, null, null);
                }
            );

            if (data != null)
            {
                System.IO.File.WriteAllBytes(@"D:\Test\testfile.pdf", data);
                string pdfMarkup = System.Text.Encoding.UTF8.GetString(data);
                System.Console.WriteLine(pdfMarkup);
            } // End if (data != null) 

            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        } // End Sub Main(string[] args)


    } // End Class Program 


} // End Namespace TestApp_Net20 
