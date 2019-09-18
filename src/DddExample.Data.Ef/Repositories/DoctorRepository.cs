using System.Threading;
using System.Threading.Tasks;
using DddExample.Data.Ef.UnitOfWork;
using DddExample.Domain.Entities.Doctors;
using DddExample.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DddExample.Data.Ef.Repositories
{
    internal class DoctorRepository : IDoctorRepository
    {
        private readonly IDbContextProvider<DddExampleDbContext> _dbContextProvider;

        public DoctorRepository(IDbContextProvider<DddExampleDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public async Task<Doctor> GetByIdAsync(DoctorId doctorId, CancellationToken cancellation = default)
        {
            var context = await _dbContextProvider.GetAsync();
            var doctorTable = await context.DoctorTables.SingleOrDefaultAsync(x => x.DoctorId == doctorId.Value, cancellation);
            if (doctorTable == null)
            {
                return null;
            }

            return new Doctor(new DoctorId(doctorTable.DoctorId), doctorTable.Name);
        }
    }
}