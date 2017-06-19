
#if !NET_2_0

namespace System
{
    public static class StreamExtensions
    {
        public static void Close(this System.IO.Stream strm)
        {
            if(strm != null)
                strm.Dispose();
        }

    }
}

#endif
