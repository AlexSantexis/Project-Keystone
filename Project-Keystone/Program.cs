
using Project_Keystone.Infrastructure.Configurations;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Project_Keystone.Core.Services;
using Microsoft.OpenApi.Models;
using Project_Keystone.Core.Filters;
using Microsoft.AspNetCore.Identity;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;
using Project_Keystone.Core.Services.Interfaces;
using Project_Keystone.Api.Middleware;


namespace Project_Keystone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddSerilogConfiguration();
            builder.Services.AddDataBaseConfiguration(builder.Configuration);
            builder.Services.AddCorsConfiguration();
            builder.Services.AddRepositoriesConfiguration();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IWishlistService, WishlistService>();
            builder.Services.AddScoped(provider =>
                 new MapperConfiguration(cfg =>
                 {
                     cfg.AddProfile(new MapperConfig());
                 })
             .CreateMapper());
            builder.Services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            })
                .AddRoles<IdentityRole<string>>()
                .AddSignInManager<SignInManager<User>>()
                .AddEntityFrameworkStores<ProjectKeystoneDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.IncludeErrorDetails = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                    };
                });
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Project-Keystone", Version = "v1" });
                options.SupportNonNullableReferenceTypes();
                options.OperationFilter<AuthorizeOperationFilter>();
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,
                    new OpenApiSecurityScheme
                    {
                       Description = "JWT Authorization header using the Bearer scheme.",
                       Name = "Authorization",
                       In = ParameterLocation.Header,
                       Type = SecuritySchemeType.Http,
                       Scheme = JwtBearerDefaults.AuthenticationScheme,
                       BearerFormat = "JWT"
                    });
            });



           
            


            var app = builder.Build();

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
    
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            


            app.MapControllers();
            
            app.Run();
        }
        

    }
}
