﻿using DatingApp.Core.Contracts.Common;
using DatingApp.Core.Contracts.Repositories;
using DatingApp.Domain.Dto;
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
        public DataTable AddUserPhoto(int UserId, string Url, string PublicId)
        {
            DataTable dt = new DataTable();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@UserId", UserId);
            parameters.Add("@Url", Url);
            parameters.Add("@PublicId", PublicId);

            dt = sqlHelper.GetDataByDataReader("sp_AddUserPhoto", CommandType.StoredProcedure, parameters);
                      
            return dt;
        }

        public int UpdateMainPhoto(int photoId)
        {
            int rowsAffectedCount = 0;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Id", photoId);

            rowsAffectedCount = sqlHelper.ExecuteDML("sp_UpdateMainPhoto", CommandType.StoredProcedure, parameters);

            return rowsAffectedCount;
        }


        public int DeletePhoto(int photoId)
        {
            int rowsAffectedCount = 0;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Id", photoId);

            rowsAffectedCount = sqlHelper.ExecuteDML("sp_DeletePhoto", CommandType.StoredProcedure, parameters);

            return rowsAffectedCount;
        }
    }
}
