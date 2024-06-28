namespace Project_Keystone.Api.Exceptions.AddressExceptions
{
    public class AddressAlreadyExistsException : Exception
    {
        public AddressAlreadyExistsException(string userId)
       : base($"Address already exists for user with ID {userId}") { }
    }
}
