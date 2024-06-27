namespace Project_Keystone.Api.Exceptions.WishlistExceptions
{
    public class WishlistItemNotFoundException : Exception
    {
        public WishlistItemNotFoundException(int productId) : base($"Product with ID {productId} not found in the wishlist.") { }
    }
}
