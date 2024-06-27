using Project_Keystone.Api.Models.DTOs;

namespace Project_Keystone.Core.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<TokenResponse> RefreshTokenAsync(string refreshToken);
        string GenerateRefreshToken();
        Task<bool> ValidateRefreshTokenAsync(string refreshToken);
    }
}
