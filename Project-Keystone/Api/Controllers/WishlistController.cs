using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        private string? GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public WishlistController(IWishlistService wishlistService, ILogger<WishlistController> logger)
        {
            _wishlistService = wishlistService ?? throw new ArgumentNullException(nameof(wishlistService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<WishlistDTO>> GetWishlist()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in the token");
            }

            var wishlist = await _wishlistService.GetWishlistItemsAsync(userId);
            return Ok(wishlist);
        }

        [HttpPost("add/{productId}")]
        [Authorize]
        public async Task<ActionResult> AddToWishlist(int productId)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in the token");
            }

            await _wishlistService.AddToWishlistAsync(userId, productId);
            return Ok(new { Message = "Item added to wishlist successfully" });
        }

        [HttpDelete("remove/{productId}")]
        [Authorize]
        public async Task<ActionResult> RemoveFromWishlist(int productId)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in the token");
            }

            await _wishlistService.RemoveFromWishlistAsync(userId, productId);
            return Ok(new { Message = "Item removed from wishlist successfully" });
        }
    }
    
}