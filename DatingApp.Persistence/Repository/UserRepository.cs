using DatingApp.Core.Contracts.Common;
using DatingApp.Core.Contracts.Repositories;
using DatingApp.Domain.Entities;
using Microsoft.Data.SqlClient;
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

        public DataTable RegisterUser(AppUser user)
        {
            DataTable dt;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@UserName", user.UserName);
            parameters.Add("@PasswordHash", user.PasswordHash);
            parameters.Add("@PasswordSalt", user.PasswordSalt);

            dt = sqlHelper.GetDataByDataReader("sp_RegisterUser", CommandType.StoredProcedure, parameters);
            return dt;

        }


        public DataTable GetLoginUser(string username)
        {
            DataTable dt;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Username", username);          

            dt = sqlHelper.GetDataByDataReader("sp_GetLoginUser", CommandType.StoredProcedure, parameters);
            return dt;

        }
    }
}
