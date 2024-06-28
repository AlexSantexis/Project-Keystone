namespace Project_Keystone.Api.Exceptions.AuthExceptions
{
    public class UserDeletionFailedException : Exception
    {
        public UserDeletionFailedException(string? userId) : base($"User Deletiong with id {userId} failed") { }
        
    }
}
