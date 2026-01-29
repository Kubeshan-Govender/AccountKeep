using AccountKeep.Data;
using AccountKeep.Interfaces;
using AccountKeep.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountKeep.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly DataContext _context;

        public TransactionService(DataContext context)
        {
            _context = context;
        }

        // Get all transactions for an account
        public IEnumerable<Transaction> GetByAccount(int accountId)
        {
            return _context.Transactions
                .Where(t => t.AccountId == accountId)
                .Include(t => t.Account)
                .ThenInclude(a => a.Person) // Load related Account and Person for display
                .ToList();
        }

        // Get transaction by ID
        public Transaction? GetById(int id)
        {
            return _context.Transactions
                .Include(t => t.Account) // Include account for balance updates / info
                .FirstOrDefault(t => t.TransactionId == id);
        }

        // Add new transaction
        public void Add(Transaction transaction)
        {
            var account = _context.Accounts.Find(transaction.AccountId);

            // Business rules enforced in service
            if (account == null)
                throw new Exception("Account not found");

            if (account.Status == "Closed")
                throw new Exception("Cannot add transactions to a closed account");

            if (transaction.TransactionDate > DateTime.Today)
                throw new Exception("Transaction date cannot be in the future");

            if (transaction.Amount == 0)
                throw new Exception("Transaction amount cannot be zero");

            transaction.CaptureDate = DateTime.Now;

            // Update account balance
            account.Balance += transaction.Amount;

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }


        // Update existing transaction
        public void Update(Transaction transaction)
        {
            transaction.CaptureDate = DateTime.Now;  // Updates timestamp, but does NOT adjust balance — controller must ensure correctness
            _context.Transactions.Update(transaction);
            _context.SaveChanges();
        }

        public void Delete(int transactionId)
        {
            var transaction = _context.Transactions.Find(transactionId);
            if (transaction == null) return;

            // Reverse transaction effect on account balance
            var account = _context.Accounts.Find(transaction.AccountId);
            if (account != null)
            {
                account.Balance -= transaction.Amount;
            }

            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
        }


    }

}
