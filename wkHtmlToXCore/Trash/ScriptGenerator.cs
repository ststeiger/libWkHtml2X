
namespace wkHtmlToXCore
{


    public class ScriptGenerator
    {


        // ScriptGenerator.FileToHexString();
        public static void FileToHexString()
        {
            // byte[] file = System.IO.File.ReadAllBytes(@"P:\BaslerKB\Logos\Logo Bank Cler_20170919_300dpi.png");
            byte[] file = System.IO.File.ReadAllBytes(@"D:\username\Desktop\b-cler_logo_pos_rgb.png");

            string sql = @"
-- SELECT *, [MDT_Image] FROM T_SYS_Ref_Mandant WHERE MDT_Lang_DE = 'Bank Coop' 

UPDATE T_SYS_Ref_Mandant 
SET MDT_Image = 0x" + System.BitConverter.ToString(file).Replace("-", "") + @" 
-- WHERE MDT_Lang_DE = 'Bank Coop' 
WHERE MDT_Kurz_DE = 'BC' 
";

            System.IO.File.WriteAllText(@"D:\Stefan.Steiger\Desktop\logo_cler.sql", sql, System.Text.Encoding.UTF8);

            System.Console.WriteLine(sql);
        } // End Function FileToHexString 


    } // End Class ScriptGenerator 


} // End Namespace wkHtmlToXCore 
