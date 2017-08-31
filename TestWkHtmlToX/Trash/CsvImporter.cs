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
            string fileName = @"C:\Users\anonymous\Downloads\CH\CH_semicolon.csv";
            using (System.Data.DataTable dt = new System.Data.DataTable())
            {

                using (System.Data.Common.DbDataAdapter da = new System.Data.SqlClient.SqlDataAdapter("SELECT * FROM geoname WHERE (1=2)", gc()))
                {
                    System.Data.Common.DbCommandBuilder cb = new System.Data.SqlClient.SqlCommandBuilder();
                    cb.DataAdapter = da;
                    da.DeleteCommand = cb.GetDeleteCommand();
                    da.InsertCommand = cb.GetInsertCommand();
                    da.UpdateCommand = cb.GetUpdateCommand();

                    da.Fill(dt);
                    System.Console.WriteLine(dt.Columns.Count);

                    CsvParser.ParseTable(fileName, dt);

                    da.Update(dt);
                } // End Using da

            } // End Using dt

        } // End Sub TestImport 


    }
}
