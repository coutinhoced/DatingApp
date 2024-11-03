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

        public DataSet GetAllUsers()
        {
            DataSet ds;
            ds = sqlHelper.GetDataSet("sp_GetAllUsers", CommandType.StoredProcedure, null);
            return ds;
           
        }
    }
}
