using DatingApp.Core.Contracts.Services;
using DatingApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Features.User.Commands
{
    public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand, Task>
    {
        private IPhotoService _photoService;
        public DeletePhotoCommandHandler(IPhotoService photoService)
        {
            this._photoService = photoService;
        }              

        public Task<Task> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
        {
            Photo photo = new Photo
            {
                Id = request.Id,
                IsMain = request.IsMain,
                PublicId = request.PublicId,
                Url = request.Url,
                UserId = request.UserId
            };
            var result = _photoService.DeletePhotoAsync(photo);
            return Task.FromResult(result);
        }
        
    }
}
