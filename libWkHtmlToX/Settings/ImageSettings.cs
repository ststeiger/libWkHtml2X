
namespace libWkHtml2X
{


    // https://wkhtmltopdf.org/libwkhtmltox/pagesettings.html
    public class ImageSettings
    {

        /// <summary>
        /// 
        /// </summary>
        public ImageSettings SetCookieJar(string cookieDir)
        {
            return this;
        }


        /// <summary>
        /// left/x coordinate of the window to capture in pixels. E.g. "200"
        /// </summary>
        [wkHtmlOptionName("crop.left")]
        public double? CropLeft; // X

        /// <summary>
        /// top/y coordinate of the window to capture in pixels. E.g. "200"
        /// </summary>
        [wkHtmlOptionName("crop.top")]
        public double? CropTop; // Y

        /// <summary>
        /// Width of the window to capture in pixels. E.g. "200"
        /// </summary>
        [wkHtmlOptionName("crop.width")]
        public double? CropWidth;

        /// <summary>
        /// Height of the window to capture in pixels. E.g. "200"
        /// </summary>
        [wkHtmlOptionName("crop.height")]
        public double? CropHeight;


        /// <summary>
        /// Path of file used to load and store cookies.
        /// </summary>
        [wkHtmlOptionName("load.cookieJar")]
        public string CookieJar;




        /// <summary>
        /// load.*    Page specific settings related to loading content, see https://wkhtmltopdf.org/libwkhtmltox/pagesettings.html#pageLoad
        /// </summary>
        public LoadSettings Load = new LoadSettings();


        // 
        /// <summary>
        /// web.* https://wkhtmltopdf.org/libwkhtmltox/pagesettings.html#pageWeb
        /// </summary>
        public WebPageSpecificSettings Web = new WebPageSpecificSettings();

        /// <summary>
        /// When outputting a PNG or SVG, make the white background transparent. Must be either "true" or "false"
        /// </summary>
        [wkHtmlOptionName("transparent")]
        public bool? Transparent;

        /// <summary>
        /// The URL or path of the input file, if "-" stdin is used. E.g. "http://google.com"
        /// </summary>
        [wkHtmlOptionName("in")]
        public string InputFileUrl;


        /// <summary>
        /// The path of the output file, if "-" stdout is used, if empty the content is stored to a internalBuffer.
        /// </summary>
        [wkHtmlOptionName("out")]
        public string OutputFileUrl;

        /// <summary>
        /// The output format to use, must be either "", "jpg", "png", "bmp" or "svg".
        /// </summary>
        [wkHtmlOptionName("fmt")]
        public SupportedFormat? SupportedFormat;

        /// <summary>
        /// The with of the screen used to render is pixels, e.g "800".
        /// </summary>
        [wkHtmlOptionName("screenWidth")]
        public int? ScreenWidth;


        /// <summary>
        /// Should we expand the screenWidth if the content does not fit? must be either "true" or "false".
        /// </summary>
        [wkHtmlOptionName("smartWidth")]
        public int? SmartWidth;

        /// <summary>
        /// The compression factor to use when outputting a JPEG image. E.g. "94"
        /// </summary>
        [wkHtmlOptionName("quality")]
        public int? Quality;


        /// <summary>
        /// 
        /// </summary>
        public void SetConfigValues(System.IntPtr config)
        {
            libWkHtml2X.ConfigValueHelper.SetConfigValues(config, this, libWkHtml2X.CallsImage.wkhtmltoimage_set_global_setting);
        } // End Sub SetConfigValues 


    } // End Class ImageSettings 


}
