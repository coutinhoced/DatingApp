using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Common
{
    public class ApiException(int statuscode, string message, string? details)
    {             

        public int Statuscode { get; set; } = statuscode;
        public string Message { get; set; } = message;
        public string? Details { get; set; } = details;

    }
}
