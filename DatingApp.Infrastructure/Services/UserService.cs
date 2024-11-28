using DatingApp.Core.Contracts.Repositories;
using DatingApp.Core.Contracts.Services;
using DatingApp.Domain.Dto;
using DatingApp.Domain.Entities;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DatingApp.Infrastructure.Extensions;

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

        public List<MemberDto> GetAllUsers(string? name = null)
        {           
            List<PhotoDto> photos = new List<PhotoDto>();
            List<MemberDto> memberDtos = new List<MemberDto>();
            try
            {
                using (DataSet ds = userRepository.GetAllUsers(name))
                {                   
                    if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            PhotoDto photo = new PhotoDto();
                            photos.Add(MapSingleRowModel<PhotoDto>(dr, photo));
                        }
                    }

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {                                               
                            MemberDto memberDto = new MemberDto();                            
                            int userId = Convert.ToInt32(dr["Id"]);
                            List<PhotoDto> photosToMap = photos.Where(x => x.UserId == userId).ToList();
                            memberDtos.Add(MapToUserDto(dr, photosToMap));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return memberDtos;
        }

        public UserDto RegisterUser(string username, string password)
        {
            AppUser appUser = new AppUser();
            UserDto userDto = new UserDto();
            try
            {
                var userExists = GetAllUsers(username);
                if (userExists.Count > 0)
                {
                    throw new DuplicateNameException("username already exists");
                }

                using var hmac = new HMACSHA512();
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
                    userDto.ValidationError = "Invalid username";
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

        private MemberDto MapToUserDto(DataRow dataRow, List<PhotoDto> photos)
        {
            var photoUrl = photos.Where(p => p.IsMain == true).Select(x => x.Url).FirstOrDefault();

            MemberDto memberDto = new MemberDto();
            memberDto.Id = Convert.ToInt32(dataRow["Id"]);
            memberDto.UserName = Convert.ToString(dataRow["UserName"]);
            memberDto.Age = Convert.ToDateTime(dataRow["DateOfBirth"]).Calculate();
            memberDto.KnownAs = Convert.ToString(dataRow["KnownAs"]);
            memberDto.Created = Convert.ToDateTime(dataRow["Created"]);
            memberDto.LastActive = Convert.ToDateTime(dataRow["LastActive"]);
            memberDto.Gender = Convert.ToString(dataRow["Gender"]);
            memberDto.Introduction = Convert.ToString(dataRow["Introduction"]);
            memberDto.Interests = Convert.ToString(dataRow["Interests"]);
            memberDto.LookingFor = Convert.ToString(dataRow["LookingFor"]);
            memberDto.City = Convert.ToString(dataRow["City"]);
            memberDto.Country = Convert.ToString(dataRow["Country"]);
            memberDto.PhotoUrl = photoUrl;
            memberDto.Photos = photos;
            return memberDto;
        }

        private T MapSingleRowModel<T>(DataRow dataRow, T model)
        {
            foreach (var prop in model.GetType().GetProperties(BindingFlags.DeclaredOnly |
                                                              BindingFlags.Public | BindingFlags.Instance))
            {
                object currentRowValue = dataRow[prop.Name];
                if (prop.PropertyType.Name != dataRow[prop.Name].GetType().Name)
                {
                    Type modelPropertyType = Type.GetType(prop.PropertyType.FullName);
                    currentRowValue = Convert.ChangeType(currentRowValue, modelPropertyType);
                }

                prop.SetValue(model, currentRowValue);
            }
            return model;
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
