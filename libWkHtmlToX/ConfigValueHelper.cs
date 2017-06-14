
namespace libWkHtml2X
{


    public class ConfigValueHelper
    {


        /// <summary>
        /// 
        /// </summary>
        private void SetConfigValues(object instance)
        {
            System.Type t = instance.GetType();

            System.Reflection.FieldInfo[] fis = System.Reflection.IntrospectionExtensions.GetTypeInfo(t).GetFields();
            
            for (int i = 0; i < fis.Length; ++i)
            {
                System.Reflection.FieldInfo fi = fis[i];

                string attName = AttributeHelper.GetAttributValue<wkHtmlOptionNameAttribute, string>(fi, a => a.Name);
                object objVal = fi.GetValue(instance);

                if (attName == null)
                {
                    SetConfigValues(objVal);
                    continue;
                } // End if (attName == null)

                // Set Value
                if (objVal != null)
                    System.Console.WriteLine(objVal);
            } // Next i 

        } // End Sub SetConfigValues 


    } // End Class ConfigValueHelper 


} // End Namespace libWkHtml2X 
