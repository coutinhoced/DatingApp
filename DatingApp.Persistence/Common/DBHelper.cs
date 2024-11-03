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
    }
}
