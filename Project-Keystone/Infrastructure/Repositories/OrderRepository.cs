using Microsoft.EntityFrameworkCore;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;
using Project_Keystone.Core.Interfaces;

namespace Project_Keystone.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ProjectKeystoneDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order?>> GetOrdersByUserId(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ToListAsync();
                
        }

        public async Task<Order?> GetOrderWithDetailsAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }
    }
}
