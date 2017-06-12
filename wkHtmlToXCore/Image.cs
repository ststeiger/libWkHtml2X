
namespace wkHtmlToXCore
{




    // https://github.com/wkhtmltopdf/wkhtmltopdf/blob/master/examples/image_c_api.c
    public class TestImage
    {


        public static void CreateImg(string htmlData)
        {
            string ver = libWkHtml2XNetStandard.CallsImage.wkhtmltoimage_version();

            int init = libWkHtml2XNetStandard.CallsImage.wkhtmltoimage_init(0);


            System.IntPtr globalSettings = libWkHtml2XNetStandard.CallsImage.wkhtmltoimage_create_global_settings();

            string format = "svg";
            format = "jpg";
            format = "png";
            format = "bmp";
            // format = ""; // nothingness


            libWkHtml2XNetStandard.CallsImage.wkhtmltoimage_set_global_setting(globalSettings, "fmt", format);
            // libWkHtml2XNetStandard.CallsImage.wkhtmltoimage_set_global_setting(globalSettings, "in", "https://www.google.com/");

            // libWkHtml2XNetStandard.CallsImage.wkhtmltoimage_get_global_setting(globalSettings, "", "", 0);


            // System.IntPtr data = System.IntPtr.Zero;
            System.IntPtr data = libWkHtml2XNetStandard.Utf8Marshaler._staticInstance.MarshalManagedToNative(htmlData);


            System.IntPtr converter = libWkHtml2XNetStandard.CallsImage.wkhtmltoimage_create_converter(globalSettings, data);

            int res = libWkHtml2XNetStandard.CallsImage.wkhtmltoimage_convert(converter);

            byte[] imgBytes = libWkHtml2XNetStandard.CallsImage.wkhtmltoimage_get_output(converter);
            System.IO.File.WriteAllBytes(@"C:\Users\username\Desktop\nreco.imagegenerator.1.1.0\file." + format, imgBytes);



            libWkHtml2XNetStandard.CallsImage.wkhtmltoimage_destroy_converter(converter);
            libWkHtml2XNetStandard.Utf8Marshaler._staticInstance.CleanUpNativeData(data);

            int deinitSuccess = libWkHtml2XNetStandard.CallsImage.wkhtmltoimage_deinit();
            System.Console.WriteLine(ver);
        }


    }


}
