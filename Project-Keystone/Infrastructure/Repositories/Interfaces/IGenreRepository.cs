using Project_Keystone.Core.Entities;

namespace Project_Keystone.Infrastructure.Repositories.Interfaces
{
    public interface IGenreRepository : IBaseRepository<Genre>
    {
        Task<Genre?> GetGenreByNameAsync(string name);
        Task<List<Genre>> GetByIdsAsync(IEnumerable<int> ids);
        Task<List<Genre>> GetMostPopularGenresAsync(int count);
        Task<bool> GenreExistsAsync(string name);
        Task<List<Genre>> SearchGenresAsync(string searchTerm);


    }
}
