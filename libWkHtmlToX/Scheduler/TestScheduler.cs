
namespace libWkHtml2X
{


    public class TestScheduler
    {


        public static void Test()
        {
            Scheduler.Ver();
            // System.Threading.Thread.Sleep(5000);

            string testHtmlTemplate = @"<!doctype html>
<html>
<head>
<title>Test</title>
<script type=""text/javascript"">
</script>

<style type=""text/css"" media=""all"">

div
{
    background-color: red !important;
}

</style>
</head>
<body>

<div style=""display: block; background-color: hotpink;"">Job {@threadid} QueueId {@qid} Message: {@testMessage}</div>

</body>
</html>
";


            System.Random r = new System.Random();
            for (int j = 0; j < 21; ++j)
            {
                System.Threading.Thread thread = new System.Threading.Thread(delegate (object args)
                {
                    // long coInit = libWkHtml2X.CoInitHelper.CoInitialize();
                    // System.Console.WriteLine("CoInitialize: {0}", coInit);

                    int rInt = r.Next(1, 4); //for ints
                    System.Threading.Thread.Sleep(rInt * 1000);

                    int threadId = (int)args;
                    string treadIdString = System.Convert.ToString(threadId);
                    string testMessage = testHtmlTemplate.Replace("{@threadid}", treadIdString).Replace("{@testMessage}", "Message " + treadIdString);

                    // Hier wird konvertiert.
                    // byte[] data = Scheduler.ConvertFile(testMessage, (int)o, null);
                    byte[] data = Scheduler.ConvertFile(testMessage, threadId, delegate (object queueId) 
                        {
                            testMessage = testMessage.Replace("{@qid}", System.Convert.ToString(queueId));
                            // System.Console.WriteLine("theoretically converting" + testMessage );
                            // libWkHtml2X.Converter.CreatePdf(xml, gs, os);
                            return libWkHtml2X.Converter.CreatePdf(testMessage);
                        }
                    );

                    if (data == null)
                    {
                        System.Console.WriteLine("Converted job {0} got NO bytes.", treadIdString);
                        return;
                    }

                    // System.Console.WriteLine(System.Text.Encoding.UTF8.GetString(data));
                    System.IO.File.WriteAllBytes(@"D:\Test\Job" + treadIdString + ".pdf", data);
                })
                { IsBackground = true };
                thread.Start(j);

            } // Next j 


#if false

            System.Threading.Thread.Sleep(10000);


            for (int j = 0; j < 100; ++j)
            {
                System.Threading.Thread thread = new System.Threading.Thread(delegate ()
                {
                    int i = j;
                    int rInt = r.Next(0, 5); //for ints
                    System.Threading.Thread.Sleep(rInt * 1000);

                    byte[] data = Scheduler.ConvertFile("hello " + i.ToString());
                    if (data == null)
                        return;

                    System.Console.WriteLine(System.Text.Encoding.UTF8.GetString(data));
                })
                { IsBackground = true };
                thread.Start();
            } // Next j 
#endif

            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        } // End Sub Test 


    }


}
