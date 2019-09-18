using System.Threading;
using System.Threading.Tasks;

namespace DddExample.Common.Cqrs
{
    // Prefer this interface: Commands should not return data
    public interface ICommandHandlerAsync<in TCommand> where TCommand: ICommand
    {
        Task HandleAsync(TCommand command, CancellationToken cancellationToken = default(CancellationToken));
    }

    public interface ICommandHandlerAsync<in TCommand, TReturn> where TCommand : ICommand
    {
        Task<TReturn> HandleAsync(TCommand command, CancellationToken cancellationToken = default(CancellationToken));
    }
}