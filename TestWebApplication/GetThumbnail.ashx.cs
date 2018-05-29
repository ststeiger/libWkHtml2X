using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApplication
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für GetThumbnail
    /// </summary>
    public class GetThumbnail : IHttpHandler
    {



        public static byte[] GeneratePNG()
        {
            string html = System.IO.File.ReadAllText(@"d:\debugSVG.htm", System.Text.Encoding.UTF8);

            int w = 204;
            int h = 265;

            byte[] pngBytes = null;

            libWkHtmlToX.WkHtmlToImageCommandLineOptions opts = new libWkHtmlToX.WkHtmlToImageCommandLineOptions();
            opts.ExecutableDirectory = libWkHtmlToX.VisualStudioHelper.GetDllDirectory();
            opts.ExecutableDirectory = @"D:\Stefan.Steiger\Documents\Visual Studio 2017\TFS\COR-Basic-V4\Portal\Portal_Convert\External\wkhtmltox\x86-32";


            opts.DisableSmartWidth = true;
            opts.ScreenWidth = System.Convert.ToInt32(System.Math.Ceiling((double)w));
            opts.ScreenHeight = System.Convert.ToInt32(System.Math.Ceiling((double)h));


            using (libWkHtmlToX.ProcessManager p = new libWkHtmlToX.ProcessManager(opts))
            {
                p.Start();
                p.WriteStandardInput(html);
                pngBytes = p.ReadOutputStream();

                System.Console.WriteLine("waiting");
                bool b = p.WaitForExit(5000);
                System.Console.WriteLine(b);
            } // End Using p 


            System.IO.File.WriteAllBytes(@"d:\zomg.png", pngBytes);
            System.Console.WriteLine("test");
            return pngBytes;
        }


        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/png";
            byte[] pngBytes = GeneratePNG();
            context.Response.BinaryWrite(pngBytes);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}