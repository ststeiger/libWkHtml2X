
namespace wkHtmlToXCore
{


    class Program
    {

        

        [System.STAThread()]
        static void Main(string[] args)
        {
            // System.Threading.Thread.CurrentThread.SetApartmentState(System.Threading.ApartmentState.STA);
            // System.Threading.Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA;

            long coInit = CoInitHelper.CoInitialize();
            // long oleInit = CoInitHelper.InitOle();
            // System.Console.WriteLine(coInit);
            // System.Console.WriteLine(oleInit);

            // System.IO.Compression.DeflateStream
            // System.IO.Compression.GZipStream


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


            TestImage.CreateImg(htmlData);


            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(" --- Press any key to continue");
            System.Console.ReadKey();
        }


    }


}
