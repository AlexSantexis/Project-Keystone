using Project_Keystone.Core.Entities;

namespace Project_Keystone.Core.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
        Task<Product?> UpdateProductAsync(int productId, Product updatedProduct);
        Task<bool> UpdateProductImageAsync(int productId, byte[] imageData);
        Task<IEnumerable<Product>> GetProductWithDetailsAsync();

        Task<IEnumerable<Product>> FilterProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<Product>> GetProductsByGenreIdAsync(int genreId);

    }
}
