using Project_Keystone.Api.Models.DTOs.OrderDTOs;

namespace Project_Keystone.Core.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetOrdersByUserEmailAsync(string email);
        Task<OrderDTO> CreateOrderAsync(CreateOrderDTO createOrderDTO);
        Task<bool> DeleteOrderAsync(int orderId);
        Task<OrderDTO> GetOrderByIdAsync(int orderId);
    }
}
