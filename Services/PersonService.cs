using AccountKeep.Data;
using AccountKeep.Interfaces;
using AccountKeep.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountKeep.Services
{
    public class PersonService : IPersonService
    {
        private readonly DataContext _context;

        public PersonService(DataContext context)
        {
            _context = context;
        }

        // Get all persons
        public IEnumerable<Person> GetAll()
        {
            return _context.Persons
                .Include(p => p.Accounts) // Eager-load accounts for list views
                .ToList();
        }

        // Get person by ID
        public Person? GetById(int id)
        {
            return _context.Persons
                .Include(p => p.Accounts)
                .ThenInclude(a => a.Transactions) // Loads full object graph (Person → Accounts → Transactions)
                .FirstOrDefault(p => p.PersonId == id);
        }

        // Search persons
        public IEnumerable<Person> Search(string? idNumber, string? surname, string? accountNumber)
        {
            var query = _context.Persons
                .Include(p => p.Accounts)
                .AsQueryable(); // Build query dynamically based on provided filters

            
            if (!string.IsNullOrWhiteSpace(idNumber))
                query = query.Where(p => p.IdNumber.Contains(idNumber));

            if (!string.IsNullOrWhiteSpace(surname))
                query = query.Where(p => p.Surname.Contains(surname));

            // Searches across related Accounts
            if (!string.IsNullOrWhiteSpace(accountNumber))
                query = query.Where(p => p.Accounts.Any(a => a.AccountNumber.Contains(accountNumber)));

            return query.ToList();
        }

        // Add new person
        public void Add(Person person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();
        }

        // Update existing person
        public void Update(Person person)
        {
            _context.Persons.Update(person);
            _context.SaveChanges();
        }

        // Delete person
        public void Delete(int id)
        {
            // Silent return if not found
            var person = _context.Persons.Find(id);
            if (person == null) return;

            _context.Persons.Remove(person);
            _context.SaveChanges();
        }
    }
}
