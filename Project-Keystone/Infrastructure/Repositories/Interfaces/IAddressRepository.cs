using Project_Keystone.Core.Entities;

namespace Project_Keystone.Infrastructure.Repositories.Interfaces
{
    public interface IAddressRepository : IBaseRepository<Address>
    {
        Task<Address?> GetAddressByUserIdAsync(string userId);
    }
}
