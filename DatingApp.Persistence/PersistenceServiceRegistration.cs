using DatingApp.Core.Contracts.Common;
using DatingApp.Core.Contracts.Repositories;
using DatingApp.Domain.Common.Options;
using DatingApp.Persistence.Common;
using DatingApp.Persistence.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddOptions<DBOptions>(configuration.GetConnectionString("ConnectionStrings"));
     
            services.AddTransient<IDBHelper, DBHelper>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();

            return services;
        
        }


    }
}
