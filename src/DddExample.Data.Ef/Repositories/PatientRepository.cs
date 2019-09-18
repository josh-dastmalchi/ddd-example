using System.Threading;
using System.Threading.Tasks;
using DddExample.Data.Ef.UnitOfWork;
using DddExample.Domain.Entities.Patients;
using DddExample.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DddExample.Data.Ef.Repositories
{
    internal class PatientRepository : IPatientRepository
    {
        private readonly IDbContextProvider<DddExampleDbContext> _dbContextProvider;

        public PatientRepository(IDbContextProvider<DddExampleDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }


        public async Task<Patient> GetByIdAsync(PatientId patientId, CancellationToken cancellation = default)
        {
            var context = await _dbContextProvider.GetAsync();
            var patientTable = await context.PatientTables.SingleOrDefaultAsync(x => x.PatientId == patientId.Value, cancellation);
            if (patientTable == null)
            {
                return null;
            }

            return new Patient(new PatientId(patientTable.PatientId), patientTable.Name);
        }
    }
}