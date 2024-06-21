using Project_Keystone.Core.Entities;

namespace Project_Keystone.Core.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTToken(User user, List<string> roles);
    }
}
