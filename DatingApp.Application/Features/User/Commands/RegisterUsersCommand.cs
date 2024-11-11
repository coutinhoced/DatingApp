using DatingApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Features.User.Commands
{
    public class RegisterUsersCommand : IRequest<AppUser>
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
