using AutoMapper;
using Project_Keystone.Api.Models.DTOs.ProductDTOs;
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

        public async Task<bool> AddProductsAsync(ProductCreateDTO productDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productDto);
                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.CommitAsync();
                _logger.LogInformation("Product added successfully.");
                return true;
            }
            catch   (Exception ex)
            {
                _logger.LogError(ex, "Error adding product.");
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

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO?> GetProductByIdAsync(int productId)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<bool> UpdateProductAsync(int productId,ProductUpdateDTO productDto)
        {
            try
            {
                var existingProduct = await _unitOfWork.Products.GetByIdAsync(productId);
                if (existingProduct == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} not found.", productId);
                    return false;
                }

                var product = _mapper.Map(productDto, existingProduct);
                _unitOfWork.Products.UpdateAsync(product);
               await  _unitOfWork.CommitAsync();
                _logger.LogInformation("Product updated successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating product {ProductId}", productId);
                return false;
            }   
        }
    }
}
