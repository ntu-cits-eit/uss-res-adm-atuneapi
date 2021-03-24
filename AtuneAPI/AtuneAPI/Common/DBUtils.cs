using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.Common;

namespace AtuneAPI.Common
{
    class DBUtils
    {
        public static void BindParam(DbParameter Param, string Value)
        {
            if (String.IsNullOrEmpty(Value)) Param.Value = DBNull.Value;
            else Param.Value = Value;
        }

        public static void BindParam(DbParameter Param, string[] Value)
        {
            if (Value.Length == 0) Param.Value = DBNull.Value;
            else Param.Value = Value;
        }

        public static void BindParam(DbParameter Param, Queue<string> Value)
        {
            if (Value.Count == 0) Param.Value = DBNull.Value;
            else Param.Value = Value;
        }


        public static void BindParam(DbParameter Param, long[] Value)
        {
            if (Value.Length == 0) Param.Value = DBNull.Value;
            else Param.Value = Value;
        }

        public static void BindParam(DbParameter Param, byte[] Value)
        {
            if (Value.Length == 0) Param.Value = DBNull.Value;
            else Param.Value = Value;
        }

        public static void BindParam(DbParameter Param, byte[][] Value)
        {
            if (Value.Length == 0) Param.Value = DBNull.Value;
            else Param.Value = Value;
        }

        public static void BindParam(DbParameter Param, DateTime[] Value)
        {
            if (Value.Length == 0) Param.Value = DBNull.Value;
            else Param.Value = Value;
        }

        public static void BindParam(DbParameter Param, long? Value)
        {
            if (!Value.HasValue) Param.Value = DBNull.Value;
            else Param.Value = Value;
        }

        public static void BindParam(DbParameter Param, double? Value)
        {
            if (!Value.HasValue) Param.Value = DBNull.Value;
            else Param.Value = Value;
        }


        public static void BindParam(DbParameter Param, decimal? Value)
        {
            if (!Value.HasValue) Param.Value = DBNull.Value;
            else Param.Value = Value;
        }
        public static void BindParam(DbParameter Param, double[] Value)
        {
            if (Value.Length == 0) Param.Value = DBNull.Value;
            else Param.Value = Value;

        }

        public static void BindParam(DbParameter Param, int? Value)
        {
            if (!Value.HasValue) Param.Value = DBNull.Value;
            else Param.Value = Value;
        }

        public static void BindParam(DbParameter Param, char? Value)
        {
            if (!Value.HasValue) Param.Value = DBNull.Value;
            else Param.Value = Value;
        }

        public static void BindParam(DbParameter Param, char[] Value)
        {
            if (Value.Length == 0) Param.Value = DBNull.Value;
            else Param.Value = Value;
        }

        public static void BindParam(DbParameter Param, DateTime? Value)
        {
            if (!Value.HasValue) Param.Value = DBNull.Value;
            else Param.Value = Value;
        }


        public static DbParameter CreateParameter(string ParamName, string Value)
        {
            DbParameter result = CreateStringParameter(ParamName);
            BindParam(result, Value);
            return result;
        }


        public static DbParameter CreateParameter(string ParamName, string[] Value)
        {
            DbParameter result = CreateStringParameter(ParamName);
            BindParam(result, Value);
            return result;
        }

        public static DbParameter CreateParameter(string ParamName, Queue<string> Value)
        {
            DbParameter result = CreateStringParameter(ParamName);
            BindParam(result, Value);
            return result;
        }

        public static DbParameter CreateParameter(string ParamName, long[] Value)
        {
            DbParameter result = CreateLongParameter(ParamName);
            BindParam(result, Value);
            return result;
        }


        public static DbParameter CreateParameter(string ParamName, byte[] Value)
        {
            DbParameter result = CreateByteParameter(ParamName);
            BindParam(result, Value);
            return result;
        }

        public static DbParameter CreateParameter(string ParamName, byte[][] Value)
        {
            DbParameter result = CreateByteParameter(ParamName);
            BindParam(result, Value);
            return result;
        }

        public static DbParameter CreateParameter(string ParamName, DateTime[] Value)
        {
            DbParameter result = CreateDateParameter(ParamName);
            BindParam(result, Value);
            return result;
        }

        public static DbParameter CreateParameter(string ParamName, Char? Value)
        {
            DbParameter result = CreateCharParameter(ParamName);
            BindParam(result, Value);
            return result;
        }

        public static DbParameter CreateParameter(string ParamName, Char[] Value)
        {
            DbParameter result = CreateCharParameter(ParamName);
            BindParam(result, Value);
            return result;
        }

        public static DbParameter CreateParameter(string ParamName, DateTime? Value)
        {
            DbParameter result = CreateDateParameter(ParamName);
            BindParam(result, Value);

            return result;
        }

        public static DbParameter CreateParameter(string ParamName, long? Value)
        {
            DbParameter result = CreateLongParameter(ParamName);
            BindParam(result, Value);

            return result;
        }

        public static DbParameter CreateParameter(string ParamName, double? Value)
        {
            DbParameter result = CreateDoubleParameter(ParamName);
            BindParam(result, Value);

            return result;
        }


        public static DbParameter CreateParameter(string ParamName, decimal? Value)
        {
            DbParameter result = CreateDecimalParameter(ParamName);
            BindParam(result, Value);

            return result;
        }
        public static DbParameter CreateParameter(string ParamName, double[] Value)
        {
            DbParameter result = CreateLongParameter(ParamName);
            BindParam(result, Value);

            return result;
        }

        public static DbParameter CreateParameter(string ParamName, int? Value)
        {
            DbParameter result = CreateLongParameter(ParamName);
            BindParam(result, Value);

            return result;
        }

        public static DbParameter CreateParameter(string ParamName)
        {
            DbParameter result = CreateCursorParameter(ParamName);
            result.Direction = ParameterDirection.Output;

            return result;

        }

        public enum OutputParamType
        {
            Blob = 102,
            Byte = 103,
            Char = 104,
            Date = 106,
            Decimal = 107,
            Double = 108,
            Long = 109,
            Int = 112,
            RefCursor = 121,
            Varchar2 = 126,

        }
        public static DbParameter CreateParameter(string ParamName, OutputParamType type)
        {
            DbParameter result = null;
            switch (type)
            {
                case OutputParamType.Varchar2:
                    result = CreateStringParameter(ParamName);
                    result.Direction = ParameterDirection.Output;
                    result.Size = 4000;
                    break;


                case OutputParamType.Long:
                    result = CreateLongParameter(ParamName);
                    result.Direction = ParameterDirection.Output;
                    break;

                case OutputParamType.Int:
                    result = CreateIntParameter(ParamName);
                    result.Direction = ParameterDirection.Output;
                    break;

                case OutputParamType.Date:
                    result = CreateDateParameter(ParamName);
                    result.Direction = ParameterDirection.Output;
                    break;

                case OutputParamType.Char:
                    result = CreateCharParameter(ParamName);
                    result.Direction = ParameterDirection.Output;
                    break;

                case OutputParamType.RefCursor:
                    result = CreateCursorParameter(ParamName);
                    result.Direction = ParameterDirection.Output;
                    break;

                case OutputParamType.Byte:
                    result = CreateByteParameter(ParamName);
                    result.Direction = ParameterDirection.Output;
                    break;


            }
            return result;


        }

        public static DbParameter CreateStringParameter(string ParameterName)
        {
            return new OracleParameter(ParameterName, OracleDbType.Varchar2);
        }

        public static DbParameter CreateLongParameter(string ParameterName)
        {
            return new OracleParameter(ParameterName, OracleDbType.Int64);
        }

        public static DbParameter CreateDoubleParameter(string ParameterName)
        {
            return new OracleParameter(ParameterName, OracleDbType.Double);
        }

        public static DbParameter CreateDecimalParameter(string ParameterName)
        {
            return new OracleParameter(ParameterName, OracleDbType.Decimal);
        }


        public static DbParameter CreateIntParameter(string ParameterName)
        {
            return new OracleParameter(ParameterName, OracleDbType.Int32);
        }

        public static DbParameter CreateDateParameter(string ParameterName)
        {
            return new OracleParameter(ParameterName, OracleDbType.Date);
        }

        public static DbParameter CreateCharParameter(string ParameterName)
        {
            return new OracleParameter(ParameterName, OracleDbType.Char);
        }

        public static DbParameter CreateCursorParameter(string ParameterName)
        {
            return new OracleParameter(ParameterName, OracleDbType.RefCursor);
        }

        public static DbParameter CreateByteParameter(string ParameterName)
        {
            return new OracleParameter(ParameterName, OracleDbType.Blob);
        }

        public static string ReadParameter(DbParameter Parameter)
        {
            return Convert.ToString(Parameter.Value);
        }


        public static void FreeCommand(DbCommand Cmd)
        {
            if (Cmd != null) Cmd.Dispose();
        }


        public static DataTable CreateTable(DbCommand Cmd)
        {
            DbDataAdapter da = new OracleDataAdapter();

            try
            {
                da.SelectCommand = Cmd;
                DataTable result = new DataTable();
                da.Fill(result);

                return result;
            }
            finally
            {
                da.Dispose();
            }
        }

        public static DataTable CreateTable(DbDataReader Reader)
        {
            DataTable result = new DataTable();
            result.Load(Reader);
            return result;
        }



        public static string ReadString(DbDataReader Reader, string FieldName)
        { return ReadString(Reader, FieldName, string.Empty); }


        public static string ReadString(DbDataReader Reader, string FieldName,
                                        string DefaultValue)
        {

            int ordinal = Reader.GetOrdinal(FieldName);


            if (Reader.IsDBNull(ordinal)) return DefaultValue;

            return Reader.GetValue(ordinal).ToString();
        }


        public static char? ReadChar(DbDataReader Reader, string FieldName)
        { return ReadChar(Reader, FieldName, null); }



        public static char? ReadChar(DbDataReader Reader, string FieldName,
                                     char? DefaultValue)
        {
            int ordinal = Reader.GetOrdinal(FieldName);
            if (Reader.IsDBNull(ordinal)) return DefaultValue;

            return Convert.ToChar(Reader[ordinal]);
        }



        public static long? ReadLong(DbDataReader Reader, string FieldName)
        { return ReadLong(Reader, FieldName, null); }



        public static long? ReadLong(DbDataReader Reader, string FieldName,
                                     long? DefaultValue)
        {
            int ordinal = Reader.GetOrdinal(FieldName);
            if (Reader.IsDBNull(ordinal)) return DefaultValue;
            return Convert.ToInt64(Reader[ordinal]);
        }


        public static int? ReadInt(DbDataReader Reader, string FieldName)
        { return ReadInt(Reader, FieldName, null); }

        public static int? ReadInt(DbDataReader Reader, string FieldName,
                                   int? DefaultValue)
        {
            int ordinal = Reader.GetOrdinal(FieldName);
            if (Reader.IsDBNull(ordinal)) return DefaultValue;
            return Convert.ToInt32(Reader[ordinal]);
        }



        public static double? ReadDouble(DbDataReader Reader, string FieldName)
        { return ReadDouble(Reader, FieldName, null); }

        public static double? ReadDouble(DbDataReader Reader, string FieldName,
                                 double? DefaultValue)
        {
            int ordinal = Reader.GetOrdinal(FieldName);
            if (Reader.IsDBNull(ordinal)) return DefaultValue;
            return Convert.ToDouble(Reader[ordinal]);
        }

        public static DateTime? ReadDate(DbDataReader Reader, string FieldName)
        { return ReadDate(Reader, FieldName, null); }



        public static DateTime? ReadDate(DbDataReader Reader, string FieldName,
                                         DateTime? DefaultValue)
        {
            int ordinal = Reader.GetOrdinal(FieldName);
            if (Reader.IsDBNull(ordinal)) return DefaultValue;

            return Convert.ToDateTime(Reader[ordinal]);

        }


        public static DbDataAdapter CreateAdapter()
        {
            DbProviderFactory objFactory = null;
            objFactory = OracleClientFactory.Instance;
            DbDataAdapter adapter = objFactory.CreateDataAdapter();
            return adapter;
        }


        public static DateTime GetServerDate(DbConnection Connection)
        {
            const string SQL = "SELECT SYSDATE FROM DUAL";
            DbCommand Cmd = Connection.CreateCommand();
            Cmd.CommandText = SQL;
            return Convert.ToDateTime(Cmd.ExecuteScalar());
        }


        public static DateTime GetServerDate(DbTransaction Transaction)
        {
            const string SQL = "SELECT SYSDATE FROM DUAL";
            DbCommand Cmd = Transaction.Connection.CreateCommand();
            Cmd.Transaction = Transaction;
            Cmd.CommandText = SQL;
            return Convert.ToDateTime(Cmd.ExecuteScalar());
        }


        public static DataSet ExecuteQuery(DbConnection Connection,
                                           DbCommand Cmd, DataSet ds,
                                           string TableName)
        {
            DbDataAdapter adapter = CreateAdapter();

            adapter.SelectCommand = Cmd;
            try
            {
                adapter.Fill(ds, TableName);
            }
            finally
            {
                Cmd.Dispose();
            }

            return ds;
        }



    }
}
