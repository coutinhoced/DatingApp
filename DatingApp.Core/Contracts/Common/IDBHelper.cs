using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;


namespace DatingApp.Core.Contracts.Common
{
    public interface IDBHelper
    {        
        SqlConnection CreateConnection();
        int ExecuteDML(string sql, CommandType cmdtype, Dictionary<string, object> parameters);
        int ExecuteDMLWithSqlParameters(string sql, CommandType cmdtype, List<SqlParameter> parameters);

        DataSet GetDataSet(string sql, CommandType cmdtype, Dictionary<string, string?>? sqlparams = null);

        DataSet GetDataSet<T>(string sql, CommandType cmdtype, Dictionary<string, T?>? sqlparams = null);

        DataTable GetDataByDataReader<T>(string sql, CommandType cmdtype, Dictionary<string, T> parameters);
        SqlDataReader GetSequentialDataByDataReader<T>(SqlConnection con, string sql, CommandType cmdtype, Dictionary<string, T> parameters);

        object GetSingleResult(string sql, CommandType cmdtype, Dictionary<string, string> sqlparams);


    }
}
