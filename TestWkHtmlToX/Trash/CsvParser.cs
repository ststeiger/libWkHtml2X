
namespace TestWkHtmlToX.Trash
{


    public static class CsvParser
    {


        public static System.Collections.Generic.List<System.Collections.Generic.List<string>> Parse(string path)
        {
            return Parse(path, ';', '"');
        }


        public static System.Data.DataTable ParseTable(string path)
        {
            return ParseTable(path, null, ';', '"');
        }


        public static System.Data.DataTable ParseTable(string path, System.Data.DataTable dt)
        {
            return ParseTable(path, dt, ';', '"');
        }


        public static System.Data.DataTable ParseTable(string path, System.Data.DataTable dt, char delimiter, char qualifier)
        {
            System.Collections.Generic.List<System.Collections.Generic.List<string>> dl = Parse(path, delimiter, qualifier);
            int colCount = dt.Columns.Count;

            if (dt == null)
            {
                dt = new System.Data.DataTable();
                for (int i = 0; i < colCount; ++i)
                {
                    dt.Columns.Add("Column_" + (i + 1).ToString(), typeof(string));
                } // Next i 

            } // End if (dt == null) 

            colCount--;

            for (int i = 0; i < dl.Count; ++i)
            {
                System.Data.DataRow dr = dt.NewRow();

                for (int j = 0; j < dl[i].Count; ++j)
                {
                    if (j > colCount) continue;

                    string item = dl[i][j];
                    // System.Console.WriteLine(item);

                    System.Type columnType = dt.Columns[j].DataType;

                    if (object.ReferenceEquals(columnType, typeof(string)))
                    {
                        dr[j] = item;
                    }
                    else if (object.ReferenceEquals(columnType, typeof(bool)))
                    {
                        bool a = false;

                        if (string.IsNullOrEmpty(item))
                            dr[j] = System.DBNull.Value;
                        else
                        {
                            if (!bool.TryParse(item, out a))
                                throw new System.IO.InvalidDataException("Could not cast \"" + item + "\" to bool.");

                            dr[j] = a;
                        }
                    }
                    else if (object.ReferenceEquals(columnType, typeof(char)))
                    {
                        char a = '\0';
                        
                        if (string.IsNullOrEmpty(item))
                            dr[j] = System.DBNull.Value;
                        else
                        {
                            if (!char.TryParse(item, out a))
                                throw new System.IO.InvalidDataException("Could not cast \"" + item + "\" to char.");

                            dr[j] = a;
                        }
                    }
                    else if (object.ReferenceEquals(columnType, typeof(int)))
                    {
                        int a = 0;

                        if (string.IsNullOrEmpty(item))
                            dr[j] = System.DBNull.Value;
                        else
                        {
                            if (!int.TryParse(item, out a))
                                throw new System.IO.InvalidDataException("Could not cast \"" + item + "\" to int.");

                            dr[j] = a;
                        }
                    }
                    else if (object.ReferenceEquals(columnType, typeof(uint)))
                    {
                        uint a = 0;

                        if (string.IsNullOrEmpty(item))
                            dr[j] = System.DBNull.Value;
                        else
                        {
                            if (!uint.TryParse(item, out a))
                                throw new System.IO.InvalidDataException("Could not cast \"" + item + "\" to uint.");

                            dr[j] = a;
                        }
                    }
                    else if (object.ReferenceEquals(columnType, typeof(long)))
                    {
                        long a = 0;

                        if (string.IsNullOrEmpty(item))
                            dr[j] = System.DBNull.Value;
                        else
                        {
                            if (!long.TryParse(item, out a))
                                throw new System.IO.InvalidDataException("Could not cast \"" + item + "\" to long.");

                            dr[j] = a;
                        }
                    }
                    else if (object.ReferenceEquals(columnType, typeof(ulong)))
                    {
                        ulong a = 0;

                        if (string.IsNullOrEmpty(item))
                            dr[j] = System.DBNull.Value;
                        else
                        {
                            if (!ulong.TryParse(item, out a))
                                throw new System.IO.InvalidDataException("Could not cast \"" + item + "\" to ulong.");

                            dr[j] = a;
                        }
                    }
                    else if (object.ReferenceEquals(columnType, typeof(float)))
                    {
                        float a = 0;

                        if (string.IsNullOrEmpty(item))
                            dr[j] = System.DBNull.Value;
                        else
                        {
                            if (!float.TryParse(item, out a))
                                throw new System.IO.InvalidDataException("Could not cast \"" + item + "\" to float.");

                            dr[j] = a;
                        }
                    }
                    else if (object.ReferenceEquals(columnType, typeof(double)))
                    {
                        double a = 0;
                        
                        if (string.IsNullOrEmpty(item))
                            dr[j] = System.DBNull.Value;
                        else
                        {
                            if (!double.TryParse(item, out a))
                                throw new System.IO.InvalidDataException("Could not cast \"" + item + "\" to double.");

                            dr[j] = a;
                        }
                    }
                    else if (object.ReferenceEquals(columnType, typeof(decimal)))
                    {
                        decimal a = 0;

                        if (string.IsNullOrEmpty(item))
                            dr[j] = System.DBNull.Value;
                        else
                        {
                            if (!decimal.TryParse(item, out a))
                                throw new System.IO.InvalidDataException("Could not cast \"" + item + "\" to decimal.");

                            dr[j] = a;
                        }
                    }
                    else if (object.ReferenceEquals(columnType, typeof(System.DateTime)))
                    {
                        System.DateTime a = System.DateTime.MinValue;

                        if (string.IsNullOrEmpty(item))
                            dr[j] = System.DBNull.Value;
                        else
                        {
                            if (!System.DateTime.TryParse(item, out a))
                                throw new System.IO.InvalidDataException("Could not cast \"" + item + "\" to System.DateTime.");

                            dr[j] = a;
                        }
                    }
                    else
                        throw new System.NotImplementedException("DataType \"" + columnType.FullName + "\" has not been implemented, yet.");
                } // Next j 

                dt.Rows.Add(dr);
            } // Next i 

            return dt;
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


        public static System.Collections.Generic.List<System.Collections.Generic.List<string>> 
            Parse(System.IO.TextReader reader, char delimiter, char qualifier)
        {
            return Parse(reader, 0, delimiter, qualifier);
        }


        public static System.Collections.Generic.List<System.Collections.Generic.List<string>> 
            Parse(System.IO.TextReader reader, int maxNum, char delimiter, char qualifier)
        {
            System.Collections.Generic.List<System.Collections.Generic.List<string>> records = 
                new System.Collections.Generic.List<System.Collections.Generic.List<string>>();

            bool inQuote = false;
            System.Collections.Generic.List<string> record = new System.Collections.Generic.List<string>();
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
                        } // End if (record.Count > 0 || sb.Length > 0)

                        if (record.Count > 0)
                        {
                            //yield return record;
                            records.Add(record);

                            // return if maxNum is reached - fetch all if maxNum = 0
                            if (maxNum == 0 && records.Count == maxNum)
                                return records;
                        } // End if (record.Count > 0)

                        record = new System.Collections.Generic.List<string>(record.Count);
                    } // End else of if (inQuote)
                } // End if (readChar == '\n' || (readChar == '\r' && (char)reader.Peek() == '\n'))
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
                } // End else if (sb.Length == 0 && !inQuote)
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
                } // End else if (readChar == delimiter) 
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
                } // End else if (readChar == qualifier) 
                else
                    sb.Append(readChar);
            } // Whend 

            if (record.Count > 0 || sb.Length > 0)
                record.Add(sb.ToString());

            if (record.Count > 0)
                //yield return record;
                records.Add(record);

            return records;
        } // End Function Parse 


    } // End Class CsvParser


} // End Namespace TestWkHtmlToX.Trash 
