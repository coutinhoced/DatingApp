using DatingApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Features.User.Queries
{
    public class GetUsersQuery  :IRequest<List<AppUser>>
    { 
        public string name {  get; set; }
    }
}
