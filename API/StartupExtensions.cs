using DatingApp.Application;
using DatingApp.Infrastructure;
using DatingApp.Persistence;

namespace API
{
    public static class StartupExtensions
    {
        public static WebApplicationBuilder ConfigureDatingServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddInfrastructureServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            return builder;
        }
    }
}
