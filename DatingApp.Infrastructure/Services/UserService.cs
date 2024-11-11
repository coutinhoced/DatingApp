using DatingApp.Core.Contracts.Repositories;
using DatingApp.Core.Contracts.Services;
using DatingApp.Domain.Dto;
using DatingApp.Domain.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        private ITokenService tokenService;

        public UserService(IUserRepository repo, ITokenService tokenService)
        {
            this.userRepository = repo;
            this.tokenService = tokenService;
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
                            appUsers.Add(MapToUser(dr));
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

        public UserDto RegisterUser(string username, string password)
        {
            AppUser appUser = new AppUser();
            UserDto userDto = new UserDto();
            try
            {
                using var hmac = new HMACSHA512();
                var userExists = GetAllUsers(username);

                if (userExists.Count > 0)
                {
                    throw new DuplicateNameException("username already exists");
                }

                var user = new AppUser
                {
                    UserName = username,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                    PasswordSalt = hmac.Key
                };

                DataTable registeredUserTable = userRepository.RegisterUser(user);

                userDto.Username = username;
                userDto.Token = tokenService.CreateToken(user);                                             
            }
            catch (Exception ex)
            {
                userDto.Exception = ex;
                userDto.ValidationError = ex.Message;
            }
            return userDto;
        }


        public UserDto GetLoginUser(string username, string password)
        {
            AppUser appUser = new AppUser();
            UserDto userDto = new UserDto();
            try
            {
                DataTable loginUser = userRepository.GetLoginUser(username);
                
                if (loginUser.Rows.Count == 0)
                {
                    appUser.ValidationError = "Invalid username";
                    return userDto;
                }

                appUser = MapSingleRowModel<AppUser>(loginUser, appUser);

                using var hmac = new HMACSHA512(appUser.PasswordSalt);

                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != appUser.PasswordHash[i])
                    {
                        userDto.ValidationError = "Invalid password";
                        return userDto;
                    }
                }

                userDto.Username = username;
                userDto.Token = tokenService.CreateToken(appUser);

            }
            catch (Exception ex)
            {
                userDto.Exception = ex;
                userDto.ValidationError = ex.Message;
            }
            return userDto;
        }

        private AppUser MapToUser(DataRow dataRow)
        {
            AppUser appUser = new AppUser();
            appUser.Id = Convert.ToInt32(dataRow["Id"]);
            appUser.UserName = Convert.ToString(dataRow["UserName"]);

            return appUser;
        }

        private T MapSingleRowModel<T>(DataTable table, T model)
        {
            foreach (var prop in model.GetType().GetProperties(BindingFlags.DeclaredOnly |
                                                               BindingFlags.Public | BindingFlags.Instance))
            {
                prop.SetValue(model, table.Rows[0][prop.Name]);
            }
            return model;
        }
    }
}
