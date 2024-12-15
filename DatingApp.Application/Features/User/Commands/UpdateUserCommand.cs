using DatingApp.Domain.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Features.User.Commands
{
    public class UpdateUserCommand : MemberUpdateDto,IRequest
    {
       
    }
}
