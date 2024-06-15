using Project_Keystone.Core.Entities;

namespace Project_Keystone.Core.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        
        
        Task<User?> UpdateUserAsync(int userId, User request);
        Task<User?> GetUserByEmailAsync(string email);
        Task<List<User>> GetAllUsersFilteredAsync(int pageNumber, int pageSize, List<Func<User, bool>> filters);

        Task AddUserRoleAsync(int userId, string role);
        Task RemoveUserRoleAsync(int userId, string role);
    }
}
