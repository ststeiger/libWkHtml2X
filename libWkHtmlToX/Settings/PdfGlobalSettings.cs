
namespace libWkHtmlToX
{

    // https://wkhtmltopdf.org/libwkhtmltox/pagesettings.html#pagePdfGlobal
    public class PdfGlobalSettings
    {

        // The paper size of the output document, e.g. "A4".
        [wkHtmlOptionName("size.pageSize")]
        public string PageSize; 

        // The width of the output document, e.g. "4cm".
        [wkHtmlOptionName("size.width")]
        public string Width; 

        // The height  of the output document, e.g. "12In".
        [wkHtmlOptionName("size.height")]
        public string Height; 

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


        [wkHtmlOptionName("outputFormat")]
        public OutputFormat_t? OutputFormat; 

        // The maximal DPI to use for images in the pdf document.
        [wkHtmlOptionName("imageDPI")]
        public int? ImageDPI; 

        // The jpeg compression factor to use when producing the pdf document, e.g. "92".
        [wkHtmlOptionName("imageQuality")]
        public int? ImageQuality;

        // Path of file used to load and store cookies.
        [wkHtmlOptionName("load.cookieJar")]
        public string CookieJar;


        /// <summary>
        /// 
        /// </summary>
        public void SetConfigValues(System.IntPtr config)
        {
            libWkHtmlToX.ConfigValueHelper.SetConfigValues(config, this, libWkHtmlToX.CallsPDF.wkhtmltopdf_set_global_setting);
        } // End Sub SetConfigValues 


    } // End Class PdfGlobalSettings 


}
