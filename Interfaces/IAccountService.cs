using AccountKeep.Models;

namespace AccountKeep.Interfaces
{
    public interface IAccountService
    {
        // Returns all accounts for a specific person
        IEnumerable<Account> GetByPerson(int personId);
        Account? GetById(int id);

        // Creates a new account
        void Add(Account account);

        // Updates account details (status, type, etc.)
        void Update(Account account);
    }
}

