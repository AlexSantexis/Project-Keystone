using Microsoft.EntityFrameworkCore;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;
using Project_Keystone.Infrastructure.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Project_Keystone.Tests
{
    public class ProductRepositoryTests
    {
        private readonly ProductRepository _productRepository;
        private readonly ProjectKeystoneDbContext _context;

        public ProductRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ProjectKeystoneDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ProjectKeystoneDbContext(options);

            _productRepository = new ProductRepository(_context);
            ClearDatabase();
        }

        [Fact]
        public void ClearDatabase()
        {
            _context.Products.RemoveRange(_context.Products);
            _context.SaveChanges();
        }

        [Fact]
        public async Task AddProductAsync_ShouldAddProduct()
        {
            var product = new Product
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 100,
                ImageUrl = "http://example.com/image.png",
                CategoryId = 1
            };

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            var result = await _productRepository.GetByIdAsync(product.ProductId);

            Assert.NotNull(result);
            Assert.Equal("Test Product", result.Name);
            Assert.Equal("Test Description", result.Description);
            Assert.Equal(100, result.Price);
            Assert.Equal("http://example.com/image.png", result.ImageUrl);
            Assert.Equal(1, result.CategoryId);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnProduct()
        {
            var product = new Product
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 100,
                ImageUrl = "http://example.com/image.png",
                CategoryId = 1
            };
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            var result = await _productRepository.GetByIdAsync(product.ProductId);

            Assert.NotNull(result);
            Assert.Equal("Test Product", result.Name);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldUpdateProduct()
        {
            var product = new Product
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 100,
                ImageUrl = "http://example.com/image.png",
                CategoryId = 1
            };
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            product.Name = "Updated Product";
            product.Description = "Updated Description";
            product.Price = 200;
            product.ImageUrl = "http://example.com/updated_image.png";
            product.CategoryId = 2;

            _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();

            var result = await _productRepository.GetByIdAsync(product.ProductId);

            Assert.NotNull(result);
            Assert.Equal("Updated Product", result.Name);
            Assert.Equal("Updated Description", result.Description);
            Assert.Equal(200, result.Price);
            Assert.Equal("http://example.com/updated_image.png", result.ImageUrl);
            Assert.Equal(2, result.CategoryId);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldDeleteProduct()
        {
            ClearDatabase();

            var product = new Product
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 100,
                ImageUrl = "http://example.com/image.png",
                CategoryId = 1
            };
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            var result = await _productRepository.DeleteAsync(product.ProductId);
            await _productRepository.SaveChangesAsync();

            Assert.True(result);
            var deletedProduct = await _productRepository.GetByIdAsync(product.ProductId);
            Assert.Null(deletedProduct);
        }

        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            ClearDatabase();
            var product1 = new Product
            {
                Name = "Test Product 1",
                Description = "Test Description 1",
                Price = 100,
                ImageUrl = "http://example.com/image1.png",
                CategoryId = 1
            };
            var product2 = new Product
            {
                Name = "Test Product 2",
                Description = "Test Description 2",
                Price = 200,
                ImageUrl = "http://example.com/image2.png",
                CategoryId = 2
            };

            await _productRepository.AddAsync(product1);
            await _productRepository.AddAsync(product2);
            await _productRepository.SaveChangesAsync();

            var result = await _productRepository.GetAllAsync();

            Assert.Equal(2, result.Count());
        }
    }
}


