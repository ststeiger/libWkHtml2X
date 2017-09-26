
namespace libWkHtmlToX
{

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
        public ErrorBehaviour_t? LoadErrorHandling;

        [wkHtmlOptionName("load.proxy")]
        public string Proxy;

        [wkHtmlOptionName("load.runScript")]
        public object RunScript;
    } // End Class LoadSettings 

}
