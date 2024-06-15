using Project_Keystone.Core.Entities;

namespace Project_Keystone.Core.Interfaces
{
    public interface IGenreRepository : IBaseRepository<Genre>
    {
        Task<Genre?> GetGenreByNameAsync(string name);
    }
}
