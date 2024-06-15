using Microsoft.AspNetCore.Mvc;
using Project_Keystone.Api.Models.DTOs;
using Project_Keystone.Core.Interfaces;

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
                return Ok("User registered successfully");
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
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in user {Email}", loginDTO.Email);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
