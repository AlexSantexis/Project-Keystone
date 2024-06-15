using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Project_Keystone.Api.Models.DTOs;
using Project_Keystone.Core.Entities;

namespace Project_Keystone.Core.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(UserRegisterDTO registerDTO);
        Task<string> LoginUserAsync(UserLoginDTO loginDTO);
        Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task<TokenModel> RefreshToken(TokenModel model);
        string GenerateToken(User user);
        string GenerateRefreshToken();
    }
}
