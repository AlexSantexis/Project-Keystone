using Project_Keystone.Core.Entities;

namespace Project_Keystone.Infrastructure.Repositories.Interfaces
{
    public interface IWishListRepository : IBaseRepository<Wishlist>
    {
        Task<Wishlist?> GetWishlistByUserIdAsync(string userId);
        Task AddItemToWishlistAsync(int wishlistId, WishlistItem item);

        Task RemoveItemFromWishlistAsync(int wishlistId, int itemId);
    }
}
