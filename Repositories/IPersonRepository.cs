using PersonApi.Models;

namespace PersonApi.Repositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllPersonsAsync();
        Task<Person?> GetPersonByIdAsync(int id);
        Task<Person> CreatePersonAsync(Person person);
        Task<Person?> UpdatePersonAsync(int id, Person person);
        Task<bool> DeletePersonAsync(int id);
    }
}