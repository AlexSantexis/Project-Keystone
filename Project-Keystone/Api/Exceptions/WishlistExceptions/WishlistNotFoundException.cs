namespace Project_Keystone.Api.Exceptions.WishlistExceptions
{
    public class WishlistNotFoundException : Exception
    {
        public WishlistNotFoundException(string userId) : base($"Wishlist not found for user with ID {userId}.") { }
    }
}
