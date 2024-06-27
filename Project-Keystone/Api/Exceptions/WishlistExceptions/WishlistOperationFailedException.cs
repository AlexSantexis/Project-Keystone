namespace Project_Keystone.Api.Exceptions.WishlistExceptions
{
    public class WishlistOperationFailedException : Exception
    {
        public WishlistOperationFailedException(string operation) : base($"Wishlist operation failed: {operation}") { }
    }
}
