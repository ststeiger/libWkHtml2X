
namespace wkHtmlToXCore
{


    // https://github.com/wkhtmltopdf/wkhtmltopdf/blob/master/examples/pdf_c_api.c
    public class TestPDF
    {


        // https://wkhtmltopdf.org/libwkhtmltox/
        public static void CreatePdf(string htmlData)
        {
            CreatePdf(htmlData, null, null);
        } // End Sub CreatePdf(string htmlData) 


        public static byte[] CreatePdf(string htmlData, libWkHtml2X.PdfGlobalSettings globalSettings, libWkHtml2X.PdfObjectSettings objectSettings)
        {
            byte[] output = null;

            // System.IO.File.WriteAllText(@"C:\Users\username\Desktop\nreco.imagegenerator.1.1.0\file.htm", htmlData, System.Text.Encoding.UTF8);


            string ver = libWkHtml2X.CallsPDF.wkhtmltopdf_version();
            // System.Console.WriteLine(ver);

            int init = libWkHtml2X.CallsPDF.wkhtmltopdf_init(0);

            System.IntPtr ptrGlobalSettings = libWkHtml2X.CallsPDF.wkhtmltopdf_create_global_settings();
            // libWkHtml2X.CallsPDF.wkhtmltopdf_set_global_setting(ptrGlobalSettings, "", "");
            if (globalSettings != null)
            {
                globalSettings.SetConfigValues(ptrGlobalSettings);
            } // End if (globalSettings != null) 



            System.IntPtr ptrObjectSettings = libWkHtml2X.CallsPDF.wkhtmltopdf_create_object_settings();
            // libWkHtml2X.CallsPDF.wkhtmltopdf_set_object_setting(ptrObjectSettings, "", "");
            if (objectSettings != null)
            {
                objectSettings.SetConfigValues(ptrObjectSettings);
            } // End if (objectSettings != null) 



            System.IntPtr converter = libWkHtml2X.CallsPDF.wkhtmltopdf_create_converter(ptrGlobalSettings);
            libWkHtml2X.CallsPDF.wkhtmltopdf_add_object(converter, ptrObjectSettings, htmlData);


            libWkHtml2X.CallsPDF.wkhtmltopdf_convert(converter);

            output = libWkHtml2X.CallsPDF.wkhtmltopdf_get_output(converter);
            //System.IO.File.WriteAllBytes(@"C:\Users\username\Desktop\nreco.imagegenerator.1.1.0\file.pdf", output);


            libWkHtml2X.CallsPDF.wkhtmltopdf_destroy_converter(converter);

            // if destructor 
            // libWkHtml2X.CallsPDF.wkhtmltopdf_destroy_global_settings(globalSettings);
            // if destructor 
            // libWkHtml2X.CallsPDF.wkhtmltopdf_destroy_object_settings(objectSettings);

            int deinitSuccess = libWkHtml2X.CallsPDF.wkhtmltopdf_deinit();

            return output;
        } // End Sub CreatePdf(string htmlData, libWkHtml2X.PdfGlobalSettings globalSettings, libWkHtml2X.PdfObjectSettings objectSettings)


        public static void CreatePdfFile(string htmlData, libWkHtml2X.PdfGlobalSettings globalSettings, libWkHtml2X.PdfObjectSettings objectSettings, string fileName)
        {
            byte[] pdfBytes = CreatePdf(htmlData, globalSettings, objectSettings);


            if (!fileName.EndsWith(".pdf", System.StringComparison.InvariantCultureIgnoreCase))
                fileName += ".pdf";

            System.IO.File.WriteAllBytes(fileName, pdfBytes);
        } // End Sub CreatePdfFile(string htmlData, libWkHtml2X.PdfGlobalSettings globalSettings, libWkHtml2X.PdfObjectSettings objectSettings)


    } // End Class TestPDF 


} // End Namespace wkHtmlToXCore
