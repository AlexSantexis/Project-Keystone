using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Keystone.Api.Models.DTOs;
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
                _logger.LogWarning("Invalid model state for Register");
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _authService.RegisterUserAsync(registerDTO);
                if (!result)
                {
                    _logger.LogWarning("User registration failed for {Email}", registerDTO.Email);
                    return BadRequest("User registration failed");
                }

                _logger.LogInformation("User {Email} registered successfully", registerDTO.Email);
                return Ok(new { msg = "User registered successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering user {Email}", registerDTO.Email);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for Login");
                return BadRequest(ModelState);
            }

            try
            {
                var token = await _authService.LoginUserAsync(loginDTO);
                if (string.IsNullOrEmpty(token))
                {
                    _logger.LogWarning("Login failed for {Email}", loginDTO.Email);
                    return Unauthorized("Invalid email or password");
                }

                _logger.LogInformation("User {Email} logged in successfully", loginDTO.Email);
                return Ok(new { access_token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in user {Email}", loginDTO.Email);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserDTO deleteUserDTO)
        {
            if (!ModelState.IsValid) 
            {
                _logger.LogWarning("Invalid model state for DeleteUser");
                return BadRequest(new {msg = "Invalid user data."});
            }
            try
            {
                var result = await _authService.DeleteUserAsync(deleteUserDTO.Email);
                if (!result)
                {
                    _logger.LogWarning("User deletion failed for {Email}", deleteUserDTO.Email);
                    return BadRequest(new { msg = "User deletion failed." });
                }

                _logger.LogInformation("User {Email} deleted successfully", deleteUserDTO.Email);
                return Ok(new { msg = "User deleted successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user {Email}", deleteUserDTO.Email);
                return StatusCode(500, new { msg = "Internal server error." });
            }
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDTO userUpdateDTO)
        {
           var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            if (!User.IsInRole("User"))
            {
                _logger.LogWarning("User is not authorized to update user");
                return Unauthorized();
            }
            _logger.LogInformation("Authenticated user roles: {Roles}", string.Join(", ", roles));
            if (!ModelState.IsValid)
            {
                _logger.LogError("An error occured while updating user");
                return BadRequest(ModelState);
            }
            try
            {
                var (success, newToken) = await _authService.UpdateUserAsync(userUpdateDTO);
                if (!success)
                {
                    _logger.LogWarning("User update failed for {Email}", userUpdateDTO.Email);
                    return BadRequest(new { msg = "User update failed." });
                }
                return Ok(new { msg = "User updated successfully.", token = newToken });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user {Email}", userUpdateDTO.Email);
                return StatusCode(500, new { msg = "Internal server error." });
            }
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var userDto = await _authService.GetCurrentUserAsync(User);
                return Ok(userDto);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "User ID not found in claims" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "User not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the user", details = ex.Message });
            }
        }
    }

}

