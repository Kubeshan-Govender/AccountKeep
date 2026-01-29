using System.ComponentModel.DataAnnotations;

namespace AccountKeep.Models
{// Represents a user in the system
    public class User
    {
        [Key]
        public int UserId { get; set; }// Primary key

        [Required]
        public string Username { get; set; } = null!;// Must have a value; null-forgiving operator (!) used to avoid compiler warnings

        [Required]
        public string PasswordHash { get; set; } = null!;// Stores hashed password, not plain text

        public DateTime CreatedAt { get; set; } = DateTime.Now;// Automatically stores creation time
    }
}
