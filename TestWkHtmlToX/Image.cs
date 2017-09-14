
namespace wkHtmlToXCore
{


    // https://github.com/wkhtmltopdf/wkhtmltopdf/blob/master/examples/image_c_api.c
    public class TestImage
    {


        // https://wkhtmltopdf.org/libwkhtmltox/
        public static byte[] CreateImage(string htmlData, libWkHtml2X.ImageSettings imageSettings)
        {
            byte[] imgBytes = null;

            // string ver = libWkHtml2X.CallsImage.wkhtmltoimage_version();
            // System.Console.WriteLine(ver);

            int init = libWkHtml2X.CallsImage.wkhtmltoimage_init(false);

            System.IntPtr globalSettings = libWkHtml2X.CallsImage.wkhtmltoimage_create_global_settings();
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


    } // End Class TestImage 


} // End Namespace wkHtmlToXCore 
