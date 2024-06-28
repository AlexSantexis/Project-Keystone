using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Keystone.Api.Exceptions.ProductExceptions;
using Project_Keystone.Api.Models.DTOs.ProductDTOs;
using Project_Keystone.Core.Services.Interfaces;


namespace Project_Keystone.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductDTO>> CreateProduct([FromForm] ProductCreateDTO productCreateDto)
        {
            var createdProduct = await _productService.CreateProductAsync(productCreateDto);
            _logger.LogInformation("Product created successfully.");
            return CreatedAtAction(nameof(GetProductById), new { productId = createdProduct.ProductId }, createdProduct);
        }

        [HttpPost("update/{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductDTO>> UpdateProduct(int productId, [FromBody] ProductUpdateDTO productUpdateDto)
        {
            var updatedProduct = await _productService.UpdateProductAsync(productId, productUpdateDto);
            _logger.LogInformation("Product updated successfully. ProductId: {ProductId}", productId);
            return Ok(updatedProduct);
        }

        [HttpDelete("{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            await _productService.DeleteProductAsync(productId);
            _logger.LogInformation("Product deleted successfully.");
            return NoContent();
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound("No product found with the specified id");
            }
            return Ok(product);
        }



        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByCategoryId(int categoryId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var products = await _productService.GetProductsByCategoryIdAsync(categoryId, page, pageSize);
            if (!products.Any())
            {
                return NotFound("No products found for the specified category.");
            }
            return Ok(products);
        }

        [HttpGet("genre/{genreId}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByGenreId(int genreId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var products = await _productService.GetProductsByGenreIdAsync(genreId, page, pageSize);
            if (!products.Any())
            {
                return NotFound("No products found for the specified genre.");
            }
            return Ok(products);
        }

        [HttpGet("details")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductWithDetails([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var products = await _productService.GetProductWithDetailsAsync(page, pageSize);
            if (!products.Any())
            {
                return NotFound("No products found.");
            }
            return Ok(products);
        }


        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> SearchProductsByName([FromQuery] string searchTerm)
        {
            var products = await _productService.SearchProductsByNameAsync(searchTerm);
            if (!products.Any())
            {
                return NotFound("No products found matching the search term.");
            }
            return Ok(products);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> FilterAndSortProducts(
            [FromQuery] string sortOrder = "asc",
            [FromQuery] string? genreName = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? categoryName = null)
        {
            var products = await _productService.FilterAndSortProductsAsync(sortOrder, genreName, page, pageSize, categoryName);
            if (!products.Any())
            {
                return NotFound("No products found matching the criteria.");
            }
            return Ok(products);
        }


    }
}
