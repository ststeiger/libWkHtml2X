
namespace TestWkHtmlToX
{


    static class Program
    {



        public static object GetAttribute(System.Reflection.MemberInfo mi, System.Type t)
        {
            object[] objs = mi.GetCustomAttributes(t, true);

            if (objs == null || objs.Length < 1)
                return null;

            return objs[0];
        }



        public static T GetAttribute<T>(System.Reflection.MemberInfo mi)
        {
            return (T)GetAttribute(mi, typeof(T));
        }


        public delegate TResult GetValue_t<in T, out TResult>(T arg1);

        public static TValue GetAttributValue<TAttribute, TValue>(System.Reflection.MemberInfo mi, GetValue_t<TAttribute, TValue> value) where TAttribute : System.Attribute
        {
            TAttribute[] objAtts = (TAttribute[])mi.GetCustomAttributes(typeof(TAttribute), true);
            TAttribute att = (objAtts == null || objAtts.Length < 1) ? default(TAttribute) : objAtts[0];
            // TAttribute att = (TAttribute)GetAttribute(mi, typeof(TAttribute));

            if (att != null)
            {
                return value(att);
            }
            return default(TValue);
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [System.STAThread]
        static void Main()
        {
#if false 
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new Form1());
#endif 
            libWkHtml2X.WebPageSpecificSettings wss = new libWkHtml2X.WebPageSpecificSettings();
            System.Type t = typeof(libWkHtml2X.WebPageSpecificSettings);

            System.IntPtr hSO = libWkHtml2X.LibraryLoader.Load("wkhtmltox");


            libWkHtml2X.LibraryLoader.Unload(hSO);



            
            System.Reflection.FieldInfo fi = t.GetField("PrintBackground");
            
            // libWkHtml2X.wkHtmlOptionNameAttribute att = (libWkHtml2X.wkHtmlOptionNameAttribute)fi.GetCustomAttributes(typeof(libWkHtml2X.wkHtmlOptionNameAttribute), false)[0];
            libWkHtml2X.wkHtmlOptionNameAttribute att = GetAttribute<libWkHtml2X.wkHtmlOptionNameAttribute>(fi);

            string aname = GetAttributValue<libWkHtml2X.wkHtmlOptionNameAttribute, string>(fi, delegate(libWkHtml2X.wkHtmlOptionNameAttribute a) { return a.Name; });
            string aname2 = GetAttributValue<libWkHtml2X.wkHtmlOptionNameAttribute, string>(fi, a => a.Name);
            System.Console.WriteLine(aname2);


            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        } // End Sub Main 


    } // End Class Program 


} // End Namespace TestWkHtmlToX 
