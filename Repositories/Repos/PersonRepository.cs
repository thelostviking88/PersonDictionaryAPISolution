using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repos
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        private readonly PersonDBContext _dBContext;
        public PersonRepository(PersonDBContext context) : base(context)
        {
            _dBContext = context;
        }

        public void CreatePerson(Person person)
        {
            Add(person);
        }

        public void DeletePerson(Person person)
        {
            Delete(person);
        }

        public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<Person> GetPersonByIdAsync(int personId)
        {
            return await GetByCondition(person => person.Id.Equals(personId)).FirstOrDefaultAsync();
        }

        public async Task<Person> GetPersonWithDetailsAsync(int personId)
        {
            return await GetByCondition(person => person.Id.Equals(personId)).
                Include(p => p.PersonConnectionPerson).
                ThenInclude(p => p.ConnectedPerson).
                ThenInclude(cp => cp.City).
                Include(p => p.PersonConnectionPerson).
                ThenInclude(p => p.ConnectedPerson).
                ThenInclude(pp => pp.Phone).
                Include(p => p.City).
                Include(p => p.Phone).FirstOrDefaultAsync()
                ;
        }

        public void UpdatePerson(Person person)
        {
            Update(person);
        }
    }
}
