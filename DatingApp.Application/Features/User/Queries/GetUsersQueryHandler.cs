using DatingApp.Core.Contracts.Services;
using DatingApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Features.User.Queries
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<AppUser>>
    {
        private IUserService userService;
        public GetUsersQueryHandler(IUserService userService)
        {
            this.userService = userService; 
        }

        public Task<List<AppUser>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var allUsers = userService.GetAllUsers();   
            return Task.FromResult(allUsers);
        }
    }
}
