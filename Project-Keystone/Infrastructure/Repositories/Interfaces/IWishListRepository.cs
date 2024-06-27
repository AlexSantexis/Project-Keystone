using Project_Keystone.Core.Entities;

namespace Project_Keystone.Infrastructure.Repositories.Interfaces
{
    public interface IWishListRepository : IBaseRepository<Wishlist>
    {
        Task<Wishlist?> GetWishlistByUserIdAsync(string userId);
        Task<bool> AddItemToWishlistAsync(int wishlistId, int productId);
        Task<bool> RemoveItemFromWishlistAsync(int wishlistId, int productId);
        Task<IEnumerable<WishlistItem>> GetWishlistItemsAsync(int wishlistId);

    }
}
