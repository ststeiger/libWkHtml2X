
namespace libWkHtml2X
{


    public class TestScheduler
    {


        public static void Test()
        {
            libWkHtml2X.CallsInitializer.InitWkhtmlToX();
            Scheduler.Init();
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

            string[] sourceFiles = new string[] {
                 libWkHtml2X.VisualStudioHelper.MapSolutionPath(@"~/TestFiles/1503497977772.svg")
                ,libWkHtml2X.VisualStudioHelper.MapSolutionPath(@"~/TestFiles/1503647812149.svg")
                ,libWkHtml2X.VisualStudioHelper.MapSolutionPath(@"~/TestFiles/1503666084152.svg")
                ,libWkHtml2X.VisualStudioHelper.MapSolutionPath(@"~/TestFiles/1503666154395.svg")
                ,libWkHtml2X.VisualStudioHelper.MapSolutionPath(@"~/TestFiles/TestBug.svg")
                ,libWkHtml2X.VisualStudioHelper.MapSolutionPath(@"~/TestFiles/TestFixed.svg")
        };


            string svgTemplate = @"<!DOCTYPE html>
<html lang=""en-US"" xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1"" />
    <meta http-equiv=""content-type"" content=""text/html; charset=utf-8"" />
    <meta charset=""UTF-8"" />
    <title> Template </title>
    <style type=""text/css"" media=""all"">
        *
        {{
            margin: 0px;
            padding: 0px;
        }}
        
        body
        {{
            display: block;
        }}
    </style>
</head>
<body>{0}</body></html>
";
            

            System.Random r = new System.Random();
            for (int j = 0; j < 21; ++j)
            {
                System.Threading.Thread thread = new System.Threading.Thread(delegate (object args)
                {
                    // long coInit = libWkHtml2X.CoInitHelper.CoInitialize();
                    // System.Console.WriteLine("CoInitialize: {0}", coInit);

                    int rInt = r.Next(1, 4); //for ints
                    int sourceFileIndex = r.Next(0, sourceFiles.Length);
                    System.Threading.Thread.Sleep(rInt * 1000);

                    int threadId = (int)args;
                    string treadIdString = System.Convert.ToString(threadId);
                    string testMessage = testHtmlTemplate.Replace("{@threadid}", treadIdString).Replace("{@testMessage}", "Message " + sourceFileIndex.ToString());


                    string inputSvg = System.IO.File.ReadAllText(sourceFiles[sourceFileIndex], System.Text.Encoding.UTF8);
                    inputSvg = string.Format(svgTemplate, inputSvg);


                    // Hier wird konvertiert.
                    // byte[] data = Scheduler.ConvertFile(testMessage, (int)o, null);
                    byte[] data = Scheduler.ConvertFile(delegate (ulong queueId) 
                        {
                            testMessage = testMessage.Replace("{@qid}", System.Convert.ToString(queueId));
                            // System.Console.WriteLine("theoretically converting" + testMessage );
                            // libWkHtml2X.Converter.CreatePdf(xml, gs, os);
                            // return libWkHtml2X.Converter.CreatePdf(testMessage);
                            return libWkHtml2X.Converter.CreatePdf(inputSvg);
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
                    
                    // byte[] data = Scheduler.ConvertFile("hello " + i.ToString());
                    
                    byte[] data = Scheduler.ConvertFile(
                        delegate (ulong id)
                        {
                            return libWkHtml2X.Converter.CreatePdf("hello " + i.ToString());
                        }
                    );
                    
                    if (data == null)
                        return;
                    
                    // Print the PDF-Text 
                    System.Console.WriteLine(System.Text.Encoding.UTF8.GetString(data));
                })
                { IsBackground = true };
                
                thread.Start();
            } // Next j 
#endif

            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        } // End Sub Test 


    } // End Class TestScheduler 


} // End Namespace libWkHtml2X 
