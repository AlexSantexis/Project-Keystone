using Project_Keystone.Infrastructure.Repositories;
using Project_Keystone.Core.Interfaces;

namespace Project_Keystone.Infrastructure.Configurations
{
    public static  class RepositoriesConfiguration
    {
        public static IServiceCollection AddRepositoriesConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWishListRepository, WishlistRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IProductGenreRepository, ProductGenreRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
            
        }
    }
}
