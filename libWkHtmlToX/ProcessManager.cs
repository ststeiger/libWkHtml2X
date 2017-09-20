
namespace libWkHtml2X
{


    public class ProcessManager : System.IDisposable
    {

        public delegate void OutputCallback_t(string strText);
        public OutputCallback_t OutputCallback;

        private string m_exePath;
        private string m_arguments;

        private System.Diagnostics.Process m_currentProcess;
        private System.IO.StreamWriter m_inputStream;
        private System.IO.BinaryReader m_outputReader;


        public ProcessManager(string html, string executable, string args)
            : this(html, executable, args, new OutputCallback_t(OnOutputDefault))
        { } // End Constructor


        public ProcessManager(string html, string executable, string args, OutputCallback_t outputCallback)
        {
            this.m_exePath = executable;
            this.m_arguments = args;

            OutputCallback = outputCallback;
        } // End Constructor


        private static void OnOutputDefault(string strText)
        {
            // this.txtOutput.AppendText(strText);
            System.Console.WriteLine(strText);
        } // End Sub AddTextToOutputTextBox


        //private void Close()
        //{
        //    this.m_inputStream.WriteLine("Closing program");
        //    this.m_inputStream.Close();

        //    // this.m_currentProcess.WaitForExit();
        //    // this.m_currentProcess.Kill()

        //    this.m_currentProcess.Close();
        //    this.m_currentProcess.Dispose();
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
            if (this.m_currentProcess.HasExited)
            {
                throw new System.InvalidOperationException("The process has exited...");
            } // End if (this.m_currentProcess.HasExited)

            this.m_inputStream.WriteLine(command);
            this.m_inputStream.Flush();
            // this.m_inputStream.Close(); // not present in .NET Core - call Dispose instead
            this.m_inputStream.Dispose();
        } // End Sub Execute


        public int ReadOutputStream(System.IO.Stream targetStream)
        {
            byte[] buffer = new byte[65536];
            int size = 0;
            int bytesRead;
            //while ((bytesRead = this.m_currentProcess.StandardOutput.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
            //{
            //    targetStream.Write(buffer, 0, bytesRead);
            //    size += bytesRead;
            //} // Whend 

            //using (System.IO.BinaryReader reader = new System.IO.BinaryReader(this.m_currentProcess.StandardOutput.BaseStream))
            //{
            //while ((bytesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
            //{
            //    targetStream.Write(buffer, 0, bytesRead);
            //    size += bytesRead;
            //} // Whend 
            //}

            while ((bytesRead = this.m_outputReader.Read(buffer, 0, buffer.Length)) > 0)
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
            return this.m_currentProcess.WaitForExit(milliseconds);
        } // End Sub ProcessExited


        public void Start()
        {
            this.m_currentProcess = new System.Diagnostics.Process();

            //if (System.Environment.OSVersion.Platform == System.PlatformID.Unix)
            //    this.m_currentProcess.StartInfo.FileName = "/bin/bash";
            //else
            //    this.m_currentProcess.StartInfo.FileName = "cmd.exe";

            this.m_currentProcess.StartInfo.FileName = this.m_exePath;
            this.m_currentProcess.StartInfo.Arguments = this.m_arguments;


            this.m_currentProcess.StartInfo.UseShellExecute = false;
            this.m_currentProcess.StartInfo.CreateNoWindow = true;
            this.m_currentProcess.StartInfo.RedirectStandardInput = true;
            this.m_currentProcess.StartInfo.RedirectStandardOutput = true;
            this.m_currentProcess.StartInfo.RedirectStandardError = true;

            this.m_currentProcess.EnableRaisingEvents = true;
            this.m_currentProcess.Exited += new System.EventHandler(ProcessExited);
            this.m_currentProcess.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(ConsoleOutputHandler);
            //this.m_currentProcess.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(ConsoleOutputHandler);

            this.m_currentProcess.Start();


            // this.m_inputStream = this.m_currentProcess.StandardInput;
            // https://stackoverflow.com/questions/2855675/process-standardinput-encoding-problem
            this.m_inputStream = new System.IO.StreamWriter(this.m_currentProcess.StandardInput.BaseStream, System.Text.Encoding.UTF8);
            // new System.IO.StreamReader(this.m_currentProcess.StandardOutput.BaseStream, System.Text.Encoding.UTF8);
            // new System.IO.BinaryReader(this.m_currentProcess.StandardOutput.BaseStream);
            this.m_outputReader = new System.IO.BinaryReader(this.m_currentProcess.StandardOutput.BaseStream);

            // this.m_currentProcess.BeginOutputReadLine();
            this.m_currentProcess.BeginErrorReadLine();
        } // End Sub Init




        private bool disposedValue = false; // Dient zur Erkennung redundanter Aufrufe.

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: verwalteten Zustand (verwaltete Objekte) entsorgen.
                    // if(this.m_currentProcess != null) this.m_currentProcess.Close();

                    if (this.m_inputStream != null)
                        this.m_inputStream.Dispose();

                    if (this.m_outputReader != null)
                    { 
#if NET_2_0
                        this.m_outputReader.Close();
#else
                        this.m_outputReader.Dispose();
#endif
                    }


                    if (this.m_currentProcess != null)
                        this.m_currentProcess.Dispose();
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
