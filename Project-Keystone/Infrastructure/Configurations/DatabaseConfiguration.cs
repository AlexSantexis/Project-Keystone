using Microsoft.EntityFrameworkCore;
using Project_Keystone.Infrastructure.Data;

namespace Project_Keystone.Infrastructure.Configurations
{
    public static  class DatabaseConfiguration
    {
        public static void AddDataBaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            services.AddDbContext<ProjectKeystoneDbContext>(options =>
            options.UseSqlServer(connectionString));
        }
    }
}
