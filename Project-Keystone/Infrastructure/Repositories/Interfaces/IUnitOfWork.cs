namespace Project_Keystone.Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBasketRepository Baskets { get; }
        ICategoryRepository Categories { get; }
        IOrderRepository Orders { get; }
        IProductRepository Products { get; }
        IUserRepository Users { get; }
        IWishListRepository Wishlists { get; }
        IGenreRepository Genres { get; }
        IOrderDetailRepository OrderDetails { get; }
        IProductGenreRepository ProductGenres { get; }
        //IRoleRepository Roles { get; }
        //IUserRoleRepository UserRoles { get; }
        Task<int> CompleteAsync();
    }
}
