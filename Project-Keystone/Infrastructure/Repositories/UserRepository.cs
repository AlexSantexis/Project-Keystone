using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Project_Keystone.Core.Entities;
using Project_Keystone.Infrastructure.Data;
using Project_Keystone.Core.Interfaces;

namespace Project_Keystone.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        private readonly UserManager<User> _userManager;
        public UserRepository(ProjectKeystoneDbContext context, UserManager<User> userManager) : base(context)
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

        public async Task<List<User>> GetAllUsersFilteredAsync(int pageNumber, int pageSize, List<Func<User, bool>> filters)
        {

            int skip = pageSize * (pageNumber -1 );
            IQueryable<User> query = _context.Users.Skip(skip).Take(pageSize);
            if(filters != null && filters.Any())
            {
                query = query.Where(u => filters.All(filters => filters(u)));
            }
            return await query.ToListAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
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
