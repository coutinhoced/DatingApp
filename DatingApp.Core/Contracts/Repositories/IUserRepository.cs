using DatingApp.Domain.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Contracts.Repositories
{
    public interface IUserRepository
    {
        DataSet GetAllUsers(string? name =null);
        DataTable RegisterUser(AppUser user);

        DataTable GetLoginUser(string username);
    }
}
