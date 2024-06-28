using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project_Keystone.Api.Exceptions.AuthExceptions;
using Project_Keystone.Api.Exceptions.ProductExceptions;
using Project_Keystone.Api.Exceptions.WishlistExceptions;
using Project_Keystone.Api.Models.DTOs.Wishlist;
using Project_Keystone.Core.Services.Interfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project_Keystone.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        private readonly ILogger<WishlistController> _logger;

      
        public WishlistController(IWishlistService wishlistService, ILogger<WishlistController> logger)
        {
            _wishlistService = wishlistService ?? throw new ArgumentNullException(nameof(wishlistService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private string GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User ID not found in token");
            }
            return userId;
        }
        //private string? GetUserId()
        //{
        //    return User.FindFirstValue(ClaimTypes.NameIdentifier);
        //}

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<WishlistDTO>> GetWishlist()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User ID not found in the token" });
            }

            try
            {
                var wishlist = await _wishlistService.GetWishlistItemsAsync(userId);
                return Ok(wishlist);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (WishlistNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving wishlist for user {UserId}", userId);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpPost("add/{productId}")]
        [Authorize]
        public async Task<ActionResult> AddToWishlist(int productId)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User ID not found in the token" });
            }

            try
            {
                await _wishlistService.AddToWishlistAsync(userId, productId);
                return Ok(new { message = "Item added to wishlist successfully" });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (WishlistItemAlreadyExistsException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (WishlistOperationFailedException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding product {ProductId} to wishlist for user {UserId}", productId, userId);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        [HttpDelete("remove/{productId}")]
        [Authorize]
        public async Task<ActionResult> RemoveFromWishlist(int productId)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User ID not found in the token" });
            }

            try
            {
                await _wishlistService.RemoveFromWishlistAsync(userId, productId);
                return Ok(new { message = "Item removed from wishlist successfully" });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (WishlistNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (WishlistItemNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (WishlistOperationFailedException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing product {ProductId} from wishlist for user {UserId}", productId, userId);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }
    }
}
    
    
