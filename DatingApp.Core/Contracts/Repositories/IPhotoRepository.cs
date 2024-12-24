using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Contracts.Repositories
{
    public interface IPhotoRepository
    {
        T AddUserPhoto<T>(int UserId, string Url, string PublicId);
        int UpdateMainPhoto(int photoId);
    }
}
