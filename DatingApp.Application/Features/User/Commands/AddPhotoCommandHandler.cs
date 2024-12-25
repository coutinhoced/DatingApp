using DatingApp.Core.Contracts.Services;
using DatingApp.Domain.Dto;
using DatingApp.Domain.Entities;
using DatingApp.Infrastructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Application.Features.User.Commands
{
    public class AddPhotoCommandHandler : IRequestHandler<AddPhotoCommand, Photo>
    {
        private IPhotoService _photoService;
        public AddPhotoCommandHandler(IPhotoService photoService)
        {
            this._photoService = photoService;
        }
        public async Task<Photo> Handle(AddPhotoCommand request, CancellationToken cancellationToken)
        {
            var userPhoto = await _photoService.AddPhotoAsync(request.file, request.UserId);                                             
            return await Task.FromResult(userPhoto);
        }
    }
}
