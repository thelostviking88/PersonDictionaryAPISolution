using System;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public  interface IUnitOfWork : IDisposable
    {
        Task Commit();
    }
}
