using Project_Keystone.Api.Models.DTOs.Wishlist;
using Project_Keystone.Core.Entities;

namespace Project_Keystone.Core.Services.Interfaces
{
    public interface IWishlistService
    {
        Task<WishlistDTO> GetUserWishlistAsync(string userId);
        Task<bool> AddToWishlistAsync(string userId, int productId);
        Task<bool> RemoveFromWishlistAsync(string userId, int productId);
        Task<WishlistDTO> GetWishlistItemsAsync(string userId);
    }
}
