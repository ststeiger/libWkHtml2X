
namespace libWkHtml2X
{


    // https://wkhtmltopdf.org/libwkhtmltox/pagesettings.html#pageWeb
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


}
