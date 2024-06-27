using Microsoft.IdentityModel.Tokens;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project_Keystone.Infrastructure.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _config;
        public TokenRepository(IConfiguration config) 
        {
            _config = config;
        }
        public string CreateJWTToken(User user, List<string> roles)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),  
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("Firstname", user.Firstname!),
            new Claim("Lastname", user.Lastname!),
            new Claim("Email",user.Email!)
        };

            
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured.")));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expirationMinutes = Convert.ToDouble(_config["Jwt:ExpirationInMinutes"] ?? "15");
            var token = new JwtSecurityToken
                (
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(expirationMinutes),
                    signingCredentials: credentials);

                    return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
