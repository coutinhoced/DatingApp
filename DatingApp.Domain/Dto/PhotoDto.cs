using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatingApp.Domain.Common;

namespace DatingApp.Domain.Dto
{
    public class PhotoDto : EntityBase
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public bool IsMain { get; set; }
        public int UserId { get; set; }
    }
}
