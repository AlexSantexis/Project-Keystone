namespace Project_Keystone.Api.Exceptions.WishlistExceptions
{
    public class WishlistItemAlreadyExistsException : Exception
    {
        public WishlistItemAlreadyExistsException(int productId) : base($"Product with ID {productId} already exists in the wishlist.") { }
    }
}
