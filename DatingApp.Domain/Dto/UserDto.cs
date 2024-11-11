using DatingApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Dto
{
    public class UserDto: EntityBase
    {
        public string Username { get; set; }    
        public string Token { get; set; }
    }
}
