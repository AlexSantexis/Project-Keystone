namespace Project_Keystone.Api.Exceptions.AuthExceptions
{
    public class UserUpdateFailedException : Exception
    {
        public UserUpdateFailedException(string errors) : base($"User update failed: {errors}") { }
    }
}
