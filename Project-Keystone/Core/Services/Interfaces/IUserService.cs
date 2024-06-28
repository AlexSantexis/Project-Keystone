using Project_Keystone.Api.Models.DTOs.UserDTOs;

namespace Project_Keystone.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDetailedDTO>> GetAllUsersAsync();
        Task<UserDetailedDTO?> GetUserByIdAsync(string userId);
        Task<UserDetailedDTO> UpdateUserAsync(string userId, UserDetailedDTO userDto);
        Task<bool> DeleteUserAsync(string userId);
    }
}
