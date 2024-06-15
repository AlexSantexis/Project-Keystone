using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Project_Keystone.Infrastructure.Configurations
{
    public static class AuthenticationConfiguration
    {
       
        public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            var secretKey = jwtSettings["Key"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expiryInMinutes = Convert.ToInt32(jwtSettings["ExpiryMinutes"]);


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.IncludeErrorDetails = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudience = audience,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
                        ClockSkew = TimeSpan.Zero,  // Override the default clock skew of 5 mins
                        SignatureValidator = (token, validationParameters) => new JwtSecurityToken(token)
                    
                    };
                });
            return services;
        }
    }
}
