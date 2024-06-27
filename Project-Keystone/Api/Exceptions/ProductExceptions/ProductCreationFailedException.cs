namespace Project_Keystone.Api.Exceptions.ProductExceptions
{
    public class ProductCreationFailedException : Exception
    {
        public ProductCreationFailedException(string message) : base($"Failed to create product: {message}") { }
    }
}
