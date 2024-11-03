using DatingApp.Core.Contracts.Repositories;
using DatingApp.Core.Contracts.Services;
using DatingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository repo)
        {
            userRepository = repo;
        }

        public List<AppUser> GetAllUsers(string? name = null)
        {
            List<AppUser> appUsers = new List<AppUser>();

            try
            {
                using (DataSet ds = userRepository.GetAllUsers(name))
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            appUsers.Add(MapToContact(dr));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return appUsers;
        }
        private AppUser MapToContact(DataRow dataRow)
        {
            AppUser appUser = new AppUser();
            appUser.Id =Convert.ToInt32( dataRow["Id"]);
            appUser.UserName = Convert.ToString( dataRow["UserName"]);

            return appUser;
        }
    }
}
