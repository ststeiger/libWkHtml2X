
namespace wkHtmlToXCore
{


    // https://github.com/wkhtmltopdf/wkhtmltopdf/blob/master/examples/pdf_c_api.c
    public class TestPDF
    {


        public static void CreatePdf(string htmlData)
        {
            // System.IO.File.WriteAllText(@"C:\Users\username\Desktop\nreco.imagegenerator.1.1.0\file.htm", htmlData, System.Text.Encoding.UTF8);

            string ver = libWkHtml2X.CallsPDF.wkhtmltopdf_version();
            int init = libWkHtml2X.CallsPDF.wkhtmltopdf_init(0);

            System.IntPtr globalSettings = libWkHtml2X.CallsPDF.wkhtmltopdf_create_global_settings();
            // libWkHtml2X.CallsPDF.wkhtmltopdf_set_global_setting(globalSettings, "", "");

            System.IntPtr objectSettings = libWkHtml2X.CallsPDF.wkhtmltopdf_create_object_settings();
            // libWkHtml2X.CallsPDF.wkhtmltopdf_set_object_setting(objectSettings, "", "");


            System.IntPtr converter = libWkHtml2X.CallsPDF.wkhtmltopdf_create_converter(globalSettings);
            libWkHtml2X.CallsPDF.wkhtmltopdf_add_object(converter, objectSettings, htmlData);


            libWkHtml2X.CallsPDF.wkhtmltopdf_convert(converter);

            byte[] output = libWkHtml2X.CallsPDF.wkhtmltopdf_get_output(converter);
            System.IO.File.WriteAllBytes(@"C:\Users\username\Desktop\nreco.imagegenerator.1.1.0\file.pdf", output);

            libWkHtml2X.CallsPDF.wkhtmltopdf_destroy_converter(converter);
            libWkHtml2X.CallsPDF.wkhtmltopdf_destroy_global_settings(globalSettings);
            libWkHtml2X.CallsPDF.wkhtmltopdf_destroy_object_settings(objectSettings);

            int deinitSuccess = libWkHtml2X.CallsPDF.wkhtmltopdf_deinit();

            System.Console.WriteLine(ver);
        }


    }


}
