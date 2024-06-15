using Project_Keystone.Core.Entities;

namespace Project_Keystone.Core.Interfaces
{
    public interface IProductGenreRepository : IBaseRepository<ProductGenre>
    {
        Task<IEnumerable<ProductGenre?>> GetGenresByProductIdAsync(int productId);
        Task<IEnumerable<ProductGenre?>> GetGenresByGenreIdAsync(int genreId);
    }
}
