using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;
using Project_Keystone.Infrastructure.Repositories.Interfaces;

namespace Project_Keystone.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        private readonly UserManager<User> _userManager;
        

        public UserRepository(ProjectKeystoneDbContext context,UserManager<User> userManager) :base(context)
        {
            _userManager = userManager;
        }

        public async Task AddUserRoleAsync(int userId, string role)
        {
            var user = await GetByIdAsync(userId);
            if(user != null)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
        }

        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var user = await GetByIdAsync(userId);
            if(user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<List<User>> GetAllUsersFilteredAsync(int pageNumber, int pageSize, List<Func<User, bool>> filters)
        {

            int skip = pageSize * pageNumber;
            IQueryable<User> query = _context.Users.Skip(skip).Take(pageSize);
            if(filters != null && filters.Any())
            {
                query = query.Where(u => filters.All(filters => filters(u)));
            }
            return await query.ToListAsync();
        }

        public async Task<User?> GetUserAsync(string username, string password)
        {
            var user  = await _userManager.FindByNameAsync(username);
            if(user != null && await _userManager.CheckPasswordAsync(user, password)) 
            {
                return user;
            }
            return null;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<List<string>> GetUserRolesAsync(int userId)
        {
            var user = await GetByIdAsync(userId);
            if (user != null)
            {
                return (await _userManager.GetRolesAsync(user)).ToList();
            }
            return new List<string>();
        }

        public async Task RemoveUserRoleAsync(int userId, string role)
        {
            var user = await GetByIdAsync(userId);
            if(user != null)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }
        }

        public async Task<User?> UpdateUserAsync(int userId, User request)
        {
            var user = await GetByIdAsync(userId);
            if (user != null)
            {
                user.Firstname = request.Firstname;
                user.Lastname = request.Lastname;
                user.Email = request.Email;
                user.UserName = request.UserName;
                UpdateAsync(user);
                return user;
            }
            return null;
        }
    }
}
