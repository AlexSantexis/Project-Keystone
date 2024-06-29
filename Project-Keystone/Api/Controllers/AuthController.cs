using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project_Keystone.Api.Exceptions;
using Project_Keystone.Api.Exceptions.AuthExceptions;
using Project_Keystone.Api.Models.DTOs;
using Project_Keystone.Api.Models.DTOs.UserDTOs;
using Project_Keystone.Core.Services.Interfaces;
using System.Security.Claims;

namespace Project_Keystone.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerDTO">The user registration data.</param>
        /// <returns>A success message if registration is successful.</returns>
        /// <response code="200">Returns success message when user is registered</response>
        /// <response code="400">If the registration data is invalid or registration fails</response>
        /// <response code="500">If an unexpected error occurs</response>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid input", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            try
            {
                await _authService.RegisterUserAsync(registerDTO);
                return Ok(new { message = "User registered successfully" });
            }
            catch (UserRegistrationFailedException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during user registration");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        /// <summary>
        /// Authenticates a user and returns a token.
        /// </summary>
        /// <param name="loginDTO">The user login credentials.</param>
        /// <returns>A token model if authentication is successful.</returns>
        /// <response code="200">Returns the token model when authentication is successful</response>
        /// <response code="400">If the login data is invalid</response>
        /// <response code="401">If the credentials are invalid</response>
        /// <response code="500">If an unexpected error occurs</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid input data" });
            }

            try
            {
                var tokenModel = await _authService.LoginUserAsync(loginDTO);
                return Ok(tokenModel);
            }
            catch (InvalidCredentialsException)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during login for user {Email}", loginDTO.Email);
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        /// <summary>
        /// Deletes a user account.
        /// </summary>
        /// <param name="deleteUserDTO">The user deletion data.</param>
        /// <returns>A success message if deletion is successful.</returns>
        /// <response code="200">Returns success message when user is deleted</response>
        /// <response code="400">If the deletion data is invalid</response>
        /// <response code="404">If the user is not found</response>
        /// <response code="500">If an unexpected error occurs</response>
        [HttpDelete("delete")]
        [Authorize]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserDTO deleteUserDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid input", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            try
            {
                await _authService.DeleteUserAsync(deleteUserDTO.Email);
                return Ok(new { message = "User deleted successfully" });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during user deletion");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        /// <summary>
        /// Updates a user's information.
        /// </summary>
        /// <param name="userUpdateDTO">The user update data.</param>
        /// <returns>A success message and new token if update is successful.</returns>
        /// <response code="200">Returns success message and new token when user is updated</response>
        /// <response code="400">If the update data is invalid or update fails</response>
        /// <response code="404">If the user is not found</response>
        /// <response code="500">If an unexpected error occurs</response>
        [HttpPost("update")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDTO userUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid input", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            try
            {
                var (success, newToken) = await _authService.UpdateUserAsync(userUpdateDTO);
                return Ok(new { message = "User updated successfully", token = newToken });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UserUpdateFailedException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during user update");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        /// <summary>
        /// Changes a user's password.
        /// </summary>
        /// <param name="changePasswordDto">The password change data.</param>
        /// <returns>A success message if password change is successful.</returns>
        /// <response code="200">Returns success message when password is changed</response>
        /// <response code="400">If the password change data is invalid or change fails</response>
        /// <response code="404">If the user is not found</response>
        /// <response code="500">If an unexpected error occurs</response>
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

            try
            {
                await _authService.ChangePasswordAsync(userId, changePasswordDto);
                return Ok(new { message = "Password changed successfully" });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (PasswordChangeFailedException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during password change");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        /// <summary>
        /// Gets the current user's information.
        /// </summary>
        /// <returns>The current user's data.</returns>
        /// <response code="200">Returns the current user's data</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="404">If the user is not found</response>
        /// <response code="500">If an unexpected error occurs</response>
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var userDto = await _authService.GetCurrentUserAsync(User);
                return Ok(userDto);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while getting current user");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }

        /// <summary>
        /// Refreshes an authentication token.
        /// </summary>
        /// <param name="tokenModel">The current token model.</param>
        /// <returns>A new token model if refresh is successful.</returns>
        /// <response code="200">Returns a new token model when refresh is successful</response>
        /// <response code="400">If the token model is invalid</response>
        /// <response code="401">If the token is invalid or expired</response>
        /// <response code="500">If an unexpected error occurs</response>
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            try
            {
                var newTokenModel = await _authService.RefreshTokenAsync(tokenModel);
                return Ok(newTokenModel);
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during token refresh");
                return StatusCode(500, new { message = "An internal server error occurred" });
            }
        }
    }
}

