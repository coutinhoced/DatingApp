using DatingApp.Core.Contracts.Common;
using DatingApp.Core.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDBHelper sqlHelper;
        public UserRepository(IDBHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }

        public DataSet GetAllUsers(string? name = null)
        {
            DataSet ds;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("@Name", name);

            ds = sqlHelper.GetDataSet("sp_GetAllUsers", CommandType.StoredProcedure, parameters);
            return ds;
           
        }
    }
}
