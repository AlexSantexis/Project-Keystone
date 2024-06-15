namespace Project_Keystone.Core.Interfaces
{
    public interface IUserRoleRepository
    {
        Task AddUserRoleAsync(int userId, string role);
        Task RemoveUserRoleAsync(int userId, string role);
        Task<List<string>> GetUserRolesAsync(int userId);
    }
}
