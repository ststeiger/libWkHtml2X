
namespace InputOutputReadTest
{
    class Program
    {

        // P/Invoke:
        private enum FileType { Unknown, Disk, Char, Pipe };
        private enum StdHandle { Stdin = -10, Stdout = -11, Stderr = -12 };

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern FileType GetFileType(System.IntPtr hdl);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern System.IntPtr GetStdHandle(StdHandle std);



        public static bool IsOutputRedirected
        {
            get { return FileType.Char != GetFileType(GetStdHandle(StdHandle.Stdout)); }
        }
        public static bool IsInputRedirected
        {
            get { return FileType.Char != GetFileType(GetStdHandle(StdHandle.Stdin)); }
        }
        public static bool IsErrorRedirected
        {
            get { return FileType.Char != GetFileType(GetStdHandle(StdHandle.Stderr)); }
        }


        // D:\Stefan.Steiger\Documents\Visual Studio 2017\Projects\libWkHtml2X\InputOutputReadTest\bin\Debug\InputOutputReadTest.exe
        // InputOutputReadTest.exe < info.txt
        static void Main(string[] args)
        {
            string stdin = null;

            //if (System.Console.IsInputRedirected)
            if(Program.IsInputRedirected)
            {
                using (System.IO.Stream stream = System.Console.OpenStandardInput())
                {
                    // This will block waiting for input that never comes
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(stream, System.Console.InputEncoding))
                    {
                        stdin = reader.ReadToEnd();
                    }
                }

            }

            System.IO.File.WriteAllText(@"d:\omg.txt", stdin, System.Text.Encoding.UTF8);
            System.Console.WriteLine("stdin:");
            System.Console.WriteLine(stdin);
        }
    }
}
