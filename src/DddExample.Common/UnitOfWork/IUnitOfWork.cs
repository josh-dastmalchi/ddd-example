using System;
using System.Threading.Tasks;

namespace DddExample.Common.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
    }
}
