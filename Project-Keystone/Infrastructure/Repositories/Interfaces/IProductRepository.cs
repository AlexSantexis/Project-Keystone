using Project_Keystone.Core.Entities;

namespace Project_Keystone.Infrastructure.Repositories.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
        Task<Product?> UpdateProductAsync(int productId, Product updatedProduct);
        Task<bool> UpdateProductImageUrlAsync(int productId, string imageUrl);
        Task<IEnumerable<Product>> GetProductWithDetailsAsync();

        Task<IEnumerable<Product>> FilterProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<Product>> GetProductsByGenreIdAsync(int genreId);

    }
}
