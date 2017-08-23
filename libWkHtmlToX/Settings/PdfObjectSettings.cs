
namespace libWkHtml2X
{


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
        public HeaderSettings Header = new HeaderSettings();

        // footer.*   Footer specific settings see Header and footer settings. https://wkhtmltopdf.org/libwkhtmltox/pagesettings.html#pageHeaderFooter
        public FooterSettings Footer = new FooterSettings();


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


        /// <summary>
        /// 
        /// </summary>
        public void SetConfigValues(System.IntPtr config)
        {
            libWkHtml2X.ConfigValueHelper.SetConfigValues(config, this, libWkHtml2X.CallsPDF.wkhtmltopdf_set_object_setting);
        } // End Sub SetConfigValues 


    } // End Class PdfObjectSettings 


}
