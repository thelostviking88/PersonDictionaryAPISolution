using Data.Models;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IConnectionRepository : IRepositoryBase<PersonConnection>
    {
        void CreateConnection(PersonConnection personConnection);
        void DeleteConnection(PersonConnection personConnection);
        Task<PersonConnection> GetConnectionByConnectedPersonId(int ConnectedPersonId);
    }
}
