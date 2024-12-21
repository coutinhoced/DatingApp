using DatingApp.Core.Contracts.Common;
using DatingApp.Core.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Persistence.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly IDBHelper sqlHelper;
        public PhotoRepository(IDBHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }
        public T AddUserPhoto<T>(int UserId, string Url, string PublicId)
        {
            T returnValue;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@UserId", UserId);
            parameters.Add("@Url", Url);
            parameters.Add("@PublicId", PublicId);

            returnValue =(T)sqlHelper.ExecuteDMLScalar("sp_AddUserPhoto", CommandType.StoredProcedure, parameters);

            return returnValue;
        }
    }
}
