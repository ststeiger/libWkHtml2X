
namespace libWkHtml2X
{


    public class ProcessTesting
    {


        public static void Test()
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


            string args = "--page-height 30cm --page-width 20cm  -T 0px -B 0px -L 0px -R 0px --zoom 1.0 --disable-smart-shrinking --dpi 300 - \"simpleP.pdf\" ";
            args = "--page-height 30cm --page-width 20cm  -T 0px -B 0px -L 0px -R 0px --zoom 1.0 --disable-smart-shrinking --dpi 300 - - ";

            byte[] pdfBytes = null;

            using (ProcessManager p = new ProcessManager(html, wkPdf, args))
            {
                p.Start();
                p.WriteStandardInput(html);
                pdfBytes = p.ReadOutputStream();

                System.Console.WriteLine("waiting");
                bool b = p.WaitForExit(5000);
                System.Console.WriteLine(b);
            } // End Using p 

            System.IO.File.WriteAllBytes(@"d:\test\pdfBytes.pdf", pdfBytes);
        } // End Sub Test


    } // End Class ProcessTesting 


} // End Namespace libWkHtml2X 
