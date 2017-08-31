﻿
using System.Collections.Generic;


namespace TestWkHtmlToX.Trash
{


    public static class CsvParser
    {

        public static System.Collections.Generic.List<System.Collections.Generic.List<string>> Parse(string path)
        {
            return Parse(path, ';', '"');
        }


        public static System.Collections.Generic.List<System.Collections.Generic.List<string>> Parse
            (string path, char delimiter, char qualifier)
        {
            System.Collections.Generic.List<System.Collections.Generic.List<string>> records = null;

            using (System.IO.FileStream fs = new System.IO.FileStream(path,
                System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read)
            )
            using (System.IO.StreamReader sr = new System.IO.StreamReader(fs, System.Text.Encoding.UTF8))
            {
                records = Parse(sr, delimiter, qualifier);
            }

            return records;
        }


        /*
        private static Tuple<T, IEnumerable<T>> HeadAndTail<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            var en = source.GetEnumerator();
            en.MoveNext();
            return Tuple.Create(en.Current, EnumerateTail(en));
        }

        private static IEnumerable<T> EnumerateTail<T>(IEnumerator<T> en)
        {
            while (en.MoveNext()) yield return en.Current;
        }

        public static IEnumerable<IList<string>> Parse(string content, char delimiter, char qualifier)
        {
            using (var reader = new System.IO.StringReader(content))
                return Parse(reader, delimiter, qualifier);
        }

        public static Tuple<IList<string>, IEnumerable<IList<string>>> ParseHeadAndTail(
            System.IO.TextReader reader, char delimiter, char qualifier)
        {
            return HeadAndTail(Parse(reader, delimiter, qualifier));
        }
        */


        public static List<List<string>> Parse(System.IO.TextReader reader, char delimiter, char qualifier)
        {
            List<List<string>> records = new List<List<string>>();

            bool inQuote = false;
            List<string> record = new List<string>();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            while (reader.Peek() != -1)
            {
                var readChar = (char)reader.Read();

                if (readChar == '\n' || (readChar == '\r' && (char)reader.Peek() == '\n'))
                {
                    // If it's a \r\n combo consume the \n part and throw it away.
                    if (readChar == '\r')
                        reader.Read();

                    if (inQuote)
                    {
                        if (readChar == '\r')
                            sb.Append('\r');
                        sb.Append('\n');
                    }
                    else
                    {
                        if (record.Count > 0 || sb.Length > 0)
                        {
                            record.Add(sb.ToString());
                            //sb.Clear();
                            sb.Length = 0;
                        }

                        if (record.Count > 0)
                            records.Add(record);
                            //yield return record;

                        record = new List<string>(record.Count);
                    }
                }
                else if (sb.Length == 0 && !inQuote)
                {
                    if (readChar == qualifier)
                        inQuote = true;
                    else if (readChar == delimiter)
                    {
                        record.Add(sb.ToString());
                        //sb.Clear();
                        sb.Length = 0;
                    }
                    else if (char.IsWhiteSpace(readChar))
                    {
                        // Ignore leading whitespace
                    }
                    else
                        sb.Append(readChar);
                }
                else if (readChar == delimiter)
                {
                    if (inQuote)
                        sb.Append(delimiter);
                    else
                    {
                        record.Add(sb.ToString());
                        // sb.Clear();
                        sb.Length = 0;
                    }
                }
                else if (readChar == qualifier)
                {
                    if (inQuote)
                    {
                        if ((char)reader.Peek() == qualifier)
                        {
                            reader.Read();
                            sb.Append(qualifier);
                        }
                        else
                            inQuote = false;
                    }
                    else
                        sb.Append(readChar);
                }
                else
                    sb.Append(readChar);
            } // Whend 

            if (record.Count > 0 || sb.Length > 0)
                record.Add(sb.ToString());

            if (record.Count > 0)
                //yield return record;
                records.Add(record);

            return records;
        }
    }
}