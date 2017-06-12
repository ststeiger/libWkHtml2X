
namespace libWkHtml2X
{



    public class wkHtmlOptionNameAttribute : System.Attribute
    {
        public string Name;

        // public wkHtmlOptionNameAttribute() { }

        public wkHtmlOptionNameAttribute(string name)
        {
            this.Name = name;
        }
    }


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
    }


    // https://wkhtmltopdf.org/libwkhtmltox/pagesettings.html
    public class ImageOptions
    {

        private System.Collections.Generic.Dictionary<string, string> m_properties =
            new System.Collections.Generic.Dictionary<string, string>(System.StringComparer.OrdinalIgnoreCase);


        // crop.left  - X
        // crop.top top/y
        // crop.width
        // crop.height

        public System.Collections.Generic.Dictionary<string, string> AllProperties
        {
            get
            {
                return this.m_properties;
            }
            set
            {
                this.m_properties = value;
            }
        }


        const string cl = "crop.left";


        [wkHtmlOptionName("crop.left")]
        public double CropLeft
        {

            get
            {
                if (this.m_properties.ContainsKey(cl))
                {
                    return double.Parse(this.m_properties[cl], System.Globalization.CultureInfo.InvariantCulture);
                }

                return 0.0;
            }
            set { this.m_properties[cl] = value.ToString(System.Globalization.CultureInfo.InvariantCulture); }
        }

        const string ct = "crop.top";
        [wkHtmlOptionName("crop.top")]
        public double CropTop
        {

            get
            {
                if (this.m_properties.ContainsKey(ct))
                {
                    return double.Parse(this.m_properties[ct], System.Globalization.CultureInfo.InvariantCulture);
                }

                return 0.0;
            }
            set { this.m_properties[ct] = value.ToString(System.Globalization.CultureInfo.InvariantCulture); }
        }

        const string cw = "crop.width";

        [wkHtmlOptionName("crop.width")]
        public double CropWidth
        {

            get
            {
                if (this.m_properties.ContainsKey(cw))
                {
                    return double.Parse(this.m_properties[cw], System.Globalization.CultureInfo.InvariantCulture);
                }

                return 0.0;
            }
            set { this.m_properties[cw] = value.ToString(System.Globalization.CultureInfo.InvariantCulture); }
        }

        const string ch = "crop.height";

        [wkHtmlOptionName("crop.height")]
        public double CropHeight
        {

            get
            {
                if (this.m_properties.ContainsKey(ch))
                {
                    return double.Parse(this.m_properties[ch], System.Globalization.CultureInfo.InvariantCulture);
                }

                return 0.0;
            }
            set { this.m_properties[ch] = value.ToString(System.Globalization.CultureInfo.InvariantCulture); }
        }


        const string load_cookiejar = "load.cookieJar";

        //load.cookieJar Path of file used to load and store cookies.
        [wkHtmlOptionName("load.cookieJar")]
        public string CookieJar
        {
            get
            {
                if (this.m_properties.ContainsKey(load_cookiejar))
                    return this.m_properties[load_cookiejar];
                return null;
            }
            set { this.m_properties[load_cookiejar] = value; }
        }

        // https://wkhtmltopdf.org/libwkhtmltox/pagesettings.html#pageLoad
        // load.*

        // https://wkhtmltopdf.org/libwkhtmltox/pagesettings.html#pageWeb
        // web.*

        [wkHtmlOptionName("transparent")]
        public bool Transparent; // When outputting a PNG or SVG, make the white background transparent. Must be either "true" or "false"

        [wkHtmlOptionName("in")]
        public string InputFileUrl; // The URL or path of the input file, if "-" stdin is used. E.g. "http://google.com"

        [wkHtmlOptionName("out")]
        public string OutputFileUrl;


        const string format = "fmt";
        [wkHtmlOptionName("fmt")]
        public SupportedFormat SupportedFormat
        {
            get
            {
                if (this.m_properties.ContainsKey(format))
                {
                    string fmt = this.m_properties[format];
                    return (SupportedFormat)System.Enum.Parse(typeof(SupportedFormat), fmt);
                }


                return SupportedFormat.NONE;
            }
            set
            {
                if (value == SupportedFormat.NONE)
                {
                    this.m_properties[format] = "";
                    return;
                }
                this.m_properties[format] = value.ToString();
            }
        }



        const string sw = "screenWidth";
        [wkHtmlOptionName("screenWidth")]
        public int ScreenWidth
        {

            get
            {
                if (this.m_properties.ContainsKey(sw))
                {
                    return int.Parse(this.m_properties[sw], System.Globalization.CultureInfo.InvariantCulture);
                }

                return 0;
            }
            set { this.m_properties[sw] = value.ToString(System.Globalization.CultureInfo.InvariantCulture); }
        }


        const string sm = "smartWidth";
        [wkHtmlOptionName("smartWidth")]
        public int SmartWidth
        {

            get
            {
                if (this.m_properties.ContainsKey(sm))
                {
                    return int.Parse(this.m_properties[sm], System.Globalization.CultureInfo.InvariantCulture);
                }

                return 0;
            }
            set { this.m_properties[sm] = value.ToString(System.Globalization.CultureInfo.InvariantCulture); }
        }

        const string qa = "quality";
        [wkHtmlOptionName("quality")]
        public int Quality
        {

            get
            {
                if (this.m_properties.ContainsKey(qa))
                {
                    return int.Parse(this.m_properties[qa], System.Globalization.CultureInfo.InvariantCulture);
                }

                return 0;
            }
            set { this.m_properties[qa] = value.ToString(System.Globalization.CultureInfo.InvariantCulture); }
        }

    }


    public class HeaderFooterSettings
    {
        // header.*
        int fontSize; // The font size to use for the header, e.g. "13"
        string fontName; // The name of the font to use for the header. e.g. "times"
        string left; // The text to print in the center part of the header. With replacements
        string center; // The text to print in the center part of the header.
        string right; // The text to print in the center part of the header.
        bool line; // Whether a line should be printed under the header (either "true" or "false").
        double spacing;// The amount of space to put between the header and the content, e.g. "1.8". Be aware that if this is too large the header will be printed outside the pdf document. This can be corrected with the margin.top setting.
        string htmlUrl; // Url for a HTML document to use for the header
    }


    // https://wkhtmltopdf.org/libwkhtmltox/pagesettings.html#pageLoad
    public class LoadSettings
    {
        // load.*
        public string username;
        public string password;

        public ulong jsdelay;
        public double zoomFactor;
        public object customHeaders;
        public string repertCustomHeaders;

        public object cookies;
        public object post;

        public bool blockLocalFileAccess;
        public bool stopSlowScript;
        public bool debugJavascript;
        public ErrorBehaviour loadErrorHandling;
        public string proxy;

        public object runScript;
    }

    public enum ErrorBehaviour
    {
        abort, skip, ignore
    }


    public enum SupportedFormat
    {
        BMP, JPG, PNG, NONE
    }

}
