using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DddExample.Data.Ef.UnitOfWork
{
    internal class EntityFrameworkUnitOfWork<TDbContext> : IEntityFrameworkUnitOfWork<TDbContext> where TDbContext: DbContext
    {
        public EntityFrameworkUnitOfWork(TDbContext dbContext)
        {
            DbContext = dbContext;
            Transaction = DbContext.Database.BeginTransaction();
        }

        public TDbContext DbContext { get; }
        public IDbContextTransaction Transaction { get; }
        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            Transaction?.Dispose();
            DbContext?.Dispose();
            IsDisposed = true;
        }

        public Task CommitAsync()
        {
            Transaction.Commit();
            return Task.CompletedTask;
        }



    }
}