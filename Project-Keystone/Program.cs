
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Project_Keystone.Core.Entities;
using Project_Keystone.Filters;
using Project_Keystone.Infrastructure.Configurations;
using Project_Keystone.Infrastructure.Data;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Project_Keystone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AddSerilogConfiguration();
            builder.Services.AddDataBaseConfiguration(builder.Configuration);
            builder.Services.AddAuthenticationConfiguration(builder.Configuration);
            builder.Services.AddSwaggerConfiguration();
            builder.Services.AddCorsConfiguration();
            builder.Services.AddControllers();
      
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Application started");
            app.Run();
        }
        
    }
}
