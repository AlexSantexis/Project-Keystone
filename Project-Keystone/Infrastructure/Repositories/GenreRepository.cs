using Microsoft.EntityFrameworkCore;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;
using Project_Keystone.Infrastructure.Repositories.Interfaces;

namespace Project_Keystone.Infrastructure.Repositories
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(ProjectKeystoneDbContext context) : base(context)
        {
        }


        public async Task<Genre?> GetGenreByNameAsync(string name)
        {
            return await _context.Genres.FirstOrDefaultAsync(g => g.Name == name);
        }

        public async Task<List<Genre>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.Genres.Where(g => ids.Contains(g.GenreId)).ToListAsync();
        }

        public async Task<List<Genre>> GetMostPopularGenresAsync(int count)
        {
            return await _context.Genres
                .OrderByDescending(g => g.ProductGenres.Count)
                .Take(count)
                .ToListAsync();
        }

        public async Task<bool> GenreExistsAsync(string name)
        {
            return await _context.Genres.AnyAsync(g => g.Name.ToLower() == name.ToLower());
        }

        public async Task<List<Genre>> SearchGenresAsync(string searchTerm)
        {
            return await _context.Genres
                .Where(g => g.Name.Contains(searchTerm))
                .ToListAsync();
        }

    }
}
