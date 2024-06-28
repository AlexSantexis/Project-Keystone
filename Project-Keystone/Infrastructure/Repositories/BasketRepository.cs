using Microsoft.EntityFrameworkCore;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;
using Project_Keystone.Infrastructure.Repositories.Interfaces;

namespace Project_Keystone.Infrastructure.Repositories
{
    public class BasketRepository : BaseRepository<Basket>, IBasketRepository
    {
        
        public BasketRepository(ProjectKeystoneDbContext context) : base(context)
        {
            
        }

        public async Task<Basket?> GetBasketByUserIdAsync(string userId)
        {
            return await _context.Baskets
                .Include(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefaultAsync(b => b.UserId == userId);
        }

        public async Task<BasketItem?> GetBasketItemAsync(int basketId, int productId)
        {
            return await _context.BasketItems
                .FirstOrDefaultAsync(bi => bi.BasketId == basketId && bi.ProductId == productId);
        }

        public async Task AddItemToBasketAsync(int basketId, BasketItem item)
        {
            var basket = await _context.Baskets
                .Include(b => b.BasketItems)
                .FirstOrDefaultAsync(b => b.BasketId == basketId);

            if (basket == null)
            {
                throw new InvalidOperationException($"Basket with ID {basketId} not found.");
            }

            item.BasketId = basketId;
            basket.BasketItems.Add(item);
        }

        public async Task UpdateBasketItemAsync(BasketItem item)
        {
            var existingItem = await _context.BasketItems
                .FirstOrDefaultAsync(bi => bi.BasketItemId == item.BasketItemId);

            if (existingItem == null)
            {
                throw new InvalidOperationException($"BasketItem with ID {item.BasketItemId} not found.");
            }

            _context.Entry(existingItem).CurrentValues.SetValues(item);
        }

        public async Task RemoveItemFromBasketAsync(int basketId, int basketItemId)
        {
            var basket = await _context.Baskets
                .Include(b => b.BasketItems)
                .FirstOrDefaultAsync(b => b.BasketId == basketId);

            if (basket == null)
            {
                throw new InvalidOperationException($"Basket with ID {basketId} not found.");
            }

            var itemToRemove = basket.BasketItems.FirstOrDefault(bi => bi.BasketItemId == basketItemId);
            if (itemToRemove == null)
            {
                throw new InvalidOperationException($"BasketItem with ID {basketItemId} not found in Basket {basketId}.");
            }

            basket.BasketItems.Remove(itemToRemove);
        }

        public async Task ClearBasketAsync(int basketId)
        {
            var basket = await _context.Baskets
                .Include(b => b.BasketItems)
                .FirstOrDefaultAsync(b => b.BasketId == basketId);

            if (basket == null)
            {
                throw new InvalidOperationException($"Basket with ID {basketId} not found.");
            }

            basket.BasketItems.Clear();
        }
    }

}


