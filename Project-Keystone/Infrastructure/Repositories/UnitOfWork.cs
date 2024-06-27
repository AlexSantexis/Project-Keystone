using Microsoft.AspNetCore.Identity;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;
using System.Data;
using Project_Keystone.Infrastructure.Repositories.Interfaces;

namespace Project_Keystone.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectKeystoneDbContext _context;

        public UnitOfWork(ProjectKeystoneDbContext context, ITokenRepository tokens, IBasketRepository baskets, ICategoryRepository categories, IOrderRepository orders, IProductRepository products, IWishListRepository wishlists, IGenreRepository genres, IOrderDetailRepository orderDetails)
        {
            _context = context;
            Tokens = tokens;
            Baskets = baskets;
            Categories = categories;
            Orders = orders;
            Products = products;
            Wishlists = wishlists;
            Genres = genres;
            OrderDetails = orderDetails;
           
        }

        public ITokenRepository Tokens { get; private set; }
        public IBasketRepository Baskets { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IProductRepository Products { get; private set; }
        public IWishListRepository Wishlists { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IOrderDetailRepository OrderDetails { get; private set; }
       


        public async Task<int> CommitAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
