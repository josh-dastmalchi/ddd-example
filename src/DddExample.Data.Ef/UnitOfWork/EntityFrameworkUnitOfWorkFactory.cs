using System;
using System.Threading.Tasks;
using DddExample.Common.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DddExample.Data.Ef.UnitOfWork
{
    public class EntityFrameworkUnitOfWorkFactory<TDbContext> : 
        IDbContextProvider<TDbContext>,
        IUnitOfWorkFactory
        where TDbContext : DbContext
    {
        private readonly IServiceProvider _serviceProvider;
        private IEntityFrameworkUnitOfWork<TDbContext> _unitOfWork;

        public EntityFrameworkUnitOfWorkFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<IUnitOfWork> CreateAsync()
        {
            // If there's an existing, undisposed unit of work, we're trying to nest units of work. Not supported.
            if (ActiveUnitOfWorkExists())
            {
                throw new Exception(
                    "Cannot create unit of work: A unit of work was created before the previous unit of work was complete.");
            }

            var dbContext = _serviceProvider.GetRequiredService<TDbContext>();
            _unitOfWork = new EntityFrameworkUnitOfWork<TDbContext>(dbContext);
            return Task.FromResult<IUnitOfWork>(_unitOfWork);
        }

        public async Task<IEntityFrameworkUnitOfWork<TDbContext>> GetOrCreateAsync()
        {
            if (ActiveUnitOfWorkExists())
            {
                return _unitOfWork;
            }

            return (IEntityFrameworkUnitOfWork<TDbContext>) await CreateAsync();
        }

        private bool ActiveUnitOfWorkExists()
        {
            return _unitOfWork != null && _unitOfWork.IsDisposed == false;
        }

        public Task<TDbContext> GetAsync()
        {
            if (ActiveUnitOfWorkExists())
            {
                return Task.FromResult(_unitOfWork.DbContext);
            }

            return Task.FromResult(_serviceProvider.GetRequiredService<TDbContext>());
        }
    }

    public class EntityFrameworkUnitOfWorkFactory<TDbContext, TUnitOfWork> : IDbContextProvider<TDbContext>,
        IUnitOfWorkFactory<TUnitOfWork>
        where TDbContext : DbContext
        where TUnitOfWork : IEntityFrameworkUnitOfWork<TDbContext>
    {
        private readonly IServiceProvider _serviceProvider;
        private IEntityFrameworkUnitOfWork<TDbContext> _unitOfWork;

        public EntityFrameworkUnitOfWorkFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TUnitOfWork> CreateAsync()
        {
            // If there's an existing, undisposed unit of work, we're trying to nest units of work. Not supported.
            if (ActiveUnitOfWorkExists())
            {
                throw new Exception(
                    "Cannot create unit of work: A unit of work was created before the previous unit of work was complete.");
            }

            var dbContext = _serviceProvider.GetRequiredService<TDbContext>();
            _unitOfWork = new EntityFrameworkUnitOfWork<TDbContext>(dbContext);
            return Task.FromResult((TUnitOfWork)_unitOfWork);
        }

        private bool ActiveUnitOfWorkExists()
        {
            return _unitOfWork != null && _unitOfWork.IsDisposed == false;
        }

        public Task<TDbContext> GetAsync()
        {
            if (ActiveUnitOfWorkExists())
            {
                return Task.FromResult(_unitOfWork.DbContext);
            }

            return Task.FromResult(_serviceProvider.GetRequiredService<TDbContext>());
        }
    }
}