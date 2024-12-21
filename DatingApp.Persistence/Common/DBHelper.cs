using DatingApp.Core.Contracts.Common;
using DatingApp.Domain.Common.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Persistence.Common
{
    public class DBHelper : IDBHelper
    {
        private int CommandTimeOut;
        private string ConnectionString;

        public DBHelper(IOptions<DBOptions> dbOptions)
        {
            ConnectionString = dbOptions.Value.DBConnection;
            CommandTimeOut = Convert.ToInt32(dbOptions.Value.SQLTimeout);
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public DataSet GetDataSet(string sql, CommandType cmdtype, Dictionary<string, string?>? sqlparams = null)
        {
            return GetDataSet<string?>(sql, cmdtype, sqlparams);
        }

        public DataSet GetDataSet<T>(string sql, CommandType cmdtype, Dictionary<string, T>? sqlparams = null)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = CreateConnection())
            {
                con.ConnectionString = ConnectionString;
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    da.SelectCommand.CommandType = cmdtype;
                    da.SelectCommand.Connection = con;
                    da.SelectCommand.CommandTimeout = CommandTimeOut;
                    if (sqlparams != null)
                    {
                        foreach (var key in sqlparams.Keys)
                        {
                            if (sqlparams[key] == null)
                                da.SelectCommand.Parameters.Add(new SqlParameter(key, DBNull.Value));
                            else
                                da.SelectCommand.Parameters.Add(new SqlParameter(key, sqlparams[key]));
                        }
                    }
                    var dtStart = DateTime.Now;
                    con.Open();
                    da.Fill(ds);
                }
            }

            return ds;
        }

        public DataTable GetDataByDataReader<T>(string sql, CommandType cmdtype, Dictionary<string, T> sqlparams)
        {
            using (SqlConnection con = CreateConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    cmd.CommandTimeout = CommandTimeOut;
                    cmd.CommandType = cmdtype;
                    if (sqlparams != null)
                    {
                        foreach (var key in sqlparams.Keys)
                        {
                            if (sqlparams[key] == null)
                                cmd.Parameters.Add(new SqlParameter(key, DBNull.Value));
                            else
                                cmd.Parameters.Add(new SqlParameter(key, sqlparams[key]));
                        }
                    }
                  
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    DataTable table = new DataTable();
                    int fieldCount = dr.FieldCount;
                    for (int i = 0; i < fieldCount; i++)
                    {
                        table.Columns.Add(dr.GetName(i), dr.GetFieldType(i));
                    }

                    while (dr.Read())
                    {
                        DataRow row = table.NewRow();
                        for (int i = 0; i < fieldCount; i++)
                        {                           
                            row[i]= dr.GetValue(i);                                                                        
                        }
                        table.Rows.Add(row);
                    }
                    return table;
                }
            }
        }


        public SqlDataReader GetSequentialDataByDataReader<T>(SqlConnection con, string sql, CommandType cmdtype, Dictionary<string, T> sqlparams)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = sql;
                cmd.Connection = con;
                cmd.CommandTimeout = CommandTimeOut;
                cmd.CommandType = cmdtype;
                if (sqlparams != null)
                {
                    foreach (var key in sqlparams.Keys)
                    {
                        if (sqlparams[key] == null)
                            cmd.Parameters.Add(new SqlParameter(key, DBNull.Value));
                        else
                            cmd.Parameters.Add(new SqlParameter(key, sqlparams[key]));
                    }
                }

                var dtStart = DateTime.Now;

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess);

                return dr;
            }
        }


        public object GetSingleResult(string sql, CommandType cmdtype, Dictionary<string, string> parameters)
        {
            object result = null;

            using (SqlConnection con = CreateConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    cmd.CommandTimeout = CommandTimeOut;
                    cmd.CommandType = cmdtype;
                    foreach (var key in parameters.Keys)
                    {
                        cmd.Parameters.AddWithValue(key, parameters[key]);
                    }

                    var dtStart = DateTime.Now;
                    con.Open();
                    result = cmd.ExecuteScalar();
                    con.Close();                
                }

                return result;
            }
        }
               
        public int ExecuteDML(string sql, CommandType cmdtype, Dictionary<string, object> parameters)
        {
            int rowno = -1;

            using (SqlConnection con = CreateConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    cmd.CommandTimeout = CommandTimeOut;
                    cmd.CommandType = cmdtype;
                    if (parameters != null)
                    {
                        foreach (var key in parameters.Keys)
                        {
                            cmd.Parameters.Add(new SqlParameter() { ParameterName = key, Value = parameters[key] });
                        }

                    }

                    var dtStart = DateTime.Now;
                    con.Open();
                    rowno = cmd.ExecuteNonQuery();                 
                    con.Close();                 
                }

                return rowno;
            }
        }

        public int ExecuteDMLWithSqlParameters(string sql, CommandType cmdtype, List<SqlParameter> parameters)
        {
            int rowno = -1;

            using (SqlConnection con = CreateConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    cmd.CommandTimeout = CommandTimeOut;
                    cmd.CommandType = cmdtype;
                    if (parameters != null)
                    {
                        foreach (SqlParameter param in parameters)
                        {
                            cmd.Parameters.Add(param);
                        }

                    }

                    var dtStart = DateTime.Now;
                    con.Open();
                    rowno = cmd.ExecuteNonQuery();
                    con.Close();                  
                }

                return rowno;
            }
        }


        public object ExecuteDMLScalar(string sql, CommandType cmdtype, Dictionary<string, object> parameters)
        {
            object retValue = null;

            using (SqlConnection con = CreateConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    cmd.CommandTimeout = CommandTimeOut;
                    cmd.CommandType = cmdtype;
                    if (parameters != null)
                    {
                        foreach (var key in parameters.Keys)
                        {
                            cmd.Parameters.AddWithValue(key, parameters[key]);
                        }

                    }

                    var dtStart = DateTime.Now;
                    con.Open();
                    retValue = cmd.ExecuteScalar();
                    con.Close();
                  
                }

                return retValue;
            }
        }

        public object ExecuteDMLScalar(string sql, CommandType cmdtype, List<SqlParameter> parameters)
        {
            object retValue = null;

            using (SqlConnection con = CreateConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    cmd.CommandTimeout = CommandTimeOut;
                    cmd.CommandType = cmdtype;
                    if (parameters != null)
                    {
                        foreach (SqlParameter param in parameters)
                        {
                            cmd.Parameters.Add(param);
                        }

                    }

                    var dtStart = DateTime.Now;
                    con.Open();
                    retValue = cmd.ExecuteScalar();
                    con.Close();                    
                }

                return retValue;
            }
        }

    }
}
