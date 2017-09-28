
namespace libWkHtmlToX
{


    public class TestProcessManager
    {


        public static void TestPng()
        {
            string f = @"D:\Stefan.Steiger\Documents\Visual Studio 2017\Projects\libWkHtml2X\TestWkHtmlToX\Libs\0.13.0-alpha\Win\x86-64";

            string html = @"<!doctype html>
<html>
<head>
<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
</head>
<body>
Test 123
<h1>فقط</h1><p>قطعا</p>
Hello world: 안녕하세요...
</body>
</html>
";

            html = VisualStudioHelper.MapSolutionPath(@"~/TestFiles/1503647812149.svg");
            html = System.IO.File.ReadAllText(html);



            // string args = "--page-height 30cm --page-width 20cm  -T 0px -B 0px -L 0px -R 0px --zoom 1.0 --disable-smart-shrinking --dpi 300 - \"simpleP.pdf\" ";
            // args = "--page-height 30cm --page-width 20cm  -T 0px -B 0px -L 0px -R 0px --zoom 1.0 --disable-smart-shrinking --dpi 300 - - ";

            byte[] pdfBytes = null;

            WkHtmlToImageCommandLineOptions opts = new WkHtmlToImageCommandLineOptions();
            opts.ExecutableDirectory = VisualStudioHelper.GetDllDirectory();

            opts.DisableSmartWidth = true;
            opts.ScreenWidth = 1024;
            opts.ScreenHeight = 768;


            using (ProcessManager p = new ProcessManager(html, opts))
            {
                p.Start();
                p.WriteStandardInput(html);
                pdfBytes = p.ReadOutputStream();

                System.Console.WriteLine("waiting");
                bool b = p.WaitForExit(5000);
                System.Console.WriteLine(b);
            } // End Using p 

            System.IO.File.WriteAllBytes(@"d:\test\pngBytes.png", pdfBytes);
            System.Console.WriteLine("Finished converting...");
        }


        public static void TestPdf()
        {
            string f = @"D:\Stefan.Steiger\Documents\Visual Studio 2017\Projects\libWkHtml2X\TestWkHtmlToX\Libs\0.13.0-alpha\Win\x86-64";

            string wkPdf = System.IO.Path.Combine(f, "wkhtmltopdf.exe");
            string wkImg = System.IO.Path.Combine(f, "wkhtmltoimage.exe");


            // string file = VisualStudioHelper.MapSolutionPath(@"~/TestFiles/simplePage.htm");
            // string html = System.IO.File.ReadAllText(file, System.Text.Encoding.UTF8);

            string html = @"<!doctype html>
<html>
<head>
<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
</head>
<body>
Test 123
<h1>فقط</h1><p>قطعا</p>
Hello world: 안녕하세요...
</body>
</html>
";
            html = VisualStudioHelper.MapSolutionPath(@"~/TestFiles/1503647812149.svg");
            html = System.IO.File.ReadAllText(html);

            // string args = "--page-height 30cm --page-width 20cm  -T 0px -B 0px -L 0px -R 0px --zoom 1.0 --disable-smart-shrinking --dpi 300 - \"simpleP.pdf\" ";
            // args = "--page-height 30cm --page-width 20cm  -T 0px -B 0px -L 0px -R 0px --zoom 1.0 --disable-smart-shrinking --dpi 300 - - ";

            byte[] pdfBytes = null;

            WkHtmlToPdfCommandLineOptions opts = new WkHtmlToPdfCommandLineOptions();
            opts.ExecutableDirectory = VisualStudioHelper.GetDllDirectory();

            opts.Orientation = Orientation_t.Portrait;
            opts.Width = "21cm";
            opts.Height = "29.7cm";
            opts.DisableSmartShrinking = true;
            


            using (ProcessManager p = new ProcessManager(html, opts))
            {
                p.Start();
                p.WriteStandardInput(html);
                pdfBytes = p.ReadOutputStream();

                System.Console.WriteLine("waiting");
                bool b = p.WaitForExit(5000);
                System.Console.WriteLine(b);
            } // End Using p 

            System.IO.File.WriteAllBytes(@"d:\test\pdfBytes.pdf", pdfBytes);
            System.Console.WriteLine("Finished converting...");
        } // End Sub Test


    } // End Class ProcessTesting 


} // End namespace libWkHtmlToX 
