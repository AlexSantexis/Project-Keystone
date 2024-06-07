using Project_Keystone.Core.Entities;

namespace Project_Keystone.Infrastructure.Repositories.Interfaces
{
    public interface IGenreRepository : IBaseRepository<Genre>
    {
        Task<Genre?> GetGenreByNameAsync(string name);
    }
}
