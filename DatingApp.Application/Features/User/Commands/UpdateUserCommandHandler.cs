using DatingApp.Core.Contracts.Services;
using DatingApp.Domain.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Features.User.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {

        private IUserService userService;

        public UpdateUserCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {           
            this.userService.UpdateUser(request);
            return Task.CompletedTask;
        }
    }
}
