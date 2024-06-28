namespace Project_Keystone.Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBasketRepository Baskets { get; }
        ICategoryRepository Categories { get; }
        IOrderRepository Orders { get; }
        IProductRepository Products { get; }
        IWishListRepository Wishlists { get; }
        IGenreRepository Genres { get; }
        IAddressRepository Address { get; }
        ITokenRepository Tokens { get; }

        IUserRepository Users { get; }
        Task<int> CommitAsync();
    }
}
