
namespace libWkHtml2X
{

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

}
