using Project_Keystone.Core.Entities;

namespace Project_Keystone.Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserEmailAsync(string email);
        Task<Order?> GetOrderWithDetailsAsync(int orderId);

        Task<IEnumerable<Order>> GetOrdersWithDetailsByUserIdAsync(string userId);
    }
}
