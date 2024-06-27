using Microsoft.EntityFrameworkCore;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;
using Project_Keystone.Infrastructure.Repositories.Interfaces;

namespace Project_Keystone.Infrastructure.Repositories
{
    public class WishlistRepository : BaseRepository<Wishlist>, IWishListRepository
    {
        public WishlistRepository(ProjectKeystoneDbContext context) : base(context)
        {
        }

        public async Task<Wishlist?> GetWishlistByUserIdAsync(string userId)
        {
            return await _context.Wishlist
            .Include(w => w.WishListItems)
             .ThenInclude(wi => wi.Product)
            .FirstOrDefaultAsync(w => w.UserId == userId);
        }

        public async Task<Wishlist> CreateWishlistAsync(string userId)
        {
            var wishlist = new Wishlist { UserId = userId };
            _context.Wishlist.Add(wishlist);
            await _context.SaveChangesAsync();
            return wishlist;
        }

        public async Task<bool> AddItemToWishlistAsync(int wishlistId, int productId)
        {
            var wishlistItem = new WishlistItem
            {
                WishlistId = wishlistId,
                ProductId = productId
            };
            await _context.WishlistItem.AddAsync(wishlistItem);
            return true;
        }

        public async Task<bool> RemoveItemFromWishlistAsync(int wishlistId, int productId)
        {
            var item = await _context.WishlistItem
            .FirstOrDefaultAsync(wi => wi.WishlistId == wishlistId && wi.ProductId == productId);
            if (item != null)
            {
                _context.WishlistItem.Remove(item);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<WishlistItem>> GetWishlistItemsAsync(int wishlistId)
        {
            return await _context.WishlistItem
                .Where(wi => wi.WishlistId == wishlistId)
                .Include(wi => wi.Product)
                .ToListAsync();
        }
    }
}
