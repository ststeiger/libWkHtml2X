
namespace libWkHtml2X
{


    public class TestScheduler
    {


        public static void Test()
        {
            Scheduler.Ver();
            // System.Threading.Thread.Sleep(5000);

            System.Random r = new System.Random();
            for (int j = 0; j < 20; ++j)
            {
                System.Threading.Thread thread = new System.Threading.Thread(delegate (object o)
                {
                    int rInt = r.Next(1, 4); //for ints
                    System.Threading.Thread.Sleep(rInt * 1000);

                    byte[] data = Scheduler.ConvertFile("Test " + System.Convert.ToString(o), (int)o);
                    if (data == null)
                        return;

                    System.Console.WriteLine(System.Text.Encoding.UTF8.GetString(data));
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
