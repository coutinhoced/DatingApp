using DatingApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Features.User.Queries
{
    public class LoginUserQuery : IRequest<AppUser>
    {
        public string username { get; set; }
        public string password { get; set; }

    }
}
