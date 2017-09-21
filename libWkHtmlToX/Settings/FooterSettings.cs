
namespace libWkHtmlToX
{


    // https://wkhtmltopdf.org/libwkhtmltox/pagesettings.html#pageHeaderFooter
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

}
