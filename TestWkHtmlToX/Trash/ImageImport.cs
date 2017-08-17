
namespace TestWkHtmlToX
{


    public class ImageImport
    {


        public static void BankCler()
        {
            byte[] ba = System.IO.File.ReadAllBytes(@"D:\username\Documents\Visual Studio 2013\TFS\COR-Basic\COR-Basic\Basic\Basic\images\Logo\logo_bank_cler.png");
            string hex = "0x" + System.BitConverter.ToString(ba).Replace("-", "");
            System.Console.WriteLine(hex);
        }


    }


}
