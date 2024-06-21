using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Project_Keystone.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Project_Keystone.Infrastructure.Repositories.Interfaces;
using Project_Keystone.Core.Services.Interfaces;
using Project_Keystone.Api.Models.DTOs.UserDTOs;

namespace Project_Keystone.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole<string>> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpContextAccessor httpContextAccessor,IUnitOfWork unitOfwork,UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<string>> roleManager, IConfiguration configuration, ILogger<AuthService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfwork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> RegisterUserAsync(UserRegisterDTO registerDTO)
        {
            try
            {
                var user = _mapper.Map<User>(registerDTO);
                user.UserName = registerDTO.Email!;
                user.SecurityStamp = Guid.NewGuid().ToString();
                _logger.LogInformation($"Before CreateAsync: User ID: {user.Id}, UserName: {user.UserName}");
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
                if (string.IsNullOrEmpty(user.SecurityStamp))
                {
                    user.SecurityStamp = Guid.NewGuid().ToString();
                }
                
                await _userManager.UpdateAsync(user);
                var roles = (await _userManager.GetRolesAsync(user)).ToList();
                var access_token =  _unitOfWork.Tokens.CreateJWTToken(user,roles);
                _logger.LogInformation("User {Email} logged in successfully.", loginDTO.Email);
                return access_token;
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

        public async Task<(bool Success,string? Token)> UpdateUserAsync(UserUpdateDTO updateDTO)
        {
  
            try
            {
                _logger.LogInformation("Attempting to update user with email {Email}", updateDTO.Email);

                var user = await _userManager.FindByEmailAsync(updateDTO.Email);
                if (user == null)
                {
                    _logger.LogWarning("Update user failed. User with email {Email} does not exist", user);
                    return (false, null);
                }
                _logger.LogInformation("User found. Updating user details.");
                _mapper.Map(updateDTO, user);

                user.UserName = updateDTO.Email;
                user.NormalizedEmail = _userManager.NormalizeEmail(updateDTO.Email);
                user.NormalizedUserName = _userManager.NormalizeName(updateDTO.Email);
                user.SecurityStamp = Guid.NewGuid().ToString();
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User with email {Email} updated successfully", user.Email);
                    var userRoles = (await _userManager.GetRolesAsync(user)).ToList();
                    var newToken = _unitOfWork.Tokens.CreateJWTToken(user,userRoles);
                    return (true, newToken);
                }

                var errors = string.Join(", ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
                _logger.LogWarning("Update user failed for email {Email}: {Errors}", user.Email, errors);
                return (false, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user with email {Email}", updateDTO.Email);
                throw;
            }
        }

        public async Task<bool> DeleteUserAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if(user == null)
                {
                    _logger.LogWarning("User not found with email {Email}", email);
                    return false;
                }
                var result = await _userManager.DeleteAsync(user);
                if(!result.Succeeded)
                {
                    var errors = string.Join(",", result.Errors.Select(e => e.Description));
                    _logger.LogWarning("User deletion failed for {Email}: {Errors}", email, errors);
                    return false;
                }
                _logger.LogInformation("User {Email} deleted successfully.", email);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user {Email}", email);
                throw;
            }
        }
        public async Task<UserDTO> GetCurrentUserAsync(ClaimsPrincipal userPrincipal)
        {
            var userId = userPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId == null)
            {
                throw new UnauthorizedAccessException("User ID not found");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            return _mapper.Map<UserDTO>(user);
        }
       


    }

}

