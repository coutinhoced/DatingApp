using DatingApp.Domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Features.User.Commands
{
    public class AddPhotoCommand : IRequest<PhotoDto>
    {
        public IFormFile file { get; set; }
        public int UserId { get; set; }
    }
}
