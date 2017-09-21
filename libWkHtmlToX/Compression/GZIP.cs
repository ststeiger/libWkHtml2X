
namespace libWkHtmlToX.Compression
{


    internal class GZIP
    {

        // http://www.dotnetperls.com/compress
        /// <summary>
        /// Compresses byte array to new byte array.
        /// </summary>
        public static byte[] Compress(byte[] raw)
        {
            byte[] baRetVal;

            using (System.IO.MemoryStream memstrm = new System.IO.MemoryStream())
            {
                using (System.IO.Compression.GZipStream gzip = new System.IO.Compression.GZipStream(memstrm, System.IO.Compression.CompressionMode.Compress, true))
                {
                    gzip.Write(raw, 0, raw.Length);
                    gzip.Flush();
                } // End Using System.IO.Compression.GZipStream gzip

                memstrm.Flush();
                baRetVal = memstrm.ToArray();
            } // End Using System.IO.MemoryStream memory

            return baRetVal;
        } // End Function Compress


        // http://www.dotnetperls.com/decompress
        public static byte[] Decompress(byte[] gzip)
        {
            byte[] baRetVal = null;
            using (System.IO.MemoryStream ByteStream = new System.IO.MemoryStream(gzip))
            {

                // Create a GZIP stream with decompression mode.
                // ... Then create a buffer and write into while reading from the GZIP stream.
                using (System.IO.Compression.GZipStream stream = new System.IO.Compression.GZipStream(ByteStream
                    , System.IO.Compression.CompressionMode.Decompress))
                {
                    const int size = 4096;
                    byte[] buffer = new byte[size];
                    using (System.IO.MemoryStream memstrm = new System.IO.MemoryStream())
                    {
                        int count = 0;
                        count = stream.Read(buffer, 0, size);
                        while (count > 0)
                        {
                            memstrm.Write(buffer, 0, count);
                            memstrm.Flush();
                            count = stream.Read(buffer, 0, size);
                        } // Whend

                        baRetVal = memstrm.ToArray();
                    } // End Using memstrm

                } // End Using System.IO.Compression.GZipStream stream 

            } // End Using System.IO.MemoryStream ByteStream

            return baRetVal;
        } // End Sub Decompress


        // Deflate is just the compression algorithm. GZip is actually a format.
        // If you use the GZipStream to compress a file (and save it with the extension .gz), 
        // the result can actually be opened by archivers such as WinZip or the gzip tool. 
        // If you compress with a DeflateStream, those tools won't recognize the file.
        public static void CompressFile(string FileToCompress, string CompressedFile)
        {
            //byte[] buffer = new byte[1024 * 1024 * 64];
            byte[] buffer = new byte[1024 * 1024]; // 1MB

            using (System.IO.FileStream sourceFile = System.IO.File.OpenRead(FileToCompress))
            {

                using (System.IO.FileStream destinationFile = System.IO.File.Create(CompressedFile))
                {

                    using (System.IO.Compression.GZipStream output = new System.IO.Compression.GZipStream(destinationFile,
                        System.IO.Compression.CompressionMode.Compress))
                    {

                        int bytesRead = 0;
                        while (bytesRead < sourceFile.Length)
                        {
                            int ReadLength = sourceFile.Read(buffer, 0, buffer.Length);
                            output.Write(buffer, 0, ReadLength);
                            output.Flush();
                            bytesRead += ReadLength;
                        } // Whend

                        destinationFile.Flush();
                    } // End Using System.IO.Compression.GZipStream output

                } // End Using System.IO.FileStream destinationFile 

                // Close the files.
            } // End Using System.IO.FileStream sourceFile

        } // End Sub CompressFile


        public static void DeCompressFile(string CompressedFile, string DeCompressedFile)
        {
            byte[] buffer = new byte[1024 * 1024];

            using (System.IO.FileStream fstrmCompressedFile = System.IO.File.OpenRead(CompressedFile)) // fi.OpenRead())
            {
                using (System.IO.FileStream fstrmDecompressedFile = System.IO.File.Create(DeCompressedFile))
                {
                    using (System.IO.Compression.GZipStream strmUncompress = new System.IO.Compression.GZipStream(fstrmCompressedFile,
                            System.IO.Compression.CompressionMode.Decompress))
                    {
                        int numRead = strmUncompress.Read(buffer, 0, buffer.Length);

                        while (numRead != 0)
                        {
                            fstrmDecompressedFile.Write(buffer, 0, numRead);
                            fstrmDecompressedFile.Flush();
                            numRead = strmUncompress.Read(buffer, 0, buffer.Length);
                        } // Whend

                        //int numRead = 0;

                        //while ((numRead = strmUncompress.Read(buffer, 0, buffer.Length)) != 0)
                        //{
                        //    fstrmDecompressedFile.Write(buffer, 0, numRead);
                        //    fstrmDecompressedFile.Flush();
                        //} // Whend

                    } // End Using System.IO.Compression.GZipStream strmUncompress 

                    fstrmDecompressedFile.Flush();
                } // End Using System.IO.FileStream fstrmCompressedFile 

            } // End Using System.IO.FileStream fstrmCompressedFile 

        } // End Sub DeCompressFile


    } // End Class GZIP 


} // End Namespace libWkHtmlToX.Compression 
