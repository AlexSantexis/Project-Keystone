using Microsoft.EntityFrameworkCore;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;
using Project_Keystone.Infrastructure.Repositories.Interfaces;

namespace Project_Keystone.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ProjectKeystoneDbContext context) : base(context)
        {

        }

        public async Task DeleteUserRelatedEntitiesAsync(string userId)
        {
            
                // Delete Wishlist items
                var wishlistItems = await _context.WishlistItem
                    .Where(wi => wi.Wishlist.UserId == userId)
                    .ToListAsync();
                _context.WishlistItem.RemoveRange(wishlistItems);

                // Delete Wishlist
                var wishlist = await _context.Wishlist
                    .FirstOrDefaultAsync(w => w.UserId == userId);
                if (wishlist != null)
                {
                    _context.Wishlist.Remove(wishlist);
                }

                // Delete Basket items
                var basketItems = await _context.BasketItems
                    .Where(bi => bi.Basket.UserId == userId)
                    .ToListAsync();
                _context.BasketItems.RemoveRange(basketItems);

                // Delete Basket
                var basket = await _context.Baskets
                    .FirstOrDefaultAsync(b => b.UserId == userId);
                if (basket != null)
                {
                    _context.Baskets.Remove(basket);
                }

                // Delete Order details
                var orderDetails = await _context.OrderDetails
                    .Where(od => od.Order.UserId == userId)
                    .ToListAsync();
                _context.OrderDetails.RemoveRange(orderDetails);

                // Delete Orders
                var orders = await _context.Orders
                    .Where(o => o.UserId == userId)
                    .ToListAsync();
                _context.Orders.RemoveRange(orders);

                // Delete Address
                var address = await _context.Addresses
                    .FirstOrDefaultAsync(a => a.UserId == userId);
                if (address != null)
                {
                    _context.Addresses.Remove(address);
                }

        }
        public async Task<IEnumerable<User>> GetAllUsersWithDetailsAsync()
        {
            return await _context.Users
                .Include(u => u.Address)
                .ToListAsync();
        }

        public async Task<User?> GetUserByIdWithDetailsAsync(string userId)
        {
            return await _context.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
