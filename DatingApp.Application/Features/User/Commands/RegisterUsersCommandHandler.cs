using DatingApp.Core.Contracts.Services;
using DatingApp.Domain.Dto;
using DatingApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Features.User.Commands
{
    public class RegisterUsersCommandHandler : IRequestHandler<RegisterUsersCommand, UserDto>
    {
        private IUserService userService;

        public RegisterUsersCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public Task<UserDto> Handle(RegisterUsersCommand request, CancellationToken cancellationToken)
        {
            var user = this.userService.RegisterUser(request.username.ToLower(), request.password);
            return Task.FromResult(user);
        }


    }
}
