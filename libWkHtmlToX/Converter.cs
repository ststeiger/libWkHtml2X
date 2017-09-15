
namespace libWkHtml2X
{

    // https://github.com/wkhtmltopdf/wkhtmltopdf/blob/master/examples/pdf_c_api.c
    // https://github.com/wkhtmltopdf/wkhtmltopdf/blob/master/examples/image_c_api.c
    // https://wkhtmltopdf.org/libwkhtmltox/
    public static class Converter
    {


        public static byte[] CreatePdf(string htmlData)
        {
            return CreatePdf(htmlData, null, null);
        } // End Sub CreatePdf(string htmlData) 


        public static byte[] CreatePdf(string htmlData, libWkHtml2X.PdfGlobalSettings globalSettings, libWkHtml2X.PdfObjectSettings objectSettings)
        {
            byte[] output = null;

            // System.IO.File.WriteAllText(@"C:\Users\username\Desktop\nreco.imagegenerator.1.1.0\file.htm", htmlData, System.Text.Encoding.UTF8);


            // string ver = libWkHtml2X.CallsPDF.wkhtmltopdf_version();
            // System.Console.WriteLine(ver);

            int init = libWkHtml2X.CallsPDF.wkhtmltopdf_init(false);

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


        // Specify an output format to use pdf or ps, instead of looking at the extention of the output filename
        public static void CreatePdfFile(string htmlData, libWkHtml2X.PdfGlobalSettings globalSettings, libWkHtml2X.PdfObjectSettings objectSettings, string fileName)
        {
            byte[] pdfBytes = CreatePdf(htmlData, globalSettings, objectSettings);


            if (!fileName.EndsWith(".pdf", System.StringComparison.OrdinalIgnoreCase))
                fileName += ".pdf";

            System.IO.File.WriteAllBytes(fileName, pdfBytes);
        } // End Sub CreatePdfFile(string htmlData, libWkHtml2X.PdfGlobalSettings globalSettings, libWkHtml2X.PdfObjectSettings objectSettings)


        public static byte[] CreateImage(string htmlData)
        {
            return CreateImage(htmlData, null);
        }


        public static byte[] CreateImage(string htmlData, libWkHtml2X.ImageSettings imageSettings)
        {
            byte[] imgBytes = null;

            // string ver = libWkHtml2X.CallsImage.wkhtmltoimage_version();
            // System.Console.WriteLine(ver);

            int init = libWkHtml2X.CallsImage.wkhtmltoimage_init(false);

            System.IntPtr globalSettings = libWkHtml2X.CallsImage.wkhtmltoimage_create_global_settings();
            if(imageSettings != null)
                imageSettings.SetConfigValues(globalSettings);


            // string format = "svg";
            // format = "jpg";
            // format = "png";
            // format = "bmp";
            // format = ""; // nothingness

            // libWkHtml2X.CallsImage.wkhtmltoimage_set_global_setting(globalSettings, "fmt", format);
            // libWkHtml2X.CallsImage.wkhtmltoimage_set_global_setting(globalSettings, "screen 0", "5024x5768x24");
            // libWkHtml2X.CallsImage.wkhtmltoimage_set_global_setting(globalSettings, "in", "https://www.google.com/");
            // libWkHtml2X.CallsImage.wkhtmltoimage_get_global_setting(globalSettings, "", "", 0);


            // System.IntPtr data = System.IntPtr.Zero;
            System.IntPtr data = libWkHtml2X.Utf8Marshaler._staticInstance.MarshalManagedToNative(htmlData);

            System.IntPtr converter = libWkHtml2X.CallsImage.wkhtmltoimage_create_converter(globalSettings, data);

            int res = libWkHtml2X.CallsImage.wkhtmltoimage_convert(converter);
            imgBytes = libWkHtml2X.CallsImage.wkhtmltoimage_get_output(converter);

            // libWkHtml2X.CallsImage.wkhtmltoimage_destroy_converter(converter);
            libWkHtml2X.Utf8Marshaler._staticInstance.CleanUpNativeData(data);

            int deinitSuccess = libWkHtml2X.CallsImage.wkhtmltoimage_deinit();

            return imgBytes;
        } // End Sub CreateImage(string htmlData, libWkHtml2X.ImageSettings imageSettings) 


        // CreateImageFile(htmlData, imageSettings, @"D:\test_image");
        public static void CreateImageFile(string htmlData, libWkHtml2X.ImageSettings imageSettings, string fileName)
        {
            byte[] imgBytes = CreateImage(htmlData, imageSettings);

            if (!fileName.EndsWith("." + imageSettings.SupportedFormat.ToString().ToLowerInvariant(), System.StringComparison.OrdinalIgnoreCase))
                fileName += "." + imageSettings.SupportedFormat.ToString().ToLowerInvariant();

            System.IO.File.WriteAllBytes(fileName, imgBytes);
        } // End Sub CreateImageFile 


    }


}
