using AccountKeep.Models;

namespace AccountKeep.Interfaces
{
    public interface IPersonService
    {
        // Returns all persons
        IEnumerable<Person> GetAll();

        // Nullable → caller must handle "not found"
        Person? GetById(int id);

        // Optional parameters allow flexible searching
        IEnumerable<Person> Search(
            string? idNumber,
            string? surname,
            string? accountNumber);

        // Create new person
        void Add(Person person);

        // Update existing person
        void Update(Person person);

        // Remove person (business rules enforced in implementation)
        void Delete(int id);
    }
}
