using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_Keystone.Api.Models.DTOs;
using Project_Keystone.Core.Entities;
using Project_Keystone.Core.Services.Interfaces;
using Project_Keystone.Infrastructure.Repositories.Interfaces;
using System.Security.Cryptography;

namespace Project_Keystone.Core.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenRepository _tokenRepository;
        private readonly ILogger<RefreshTokenService> _logger;

        public RefreshTokenService(UserManager<User> userManager, IConfiguration configuration, ITokenRepository tokenRepository, ILogger<RefreshTokenService> logger)
        {
            _userManager = userManager;
            _configuration = configuration;
            _tokenRepository = tokenRepository;
            _logger = logger;
        }

        public string GenerateRefreshToken()
        {
            _logger.LogInformation("Generating new refresh token");
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<TokenResponse> RefreshTokenAsync(string refreshToken)
        {
            try
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);
                if (user == null || !await ValidateRefreshTokenAsync(refreshToken))
                {
                    _logger.LogWarning("Invalid or expired refresh token attempt");
                    throw new InvalidOperationException("Invalid or expired refresh token");
                }
                var roles = await _userManager.GetRolesAsync(user);
                var newAccessToken = _tokenRepository.CreateJWTToken(user, roles.ToList());
                var newRefreshToken = GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["RefreshToken:ExpiryInDays"] ?? "7"));
                await _userManager.UpdateAsync(user);

                _logger.LogInformation("Token successfully refreshed for user: {UserId}", user.Id);

                return new TokenResponse
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while refreshing the token");
                throw;
            }
        }
        public async Task<bool> ValidateRefreshTokenAsync(string refreshToken)
        {
            _logger.LogInformation("Validating refresh token");
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);
            var isValid = user != null && user.RefreshTokenExpiryTime > DateTime.UtcNow;
            _logger.LogInformation("Refresh token validation result: {IsValid}", isValid);
            return isValid;
        }
    }

}
