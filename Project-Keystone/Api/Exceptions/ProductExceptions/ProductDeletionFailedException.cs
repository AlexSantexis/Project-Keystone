namespace Project_Keystone.Api.Exceptions.ProductExceptions
{
    public class ProductDeletionFailedException : Exception
    {
        public ProductDeletionFailedException(int productId) : base($"Failed to delete product with ID {productId}") { }
    }
}
