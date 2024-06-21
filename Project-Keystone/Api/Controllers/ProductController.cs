using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Keystone.Api.Models.DTOs.ProductDTOs;
using Project_Keystone.Core.Entities;
using Project_Keystone.Core.Services;
using Project_Keystone.Core.Services.Interfaces;
using Project_Keystone.Infrastructure.Data;

namespace Project_Keystone.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductCreateDTO productDTO)
        {
            if(!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid product creation request");
                return BadRequest(ModelState);
            }
            var result = await _productService.AddProductsAsync(productDTO);
            if (result)
            {
                _logger.LogInformation("Product added successfully.");
                return Ok("Product added successfully.");
            }

            _logger.LogError("Error adding product.");
            return StatusCode(500, "Error adding product.");
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            return await _productService.GetAllProductsAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found.", id);
                return NotFound("Product not found.");
            }

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductUpdateDTO productUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid product update request.");
                return BadRequest(ModelState);
            }

            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found.", productId);
                return NotFound("Product not found.");
            }

            

            var result = await _productService.UpdateProductAsync(productId,productUpdateDto);
            if (result)
            {
                _logger.LogInformation("Product updated successfully.");
                return Ok("Product updated successfully.");
            }

            _logger.LogError("Error updating product.");
            return StatusCode(500, "Error updating product.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (result)
            {
                _logger.LogInformation("Product deleted successfully.");
                return Ok("Product deleted successfully.");
            }

            _logger.LogWarning("Product with ID {ProductId} not found.", id);
            return StatusCode(500, "Error deleting product.");
        }

    }
}
