using Project_Keystone.Core.Entities;

namespace Project_Keystone.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetUserAsync(string email, string password);
        Task<User?> UpdateUserAsync(int userId, User request);
        Task<User?> GetUserByEmailAsync(string email);
        Task<List<User>> GetAllUsersFilteredAsync(int pageNumber, int pageSize, List<Func<User, bool>> filters);

        Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
        Task AddUserRoleAsync(int userId, string role);
        Task RemoveUserRoleAsync(int userId, string role);
        Task<List<string>> GetUserRolesAsync(int userId);
    }
}
