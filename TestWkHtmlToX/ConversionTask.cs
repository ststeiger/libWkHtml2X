
namespace TestWkHtmlToX
{

    
    public class ConversionTask
    {
        public string HTML;
        public status_t Status;
        public System.Exception Error;
        public byte[] Data;
        public System.Threading.ManualResetEvent WaitHandle;
        private System.Diagnostics.Stopwatch m_StopWatch;

        public long Duration
        {
            get
            {
                return this.m_StopWatch.ElapsedMilliseconds;
            }
        }


        public void TaskComplete(bool withHandle)
        {
            this.m_StopWatch.Stop();

            if (withHandle)
                this.WaitHandle.Set();
        }

        public void TaskComplete()
        {
            TaskComplete(true);
        }

        public ulong QueueId;
        public object Id;

        public ConversionTask(string html, object id, ulong queueId)
        {
            this.Id = id;
            this.QueueId = queueId;

            this.m_StopWatch = new System.Diagnostics.Stopwatch();
            this.m_StopWatch.Start();
            this.WaitHandle = new System.Threading.ManualResetEvent(false);
            this.Status = status_t.PENDING;
            this.HTML = html;
        }

    }


}
