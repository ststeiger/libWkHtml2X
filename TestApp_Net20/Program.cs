
namespace TestApp_Net20
{


    static class Program
    {

        private static void SetAttribute(System.Xml.XmlDocument svg, string attributeName, string value)
        {
            if (svg.DocumentElement.HasAttribute(attributeName))
            {
                // svg.DocumentElement.RemoveAttribute(attributeName);
                svg.DocumentElement.Attributes[attributeName].Value = value;
                return;
            }
            
            System.Xml.XmlAttribute att = svg.CreateAttribute(attributeName);
            att.Value = value;
            svg.DocumentElement.Attributes.Append(att);
        }

        public static System.Collections.Generic.Dictionary<string, string> StyleToDict(string style)
        {
            System.Collections.Generic.Dictionary<string, string> nvc = new System.Collections.Generic.Dictionary<string, string>();

            if (style == null)
                return nvc;

            string[] keyvaluepair = style.Split(';');

            for (int i = 0; i < keyvaluepair.Length; ++i)
            {
                keyvaluepair[i] = keyvaluepair[i].Trim();
                int pos = keyvaluepair[i].IndexOf(':');
                if (pos != -1)
                {
                    string key = keyvaluepair[i].Substring(0, pos).Trim();
                    string value = keyvaluepair[i].Substring(pos+1).Trim();
                    nvc[key] = value;
                }

            }
            
            return nvc;
        }

        public static string DictToStyle(System.Collections.Generic.Dictionary<string, string> styles)
        {
            string style = "";

            foreach (System.Collections.Generic.KeyValuePair<string,string> kvp in styles)
            {
                style += kvp.Key + ": " + kvp.Value + "; ";
            }

            return style;
        }

        public static void RemoveBgAndBorder(System.Xml.XmlNode node)
        {
            if (node.Attributes["style"] != null)
            {
                string style = node.Attributes["style"].Value;
                System.Collections.Generic.Dictionary<string, string> styles = StyleToDict(style);

                if (styles.ContainsKey("background-color"))
                {
                    styles.Remove("background-color");
                    styles.Remove("border");
                    style = DictToStyle(styles);
                    node.Attributes["style"].Value = style;
                }
            }

        }

        public static void Leg()
        {
            string svgg = @"D:\Stefan.Steiger\Documents\Visual Studio 2017\Projects\libWkHtml2X\TestApp_Net20\Resources\1512467650594.svg";
            svgg = System.IO.File.ReadAllText(svgg, System.Text.Encoding.UTF8);

            string legendTemplate = @"D:\Stefan.Steiger\Documents\Visual Studio 2017\Projects\libWkHtml2X\TestApp_Net20\Resources\theLegend.htm";
            legendTemplate = System.IO.File.ReadAllText(legendTemplate, System.Text.Encoding.UTF8);

            System.Xml.XmlDocument svg = new System.Xml.XmlDocument();
            svg.XmlResolver = null;
            svg.LoadXml(svgg);

            SetAttribute(svg, "width", "100%");
            SetAttribute(svg, "height", "100%");
            //SetAttribute(svg, "preserveAspectRatio", "xMinYMin");
            SetAttribute(svg, "preserveAspectRatio", "xMidYMid");


            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.XmlResolver = null;
            doc.LoadXml(legendTemplate);

            System.Xml.XmlNamespaceManager nsm = 
                new System.Xml.XmlNamespaceManager(doc.NameTable);
            nsm.AddNamespace("dft", "http://www.w3.org/1999/xhtml");


            //System.Xml.XmlNode titleNode = doc.SelectSingleNode("//*[local-name()='div'][@id='title']");
            System.Xml.XmlNode title = doc.SelectSingleNode("//*[@id='title']");
            title.FirstChild.InnerText = "Adliswil - Soodring 6 - Upper floor 3 - Space Type";

            System.Xml.XmlNode logo = doc.SelectSingleNode("//*[@id='logo']");
            
            System.Xml.XmlNode printdate = doc.SelectSingleNode("//*[@id='printdate']");
            string pd = System.DateTime.Now.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            printdate.FirstChild.InnerText = pd;

            System.Xml.XmlNode Legend = doc.SelectSingleNode("//*[@id='Legend']");


            System.Xml.XmlNode drawing = doc.SelectSingleNode("//*[@id='drawing']");
            drawing.InnerXml = svg.DocumentElement.OuterXml;


            System.Xml.XmlNode drawing_border = doc.SelectSingleNode("//*[@id='drawing_border']");


            System.Console.WriteLine(title);
            System.Console.WriteLine(logo);
            System.Console.WriteLine(printdate);
            System.Console.WriteLine(Legend);
            System.Console.WriteLine(drawing);
            System.Console.WriteLine(drawing_border);


            RemoveBgAndBorder(title);
            RemoveBgAndBorder(logo);
            RemoveBgAndBorder(Legend);
            RemoveBgAndBorder(printdate);
            RemoveBgAndBorder(drawing);
            RemoveBgAndBorder(drawing_border);


            using (System.Xml.XmlTextWriter xtw = new System.Xml.XmlTextWriter(@"d:\test5.htm", System.Text.Encoding.UTF8))
            {
                xtw.Formatting = System.Xml.Formatting.Indented; // if you want it indented
                xtw.Indentation = 4;
                xtw.IndentChar = ' ';

                doc.Save(xtw);
                xtw.Flush();
                xtw.Close();
            } // End Using xtw


        }



        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [System.STAThread]
        static void Main(string[] args)
        {
            Leg();




            // libWkHtmlToX.TestProcessManager.TestPdf();
            libWkHtmlToX.TestProcessManager.TestPng();

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



            string fn = "1503497977772.svg";
            fn = "1506332283409.svg";

            string fileName = libWkHtmlToX.VisualStudioHelper.MapSolutionPath("~TestFiles/" + fn);
            htmlData = System.IO.File.ReadAllText(fileName, System.Text.Encoding.UTF8);



            ////////////////////
            libWkHtmlToX.PdfGlobalSettings gs = new libWkHtmlToX.PdfGlobalSettings();


            gs.DocumentTitle = "Legende";
            // gs.PageSize = "width height";
            gs.Orientation = libWkHtmlToX.Orientation_t.Portrait;
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
            th.Abort();
        } // End Sub Main(string[] args)


        private static System.Globalization.NumberFormatInfo CreateWebNumberFormat()
        {
            return new System.Globalization.NumberFormatInfo
            {
                NumberGroupSeparator = "",
                NumberDecimalSeparator = ".",
                CurrencyGroupSeparator = "",
                CurrencyDecimalSeparator = ".",
                CurrencySymbol = ""
            };
        }



        public static System.IO.Stream FirstPageOnly(byte[] pdfBytes)
        {
            System.IO.MemoryStream msOutput = new System.IO.MemoryStream();

            // using(System.IO.MemoryStream msOutput = new System.IO.MemoryStream())
            //{

            using (System.IO.MemoryStream msInput = new System.IO.MemoryStream(pdfBytes))
            {

                using (PdfSharp.Pdf.PdfDocument pdfOutputDoc = new PdfSharp.Pdf.PdfDocument())
                {

                    using (PdfSharp.Pdf.PdfDocument pdfInputDocument = PdfSharp.Pdf.IO.PdfReader.Open(msInput, PdfSharp.Pdf.IO.PdfDocumentOpenMode.Import))
                    {

                        if (pdfInputDocument.Pages.Count > 0)
                        {
                            pdfOutputDoc.AddPage(pdfInputDocument.Pages[0]);
                        } // End if (pdfInputDocument.Pages.Count > 0) 

                    } // End Using pdfInputDocument

                    pdfOutputDoc.Save(msOutput);
                } // End Using pdfOutputDoc

            } // End Using msInput

            // } //End Using ' msOutput

            return msOutput;
        } // End Sub FirstPageOnly 


        // [OperationContract, WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]

        // [OperationContract]
        // [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        // [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public static System.IO.Stream toPDF2(string File, bool Current_view)
        {
            // Portal.Converter.Log tLog = new Portal.Converter.Log();

            System.IO.Stream retValue = null;


            //File = System.IO.File.ReadAllText(@"", System.Text.Encoding.UTF8);

            
            double paper_maxWidth = 21.0;
            double paper_maxHeight = 29.7;

            try
            {
                System.Globalization.NumberFormatInfo nfi = CreateWebNumberFormat();
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.XmlResolver = null;
                doc.PreserveWhitespace = true;
                doc.LoadXml(File);

                System.Xml.XmlAttribute awidth = doc.DocumentElement.Attributes["width"];
                System.Xml.XmlAttribute aheight = doc.DocumentElement.Attributes["height"];
                System.Xml.XmlAttribute aviewBox = doc.DocumentElement.Attributes["viewBox"];

                double width = -1.0;
                double.TryParse(awidth.Value, out width);

                double height = -1.0;
                double.TryParse(aheight.Value, out height);

                string[] sv = aviewBox.Value.Split(new char[] { ' ', ','}, System.StringSplitOptions.RemoveEmptyEntries);

                double viewbox_width;
                double viewbox_height;
                double factor;

                checked
                {
                    double[] dv = new double[sv.Length - 1 + 1];
                    int[] v = new int[sv.Length - 1 + 1];
                    int num = sv.Length - 1;
                    for (int i = 0; i <= num; i++)
                    {
                        double.TryParse(sv[i], out dv[i]);
                    } // Next i 

                    double viewbox_x = dv[0];
                    double viewbox_y = dv[1];
                    viewbox_width = dv[2];
                    viewbox_height = dv[3];

                    double r = width / viewbox_width;
                    double r2 = height / viewbox_height;
                    factor = System.Math.Min(r, r2);
                }

                double newWidth = factor * viewbox_width;
                double newHeight = factor * viewbox_height;

                double fWidth = paper_maxWidth / newWidth;
                double fHeight = paper_maxHeight / newHeight;
                double fPaperSizeFactor = System.Math.Min(fWidth, fHeight);

                double newPaperWidth = newWidth * fPaperSizeFactor;
                double newPaperHeight = newHeight * fPaperSizeFactor;

                awidth.Value = newPaperWidth.ToString("N2", nfi) + "cm";
                aheight.Value = newPaperHeight.ToString("N2", nfi) + "cm";

                string svg = doc.OuterXml;
                string svgHtmlWrapper = libWkHtmlToX.ResourceLoader.ReadEmbeddedResource(typeof(Program).Assembly, ".SvgHtmlWrapper.htm");


                string html = string.Format(svgHtmlWrapper, svg);
                // System.IO.File.WriteAllText(@"D:\Test\svgHtmlWrapper.htm", html);


                libWkHtmlToX.PdfGlobalSettings pdfGlobalSettings = new libWkHtmlToX.PdfGlobalSettings();
                pdfGlobalSettings.DocumentTitle = "Legende";
                pdfGlobalSettings.Orientation = libWkHtmlToX.Orientation_t.Portrait;
                pdfGlobalSettings.OutputFormat = libWkHtmlToX.OutputFormat_t.pdf;

                pdfGlobalSettings.MarginBottom = "0px";
                pdfGlobalSettings.MarginTop = "0px";
                pdfGlobalSettings.MarginLeft = "0px";
                pdfGlobalSettings.MarginRight = "0px";

                pdfGlobalSettings.ImageQuality = 50;
                pdfGlobalSettings.UseCompression = false;

                pdfGlobalSettings.Outline = true;
                pdfGlobalSettings.Copies = 1;

                pdfGlobalSettings.ImageDPI = 600;
                // pdfGlobalSettings.DPI = 96;
                pdfGlobalSettings.DPI = 300;

                pdfGlobalSettings.Width = awidth.Value;
                pdfGlobalSettings.Height = aheight.Value;


                libWkHtmlToX.PdfObjectSettings pdfObjectSettings = new libWkHtmlToX.PdfObjectSettings();
                pdfObjectSettings.Web.DefaultEncoding = System.Text.Encoding.UTF8.WebName;
                pdfObjectSettings.Web.EnableIntelligentShrinking = false;
                // pdfObjectSettings.Web.PrintBackground = True
                // pdfObjectSettings.Web.PrintMediaType = true;
                // pdfObjectSettings.Load.ZoomFactor = 1.0;

                byte[] pdfBytes = libWkHtmlToX.Scheduler.ConvertFile(
                    delegate (ulong queueId)
                    {
                        return libWkHtmlToX.Converter.CreatePdf(html, pdfGlobalSettings, pdfObjectSettings);
                    }
                );

                retValue = FirstPageOnly(pdfBytes);
            }
            catch (System.Exception ex)
            {
                // tLog.addMessage(ex)
                retValue = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes("@@Error: " + ex.Message));
            }
            finally
            {
                // tLog.Write();
            }

            return retValue;
        } // End Sub toPDF2 


    } // End Class Program 


} // End Namespace TestApp_Net20 
