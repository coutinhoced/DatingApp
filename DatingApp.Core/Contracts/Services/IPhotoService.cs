using CloudinaryDotNet.Actions;
using DatingApp.Domain.Dto;
using DatingApp.Domain.Entities;
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

        Task<Photo> AddPhotoAsync(IFormFile file, int UserId);

        Task DeletePhotoAsync(Photo photo);

        void UpdateMainPhoto(int photoId);
    }
}
