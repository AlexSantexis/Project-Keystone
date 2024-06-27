using Project_Keystone.Core.Entities;

namespace Project_Keystone.Infrastructure.Repositories.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category?> GetCategoryByNameAsync(string name);
        Task<IEnumerable<Category>> GetByIdsAsync(IEnumerable<int> ids);
        Task<List<Category>> GetCategoriesWithProductCountAsync();
        Task<bool> CategoryExistsAsync(string name);
        Task<List<Category>> SearchCategoriesAsync(string searchTerm);
    }
}
