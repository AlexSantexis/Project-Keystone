using Project_Keystone.Core.Entities;

namespace Project_Keystone.Core.Interfaces
{
    public interface IBasketRepository : IBaseRepository<Basket>
    {
        Task<Basket?> GetBasketByUserIdAsync(int userId);
        Task AddItemToBasketAsync(int basketId, BasketItem item);
        Task RemoveItemFromBasketAsync(int basketId,int itemId);
    }
}
