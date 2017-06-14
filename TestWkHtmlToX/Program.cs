
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
#if false
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new Form1());
#endif
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

            wkHtmlToXCore.TestPDF.CreatePdf(htmlData);



            libWkHtml2X.WebPageSpecificSettings wss = new libWkHtml2X.WebPageSpecificSettings();
            System.Type t = typeof(libWkHtml2X.WebPageSpecificSettings);

            



            System.Reflection.FieldInfo fi = t.GetField("PrintBackground");
            
            // libWkHtml2X.wkHtmlOptionNameAttribute att = (libWkHtml2X.wkHtmlOptionNameAttribute)fi.GetCustomAttributes(typeof(libWkHtml2X.wkHtmlOptionNameAttribute), false)[0];
            libWkHtml2X.wkHtmlOptionNameAttribute att = libWkHtml2X.AttributeHelper.GetAttribute<libWkHtml2X.wkHtmlOptionNameAttribute>(fi);


            string aname = libWkHtml2X.AttributeHelper.GetAttributValue<libWkHtml2X.wkHtmlOptionNameAttribute, string>(fi, delegate(libWkHtml2X.wkHtmlOptionNameAttribute a) { return a.Name; });
            string aname2 = libWkHtml2X.AttributeHelper.GetAttributValue<libWkHtml2X.wkHtmlOptionNameAttribute, string>(fi, a => a.Name);
            System.Console.WriteLine(aname2);


            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        } // End Sub Main 


    } // End Class Program 


} // End Namespace TestWkHtmlToX 
