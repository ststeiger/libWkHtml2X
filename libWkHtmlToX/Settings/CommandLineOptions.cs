
namespace libWkHtmlToX
{

    public class WkHtmlToImageCommandLineOptions
    {
        libWkHtmlToX.ImageSettings imageSettings = new libWkHtmlToX.ImageSettings();


        [wkHtmlOptionName("--image-quality")]
        public int? ImageQuality;



        /// <summary>
        /// Should we expand the screenWidth if the content does not fit? must be either "true" or "false".
        /// </summary>
        [wkHtmlOptionName("smartWidth")]
        public bool? SmartWidth;

        /// <summary>
        /// The compression factor to use when outputting a JPEG image. E.g. "94"
        /// </summary>
        [wkHtmlOptionName("quality")]
        public int? Quality;


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


        public WkHtmlToImageCommandLineOptions()
        {
            this.SmartWidth = false;
            this.Quality = 50;

            this.SupportedFormat = libWkHtmlToX.SupportedFormat.PNG;
            // this.ScreenWidth = (int)System.Math.Ceiling(factorSize * viewbox_width);
        }


            
            //imageSettings.Web.DefaultEncoding = System.Text.Encoding.UTF8.WebName;
            //imageSettings.Web.EnableIntelligentShrinking = false;
            //imageSettings.Web.PrintBackground = true;
    }


    public class WkHtmlToPdfCommandLineOptions
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


        
        private System.Globalization.NumberFormatInfo m_webNumberFormat;

        public WkHtmlToPdfCommandLineOptions()
        {
            this.m_webNumberFormat = CreateWebNumberFormat();
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
        }



        


        private static System.Globalization.NumberFormatInfo CreateWebNumberFormat()
        {
            //System.Globalization.NumberFormatInfo nfi = (System.Globalization.NumberFormatInfo)System.Globalization.CultureInfo.InvariantCulture.NumberFormat.Clone();
            System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();
            nfi.NumberGroupSeparator = "";
            nfi.NumberDecimalSeparator = ".";

            nfi.CurrencyGroupSeparator = "";
            nfi.CurrencyDecimalSeparator = ".";
            nfi.CurrencySymbol = "";

            return nfi;
        } // End Function SetupNumberFormatInfo

        
        public string CommandLine
        {
            get {
                return GetCommandLineArguments();
            }
        }

        protected string GetCommandLineArguments()
        {
            string retValue = null;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            System.Type t = this.GetType();

            System.Reflection.FieldInfo[] fis = System.Reflection.IntrospectionExtensions.GetTypeInfo(t).GetFields();

            for (int i = 0; i < fis.Length; ++i)
            {
                System.Reflection.FieldInfo fi = fis[i];

                string attName = AttributeHelper.GetAttributValue<wkHtmlOptionNameAttribute, string>(fi, a => a.Name);
                object objVal = fi.GetValue(this);

                if (attName == null)
                {
                    // SetConfigValues(config, objVal, setter);
                    continue;
                } // End if (attName == null)

                // Set Value
                if (objVal != null)
                {
                    System.Type tField = fi.FieldType;

                    bool isOfTypeSystemNullable = (System.Reflection.IntrospectionExtensions.GetTypeInfo(tField).IsGenericType
                        && object.ReferenceEquals(tField.GetGenericTypeDefinition(), typeof(System.Nullable<>))
                    );

                    if (isOfTypeSystemNullable)
                        tField = System.Nullable.GetUnderlyingType(tField);

                    string strValue = null;

                    if (object.ReferenceEquals(tField, typeof(bool)))
                    {
                        if (System.Convert.ToBoolean(objVal) != true)
                            attName = "";

                        strValue = "";
                    }
                    else if (object.ReferenceEquals(tField, typeof(double)))
                    {
                        double dblVal = (double)objVal;
                        strValue = dblVal.ToString("N2", this.m_webNumberFormat);
                    }
                    else if (object.ReferenceEquals(tField, typeof(int)))
                    {
                        int iVal = (int)objVal;
                        strValue = iVal.ToString(this.m_webNumberFormat);
                    }
                    else
                        strValue = System.Convert.ToString(objVal, System.Globalization.CultureInfo.InvariantCulture);

                    sb.Append(" ");
                    sb.Append(attName);
                    sb.Append(" ");
                    sb.Append(strValue);
                } // End if (objVal != null) 

            } // Next i 

            retValue = sb.ToString();
            sb.Length = 0;
            sb = null;

            return retValue;
        } // End Sub SetConfigValues 

    }
}
