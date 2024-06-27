namespace Project_Keystone.Api.Exceptions.ProductExceptions
{
    public class ProductUpdateFailedException : Exception
    {
        public ProductUpdateFailedException(int productId, string message) : base($"Failed to update product with ID {productId}: {message}") { }
    }
}
