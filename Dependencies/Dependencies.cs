using ApiDevBP;
using ApiDevBP.Services;
using ApiDevBP.Services.Interfaces;
using ApiDevBP.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ApiDevBP.Mapper;

namespace ApiDevBP.Dependencies
{
    public static class Dependencies
    {
        public static void ConfigureDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApiDevBPContext>(options =>
                   options.UseSqlite("Data Source=ApiDevBP_DB.db"));
            services.Configure<ForbbidenNames>(configuration.GetSection("Forbbiden"));   
            services.AddScoped<IUserService, UserService>();

            services.AddSingleton<IAutoMapper, AutoMapperWrapper>();
        }
    }
}
