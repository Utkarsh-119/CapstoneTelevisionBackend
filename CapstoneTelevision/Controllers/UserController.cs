using CapstoneTelevision.Data;
using CapstoneTelevision.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CapstoneTelevision.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        //[Authorize("AdminOnly")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            if (users == null || !users.Any()) return NotFound("No users found.");

            var userDtos = users.Select(user => new UserDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Role = user.Role,
                Email = user.Email
            }).ToList();

            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        //[Authorize("AdminOnly")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            var userDto = new UserDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Role = user.Role,
                Email = user.Email
            };
            return Ok(userDto);
        }
        [HttpPut("{id}")]
        //[Authorize("AdminOnly")]
        public async Task<IActionResult> UpdateUser(int id, UserDTO userDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.Username = userDto.Username;
            user.Role = userDto.Role;
            user.Email = userDto.Email;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        //[Authorize("Adminonly")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
