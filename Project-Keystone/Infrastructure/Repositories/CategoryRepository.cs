using Microsoft.EntityFrameworkCore;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;
using Project_Keystone.Infrastructure.Repositories.Interfaces;

namespace Project_Keystone.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ProjectKeystoneDbContext context) : base(context)
        {
        }

        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<IEnumerable<Category>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.Categories.Where(c => ids.Contains(c.CategoryId)).ToListAsync();
        }



        public async Task<List<Category>> SearchCategoriesAsync(string searchTerm)
        {
            return await _context.Categories
                .Where(c => c.Name.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<List<Category>> GetCategoriesWithProductCountAsync()
        {
            return await _context.Categories
            .Include(c => c.ProductCategories)
            .Select(c => new Category
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
            })
            .ToListAsync();
        }

        public async Task<bool> CategoryExistsAsync(string name)
        {
            return await _context.Categories.AnyAsync(c => c.Name.ToLower() == name.ToLower());
        }
    }
}
