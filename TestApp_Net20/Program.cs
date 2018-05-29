
namespace TestApp_Net20
{


    static class Program
    {


        public class DrawingInfo
        {
            public DrawingInfo()
            { }


            protected string m_LC_Lang_en;
            public string LC_Lang_en
            {
                get
                {
                    if (string.IsNullOrEmpty(this.m_LC_Lang_en))
                        return "N/A"; ;

                    return this.m_LC_Lang_en;
                }
                set
                {
                    this.m_LC_Lang_en = value;
                }
            }

            protected string m_PR_Name;
            public string PR_Name
            {
                get
                {
                    if (string.IsNullOrEmpty(this.m_PR_Name))
                        return "N/A"; ;

                    return this.m_PR_Name;
                }
                set
                {
                    this.m_PR_Name = value;
                }
            }
            

            protected string m_FloorDisplayString;
            public string FloorDisplayString
            {
                get
                {
                    if (string.IsNullOrEmpty(m_FloorDisplayString))
                        return "N/A"; ;

                    return this.m_FloorDisplayString;
                }
                set
                {
                    this.m_FloorDisplayString = value;
                }
            }

            public string Darstellung = "Space Type";

            public string FL_Level;
            public string ZO_FLArea_Area;
            
        }


        public static DrawingInfo GetData()
        {
            // in_aperturedwg

            string sql = @"
SELECT TOP 1 
	 T_Ref_Location.LC_Lang_en
	 
	,T_Premises.PR_Name
	
	,ISNULL(T_Ref_FloorType.FT_Lang_en + ' ', '') + CAST(T_Floor.FL_Level AS varchar(10)) AS FloorDisplayString  
	,T_Ref_FloorType.FT_Lang_en
	,T_Floor.FL_Level
	,T_Floor.FL_Sort
	 
	,T_ZO_Premises_DWG.ZO_PRDWG_ApertureDWG
	,T_ZO_Premises_DWG.ZO_PRDWG_ApertureObjID
	 
	,T_ZO_Floor_DWG.ZO_FLDWG_ApertureDWG
	,T_ZO_Floor_DWG.ZO_FLDWG_ApertureObjID
	,T_ZO_Floor_Area.ZO_FLArea_Area
FROM T_Ref_Location 

LEFT JOIN T_Ref_Country
	ON T_Ref_Country.CTR_UID = T_Ref_Location.LC_CTR_UID 
	AND T_Ref_Country.CTR_Status = 1 
	
LEFT JOIN T_Ref_Region
	ON T_Ref_Region.RG_UID = T_Ref_Country.CTR_RG_UID 
	AND T_Ref_Region.RG_Status = 1 
	
LEFT JOIN T_Premises 
	ON T_Premises.PR_LC_UID = LC_UID 
	AND T_Premises.PR_Status = 1 
	AND {fn curdate()} BETWEEN T_Premises.PR_DateFrom AND T_Premises.PR_DateTo 
	
LEFT JOIN T_ZO_Premises_DWG
	ON T_ZO_Premises_DWG.ZO_PRDWG_PR_UID = PR_UID 
	AND T_ZO_Premises_DWG.ZO_PRDWG_Status = 1 
	AND {fn curdate()} BETWEEN  T_ZO_Premises_DWG.ZO_PRDWG_DateFrom AND T_ZO_Premises_DWG.ZO_PRDWG_DateTo 
	
LEFT JOIN T_Floor 
	ON T_Floor.FL_PR_UID = PR_UID 
	AND T_Floor.FL_Status = 1 
	AND {fn curdate()} BETWEEN  T_Floor.FL_DateFrom AND T_Floor.FL_DateTo 
	
LEFT JOIN T_Ref_FloorType
	ON T_Ref_FloorType.FT_UID = T_Floor.FL_FT_UID 
	AND T_Ref_FloorType.FT_Status = 1 
	
LEFT JOIN T_ZO_Floor_DWG 
	ON T_ZO_Floor_DWG.ZO_FLDWG_FL_UID = T_Floor.FL_UID 
	AND T_ZO_Floor_DWG.ZO_FLDWG_Status = 1 
	AND {fn curdate()} BETWEEN  T_ZO_Floor_DWG.ZO_FLDWG_DateFrom AND T_ZO_Floor_DWG.ZO_FLDWG_DateTo 
	
LEFT JOIN T_ZO_Floor_Area
	ON T_ZO_Floor_Area.ZO_FLArea_FL_UID = T_Floor.FL_UID 
	AND T_ZO_Floor_Area.ZO_FLArea_Status = 1 
	AND {fn curdate()} BETWEEN  T_ZO_Floor_Area.ZO_FLArea_DateFrom AND T_ZO_Floor_Area.ZO_FLArea_DateTo 
	
WHERE (1=1) 
AND T_Ref_Location.LC_Status = 1 

--AND PR_Name LIKE '%Soodring 33%'
--AND FT_Lang_en = 'Upper floor'
--AND FL_Level = 3
AND 
(
	T_ZO_Floor_DWG.ZO_FLDWG_ApertureDWG = @in_aperturedwg 
	OR 
	T_ZO_Premises_DWG.ZO_PRDWG_ApertureDWG = @in_aperturedwg 
) 
";

            // SQL.fromFile("");
            DrawingInfo di = null;

            using (System.Data.Common.DbCommand cmd = SQL.CreateCommand(sql))
            {
                SQL.AddParameter(cmd, "in_aperturedwg", "ADSW_2");
                di = SQL.GetClass<DrawingInfo>(cmd);
            }

            return di;
        }


        private static void SetAttribute(System.Xml.XmlDocument svg, string attributeName, string value)
        {
            if (svg.DocumentElement.HasAttribute(attributeName))
            {
                // svg.DocumentElement.RemoveAttribute(attributeName);
                svg.DocumentElement.Attributes[attributeName].Value = value;
                return;
            }
            
            System.Xml.XmlAttribute att = svg.CreateAttribute(attributeName);
            att.Value = value;
            svg.DocumentElement.Attributes.Append(att);
        }

        public static System.Collections.Generic.Dictionary<string, string> StyleToDict(string style)
        {
            System.Collections.Generic.Dictionary<string, string> nvc = new System.Collections.Generic.Dictionary<string, string>();

            if (style == null)
                return nvc;

            string[] keyvaluepair = style.Split(';');

            for (int i = 0; i < keyvaluepair.Length; ++i)
            {
                keyvaluepair[i] = keyvaluepair[i].Trim();
                int pos = keyvaluepair[i].IndexOf(':');
                if (pos != -1)
                {
                    string key = keyvaluepair[i].Substring(0, pos).Trim();
                    string value = keyvaluepair[i].Substring(pos+1).Trim();
                    nvc[key] = value;
                }

            }
            
            return nvc;
        }

        public static string DictToStyle(System.Collections.Generic.Dictionary<string, string> styles)
        {
            string style = "";

            foreach (System.Collections.Generic.KeyValuePair<string,string> kvp in styles)
            {
                style += kvp.Key + ": " + kvp.Value + "; ";
            }

            return style;
        }

        public static void RemoveBgAndBorder(System.Xml.XmlNode node)
        {
            if (node.Attributes["style"] != null)
            {
                string style = node.Attributes["style"].Value;
                System.Collections.Generic.Dictionary<string, string> styles = StyleToDict(style);

                if (styles.ContainsKey("background-color"))
                {
                    styles.Remove("background-color");
                    styles.Remove("border");
                    style = DictToStyle(styles);
                    node.Attributes["style"].Value = style;
                }
            }

        }

        public static void Leg(string legendenText, string css)
        {
            string svgg = @"D:\Stefan.Steiger\Documents\Visual Studio 2017\Projects\libWkHtml2X\TestApp_Net20\Resources\1512467650594.svg";
            svgg = System.IO.File.ReadAllText(svgg, System.Text.Encoding.UTF8);

            string legendTemplate = @"D:\Stefan.Steiger\Documents\Visual Studio 2017\Projects\libWkHtml2X\TestApp_Net20\Resources\theLegend.htm";
            legendTemplate = System.IO.File.ReadAllText(legendTemplate, System.Text.Encoding.UTF8);

            System.Xml.XmlDocument svg = new System.Xml.XmlDocument();
            svg.XmlResolver = null;
            svg.LoadXml(svgg);

            SetAttribute(svg, "width", "100%");
            SetAttribute(svg, "height", "100%");
            //SetAttribute(svg, "preserveAspectRatio", "xMinYMin");
            SetAttribute(svg, "preserveAspectRatio", "xMidYMid");


            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.XmlResolver = null;
            doc.LoadXml(legendTemplate);

            System.Xml.XmlNamespaceManager nsm = 
                new System.Xml.XmlNamespaceManager(doc.NameTable);
            nsm.AddNamespace("dft", "http://www.w3.org/1999/xhtml");


            //System.Xml.XmlNode titleNode = doc.SelectSingleNode("//*[local-name()='div'][@id='title']");
            DrawingInfo di = GetData();
            System.Xml.XmlNode title = doc.SelectSingleNode("//*[@id='title']");
            title.FirstChild.InnerText = $"{di.LC_Lang_en} - {di.PR_Name} - {di.FloorDisplayString} - {di.Darstellung}";



            System.Xml.XmlNode logo = doc.SelectSingleNode("//*[@id='logo']");
            
            System.Xml.XmlNode printdate = doc.SelectSingleNode("//*[@id='printdate']");
            string pd = System.DateTime.Now.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            printdate.FirstChild.InnerText = pd;

            System.Xml.XmlNode legendStyle = doc.SelectSingleNode("//*[@id='legendStyle']");
            legendStyle.InnerXml = css;

            System.Xml.XmlNode Legend = doc.SelectSingleNode("//*[@id='Legend']");
            Legend.InnerXml = legendenText;


            System.Xml.XmlNode drawing = doc.SelectSingleNode("//*[@id='drawing']");
            drawing.InnerXml = svg.DocumentElement.OuterXml;


            System.Xml.XmlNode drawing_border = doc.SelectSingleNode("//*[@id='drawing_border']");

            RemoveBgAndBorder(title);
            RemoveBgAndBorder(logo);
            RemoveBgAndBorder(Legend);
            RemoveBgAndBorder(printdate);
            RemoveBgAndBorder(drawing);
            RemoveBgAndBorder(drawing_border);

            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            settings.Encoding = System.Text.Encoding.UTF8;
            settings.Indent = true;
            settings.IndentChars = "    ";
            settings.NewLineChars = System.Environment.NewLine;


            System.Text.StringBuilder builder = new System.Text.StringBuilder();

            using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(builder, settings))
            {
                doc.Save(writer);
            }

            string str = builder.ToString();
            System.Console.WriteLine(str);

            // System.Text.StringBuilder strBuilder = new System.Text.StringBuilder("file path characters are: ");
            // System.IO.StringWriter strWriter = new System.IO.StringWriter(strBuilder);
            // using( System.Xml.XmlTextWriter xtw2 = new System.Xml.XmlTextWriter(strWriter))

            using (System.Xml.XmlTextWriter xtw = new System.Xml.XmlTextWriter(@"d:\test5.htm", System.Text.Encoding.UTF8))
            {
                xtw.Formatting = System.Xml.Formatting.Indented; // if you want it indented
                xtw.Indentation = 4;
                xtw.IndentChar = ' ';

                doc.Save(xtw);
                xtw.Flush();
                xtw.Close();
            } // End Using xtw


        }


        static void testme()
        {
            string legendenText = @"
<h1>Space type</h1>
<table>
    <tr>
        <th data-alias=""_LEG_Color-Spacetype"" style=""""></th>
        <th data-alias=""Spacetype-Short name (Room)"" style=""""></th>
        <th data-alias=""Spacetype-Name (Room)"" style=""""></th>
        <th data-alias=""Area (Room)"" style=""text-align: right;"">Surface</th>
        <th data-alias=""Area2 (Room)"" style=""text-align: right;"">Surface2</th>
    </tr>
    <tr class=""Sum"">
        <td style=""""></td>
        <td style=""""></td>
        <td style=""""></td>
        <td style=""text-align: right;"">2288.24</td>
        <td style=""text-align: right;"">NaN</td>
    </tr>
    <tr>
        <td class=""asColor""><span data-primary=""9D176C6F-E7F8-40F4-B9B0-0C6B251ECF85"" style=""background-color: rgb(252, 210, 252);""></span></td>
        <td style="""">1.3</td>
        <td style="""">break room</td>
        <td style=""text-align: right;"">46.60</td>
        <td style=""text-align: right;""></td>
    </tr>
    <tr>
        <td class=""asColor""><span data-primary=""2D020F6A-7537-4FF6-A037-5B926BEAEA22,31B043F6-3616-4A98-A22B-0290E3F45099,375F4955-CC7C-49B4-9464-F4C283EC0E98,51A14A2A-860E-4C9D-BB3E-538CD3ACE728,85860EDF-A7CB-4DBD-ABF1-9F6ED7B4CF27,861A26F7-F2D9-4783-8F15-9EB41143D3AA,AB016A12-D7A2-447E-BBFF-5893BE24855F,BB25A02B-5AAB-4006-8668-4AF9C3590ABB,CA49DAFE-1568-4153-92F9-17CC18D3DA73,E42C8447-4465-4487-BB7C-2AD97158478A"" style=""background-color: rgb(252, 170, 172);""></span></td>
        <td style="""">2.1</td>
        <td style="""">private office space</td>
        <td style=""text-align: right;"">214.74</td>
        <td style=""text-align: right;""></td>
    </tr>
    <tr>
        <td class=""asColor""><span data-primary=""0D79F96B-E492-4298-B219-ACA534BE4674,101C6828-533E-4F80-958F-FBA52BB347AC,1ECCD315-60CE-494D-ADDB-847F532AF1F0,20A39D90-8EB8-4905-B505-94F497D5F799,3D0B70F5-1256-4331-99EC-B7744501E745,6BBEC095-979C-480C-9A27-3951F07D1F7E,82A13CF4-D5CC-421E-B580-9586191E8C2E,AE12598D-7404-4E11-9CDB-98B21DC01AC9,AFC57EE7-CB4D-4366-9FA8-31CBB0DB1C9D,C46E8C3B-9DFE-4082-A248-24D5CD1C010F,FCDD43D5-4C20-4083-911D-EEE5A4FBEB3B"" style=""background-color: rgb(180, 178, 252);""></span></td>
        <td style="""">2.2</td>
        <td style="""">open office space</td>
        <td style=""text-align: right;"">1005.15</td>
        <td style=""text-align: right;""></td>
    </tr>
    <tr>
        <td class=""asColor""><span data-primary=""4AA729F7-3B67-4CC3-9FB4-3749BBB7C4FA,4ECFFA6D-99C3-4A23-8B53-5046CFDA7AE4,69D64644-6D3E-44AE-B267-970820BF5BBA"" style=""background-color: rgb(180, 255, 185);""></span></td>
        <td style="""">2.2.2</td>
        <td style="""">project zone</td>
        <td style=""text-align: right;"">60.20</td>
        <td style=""text-align: right;""></td>
    </tr>
    <tr>
        <td class=""asColor""><span data-primary=""0495B5A3-CDCC-4063-A764-337B576E4475,29B69186-0DE5-463F-80C1-5835B214C67C,4889C0F7-CB80-454A-9F89-415DFB9C0A7F,528EAE51-F858-481C-98A9-7C2AE598E2D4,5BEA7EB8-F9FD-4578-B8BE-61150AF0DDC1,6BB82185-D563-4170-9349-72A7816F9727,6CED5A72-0C32-4485-A49A-A210FC984024,6D1B1409-E600-472E-ADEA-41DA6F483D17,81955BE2-27EE-4EEA-A6E9-0A8D35B2AAD3,8BAA1049-557C-4002-B8BE-7D79CA6805AA,A85B6EB7-7A16-43BF-AFAF-B4A7EF37D79F,F55FDA1A-E3FB-421B-921C-2252482350FA"" style=""background-color: rgb(255, 220, 164);""></span></td>
        <td style="""">2.2.3</td>
        <td style="""">think tank</td>
        <td style=""text-align: right;"">105.84</td>
        <td style=""text-align: right;""></td>
    </tr>
    <tr>
        <td class=""asColor""><span data-primary=""97691C6A-270C-4424-A022-04ABE0EB5189"" style=""background-color: rgb(252, 166, 252);""></span></td>
        <td style="""">2.3</td>
        <td style="""">meeting room</td>
        <td style=""text-align: right;"">42.05</td>
        <td style=""text-align: right;""></td>
    </tr>
    <tr>
        <td class=""asColor""><span data-primary=""FACF3AC8-6A1C-4992-AC60-3C88D1B7BA84"" style=""background-color: rgb(145, 50, 255);""></span></td>
        <td style="""">2.3.1</td>
        <td style="""">collaboration room</td>
        <td style=""text-align: right;"">46.60</td>
        <td style=""text-align: right;""></td>
    </tr>
    <tr>
        <td class=""asColor""><span data-primary=""6D3298FF-3F65-4419-89AA-22D6AB0521D3,C06F5363-72CD-4343-A35D-A6B06E4145C2"" style=""background-color: rgb(228, 210, 252);""></span></td>
        <td style="""">2.8</td>
        <td style="""">office technology room</td>
        <td style=""text-align: right;"">53.16</td>
        <td style=""text-align: right;""></td>
    </tr>
    <tr>
        <td class=""asColor""><span data-primary=""4BB3EDDF-D4A8-4937-9ED5-D1C2375ADE0B,6267A3FB-35F3-40A2-8573-C0C1CD42A08D,9AE859CC-BBDC-4DA6-B4FF-9E0036A28319,D79497E2-6AD5-4E12-AB43-7B04C3F84C17,F5E0460D-6451-4310-9B56-07110D8DDB48"" style=""background-color: rgb(84, 82, 244);""></span></td>
        <td style="""">4.2</td>
        <td style="""">archive room</td>
        <td style=""text-align: right;"">96.12</td>
        <td style=""text-align: right;""></td>
    </tr>
    <tr>
        <td class=""asColor""><span data-primary=""065445CB-055A-46B6-8063-843E3B19CA18,2FDD0DFD-0B27-4070-B585-9987F4AE47E2,5E5FA0E9-9D33-4AA5-A1BC-4FA136951D00,6717AE47-8524-452C-A373-0C67508A3491,DCC05344-9187-4580-B25B-20F6CA88DDA5,DE789824-41E3-44F0-8079-6C21B29BD892,FD74F68C-5FFD-4271-9DD1-DB48F28A3969"" style=""background-color: rgb(160, 255, 255);""></span></td>
        <td style="""">7.1</td>
        <td style="""">sanitation facilities</td>
        <td style=""text-align: right;"">61.53</td>
        <td style=""text-align: right;""></td>
    </tr>
    <tr>
        <td class=""asColor""><span data-primary=""00B21D11-1E28-4D44-937E-D6F2A663C8FD,CC4A60AF-C813-49C4-8263-0F238D880942"" style=""background-color: rgb(0, 223, 0);""></span></td>
        <td style="""">8.5.1</td>
        <td style="""">connectivity room</td>
        <td style=""text-align: right;"">36.48</td>
        <td style=""text-align: right;""></td>
    </tr>
    <tr>
        <td class=""asColor""><span data-primary=""08662404-082A-4BA5-9CC1-8A99D76849AC,205A6C32-8159-4F1F-A727-7D375D47F603,5EF20B59-0B0A-4F77-987A-4ED4E26876CB,7293CB74-57E9-4419-B731-49019D8F308C,EADDDCBE-3B67-45D1-AC68-CE3E7E45B442,FD5DB521-6D08-4A13-B169-2A62CFB4B892"" style=""background-color: rgb(220, 2, 220);""></span></td>
        <td style="""">8.9</td>
        <td style="""">miscellaneous operational equipment</td>
        <td style=""text-align: right;"">42.46</td>
        <td style=""text-align: right;""></td>
    </tr>
    <tr>
        <td class=""asColor""><span data-primary=""1220315D-CD42-48F6-9C84-1DA432E398D5,4C441CAA-6C62-4195-9D1E-B3906B2F1C79,ACBF7B42-90FC-4E43-B18B-3CB7DAABC10A,FDE975A1-3B8D-422A-AEA4-15E8CF800954"" style=""background-color: rgb(236, 234, 236);""></span></td>
        <td style="""">9.1</td>
        <td style="""">corridor, hall</td>
        <td style=""text-align: right;"">407.51</td>
        <td style=""text-align: right;""></td>
    </tr>
    <tr>
        <td class=""asColor""><span data-primary=""0A608566-E8BA-4368-9FB4-A83FBA18371B,10E76833-E860-422D-870E-5836B49898A4,7CAE0E4C-4B1F-4440-8C3A-46945DE9D009"" style=""background-color: rgb(185, 185, 185);""></span></td>
        <td style="""">9.2</td>
        <td style="""">stairs</td>
        <td style=""text-align: right;"">39.68</td>
        <td style=""text-align: right;""></td>
    </tr>
    <tr>
        <td class=""asColor""><span data-primary=""243636C3-9F44-4704-BC5B-E348A7DE9CCB,53EF1425-17D1-4546-8208-F08270B1B933,876B148F-7879-47B4-955A-1C1F8CE715E9,C7FA503C-F593-4CB8-BCF6-A433B7BA0156,C8277F07-FD9C-4DA0-B938-1369F9EAED78"" style=""background-color: rgb(110, 110, 110);""></span></td>
        <td style="""">9.3</td>
        <td style="""">elevator shaft</td>
        <td style=""text-align: right;"">30.12</td>
        <td style=""text-align: right;""></td>
    </tr>
</table>
";

            string legCSS = System.IO.File.ReadAllText(@"D:\Stefan.Steiger\Documents\Visual Studio 2017\Projects\libWkHtml2X\TestApp_Net20\Resources\legendCSS.css", System.Text.Encoding.UTF8);
            Leg(legendenText, legCSS);
        }


        public static void GeneratePNG()
        {
            string html = System.IO.File.ReadAllText(@"d:\debugSVG.htm", System.Text.Encoding.UTF8);

            int w = 204;
            int h = 265;

            byte[] pngBytes = null;

            libWkHtmlToX.WkHtmlToImageCommandLineOptions opts = new libWkHtmlToX.WkHtmlToImageCommandLineOptions();
            opts.ExecutableDirectory = libWkHtmlToX.VisualStudioHelper.GetDllDirectory();
            opts.ExecutableDirectory = @"D:\Stefan.Steiger\Documents\Visual Studio 2017\TFS\COR-Basic-V4\Portal\Portal_Convert\External\wkhtmltox\x86-32";


            opts.DisableSmartWidth = true;
            opts.ScreenWidth = System.Convert.ToInt32(System.Math.Ceiling((double)w));
            opts.ScreenHeight = System.Convert.ToInt32(System.Math.Ceiling((double)h));

            
            using (libWkHtmlToX.ProcessManager p = new libWkHtmlToX.ProcessManager(opts))
            {
                p.Start();
                p.WriteStandardInput(html);
                pngBytes = p.ReadOutputStream();

                System.Console.WriteLine("waiting");
                bool b = p.WaitForExit(5000);
                System.Console.WriteLine(b);
            } // End Using p 

            System.IO.File.WriteAllBytes(@"d:\zomg.png", pngBytes);
            System.Console.WriteLine("test");
        }


        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [System.STAThread]
        static void Main(string[] args)
        {
            GeneratePNG();
            testme();
            
            // libWkHtmlToX.TestProcessManager.TestPdf();
            libWkHtmlToX.TestProcessManager.TestPng();

#if false
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new Form1());
#endif 

            System.Threading.Thread th = libWkHtmlToX.Scheduler.Init(libWkHtmlToX.VisualStudioHelper.GetDllDirectory());


            string htmlData = @"<!doctype html>
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

<div style=""display: block; width: 2000p; height: 2000px; background-color: hotpink;""></div>

</body>
</html>
";



            string fn = "1503497977772.svg";
            fn = "1506332283409.svg";

            string fileName = libWkHtmlToX.VisualStudioHelper.MapSolutionPath("~TestFiles/" + fn);
            htmlData = System.IO.File.ReadAllText(fileName, System.Text.Encoding.UTF8);



            ////////////////////
            libWkHtmlToX.PdfGlobalSettings gs = new libWkHtmlToX.PdfGlobalSettings();


            gs.DocumentTitle = "Legende";
            // gs.PageSize = "width height";
            gs.Orientation = libWkHtmlToX.Orientation_t.Portrait;
            gs.OutputFormat = libWkHtmlToX.OutputFormat_t.pdf;

            gs.MarginBottom = "0px";
            gs.MarginTop = "0px";
            gs.MarginLeft = "0px";
            gs.MarginRight = "0px";

            gs.ImageQuality = 100;
            gs.UseCompression = false;

            gs.Outline = true;
            gs.Copies = 1;

            gs.ImageDPI = 600;
            gs.DPI = 96;

            gs.Width = "21cm";  // awidth.Value;
            gs.Height = "29.7cm"; //  aheight.Value;

            ////////////////////

            libWkHtmlToX.PdfObjectSettings os = new libWkHtmlToX.PdfObjectSettings();
            os.Web.DefaultEncoding = System.Text.Encoding.UTF8.WebName;
            os.Web.PrintBackground = true;
            os.Web.EnableIntelligentShrinking = false;
            // os.Web.PrintMediaType = true;
            // os.Load.ZoomFactor = 1.0;

            ////////////////////
            

            byte[] data = libWkHtmlToX.Scheduler.ConvertFile(
                delegate (ulong queueId)
                {
                    //return libWkHtmlToX.Converter.CreatePdf(htmlData, gs, os);
                    return libWkHtmlToX.Converter.CreatePdf(htmlData, null, null);
                }
            );

            if (data != null)
            {
                System.IO.File.WriteAllBytes(@"D:\Test\testfile.pdf", data);
                string pdfMarkup = System.Text.Encoding.UTF8.GetString(data);
                System.Console.WriteLine(pdfMarkup);
            } // End if (data != null) 

            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
            th.Abort();
        } // End Sub Main(string[] args)


        private static System.Globalization.NumberFormatInfo CreateWebNumberFormat()
        {
            return new System.Globalization.NumberFormatInfo
            {
                NumberGroupSeparator = "",
                NumberDecimalSeparator = ".",
                CurrencyGroupSeparator = "",
                CurrencyDecimalSeparator = ".",
                CurrencySymbol = ""
            };
        }



        public static System.IO.Stream FirstPageOnly(byte[] pdfBytes)
        {
            System.IO.MemoryStream msOutput = new System.IO.MemoryStream();

            // using(System.IO.MemoryStream msOutput = new System.IO.MemoryStream())
            //{

            using (System.IO.MemoryStream msInput = new System.IO.MemoryStream(pdfBytes))
            {

                using (PdfSharp.Pdf.PdfDocument pdfOutputDoc = new PdfSharp.Pdf.PdfDocument())
                {

                    using (PdfSharp.Pdf.PdfDocument pdfInputDocument = PdfSharp.Pdf.IO.PdfReader.Open(msInput, PdfSharp.Pdf.IO.PdfDocumentOpenMode.Import))
                    {

                        if (pdfInputDocument.Pages.Count > 0)
                        {
                            pdfOutputDoc.AddPage(pdfInputDocument.Pages[0]);
                        } // End if (pdfInputDocument.Pages.Count > 0) 

                    } // End Using pdfInputDocument

                    pdfOutputDoc.Save(msOutput);
                } // End Using pdfOutputDoc

            } // End Using msInput

            // } //End Using ' msOutput

            return msOutput;
        } // End Sub FirstPageOnly 


        // [OperationContract, WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]

        // [OperationContract]
        // [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        // [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public static System.IO.Stream toPDF2(string File, bool Current_view)
        {
            // Portal.Converter.Log tLog = new Portal.Converter.Log();

            System.IO.Stream retValue = null;


            //File = System.IO.File.ReadAllText(@"", System.Text.Encoding.UTF8);

            
            double paper_maxWidth = 21.0;
            double paper_maxHeight = 29.7;

            try
            {
                System.Globalization.NumberFormatInfo nfi = CreateWebNumberFormat();
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.XmlResolver = null;
                doc.PreserveWhitespace = true;
                doc.LoadXml(File);

                System.Xml.XmlAttribute awidth = doc.DocumentElement.Attributes["width"];
                System.Xml.XmlAttribute aheight = doc.DocumentElement.Attributes["height"];
                System.Xml.XmlAttribute aviewBox = doc.DocumentElement.Attributes["viewBox"];

                double width = -1.0;
                double.TryParse(awidth.Value, out width);

                double height = -1.0;
                double.TryParse(aheight.Value, out height);

                string[] sv = aviewBox.Value.Split(new char[] { ' ', ','}, System.StringSplitOptions.RemoveEmptyEntries);

                double viewbox_width;
                double viewbox_height;
                double factor;

                checked
                {
                    double[] dv = new double[sv.Length - 1 + 1];
                    int[] v = new int[sv.Length - 1 + 1];
                    int num = sv.Length - 1;
                    for (int i = 0; i <= num; i++)
                    {
                        double.TryParse(sv[i], out dv[i]);
                    } // Next i 

                    double viewbox_x = dv[0];
                    double viewbox_y = dv[1];
                    viewbox_width = dv[2];
                    viewbox_height = dv[3];

                    double r = width / viewbox_width;
                    double r2 = height / viewbox_height;
                    factor = System.Math.Min(r, r2);
                }

                double newWidth = factor * viewbox_width;
                double newHeight = factor * viewbox_height;

                double fWidth = paper_maxWidth / newWidth;
                double fHeight = paper_maxHeight / newHeight;
                double fPaperSizeFactor = System.Math.Min(fWidth, fHeight);

                double newPaperWidth = newWidth * fPaperSizeFactor;
                double newPaperHeight = newHeight * fPaperSizeFactor;

                awidth.Value = newPaperWidth.ToString("N2", nfi) + "cm";
                aheight.Value = newPaperHeight.ToString("N2", nfi) + "cm";

                string svg = doc.OuterXml;
                string svgHtmlWrapper = libWkHtmlToX.ResourceLoader.ReadEmbeddedResource(typeof(Program).Assembly, ".SvgHtmlWrapper.htm");


                string html = string.Format(svgHtmlWrapper, svg);
                // System.IO.File.WriteAllText(@"D:\Test\svgHtmlWrapper.htm", html);


                libWkHtmlToX.PdfGlobalSettings pdfGlobalSettings = new libWkHtmlToX.PdfGlobalSettings();
                pdfGlobalSettings.DocumentTitle = "Legende";
                pdfGlobalSettings.Orientation = libWkHtmlToX.Orientation_t.Portrait;
                pdfGlobalSettings.OutputFormat = libWkHtmlToX.OutputFormat_t.pdf;

                pdfGlobalSettings.MarginBottom = "0px";
                pdfGlobalSettings.MarginTop = "0px";
                pdfGlobalSettings.MarginLeft = "0px";
                pdfGlobalSettings.MarginRight = "0px";

                pdfGlobalSettings.ImageQuality = 50;
                pdfGlobalSettings.UseCompression = false;

                pdfGlobalSettings.Outline = true;
                pdfGlobalSettings.Copies = 1;

                pdfGlobalSettings.ImageDPI = 600;
                // pdfGlobalSettings.DPI = 96;
                pdfGlobalSettings.DPI = 300;

                pdfGlobalSettings.Width = awidth.Value;
                pdfGlobalSettings.Height = aheight.Value;


                libWkHtmlToX.PdfObjectSettings pdfObjectSettings = new libWkHtmlToX.PdfObjectSettings();
                pdfObjectSettings.Web.DefaultEncoding = System.Text.Encoding.UTF8.WebName;
                pdfObjectSettings.Web.EnableIntelligentShrinking = false;
                // pdfObjectSettings.Web.PrintBackground = True
                // pdfObjectSettings.Web.PrintMediaType = true;
                // pdfObjectSettings.Load.ZoomFactor = 1.0;

                byte[] pdfBytes = libWkHtmlToX.Scheduler.ConvertFile(
                    delegate (ulong queueId)
                    {
                        return libWkHtmlToX.Converter.CreatePdf(html, pdfGlobalSettings, pdfObjectSettings);
                    }
                );

                retValue = FirstPageOnly(pdfBytes);
            }
            catch (System.Exception ex)
            {
                // tLog.addMessage(ex)
                retValue = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes("@@Error: " + ex.Message));
            }
            finally
            {
                // tLog.Write();
            }

            return retValue;
        } // End Sub toPDF2 


    } // End Class Program 


} // End Namespace TestApp_Net20 
