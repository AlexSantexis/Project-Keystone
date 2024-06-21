using Project_Keystone.Api.Models.DTOs.ProductDTOs;
using Project_Keystone.Core.Entities;

namespace Project_Keystone.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddProductsAsync(ProductCreateDTO productDto);
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO?> GetProductByIdAsync(int productId);
        Task<bool> UpdateProductAsync(int productId,ProductUpdateDTO productDto);
        Task<bool> DeleteProductAsync(int productId);

    }
}
