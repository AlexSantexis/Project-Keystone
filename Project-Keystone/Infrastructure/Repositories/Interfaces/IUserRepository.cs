using Project_Keystone.Core.Entities;

namespace Project_Keystone.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IEnumerable<User>> GetAllUsersWithDetailsAsync();
        Task<User?> GetUserByIdWithDetailsAsync(string userId);

        Task DeleteUserRelatedEntitiesAsync(string userId);
    }
}
