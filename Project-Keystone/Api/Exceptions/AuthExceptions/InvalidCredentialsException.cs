namespace Project_Keystone.Api.Exceptions.AuthExceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("Invalid email or password.") { }
    }
}
