using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Keystone.Api.Models.DTOs.OrderDTOs;
using Project_Keystone.Core.Services.Interfaces;
using System.Security.Claims;

namespace Project_Keystone.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("user/{email}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByUserEmail(string email)
        {
            
            if (email != User.FindFirstValue(ClaimTypes.Email))
            {
                return Forbid();
            }
            var orders = await _orderService.GetOrdersByUserEmailAsync(email);
            return Ok(orders);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderDTO>> CreateOrder(CreateOrderDTO createOrderDTO)
        {
            
            if (createOrderDTO.UserEmail != User.FindFirstValue(ClaimTypes.Email))
            {
                return Forbid();
            }
            var order = await _orderService.CreateOrderAsync(createOrderDTO);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, order);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrderAsync(id);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<OrderDTO>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order.UserEmail != User.FindFirstValue(ClaimTypes.Email))
            {
                return Forbid();
            }
            return Ok(order);
        }
    }
}
