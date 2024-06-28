using AutoMapper;
using Project_Keystone.Api.Exceptions.BasketExceptions;
using Project_Keystone.Api.Exceptions.ProductExceptions;
using Project_Keystone.Api.Models.DTOs.BasketDTOs;
using Project_Keystone.Core.Entities;
using Project_Keystone.Core.Services.Interfaces;
using Project_Keystone.Infrastructure.Repositories.Interfaces;

namespace Project_Keystone.Core.Services
{
    public class BasketService : IBasketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<BasketService> _logger;

        public BasketService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BasketService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BasketDTO> GetBasketAsync(string userId)
        {
            var basket = await _unitOfWork.Baskets.GetBasketByUserIdAsync(userId);
            if (basket == null)
            {
                _logger.LogInformation($"Basket not found for user {userId}. Creating a new one.");
                basket = new Basket { UserId = userId };
                await _unitOfWork.Baskets.AddAsync(basket);
                await _unitOfWork.CommitAsync();
            }

            var basketDto = _mapper.Map<BasketDTO>(basket);

            
            foreach (var item in basketDto.Items)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);
                if (product != null)
                {
                    item.ProductName = product.Name;
                    item.Price = product.Price;
                    item.ImgUrl = product.ImageUrl!;
                    
                }
            }

            return basketDto;
        }

        public async Task<BasketDTO> AddToBasketAsync(string userId, AddToBasketDTO addToBasketDto)
        {
            var basket = await _unitOfWork.Baskets.GetBasketByUserIdAsync(userId);
            if (basket == null)
            {
                _logger.LogInformation($"Basket not found for user {userId}. Creating a new one.");
                basket = new Basket { UserId = userId };
                await _unitOfWork.Baskets.AddAsync(basket);
                await _unitOfWork.CommitAsync();
            }

            var product = await _unitOfWork.Products.GetByIdAsync(addToBasketDto.ProductId);
            if (product == null)
            {
                throw new ProductNotFoundException(addToBasketDto.ProductId);
            }

            var existingItem = await _unitOfWork.Baskets.GetBasketItemAsync(basket.BasketId, addToBasketDto.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += addToBasketDto.Quantity;
                await _unitOfWork.Baskets.UpdateBasketItemAsync(existingItem);
            }
            else
            {
                var newItem = new BasketItem
                {
                    ProductId = addToBasketDto.ProductId,
                    Quantity = addToBasketDto.Quantity,
                    AddedAt = DateTime.UtcNow
                };
                await _unitOfWork.Baskets.AddItemToBasketAsync(basket.BasketId, newItem);
            }
            await _unitOfWork.CommitAsync();
            return await GetBasketAsync(userId);
        }

        public async Task<BasketDTO> UpdateBasketItemAsync(string userId, UpdateBasketItemDTO updateBasketItemDto)
        {
            var basket = await _unitOfWork.Baskets.GetBasketByUserIdAsync(userId);
            if (basket == null)
            {
                throw new BasketNotFoundException($"Basket not found for user {userId}");
            }

            var basketItem = basket.BasketItems.FirstOrDefault(bi => bi.BasketItemId == updateBasketItemDto.BasketItemId);
            if (basketItem == null)
            {
                throw new BasketItemNotFoundException($"Basket item {updateBasketItemDto.BasketItemId} not found in basket");
            }

            basketItem.Quantity = updateBasketItemDto.Quantity;
            await _unitOfWork.Baskets.UpdateBasketItemAsync(basketItem);
            await _unitOfWork.CommitAsync();

            return await GetBasketAsync(userId);
        }

        public async Task<BasketDTO> RemoveFromBasketAsync(string userId, int basketItemId)
        {
            var basket = await _unitOfWork.Baskets.GetBasketByUserIdAsync(userId);
            if (basket == null)
            {
                throw new BasketNotFoundException($"Basket not found for user {userId}");
            }

            await _unitOfWork.Baskets.RemoveItemFromBasketAsync(basket.BasketId, basketItemId);
            await _unitOfWork.CommitAsync();

            return await GetBasketAsync(userId);
        }

        public async Task<BasketDTO> ClearBasketAsync(string userId)
        {
            var basket = await _unitOfWork.Baskets.GetBasketByUserIdAsync(userId);
            if (basket == null)
            {
                throw new BasketNotFoundException($"Basket not found for user {userId}");
            }

            await _unitOfWork.Baskets.ClearBasketAsync(basket.BasketId);
            await _unitOfWork.CommitAsync();

            return await GetBasketAsync(userId);
        }
    }
}
