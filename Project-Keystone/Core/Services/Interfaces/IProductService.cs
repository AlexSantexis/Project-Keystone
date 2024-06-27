using Project_Keystone.Api.Models.DTOs.ProductDTOs;
using Project_Keystone.Core.Entities;
using System.Linq.Expressions;

namespace Project_Keystone.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDTO> CreateProductAsync(ProductCreateDTO productDto);
        Task<ProductDTO?> UpdateProductAsync(int productId, ProductUpdateDTO productUpdateDto);
        Task<bool> DeleteProductAsync(int productId);
        Task<ProductDTO?> GetProductByIdAsync(int productId);
        Task<IEnumerable<ProductDTO?>> GetProductsByCategoryIdAsync(int categoryId, int page = 1, int pageSize = 10);
        Task<IEnumerable<ProductDTO?>> GetProductsByGenreIdAsync(int genreId, int page = 1, int pageSize = 10);
        Task<IEnumerable<ProductDTO?>> GetProductWithDetailsAsync(int page = 1, int pageSize = 10);

        Task<IEnumerable<ProductDTO>> SearchProductsByNameAsync(string searchTerm);
        Task<IEnumerable<ProductDTO>> FilterAndSortProductsAsync(
               string sortOrder = "asc",
               string? genreName = null,
               int page = 1,
               int pageSize = 10,
               string? categoryName = null);
    }
}
