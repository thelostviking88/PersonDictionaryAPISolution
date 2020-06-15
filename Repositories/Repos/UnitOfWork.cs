using Data.Models;
using Repositories.Interfaces;
using System.Threading.Tasks;

namespace Repositories.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PersonDBContext _dBContext;
        public PersonRepository PersonRepository;
        public ConnecttionRepository ConnecttionRepository;
        public UnitOfWork(PersonDBContext context)
        {
            _dBContext = context;
            PersonRepository = new PersonRepository(_dBContext);
            ConnecttionRepository = new ConnecttionRepository(_dBContext);
        }

        public async Task Commit()
        {
            await _dBContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dBContext.Dispose();
        }
    }
}
