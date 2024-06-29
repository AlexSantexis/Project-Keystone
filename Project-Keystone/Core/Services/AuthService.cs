using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Project_Keystone.Core.Entities;
using System.Security.Claims;
using Project_Keystone.Infrastructure.Repositories.Interfaces;
using Project_Keystone.Core.Services.Interfaces;
using Project_Keystone.Api.Models.DTOs.UserDTOs;

using Project_Keystone.Api.Exceptions.AuthExceptions;
using Project_Keystone.Api.Models.DTOs;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Project_Keystone.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole<string>> _roleManager;
        private readonly ILogger<AuthService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<string>> roleManager, ILogger<AuthService> logger, IMapper mapper, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<bool> RegisterUserAsync(UserRegisterDTO registerDTO)
        {

            var user = _mapper.Map<User>(registerDTO);
            user.UserName = registerDTO.Email!;
            user.SecurityStamp = Guid.NewGuid().ToString();
            _logger.LogInformation($"Attempting to create user: User ID: {user.Id}, UserName: {user.UserName}");

            var result = await _userManager.CreateAsync(user, registerDTO.Password!);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
                _logger.LogWarning("User registration failed for {Email}: {Errors}", user.Email, errors);
                throw new UserRegistrationFailedException(errors);
            }

            await _userManager.AddToRoleAsync(user, "User");
            _logger.LogInformation("User {Email} registered successfully.", user.Email);
            return true;
        }

        public async Task<TokenModel> LoginUserAsync(UserLoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email!);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password!))
            {
                _logger.LogWarning("Login failed for {Email}", loginDTO.Email);
                throw new InvalidCredentialsException();
            }

            if (string.IsNullOrEmpty(user.SecurityStamp))
            {
                user.SecurityStamp = Guid.NewGuid().ToString();
                await _userManager.UpdateAsync(user);
            }

            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _unitOfWork.Tokens.CreateJWTToken(user, roles.ToList());
            var refreshToken = _unitOfWork.Tokens.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:RefreshTokenValidityInDays"]));
            await _userManager.UpdateAsync(user);

            _logger.LogInformation("User {Email} logged in successfully.", loginDTO.Email);
            return new TokenModel { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        public async Task<TokenModel> RefreshTokenAsync(TokenModel tokenModel)
        {
            if (tokenModel == null)
            {
                _logger.LogError("Token model is null");
                throw new ArgumentNullException(nameof(tokenModel));
            }

            ClaimsPrincipal principal;
            try
            {
                principal = _unitOfWork.Tokens.GetPrincipalFromExpiredToken(tokenModel.AccessToken!);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get principal from expired token");
                throw new SecurityTokenException("Invalid access token", ex);
            }

            
            var email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                _logger.LogError("Email is null or empty in the token");
                throw new SecurityTokenException("Invalid token, email not found");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning("User not found: {Email}", email);
                throw new SecurityTokenException("Invalid token, user not found");
            }

            if (user.RefreshToken != tokenModel.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                _logger.LogWarning("Invalid refresh token for user: {Email}", email);
                throw new SecurityTokenException("Invalid refresh token");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var newAccessToken = _unitOfWork.Tokens.CreateJWTToken(user, roles.ToList());
            var newRefreshToken = _unitOfWork.Tokens.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:RefreshTokenValidityInDays"]));

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                var errors = string.Join(", ", updateResult.Errors.Select(e => e.Description));
                _logger.LogError("Failed to update user refresh token: {Errors}", errors);
                throw new Exception($"Failed to update user: {errors}");
            }

            return new TokenModel
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }



        public async Task<bool> ChangePasswordAsync(string userId, UserUpdatePasswordDTO updatePasswordDTO)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("UserId cannot be null or empty.", nameof(userId));
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("Change password failed. User with ID {UserId} not found.", userId);
                throw new UserNotFoundException(userId);
            }

            var result = await _userManager.ChangePasswordAsync(user, updatePasswordDTO.currentPassword!, updatePasswordDTO.newPassword!);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("Change password failed for {Email}: {Errors}", user.Email, errors);
                throw new PasswordChangeFailedException(errors);
            }

            _logger.LogInformation("Password changed successfully for user {Email}", user.Email);
            return true;
        }


        public async Task<(bool Success,string? Token)> UpdateUserAsync(UserUpdateDTO updateDTO)
        {
            var user = await _userManager.FindByEmailAsync(updateDTO.CurrentEmail);
            if (user == null)
            {
                _logger.LogWarning("Update user failed. User with email {Email} does not exist", updateDTO.Email);
                throw new UserNotFoundException(updateDTO.Email);
            }

            _mapper.Map(updateDTO, user);
            user.UserName = updateDTO.Email;
            user.NormalizedEmail = _userManager.NormalizeEmail(updateDTO.Email);
            user.NormalizedUserName = _userManager.NormalizeName(updateDTO.Email);
            user.SecurityStamp = Guid.NewGuid().ToString();

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
                _logger.LogWarning("Update user failed for email {Email}: {Errors}", user.Email, errors);
                throw new UserUpdateFailedException(errors);
            }

            _logger.LogInformation("User with email {Email} updated successfully", user.Email);
            var userRoles = await _userManager.GetRolesAsync(user);
            var newToken = _unitOfWork.Tokens.CreateJWTToken(user, userRoles.ToList());
            return (true, newToken);
        }

        public async Task<bool> DeleteUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning("User not found with email {Email}", email);
                throw new UserNotFoundException(email);
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(",", result.Errors.Select(e => e.Description));
                _logger.LogWarning("User deletion failed for {Email}: {Errors}", email, errors);
                throw new Exception($"Failed to delete user: {errors}");
            }

            _logger.LogInformation("User {Email} deleted successfully.", email);
            return true;
        }
        public async Task<UserDTO> GetCurrentUserAsync(ClaimsPrincipal userPrincipal)
        {
            var userId = userPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User ID not found");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }

            return _mapper.Map<UserDTO>(user);
        }
       


    }

}

