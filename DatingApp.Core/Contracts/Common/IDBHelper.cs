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

        DataSet GetDataSet(string sql, CommandType cmdtype, Dictionary<string, string?>? sqlparams = null);

        DataSet GetDataSet<T>(string sql, CommandType cmdtype, Dictionary<string, T?>? sqlparams = null);

    }
}
