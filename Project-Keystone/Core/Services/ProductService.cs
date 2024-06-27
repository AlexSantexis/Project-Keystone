using AutoMapper;
using Project_Keystone.Api.Exceptions.ProductExceptions;
using Project_Keystone.Api.Models.DTOs.ProductDTOs;
using Project_Keystone.Core.Entities;
using Project_Keystone.Core.Services.Interfaces;
using Project_Keystone.Infrastructure.Repositories;
using Project_Keystone.Infrastructure.Repositories.Interfaces;
using System.Linq.Expressions;


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



        public async Task<ProductDTO> CreateProductAsync(ProductCreateDTO productCreateDto)
        {
            try
            {
                var product = _mapper.Map<Product>(productCreateDto);
                product.CreatedAt = DateTime.UtcNow;
                product.UpdatedAt = DateTime.UtcNow;
                product.ImageUrl = productCreateDto.ImgUrl;

                await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.CommitAsync();

                var createdProduct = await _unitOfWork.Products.GetProductByIdWithDetailsAsync(product.ProductId);
                if (createdProduct == null)
                {
                    throw new ProductCreationFailedException("Product was not found after creation");
                }
                return _mapper.Map<ProductDTO>(createdProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product.");
                throw new ProductCreationFailedException(ex.Message);
            }
        }

        public async Task<ProductDTO?> UpdateProductAsync(int productId, ProductUpdateDTO productUpdateDto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            if (product == null)
            {
                throw new ProductNotFoundException(productId);
            }

            try
            {
                _mapper.Map(productUpdateDto, product);
                product.ImageUrl = productUpdateDto.ImgUrl ?? string.Empty;
                product.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.Products.UpdateAsync(product);
                await _unitOfWork.CommitAsync();

                var updatedProduct = await _unitOfWork.Products.GetProductByIdWithDetailsAsync(product.ProductId);
                if (updatedProduct == null)
                {
                    throw new ProductUpdateFailedException(productId, "Product was not found after update");
                }
                return _mapper.Map<ProductDTO>(updatedProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product. ProductId: {ProductId}", productId);
                throw new ProductUpdateFailedException(productId, ex.Message);
            }
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            try
            {
                var result = await _unitOfWork.Products.DeleteAsync(productId);
                if (!result)
                {
                    throw new ProductNotFoundException(productId);
                }
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product. ProductId: {ProductId}", productId);
                throw new ProductDeletionFailedException(productId);
            }
        }

        public async Task<ProductDTO?> GetProductByIdAsync(int productId)
        {
            var product = await _unitOfWork.Products.GetProductByIdWithDetailsAsync(productId);
            if (product == null)
            {
                throw new ProductNotFoundException(productId);
            }
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<IEnumerable<ProductDTO?>> GetProductsByCategoryIdAsync(int categoryId, int page = 1, int pageSize = 10)
        {
            var products = await _unitOfWork.Products.GetProductsByCategoryIdWithDetailsAsync(categoryId, page, pageSize);
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }
        public async Task<IEnumerable<ProductDTO?>> GetProductsByGenreIdAsync(int genreId, int page = 1, int pageSize = 10)
        {
            var products = await _unitOfWork.Products.GetProductsByGenreIdWithDetailsAsync(genreId, page, pageSize);
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }


        public async Task<IEnumerable<ProductDTO?>> GetProductWithDetailsAsync(int page = 1, int pageSize = 10)
        {
            var products = await _unitOfWork.Products.GetProductWithDetailsAsync(page, pageSize);
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }


        public async Task<IEnumerable<ProductDTO>> SearchProductsByNameAsync(string searchTerm)
        {
            var products = await _unitOfWork.Products.SearchProductsByNameAsync(searchTerm);
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<IEnumerable<ProductDTO>> FilterAndSortProductsAsync(
        string sortOrder = "asc",
        string? genreName = null,
        int page = 1,
        int pageSize = 10,
        string? categoryName = null)
        {
            var products = await _unitOfWork.Products.FilterAndSortProductsAsync(sortOrder, genreName, page, pageSize, categoryName);
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }
    }
}
