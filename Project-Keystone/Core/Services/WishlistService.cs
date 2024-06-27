using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Project_Keystone.Api.Exceptions.AuthExceptions;
using Project_Keystone.Api.Exceptions.WishlistExceptions;
using Project_Keystone.Api.Models.DTOs.Wishlist;
using Project_Keystone.Core.Entities;
using Project_Keystone.Core.Services.Interfaces;
using Project_Keystone.Infrastructure.Repositories.Interfaces;


namespace Project_Keystone.Core.Services
    {
        public class WishlistService : IWishlistService
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<WishlistService> _logger;
            private readonly IMapper _mapper;
            private readonly UserManager<User> _userManager;

        public WishlistService(IUnitOfWork unitOfWork, ILogger<WishlistService> logger, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<WishlistDTO> GetUserWishlistAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty", nameof(userId));
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }

            var wishlist = await _unitOfWork.Wishlists.GetWishlistByUserIdAsync(userId);
            if (wishlist == null)
            {
                _logger.LogInformation("Creating new wishlist for user {UserId}", userId);
                var newWishlist = new Wishlist { UserId = userId };
                await _unitOfWork.Wishlists.AddAsync(newWishlist);
                await _unitOfWork.CommitAsync();
                wishlist = await _unitOfWork.Wishlists.GetWishlistByUserIdAsync(userId);
            }
            return _mapper.Map<WishlistDTO>(wishlist);                  
        }

            public async Task<bool> AddToWishlistAsync(string userId, int productId)
            {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty", nameof(userId));
            }

            if (productId <= 0)
            {
                throw new ArgumentException("Product ID must be a positive integer", nameof(productId));
            }

            var wishlist = await _unitOfWork.Wishlists.GetWishlistByUserIdAsync(userId);
            if (wishlist == null)
            {
                wishlist = new Wishlist { UserId = userId };
                await _unitOfWork.Wishlists.AddAsync(wishlist);
                await _unitOfWork.CommitAsync();
            }

            var existingItem = wishlist.WishListItems.FirstOrDefault(item => item.ProductId == productId);
            if (existingItem != null)
            {
                throw new WishlistItemAlreadyExistsException(productId);
            }

            var result = await _unitOfWork.Wishlists.AddItemToWishlistAsync(wishlist.WishlistId, productId);
            if (result)
            {
                await _unitOfWork.CommitAsync();
                _logger.LogInformation("Added product {ProductId} to wishlist for user {UserId}", productId, userId);
                return true;
            }

            throw new WishlistOperationFailedException("Add item to wishlist");
        }

            public async Task<bool> RemoveFromWishlistAsync(string userId, int productId)
            {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty", nameof(userId));
            }

            if (productId <= 0)
            {
                throw new ArgumentException("Product ID must be a positive integer", nameof(productId));
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }

            var wishlist = await _unitOfWork.Wishlists.GetWishlistByUserIdAsync(userId);
            if (wishlist == null)
            {
                throw new WishlistNotFoundException(userId);
            }

            var result = await _unitOfWork.Wishlists.RemoveItemFromWishlistAsync(wishlist.WishlistId, productId);
            if (result)
            {
                await _unitOfWork.CommitAsync();
                _logger.LogInformation("Removed product {ProductId} from wishlist for user {UserId}", productId, userId);
                return true;
            }

            throw new WishlistItemNotFoundException(productId);
        }

            public async Task<WishlistDTO> GetWishlistItemsAsync(string userId)
            {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty", nameof(userId));
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }

            var wishlist = await _unitOfWork.Wishlists.GetWishlistByUserIdAsync(userId);
            if (wishlist == null)
            {
                throw new WishlistNotFoundException(userId);
            }

            var wishlistDto = _mapper.Map<WishlistDTO>(wishlist);
            _logger.LogInformation("Retrieved {ItemCount} wishlist items for user {UserId}", wishlistDto.Items?.Count ?? 0, userId);
            return wishlistDto;
            }
        }
}
    


