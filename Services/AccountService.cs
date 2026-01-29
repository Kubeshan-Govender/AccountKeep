using AccountKeep.Data;
using AccountKeep.Interfaces;
using AccountKeep.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountKeep.Services
{
    public class AccountService : IAccountService
    {
        private readonly DataContext _context;

        public AccountService(DataContext context)
        {
            _context = context;
        }

        // Get all accounts for a specific person
        public IEnumerable<Account> GetByPerson(int personId)
        {
            return _context.Accounts
                .Where(a => a.PersonId == personId) // Eager-load transactions to avoid lazy-loading issues
                .Include(a => a.Transactions)
                .ToList();
        }

        // Get account by ID
        public Account? GetById(int id)
        {
            return _context.Accounts
                .Include(a => a.Transactions) // Ensures related transactions are available
                .FirstOrDefault(a => a.AccountId == id);
        }


        public void Add(Account account)
        {
            // Persists new account
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }

      
        public void Update(Account account)
        {
            // Saves changes to existing account
            _context.Accounts.Update(account);
            _context.SaveChanges();
        }
    }
}
