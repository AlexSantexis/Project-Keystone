namespace Project_Keystone.Api.Exceptions.ProductExceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(int productId) : base($"Product with ID {productId} not found.") { }
    }

}
