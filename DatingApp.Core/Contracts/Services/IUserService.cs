using DatingApp.Domain.Dto;
using DatingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Contracts.Services
{
    public interface IUserService
    {
        List<MemberDto> GetAllUsers(string? name = null);
        UserDto RegisterUser(string username, string password);

        UserDto GetLoginUser(string username, string password);
    }
}
