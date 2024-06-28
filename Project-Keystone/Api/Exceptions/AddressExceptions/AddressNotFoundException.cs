namespace Project_Keystone.Api.Exceptions.AddressExceptions
{
    public class AddressNotFoundException : Exception
    {
        public AddressNotFoundException(string userId)
        : base($"Address not found for user with ID {userId}") { }
    }
}
