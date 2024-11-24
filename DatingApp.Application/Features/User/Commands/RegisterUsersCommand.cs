using DatingApp.Domain.Dto;
using DatingApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Features.User.Commands
{
    public class RegisterUsersCommand : IRequest<UserDto>
    {
        [Required]
        public string username { get; set; }
        [Required]
        [MinLength(5)]        
        public string password { get; set; }
    }
}
