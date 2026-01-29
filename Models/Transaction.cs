using System.ComponentModel.DataAnnotations.Schema;

namespace AccountKeep.Models
{
    // Represents a financial transaction linked to an account
    public class Transaction
    {
        public int TransactionId { get; set; }// Primary key

        public DateTime TransactionDate { get; set; }

        public decimal Amount { get; set; }// Use decimal for money to avoid rounding errors

        public string Description { get; set; } = string.Empty;

        public DateTime CaptureDate { get; set; } = DateTime.Now;// Defaults to creation time

        // Foreign Key
        public int AccountId { get; set; }// Foreign key to Account

        // Navigation property; can be null if not loaded
        public Account? Account { get; set; }
    }
}
