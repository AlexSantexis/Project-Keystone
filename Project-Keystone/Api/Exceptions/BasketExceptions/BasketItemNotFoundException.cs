namespace Project_Keystone.Api.Exceptions.BasketExceptions
{
    public class BasketItemNotFoundException : Exception
    {
        public BasketItemNotFoundException(string message) : base(message) { }
    }
}
