
namespace libWkHtmlToX
{



    /// <summary>
    /// Marshaller for UTF8 strings.
    /// </summary>
    public class Utf8Marshaler
    {
        public static Utf8Marshaler _staticInstance;

        public System.IntPtr MarshalManagedToNative(string managedObj)
        {
            if (managedObj == null)
                return System.IntPtr.Zero;
            
            // not null terminated
            byte[] strbuf = System.Text.Encoding.UTF8.GetBytes(managedObj);
            

            System.IntPtr buffer = System.Runtime.InteropServices.Marshal.AllocHGlobal(strbuf.Length + 1);
            System.Runtime.InteropServices.Marshal.Copy(strbuf, 0, buffer, strbuf.Length);

            // write the terminating null
            //Marshal.WriteByte(buffer + strbuf.Length, 0);

            long lngPosEnd = buffer.ToInt64() + strbuf.Length;
            System.IntPtr ptrPosEnd = new System.IntPtr(lngPosEnd);
            System.Runtime.InteropServices.Marshal.WriteByte(ptrPosEnd, 0);

            return buffer;
        }


        public unsafe string MarshalNativeToManaged(System.IntPtr pNativeData)
        {
            byte* walk = (byte*)pNativeData;

            // find the end of the string
            while (*walk != 0)
            {
                walk++;
            }
            int length = (int)(walk - (byte*)pNativeData);

            // should not be null terminated
            //byte[] strbuf = new byte[length - 1];
            byte[] strbuf = new byte[length];


            // skip the trailing null
            //Marshal.Copy(pNativeData, strbuf, 0, length - 1);
            System.Runtime.InteropServices.Marshal.Copy(pNativeData, strbuf, 0, length);

            string data = System.Text.Encoding.UTF8.GetString(strbuf);
            return data;
        }


        public void CleanUpNativeData(System.IntPtr pNativeData)
        {
            System.Runtime.InteropServices.Marshal.FreeHGlobal(pNativeData);
        }


        public void CleanUpManagedData(object managedObj)
        {
        }


        public int GetNativeDataSize()
        {
            return -1;
        }


        public static Utf8Marshaler GetInstance()
        {
            if (_staticInstance == null)
            {
                return _staticInstance = new Utf8Marshaler();
            }
            return _staticInstance;
        }


    } // End Class Utf8Marshaler


} // End Namespace 
