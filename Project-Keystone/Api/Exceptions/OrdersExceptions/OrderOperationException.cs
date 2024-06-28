namespace Project_Keystone.Api.Exceptions.OrdersExceptions
{
    public class OrderOperationException : Exception
    {
        public OrderOperationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
