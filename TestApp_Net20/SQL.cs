
namespace TestApp_Net20
{


    public class SQL
    {

        private static System.Data.Common.DbProviderFactory fact = System.Data.Common.DbProviderFactories.GetFactory(typeof(System.Data.SqlClient.SqlClientFactory).Namespace);


        public static string GetConnectionString()
        {
            System.Data.SqlClient.SqlConnectionStringBuilder csb = new System.Data.SqlClient.SqlConnectionStringBuilder();

            csb.DataSource = System.Environment.MachineName;
            if(System.StringComparer.OrdinalIgnoreCase.Equals("COR", System.Environment.UserDomainName))
            {
                csb.DataSource += @"\SqlExpress";
            }

            csb.InitialCatalog = "COR_Basic_Demo_V4";
            csb.InitialCatalog = "SwissRe_Test_V4";
            
            csb.IntegratedSecurity = System.StringComparer.OrdinalIgnoreCase.Equals(System.Environment.UserDomainName, "COR");
            if (!csb.IntegratedSecurity)
            {
                csb.UserID = SecretManager.GetSecret<string>("DefaultDbUser");
                csb.Password = SecretManager.GetSecret<string>("DefaultDbPassword");
            }
            
            return csb.ToString();
        }
        
        
        public static System.Data.Common.DbConnection GetConnection()
        {
            System.Data.Common.DbConnection con = fact.CreateConnection();
            con.ConnectionString = GetConnectionString();
            return con;
        }
        
        
        public static System.Data.Common.DbCommand CreateCommand(string sql)
        {
            System.Data.Common.DbCommand cmd = fact.CreateCommand();
            cmd.CommandText = sql;

            return cmd;
        }


        public static System.Data.Common.DbCommand fromFile(string resourceName)
        {
            System.Data.Common.DbCommand cmd = fact.CreateCommand();
            cmd.CommandText = ResourceLoader.ReadEmbeddedResource(typeof(SQL), resourceName); ;

            return cmd;
        }
        

        public static System.Data.Common.DbDataReader GetDataReader(System.Data.Common.DbCommand cmd)
        {
            System.Data.Common.DbDataReader dr = null;

            System.Data.Common.DbConnection con = GetConnection();
            cmd.Connection = con;

            if (con.State != System.Data.ConnectionState.Open)
                con.Open();

            dr = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess | System.Data.CommandBehavior.CloseConnection);

            return dr;
        }

        public delegate void DataReaderCallback_t(System.Data.Common.DbDataReader reader);

        public static void ExecuteReader(System.Data.IDbCommand cmd, DataReaderCallback_t callback)
        {
            using (System.Data.Common.DbConnection con = GetConnection())
            {
                cmd.Connection = con;

                if (con.State != System.Data.ConnectionState.Open)
                    con.Open();

                using (System.Data.Common.DbDataReader idr = (System.Data.Common.DbDataReader) cmd.ExecuteReader(
                        System.Data.CommandBehavior.SequentialAccess | System.Data.CommandBehavior.CloseConnection
                ))
                {
                    callback(idr);
                }

                if (con.State != System.Data.ConnectionState.Closed)
                    con.Close();
            } // End Using con 
        }


        public static void ExecuteReader(string sql, DataReaderCallback_t callback)
        {
            using (System.Data.Common.DbCommand cmd = CreateCommand(sql))
            {
                ExecuteReader(cmd, callback);
            } // End Using cmd 
        }
        

        public static System.Data.DataTable GetDataTable(System.Data.Common.DbCommand cmd)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            using (System.Data.Common.DbConnection con = GetConnection())
            {
                cmd.Connection = con;

                using (System.Data.Common.DbDataAdapter da = fact.CreateDataAdapter())
                {
                    da.SelectCommand = cmd;

                    da.Fill(dt);
                }
            }

            return dt;
        }
        

        public static bool IsNullable(System.Type t)
        {
            if (t == null)
                return false;

            return t.IsGenericType && object.ReferenceEquals(t.GetGenericTypeDefinition(), typeof(System.Nullable<>));
        } // End Function IsNullable


        public static object MyChangeType(object objVal, System.Type t)
        {
            bool typeIsNullable = IsNullable(t);
            bool typeCanBeAssignedNull = !t.IsValueType || typeIsNullable;

            if (objVal == null || object.ReferenceEquals(objVal, System.DBNull.Value))
            {
                if (typeCanBeAssignedNull)
                    return null;
                else
                    throw new System.ArgumentNullException("objVal ([DataSource] => SetProperty => MyChangeType => you're trying to NULL a type that NULL cannot be assigned to...)");
            }

            //getbasetype
            System.Type tThisType = objVal.GetType();

            if (typeIsNullable)
            {
                t = System.Nullable.GetUnderlyingType(t);
            }


            if (object.ReferenceEquals(tThisType, t))
                return objVal;

            // Convert Guid => string 
            if (object.ReferenceEquals(t, typeof(string)) && object.ReferenceEquals(tThisType, typeof(System.Guid)))
            {
                return objVal.ToString();
            }

            // Convert string => Guid 
            if (object.ReferenceEquals(t, typeof(System.Guid)) && object.ReferenceEquals(tThisType, typeof(string)))
            {
                return new System.Guid(objVal.ToString());
            }

            return System.Convert.ChangeType(objVal, t);
        } // End Function MyChangeType

        private const System.Reflection.BindingFlags m_CaseSensitivity = System.Reflection.BindingFlags.Instance
         | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.IgnoreCase
         ;

        public static System.Reflection.MemberInfo GetMemberInfo(System.Type t, string strName)
        {
            System.Reflection.MemberInfo mi = t.GetField(strName, m_CaseSensitivity);

            if (mi == null)
                mi = t.GetProperty(strName, m_CaseSensitivity);

            return mi;
        } // End Function GetMemberInfo


        public static void SetMemberValue(object obj, System.Reflection.MemberInfo mi, object objValue)
        {
            if (mi is System.Reflection.FieldInfo)
            {
                System.Reflection.FieldInfo fi = (System.Reflection.FieldInfo)mi;
                fi.SetValue(obj, MyChangeType(objValue, fi.FieldType));
                return;
            }

            if (mi is System.Reflection.PropertyInfo)
            {
                System.Reflection.PropertyInfo pi = (System.Reflection.PropertyInfo)mi;
                pi.SetValue(obj, MyChangeType(objValue, pi.PropertyType), null);
                return;
            }

            // Else silently ignore value
        } // End Sub SetMemberValue
        

        public static T GetClass<T>(System.Data.IDbCommand cmd)
        {
            T tThisClassInstance = System.Activator.CreateInstance<T>();
            return GetClass<T>(cmd, tThisClassInstance);
        }


        public static T GetClass<T>(System.Data.IDbCommand cmd, T tThisClassInstance)
        {
            System.Type t = typeof(T);

            lock (cmd)
            {

                ExecuteReader(cmd, delegate (System.Data.Common.DbDataReader idr) 
                {
                    while (idr.Read())
                    {

                        for (int i = 0; i < idr.FieldCount; ++i)
                        {
                            string strName = idr.GetName(i);
                            object objVal = idr.GetValue(i);

                            System.Reflection.MemberInfo mi = GetMemberInfo(t, strName);
                            SetMemberValue(tThisClassInstance, mi, objVal);
                        } // Next i

                        break;
                    } // Whend
                });

            } // End lock cmd

            return tThisClassInstance;
        } // End Function GetClass


        // Anything else than a struct or a class
        private static bool IsSimpleType(System.Type tThisType)
        {

            if (tThisType.IsPrimitive)
            {
                return true;
            }

            if (object.ReferenceEquals(tThisType, typeof(System.String)))
            {
                return true;
            }

            if (object.ReferenceEquals(tThisType, typeof(System.DateTime)))
            {
                return true;
            }

            if (object.ReferenceEquals(tThisType, typeof(System.Guid)))
            {
                return true;
            }

            if (object.ReferenceEquals(tThisType, typeof(System.Decimal)))
            {
                return true;
            }

            if (object.ReferenceEquals(tThisType, typeof(System.Object)))
            {
                return true;
            }

            return false;
        } // End Function IsSimpleType


        public static System.Collections.Generic.List<T> GetList<T>(System.Data.IDbCommand cmd)
        {
            System.Collections.Generic.List<T> lsReturnValue = new System.Collections.Generic.List<T>();
            T tThisValue = default(T);
            System.Type tThisType = typeof(T);


            ExecuteReader(cmd, delegate (System.Data.Common.DbDataReader idr) {
                if (IsSimpleType(tThisType))
                {
                    while (idr.Read())
                    {
                        object objVal = idr.GetValue(0);
                        tThisValue = (T)MyChangeType(objVal, typeof(T));
                        //tThisValue = System.Convert.ChangeType(objVal, T),

                        lsReturnValue.Add(tThisValue);
                    } // End while (idr.Read())

                }
                else
                {
                    int myi = idr.FieldCount;

                    System.Reflection.FieldInfo[] fis = new System.Reflection.FieldInfo[idr.FieldCount];
                    //Action<T, object>[] setters = new Action<T, object>[idr.FieldCount];

                    for (int i = 0; i < idr.FieldCount; ++i)
                    {
                        string strName = idr.GetName(i);
                        System.Reflection.FieldInfo fi = tThisType.GetField(strName, m_CaseSensitivity);
                        fis[i] = fi;

                        //if (fi != null)
                        //    setters[i] = GetSetter<T>(fi);
                    } // Next i


                    while (idr.Read())
                    {
                        //idr.GetOrdinal("")
                        tThisValue = System.Activator.CreateInstance<T>();

                        // Console.WriteLine(idr.FieldCount);
                        for (int i = 0; i < idr.FieldCount; ++i)
                        {
                            string strName = idr.GetName(i);
                            object objVal = idr.GetValue(i);

                            //System.Reflection.FieldInfo fi = t.GetField(strName, m_CaseSensitivity);
                            if (fis[i] != null)
                            //if (fi != null)
                            {
                                //fi.SetValue(tThisValue, System.Convert.ChangeType(objVal, fi.FieldType));
                                fis[i].SetValue(tThisValue, MyChangeType(objVal, fis[i].FieldType));
                            } // End if (fi != null) 
                            else
                            {
                                System.Reflection.PropertyInfo pi = tThisType.GetProperty(strName, m_CaseSensitivity);
                                if (pi != null)
                                {
                                    //pi.SetValue(tThisValue, System.Convert.ChangeType(objVal, pi.PropertyType), null);
                                    pi.SetValue(tThisValue, MyChangeType(objVal, pi.PropertyType), null);
                                } // End if (pi != null)

                                // Else silently ignore value
                            } // End else of if (fi != null)

                            //Console.WriteLine(strName);
                        } // Next i

                        lsReturnValue.Add(tThisValue);
                    } // Whend

                } // End if IsSimpleType(tThisType)
            });
            
            return lsReturnValue;
        } // End Function GetList


        /*
        public static void Serialize(object obj, System.Web.HttpContext context)
        {
#if DEBUG 
            Serialize(obj, context, true);
#else
            Serialize(obj, context, false);
#endif
        }

        
        public static void Serialize(object obj, System.Web.HttpContext context, bool pretty)
        {
            Newtonsoft.Json.JsonSerializer ser = new Newtonsoft.Json.JsonSerializer();

            using (Newtonsoft.Json.JsonTextWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(context.Response.Output))
            {
                ser.Serialize(jsonWriter, obj);
            }
        }
        

        public static void WriteAssociativeArray(Newtonsoft.Json.JsonTextWriter jsonWriter, System.Data.Common.DbDataReader dr)
        {
            WriteAssociativeArray(jsonWriter, dr, false);
        }


        public static void WriteAssociativeArray(Newtonsoft.Json.JsonTextWriter jsonWriter, System.Data.Common.DbDataReader dr, bool dataType)
        {
            // JSON: 
            //{
            //     "column_1":{ "index":0,"fieldType":"int"}
            //    ,"column_2":{ "index":1,"fieldType":"int"}
            //}

            jsonWriter.WriteStartObject();

            for (int i = 0; i < dr.FieldCount; ++i)
            {
                jsonWriter.WritePropertyName(dr.GetName(i));
                jsonWriter.WriteStartObject();

                jsonWriter.WritePropertyName("index");
                jsonWriter.WriteValue(i);

#if false
                jsonWriter.WritePropertyName("columnName");
                jsonWriter.WriteValue(dr.GetName(i));
#endif

                if (dataType)
                {
                    jsonWriter.WritePropertyName("fieldType");
                    jsonWriter.WriteValue(GetAssemblyQualifiedNoVersionName(dr.GetFieldType(i)));
                }
                
                jsonWriter.WriteEndObject();
            }

            jsonWriter.WriteEndObject();
        }


        public static void WriteArray(Newtonsoft.Json.JsonTextWriter jsonWriter, System.Data.Common.DbDataReader dr)
        {
            jsonWriter.WriteStartArray();

            for (int i = 0; i < dr.FieldCount; ++i)
            {
                jsonWriter.WriteStartObject();

                jsonWriter.WritePropertyName("index");
                jsonWriter.WriteValue(i);

                jsonWriter.WritePropertyName("columnName");
                jsonWriter.WriteValue(dr.GetName(i));

                jsonWriter.WritePropertyName("fieldType");
                jsonWriter.WriteValue(GetAssemblyQualifiedNoVersionName(dr.GetFieldType(i)));

                jsonWriter.WriteEndObject();
            } // Next i 

            jsonWriter.WriteEndArray();
        }


        public static void SerializeLargeDataset(System.Data.Common.DbCommand cmd, System.Web.HttpContext context, bool pretty)
        {
            Newtonsoft.Json.JsonSerializer ser = new Newtonsoft.Json.JsonSerializer();

            using (Newtonsoft.Json.JsonTextWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(context.Response.Output))
            {
                if (pretty)
                    jsonWriter.Formatting = Newtonsoft.Json.Formatting.Indented;

                jsonWriter.WriteStartObject();

                jsonWriter.WritePropertyName("tables");
                jsonWriter.WriteStartArray();


                using (System.Data.Common.DbConnection con = GetConnection())
                {
                    cmd.Connection = con;

                    if (con.State != System.Data.ConnectionState.Open)
                        con.Open();

                    using (System.Data.Common.DbDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess
                         | System.Data.CommandBehavior.CloseConnection
                        ))
                    {

                        do
                        {
                            jsonWriter.WriteStartObject(); // tbl = new Table();

                            jsonWriter.WritePropertyName("columns");

                            // WriteArray(jsonWriter, dr);
                            WriteAssociativeArray(jsonWriter, dr);
                            


                            jsonWriter.WritePropertyName("rows");
                            jsonWriter.WriteStartArray();

                            if (dr.HasRows)
                            {

                                while (dr.Read())
                                {
                                    object[] thisRow = new object[dr.FieldCount];

                                    jsonWriter.WriteStartArray(); // object[] thisRow = new object[dr.FieldCount];
                                    for (int i = 0; i < dr.FieldCount; ++i)
                                    {
                                        jsonWriter.WriteValue(dr.GetValue(i));
                                    } // Next i
                                    jsonWriter.WriteEndArray(); // tbl.Rows.Add(thisRow);
                                } // Whend 

                            } // End if (dr.HasRows) 

                            jsonWriter.WriteEndArray();

                            jsonWriter.WriteEndObject(); // ser.Tables.Add(tbl);
                        } while (dr.NextResult());

                    } // End using dr 

                    if (con.State != System.Data.ConnectionState.Closed)
                        con.Close();
                } // End using con 

                jsonWriter.WriteEndArray();

                jsonWriter.WriteEndObject();
                jsonWriter.Flush();
            } // End Using jsonWriter 

            context.Response.Output.Flush();
            context.Response.OutputStream.Flush();
            context.Response.Flush();
        } // End Sub SerializeLargeDataset 


        public static void SerializeLargeDataset(System.Data.Common.DbCommand cmd, System.Web.HttpContext context)
        {
#if DEBUG 
            SerializeLargeDataset(cmd, context, true);
#else
            SerializeLargeDataset(cmd, context, false);
#endif
        } // End Sub SerializeLargeDataset 


        public static string GetAssemblyQualifiedNoVersionName(System.Type type)
        {
            if (type == null)
                return null;

            return GetAssemblyQualifiedNoVersionName(type.AssemblyQualifiedName);
        } // End Function GetAssemblyQualifiedNoVersionName


        public static string GetAssemblyQualifiedNoVersionName(string input)
        {
            int i = 0;
            bool isNotFirst = false;
            for (; i < input.Length; ++i)
            {
                if (input[i] == ',')
                {
                    if (isNotFirst)
                        break;

                    isNotFirst = true;
                }
            }

            return input.Substring(0, i);
        } // End Function GetAssemblyQualifiedNoVersionName

    */





        public static string JoinArray<T>(string separator, T[] inputTypeArray)
        {
            return JoinArray<T>(separator, inputTypeArray, object.ReferenceEquals(typeof(T), typeof(string)));
        }


        public static string JoinArray<T>(string separator, T[] inputTypeArray, bool sqlEscapeString)
        {
            string strRetValue = null;
            System.Collections.Generic.List<string> ls = new System.Collections.Generic.List<string>();

            for (int i = 0; i < inputTypeArray.Length; ++i)
            {
                string str = System.Convert.ToString(inputTypeArray[i], System.Globalization.CultureInfo.InvariantCulture);

                if (!string.IsNullOrEmpty(str))
                {
                    // SQL-Escape
                    if (sqlEscapeString)
                        str = str.Replace("'", "''");

                    ls.Add(str);
                } // End if (!string.IsNullOrEmpty(str))

            } // Next i 

            strRetValue = string.Join(separator, ls.ToArray());
            ls.Clear();
            ls = null;

            return strRetValue;
        } // End Function JoinArray


        
        public static void AddArrayParameter<T>(System.Data.IDbCommand command, string strParameterName, params T[] values)
        {
            if (values == null)
                return;

            if (!strParameterName.StartsWith("@"))
                strParameterName = "@" + strParameterName;

            string strSqlInStatement = JoinArray<T>(",", values);

            command.CommandText = command.CommandText.Replace(strParameterName, strSqlInStatement);
        } // End Function AddArrayParameter



        public static void ResetParameter(System.Data.IDbCommand cmd2, string parameterName, object objValue)
        {
            System.Data.Common.DbCommand cmd = (System.Data.Common.DbCommand)cmd2;

            if (!parameterName.StartsWith("@"))
                parameterName = "@" + parameterName;

            if (!cmd.Parameters.Contains(parameterName))
            {
                AddParameter(cmd, parameterName, objValue);
                return;
            }

            if (objValue == null)
                objValue = System.DBNull.Value;

            cmd.Parameters[parameterName].Value = objValue;
        }



        public static void SetParameter(object param, object objValue)
        {
            SetParameter((System.Data.IDbDataParameter)param, objValue);
        }


        public static void SetParameter(System.Data.IDbDataParameter param, object objValue)
        {
            if (objValue == null)
                param.Value = System.DBNull.Value;
            else
                param.Value = objValue;
        }


        public static T GetParameterValue<T>(System.Data.IDbCommand idbc, string strParameterName)
        {
            if (!strParameterName.StartsWith("@"))
            {
                strParameterName = "@" + strParameterName;
            }

            return (T)(((System.Data.IDbDataParameter)idbc.Parameters[strParameterName]).Value);
        } // End Function GetParameterValue<T>


        // From Type to DBType
        protected static System.Data.DbType GetDbType(System.Type type)
        {
            // http://social.msdn.microsoft.com/Forums/en/winforms/thread/c6f3ab91-2198-402a-9a18-66ce442333a6
            string strTypeName = type.Name;
            System.Data.DbType dbType = System.Data.DbType.String; // default value

            try
            {
                if (object.ReferenceEquals(type, typeof(System.DBNull)))
                {
                    return dbType;
                }

                if (object.ReferenceEquals(type, typeof(System.Byte[])))
                {
                    return System.Data.DbType.Binary;
                }

                dbType = (System.Data.DbType)System.Enum.Parse(typeof(System.Data.DbType), strTypeName, true);

                // Es ist keine Zuordnung von DbType UInt64 zu einem bekannten SqlDbType vorhanden.
                // http://msdn.microsoft.com/en-us/library/bbw6zyha(v=vs.71).aspx
                if (dbType == System.Data.DbType.UInt64)
                    dbType = System.Data.DbType.Int64;
            }
            catch (System.Exception)
            {
                // add error handling to suit your taste
            }

            return dbType;
        } // End Function GetDbType

        public static System.Data.IDbDataParameter AddParameter(System.Data.IDbCommand command, string strParameterName, object objValue)
        {
            return AddParameter(command, strParameterName, objValue, System.Data.ParameterDirection.Input);
        } // End Function AddParameter


        
        public static System.Data.IDbDataParameter AddParameter(System.Data.IDbCommand command, string strParameterName, object objValue, System.Data.ParameterDirection pad)
        {
            if (objValue == null)
            {
                //throw new ArgumentNullException("objValue");
                objValue = System.DBNull.Value;
            } // End if (objValue == null)

            System.Type tDataType = objValue.GetType();
            System.Data.DbType dbType = GetDbType(tDataType);

            return AddParameter(command, strParameterName, objValue, pad, dbType);
        } // End Function AddParameter


        public static System.Data.IDbDataParameter AddParameter(System.Data.IDbCommand command, string strParameterName, object objValue, System.Data.ParameterDirection pad, System.Data.DbType dbType)
        {
            System.Data.IDbDataParameter parameter = command.CreateParameter();

            if (!strParameterName.StartsWith("@"))
            {
                strParameterName = "@" + strParameterName;
            } // End if (!strParameterName.StartsWith("@"))

            parameter.ParameterName = strParameterName;
            parameter.DbType = dbType;
            parameter.Direction = pad;

            // Es ist keine Zuordnung von DbType UInt64 zu einem bekannten SqlDbType vorhanden.
            // No association  DbType UInt64 to a known SqlDbType
            SetParameter(parameter, objValue);

            command.Parameters.Add(parameter);
            return parameter;
        } // End Function AddParameter


        private static T InlineTypeAssignHelper<T>(object UTO)
        {
            if (UTO == null)
            {
                T NullSubstitute = default(T);
                return NullSubstitute;
            }
            return (T)UTO;
        } // End Template InlineTypeAssignHelper


        public static T ExecuteScalar<T>(System.Data.IDbCommand cmd)
        {
            string strReturnValue = null;
            System.Type tReturnType = typeof(T);
            object objReturnValue = null;

            lock (cmd)
            {

                using (System.Data.IDbConnection idbc = GetConnection())
                {
                    cmd.Connection = idbc;

                    lock (cmd.Connection)
                    {

                        try
                        {
                            if (cmd.Connection.State != System.Data.ConnectionState.Open)
                                cmd.Connection.Open();

                            objReturnValue = cmd.ExecuteScalar();

                            if (objReturnValue != null)
                            {

                                if (!object.ReferenceEquals(tReturnType, typeof(System.Byte[])))
                                {
                                    strReturnValue = objReturnValue.ToString();
                                } // End if (!object.ReferenceEquals(tReturnType, typeof(System.Byte[])))

                            } // End if (objReturnValue != null)

                        } // End Try
                        catch (System.Data.Common.DbException ex)
                        {
                            // LogError("claSQL.cs ==> SQL.ExecuteScalar", ex, cmd);
                            throw;
                        } // End Catch
                        finally
                        {
                            if (cmd.Connection.State != System.Data.ConnectionState.Closed)
                                cmd.Connection.Close();
                        } // End Finally

                    } // End lock (cmd.Connection)

                } // End using idbc

            } // End lock (cmd)


            try
            {

                if (object.ReferenceEquals(tReturnType, typeof(object)))
                {
                    return InlineTypeAssignHelper<T>(objReturnValue);
                }
                else if (object.ReferenceEquals(tReturnType, typeof(string)))
                {
                    return InlineTypeAssignHelper<T>(strReturnValue);
                } // End if string
                else if (object.ReferenceEquals(tReturnType, typeof(bool)))
                {
                    bool bReturnValue = false;
                    bool bSuccess = bool.TryParse(strReturnValue, out bReturnValue);

                    if (bSuccess)
                        return InlineTypeAssignHelper<T>(bReturnValue);

                    if (strReturnValue == "0")
                        return InlineTypeAssignHelper<T>(false);

                    return InlineTypeAssignHelper<T>(true);
                } // End if bool
                else if (object.ReferenceEquals(tReturnType, typeof(int)))
                {
                    int iReturnValue = int.Parse(strReturnValue);
                    return InlineTypeAssignHelper<T>(iReturnValue);
                } // End if int
                else if (object.ReferenceEquals(tReturnType, typeof(uint)))
                {
                    uint uiReturnValue = uint.Parse(strReturnValue);
                    return InlineTypeAssignHelper<T>(uiReturnValue);
                } // End if uint
                else if (object.ReferenceEquals(tReturnType, typeof(long)))
                {
                    long lngReturnValue = long.Parse(strReturnValue);
                    return InlineTypeAssignHelper<T>(lngReturnValue);
                } // End if long
                else if (object.ReferenceEquals(tReturnType, typeof(ulong)))
                {
                    ulong ulngReturnValue = ulong.Parse(strReturnValue);
                    return InlineTypeAssignHelper<T>(ulngReturnValue);
                } // End if ulong
                else if (object.ReferenceEquals(tReturnType, typeof(float)))
                {
                    float fltReturnValue = float.Parse(strReturnValue);
                    return InlineTypeAssignHelper<T>(fltReturnValue);
                }
                else if (object.ReferenceEquals(tReturnType, typeof(double)))
                {
                    double dblReturnValue = double.Parse(strReturnValue);
                    return InlineTypeAssignHelper<T>(dblReturnValue);
                }
                else if (object.ReferenceEquals(tReturnType, typeof(System.Net.IPAddress)))
                {
                    System.Net.IPAddress ipaAddress = null;

                    if (string.IsNullOrEmpty(strReturnValue))
                        return InlineTypeAssignHelper<T>(ipaAddress);

                    ipaAddress = System.Net.IPAddress.Parse(strReturnValue);
                    return InlineTypeAssignHelper<T>(ipaAddress);
                } // End if IPAddress
                else if (object.ReferenceEquals(tReturnType, typeof(System.Byte[])))
                {
                    if (objReturnValue == System.DBNull.Value)
                        return InlineTypeAssignHelper<T>(null);

                    return InlineTypeAssignHelper<T>(objReturnValue);
                }
                else if (object.ReferenceEquals(tReturnType, typeof(System.Guid)))
                {
                    if (string.IsNullOrEmpty(strReturnValue)) return InlineTypeAssignHelper<T>(null);

                    return InlineTypeAssignHelper<T>(new System.Guid(strReturnValue));
                } // End if GUID
                else if (object.ReferenceEquals(tReturnType, typeof(System.DateTime)))
                {
                    System.DateTime bReturnValue = System.DateTime.Now;
                    bool bSuccess = System.DateTime.TryParse(strReturnValue, out bReturnValue);

                    if (bSuccess)
                        return InlineTypeAssignHelper<T>(bReturnValue);

                    if (strReturnValue == "0")
                        return InlineTypeAssignHelper<T>(false);

                    return InlineTypeAssignHelper<T>(true);
                } // End if datetime
                else // No datatype matches
                {
                    throw new System.NotImplementedException("ExecuteScalar<T>: This type is not yet defined.");
                } // End else of if tReturnType = datatype

            } // End Try
            catch (System.Exception ex)
            {
                // LogError("claSQL.cs ==> SQL.ExecuteScalar (2)", ex, cmd);
                throw;
            } // End Catch

            return InlineTypeAssignHelper<T>(null);
        } // End Function ExecuteScalar(cmd)


        public static System.Data.DataTable GetDataTable(System.Data.IDbCommand cmd)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            using (System.Data.Common.DbConnection conn = GetConnection())
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                cmd.Connection = conn;

                try
                {
                    using (var da = fact.CreateDataAdapter())
                    {
                        da.SelectCommand = (System.Data.Common.DbCommand) cmd;
                        da.Fill(dt);
                    }

                } // End Try 
                catch (System.Exception)
                {
                    throw;
                } // End Catch 
                finally
                {
                    if (conn.State != System.Data.ConnectionState.Closed)
                        conn.Close();
                } // End Finally

            } // End Using conn 

            return dt;
        }


        public static void ExecuteNonQuery(System.Data.IDbCommand cmd)
        {
            using (System.Data.Common.DbConnection conn = GetConnection())
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                cmd.Connection = conn;

                using (System.Data.Common.DbTransaction transact = conn.BeginTransaction())
                {
                    cmd.Transaction = transact;

                    try
                    {
                        cmd.ExecuteNonQuery();
                        transact.Commit();
                    } // End Try 
                    catch (System.Exception)
                    {
                        transact.Rollback();
                        throw;
                    } // End Catch 
                    finally
                    {
                        if (conn.State != System.Data.ConnectionState.Closed)
                            conn.Close();
                    } // End Finally

                } // End Using transact 

            } // End Using conn 

        } // End Sub 




        public delegate void callbackAddData_t<T>(System.Data.IDbCommand cmd, T thisItem);


        public static void InsertList<T>(System.Data.IDbCommand cmd
            , System.Collections.Generic.IEnumerable<T> listToInsert
            , callbackAddData_t<T> addDataCallback)
        {
            using (System.Data.Common.DbConnection conn = GetConnection())
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();

                cmd.Connection = conn;


                using (System.Data.Common.DbTransaction transact = conn.BeginTransaction())
                {
                    cmd.Transaction = transact;

                    try
                    {
                        foreach (T thisItem in listToInsert)
                        {
                            addDataCallback(cmd, thisItem);

                            if (cmd.ExecuteNonQuery() != 1)
                            {
                                //'handled as needed, 
                                //' but this snippet will throw an exception to force a rollback
                                throw new System.InvalidProgramException();
                            }
                        } // Next thisObject
                        transact.Commit();
                    } // End Try 
                    catch (System.Exception)
                    {
                        transact.Rollback();
                        throw;
                    } // End Catch 
                    finally
                    {
                        if (conn.State != System.Data.ConnectionState.Closed)
                            conn.Close();
                    } // End Finally

                } // End Using transact 

            } // End Using conn 

        } // End Sub 


        public static string QuoteObject(string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                throw new System.ArgumentNullException("objectName");

            return "\"" + objectName.Replace("\"", "\"\"") + "\"";
        }


        public static void InsertUpdateDataTable(string tableSchema, string tableName, System.Data.DataTable dt)
        {
            string strSQL = "SELECT * FROM ";

            if (tableSchema != null)
            {
                strSQL += QuoteObject(tableSchema) + ".";
            }

            strSQL += QuoteObject(tableName) + " WHERE (1 = 2) ";

            using (System.Data.Common.DbConnection connection = GetConnection())
            {

                using (System.Data.Common.DbDataAdapter daInsertUpdate = fact.CreateDataAdapter())
                {

                    using (System.Data.Common.DbCommand cmdSelect = connection.CreateCommand())
                    {
                        cmdSelect.CommandText = strSQL;

                        System.Data.Common.DbCommandBuilder cb = fact.CreateCommandBuilder();
                        cb.DataAdapter = daInsertUpdate;

                        daInsertUpdate.SelectCommand = cmdSelect;
                        daInsertUpdate.InsertCommand = cb.GetInsertCommand();
                        daInsertUpdate.UpdateCommand = cb.GetUpdateCommand();
                        daInsertUpdate.DeleteCommand = cb.GetDeleteCommand();

                        daInsertUpdate.Update(dt);
                    } // End Using cmdSelect

                } // End Using daInsertUpdate

            } // End Using connection 

        } // End Sub InsertUpdateDataTable 


    } // End Class SQL 


} // End Namespace TestPlotly
