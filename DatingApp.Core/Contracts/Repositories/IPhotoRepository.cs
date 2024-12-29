using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Contracts.Repositories
{
    public interface IPhotoRepository
    {
        DataTable AddUserPhoto(int UserId, string Url, string PublicId);
        int UpdateMainPhoto(int photoId);

        int DeletePhoto(int photoId);
    }
}
