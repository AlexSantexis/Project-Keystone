using Microsoft.AspNetCore.Identity;
using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;
using Project_Keystone.Infrastructure.Repositories.Interfaces;
using System.Data;

namespace Project_Keystone.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectKeystoneDbContext _context;

       public UnitOfWork(ProjectKeystoneDbContext context,UserManager<User> userManager)
        {
            _context = context;
            Baskets = new BasketRepository(_context);
            Categories = new CategoryRepository(_context);
            Orders = new OrderRepository(_context);
            Products = new ProductRepository(_context);
            Users = new UserRepository(_context,userManager);
            Wishlists = new WishlistRepository(_context);
            Genres = new GenreRepository(_context);
            OrderDetails = new OrderDetailRepository(_context);
            ProductGenres = new ProductGenreRepository(_context);
            //Roles = new RoleRepository(_context);
            //UserRoles = new UserRoleRepository(_context);
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
        //public IRoleRepository Roles { get; private set; }
        //public IUserRoleRepository UserRoles { get; private set; }

        public async Task<int> CompleteAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
