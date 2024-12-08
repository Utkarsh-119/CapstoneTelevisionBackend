using CapstoneTelevision.Data;
using CapstoneTelevision.Models;
using Microsoft.EntityFrameworkCore;

namespace CapstoneTelevision.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        // Add a new user
        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        // Get a user by email
        public async Task<User> GetUserByName(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == name);
        }

        // Get a user by ID
        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
