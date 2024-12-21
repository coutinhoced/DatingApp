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

        /// <summary>
        /// Performs ExecuteScalar and returns first row of the first column and rest are ignored
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdtype"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object ExecuteDMLScalar(string sql, CommandType cmdtype, Dictionary<string, object> parameters);

        /// <summary>
        ///  Performs ExecuteScalar and returns first row of the first column and rest are ignored
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdtype"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object ExecuteDMLScalar(string sql, CommandType cmdtype, List<SqlParameter> parameters);
    }
}
