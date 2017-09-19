
namespace libWkHtml2X
{


    public class ProcessTesting
    {


        public static void Test()
        {
            string f = @"D:\Stefan.Steiger\Documents\Visual Studio 2017\Projects\libWkHtml2X\TestWkHtmlToX\Libs\0.13.0-alpha\Win\x86-64";

            string wkPdf = System.IO.Path.Combine(f, "wkhtmltopdf.exe");
            string wkImg = System.IO.Path.Combine(f, "wkhtmltoimage.exe");

            
            // string file = VisualStudioHelper.MapSolutionPath(@"~/TestFiles/simplePage.htm");
            // string html = System.IO.File.ReadAllText(file, System.Text.Encoding.UTF8);

            string html = @"<!doctype html>
<html>
<head>
<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
</head>
<body>
Test 123
<h1>فقط</h1><p>قطعا</p>
Hello world: 안녕하세요...
</body>
</html>
";


            string args = "--page-height 30cm --page-width 20cm  -T 0px -B 0px -L 0px -R 0px --zoom 1.0 --disable-smart-shrinking --dpi 300 - \"simpleP.pdf\" ";
            args = "--page-height 30cm --page-width 20cm  -T 0px -B 0px -L 0px -R 0px --zoom 1.0 --disable-smart-shrinking --dpi 300 - - ";

            byte[] pdfBytes = null;

            using (TestProcess p = new TestProcess(html, wkPdf, args))
            {
                p.Start();
                p.WriteStandardInput(html);
                pdfBytes = p.ReadOutputStream();

                System.Console.WriteLine("waiting");
                bool b = p.WaitForExit(5000);                
                System.Console.WriteLine(b);
            } // End Using p 
            System.IO.File.WriteAllBytes(@"d:\test\pdfBytes.pdf", pdfBytes);

        }
    }


    public class TestProcess : System.IDisposable
    {
        private System.Diagnostics.Process CurrentProcess;
        private System.IO.StreamWriter swInputStream;
        private System.IO.BinaryReader swOutputReader;

        private delegate void OutputCallback_t(string strText);
        private OutputCallback_t OutputCallback;
        
        private string m_ExePath;
        private string m_Arguments;


        public TestProcess(string html, string executable, string args)
        {
            this.m_ExePath = executable;
            this.m_Arguments = args;

            OutputCallback = new OutputCallback_t(OnOutput);
        } // End Constructor


        public void OnOutput(string strText)
        {
            // this.txtOutput.AppendText(strText);
            System.Console.WriteLine(strText);
        } // End Sub AddTextToOutputTextBox


        //private void Close()
        //{
        //    swInputStream.WriteLine("Closing program");
        //    swInputStream.Close();

        //    // this.CurrentProcess.WaitForExit();
        //    // this.CurrentProcess.Kill()

        //    this.CurrentProcess.Close();
        //    this.CurrentProcess.Dispose();
        //} // End Sub btnQuit_Click


        private void ConsoleOutputHandler(object sendingProcess, System.Diagnostics.DataReceivedEventArgs outLine)
        {
            if (!string.IsNullOrEmpty(outLine.Data))
            {
                OutputCallback(System.Environment.NewLine + outLine.Data);
            } // End if (!String.IsNullOrEmpty(outLine.Data))

        } // End Sub ConsoleOutputHandler


        public void WriteStandardInput(string command)
        {
            if (this.CurrentProcess.HasExited)
            {
                throw new System.InvalidOperationException("The process has exited...");
            } // End if (this.CurrentProcess.HasExited)

            swInputStream.WriteLine(command);
            swInputStream.Flush();
            // swInputStream.Close();
        } // End Sub Execute


        public int ReadOutputStream(System.IO.Stream targetStream)
        {
            byte[] buffer = new byte[65536];
            int size = 0;
            int bytesRead;
            //while ((bytesRead = this.CurrentProcess.StandardOutput.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
            //{
            //    targetStream.Write(buffer, 0, bytesRead);
            //    size += bytesRead;
            //} // Whend 

            //using (System.IO.BinaryReader reader = new System.IO.BinaryReader(this.CurrentProcess.StandardOutput.BaseStream))
            //{
            //while ((bytesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
            //{
            //    targetStream.Write(buffer, 0, bytesRead);
            //    size += bytesRead;
            //} // Whend 
            //}

            while ((bytesRead = this.swOutputReader.Read(buffer, 0, buffer.Length)) > 0)
            {
                targetStream.Write(buffer, 0, bytesRead);
                size += bytesRead;
            } // Whend 

            return size;
        }


        public byte[] ReadOutputStream()
        {
            byte[] outputBytes = null;

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                int size = this.ReadOutputStream(ms);
                outputBytes = ms.ToArray();
            }

            return outputBytes;
        }


        public void ProcessExited(object sender, System.EventArgs e)
        {
            System.Console.WriteLine("Process stopped.");
        } // End Sub ProcessExited

        public bool WaitForExit(int milliseconds)
        {
            return this.CurrentProcess.WaitForExit(milliseconds);
        } // End Sub ProcessExited


        public void Start()
        {
            this.CurrentProcess = new System.Diagnostics.Process();

            //if (System.Environment.OSVersion.Platform == System.PlatformID.Unix)
            //    this.CurrentProcess.StartInfo.FileName = "/bin/bash";
            //else
            //    this.CurrentProcess.StartInfo.FileName = "cmd.exe";

            this.CurrentProcess.StartInfo.FileName = this.m_ExePath;
            this.CurrentProcess.StartInfo.Arguments = this.m_Arguments;


            this.CurrentProcess.StartInfo.UseShellExecute = false;
            this.CurrentProcess.StartInfo.CreateNoWindow = true;
            this.CurrentProcess.StartInfo.RedirectStandardInput = true;
            this.CurrentProcess.StartInfo.RedirectStandardOutput = true;
            this.CurrentProcess.StartInfo.RedirectStandardError = true;

            this.CurrentProcess.EnableRaisingEvents = true;
            this.CurrentProcess.Exited += new System.EventHandler(ProcessExited);
            this.CurrentProcess.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(ConsoleOutputHandler);
            //this.CurrentProcess.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(ConsoleOutputHandler);

            this.CurrentProcess.Start();
            
            // swInputStream = this.CurrentProcess.StandardInput;
            // https://stackoverflow.com/questions/2855675/process-standardinput-encoding-problem
            this.swInputStream = new System.IO.StreamWriter(this.CurrentProcess.StandardInput.BaseStream, System.Text.Encoding.UTF8);
            // new System.IO.StreamReader(this.CurrentProcess.StandardOutput.BaseStream, System.Text.Encoding.UTF8);
            // new System.IO.BinaryReader(this.CurrentProcess.StandardOutput.BaseStream);
            this.swOutputReader = new System.IO.BinaryReader(this.CurrentProcess.StandardOutput.BaseStream);

            // this.CurrentProcess.BeginOutputReadLine();
            this.CurrentProcess.BeginErrorReadLine();
        } // End Sub Init




        private bool disposedValue = false; // Dient zur Erkennung redundanter Aufrufe.

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: verwalteten Zustand (verwaltete Objekte) entsorgen.
                    // if(this.CurrentProcess != null) this.CurrentProcess.Close();

                    if (this.swInputStream != null)
                        this.swInputStream.Dispose();

                    // if (this.swOutputReader != null) this.swOutputReader.Close();

                    if (this.CurrentProcess != null)
                        this.CurrentProcess.Dispose();
                }

                // TODO: nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer weiter unten überschreiben.
                // TODO: große Felder auf Null setzen.

                disposedValue = true;
            }
        }

        // TODO: Finalizer nur überschreiben, wenn Dispose(bool disposing) weiter oben Code für die Freigabe nicht verwalteter Ressourcen enthält.
        // ~TestProcess() {
        //   // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
        //   Dispose(false);
        // }

        // Dieser Code wird hinzugefügt, um das Dispose-Muster richtig zu implementieren.
        void System.IDisposable.Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
            Dispose(true);
            // TODO: Auskommentierung der folgenden Zeile aufheben, wenn der Finalizer weiter oben überschrieben wird.
            // GC.SuppressFinalize(this);
        }


    } // End Class TestProcess


} // End Namespace TestWkHtmlToX
