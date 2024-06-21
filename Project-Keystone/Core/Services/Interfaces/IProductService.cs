using Project_Keystone.Core.Entities;

namespace Project_Keystone.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddProductsAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int productId);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int productId);

    }
}
