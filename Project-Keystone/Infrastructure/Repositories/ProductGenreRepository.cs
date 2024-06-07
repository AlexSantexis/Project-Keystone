using Microsoft.EntityFrameworkCore;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;
using Project_Keystone.Infrastructure.Repositories.Interfaces;

namespace Project_Keystone.Infrastructure.Repositories
{
    public class ProductGenreRepository : BaseRepository<ProductGenre>, IProductGenreRepository
    {
        public ProductGenreRepository(ProjectKeystoneDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProductGenre?>> GetGenresByGenreIdAsync(int genreId)
        {
            return await _context.ProductGenres
                .Where(pg => pg.GenreId == genreId)
                .Include(pg => pg.product)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductGenre?>> GetGenresByProductIdAsync(int productId)
        {
            return await _context.ProductGenres
                .Where(pg => pg.ProductId == productId)
                .Include(pg => pg.product)
                .ToListAsync();

        }
    }
}
