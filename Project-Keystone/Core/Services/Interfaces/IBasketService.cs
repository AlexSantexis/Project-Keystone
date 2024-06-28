using Project_Keystone.Api.Models.DTOs.BasketDTOs;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Repositories.Interfaces;

namespace Project_Keystone.Core.Services.Interfaces
{
    public interface IBasketService
    {
        Task<BasketDTO> GetBasketAsync(string userId);
        Task<BasketDTO> AddToBasketAsync(string userId, AddToBasketDTO addToBasketDto);
        Task<BasketDTO> UpdateBasketItemAsync(string userId, UpdateBasketItemDTO updateBasketItemDto);
        Task<BasketDTO> RemoveFromBasketAsync(string userId, int basketItemId);
        Task<BasketDTO> ClearBasketAsync(string userId);
    }
}
