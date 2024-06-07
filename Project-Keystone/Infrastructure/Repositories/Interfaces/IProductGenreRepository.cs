using Project_Keystone.Core.Entities;

namespace Project_Keystone.Infrastructure.Repositories.Interfaces
{
    public interface IProductGenreRepository : IBaseRepository<ProductGenre>
    {
        Task<IEnumerable<ProductGenre?>> GetGenresByProductIdAsync(int productId);
        Task<IEnumerable<ProductGenre?>> GetGenresByGenreIdAsync(int genreId);
    }
}
