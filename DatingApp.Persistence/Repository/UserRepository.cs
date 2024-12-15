using DatingApp.Core.Contracts.Common;
using DatingApp.Core.Contracts.Repositories;
using DatingApp.Domain.Dto;
using DatingApp.Domain.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
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
            parameters.Add("@Gender", user.Gender);
            parameters.Add("@DateOfBirth", user.DateOfBirth);
            parameters.Add("@KnownAs", user.KnownAs);
            parameters.Add("@Created", user.Created);
            parameters.Add("@LastActive", user.LastActive);
            parameters.Add("@Introduction", user.Introduction);
            parameters.Add("@LookingFor", user.LookingFor);
            parameters.Add("@Interests", user.Interests);
            parameters.Add("@City", user.City);
            parameters.Add("@Country", user.Country);

            dt = sqlHelper.GetDataByDataReader("sp_RegisterUser", CommandType.StoredProcedure, parameters);
            return dt;

        }

        public int UpdateUser(MemberUpdateDto user)
        {
            int rowsAffectedCount = 0;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Id", user.Id);
            parameters.Add("@Introduction", user.Introduction);
            parameters.Add("@LookingFor", user.LookingFor);
            parameters.Add("@Interests", user.Interests);
            parameters.Add("@City", user.City);
            parameters.Add("@Country", user.Country);

            rowsAffectedCount = sqlHelper.ExecuteDML("sp_UpdateUser", CommandType.StoredProcedure, parameters);
                      
            return rowsAffectedCount;
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
