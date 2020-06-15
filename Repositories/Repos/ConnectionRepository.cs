using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System.Threading.Tasks;

namespace Repositories.Repos
{
    public class ConnecttionRepository : RepositoryBase<PersonConnection>,IConnectionRepository
    {
        private readonly PersonDBContext _dBContext;
        public ConnecttionRepository(PersonDBContext context) : base(context)
        {
            _dBContext = context;
        }

        public void CreateConnection(PersonConnection personConnection)
        {
            Add(personConnection);
        }

        public void DeleteConnection(PersonConnection personConnection)
        {
            Delete(personConnection);
        }
        public async Task<PersonConnection> GetConnectionByConnectedPersonId(int ConnectedPersonId)
        {
            return await GetByCondition(connection => connection.ConnectedPersonId.Equals(ConnectedPersonId)).FirstOrDefaultAsync();
        }
    }
}
