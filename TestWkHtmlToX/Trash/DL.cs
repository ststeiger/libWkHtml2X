
namespace TestWkHtmlToX.Trash
{


    internal class DL
    {


        private static void DownloadProgressCallback(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            // Displays the operation identifier, and the transfer progress.
            System.Console.WriteLine("{0}    downloaded {1} of {2} bytes. {3} % complete...",
                (string)e.UserState,
                e.BytesReceived,
                e.TotalBytesToReceive,
                e.ProgressPercentage
            );
        }


        public static void DownloadFileCallback2(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            System.Console.WriteLine("Download complete !");
            System.Windows.Forms.MessageBox.Show("Download complete");
        }


        internal static void foo()
        {
            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                //wc.Headers.Add("Accept", "*/*");

                wc.Headers.Add("Accept-Encoding", "identity;q=1, *;q=0");
                wc.Headers.Add("Accept-Language", "en-US,en;q=0.8,fr;q=0.6");

                //wc.Headers.Add("Connection", "keep-alive");
                
                wc.Headers.Add("Cookie", "lang=1");
                // wc.Headers.Add("Host", "file22.coders.to");
                wc.Headers.Add("Range", "bytes=0-");
                wc.Headers.Add("Referer", "http://coders.to/dsfjajsdfl"); // 12 random chars
                wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.101 Safari/537.36");

                // 60 random chars
                string url = "http://file22.coders.to/asfdasdfadsfadsf/f.zip";


                // wc.DownloadFile(url, @"lol.zip");

                wc.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadFileCallback2);
                // Specify a progress notification handler.
                wc.DownloadProgressChanged += new System.Net.DownloadProgressChangedEventHandler(DownloadProgressCallback);
                wc.DownloadFileAsync( new System.Uri(url, System.UriKind.RelativeOrAbsolute), "asyncLoL.zip");
            }


        }


    }


}
