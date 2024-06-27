using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Project_Keystone.Api.Models.DTOs.UserDTOs;
using Project_Keystone.Core.Entities;
using System.Security.Claims;

namespace Project_Keystone.Core.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(UserRegisterDTO registerDTO);
        Task<string> LoginUserAsync(UserLoginDTO loginDTO);
        //Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        Task<bool> ChangePasswordAsync(string userId, UserUpdatePasswordDTO updatePasswordDTO);
        Task<(bool Success, string? Token)> UpdateUserAsync(UserUpdateDTO updateDTO);
        Task<bool> DeleteUserAsync(string email);
        Task<UserDTO> GetCurrentUserAsync(ClaimsPrincipal user);

    }
}
