
namespace wkHtmlToXCore
{


    class CompressionTests
    {


        public static void Test()
        {
            string OriginalText = "Hello WOrld";

            System.Text.Encoding enc = System.Text.Encoding.UTF8;
            byte[] dataBytes = enc.GetBytes(OriginalText);


            byte[] compressed = libWkHtmlToX.Compression.LZF2.Compress(dataBytes);
            byte[] decompressed = libWkHtmlToX.Compression.LZF2.Decompress(compressed);
            string notOriginal = enc.GetString(decompressed);
            System.Console.WriteLine(notOriginal);


            System.Console.WriteLine("Original data is {0} bytes", dataBytes.Length);

            // Compress it
            byte[] Compressed = SevenZip.Compression.LZMA.SevenZipHelper.Compress(dataBytes);
            System.Console.WriteLine("Compressed data is {0} bytes", Compressed.Length);

            // Decompress it
            byte[] Decompressed = SevenZip.Compression.LZMA.SevenZipHelper.Decompress(Compressed);
            System.Console.WriteLine("Decompressed data is {0} bytes", Decompressed.Length);

            // Convert it back to text
            string DecompressedText = enc.GetString(Decompressed);
            System.Console.WriteLine("Is the decompressed text the same as the original? {0}", true);



            //byte[] ba = System.IO.File.ReadAllBytes(@"path");
            //byte[] outp = new byte[ba.Length*2];
            //int size = lz.Compress(ba, ba.Length, outp, outp.Length);
        }


    }


}
