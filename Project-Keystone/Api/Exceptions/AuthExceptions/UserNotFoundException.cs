namespace Project_Keystone.Api.Exceptions.AuthExceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string email) : base($"User with email {email} not found.") { }
    }
}
