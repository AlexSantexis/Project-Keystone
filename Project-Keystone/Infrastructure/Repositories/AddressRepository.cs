using Microsoft.EntityFrameworkCore;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;
using Project_Keystone.Infrastructure.Repositories.Interfaces;

namespace Project_Keystone.Infrastructure.Repositories
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(ProjectKeystoneDbContext context) : base(context)
        {
        }

        public async Task<Address?> GetAddressByUserIdAsync(string userId)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.UserId == userId);
        }
        
    }
}
