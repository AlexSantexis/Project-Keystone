
using Project_Keystone.Infrastructure.Configurations;
using AutoMapper;


namespace Project_Keystone
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddSerilogConfiguration();
            builder.Services.AddDataBaseConfiguration(builder.Configuration);
            builder.Services.AddAuthenticationConfiguration(builder.Configuration);
            builder.Services.AddSwaggerConfiguration();
            builder.Services.AddCorsConfiguration();
            builder.Services.AddIdentityConfiguration();
            builder.Services.AddRepositoriesConfiguration();
            builder.Services.AddAuthServiceConfiguration();
            builder.Services.AddScoped(provider =>
                 new MapperConfiguration(cfg =>
                 {
                     cfg.AddProfile(new MapperConfig());
                 })
             .CreateMapper());
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();


            var app = builder.Build();

            await RoleConfiguration.CreateRoles(app.Services);

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
            
        
            app.Run();
        }
        

    }
}
