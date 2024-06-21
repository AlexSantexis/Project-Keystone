using Microsoft.EntityFrameworkCore;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;
using Project_Keystone.Core.Interfaces;

namespace Project_Keystone.Infrastructure.Repositories
{
    public class WishlistRepository : BaseRepository<Wishlist>, IWishListRepository
    {
        public WishlistRepository(ProjectKeystoneDbContext context) : base(context)
        {
        }

        public async Task AddItemToWishlistAsync(int wishlistId, WishlistItem item)
        {
            var wishlist = await _context.Wishlist
                .Include(w => w.WishListItems)
                .FirstOrDefaultAsync(w => w.WishlistId == wishlistId);
            if(wishlist is not null)
            {
                wishlist.WishListItems.Add(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Wishlist?> GetWishlistByUserIdAsync(string userId)
        {
            return await _context.Wishlist
                .Include(w => w.WishListItems)
                .ThenInclude(wi => wi.Product)
                .FirstOrDefaultAsync(w => w.UserId == userId);
        }

        public Task RemoveItemFromWishlistAsync(int wishlistId, int itemId)
        {
            var wishlist = _context.Wishlist
                .Include(w => w.WishListItems)
                .FirstOrDefault(w => w.WishlistId == wishlistId);
            if(wishlist is not null)
            {
                var item = wishlist.WishListItems.FirstOrDefault(wi => wi.WishlistItemId == itemId);
                if(item is not null)
                {
                    wishlist.WishListItems.Remove(item);
                    _context.SaveChanges();
                }
            }
            return Task.FromResult(false);
        }
    }
}
