namespace Project_Keystone.Api.Exceptions.AddressExceptions
{
    public class AddressOperationFailedException : Exception
    {
        public AddressOperationFailedException(string operation)
            : base($"Address {operation} operation failed") { }
    }
}
