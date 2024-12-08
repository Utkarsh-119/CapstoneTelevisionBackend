using CapstoneTelevision.Models;
using CapstoneTelevision.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace CapstoneTelevision.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly JwtService _jwtService;

        public UserService(UserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        // Register a new user
        public async Task RegisterUser(User user)
        {
            // Check if user already exists
            var existingUser = await _userRepository.GetUserByName(user.Username);
            if (existingUser != null)
                throw new Exception("A user with this name already exists.");

            // Add new user
            await _userRepository.AddUser(user);
        }

        // Authenticate user and return JWT token
        public async Task<string> AuthenticateUser(string name, string password, string role)
        {
            // Find user by name and role
            var user = await _userRepository.GetUserByName(name); // Assuming email is being used here
            if (user == null || user.PasswordHash != HashPassword(password) || user.Role != role)
                throw new Exception("Invalid credentials.");

            // Generate JWT token
            return _jwtService.GenerateToken(user.Email, user.Role);
        }
        private string HashPassword(string password)
        {
            // Example hashing logic (use a proper library in production, e.g., BCrypt)
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}

