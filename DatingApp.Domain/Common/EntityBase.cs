using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Common
{
    public class EntityBase
    {
        public string ValidationError { get; set; } = string.Empty;

        public Exception Exception { get; set; }

    }
}
