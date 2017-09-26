
namespace wkHtmlToXCore
{


    class Program
    {

        [System.STAThread()]
        static void Main(string[] args)
        {
            libWkHtmlToX.ResourceLoader.EmbeddedResourceToFile("", "tgt");


            // System.Threading.Thread.CurrentThread.SetApartmentState(System.Threading.ApartmentState.STA);
            // System.Threading.Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA;
            long coInit = libWkHtmlToX.CoInitHelper.CoInitialize();
            // long oleInit = CoInitHelper.InitOle();
            // System.Console.WriteLine(coInit);
            // System.Console.WriteLine(oleInit);

            // System.IO.Compression.DeflateStream
            // System.IO.Compression.GZipStream


            libWkHtmlToX.TestScheduler.Test();


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

            // https://stackoverflow.com/questions/20525554/pyside-qt-could-not-initialize-ole-error-80010106
            // TestPDF.CreatePdf(htmlData);
            // TestPDF.CreatePdf(htmlData);

            libWkHtmlToX.Converter.CreatePdfFile(htmlData, null, null, @"D:\test_pdf.pdf");
            libWkHtmlToX.Converter.CreatePdfFile(htmlData, null, null, @"D:\test_pdf1.pdf");
            libWkHtmlToX.Converter.CreatePdfFile(htmlData, null, null, @"D:\test_pdf2.pdf");



            libWkHtmlToX.ImageSettings imageSettings = new libWkHtmlToX.ImageSettings();

            imageSettings.SupportedFormat = libWkHtmlToX.SupportedFormat_t.PNG;
            // imageSettings.ScreenWidth = (int)System.Math.Ceiling(factorSize * viewbox_width); // Width will be fixed at small sizes, if this property isn't set
            imageSettings.SmartWidth = false;
            imageSettings.Quality = 50;

            imageSettings.Web.DefaultEncoding = System.Text.Encoding.UTF8.WebName;
            imageSettings.Web.EnableIntelligentShrinking = false;
            imageSettings.Web.PrintBackground = true;


            // https://stackoverflow.com/questions/20577991/wkhtmltoimage-mention-size-when-taking-screenshot
            // wkhtmltoimage.exe"  --width 1024 --height 768 http://www.google.com/ D:\example.jpg 

            libWkHtmlToX.Converter.CreateImageFile(htmlData, imageSettings, @"D:\test_image.png");
            libWkHtmlToX.Converter.CreateImageFile(htmlData, imageSettings, @"D:\test_image1.png");
            libWkHtmlToX.Converter.CreateImageFile(htmlData, imageSettings, @"D:\test_image2.png");
            libWkHtmlToX.Converter.CreateImageFile(htmlData, imageSettings, @"D:\test_image2.jpg");

            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(" --- Press any key to continue");
            System.Console.ReadKey();
        } // End Sub Main 


    } // End Class Program 


} // End Namespace wkHtmlToXCore 
