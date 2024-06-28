using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Project_Keystone.Api.Exceptions.OrdersExceptions;
using Project_Keystone.Api.Models.DTOs.OrderDTOs;
using Project_Keystone.Core.Entities;
using Project_Keystone.Core.Services.Interfaces;
using Project_Keystone.Infrastructure.Repositories.Interfaces;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<OrderService> _logger;
    private readonly UserManager<User> _userManager;

    public OrderService(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper, ILogger<OrderService> logger)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<OrderDTO> CreateOrderAsync(CreateOrderDTO createOrderDTO)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(createOrderDTO.UserEmail);
            if (user == null)
            {
                throw new NotFoundException($"User with email {createOrderDTO.UserEmail} not found");
            }

            var basket = await _unitOfWork.Baskets.GetBasketByUserIdAsync(user.Id);
            if (basket == null || !basket.BasketItems.Any())
            {
                throw new InvalidOperationException("User's basket is empty");
            }

            var order = new Order
            {
                UserId = user.Id,
                OrderDate = DateTime.UtcNow,
                StreetAddress = createOrderDTO.StreetAddress,
                City = createOrderDTO.City,
                ZipCode = createOrderDTO.ZipCode,
                Country = createOrderDTO.Country,
                OrderDetails = basket.BasketItems.Select(bi => new OrderDetail
                {
                    ProductId = bi.ProductId,
                    Quantity = bi.Quantity,
                    Price = bi.Product.Price,
                    Total = bi.Quantity * bi.Product.Price
                }).ToList()
            };

            order.TotalAmount = order.OrderDetails.Sum(od => od.Total);

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.Baskets.ClearBasketAsync(basket.BasketId);
            await _unitOfWork.CommitAsync();

            var orderDTO = _mapper.Map<OrderDTO>(order);
            orderDTO.UserEmail = user.Email!;
            return orderDTO;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating order for user {Email}", createOrderDTO.UserEmail);
            throw new OrderOperationException("Failed to create order", ex);
        }
    }

    public async Task<bool> DeleteOrderAsync(int orderId)
    {
        try
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null)
            {
                return false;
            }

            await _unitOfWork.Orders.DeleteAsync(orderId);
            await _unitOfWork.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting order {OrderId}", orderId);
            throw new OrderOperationException("Failed to delete order", ex);
        }
    }

    public async Task<OrderDTO> GetOrderByIdAsync(int orderId)
    {
        try
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new NotFoundException($"Order with ID {orderId} not found");
            }

            var orderDTO = _mapper.Map<OrderDTO>(order);
            orderDTO.UserEmail = (await _userManager.FindByIdAsync(order.UserId))?.Email;

            return orderDTO;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting order {OrderId}", orderId);
            throw new OrderOperationException("Failed to retrieve order", ex);
        }
    }

    public async Task<IEnumerable<OrderDTO>> GetOrdersByUserEmailAsync(string email)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new NotFoundException($"User with email {email} not found");
            }

            var orders = await _unitOfWork.Orders.GetOrdersWithDetailsByUserIdAsync(user.Id);
            var orderDTOs = _mapper.Map<IEnumerable<OrderDTO>>(orders);

            foreach (var orderDTO in orderDTOs)
            {
                orderDTO.UserEmail = email;
            }

            return orderDTOs;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting orders for user {Email}", email);
            throw new OrderOperationException("Failed to retrieve orders", ex);
        }
    }
}