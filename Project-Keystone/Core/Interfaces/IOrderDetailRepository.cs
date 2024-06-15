using Project_Keystone.Core.Entities;

namespace Project_Keystone.Core.Interfaces
{
    public interface IOrderDetailRepository : IBaseRepository<OrderDetail>
    {
        Task<IEnumerable<OrderDetail?>> GetOrderDetailsByOrderId(int orderId);
    }
}
