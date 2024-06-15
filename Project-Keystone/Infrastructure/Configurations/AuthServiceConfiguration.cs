using Project_Keystone.Core.Interfaces;
using Project_Keystone.Core.Services;

namespace Project_Keystone.Infrastructure.Configurations
{
    public static class AuthServiceConfiguration
    {
        public static IServiceCollection AddAuthServiceConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
