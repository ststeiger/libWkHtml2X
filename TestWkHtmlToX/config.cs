
namespace libWkHtml2X
{


    public class WebPageSpecificSettings
    {

        // Should we print the background? Must be either "true" or "false".
        [wkHtmlOptionName("web.background")]
        public bool? PrintBackground;

        //  Should we load images? Must be either "true" or "false".
        [wkHtmlOptionName("web.loadImages")]
        public bool? LoadImages;

        // web.enableJavascript Should we enable javascript? Must be either "true" or "false".
        [wkHtmlOptionName("web.enableJavascript")]
        public bool? EnableJavascript;

        // Should we enable intelligent shrinkng 
        // to fit more content on one page? 
        // Must be either "true" or "false". Has no effect for wkhtmltoimage.
        [wkHtmlOptionName("web.enableIntelligentShrinking")]
        public bool? EnableIntelligentShrinking;

        // The minimum font size allowed.E.g. "9"
        [wkHtmlOptionName("web.minimumFontSize")]
        public int? MinimumFontSize;

        // Should the content be printed using the print media type 
        // instead of the screen media type.Must be either "true" or "false". 
        // Has no effect for wkhtmltoimage.
        [wkHtmlOptionName("web.printMediaType")]
        public bool? PrintMediaType;

        // What encoding should we guess content is using 
        // if they do not specify it properly? E.g. "utf-8"
        [wkHtmlOptionName("web.defaultEncoding")]
        public string DefaultEncoding;

        // Url er path to a user specified style sheet.
        [wkHtmlOptionName("web.userStyleSheet")]
        public string UserStyleSheet;

        // Should we enable NS plugins, must be either "true" or "false". 
        // Enabling this will have limited success
        [wkHtmlOptionName("web.enablePlugins")]
        public bool? EnablePlugins;
    } // End Class WebPageSpecificSettings  


    // https://wkhtmltopdf.org/libwkhtmltox/pagesettings.html#pageLoad
    public class LoadSettings
    {
        // load.*
        [wkHtmlOptionName("load.username")]
        public string UserName;

        [wkHtmlOptionName("load.password")]
        public string Password;

        // The mount of time in milliseconds to wait after a page has done loading until it is actually printed. E.g. "1200". We will wait this amount of time or until, javascript calls window.print().
        [wkHtmlOptionName("load.jsdelay")]
        public ulong? JsDelay;

        [wkHtmlOptionName("load.zoomFactor")]
        public double? ZoomFactor;

        [wkHtmlOptionName("load.customHeaders")]
        public object CustomHeaders;

        // Should the custom headers be sent all elements loaded instead of only the main page? Must be either "true" or "false".
        [wkHtmlOptionName("load.repertCustomHeaders")]
        public bool? RepertCustomHeaders;

        [wkHtmlOptionName("load.cookies")]
        public object Cookies;

        [wkHtmlOptionName("load.post")]
        public object Post;

        [wkHtmlOptionName("load.blockLocalFileAccess")]
        public bool? BlockLocalFileAccess;

        [wkHtmlOptionName("load.stopSlowScript")]
        public bool? StopSlowScript;

        [wkHtmlOptionName("load.debugJavascript")]
        public bool? DebugJavascript;

        [wkHtmlOptionName("load.loadErrorHandling")]
        public ErrorBehaviour? LoadErrorHandling;

        [wkHtmlOptionName("load.proxy")]
        public string Proxy;

        [wkHtmlOptionName("load.runScript")]
        public object RunScript;
    } // End Class LoadSettings 



    public class HeaderSettings
    {

        [wkHtmlOptionName("header.fontSize")]
        public int? FontSize; // The font size to use for the header, e.g. "13"

        [wkHtmlOptionName("header.fontName")]
        public string FontName; // The name of the font to use for the header. e.g. "times"

        [wkHtmlOptionName("header.left")]
        public string Left; // The text to print in the center part of the header. With replacements

        [wkHtmlOptionName("header.center")]
        public string Center; // The text to print in the center part of the header.

        [wkHtmlOptionName("header.right")]
        public string Right; // The text to print in the center part of the header.

        [wkHtmlOptionName("header.line")]
        public bool? Line; // Whether a line should be printed under the header (either "true" or "false").

        [wkHtmlOptionName("header.spacing")]
        public double? Spacing;// The amount of space to put between the header and the content, e.g. "1.8". Be aware that if this is too large the header will be printed outside the pdf document. This can be corrected with the margin.top setting.

        [wkHtmlOptionName("header.htmlUrl")]
        public string HtmlUrl; // Url for a HTML document to use for the header
    } // End Class HeaderSettings 


    public class FooterSettings
    {

        [wkHtmlOptionName("footer.fontSize")]
        public int? FontSize; // The font size to use for the footer, e.g. "13"

        [wkHtmlOptionName("footer.fontName")]
        public string FontName; // The name of the font to use for the footer. e.g. "times"

        [wkHtmlOptionName("footer.left")]
        public string Left; // The text to print in the center part of the footer. With replacements

        [wkHtmlOptionName("footer.center")]
        public string Center; // The text to print in the center part of the footer.

        [wkHtmlOptionName("footer.right")]
        public string Right; // The text to print in the center part of the footer.

        [wkHtmlOptionName("footer.line")]
        public bool? Line; // Whether a line should be printed under the footer (either "true" or "false").

        [wkHtmlOptionName("footer.spacing")]
        public double? Spacing;// The amount of space to put between the footer and the content, e.g. "1.8". Be aware that if this is too large the footer will be printed outside the pdf document. This can be corrected with the margin.top setting.

        [wkHtmlOptionName("footer.htmlUrl")]
        public string HtmlUrl; // Url for a HTML document to use for the footer

    } // End Class FooterSettings 



    public class PdfGlobalSettings
    {

        // The paper size of the output document, e.g. "A4".
        [wkHtmlOptionName("size.pageSize")]
        public string PageSize; 

        // The width of the output document, e.g. "4cm".
        [wkHtmlOptionName("size.width")]
        public double? Width; 

        // The height  of the output document, e.g. "12In".
        [wkHtmlOptionName("size.height")]
        public double? Height; 

        // The orientation of the output document, must be either "Landscape" or "Portrait".
        [wkHtmlOptionName("orientation")]
        public Orientation? Orientation; 

        // Should the output be printed in color or gray scale, must be either "Color" or "Grayscale"
        [wkHtmlOptionName("colorMode")]
        public ColorMode? ColorMode;

        // Most likely has no effect.
        [wkHtmlOptionName("resolution")]
        public string Resolution;

        // What dpi should we use when printing, e.g. "80".
        [wkHtmlOptionName("dpi")]
        public int? DPI;

        // A number that is added to all page numbers when printing headers, footers and table of content.
        [wkHtmlOptionName("pageOffset")]
        public int? PageOffset; 

        // How many copies should we print?. e.g. "2".
        [wkHtmlOptionName("copies")]
        public int? Copies; 

        // Should the copies be collated? Must be either "true" or "false".
        [wkHtmlOptionName("collate")]
        public bool? Collate; 

        //  Should a outline (table of content in the sidebar) be generated and put into the PDF? Must be either "true" or false".
        [wkHtmlOptionName("outline")]
        public bool? Outline; 

        //  The maximal depth of the outline, e.g. "4".
        [wkHtmlOptionName("outlineDepth")]
        public int? OutlineDepth; 

        // If not set to the empty string a XML representation of the outline is dumped to this file.
        [wkHtmlOptionName("dumpOutline")]
        public string DumpOutline; 

        // The path of the output file, if "-" output is sent to stdout, if empty the output is stored in a buffer.
        [wkHtmlOptionName("out")]
        public string OutputFilePath; 

        // The title of the PDF document.
        [wkHtmlOptionName("documentTitle")]
        public string DocumentTitle; 

        // Should we use loss less compression when creating the pdf file? Must be either "true" or "false".
        [wkHtmlOptionName("useCompression")]
        public bool? UseCompression; 

        // margin.top Size of the top margin, e.g. "2cm"
        [wkHtmlOptionName("margin.top")]
        public string MarginTop; 

        // Size of the bottom margin, e.g. "2cm"
        [wkHtmlOptionName("margin.bottom")]
        public string MarginBottom; 

        // margin.left Size of the left margin, e.g. "2cm"
        [wkHtmlOptionName("margin.left")]
        public string MarginLeft ; 

        // margin.right Size of the right margin, e.g. "2cm"
        [wkHtmlOptionName("margin.right")]
        public string MarginRight; 

        // The maximal DPI to use for images in the pdf document.
        [wkHtmlOptionName("imageDPI")]
        public int? ImageDPI; 

        // The jpeg compression factor to use when producing the pdf document, e.g. "92".
        [wkHtmlOptionName("imageQuality")]
        public int? ImageQuality;

        // Path of file used to load and store cookies.
        [wkHtmlOptionName("load.cookieJar")]
        public string CookieJar;
    } // End Class PdfGlobalSettings 



    public class PdfObjectSettings
    {

        // Should we use a dotted line when creating a table of content? Must be either "true" or "false".
        [wkHtmlOptionName("toc.useDottedLines")]
        public string TocUseDottedLines;

        // The caption to use when creating a table of content.
        [wkHtmlOptionName("toc.captionText")]
        public string TocCaptionText;

        // Should we create links from the table of content into the actual content? Must be either "true or "false.
        [wkHtmlOptionName("toc.forwardLinks")]
        public string TocForwardLinks;

        //  Should we link back from the content to this table of content.
        [wkHtmlOptionName("toc.backLinks")]
        public string TocBackLinks;

        //  The indentation used for every table of content level, e.g. "2em".
        [wkHtmlOptionName("toc.indentation")]
        public string TocIndentation;

        // How much should we scale down the font for every toc level? E.g. "0.8"
        [wkHtmlOptionName("toc.fontScale")]
        public string TocFontScale;

        // The URL or path of the web page to convert, if "-" input is read from stdin.
        [wkHtmlOptionName("page")]
        public string Page;


        // header.* Header specific settings see Header and footer settings. https://wkhtmltopdf.org/libwkhtmltox/pagesettings.html#pageHeaderFooter
        public HeaderSettings Header;

        // footer.*   Footer specific settings see Header and footer settings. https://wkhtmltopdf.org/libwkhtmltox/pagesettings.html#pageHeaderFooter
        public FooterSettings Footer;


        // Should external links in the HTML document be converted into external pdf links? Must be either "true" or "false.
        [wkHtmlOptionName("useExternalLinks")]
        public string UseExternalLinks;

        // Should internal links in the HTML document be converted into pdf references? Must be either "true" or "false"
        [wkHtmlOptionName("useLocalLinks")]
        public string UseLocalLinks;

        // TODO     
        [wkHtmlOptionName("replacements")]
        public string Replacements;

        // Should we turn HTML forms into PDF forms? Must be either "true" or file".
        [wkHtmlOptionName("produceForms")]
        public string ProduceForms;
        
        
        
        /// <summary>
        /// load.*    Page specific settings related to loading content, see https://wkhtmltopdf.org/libwkhtmltox/pagesettings.html#pageLoad
        /// </summary>
        public LoadSettings Load = new LoadSettings();


        // 
        /// <summary>
        /// web.*   See Web page specific settings. https://wkhtmltopdf.org/libwkhtmltox/pagesettings.html#pageWeb
        /// </summary>
        public WebPageSpecificSettings Web = new WebPageSpecificSettings();


        // Should the sections from this document be included in the outline and table of content?
        [wkHtmlOptionName("includeInOutline")]
        public string IncludeInOutline;

        // Should we count the pages of this document, in the counter used for TOC, headers and footers?
        [wkHtmlOptionName("pagesCount")]
        public string PagesCount;

        // If not empty this object is a table of content object, "page" is ignored and this xsl style sheet is used to convert the outline XML into a table of content.
        [wkHtmlOptionName("tocXsl")]
        public string TocXsl;

    } // End Class PdfObjectSettings 


    public enum ColorMode 
    {
        Color, Grayscale
    }


    public enum Orientation
    {
        Landscape, Portrait
    }


    public enum ErrorBehaviour
    {
        abort, skip, ignore
    }


    public enum SupportedFormat
    {
        BMP, JPG, PNG, NONE
    }



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
        public void SetConfigValues()
        {
            libWkHtml2X.ConfigValueHelper.SetConfigValues(this);
        } // End Sub SetConfigValues 


    } // End Class ImageSettings 


} // End Namespace 
