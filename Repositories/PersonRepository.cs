using Microsoft.EntityFrameworkCore;
using PersonApi.Data;
using PersonApi.Models;

namespace PersonApi.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;
        
        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            return await _context.Persons.ToListAsync();
        }
        
        public async Task<Person?> GetPersonByIdAsync(int id)
        {
            return await _context.Persons.FindAsync(id);
        }
        
        public async Task<Person> CreatePersonAsync(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }
        
        public async Task<Person?> UpdatePersonAsync(int id, Person personUpdate)
        {
            var person = await _context.Persons.FindAsync(id);
            
            if (person == null)
                return null;
            
            person.FirstName = personUpdate.FirstName;
            person.LastName = personUpdate.LastName;
            person.Address = personUpdate.Address;
            person.Phone = personUpdate.Phone;
            person.Email = personUpdate.Email;
            person.DateOfBirth = personUpdate.DateOfBirth;
            person.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return person;
        }
        
        public async Task<bool> DeletePersonAsync(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            
            if (person == null)
                return false;
            
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}