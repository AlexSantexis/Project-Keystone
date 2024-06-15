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

            if (authAttributes.Any())
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

                var securityRequirement = new OpenApiSecurityRequirement();
                var scheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = JwtBearerDefaults.AuthenticationScheme },
                    Scheme = "oath2",
                    Name = "Authorization",
                    In = ParameterLocation.Header
                };
                securityRequirement.Add(scheme, new List<string>());
                operation.Security = new List<OpenApiSecurityRequirement> { securityRequirement };

                var roles = authAttributes
                    .Where(attr => !string.IsNullOrWhiteSpace(attr.Roles))
                    .SelectMany(attr => attr.Roles!.Split(','))
                    .Distinct();
                if (roles.Any())
                {
                    operation.Description += $"Roles: {string.Join(", ", roles)}";
                }
            }

        }
    }
}
