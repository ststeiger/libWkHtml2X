
namespace TestWkHtmlToX
{


    public enum status_t : int
    {
        PENDING = 0, OK = 1, ERROR = 2
    }


    public class Scheduler
    {

        private static readonly object s_queueLock;
        private static System.Threading.Thread s_BackgroundThread;
        private static System.Collections.Generic.LinkedList<ConversionTask> s_TaskList;
        

        static Scheduler()
        {
            s_queueLock = new object();
            s_TaskList = new System.Collections.Generic.LinkedList<ConversionTask>();
            StartSingleThreadProcessing();
        } // End Constructor 


        public static void Ver()
        {
            System.Console.WriteLine("v1");
        } // End Sub Ver 


        private static ulong m_QueueId;

        public static ConversionTask QueueConversion(string html, object id)
        {
            ConversionTask ct = null;
            
            lock (s_queueLock)
            {
                m_QueueId++;
                ct = new ConversionTask(html, id, m_QueueId);
                
                s_TaskList.AddLast(ct);
            } // End Lock s_queueLock

            return ct;
        } // End Function QueueConversion 


        public static byte[] ConvertFile(string html)
        {
            return ConvertFile(html, null);
        } // End Sub ConvertFile 

        public static byte[] ConvertFile(string html, object id)
        {
            byte[] data = null;

            ConversionTask ct = QueueConversion(html, id);
            System.Diagnostics.Trace.WriteLine("testetest");
            System.Console.WriteLine("Queued YOUR item #" + System.Convert.ToString(ct.Id)
                + " as qid[" + System.Convert.ToString(ct.QueueId) + "]"
            );

            bool timeout = !ct.WaitHandle.WaitOne(31000, false);
            if (timeout)
            {
                // throw new System.Exception("Timeout reached.");

                Dequeue(ct);
                ct.TaskComplete(false);

                // OMG, lock will keep timeout...
                if (ct.Status != status_t.OK)
                {
                    System.Console.WriteLine("Cancelled YOUR item #" + ct.Id
                        + ", qid[" + System.Convert.ToString(ct.QueueId) + "]"
                    );

                    return null;
                } // End if (ct.Status != status_t.OK) 

            } // End if (timeout) 

            System.Console.WriteLine("Output for YOUR item #" + System.Convert.ToString(ct.Id) 
                + ", qid[" + ct.QueueId.ToString()
                + "] - Duration " + ct.Duration.ToString()
            );

            if (ct.Status == status_t.ERROR)
            {
                System.Exception ex = ct.Error;
                ct.Data = null;
                ct.Error = null;
                ct = null;
                throw ex;
            }

            data = ct.Data;

            ct.Data = null;
            ct = null;

            return data;
        } // End Function ConvertFile 


        public static ConversionTask Dequeue(ConversionTask s)
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


        public static ConversionTask Dequeue()
        {
            return Dequeue(null);
        }


        public static byte[] Process(string html)
        {
            System.Threading.Thread.Sleep(5000);
            return System.Text.Encoding.UTF8.GetBytes(html);
        } // End Function Process 


        public static void StartSingleThreadProcessing()
        {
            s_BackgroundThread = new System.Threading.Thread(
                delegate()
                {
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

                                if (string.IsNullOrEmpty(s.HTML) || s.HTML.Trim() == string.Empty)
                                {
                                    s.Error = new System.IO.InvalidDataException("Invalid input data...");
                                    s.Status = status_t.ERROR;
                                    s.TaskComplete();
                                    continue;
                                } // End if (string.IsNullOrEmpty(s.HTML) || s.HTML.Trim() == string.Empty)

                                s.Data = Process(s.HTML);

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
        } // End Sub StartSingleThreadProcessing 


    } // End Class Scheduler 


} // End Namespace TestWkHtmlToX 
