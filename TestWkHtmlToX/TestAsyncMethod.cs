
#define WITH_CALLBACK

// http://www.c-sharpcorner.com/UploadFile/e70b61/asynchronous-methods-calls-in-C-Sharp/
namespace TestWkHtmlToX
{


    public class DataTable
    {
        public System.Collections.Generic.List<string> Columns 
            = new System.Collections.Generic.List<string>();

        public System.Collections.Generic.List<int> Rows 
            = new System.Collections.Generic.List<int>();
    } // End Class DataTable


    public class TestAsyncMethod
    {

        private delegate DataTable async_operation_t(string html);

        // private static async_operation_t asyncMethod;


        public static void EntryPoint()
        {
            // asyncMethod = new async_operation_t(OnPrintCompleteCallback);

            async_operation_t asyncMethod = new async_operation_t(
                delegate (string html)
                {
                    System.Threading.Thread.Sleep(1000);
                    System.Console.WriteLine(html);

                    DataTable dt = new DataTable();

                    dt.Columns.Add("Age");

                    dt.Rows.Add(11);

                    throw new System.Exception("Unexpected error");

                    dt.Rows.Add(12);
                    dt.Rows.Add(13);

                    return dt;
                }
            );


#if WITH_CALLBACK

            // System.IAsyncResult asyncResult = asyncMethod.BeginInvoke("Hello World", new System.AsyncCallback(Callback), null);
            // Pass method as state, so we don't need a global-object
            System.IAsyncResult asyncResult = asyncMethod.BeginInvoke("Hello World"
                , new System.AsyncCallback(OnPrintCompleteCallback)
                , asyncMethod
            );


            //asyncResult.AsyncWaitHandle.WaitOne();
            System.Console.WriteLine("hohoho");

#else
            System.IAsyncResult asyncResult = asyncMethod.BeginInvoke("Hello World", null, null);
            asyncResult.AsyncWaitHandle.WaitOne();
            try
            {
                DataTable res = asyncMethod.EndInvoke(asyncResult);
                System.Console.WriteLine(res);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }


#endif

            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        } // End Sub EntryPoint 


        public static DataTable Print(string html)
        {
            System.Threading.Thread.Sleep(1000);
            System.Console.WriteLine(html);

            DataTable dt = new DataTable();

            dt.Columns.Add("Age");

            dt.Rows.Add(11);
            dt.Rows.Add(12);

            throw new System.Exception("Unexpected Error");

            dt.Rows.Add(13);

            return dt;
        } // End Function Print 


        public static void OnPrintCompleteCallback(System.IAsyncResult ar)
        {
            // ar.AsyncState only contains the function if passed as "state" parameter in BeginInvoke
            async_operation_t asyncMethod = (async_operation_t) ar.AsyncState;

            DataTable dt = null;

            // Catch exception within the async delegate 
            try
            {
                dt = asyncMethod.EndInvoke(ar);

                // IF we dont pass AsyncState, we need to use the "global/class-scope" variable "asyncMethod" 
                // DataTable dt = asyncMethod.EndInvoke(ar);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return;
            }

            foreach (int row in dt.Rows)
            {
                System.Console.WriteLine(row);
            }

        } // End Sub OnPrintCompleteCallback 


    } // End Class TestAsyncMethod 


} // End Namespace TestWkHtmlToX 
