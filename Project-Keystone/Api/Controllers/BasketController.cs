using Microsoft.AspNetCore.Mvc;
using Project_Keystone.Api.Exceptions.BasketExceptions;
using Project_Keystone.Api.Exceptions.ProductExceptions;
using Project_Keystone.Api.Models.DTOs.BasketDTOs;
using Project_Keystone.Core.Services.Interfaces;
using System.Security.Claims;

namespace Project_Keystone.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;


    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IBasketService basketService, ILogger<BasketController> logger)
        {
            _basketService = basketService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<BasketDTO>> GetBasket()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            try
            {
                var basket = await _basketService.GetBasketAsync(userId);
                return Ok(basket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting basket for user {UserId}", userId);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("items/add")]
        [Authorize]
        public async Task<ActionResult<BasketDTO>> AddToBasket([FromBody] AddToBasketDTO addToBasketDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            try
            {
                var updatedBasket = await _basketService.AddToBasketAsync(userId, addToBasketDto);
                return Ok(updatedBasket);
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding item to basket for user {UserId}", userId);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("items/update")]
        [Authorize]
        public async Task<ActionResult<BasketDTO>> UpdateBasketItem([FromBody] UpdateBasketItemDTO updateBasketItemDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            try
            {
                var updatedBasket = await _basketService.UpdateBasketItemAsync(userId, updateBasketItemDto);
                return Ok(updatedBasket);
            }
            catch (BasketNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BasketItemNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating basket item for user {UserId}", userId);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("items/{basketItemId}")]
        [Authorize]
        public async Task<ActionResult<BasketDTO>> RemoveFromBasket(int basketItemId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            try
            {
                var updatedBasket = await _basketService.RemoveFromBasketAsync(userId, basketItemId);
                return Ok(updatedBasket);
            }
            catch (BasketNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BasketItemNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while removing item from basket for user {UserId}", userId);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<BasketDTO>> ClearBasket()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            try
            {
                var updatedBasket = await _basketService.ClearBasketAsync(userId);
                return Ok(updatedBasket);
            }
            catch (BasketNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while clearing basket for user {UserId}", userId);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
