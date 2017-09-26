
namespace libWkHtmlToX
{


    public enum status_t : int
    {
        PENDING = 0, OK = 1, ERROR = 2
    } // End Enum status_t 


    public class Scheduler
    {

        private static ulong m_QueueId;
        private static readonly object s_queueLock;
        private static System.Threading.Thread s_BackgroundThread;
        private static System.Collections.Generic.LinkedList<ConversionTask> s_TaskList;
        

        static Scheduler()
        {
            s_queueLock = new object();
            s_TaskList = new System.Collections.Generic.LinkedList<ConversionTask>();
        } // End Constructor 



        private static ConversionTask QueueConversion(ConversionTask.convert_callback_t cb)
        {
            ConversionTask ct = null;
            
            lock (s_queueLock)
            {
                m_QueueId++;
                ct = new ConversionTask(m_QueueId, cb);
                
                s_TaskList.AddLast(ct);
            } // End Lock s_queueLock

            return ct;
        } // End Function QueueConversion 


        public static byte[] ConvertFile(ConversionTask.convert_callback_t cb)
        {
            const int DEFAULT_TIMEOUT = 31000;

            byte[] data = null;

            ConversionTask ct = QueueConversion(cb);
            System.Console.WriteLine("Queued job as qid[" + System.Convert.ToString(ct.QueueId) + "]");

#if NET_2_0
            bool timeout = !ct.WaitHandle.WaitOne(DEFAULT_TIMEOUT, false);
#else
            bool timeout = !ct.WaitHandle.WaitOne(DEFAULT_TIMEOUT);
#endif

            if (timeout)
            {
                // throw new System.Exception("Timeout reached.");

                Dequeue(ct);
                ct.TaskComplete(false);

                // OMG, lock will keep timeout...
                if (ct.Status != status_t.OK)
                {
                    System.Console.WriteLine("Canceled qid[" + System.Convert.ToString(ct.QueueId) + "]");
                    throw new System.TimeoutException("DEFAULT_TIMEOUT exceeded. Canceled qid[" + System.Convert.ToString(ct.QueueId) + "]");
                    // return null;
                } // End if (ct.Status != status_t.OK) 

            } // End if (timeout) 

            System.Console.WriteLine("Output for qid[" + ct.QueueId.ToString() + "] - Duration " + ct.QueueDuration.ToString()
                + "(Actual: " + ct.ConversionDuration.ToString() + ")"
            );

            if (ct.Status == status_t.ERROR)
            {
                System.Exception ex = ct.Error;
                ct.Data = null;
                ct.Error = null;
                ct = null;
                throw ex;
            } // End if (ct.Status == status_t.ERROR) 

            data = ct.Data;

            ct.Data = null;
            ct = null;

            return data;
        } // End Function ConvertFile 


        private static ConversionTask Dequeue(ConversionTask s)
        {
            ConversionTask ret = null;

            lock (s_queueLock)
            {
                if (s_TaskList.First == null)
                    return null;

                if (s == null)
                {
                    ret = s_TaskList.First.Value;
                    s_TaskList.RemoveFirst();
                    return ret;
                } // End if (s == null) 

                if (s_TaskList.Contains(s))
                    s_TaskList.Remove(s);
            } // End Lock s_queueLock 

            return null;
        } // End Sub Dequeue 


        private static ConversionTask Dequeue()
        {
            return Dequeue(null);
        } // End Sub Dequeue 


        // For Testing
        // public static byte[] Process(string html)
        // { System.Threading.Thread.Sleep(5000); return System.Text.Encoding.UTF8.GetBytes(html); } // End Function Process 
        

        public static System.Threading.Thread Init(string dllDirectory)
        {
            if (string.IsNullOrEmpty(dllDirectory))
                throw new System.ArgumentNullException("dllDirectory cannot be NULL or string.emtpy.");

            System.Console.WriteLine("Starting background thread.");
            
            s_BackgroundThread = new System.Threading.Thread(
                delegate()
                {
                    libWkHtmlToX.CallsInitializer.InitWkhtmlToX(dllDirectory);


                    while (true)
                    {
                        // System.Console.WriteLine("Idle waiting");
                        System.Threading.Thread.Sleep(1000);


                        ConversionTask s = null;

                        while ((s = Dequeue()) != null)
                        {

                            try
                            {
                                s.Data = null;
                                s.Error = null;

                                // Hier wird die Konvertierung ausgeführt.
                                // s.Data = Process("<html><head><title>Test</title></head><body>Test 123</body></html>");
                                s.ToggleConverterStopwatch();

                                try
                                {
                                    s.ConversionCallback.Invoke(s.QueueId);

                                    s.Data = s.ConversionCallback(s.QueueId);
                                    s.ToggleConverterStopwatch();
                                }
                                catch 
                                {
                                    s.ToggleConverterStopwatch();
                                    throw;
                                }
                                

                                if (s.Data == null)
                                {
                                    s.Error = new System.Exception("Unknown conversion error...");
                                    s.Status = status_t.ERROR;
                                    s.TaskComplete();
                                    continue;
                                } // End if (s.Data == null)

                                s.Status = status_t.OK;
                                s.TaskComplete();
                            }
                            catch (System.Exception ex)
                            {
                                s.Data = null;
                                s.Error = ex;
                                s.Status = status_t.ERROR;
                                s.TaskComplete();
                            } // End Catch 

                        } // Whend - queue.Dequeue != null

                    } // Whend while(true) 

                } // End Delegate 

            ) { IsBackground = true };

            s_BackgroundThread.Start();

            return s_BackgroundThread;
        } // End Sub Init 


    } // End Class Scheduler 


} // End Namespace TestWkHtmlToX 
