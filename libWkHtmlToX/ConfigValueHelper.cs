
namespace libWkHtml2X
{


    public delegate int set_config_value_t(System.IntPtr settings, string name, string value);


    public class ConfigValueHelper
    {

        // private static object s_initLock = new object();
        private static System.Globalization.NumberFormatInfo s_webNumberFormat;


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


        // https://stackoverflow.com/questions/7095/is-the-c-sharp-static-constructor-thread-safe
        static ConfigValueHelper()
        {
            // lock (s_initLock)
            // {
            //     if (s_webNumberFormat == null)
            s_webNumberFormat = CreateWebNumberFormat();
            // } // End Lock 

        } // End Static Constructor 


        

        /// <summary>
        /// 
        /// </summary>
        public static void SetConfigValues(System.IntPtr config, object instance, set_config_value_t setter)
        {
            if (config == System.IntPtr.Zero)
                throw new System.ArgumentNullException("config");

            System.Type t = instance.GetType();

            System.Reflection.FieldInfo[] fis = System.Reflection.IntrospectionExtensions.GetTypeInfo(t).GetFields();
            
            for (int i = 0; i < fis.Length; ++i)
            {
                System.Reflection.FieldInfo fi = fis[i];

                string attName = AttributeHelper.GetAttributValue<wkHtmlOptionNameAttribute, string>(fi, a => a.Name);
                object objVal = fi.GetValue(instance);

                if (attName == null)
                {
                    SetConfigValues(config, objVal, setter);
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
                        strValue = System.Convert.ToString(objVal, System.Globalization.CultureInfo.InvariantCulture).ToLowerInvariant();
                    }
                    else if (object.ReferenceEquals(tField, typeof(double)))
                    {
                        double dblVal = (double)objVal;
                        strValue = dblVal.ToString("N2", s_webNumberFormat);
                    }
                    else if (object.ReferenceEquals(tField, typeof(int)))
                    {
                        int iVal = (int)objVal;
                        strValue = iVal.ToString(s_webNumberFormat);
                    }
                    else
                        strValue = System.Convert.ToString(objVal, System.Globalization.CultureInfo.InvariantCulture);

                    // System.Console.WriteLine(attName);
                    // System.Console.WriteLine(objVal);
                    // System.Console.WriteLine(strValue);
                    setter(config, attName, strValue);
                }
            } // Next i 

        } // End Sub SetConfigValues 


    } // End Class ConfigValueHelper 


} // End Namespace libWkHtml2X 
