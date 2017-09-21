
namespace libWkHtmlToX
{


    public class PaperSizeString
    {
        public string Width;
        public string Height;
        public string Dimension;
        libWkHtmlToX.PaperKind PaperKind;


        public PaperSizeString()
        { }

        public PaperSizeString(string width, string height, libWkHtmlToX.PaperKind kind)
        {
            this.Width = width;
            this.Height = height;
            this.Dimension = width + "x" + height;
            this.PaperKind = kind;
        }


    }


    public class PaperSizes
    {
        public static PaperSizeString Letter = new PaperSizeString("8.5in", "11in", PaperKind.Letter);
        public static PaperSizeString Legal = new PaperSizeString("8.5in", "14in", PaperKind.Legal);
        public static PaperSizeString A4 = new PaperSizeString("210mm", "297mm", PaperKind.A4);
        public static PaperSizeString CSheet = new PaperSizeString("17in", "22in", PaperKind.CSheet);
        public static PaperSizeString DSheet = new PaperSizeString("22in", "34in", PaperKind.DSheet);
        public static PaperSizeString ESheet = new PaperSizeString("34in", "44in", PaperKind.ESheet);
        public static PaperSizeString LetterSmall = new PaperSizeString("8.5in", "11in", PaperKind.LetterSmall);
        public static PaperSizeString Tabloid = new PaperSizeString("11in", "17in", PaperKind.Tabloid);
        public static PaperSizeString Ledger = new PaperSizeString("17in", "11in", PaperKind.Ledger);
        public static PaperSizeString Statement = new PaperSizeString("5.5in", "8.5in", PaperKind.Statement);
        public static PaperSizeString Executive = new PaperSizeString("7.25in", "10.5in", PaperKind.Executive);
        public static PaperSizeString A3 = new PaperSizeString("297mm", "420mm", PaperKind.A3);
        public static PaperSizeString A4Small = new PaperSizeString("210mm", "297mm", PaperKind.A4Small);
        public static PaperSizeString A5 = new PaperSizeString("148mm", "210mm", PaperKind.A5);
        public static PaperSizeString B4 = new PaperSizeString("250mm", "353mm", PaperKind.B4);
        public static PaperSizeString B5 = new PaperSizeString("176mm", "250mm", PaperKind.B5);
        public static PaperSizeString Folio = new PaperSizeString("8.5in", "13in", PaperKind.Folio);
        public static PaperSizeString Quarto = new PaperSizeString("215mm", "275mm", PaperKind.Quarto);
        public static PaperSizeString Standard10x14 = new PaperSizeString("10in", "14in", PaperKind.Standard10x14);
        public static PaperSizeString Standard11x17 = new PaperSizeString("11in", "17in", PaperKind.Standard11x17);
        public static PaperSizeString Note = new PaperSizeString("8.5in", "11in", PaperKind.Note);
        public static PaperSizeString Number9Envelope = new PaperSizeString("3.875in", "8.875in", PaperKind.Number9Envelope);
        public static PaperSizeString Number10Envelope = new PaperSizeString("4.125in", "9.5in", PaperKind.Number10Envelope);
        public static PaperSizeString Number11Envelope = new PaperSizeString("4.5in", "10.375in", PaperKind.Number11Envelope);
        public static PaperSizeString Number12Envelope = new PaperSizeString("4.75in", "11in", PaperKind.Number12Envelope);
        public static PaperSizeString Number14Envelope = new PaperSizeString("5in", "11.5in", PaperKind.Number14Envelope);
        public static PaperSizeString DLEnvelope = new PaperSizeString("110mm", "220mm", PaperKind.DLEnvelope);
        public static PaperSizeString C5Envelope = new PaperSizeString("162mm", "229mm", PaperKind.C5Envelope);
        public static PaperSizeString C3Envelope = new PaperSizeString("324mm", "458mm", PaperKind.C3Envelope);
        public static PaperSizeString C4Envelope = new PaperSizeString("229mm", "324mm", PaperKind.C4Envelope);
        public static PaperSizeString C6Envelope = new PaperSizeString("114mm", "162mm", PaperKind.C6Envelope);
        public static PaperSizeString C65Envelope = new PaperSizeString("114mm", "229mm", PaperKind.C65Envelope);
        public static PaperSizeString B4Envelope = new PaperSizeString("250mm", "353mm", PaperKind.B4Envelope);
        public static PaperSizeString B5Envelope = new PaperSizeString("176mm", "250mm", PaperKind.B5Envelope);
        public static PaperSizeString B6Envelope = new PaperSizeString("176mm", "125mm", PaperKind.B6Envelope);
        public static PaperSizeString ItalyEnvelope = new PaperSizeString("110mm", "230mm", PaperKind.ItalyEnvelope);
        public static PaperSizeString MonarchEnvelope = new PaperSizeString("3.875in", "7.5in", PaperKind.MonarchEnvelope);
        public static PaperSizeString PersonalEnvelope = new PaperSizeString("3.625in", "6.5in", PaperKind.PersonalEnvelope);
        public static PaperSizeString USStandardFanfold = new PaperSizeString("14.875in", "11in", PaperKind.USStandardFanfold);
        public static PaperSizeString GermanStandardFanfold = new PaperSizeString("8.5in", "12in", PaperKind.GermanStandardFanfold);
        public static PaperSizeString GermanLegalFanfold = new PaperSizeString("8.5in", "13in", PaperKind.GermanLegalFanfold);
        public static PaperSizeString IsoB4 = new PaperSizeString("250mm", "353mm", PaperKind.IsoB4);
        public static PaperSizeString JapanesePostcard = new PaperSizeString("100mm", "148mm", PaperKind.JapanesePostcard);
        public static PaperSizeString Standard9x11 = new PaperSizeString("9in", "11in", PaperKind.Standard9x11);
        public static PaperSizeString Standard10x11 = new PaperSizeString("10in", "11in", PaperKind.Standard10x11);
        public static PaperSizeString Standard15x11 = new PaperSizeString("15in", "11in", PaperKind.Standard15x11);
        public static PaperSizeString InviteEnvelope = new PaperSizeString("220mm", "220mm", PaperKind.InviteEnvelope);
        public static PaperSizeString LetterExtra = new PaperSizeString("9.275in", "12in", PaperKind.LetterExtra);
        public static PaperSizeString LegalExtra = new PaperSizeString("9.275in", "15in", PaperKind.LegalExtra);
        public static PaperSizeString TabloidExtra = new PaperSizeString("11.69in", "18in", PaperKind.TabloidExtra);
        public static PaperSizeString A4Extra = new PaperSizeString("236mm", "322mm", PaperKind.A4Extra);
        public static PaperSizeString LetterTransverse = new PaperSizeString("8.275in", "11in", PaperKind.LetterTransverse);
        public static PaperSizeString A4Transverse = new PaperSizeString("210mm", "297mm", PaperKind.A4Transverse);
        public static PaperSizeString LetterExtraTransverse = new PaperSizeString("9.275in", "12in", PaperKind.LetterExtraTransverse);
        public static PaperSizeString APlus = new PaperSizeString("227mm", "356mm", PaperKind.APlus);
        public static PaperSizeString BPlus = new PaperSizeString("305mm", "487mm", PaperKind.BPlus);
        public static PaperSizeString LetterPlus = new PaperSizeString("8.5in", "12.69in", PaperKind.LetterPlus);
        public static PaperSizeString A4Plus = new PaperSizeString("210mm", "330mm", PaperKind.A4Plus);
        public static PaperSizeString A5Transverse = new PaperSizeString("148mm", "210mm", PaperKind.A5Transverse);
        public static PaperSizeString B5Transverse = new PaperSizeString("182mm", "257mm", PaperKind.B5Transverse);
        public static PaperSizeString A3Extra = new PaperSizeString("322mm", "445mm", PaperKind.A3Extra);
        public static PaperSizeString A5Extra = new PaperSizeString("174mm", "235mm", PaperKind.A5Extra);
        public static PaperSizeString B5Extra = new PaperSizeString("201mm", "276mm", PaperKind.B5Extra);
        public static PaperSizeString A2 = new PaperSizeString("420mm", "594mm", PaperKind.A2);
        public static PaperSizeString A3Transverse = new PaperSizeString("297mm", "420mm", PaperKind.A3Transverse);
        public static PaperSizeString A3ExtraTransverse = new PaperSizeString("322mm", "445mm", PaperKind.A3ExtraTransverse);
        public static PaperSizeString JapaneseDoublePostcard = new PaperSizeString("200mm", "148mm", PaperKind.JapaneseDoublePostcard);
        public static PaperSizeString A6 = new PaperSizeString("105mm", "148mm", PaperKind.A6);
        public static PaperSizeString LetterRotated = new PaperSizeString("11in", "8.5in", PaperKind.LetterRotated);
        public static PaperSizeString A3Rotated = new PaperSizeString("420mm", "297mm", PaperKind.A3Rotated);
        public static PaperSizeString A4Rotated = new PaperSizeString("297mm", "210mm", PaperKind.A4Rotated);
        public static PaperSizeString A5Rotated = new PaperSizeString("210mm", "148mm", PaperKind.A5Rotated);
        public static PaperSizeString B4JisRotated = new PaperSizeString("364mm", "257mm", PaperKind.B4JisRotated);
        public static PaperSizeString B5JisRotated = new PaperSizeString("257mm", "182mm", PaperKind.B5JisRotated);
        public static PaperSizeString JapanesePostcardRotated = new PaperSizeString("148mm", "100mm", PaperKind.JapanesePostcardRotated);
        public static PaperSizeString JapaneseDoublePostcardRotated = new PaperSizeString("148mm", "200mm", PaperKind.JapaneseDoublePostcardRotated);
        public static PaperSizeString A6Rotated = new PaperSizeString("148mm", "105mm", PaperKind.A6Rotated);
        public static PaperSizeString B6Jis = new PaperSizeString("128mm", "182mm", PaperKind.B6Jis);
        public static PaperSizeString B6JisRotated = new PaperSizeString("182mm", "128mm", PaperKind.B6JisRotated);
        public static PaperSizeString Standard12x11 = new PaperSizeString("12in", "11in", PaperKind.Standard12x11);
        public static PaperSizeString Prc16K = new PaperSizeString("146mm", "215mm", PaperKind.Prc16K);
        public static PaperSizeString Prc32K = new PaperSizeString("97mm", "151mm", PaperKind.Prc32K);
        public static PaperSizeString Prc32KBig = new PaperSizeString("97mm", "151mm", PaperKind.Prc32KBig);
        public static PaperSizeString PrcEnvelopeNumber1 = new PaperSizeString("102mm", "165mm", PaperKind.PrcEnvelopeNumber1);
        public static PaperSizeString PrcEnvelopeNumber2 = new PaperSizeString("102mm", "176mm", PaperKind.PrcEnvelopeNumber2);
        public static PaperSizeString PrcEnvelopeNumber3 = new PaperSizeString("125mm", "176mm", PaperKind.PrcEnvelopeNumber3);
        public static PaperSizeString PrcEnvelopeNumber4 = new PaperSizeString("110mm", "208mm", PaperKind.PrcEnvelopeNumber4);
        public static PaperSizeString PrcEnvelopeNumber5 = new PaperSizeString("110mm", "220mm", PaperKind.PrcEnvelopeNumber5);
        public static PaperSizeString PrcEnvelopeNumber6 = new PaperSizeString("120mm", "230mm", PaperKind.PrcEnvelopeNumber6);
        public static PaperSizeString PrcEnvelopeNumber7 = new PaperSizeString("160mm", "230mm", PaperKind.PrcEnvelopeNumber7);
        public static PaperSizeString PrcEnvelopeNumber8 = new PaperSizeString("120mm", "309mm", PaperKind.PrcEnvelopeNumber8);
        public static PaperSizeString PrcEnvelopeNumber9 = new PaperSizeString("229mm", "324mm", PaperKind.PrcEnvelopeNumber9);
        public static PaperSizeString PrcEnvelopeNumber10 = new PaperSizeString("324mm", "458mm", PaperKind.PrcEnvelopeNumber10);
        public static PaperSizeString Prc16KRotated = new PaperSizeString("146mm", "215mm", PaperKind.Prc16KRotated);
        public static PaperSizeString Prc32KRotated = new PaperSizeString("97mm", "151mm", PaperKind.Prc32KRotated);
        public static PaperSizeString Prc32KBigRotated = new PaperSizeString("97mm", "151mm", PaperKind.Prc32KBigRotated);
        public static PaperSizeString PrcEnvelopeNumber1Rotated = new PaperSizeString("165mm", "102mm", PaperKind.PrcEnvelopeNumber1Rotated);
        public static PaperSizeString PrcEnvelopeNumber2Rotated = new PaperSizeString("176mm", "102mm", PaperKind.PrcEnvelopeNumber2Rotated);
        public static PaperSizeString PrcEnvelopeNumber3Rotated = new PaperSizeString("176mm", "125mm", PaperKind.PrcEnvelopeNumber3Rotated);
        public static PaperSizeString PrcEnvelopeNumber4Rotated = new PaperSizeString("208mm", "110mm", PaperKind.PrcEnvelopeNumber4Rotated);
        public static PaperSizeString PrcEnvelopeNumber5Rotated = new PaperSizeString("220mm", "110mm", PaperKind.PrcEnvelopeNumber5Rotated);
        public static PaperSizeString PrcEnvelopeNumber6Rotated = new PaperSizeString("230mm", "120mm", PaperKind.PrcEnvelopeNumber6Rotated);
        public static PaperSizeString PrcEnvelopeNumber7Rotated = new PaperSizeString("230mm", "160mm", PaperKind.PrcEnvelopeNumber7Rotated);
        public static PaperSizeString PrcEnvelopeNumber8Rotated = new PaperSizeString("309mm", "120mm", PaperKind.PrcEnvelopeNumber8Rotated);
        public static PaperSizeString PrcEnvelopeNumber9Rotated = new PaperSizeString("324mm", "229mm", PaperKind.PrcEnvelopeNumber9Rotated);
        public static PaperSizeString PrcEnvelopeNumber10Rotated = new PaperSizeString("458mm", "324mm", PaperKind.PrcEnvelopeNumber10Rotated);
    }



    public class PaperManager
    {
    
        internal static readonly System.Collections.Generic.Dictionary<libWkHtmlToX.PaperKind, libWkHtmlToX.PaperSizeString> s_PaperSizes =
            new System.Collections.Generic.Dictionary<libWkHtmlToX.PaperKind, libWkHtmlToX.PaperSizeString>();


        public static void SetupPageSizes()
        {
            // paper sizes from http://msdn.microsoft.com/en-us/library/system.drawing.printing.paperkind.aspx
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Letter, new PaperSizeString("8.5in", "11in", PaperKind.Letter));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Legal, new PaperSizeString("8.5in", "14in", PaperKind.Legal));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.A4, new PaperSizeString("210mm", "297mm", PaperKind.A4));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.CSheet, new PaperSizeString("17in", "22in", PaperKind.CSheet));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.DSheet, new PaperSizeString("22in", "34in", PaperKind.DSheet));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.ESheet, new PaperSizeString("34in", "44in", PaperKind.ESheet));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.LetterSmall, new PaperSizeString("8.5in", "11in", PaperKind.LetterSmall));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Tabloid, new PaperSizeString("11in", "17in", PaperKind.Tabloid));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Ledger, new PaperSizeString("17in", "11in", PaperKind.Ledger));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Statement, new PaperSizeString("5.5in", "8.5in", PaperKind.Statement));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Executive, new PaperSizeString("7.25in", "10.5in", PaperKind.Executive));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.A3, new PaperSizeString("297mm", "420mm", PaperKind.A3));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.A4Small, new PaperSizeString("210mm", "297mm", PaperKind.A4Small));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.A5, new PaperSizeString("148mm", "210mm", PaperKind.A5));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.B4, new PaperSizeString("250mm", "353mm", PaperKind.B4));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.B5, new PaperSizeString("176mm", "250mm", PaperKind.B5));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Folio, new PaperSizeString("8.5in", "13in", PaperKind.Folio));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Quarto, new PaperSizeString("215mm", "275mm", PaperKind.Quarto));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Standard10x14, new PaperSizeString("10in", "14in", PaperKind.Standard10x14));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Standard11x17, new PaperSizeString("11in", "17in", PaperKind.Standard11x17));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Note, new PaperSizeString("8.5in", "11in", PaperKind.Note));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Number9Envelope, new PaperSizeString("3.875in", "8.875in", PaperKind.Number9Envelope));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Number10Envelope, new PaperSizeString("4.125in", "9.5in", PaperKind.Number10Envelope));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Number11Envelope, new PaperSizeString("4.5in", "10.375in", PaperKind.Number11Envelope));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Number12Envelope, new PaperSizeString("4.75in", "11in", PaperKind.Number12Envelope));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Number14Envelope, new PaperSizeString("5in", "11.5in", PaperKind.Number14Envelope));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.DLEnvelope, new PaperSizeString("110mm", "220mm", PaperKind.DLEnvelope));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.C5Envelope, new PaperSizeString("162mm", "229mm", PaperKind.C5Envelope));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.C3Envelope, new PaperSizeString("324mm", "458mm", PaperKind.C3Envelope));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.C4Envelope, new PaperSizeString("229mm", "324mm", PaperKind.C4Envelope));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.C6Envelope, new PaperSizeString("114mm", "162mm", PaperKind.C6Envelope));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.C65Envelope, new PaperSizeString("114mm", "229mm", PaperKind.C65Envelope));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.B4Envelope, new PaperSizeString("250mm", "353mm", PaperKind.B4Envelope));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.B5Envelope, new PaperSizeString("176mm", "250mm", PaperKind.B5Envelope));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.B6Envelope, new PaperSizeString("176mm", "125mm", PaperKind.B6Envelope));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.ItalyEnvelope, new PaperSizeString("110mm", "230mm", PaperKind.ItalyEnvelope));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.MonarchEnvelope, new PaperSizeString("3.875in", "7.5in", PaperKind.MonarchEnvelope));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.PersonalEnvelope, new PaperSizeString("3.625in", "6.5in", PaperKind.PersonalEnvelope));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.USStandardFanfold, new PaperSizeString("14.875in", "11in", PaperKind.USStandardFanfold));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.GermanStandardFanfold, new PaperSizeString("8.5in", "12in", PaperKind.GermanStandardFanfold));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.GermanLegalFanfold, new PaperSizeString("8.5in", "13in", PaperKind.GermanLegalFanfold));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.IsoB4, new PaperSizeString("250mm", "353mm", PaperKind.IsoB4));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.JapanesePostcard, new PaperSizeString("100mm", "148mm", PaperKind.JapanesePostcard));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Standard9x11, new PaperSizeString("9in", "11in", PaperKind.Standard9x11));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Standard10x11, new PaperSizeString("10in", "11in", PaperKind.Standard10x11));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Standard15x11, new PaperSizeString("15in", "11in", PaperKind.Standard15x11));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.InviteEnvelope, new PaperSizeString("220mm", "220mm", PaperKind.InviteEnvelope));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.LetterExtra, new PaperSizeString("9.275in", "12in", PaperKind.LetterExtra));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.LegalExtra, new PaperSizeString("9.275in", "15in", PaperKind.LegalExtra));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.TabloidExtra, new PaperSizeString("11.69in", "18in", PaperKind.TabloidExtra));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.A4Extra, new PaperSizeString("236mm", "322mm", PaperKind.A4Extra));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.LetterTransverse, new PaperSizeString("8.275in", "11in", PaperKind.LetterTransverse));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.A4Transverse, new PaperSizeString("210mm", "297mm", PaperKind.A4Transverse));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.LetterExtraTransverse, new PaperSizeString("9.275in", "12in", PaperKind.LetterExtraTransverse));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.APlus, new PaperSizeString("227mm", "356mm", PaperKind.APlus));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.BPlus, new PaperSizeString("305mm", "487mm", PaperKind.BPlus));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.LetterPlus, new PaperSizeString("8.5in", "12.69in", PaperKind.LetterPlus));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.A4Plus, new PaperSizeString("210mm", "330mm", PaperKind.A4Plus));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.A5Transverse, new PaperSizeString("148mm", "210mm", PaperKind.A5Transverse));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.B5Transverse, new PaperSizeString("182mm", "257mm", PaperKind.B5Transverse));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.A3Extra, new PaperSizeString("322mm", "445mm", PaperKind.A3Extra));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.A5Extra, new PaperSizeString("174mm", "235mm", PaperKind.A5Extra));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.B5Extra, new PaperSizeString("201mm", "276mm", PaperKind.B5Extra));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.A2, new PaperSizeString("420mm", "594mm", PaperKind.A2));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.A3Transverse, new PaperSizeString("297mm", "420mm", PaperKind.A3Transverse));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.A3ExtraTransverse, new PaperSizeString("322mm", "445mm", PaperKind.A3ExtraTransverse));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.JapaneseDoublePostcard, new PaperSizeString("200mm", "148mm", PaperKind.JapaneseDoublePostcard));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.A6, new PaperSizeString("105mm", "148mm", PaperKind.A6));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.LetterRotated, new PaperSizeString("11in", "8.5in", PaperKind.LetterRotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.A3Rotated, new PaperSizeString("420mm", "297mm", PaperKind.A3Rotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.A4Rotated, new PaperSizeString("297mm", "210mm", PaperKind.A4Rotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.A5Rotated, new PaperSizeString("210mm", "148mm", PaperKind.A5Rotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.B4JisRotated, new PaperSizeString("364mm", "257mm", PaperKind.B4JisRotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.B5JisRotated, new PaperSizeString("257mm", "182mm", PaperKind.B5JisRotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.JapanesePostcardRotated, new PaperSizeString("148mm", "100mm", PaperKind.JapanesePostcardRotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.JapaneseDoublePostcardRotated, new PaperSizeString("148mm", "200mm", PaperKind.JapaneseDoublePostcardRotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.A6Rotated, new PaperSizeString("148mm", "105mm", PaperKind.A6Rotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.B6Jis, new PaperSizeString("128mm", "182mm", PaperKind.B6Jis));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.B6JisRotated, new PaperSizeString("182mm", "128mm", PaperKind.B6JisRotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Standard12x11, new PaperSizeString("12in", "11in", PaperKind.Standard12x11));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Prc16K, new PaperSizeString("146mm", "215mm", PaperKind.Prc16K));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Prc32K, new PaperSizeString("97mm", "151mm", PaperKind.Prc32K));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Prc32KBig, new PaperSizeString("97mm", "151mm", PaperKind.Prc32KBig));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.PrcEnvelopeNumber1, new PaperSizeString("102mm", "165mm", PaperKind.PrcEnvelopeNumber1));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.PrcEnvelopeNumber2, new PaperSizeString("102mm", "176mm", PaperKind.PrcEnvelopeNumber2));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.PrcEnvelopeNumber3, new PaperSizeString("125mm", "176mm", PaperKind.PrcEnvelopeNumber3));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.PrcEnvelopeNumber4, new PaperSizeString("110mm", "208mm", PaperKind.PrcEnvelopeNumber4));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.PrcEnvelopeNumber5, new PaperSizeString("110mm", "220mm", PaperKind.PrcEnvelopeNumber5));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.PrcEnvelopeNumber6, new PaperSizeString("120mm", "230mm", PaperKind.PrcEnvelopeNumber6));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.PrcEnvelopeNumber7, new PaperSizeString("160mm", "230mm", PaperKind.PrcEnvelopeNumber7));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.PrcEnvelopeNumber8, new PaperSizeString("120mm", "309mm", PaperKind.PrcEnvelopeNumber8));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.PrcEnvelopeNumber9, new PaperSizeString("229mm", "324mm", PaperKind.PrcEnvelopeNumber9));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.PrcEnvelopeNumber10, new PaperSizeString("324mm", "458mm", PaperKind.PrcEnvelopeNumber10));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Prc16KRotated, new PaperSizeString("146mm", "215mm", PaperKind.Prc16KRotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Prc32KRotated, new PaperSizeString("97mm", "151mm", PaperKind.Prc32KRotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.Prc32KBigRotated, new PaperSizeString("97mm", "151mm", PaperKind.Prc32KBigRotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.PrcEnvelopeNumber1Rotated, new PaperSizeString("165mm", "102mm", PaperKind.PrcEnvelopeNumber1Rotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.PrcEnvelopeNumber2Rotated, new PaperSizeString("176mm", "102mm", PaperKind.PrcEnvelopeNumber2Rotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.PrcEnvelopeNumber3Rotated, new PaperSizeString("176mm", "125mm", PaperKind.PrcEnvelopeNumber3Rotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.PrcEnvelopeNumber4Rotated, new PaperSizeString("208mm", "110mm", PaperKind.PrcEnvelopeNumber4Rotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.PrcEnvelopeNumber5Rotated, new PaperSizeString("220mm", "110mm", PaperKind.PrcEnvelopeNumber5Rotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.PrcEnvelopeNumber6Rotated, new PaperSizeString("230mm", "120mm", PaperKind.PrcEnvelopeNumber6Rotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.PrcEnvelopeNumber7Rotated, new PaperSizeString("230mm", "160mm", PaperKind.PrcEnvelopeNumber7Rotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.PrcEnvelopeNumber8Rotated, new PaperSizeString("309mm", "120mm", PaperKind.PrcEnvelopeNumber8Rotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.PrcEnvelopeNumber9Rotated, new PaperSizeString("324mm", "229mm", PaperKind.PrcEnvelopeNumber9Rotated));
            s_PaperSizes.Add(libWkHtmlToX.PaperKind.PrcEnvelopeNumber10Rotated, new PaperSizeString("458mm", "324mm", PaperKind.PrcEnvelopeNumber10Rotated));
        }


        static PaperManager()
        {
            SetupPageSizes();
        }

    }



    // Zusammenfassung:
    //     Specifies the standard paper sizes.
    public enum PaperKind
    {
        // Zusammenfassung:
        //     The paper size is defined by the user.
        Custom = 0,
        //
        // Zusammenfassung:
        //     Letter paper (8.5 in. by 11 in.).
        Letter = 1,
        //
        // Zusammenfassung:
        //     Letter small paper (8.5 in. by 11 in.).
        LetterSmall = 2,
        //
        // Zusammenfassung:
        //     Tabloid paper (11 in. by 17 in.).
        Tabloid = 3,
        //
        // Zusammenfassung:
        //     Ledger paper (17 in. by 11 in.).
        Ledger = 4,
        //
        // Zusammenfassung:
        //     Legal paper (8.5 in. by 14 in.).
        Legal = 5,
        //
        // Zusammenfassung:
        //     Statement paper (5.5 in. by 8.5 in.).
        Statement = 6,
        //
        // Zusammenfassung:
        //     Executive paper (7.25 in. by 10.5 in.).
        Executive = 7,
        //
        // Zusammenfassung:
        //     A3 paper (297 mm by 420 mm).
        A3 = 8,
        //
        // Zusammenfassung:
        //     A4 paper (210 mm by 297 mm).
        A4 = 9,
        //
        // Zusammenfassung:
        //     A4 small paper (210 mm by 297 mm).
        A4Small = 10,
        //
        // Zusammenfassung:
        //     A5 paper (148 mm by 210 mm).
        A5 = 11,
        //
        // Zusammenfassung:
        //     B4 paper (250 mm by 353 mm).
        B4 = 12,
        //
        // Zusammenfassung:
        //     B5 paper (176 mm by 250 mm).
        B5 = 13,
        //
        // Zusammenfassung:
        //     Folio paper (8.5 in. by 13 in.).
        Folio = 14,
        //
        // Zusammenfassung:
        //     Quarto paper (215 mm by 275 mm).
        Quarto = 15,
        //
        // Zusammenfassung:
        //     Standard paper (10 in. by 14 in.).
        Standard10x14 = 16,
        //
        // Zusammenfassung:
        //     Standard paper (11 in. by 17 in.).
        Standard11x17 = 17,
        //
        // Zusammenfassung:
        //     Note paper (8.5 in. by 11 in.).
        Note = 18,
        //
        // Zusammenfassung:
        //     #9 envelope (3.875 in. by 8.875 in.).
        Number9Envelope = 19,
        //
        // Zusammenfassung:
        //     #10 envelope (4.125 in. by 9.5 in.).
        Number10Envelope = 20,
        //
        // Zusammenfassung:
        //     #11 envelope (4.5 in. by 10.375 in.).
        Number11Envelope = 21,
        //
        // Zusammenfassung:
        //     #12 envelope (4.75 in. by 11 in.).
        Number12Envelope = 22,
        //
        // Zusammenfassung:
        //     #14 envelope (5 in. by 11.5 in.).
        Number14Envelope = 23,
        //
        // Zusammenfassung:
        //     C paper (17 in. by 22 in.).
        CSheet = 24,
        //
        // Zusammenfassung:
        //     D paper (22 in. by 34 in.).
        DSheet = 25,
        //
        // Zusammenfassung:
        //     E paper (34 in. by 44 in.).
        ESheet = 26,
        //
        // Zusammenfassung:
        //     DL envelope (110 mm by 220 mm).
        DLEnvelope = 27,
        //
        // Zusammenfassung:
        //     C5 envelope (162 mm by 229 mm).
        C5Envelope = 28,
        //
        // Zusammenfassung:
        //     C3 envelope (324 mm by 458 mm).
        C3Envelope = 29,
        //
        // Zusammenfassung:
        //     C4 envelope (229 mm by 324 mm).
        C4Envelope = 30,
        //
        // Zusammenfassung:
        //     C6 envelope (114 mm by 162 mm).
        C6Envelope = 31,
        //
        // Zusammenfassung:
        //     C65 envelope (114 mm by 229 mm).
        C65Envelope = 32,
        //
        // Zusammenfassung:
        //     B4 envelope (250 mm by 353 mm).
        B4Envelope = 33,
        //
        // Zusammenfassung:
        //     B5 envelope (176 mm by 250 mm).
        B5Envelope = 34,
        //
        // Zusammenfassung:
        //     B6 envelope (176 mm by 125 mm).
        B6Envelope = 35,
        //
        // Zusammenfassung:
        //     Italy envelope (110 mm by 230 mm).
        ItalyEnvelope = 36,
        //
        // Zusammenfassung:
        //     Monarch envelope (3.875 in. by 7.5 in.).
        MonarchEnvelope = 37,
        //
        // Zusammenfassung:
        //     6 3/4 envelope (3.625 in. by 6.5 in.).
        PersonalEnvelope = 38,
        //
        // Zusammenfassung:
        //     US standard fanfold (14.875 in. by 11 in.).
        USStandardFanfold = 39,
        //
        // Zusammenfassung:
        //     German standard fanfold (8.5 in. by 12 in.).
        GermanStandardFanfold = 40,
        //
        // Zusammenfassung:
        //     German legal fanfold (8.5 in. by 13 in.).
        GermanLegalFanfold = 41,
        //
        // Zusammenfassung:
        //     ISO B4 (250 mm by 353 mm).
        IsoB4 = 42,
        //
        // Zusammenfassung:
        //     Japanese postcard (100 mm by 148 mm).
        JapanesePostcard = 43,
        //
        // Zusammenfassung:
        //     Standard paper (9 in. by 11 in.).
        Standard9x11 = 44,
        //
        // Zusammenfassung:
        //     Standard paper (10 in. by 11 in.).
        Standard10x11 = 45,
        //
        // Zusammenfassung:
        //     Standard paper (15 in. by 11 in.).
        Standard15x11 = 46,
        //
        // Zusammenfassung:
        //     Invitation envelope (220 mm by 220 mm).
        InviteEnvelope = 47,
        //
        // Zusammenfassung:
        //     Letter extra paper (9.275 in. by 12 in.). This value is specific to the PostScript
        //     driver and is used only by Linotronic printers in order to conserve paper.
        LetterExtra = 50,
        //
        // Zusammenfassung:
        //     Legal extra paper (9.275 in. by 15 in.). This value is specific to the PostScript
        //     driver and is used only by Linotronic printers in order to conserve paper.
        LegalExtra = 51,
        //
        // Zusammenfassung:
        //     Tabloid extra paper (11.69 in. by 18 in.). This value is specific to the
        //     PostScript driver and is used only by Linotronic printers in order to conserve
        //     paper.
        TabloidExtra = 52,
        //
        // Zusammenfassung:
        //     A4 extra paper (236 mm by 322 mm). This value is specific to the PostScript
        //     driver and is used only by Linotronic printers to help save paper.
        A4Extra = 53,
        //
        // Zusammenfassung:
        //     Letter transverse paper (8.275 in. by 11 in.).
        LetterTransverse = 54,
        //
        // Zusammenfassung:
        //     A4 transverse paper (210 mm by 297 mm).
        A4Transverse = 55,
        //
        // Zusammenfassung:
        //     Letter extra transverse paper (9.275 in. by 12 in.).
        LetterExtraTransverse = 56,
        //
        // Zusammenfassung:
        //     SuperA/SuperA/A4 paper (227 mm by 356 mm).
        APlus = 57,
        //
        // Zusammenfassung:
        //     SuperB/SuperB/A3 paper (305 mm by 487 mm).
        BPlus = 58,
        //
        // Zusammenfassung:
        //     Letter plus paper (8.5 in. by 12.69 in.).
        LetterPlus = 59,
        //
        // Zusammenfassung:
        //     A4 plus paper (210 mm by 330 mm).
        A4Plus = 60,
        //
        // Zusammenfassung:
        //     A5 transverse paper (148 mm by 210 mm).
        A5Transverse = 61,
        //
        // Zusammenfassung:
        //     JIS B5 transverse paper (182 mm by 257 mm).
        B5Transverse = 62,
        //
        // Zusammenfassung:
        //     A3 extra paper (322 mm by 445 mm).
        A3Extra = 63,
        //
        // Zusammenfassung:
        //     A5 extra paper (174 mm by 235 mm).
        A5Extra = 64,
        //
        // Zusammenfassung:
        //     ISO B5 extra paper (201 mm by 276 mm).
        B5Extra = 65,
        //
        // Zusammenfassung:
        //     A2 paper (420 mm by 594 mm).
        A2 = 66,
        //
        // Zusammenfassung:
        //     A3 transverse paper (297 mm by 420 mm).
        A3Transverse = 67,
        //
        // Zusammenfassung:
        //     A3 extra transverse paper (322 mm by 445 mm).
        A3ExtraTransverse = 68,
        //
        // Zusammenfassung:
        //     Japanese double postcard (200 mm by 148 mm). Requires Windows 98, Windows
        //     NT 4.0, or later.
        JapaneseDoublePostcard = 69,
        //
        // Zusammenfassung:
        //     A6 paper (105 mm by 148 mm). Requires Windows 98, Windows NT 4.0, or later.
        A6 = 70,
        //
        // Zusammenfassung:
        //     Japanese Kaku #2 envelope. Requires Windows 98, Windows NT 4.0, or later.
        JapaneseEnvelopeKakuNumber2 = 71,
        //
        // Zusammenfassung:
        //     Japanese Kaku #3 envelope. Requires Windows 98, Windows NT 4.0, or later.
        JapaneseEnvelopeKakuNumber3 = 72,
        //
        // Zusammenfassung:
        //     Japanese Chou #3 envelope. Requires Windows 98, Windows NT 4.0, or later.
        JapaneseEnvelopeChouNumber3 = 73,
        //
        // Zusammenfassung:
        //     Japanese Chou #4 envelope. Requires Windows 98, Windows NT 4.0, or later.
        JapaneseEnvelopeChouNumber4 = 74,
        //
        // Zusammenfassung:
        //     Letter rotated paper (11 in. by 8.5 in.).
        LetterRotated = 75,
        //
        // Zusammenfassung:
        //     A3 rotated paper (420 mm by 297 mm).
        A3Rotated = 76,
        //
        // Zusammenfassung:
        //     A4 rotated paper (297 mm by 210 mm). Requires Windows 98, Windows NT 4.0,
        //     or later.
        A4Rotated = 77,
        //
        // Zusammenfassung:
        //     A5 rotated paper (210 mm by 148 mm). Requires Windows 98, Windows NT 4.0,
        //     or later.
        A5Rotated = 78,
        //
        // Zusammenfassung:
        //     JIS B4 rotated paper (364 mm by 257 mm). Requires Windows 98, Windows NT
        //     4.0, or later.
        B4JisRotated = 79,
        //
        // Zusammenfassung:
        //     JIS B5 rotated paper (257 mm by 182 mm). Requires Windows 98, Windows NT
        //     4.0, or later.
        B5JisRotated = 80,
        //
        // Zusammenfassung:
        //     Japanese rotated postcard (148 mm by 100 mm). Requires Windows 98, Windows
        //     NT 4.0, or later.
        JapanesePostcardRotated = 81,
        //
        // Zusammenfassung:
        //     Japanese rotated double postcard (148 mm by 200 mm). Requires Windows 98,
        //     Windows NT 4.0, or later.
        JapaneseDoublePostcardRotated = 82,
        //
        // Zusammenfassung:
        //     A6 rotated paper (148 mm by 105 mm). Requires Windows 98, Windows NT 4.0,
        //     or later.
        A6Rotated = 83,
        //
        // Zusammenfassung:
        //     Japanese rotated Kaku #2 envelope. Requires Windows 98, Windows NT 4.0, or
        //     later.
        JapaneseEnvelopeKakuNumber2Rotated = 84,
        //
        // Zusammenfassung:
        //     Japanese rotated Kaku #3 envelope. Requires Windows 98, Windows NT 4.0, or
        //     later.
        JapaneseEnvelopeKakuNumber3Rotated = 85,
        //
        // Zusammenfassung:
        //     Japanese rotated Chou #3 envelope. Requires Windows 98, Windows NT 4.0, or
        //     later.
        JapaneseEnvelopeChouNumber3Rotated = 86,
        //
        // Zusammenfassung:
        //     Japanese rotated Chou #4 envelope. Requires Windows 98, Windows NT 4.0, or
        //     later.
        JapaneseEnvelopeChouNumber4Rotated = 87,
        //
        // Zusammenfassung:
        //     JIS B6 paper (128 mm by 182 mm). Requires Windows 98, Windows NT 4.0, or
        //     later.
        B6Jis = 88,
        //
        // Zusammenfassung:
        //     JIS B6 rotated paper (182 mm by 128 mm). Requires Windows 98, Windows NT
        //     4.0, or later.
        B6JisRotated = 89,
        //
        // Zusammenfassung:
        //     Standard paper (12 in. by 11 in.). Requires Windows 98, Windows NT 4.0, or
        //     later.
        Standard12x11 = 90,
        //
        // Zusammenfassung:
        //     Japanese You #4 envelope. Requires Windows 98, Windows NT 4.0, or later.
        JapaneseEnvelopeYouNumber4 = 91,
        //
        // Zusammenfassung:
        //     Japanese You #4 rotated envelope. Requires Windows 98, Windows NT 4.0, or
        //     later.
        JapaneseEnvelopeYouNumber4Rotated = 92,
        //
        // Zusammenfassung:
        //     People's Republic of China 16K paper (146 mm by 215 mm). Requires Windows
        //     98, Windows NT 4.0, or later.
        Prc16K = 93,
        //
        // Zusammenfassung:
        //     People's Republic of China 32K paper (97 mm by 151 mm). Requires Windows
        //     98, Windows NT 4.0, or later.
        Prc32K = 94,
        //
        // Zusammenfassung:
        //     People's Republic of China 32K big paper (97 mm by 151 mm). Requires Windows
        //     98, Windows NT 4.0, or later.
        Prc32KBig = 95,
        //
        // Zusammenfassung:
        //     People's Republic of China #1 envelope (102 mm by 165 mm). Requires Windows
        //     98, Windows NT 4.0, or later.
        PrcEnvelopeNumber1 = 96,
        //
        // Zusammenfassung:
        //     People's Republic of China #2 envelope (102 mm by 176 mm). Requires Windows
        //     98, Windows NT 4.0, or later.
        PrcEnvelopeNumber2 = 97,
        //
        // Zusammenfassung:
        //     People's Republic of China #3 envelope (125 mm by 176 mm). Requires Windows
        //     98, Windows NT 4.0, or later.
        PrcEnvelopeNumber3 = 98,
        //
        // Zusammenfassung:
        //     People's Republic of China #4 envelope (110 mm by 208 mm). Requires Windows
        //     98, Windows NT 4.0, or later.
        PrcEnvelopeNumber4 = 99,
        //
        // Zusammenfassung:
        //     People's Republic of China #5 envelope (110 mm by 220 mm). Requires Windows
        //     98, Windows NT 4.0, or later.
        PrcEnvelopeNumber5 = 100,
        //
        // Zusammenfassung:
        //     People's Republic of China #6 envelope (120 mm by 230 mm). Requires Windows
        //     98, Windows NT 4.0, or later.
        PrcEnvelopeNumber6 = 101,
        //
        // Zusammenfassung:
        //     People's Republic of China #7 envelope (160 mm by 230 mm). Requires Windows
        //     98, Windows NT 4.0, or later.
        PrcEnvelopeNumber7 = 102,
        //
        // Zusammenfassung:
        //     People's Republic of China #8 envelope (120 mm by 309 mm). Requires Windows
        //     98, Windows NT 4.0, or later.
        PrcEnvelopeNumber8 = 103,
        //
        // Zusammenfassung:
        //     People's Republic of China #9 envelope (229 mm by 324 mm). Requires Windows
        //     98, Windows NT 4.0, or later.
        PrcEnvelopeNumber9 = 104,
        //
        // Zusammenfassung:
        //     People's Republic of China #10 envelope (324 mm by 458 mm). Requires Windows
        //     98, Windows NT 4.0, or later.
        PrcEnvelopeNumber10 = 105,
        //
        // Zusammenfassung:
        //     People's Republic of China 16K rotated paper (146 mm by 215 mm). Requires
        //     Windows 98, Windows NT 4.0, or later.
        Prc16KRotated = 106,
        //
        // Zusammenfassung:
        //     People's Republic of China 32K rotated paper (97 mm by 151 mm). Requires
        //     Windows 98, Windows NT 4.0, or later.
        Prc32KRotated = 107,
        //
        // Zusammenfassung:
        //     People's Republic of China 32K big rotated paper (97 mm by 151 mm). Requires
        //     Windows 98, Windows NT 4.0, or later.
        Prc32KBigRotated = 108,
        //
        // Zusammenfassung:
        //     People's Republic of China #1 rotated envelope (165 mm by 102 mm). Requires
        //     Windows 98, Windows NT 4.0, or later.
        PrcEnvelopeNumber1Rotated = 109,
        //
        // Zusammenfassung:
        //     People's Republic of China #2 rotated envelope (176 mm by 102 mm). Requires
        //     Windows 98, Windows NT 4.0, or later.
        PrcEnvelopeNumber2Rotated = 110,
        //
        // Zusammenfassung:
        //     People's Republic of China #3 rotated envelope (176 mm by 125 mm). Requires
        //     Windows 98, Windows NT 4.0, or later.
        PrcEnvelopeNumber3Rotated = 111,
        //
        // Zusammenfassung:
        //     People's Republic of China #4 rotated envelope (208 mm by 110 mm). Requires
        //     Windows 98, Windows NT 4.0, or later.
        PrcEnvelopeNumber4Rotated = 112,
        //
        // Zusammenfassung:
        //     People's Republic of China Envelope #5 rotated envelope (220 mm by 110 mm).
        //     Requires Windows 98, Windows NT 4.0, or later.
        PrcEnvelopeNumber5Rotated = 113,
        //
        // Zusammenfassung:
        //     People's Republic of China #6 rotated envelope (230 mm by 120 mm). Requires
        //     Windows 98, Windows NT 4.0, or later.
        PrcEnvelopeNumber6Rotated = 114,
        //
        // Zusammenfassung:
        //     People's Republic of China #7 rotated envelope (230 mm by 160 mm). Requires
        //     Windows 98, Windows NT 4.0, or later.
        PrcEnvelopeNumber7Rotated = 115,
        //
        // Zusammenfassung:
        //     People's Republic of China #8 rotated envelope (309 mm by 120 mm). Requires
        //     Windows 98, Windows NT 4.0, or later.
        PrcEnvelopeNumber8Rotated = 116,
        //
        // Zusammenfassung:
        //     People's Republic of China #9 rotated envelope (324 mm by 229 mm). Requires
        //     Windows 98, Windows NT 4.0, or later.
        PrcEnvelopeNumber9Rotated = 117,
        //
        // Zusammenfassung:
        //     People's Republic of China #10 rotated envelope (458 mm by 324 mm). Requires
        //     Windows 98, Windows NT 4.0, or later.
        PrcEnvelopeNumber10Rotated = 118,
    }


}
