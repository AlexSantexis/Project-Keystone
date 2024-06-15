using Project_Keystone.Core.Entities;

namespace Project_Keystone.Core.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<IEnumerable<Order?>> GetOrdersByUserId(int userId);
        Task<Order?> GetOrderWithDetailsAsync(int orderId);
    }
}
