using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Project_Keystone.Api.Exceptions.AuthExceptions;
using Project_Keystone.Api.Models.DTOs;
using Project_Keystone.Api.Models.DTOs.UserDTOs;
using Project_Keystone.Core.Entities;
using Project_Keystone.Core.Services.Interfaces;
using Project_Keystone.Infrastructure.Repositories;
using Project_Keystone.Infrastructure.Repositories.Interfaces;

namespace Project_Keystone.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<string>> _roleManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUnitOfWork unitOfWork, UserManager<User> userManager, RoleManager<IdentityRole<string>> roleManager, IMapper mapper, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    throw new UserNotFoundException(userId);
                }

                await _unitOfWork.Users.DeleteUserRelatedEntitiesAsync(userId);
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                }
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    throw new UserDeletionFailedException(userId);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user with ID: {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<UserDetailedDTO>> GetAllUsersAsync()
        {

            try
            {
                var users = await _unitOfWork.Users.GetAllUsersWithDetailsAsync();
                return _mapper.Map<IEnumerable<UserDetailedDTO>>(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all users");
                throw;
            }
        }

        public async Task<UserDetailedDTO?> GetUserByIdAsync(string userId)
        {
            try
            {
                var user = await _unitOfWork.Users.GetUserByIdWithDetailsAsync(userId);
                if (user == null)
                {
                    throw new UserNotFoundException(userId);
                }

                var userDto = _mapper.Map<UserDetailedDTO>(user);
                var roles = await _userManager.GetRolesAsync(user);
                userDto.Roles = roles.Select(role => new RoleDTO { Name = role }).ToList();
                return userDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user with ID: {UserId}", userId);
                throw;
            }
        }

        public async Task<UserDetailedDTO> UpdateUserAsync(string userId, UserDetailedDTO userDto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    throw new UserNotFoundException(userId);
                }

                _mapper.Map(userDto, user);
                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    throw new UserUpdateFailedException(userId);
                }

                
                var currentRoles = await _userManager.GetRolesAsync(user);
                var rolesToRemove = currentRoles.Except(userDto.Roles.Select(r => r.Name));
                var rolesToAdd = userDto.Roles.Select(r => r.Name).Except(currentRoles);

                await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                await _userManager.AddToRolesAsync(user, rolesToAdd);

                var updatedUser = await _unitOfWork.Users.GetUserByIdWithDetailsAsync(userId);
                var updatedUserDto = _mapper.Map<UserDetailedDTO>(updatedUser);
                updatedUserDto.Roles = (await _userManager.GetRolesAsync(user))
                                      .Select(role => new RoleDTO { Name = role })
                                      .ToList();

                return updatedUserDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user with ID: {UserId}", userId);
                throw;
            }
        }
    }
}
