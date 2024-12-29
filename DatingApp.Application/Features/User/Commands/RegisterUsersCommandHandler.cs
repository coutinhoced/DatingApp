using DatingApp.Core.Contracts.Services;
using DatingApp.Domain.Dto;
using DatingApp.Domain.Entities;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            AppUser appUser = new AppUser()
            {
                UserName = request.username,
                KnownAs = request.KnownAs,
                Gender = request.Gender,
                DateOfBirth = Convert.ToDateTime(request.DateOfBirth),
                City = request.City,
                Country = request.Country
            };

            var user = this.userService.RegisterUser(appUser, request.password);
            return Task.FromResult(user);
        }


    }
}
