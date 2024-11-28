using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static int Calculate(this DateTime dateOfBirth)
        {
            var today = DateTime.Now.Date;  // DateOnly.FromDateTime(DateTime.Now);
            var age = today.Year - dateOfBirth.Year;
            if(dateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }

    }
}
