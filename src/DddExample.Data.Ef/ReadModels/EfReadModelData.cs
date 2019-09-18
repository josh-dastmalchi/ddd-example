using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DddExample.Data.Ef.UnitOfWork;
using DddExample.Data.ReadModels;
using DddExample.Data.Schema;

namespace DddExample.Data.Ef.ReadModels
{
    public class EfReadModelData : IReadModelData
    {
        private readonly IDbContextProvider<DddExampleDbContext> _dbContextProvider;


        public EfReadModelData(IDbContextProvider<DddExampleDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public async Task<IQueryable<AppointmentTable>> GetAppointmentTablesAsync(CancellationToken cancellation = default)
        {
            var context = await _dbContextProvider.GetAsync();
            return context.Set<AppointmentTable>();
        }

        public async Task<IQueryable<DoctorAvailabilityTable>> GetDoctorAvailabilityTablesAsync(CancellationToken cancellation = default)
        {
            var context = await _dbContextProvider.GetAsync();
            return context.Set<DoctorAvailabilityTable>();
        }

        public async Task<IQueryable<DoctorTable>> GetDoctorTablesAsync(CancellationToken cancellation = default)
        {
            var context = await _dbContextProvider.GetAsync();
            return context.Set<DoctorTable>();
        }

        public async Task<IQueryable<PatientTable>> GetPatientTablesAsync(CancellationToken cancellation = default)
        {
            var context = await _dbContextProvider.GetAsync();
            return context.Set<PatientTable>();
        }

    }
}