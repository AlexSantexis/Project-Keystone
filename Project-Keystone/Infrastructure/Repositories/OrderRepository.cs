using Microsoft.EntityFrameworkCore;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;
using Project_Keystone.Infrastructure.Repositories.Interfaces;

namespace Project_Keystone.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ProjectKeystoneDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserEmailAsync(string email)
        {
            return await _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .Where(o => o.User.Email == email)
            .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersWithDetailsByUserIdAsync(string userId)
        {
             return await _context.Orders
            .Include(o => o.OrderDetails)
             .ThenInclude(od => od.Product)
            .Where(o => o.UserId == userId)
            .ToListAsync();
        }

        public async Task<Order?> GetOrderWithDetailsAsync(int orderId)
        {
            return await _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }
    }
}
