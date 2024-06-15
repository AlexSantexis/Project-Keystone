using Microsoft.AspNetCore.Identity;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;
using Project_Keystone.Core.Interfaces;
using System.Data;

namespace Project_Keystone.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectKeystoneDbContext _context;

        public UnitOfWork(ProjectKeystoneDbContext context, IBasketRepository baskets, ICategoryRepository categories, IOrderRepository orders, IProductRepository products, IUserRepository users, IWishListRepository wishlists, IGenreRepository genres, IOrderDetailRepository orderDetails, IProductGenreRepository productGenres)
        {
            _context = context;
            Baskets = baskets;
            Categories = categories;
            Orders = orders;
            Products = products;
            Users = users;
            Wishlists = wishlists;
            Genres = genres;
            OrderDetails = orderDetails;
            ProductGenres = productGenres;
            
        }

        public IBasketRepository Baskets { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IProductRepository Products { get; private set; }
        public IUserRepository Users { get; private set; }
        public IWishListRepository Wishlists { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IOrderDetailRepository OrderDetails { get; private set; }
        public IProductGenreRepository ProductGenres { get; private set; }
        

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
