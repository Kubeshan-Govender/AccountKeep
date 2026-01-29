// Represents a bank account belonging to a person.
// Stores account number, type, balance, and status (Open/Closed).
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AccountKeep.Models
{
    // Represents a bank account for a person
    public class Account
    {
        public int AccountId { get; set; }// Primary key

        [Required]
        public string AccountNumber { get; set; } = string.Empty;// Must have a value

        [Required]
        public string AccountType { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Open";// Default "Open"; consider enum for safety

        [Required]
        public int PersonId { get; set; }

        [BindNever]
        public Person? Person { get; set; }// Prevents binding errors from forms

        public decimal Balance { get; set; } = 0;// Decimal for money to avoid rounding errors

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();// Prevents null reference when adding transactions
    }
}
