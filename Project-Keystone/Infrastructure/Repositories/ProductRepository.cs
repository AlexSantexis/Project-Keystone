using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;
using Project_Keystone.Infrastructure.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Project_Keystone.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        

        public ProductRepository(ProjectKeystoneDbContext context) : base(context) { }

        public async Task<Product?> GetProductByIdWithDetailsAsync(int productId)
        {
            return await _context.Products
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.ProductGenres)
                    .ThenInclude(pg => pg.Genre)
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<IEnumerable<Product?>> GetProductsByGenreIdWithDetailsAsync(int genreId, int page = 1, int pageSize = 10)
        {
            return await _context.ProductGenres
                 .Where(pg => pg.GenreId == genreId)
                 .Include(pg => pg.Product)
                     .ThenInclude(p => p.ProductGenres)
                         .ThenInclude(pg => pg.Genre)
                 .Include(pg => pg.Product)
                     .ThenInclude(p => p.ProductCategories)
                         .ThenInclude(pc => pc.Category)
                 .Select(pg => pg.Product)
                 .OrderBy(p => p.Name)
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize)
                 .ToListAsync();
        }

        public async Task<IEnumerable<Product?>> GetProductsByCategoryIdWithDetailsAsync(int categoryId, int page = 1, int pageSize = 10)
        {
            return await _context.ProductCategories
               .Where(pc => pc.CategoryId == categoryId)
               .Include(pc => pc.Product)
                   .ThenInclude(p => p.ProductCategories)
                       .ThenInclude(pc => pc.Category)
               .Include(pc => pc.Product)
                   .ThenInclude(p => p.ProductGenres)
                       .ThenInclude(pg => pg.Genre)
               .Select(pc => pc.Product)
               .OrderBy(p => p.Name)
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
        }


        public async Task<IEnumerable<Product>> GetProductWithDetailsAsync(int page = 1, int pageSize = 10)
        {
            return await _context.Products
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.ProductGenres)
                    .ThenInclude(pg => pg.Genre)
                .OrderBy(p => p.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchProductsByNameAsync(string? searchTerm)
        {
            searchTerm = searchTerm!.ToLower();
            return await _context.Products
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.ProductGenres)
                    .ThenInclude(pg => pg.Genre)
                .Where(p => p.Name.ToLower().Contains(searchTerm))
                .ToListAsync();
        }


        public async Task<IEnumerable<Product>> FilterAndSortProductsAsync(
    string sortOrder = "asc",
    string? genreName = null,
    int page = 1,
    int pageSize = 10,
    string? categoryName = null)
        {
            var query = _context.Products
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.ProductGenres)
                    .ThenInclude(pg => pg.Genre)
                .AsQueryable();

            // Filter by category
            if (!string.IsNullOrEmpty(categoryName))
            {
                categoryName = categoryName.ToLower();
                query = query.Where(p => p.ProductCategories.Any(pc => pc.Category.Name.ToLower() == categoryName));
            }

            // Filter by genre
            if (!string.IsNullOrEmpty(genreName))
            {
                genreName = genreName.ToLower();
                query = query.Where(p => p.ProductGenres.Any(pg => pg.Genre.Name.ToLower() == genreName));
            }

            // Sort by price
            query = sortOrder.ToLower() == "desc"
                ? query.OrderByDescending(p => p.Price)
                : query.OrderBy(p => p.Price);

            // Log the generated SQL query for debugging
            var sqlQuery = query.ToQueryString();
            Console.WriteLine("Generated SQL Query: " + sqlQuery);

            // Apply pagination
            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return products;
        }


        public async Task<Product?> UpdateProductAsync(int productId, Product updatedProduct)
        {
            if (productId != updatedProduct.ProductId)
            {
                return null;
            }

            var existingProduct = await _context.Products.FindAsync(productId);
            if (existingProduct == null)
            {
                return null;
            }

            _context.Entry(existingProduct).CurrentValues.SetValues(updatedProduct);

            return existingProduct;
        }

        
    }
}

 
        


      

