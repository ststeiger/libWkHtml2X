
namespace libWkHtml2X
{

    
    public class ConversionTask
    {

        public delegate byte[] convert_callback_t(ulong queueId);

        public ulong QueueId;

        public status_t Status;
        public System.Exception Error;
        public byte[] Data;

        public System.Threading.ManualResetEvent WaitHandle;

        private System.Diagnostics.Stopwatch m_QueueStopWatch;
        private System.Diagnostics.Stopwatch m_ConverterStopWatch;

        public convert_callback_t ConversionCallback;


        public long QueueDuration
        {
            get
            {
                return this.m_QueueStopWatch.ElapsedMilliseconds;
            }

        } // End Property Duration 


        public long ConversionDuration
        {
            get
            {
                return this.m_ConverterStopWatch.ElapsedMilliseconds;
            }

        } // End Property Duration 


        public void ToggleConverterStopwatch()
        {
            if (this.m_ConverterStopWatch.IsRunning)
                this.m_ConverterStopWatch.Stop();
            else 
                this.m_ConverterStopWatch.Start();
        } // End Sub TaskComplete 


        public void TaskComplete(bool withHandle)
        {
            this.m_QueueStopWatch.Stop();

            if (withHandle)
                this.WaitHandle.Set();
        } // End Sub TaskComplete 


        public void TaskComplete()
        {
            TaskComplete(true);
        } // End Sub TaskComplete 


        public ConversionTask(ulong queueId, convert_callback_t conversionCallback)
        {
            this.m_QueueStopWatch = new System.Diagnostics.Stopwatch();
            this.m_QueueStopWatch.Start();

            this.m_ConverterStopWatch = new System.Diagnostics.Stopwatch();

            this.QueueId = queueId;
            this.ConversionCallback = conversionCallback;

            this.WaitHandle = new System.Threading.ManualResetEvent(false);
            this.Status = status_t.PENDING;
        } // End Constructor 


    } // End Class ConversionTask 


} // End Namespace libWkHtml2X 
