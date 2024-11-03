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
        List<AppUser> GetAllUsers(string? name = null);
    }
}
