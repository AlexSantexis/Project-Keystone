using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Keystone.Api.Exceptions;
using Project_Keystone.Api.Models.DTOs;
using Project_Keystone.Api.Models.DTOs.UserDTOs;
using Project_Keystone.Core.Services.Interfaces;
using System.Security.Claims;

namespace Project_Keystone.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid input", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            await _authService.RegisterUserAsync(registerDTO);
            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid input", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            var token = await _authService.LoginUserAsync(loginDTO);
            return Ok(new { access_token = token });
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserDTO deleteUserDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid input", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            await _authService.DeleteUserAsync(deleteUserDTO.Email);
            return Ok(new { message = "User deleted successfully" });
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDTO userUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid input", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            var (success, newToken) = await _authService.UpdateUserAsync(userUpdateDTO);
            return Ok(new { message = "User updated successfully", token = newToken });
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] UserUpdatePasswordDTO changePasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid input", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "User ID not found in the token" });
            }

            await _authService.ChangePasswordAsync(userId, changePasswordDto);
            return Ok(new { message = "Password changed successfully" });
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userDto = await _authService.GetCurrentUserAsync(User);
            return Ok(userDto);
        }
    }

}

