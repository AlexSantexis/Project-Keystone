using Project_Keystone.Core.Entities;

namespace Project_Keystone.Infrastructure.Repositories.Interfaces
{
    public interface IBasketRepository : IBaseRepository<Basket>
    {
        Task<Basket?> GetBasketByUserIdAsync(string userId);
        Task<BasketItem?> GetBasketItemAsync(int basketId, int productId);
        Task AddItemToBasketAsync(int basketId, BasketItem item);
        Task UpdateBasketItemAsync(BasketItem item);
        Task RemoveItemFromBasketAsync(int basketId, int basketItemId);
        Task ClearBasketAsync(int basketId);
    }
}
