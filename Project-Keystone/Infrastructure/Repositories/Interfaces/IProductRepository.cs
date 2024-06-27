using Project_Keystone.Core.Entities;
using System.Linq.Expressions;

namespace Project_Keystone.Infrastructure.Repositories.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product?> GetProductByIdWithDetailsAsync(int productId);
        Task<IEnumerable<Product?>> GetProductsByCategoryIdWithDetailsAsync(int categoryId, int page, int pageSize);
        Task<IEnumerable<Product?>> GetProductsByGenreIdWithDetailsAsync(int genreId, int page, int pageSize);
        Task<Product?> UpdateProductAsync(int productId, Product updatedProduct);
        Task<IEnumerable<Product>> GetProductWithDetailsAsync(int page, int pageSize);

        Task<IEnumerable<Product>> SearchProductsByNameAsync(string? searchTerm);
        Task<IEnumerable<Product>> FilterAndSortProductsAsync(
               string sortOrder = "asc",
               string? genreName = null,
               int page = 1,
               int pageSize = 10,
               string? categoryName = null);
    }
}
