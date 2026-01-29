using AccountKeep.Data;
using AccountKeep.Interfaces;
using AccountKeep.Models;
using System.Security.Cryptography;
using System.Text;

namespace AccountKeep.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public bool Authenticate(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null) return false; // User not found -> fail login

            string hash = HashPassword(password);
            return user.PasswordHash == hash;  // Compare hashed password, never store/compare plain text
        }

        // SHA256 hash
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes); // Deterministic hash for comparison
        }

    }
}
