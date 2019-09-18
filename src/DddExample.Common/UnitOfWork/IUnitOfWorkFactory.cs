using System.Threading.Tasks;

namespace DddExample.Common.UnitOfWork
{
    public interface IUnitOfWorkFactory
    {
        Task<IUnitOfWork> CreateAsync();
    }

    public interface IUnitOfWorkFactory<T> where T : IUnitOfWork
    {
        Task<T> CreateAsync();
    }
}
