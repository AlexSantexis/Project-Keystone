using Project_Keystone.Core.Entities;

namespace Project_Keystone.Core.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category?> GetCategoryByNameAsync(string name);
    }
}
