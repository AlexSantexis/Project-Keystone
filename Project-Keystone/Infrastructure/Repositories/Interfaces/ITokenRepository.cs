using Project_Keystone.Core.Entities;
using System.Security.Claims;

namespace Project_Keystone.Infrastructure.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTToken(User user, List<string> roles);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
