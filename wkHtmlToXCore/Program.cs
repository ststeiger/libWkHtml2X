
namespace wkHtmlToXCore
{


    class Program
    {


        [System.STAThread()]
        static void Main(string[] args)
        {
            // System.Threading.Thread.CurrentThread.SetApartmentState(System.Threading.ApartmentState.STA);

            // System.IO.Compression.DeflateStream
            // System.IO.Compression.GZipStream


            string htmlData = @"<!doctype html>
<html>
<head>
<title>Test</title>
</head>
<body>

<div style=""display: block; width: 2000p; height: 2000px; background-color: hotpink;""></div>

</body>
</html>
";

            TestPDF.CreatePdf(htmlData);
            // TestPDF.CreatePdf(htmlData);


            // TestImage.CreateImg(htmlData);


            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(" --- Press any key to continue");
            System.Console.ReadKey();
        }


    }


}
