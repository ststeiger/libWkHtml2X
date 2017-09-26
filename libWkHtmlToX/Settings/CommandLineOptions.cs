
namespace libWkHtmlToX
{

    // DRY 
    public abstract class wkHtmlToXCommandLineOptions
    {

        public string CommandLine
        {

            get
            {
                return ConfigValueHelper.GetCommandLineArguments(this).Trim();
            }

        } // End Property CommandLine 

    } // End Class wkHtmlToXCommandLineOptions 


    // https://www.mankier.com/1/wkhtmltoimage#--images
    public class WkHtmlToImageCommandLineOptions : wkHtmlToXCommandLineOptions
    {
        libWkHtmlToX.ImageSettings imageSettings = new libWkHtmlToX.ImageSettings();


        [wkHtmlOptionName("--image-quality")]
        public int? ImageQuality;

        /// <summary>
        /// The compression factor to use when outputting a JPEG image. E.g. "94"
        /// </summary>
        [wkHtmlOptionName("--quality")]
        public int? Quality;


        /// <summary>
        /// The output format to use, must be either "", "jpg", "png", "bmp" or "svg".
        /// </summary>
        [wkHtmlOptionName("--format")]
        public SupportedFormat? Format;

        /// <summary>
        /// The with of the screen used to render is pixels, e.g "800".
        /// </summary>
        [wkHtmlOptionName("--width")]
        public int? ScreenWidth;

        [wkHtmlOptionName("--height")]
        public int? ScreenHeight;

        /// <summary>
        /// Should we expand the screenWidth if the content does not fit? must be either "true" or "false".
        /// </summary>
        [wkHtmlOptionName("--disable-smart-width")]
        public bool? DisableSmartWidth;

        [wkHtmlOptionName("--encoding")]
        public string DefaultEncoding;


        [wkHtmlOptionName("--zoom")]
        public float? ZoomFactor;


        [wkHtmlOptionName("--images")]
        public bool? LoadOrPrintImages;


        [wkHtmlOptionName("--no-images")]
        public bool? NoLoadOrPrintImages;



        public WkHtmlToImageCommandLineOptions()
        {
            this.Format = libWkHtmlToX.SupportedFormat.PNG;
            this.Quality = 50;
            this.LoadOrPrintImages = true;
            this.DefaultEncoding = System.Text.Encoding.UTF8.WebName;
            this.DisableSmartWidth = true; // Use provided with

            // this.ScreenWidth = (int)System.Math.Ceiling(factorSize * viewbox_width);
        } // End Constructor 


    }  // End Class WkHtmlToImageCommandLineOptions 


    public class WkHtmlToPdfCommandLineOptions : wkHtmlToXCommandLineOptions
    {

        [wkHtmlOptionName("--orientation")]
        public Orientation? Orientation;
        
        [wkHtmlOptionName("--output-format")]
        public OutputFormat_t? OutputFormat;
        
        // The width of the output document, e.g. "4cm".
        [wkHtmlOptionName("--page-width")]
        public string Width;

        // The height  of the output document, e.g. "12In".
        [wkHtmlOptionName("--page-height")]
        public string Height;

        [wkHtmlOptionName("--margin-top")]
        public string MarginTop;

        [wkHtmlOptionName("--margin-bottom")]
        public string MarginBottom;

        [wkHtmlOptionName("--margin-left")]
        public string MarginLeft;

        [wkHtmlOptionName("--margin-right")]
        public string MarginRight;



        [wkHtmlOptionName("--image-quality")]
        public int? ImageQuality;

        [wkHtmlOptionName("--no-pdf-compression")]
        public bool? NoPdfCompression;

        [wkHtmlOptionName("--outline")]
        public bool? Outline;

        [wkHtmlOptionName("--copies")]
        public int? Copies;


        // The maximal DPI to use for images in the pdf document.
        [wkHtmlOptionName("--image-dpi")]
        public int? ImageDPI;

        [wkHtmlOptionName("--dpi")]
        public int? DPI;


        [wkHtmlOptionName("--disable-smart-shrinking")]
        public bool? DisableSmartShrinking;

        [wkHtmlOptionName("--enable-smart-shrinking")]
        public bool? EnableSmartShrinking;

        [wkHtmlOptionName("--encoding")]
        public string Encoding;


        public WkHtmlToPdfCommandLineOptions()
        {
            this.Orientation = libWkHtmlToX.Orientation.Portrait;
            this.OutputFormat = OutputFormat_t.pdf;

            this.MarginTop = "0px";
            this.MarginBottom = "0px";
            this.MarginLeft = "0px";
            this.MarginRight = "0px";

            this.DisableSmartShrinking = true;

            this.DPI = 300;
            this.ImageDPI = 600;
            this.ImageQuality = 50;
        } // End Constructor 


    } // End Class WkHtmlToPdfCommandLineOptions 


} // End Namespace libWkHtmlToX 
