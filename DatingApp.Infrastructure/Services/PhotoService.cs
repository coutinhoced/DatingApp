using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp.Core.Contracts.Repositories;
using DatingApp.Core.Contracts.Services;
using DatingApp.Domain.Common.Options;
using DatingApp.Domain.Dto;
using DatingApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Infrastructure.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        private IPhotoRepository _photoRepository;
        public PhotoService(IOptions<CloudinaryOptions> cloudinaryOptions, IPhotoRepository photoRepository)
        {
            var account = new Account(cloudinaryOptions.Value.CloudName, cloudinaryOptions.Value.ApiKey,
                                      cloudinaryOptions.Value.ApiSecret);

            _cloudinary = new Cloudinary(account);
            this._photoRepository = photoRepository;
        }

        public async Task<PhotoDto> AddPhotoAsync(IFormFile file, int UserId)
        {
            PhotoDto photoDto = new PhotoDto();
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill")
                                                         .Gravity("face"),
                    Folder = "da-loveme"
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            if (uploadResult.Error != null)
            {
                photoDto.ValidationError = uploadResult.Error.Message;
                return photoDto;
            }

            int Id = _photoRepository.AddUserPhoto<int>(UserId, uploadResult.SecureUrl.AbsoluteUri, uploadResult.PublicId);

            photoDto.Id = Id;
            photoDto.Url = uploadResult.SecureUrl.AbsoluteUri;
            photoDto.IsMain = false;
            photoDto.UserId = UserId;

            return photoDto;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}
