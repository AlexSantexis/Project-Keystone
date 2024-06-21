using Microsoft.EntityFrameworkCore;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;
using Project_Keystone.Core.Interfaces;

namespace Project_Keystone.Infrastructure.Repositories
{
    public class BasketRepository : BaseRepository<Basket>, IBasketRepository
    {
        
        public BasketRepository(ProjectKeystoneDbContext context) : base(context)
        {
            
        }

        public async Task AddItemToBasketAsync(int basketId, BasketItem item)
        {
            var basketItem = await _context.Baskets
                .Include(b => b.BasketItems)
                .FirstOrDefaultAsync(b => b.BasketId == basketId);
            if(basketItem is not null)
            {
               basketItem.BasketItems.Add(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Basket?> GetBasketByUserIdAsync(string userId)
        {
            return await _context.Baskets
                 .Include(b => b.BasketItems)
                 .ThenInclude(bi => bi.Product)
                 .FirstOrDefaultAsync(b => b.UserId == userId);
        }

        public async Task RemoveItemFromBasketAsync(int basketId, int itemId)
        {
            var basket = await _context.Baskets
                .Include(b => b.BasketItems)
                .FirstOrDefaultAsync(b => b.BasketId == basketId);

            if (basket is not null)
            {
                var item = basket.BasketItems.FirstOrDefault(bi => bi.BasketItemId == itemId);
                if(item is not null)
                {
                    basket.BasketItems.Remove(item);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}

