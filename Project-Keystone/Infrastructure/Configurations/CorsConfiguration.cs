﻿namespace Project_Keystone.Infrastructure.Configurations
{
    public static class CorsConfiguration
    {
        public static void AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    b => b.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AngularClient",
                    b => b.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyOrigin());
            });
        }
    }
}
