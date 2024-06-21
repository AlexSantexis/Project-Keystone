using Microsoft.EntityFrameworkCore;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;
using Project_Keystone.Core.Interfaces;

namespace Project_Keystone.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        

        public ProductRepository(ProjectKeystoneDbContext context) : base(context) { }

        

        public async Task<IEnumerable<Product>> FilterProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
           return await _context.Products
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByGenreIdAsync(int genreId)
        {
           return await _context.ProductGenres
                .Where(pg => pg.GenreId == genreId)
                .Select(pg => pg.product)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductWithDetailsAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
        {
            return await _context.Products
                .Where(p => p.Name.Contains(searchTerm) || p.Description!.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<Product?> UpdateProductAsync(int productId, Product updatedProduct)
        {
            var product = await _context.Products.Where(p => p.ProductId == productId).FirstOrDefaultAsync();
            if (product is null || product.ProductId != productId) return null;
            _context.Products.Attach(updatedProduct);
            _context.Entry(updatedProduct).State = EntityState.Modified;
            return product;
        }

        public async Task<bool> UpdateProductImageAsync(int productId, byte[] imageData)
        {
           var product = await _context.Products.FindAsync(productId);
            if (product == null) return false;
            product.ImageData = imageData;
            await _context.SaveChangesAsync();
            return true;
        }
    }
        


      
}
