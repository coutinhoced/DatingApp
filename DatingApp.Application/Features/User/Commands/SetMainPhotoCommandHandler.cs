using DatingApp.Core.Contracts.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Features.User.Commands
{
    public class SetMainPhotoCommandHandler : IRequestHandler<SetMainPhotoCommand>
    {
        private IPhotoService _photoService;
        public SetMainPhotoCommandHandler(IPhotoService photoService)
        {
            this._photoService = photoService;
        }
        public Task Handle(SetMainPhotoCommand request, CancellationToken cancellationToken)
        {
            _photoService.UpdateMainPhoto(request.photoId);
            return Task.CompletedTask;
        }
    }
}
