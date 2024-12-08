using CapstoneTelevision.Models;
using CapstoneTelevision.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace CapstoneTelevision.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserService _userService;

        public LoginController(UserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDto)
        {
            try
            {
                var user = new User
                {
                    Username = userDto.Username,
                    PasswordHash = HashPassword(userDto.Password), 
                    Role = userDto.Role,
                    Email = userDto.Email
                };
                await _userService.RegisterUser(user);
                return CreatedAtAction(nameof(Register), new { user.UserId }, user);
            }
            catch (Exception ex)
            {
                return Conflict(new { Message = ex.Message });
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(loginRequest.Name) || string.IsNullOrEmpty(loginRequest.Password) || string.IsNullOrEmpty(loginRequest.Role))
                    return BadRequest("All fields are required.");

                var token = await _userService.AuthenticateUser(loginRequest.Name, loginRequest.Password, loginRequest.Role);
                return Ok(new { Token = token});
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }
        private string HashPassword(string password)
        {
            // Example hashing logic (use a proper library in production, e.g., BCrypt)
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

    }
    public class LoginRequest
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
