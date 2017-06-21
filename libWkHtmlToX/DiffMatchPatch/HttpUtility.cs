
#if !NET_2_0

namespace System.Web
{
    public class HttpUtility
    {
        public static string UrlEncode(string text, System.Text.Encoding enc)
        {
            return System.Net.WebUtility.UrlEncode(text);
        }

        public static string UrlDecode(string text, System.Text.Encoding enc)
        {
            return System.Net.WebUtility.UrlDecode(text);
        }

        public static string HtmlEncode(string text)
        {
            return System.Net.WebUtility.HtmlEncode(text);
        }


    }
}

#endif
