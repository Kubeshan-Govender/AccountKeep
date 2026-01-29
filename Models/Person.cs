using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace AccountKeep.Models
{
    //Represents an individual person in the system
    public class Person
    {
        [Key]
        public int PersonId { get; set; }// Primary key

        [Required]
        [MaxLength(13)]
        [Display(Name = "ID Number")]
        public string IdNumber { get; set; } = string.Empty;// Must be provided; max length enforced

        [Required]
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [Display(Name = "Surname")]
        public string Surname { get; set; } = string.Empty;

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;// Validates proper email format

        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;// Validates phone format

        public DateTime CreatedAt { get; set; } = DateTime.Now;// Automatically stores creation time

        // Navigation property to prevent null reference
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
