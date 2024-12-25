using DatingApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Features.User.Commands
{
    public class DeletePhotoCommand : IRequest<Task>
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public bool IsMain { get; set; }
        public string? PublicId { get; set; }

        public int UserId { get; set; }
    }
}
