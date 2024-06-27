namespace Project_Keystone.Api.Exceptions.AuthExceptions
{
    public class UserRegistrationFailedException : Exception
    {
        public UserRegistrationFailedException(string errors) : base($"User registration failed: {errors}") { }
    }
}
