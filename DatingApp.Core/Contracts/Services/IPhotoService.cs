using CloudinaryDotNet.Actions;
using DatingApp.Domain.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Contracts.Services
{
    public interface IPhotoService
    {

        Task<PhotoDto> AddPhotoAsync(IFormFile file, int UserId);

        Task<DeletionResult> DeletePhotoAsync(string publicId);

        void UpdateMainPhoto(int photoId);
    }
}
