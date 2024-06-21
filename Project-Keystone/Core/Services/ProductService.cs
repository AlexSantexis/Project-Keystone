using AutoMapper;
using Project_Keystone.Core.Entities;
using Project_Keystone.Core.Services.Interfaces;
using Project_Keystone.Infrastructure.Repositories;
using Project_Keystone.Infrastructure.Repositories.Interfaces;

namespace Project_Keystone.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> AddProductsAsync(Product product)
        {
            try
            {
                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch   (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding product {ProductId}", product.ProductId);
                return false;
            }
            
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            try
            {
                var result = await _unitOfWork.Products.DeleteAsync(productId);
                if (result)
                {
                    await _unitOfWork.CommitAsync();
                    _logger.LogInformation("Product deleted successfully.");
                    return true;
                }
                _logger.LogWarning("Product with ID {ProductId} not found.", productId);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product.");
                return false;
            }
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _unitOfWork.Products.GetAllAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _unitOfWork.Products.GetByIdAsync(productId);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            try
            {
                _unitOfWork.Products.UpdateAsync(product);
               await  _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating product {ProductId}", product.ProductId);
                return false;
            }   
        }
    }
}
