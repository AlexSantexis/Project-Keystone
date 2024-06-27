namespace Project_Keystone.Api.Exceptions.AuthExceptions
{
    public class PasswordChangeFailedException : Exception
    {
        public PasswordChangeFailedException(string errors) : base($"Password change failed: {errors}") { }
    }
}
