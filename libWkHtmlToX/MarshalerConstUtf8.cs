
using System.Runtime.InteropServices;
using System.Text;


namespace libWkHtmlToX
{


    public class ConstUtf8Marshaler
    {
        public static ConstUtf8Marshaler _staticInstance;

        public System.IntPtr MarshalManagedToNative(string managedObj)
        {
            if (managedObj == null)
                return System.IntPtr.Zero;

            // not null terminated
            byte[] strbuf = Encoding.UTF8.GetBytes(managedObj);
            System.IntPtr buffer = Marshal.AllocHGlobal(strbuf.Length + 1);
            Marshal.Copy(strbuf, 0, buffer, strbuf.Length);

            // write the terminating null
            //Marshal.WriteByte(buffer + strbuf.Length, 0);

            long lngPosEnd = buffer.ToInt64() + strbuf.Length;
            System.IntPtr ptrPosEnd = new System.IntPtr(lngPosEnd);
            Marshal.WriteByte(ptrPosEnd, 0);

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
            Marshal.Copy(pNativeData, strbuf, 0, length);

            string data = Encoding.UTF8.GetString(strbuf);
            return data;
        }


        public void CleanUpNativeData(System.IntPtr pNativeData)
        {
            //Marshal.FreeHGlobal(pNativeData);
        }


        public void CleanUpManagedData(object managedObj)
        {
        }


        public int GetNativeDataSize()
        {
            return -1;
        }


        public static ConstUtf8Marshaler GetInstance()
        {
            if (_staticInstance == null)
            {
                return _staticInstance = new ConstUtf8Marshaler();
            }
            return _staticInstance;
        }


    } // End Class ConstUtf8Marshaler 


} // End Namespace 
