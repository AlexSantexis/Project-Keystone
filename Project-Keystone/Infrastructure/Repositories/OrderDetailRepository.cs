using Microsoft.EntityFrameworkCore;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;
using Project_Keystone.Core.Interfaces;

namespace Project_Keystone.Infrastructure.Repositories
{
    public class OrderDetailRepository : BaseRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(ProjectKeystoneDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<OrderDetail?>> GetOrderDetailsByOrderId(int orderId)
        {
            return await _context.OrderDetails
                .Where(od => od.OrderId == orderId)
                .Include(od => od.Product)
                .ToListAsync();
        }
    }
}
