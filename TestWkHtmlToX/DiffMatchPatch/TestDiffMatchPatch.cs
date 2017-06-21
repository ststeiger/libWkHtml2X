using System;
using System.Collections.Generic;
using System.Text;

namespace TestWkHtmlToX
{
    class TestDiffMatchPatch
    {

        public static void Test()
        {
            string text1 = @"123
            test
456
789
";

 string text2 = @"123
            test
457
789";

            var dmp = new DiffMatchPatch.diff_match_patch();

            var diffs = dmp.diff_main(text1, text2);
            System.Console.WriteLine(diffs);

            var patches = dmp.patch_make(text1, text2);
            System.Console.WriteLine(patches);



            var html = dmp.diff_prettyHtml(diffs);
            

            string pageTemplate = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""utf-8"">
    <title></title>

    <style type=""text/css"">
        
        *
        {{
            margin: 0px; padding: 0px;
        }}
        
        span, del, ins
        {{
            white-space: PRE;
            text-decoration: none;
        }}
        
        body
        {{
            padding: 0.25cm;
        }}
        
        ins
        {{
            background-color: #e6ffe6;
        }}
        
        del
        {{
            background-color: #ffe6e6;
        }}
        
    </style>

</head>
<body>
    {0}
</body>
</html>
";
            
            html = string.Format(pageTemplate, html);
            System.Console.WriteLine(html);

        }

    }
}
