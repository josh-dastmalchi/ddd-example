using System.Threading;
using System.Threading.Tasks;

namespace DddExample.Common.Cqrs
{
    public interface IQueryHandlerAsync<in TQuery, TReturn> where TQuery : IQuery
    {
        Task<TReturn> HandleAsync(TQuery query, CancellationToken cancellationToken = default(CancellationToken));
    }
}