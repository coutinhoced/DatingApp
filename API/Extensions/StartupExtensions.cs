using API.Filters;
using API.Middleware;
using DatingApp.Application;
using DatingApp.Infrastructure;
using DatingApp.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extensions
{
    public static class StartupExtensions
    {
        public static WebApplicationBuilder ConfigureDatingServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<SanitizeParametersFilter>();
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddInfrastructureServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddApplicationServices();            
            return builder;
        }     

        public static IApplicationBuilder ConfigureDatingMiddlewarePipeLine(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            return app;
        }
    }
}
