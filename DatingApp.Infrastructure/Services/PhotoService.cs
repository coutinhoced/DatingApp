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

        public async Task<Photo> AddPhotoAsync(IFormFile file, int UserId)
        {
            Photo photo = new Photo();
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
                photo.ValidationError = uploadResult.Error.Message;
                return photo;
            }

            int Id = _photoRepository.AddUserPhoto<int>(UserId, uploadResult.SecureUrl.AbsoluteUri, uploadResult.PublicId);

            photo.Id = Id;
            photo.Url = uploadResult.SecureUrl.AbsoluteUri;
            photo.IsMain = false;
            photo.UserId = UserId;
            photo.PublicId = uploadResult.PublicId;

            return photo;
        }

        public async Task DeletePhotoAsync(Photo photo)
        {
            var result = await DeletePhotoAsyncFromCloudinary(photo.PublicId);
            if (result.Error != null)
            {
                throw new Exception("Problem while deleting photo");
            }
            else
            {
                int rowsAffected = _photoRepository.DeletePhoto(photo.Id);
                if (rowsAffected == 0)
                {
                    throw new Exception("Problem while deleting photo");
                }
            }
        }

        private async Task<DeletionResult> DeletePhotoAsyncFromCloudinary(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            return await _cloudinary.DestroyAsync(deleteParams);
        }

        public void UpdateMainPhoto(int photoId)
        {
            int rowsAffected = this._photoRepository.UpdateMainPhoto(photoId);
            if (rowsAffected == 0)
            {
                throw new Exception("Problem while setting main photo");
            }
        }
    }
}
