
namespace libWkHtml2X
{


    public delegate int set_config_value_t(System.IntPtr settings, string name, string value);


    public class ConfigValueHelper
    {
        


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

                    


                    bool isSystemNullableType = (System.Reflection.IntrospectionExtensions.GetTypeInfo(tField).IsGenericType 
                        && object.ReferenceEquals(tField.GetGenericTypeDefinition(), typeof(System.Nullable<>))
                    );

                    if(isSystemNullableType)
                        tField = System.Nullable.GetUnderlyingType(tField);

                    string strValue = null;

                    if (object.ReferenceEquals(tField, typeof(bool)))
                    {
                        strValue = System.Convert.ToString(objVal, System.Globalization.CultureInfo.InvariantCulture).ToLowerInvariant();
                    }
                    else if (object.ReferenceEquals(tField, typeof(double)))
                    {
                        double dblVal = (double)objVal;
                        strValue = dblVal.ToString("N2", System.Globalization.CultureInfo.InvariantCulture);
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
