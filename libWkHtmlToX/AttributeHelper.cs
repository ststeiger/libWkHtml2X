
namespace libWkHtml2X
{


    public delegate TResult GetValue_t<in T, out TResult>(T arg1);


    internal class AttributeHelper
    {


        public static object GetAttribute(System.Reflection.MemberInfo mi, System.Type t)
        {
            
#if NET_2_0
            object[] objs = mi.GetCustomAttributes(t, true);
            if (objs == null || objs.Length < 1)
                return null;

            return objs[0];
#else
            return System.Reflection.CustomAttributeExtensions.GetCustomAttribute(mi, t, true);
            //System.Collections.Generic.IEnumerable<System.Attribute> objs = 
            //    System.Reflection.CustomAttributeExtensions.GetCustomAttributes(mi, t, true);
            //object e = null;
            //using (System.Collections.Generic.IEnumerator<object> enumer = objs.GetEnumerator())
            //{
            //    if (enumer.MoveNext()) e = enumer.Current;
            //}

            //return e;
#endif

        } // End Function GetAttribute 


        public static T GetAttribute<T>(System.Reflection.MemberInfo mi)
            where T : System.Attribute
        {
            return (T)GetAttribute(mi, typeof(T));
        } // End Function GetAttribute 


        public static TValue GetAttributValue<TAttribute, TValue>(System.Reflection.MemberInfo mi, GetValue_t<TAttribute, TValue> value)
            where TAttribute : System.Attribute
        {
            TAttribute att = (TAttribute)GetAttribute(mi, typeof(TAttribute));

            if (att != null)
            {
                return value(att);
            }

            return default(TValue);
        } // End Function GetAttributValue 


    } // End Class AttributeHelper 


} // End Namespace libWkHtmlToX 
