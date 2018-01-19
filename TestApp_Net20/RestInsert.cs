
namespace TestApp_Net20.Rest
{


    public class Insert
    {

        private static System.Reflection.MethodInfo s_ToObjectMethodInfo;

        private static System.Reflection.MethodInfo GetToObjectMethod()
        {
            System.Reflection.MethodInfo[] mis = typeof(Newtonsoft.Json.Linq.JToken).GetMethods();

            foreach (System.Reflection.MethodInfo m in mis)
            {
                if (!m.IsGenericMethodDefinition)
                    continue;

                if (!"ToObject".Equals(m.Name))
                    continue;

                System.Reflection.ParameterInfo[] pis = m.GetParameters();
                if (pis.Length != 0)
                    continue;

                return m;
            } // Next m 

            return null;
        } // End Function GetToObjectMethod 


        static Insert()
        {
            s_ToObjectMethodInfo = GetToObjectMethod();
        } // End Sub Insert 


        public static void Test()
        {
            string json = @"
{
    
""PL_XY"": 0.0,
""foo"": null,
""PL_PLK_UID"": ""00000000-0000-0000-0000-000000000000"",

    ""PL_Type"": """",
    ""PL_Format"": """",
    ""PL_X"": 0,
    ""PL_Y"": 0,
    ""PL_W"": 100,
    ""PL_H"": 100,
    ""PL_Angle"": 0,
    ""PL_AlignH"": """",
    ""PL_AlignV"": """",
    ""PL_Text_DE"": """",
    ""PL_Text_FR"": """",
    ""PL_Text_IT"": """",
    ""PL_Text_EN"": """",
    ""PL_Outline"": 0,
    ""PL_Style"": """",
    ""PL_DataBind"": """",
    ""PL_Sort"": 0
}
";


            string json1 = @"
[{ ""PL_PLK_UID"":""00000000-0000-0000-0000-000000000000"",""PL_Type"":"""",""PL_Format"":"""",""PL_X"":0,""PL_Y"":0,""PL_W"":100,""PL_H"":100,""PL_Angle"":0,""PL_AlignH"":"""",""PL_AlignV"":"""",""PL_Text_DE"":"""",""PL_Text_FR"":"""",""PL_Text_IT"":"""",""PL_Text_EN"":"""",""PL_Outline"":0,""PL_Style"":"""",""PL_DataBind"":"""",""PL_Sort"":0},{ ""PL_PLK_UID"":""00000000-0000-0000-0000-000000000000"",""PL_Type"":"""",""PL_Format"":"""",""PL_X"":0,""PL_Y"":0,""PL_W"":100,""PL_H"":100,""PL_Angle"":0,""PL_AlignH"":"""",""PL_AlignV"":"""",""PL_Text_DE"":"""",""PL_Text_FR"":"""",""PL_Text_IT"":"""",""PL_Text_EN"":"""",""PL_Outline"":0,""PL_Style"":"""",""PL_DataBind"":"""",""PL_Sort"":0}]
";

            //json = json1;
            // json = null;

            System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>> lss = Json2List(json);
            System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>> lss1 = Json2List(json1);
            System.Console.WriteLine(lss);
            System.Console.WriteLine(lss1);
        } // End Sub Test 


        private static System.Type MapJTokenTypeToDotNet(Newtonsoft.Json.Linq.JTokenType t)
        {
            //System.Collections.Generic.Dictionary<Newtonsoft.Json.Linq.JTokenType, System.Type> du = new System.Collections.Generic.Dictionary<Newtonsoft.Json.Linq.JTokenType, System.Type>();

            switch (t)
            {
                case Newtonsoft.Json.Linq.JTokenType.None: // None = 0
                case Newtonsoft.Json.Linq.JTokenType.Object: // Object = 1
                    return typeof(object);
                case Newtonsoft.Json.Linq.JTokenType.Array: // Array = 2
                    // return typeof(System.Array);
                    throw new System.Exception("Value type Array not mappable");
                case Newtonsoft.Json.Linq.JTokenType.Integer: // Integer = 6
                    return typeof(System.Int32);
                case Newtonsoft.Json.Linq.JTokenType.Float: // Float = 7
                    return typeof(System.Decimal);
                case Newtonsoft.Json.Linq.JTokenType.String: // String = 8
                    return typeof(System.String);
                case Newtonsoft.Json.Linq.JTokenType.Boolean: // Boolean = 9
                    return typeof(System.Boolean);
                case Newtonsoft.Json.Linq.JTokenType.Null: // Null = 10
                    return typeof(System.Object);
                case Newtonsoft.Json.Linq.JTokenType.Undefined: // Undefined = 11
                    // return typeof(System.Object);
                    throw new System.Exception("Value type Undefined not mappable.");
                case Newtonsoft.Json.Linq.JTokenType.Date: // Date = 12
                    return typeof(System.DateTime);
                case Newtonsoft.Json.Linq.JTokenType.Raw: // Date = 13
                    // return typeof(System.Object);
                    throw new System.Exception("Value type Raw not mappable.");
                case Newtonsoft.Json.Linq.JTokenType.Bytes: // Null = 14
                    return typeof(System.Byte[]);
                case Newtonsoft.Json.Linq.JTokenType.Guid: // Null = 15
                    return typeof(System.Guid);
                case Newtonsoft.Json.Linq.JTokenType.Uri: // Uri = 16
                    return typeof(System.String);
                case Newtonsoft.Json.Linq.JTokenType.TimeSpan: // Uri = 17
                    return typeof(System.TimeSpan);
                default:
                    throw new System.NotImplementedException($"JObject type mapping for type \"{t}\" not implemented.");
            } // End Switch t 

            // Array = 2,
            // Constructor = 3,
            // Property = 4,
            // Comment = 5,
            // return null;
        } // End Function MapJTokenTypeToDotNet 



        private static object GetValue(Newtonsoft.Json.Linq.JToken value, System.Type t)
        {
            // string foo1 = value.ToString();
            // string foo = value.ToObject<string>();
            // int bar = value.ToObject<int>();

            // System.Reflection.MethodInfo method = GetToObjectMethod();
            System.Reflection.MethodInfo generic = s_ToObjectMethodInfo.MakeGenericMethod(t);
            return generic.Invoke(value, null);
        } // End Function GetValue 


        private delegate object GetValue_t(Newtonsoft.Json.Linq.JToken val);

        private static GetValue_t GetValueDelegate(System.Type t)
        {
            // string foo1 = value.ToString();
            // string foo = value.ToObject<string>();
            // int bar = value.ToObject<int>();

            // System.Reflection.MethodInfo method = GetToObjectMethod();
            System.Reflection.MethodInfo generic = s_ToObjectMethodInfo.MakeGenericMethod(t);

            return delegate (Newtonsoft.Json.Linq.JToken val)
            {
                return generic.Invoke(val, null);
            };

        } // End Function GetValueDelegate 



        private static object GetValue(Newtonsoft.Json.Linq.JToken value)
        {
            System.Type t = MapJTokenTypeToDotNet(value.Type);

            // GetValue_t getValue = GetValueDelegate(t);
            // return getValue(value);

            return GetValue(value, t);
        } // End Function GetValue 


        //public class Parameter
        //{
        //    public string Name;
        //    public object Value;
        //    public Parameter()
        //    { }
        //    public Parameter(string name, object value)
        //    {
        //        this.Name = name;
        //        this.Value = value;
        //    }
        //}


        private static System.Collections.Generic.Dictionary<string, object>
            ProcessObject(Newtonsoft.Json.Linq.JToken json)
        {
            if (json == null)
            {
                throw new System.ArgumentNullException(nameof(json));
            }

            // System.Collections.Generic.List<Parameter> ls = new System.Collections.Generic.List<Parameter>();
            System.Collections.Generic.Dictionary<string, object> lss =
                new System.Collections.Generic.Dictionary<string, object>(System.StringComparer.OrdinalIgnoreCase);

            Newtonsoft.Json.Linq.JObject jo = (Newtonsoft.Json.Linq.JObject)json;

            foreach (System.Collections.Generic.KeyValuePair<string, Newtonsoft.Json.Linq.JToken> kvp in jo)
            {
                string name = kvp.Key;
                object value = GetValue(kvp.Value);
                System.Console.WriteLine(value);
                // ls.Add(new Parameter(name, value));
                lss.Add(name, value);
            } // Next kvp 

            return lss;
        } // End Function ProcessObject 


        private static System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>>
            ProcessArray(Newtonsoft.Json.Linq.JToken json)
        {
            if (json == null)
            {
                throw new System.ArgumentNullException(nameof(json));
            } // End if (json == null) 

            // System.Collections.Generic.List<System.Collections.Generic.List<Parameter>> ls = new System.Collections.Generic.List<System.Collections.Generic.List<Parameter>>();
            System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>> lss =
                new System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>>();

            Newtonsoft.Json.Linq.JArray jsonArray = (Newtonsoft.Json.Linq.JArray)json;

            for (int i = 0; i < jsonArray.Count; ++i)
            {
                Newtonsoft.Json.Linq.JToken jtoken = jsonArray[i];
                if (jtoken.Type != Newtonsoft.Json.Linq.JTokenType.Object)
                    throw new System.InvalidCastException("This is not a JSON-object");

                //ls.Add(ProcessObject(jtoken));
                lss.Add(ProcessObject(jtoken));
            } // Next i 

            return lss;
        } // End Function ProcessArray 


        public static System.Collections.Generic.List<
            System.Collections.Generic.Dictionary<string, object>
        > Json2List(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new System.ArgumentNullException(nameof(json));
            } // End if (string.IsNullOrEmpty(json)) 

            System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>> lss;
            Newtonsoft.Json.Linq.JToken jsonData = Newtonsoft.Json.Linq.JToken.Parse(json);

            if (jsonData.Type == Newtonsoft.Json.Linq.JTokenType.Object)
            {
                lss = new System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>>();
                lss.Add(ProcessObject(jsonData));
            }
            else if (jsonData.Type == Newtonsoft.Json.Linq.JTokenType.Array)
            {
                lss = ProcessArray(jsonData);
            }
            else
            {
                throw new System.InvalidOperationException(
                    "Cannot perform this operation on anything other than JSON-object, or JSON-array of JSON-object.");
            }

            return lss;
        } // End Function Json2List 


    } // End Class Insert 


} // End Namespace CoreCMS.Rest
