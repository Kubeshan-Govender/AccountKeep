using AccountKeep.Models;
using System.Collections.Generic;

namespace AccountKeep.Interfaces
{
    public interface ITransactionService
    {
        // Returns all transactions for a specific account
        IEnumerable<Transaction> GetByAccount(int accountId);

        // Nullable → transaction may not exist
        Transaction? GetById(int id);

        // Adds transaction and adjusts account balance
        void Add(Transaction transaction);

        // Updates transaction and recalculates balance
        void Update(Transaction transaction);

        // Removes transaction and must reverse its balance effect
        void Delete(int transactionId);
    }
}
