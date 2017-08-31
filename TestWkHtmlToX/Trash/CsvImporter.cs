using System;
using System.Collections.Generic;
using System.Text;

namespace TestWkHtmlToX.Trash
{
    class CsvImporter
    {

        private static string gc()
        {
            System.Data.SqlClient.SqlConnectionStringBuilder csb = new System.Data.SqlClient.SqlConnectionStringBuilder();
            csb.DataSource = System.Environment.MachineName;
            csb.InitialCatalog = "GeoData";

            csb.IntegratedSecurity = true;
            if (!csb.IntegratedSecurity)
            {
                csb.UserID = "";
                csb.Password = "";
            }

            csb.PersistSecurityInfo = false;
            csb.PacketSize = 4096;

            return csb.ConnectionString;
        }


        public static void TestImport()
        {

            string fn = @"C:\Users\anonymous\Downloads\CH\CH_semicolon.csv";

            System.Collections.Generic.List<System.Collections.Generic.List<string>> dl = Trash.CsvParser.Parse(fn);
            System.Console.WriteLine(dl);


            System.Data.DataTable dt = new System.Data.DataTable();

            using (System.Data.Common.DbDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("SELECT * FROM geoname WHERE (1=2)", gc()))
            {
                System.Data.Common.DbCommandBuilder cb = new System.Data.SqlClient.SqlCommandBuilder();
                cb.DataAdapter = da;
                da.DeleteCommand = cb.GetDeleteCommand();
                da.InsertCommand = cb.GetInsertCommand();
                da.UpdateCommand = cb.GetUpdateCommand();



                da.Fill(dt);

                System.Console.WriteLine(dt.Columns.Count);


                /*
                foreach (System.Collections.Generic.List<string> lsColumns in dl)
                {
                    System.Data.DataRow dr = dt.NewRow();

                    for (int j = 0; j < lsColumns.Count; ++j)
                    {
                        string item = lsColumns[j];
                        // System.Console.WriteLine(item);

                        // if (j >= dt.Columns.Count) continue;

                        System.Type tt = dt.Columns[j].DataType;

                        if (object.ReferenceEquals(tt, typeof(string)))
                        {
                            dr[j] = item;
                        }
                        else if (object.ReferenceEquals(tt, typeof(int)))
                        {
                            int a = -1;
                            int.TryParse(item, out a);
                            dr[j] = a;
                        }
                        else if (object.ReferenceEquals(tt, typeof(decimal)))
                        {
                            decimal a = -1;
                            decimal.TryParse(item, out a);
                            dr[j] = a;
                        }
                        else if (object.ReferenceEquals(tt, typeof(System.DateTime)))
                        {
                            System.DateTime a = System.DateTime.MinValue;
                            System.DateTime.TryParse(item, out a);
                            dr[j] = a;
                        }
                        else
                            System.Console.WriteLine(tt);
                    } // Next j 

                    dt.Rows.Add(dr);
                }
                */


                for (int i = 0; i < dl.Count; ++i)
                {
                    System.Data.DataRow dr = dt.NewRow();

                    for (int j = 0; j < dl[i].Count; ++j)
                    {
                        string item = dl[i][j];

                        // System.Console.WriteLine(item);
                        // if (j >= dt.Columns.Count) continue;

                        System.Type tt = dt.Columns[j].DataType;

                        if (object.ReferenceEquals(tt, typeof(string)))
                        {
                            dr[j] = item;
                        }
                        else if (object.ReferenceEquals(tt, typeof(int)))
                        {
                            int a = -1;
                            int.TryParse(item, out a);
                            dr[j] = a;
                        }
                        else if (object.ReferenceEquals(tt, typeof(decimal)))
                        {
                            decimal a = -1;
                            decimal.TryParse(item, out a);
                            dr[j] = a;
                        }
                        else if (object.ReferenceEquals(tt, typeof(System.DateTime)))
                        {
                            System.DateTime a = System.DateTime.MinValue;
                            System.DateTime.TryParse(item, out a);
                            dr[j] = a;
                        }
                        else
                            System.Console.WriteLine(tt);
                    } // Next j 

                    dt.Rows.Add(dr);
                } // Next i 

                da.Update(dt);
            } // End Using da 

        }


    }
}
