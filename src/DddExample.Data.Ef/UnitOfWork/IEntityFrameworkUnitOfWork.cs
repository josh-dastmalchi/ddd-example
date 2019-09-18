using DddExample.Common.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DddExample.Data.Ef.UnitOfWork
{
    public interface IEntityFrameworkUnitOfWork<out TDbContext> : IUnitOfWork where TDbContext: DbContext
    {
        TDbContext DbContext { get; }
        IDbContextTransaction Transaction { get; }
        bool IsDisposed { get; }
    }
}
