using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.OpenApi.Models;
using Project_Keystone.Core.Entities;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Project_Keystone.Core.Filters
{
    public class AuthorizeOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var authAttributes = context.MethodInfo
                 .GetCustomAttributes(true)
                 .OfType<AuthorizeAttribute>()
                 .Distinct();

            if (!authAttributes.Any()) return;
            var securityScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "JWT Authorization header using the Bearer scheme."
            };
            var securityRequirement = new OpenApiSecurityRequirement
            {
                { securityScheme, new List<string>() }
            };
            operation.Security = new List<OpenApiSecurityRequirement> { securityRequirement };
            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

            var roles = authAttributes
            .SelectMany(a => a.Roles?.Split(',') ?? Enumerable.Empty<string>())
            .Distinct()
            .ToList();


            if (roles.Any())
            {
                securityRequirement[securityScheme] = roles;
                operation.Description += "\nRequired roles: " + string.Join(", ", roles);
            }
        }
    }
}
