using Serilog;

namespace Project_Keystone.Infrastructure.Configurations
{
    public static  class SerilogConfiguration
    {
        public static void AddSerilogConfiguration(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);
            });
        }
    }
}
