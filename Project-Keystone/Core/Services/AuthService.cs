using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Project_Keystone.Api.Models.DTOs;
using Project_Keystone.Core.Entities;
using Project_Keystone.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Project_Keystone.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;
        private readonly IMapper _mapper;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<int>> roleManager, IConfiguration configuration, ILogger<AuthService> logger, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> RegisterUserAsync(UserRegisterDTO registerDTO)
        {
            try
            {
                var user = _mapper.Map<User>(registerDTO);
                user.UserName = registerDTO.Email!;

                var result = await _userManager.CreateAsync(user, registerDTO.Password!);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    _logger.LogInformation("User {Email} registered successfully.", user.Email);
                    return true;
                }

                var errors = string.Join(", ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
                _logger.LogWarning("User registration failed for {Email}: {Errors}", user.Email, errors);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering user {Email}", registerDTO.Email);
                throw;
            }
        }

        public async Task<string> LoginUserAsync(UserLoginDTO loginDTO)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDTO.Email!);
                if (user == null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password!))
                {
                    _logger.LogWarning("Login failed for {Email}", loginDTO.Email);
                    throw new Exception("Login failed. Invalid email or password.");
                }

                var token = GenerateToken(user);
                _logger.LogInformation("User {Email} logged in successfully.", loginDTO.Email);
                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in user {Email}", loginDTO.Email);
                throw;
            }
        }

        public async Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    _logger.LogWarning("Change password failed. User with ID {UserId} not found.", userId);
                    return false;
                }

                var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Password changed successfully for user {Email}", user.Email);
                    return true;
                }

                _logger.LogWarning("Change password failed for {Email}: {Errors}", user.Email, string.Join(", ", result.Errors));
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while changing password for user {UserId}", userId);
                throw;
            }
        }

        public async Task<TokenModel> RefreshToken(TokenModel model)
        {
            try
            {
                if (model == null)
                {
                    return new TokenModel { Message = "Invalid Request" };
                }

                var principal = GetPrincipalFromExpiredToken(model.AccessToken);
                if (principal == null)
                {
                    return new TokenModel { Message = "Invalid Refresh Token or Access Token" };
                }

                var username = principal.Identity!.Name;
                var user = await _userManager.FindByNameAsync(username!);
                if (user == null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                {
                    return new TokenModel { Message = "Invalid Refresh Token or Access Token" };
                }

                var newAccessToken = GenerateToken(user);
                var newRefreshToken = GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["Jwt:RefreshTokenValidityInMinutes"]));
                await _userManager.UpdateAsync(user);

                _logger.LogInformation("Token refreshed successfully for user {Email}", user.Email);

                return new TokenModel
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while refreshing token");
                throw;
            }
        }

        public string GenerateToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var secretKey = jwtSettings["Key"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddMinutes(Convert.ToInt32(jwtSettings["ExpiryMinutes"]));

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiry,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }

}

