using System;
using System.Collections.Generic;
using System.Text;

namespace TestWkHtmlToX.Trash
{
    class myCSV
    {

        public static void Test()
        {

            const int bufferSize = 1024;
            char[] buffer = new char[bufferSize];

            //new System.IO.FileStream("path", System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);

            bool lastCharFieldDelimiter = false;
            bool insideField = false;

            using (System.IO.FileStream csvFile = System.IO.File.OpenRead(@"C:\Users\anonymous\Downloads\CH\CH_semicolon.csv"))
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(csvFile, System.Text.Encoding.UTF8))
                {
                    int toRead;
                    while ((toRead = sr.Read(buffer, 0, bufferSize)) > 0)
                    {
                        string fx = new string(buffer);
                        System.Console.WriteLine(fx);

                        for (int i = 0; i < toRead; ++i)
                        {
                            if (buffer[i] == '"')
                            {
                                lastCharFieldDelimiter = true;
                                continue;
                            }

                            if (lastCharFieldDelimiter)
                            {
                                lastCharFieldDelimiter = false;

                                if (buffer[i] == '"')
                                {

                                }
                                else
                                    insideField = !insideField;
                            }

                            if (!insideField)
                            {

                                if (buffer[i] == ',')
                                {
                                    // Next field
                                }

                                if (buffer[i] == '\r' || buffer[i] == '\n')
                                {
                                    // AddRow
                                }
                            }

                        }


                        // sb.Append(buffer, 0, count);
                    }

                }

            }

        }

    }
}
