
namespace libWkHtmlToX
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


        public static byte[] CreatePdf(string htmlData, libWkHtmlToX.PdfGlobalSettings globalSettings, libWkHtmlToX.PdfObjectSettings objectSettings)
        {
            byte[] output = null;
            
            if (string.IsNullOrEmpty(htmlData) || htmlData.Trim() == string.Empty)
                throw new System.IO.InvalidDataException("Invalid input data...");

            // System.IO.File.WriteAllText(@"C:\Users\username\Desktop\nreco.imagegenerator.1.1.0\file.htm", htmlData, System.Text.Encoding.UTF8);


            // string ver = libWkHtmlToX.CallsPDF.wkhtmltopdf_version();
            // System.Console.WriteLine(ver);

            int init = libWkHtmlToX.CallsPDF.wkhtmltopdf_init(false);

            System.IntPtr ptrGlobalSettings = libWkHtmlToX.CallsPDF.wkhtmltopdf_create_global_settings();
            // libWkHtmlToX.CallsPDF.wkhtmltopdf_set_global_setting(ptrGlobalSettings, "", "");
            if (globalSettings != null)
            {
                globalSettings.SetConfigValues(ptrGlobalSettings);
            } // End if (globalSettings != null) 



            System.IntPtr ptrObjectSettings = libWkHtmlToX.CallsPDF.wkhtmltopdf_create_object_settings();
            // libWkHtmlToX.CallsPDF.wkhtmltopdf_set_object_setting(ptrObjectSettings, "", "");
            if (objectSettings != null)
            {
                objectSettings.SetConfigValues(ptrObjectSettings);
            } // End if (objectSettings != null) 



            System.IntPtr converter = libWkHtmlToX.CallsPDF.wkhtmltopdf_create_converter(ptrGlobalSettings);
            libWkHtmlToX.CallsPDF.wkhtmltopdf_add_object(converter, ptrObjectSettings, htmlData);


            libWkHtmlToX.CallsPDF.wkhtmltopdf_convert(converter);

            output = libWkHtmlToX.CallsPDF.wkhtmltopdf_get_output(converter);
            //System.IO.File.WriteAllBytes(@"C:\Users\username\Desktop\nreco.imagegenerator.1.1.0\file.pdf", output);


            libWkHtmlToX.CallsPDF.wkhtmltopdf_destroy_converter(converter);

            // if destructor 
            // libWkHtmlToX.CallsPDF.wkhtmltopdf_destroy_global_settings(globalSettings);
            // if destructor 
            // libWkHtmlToX.CallsPDF.wkhtmltopdf_destroy_object_settings(objectSettings);

            int deinitSuccess = libWkHtmlToX.CallsPDF.wkhtmltopdf_deinit();

            return output;
        } // End Sub CreatePdf(string htmlData, libWkHtmlToX.PdfGlobalSettings globalSettings, libWkHtmlToX.PdfObjectSettings objectSettings)


        // Specify an output format to use pdf or ps, instead of looking at the extention of the output filename
        public static void CreatePdfFile(string htmlData, libWkHtmlToX.PdfGlobalSettings globalSettings, libWkHtmlToX.PdfObjectSettings objectSettings, string fileName)
        {
            byte[] pdfBytes = CreatePdf(htmlData, globalSettings, objectSettings);

            if (!fileName.EndsWith(".pdf", System.StringComparison.OrdinalIgnoreCase))
                fileName += ".pdf";

            System.IO.File.WriteAllBytes(fileName, pdfBytes);
        } // End Sub CreatePdfFile(string htmlData, libWkHtmlToX.PdfGlobalSettings globalSettings, libWkHtmlToX.PdfObjectSettings objectSettings)


        public static byte[] CreateImage(string htmlData)
        {
            return CreateImage(htmlData, null);
        } // End Function CreateImage 


        public static byte[] CreateImage(string htmlData, libWkHtmlToX.ImageSettings imageSettings)
        {
            byte[] imgBytes = null;

            if (string.IsNullOrEmpty(htmlData) || htmlData.Trim() == string.Empty)
                throw new System.IO.InvalidDataException("Invalid input data...");

            // string ver = libWkHtmlToX.CallsImage.wkhtmltoimage_version();
            // System.Console.WriteLine(ver);

            int init = libWkHtmlToX.CallsImage.wkhtmltoimage_init(false);

            System.IntPtr globalSettings = libWkHtmlToX.CallsImage.wkhtmltoimage_create_global_settings();
            if(imageSettings != null)
                imageSettings.SetConfigValues(globalSettings);


            // string format = "svg";
            // format = "jpg";
            // format = "png";
            // format = "bmp";
            // format = ""; // nothingness

            // libWkHtmlToX.CallsImage.wkhtmltoimage_set_global_setting(globalSettings, "fmt", format);
            // libWkHtmlToX.CallsImage.wkhtmltoimage_set_global_setting(globalSettings, "screen 0", "5024x5768x24");
            // libWkHtmlToX.CallsImage.wkhtmltoimage_set_global_setting(globalSettings, "in", "https://www.google.com/");
            // libWkHtmlToX.CallsImage.wkhtmltoimage_get_global_setting(globalSettings, "", "", 0);


            // System.IntPtr data = System.IntPtr.Zero;
            System.IntPtr data = libWkHtmlToX.Utf8Marshaler._staticInstance.MarshalManagedToNative(htmlData);

            System.IntPtr converter = libWkHtmlToX.CallsImage.wkhtmltoimage_create_converter(globalSettings, data);

            int res = libWkHtmlToX.CallsImage.wkhtmltoimage_convert(converter);
            imgBytes = libWkHtmlToX.CallsImage.wkhtmltoimage_get_output(converter);

            // libWkHtmlToX.CallsImage.wkhtmltoimage_destroy_converter(converter);
            libWkHtmlToX.Utf8Marshaler._staticInstance.CleanUpNativeData(data);

            int deinitSuccess = libWkHtmlToX.CallsImage.wkhtmltoimage_deinit();

            return imgBytes;
        } // End Sub CreateImage(string htmlData, libWkHtmlToX.ImageSettings imageSettings) 


        // CreateImageFile(htmlData, imageSettings, @"D:\test_image");
        public static void CreateImageFile(string htmlData, libWkHtmlToX.ImageSettings imageSettings, string fileName)
        {
            byte[] imgBytes = CreateImage(htmlData, imageSettings);

            if (!fileName.EndsWith("." + imageSettings.SupportedFormat.ToString().ToLowerInvariant(), System.StringComparison.OrdinalIgnoreCase))
                fileName += "." + imageSettings.SupportedFormat.ToString().ToLowerInvariant();

            System.IO.File.WriteAllBytes(fileName, imgBytes);
        } // End Sub CreateImageFile 


    } // End Class Converter 


} // End namespace libWkHtmlToX 
