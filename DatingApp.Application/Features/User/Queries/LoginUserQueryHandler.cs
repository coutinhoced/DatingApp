using DatingApp.Core.Contracts.Services;
using DatingApp.Domain.Dto;
using DatingApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Features.User.Queries
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, UserDto>
    {
        private IUserService userService;
        public LoginUserQueryHandler(IUserService userService)
        {
            this.userService = userService;
        }
        public Task<UserDto> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var loginUser = userService.GetLoginUser(request.username, request.password);    
            return Task.FromResult(loginUser);
        }
    }
}
