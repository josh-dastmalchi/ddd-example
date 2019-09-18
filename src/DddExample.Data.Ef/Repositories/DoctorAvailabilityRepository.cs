using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DddExample.Data.Ef.UnitOfWork;
using DddExample.Domain.Entities;
using DddExample.Domain.Entities.Doctors;
using DddExample.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DddExample.Data.Ef.Repositories
{
    internal class DoctorAvailabilityRepository : IDoctorAvailabilityRepository
    {
        private readonly IDbContextProvider<DddExampleDbContext> _dbContextProvider;

        public DoctorAvailabilityRepository(IDbContextProvider<DddExampleDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public async Task<IReadOnlyList<DoctorAvailability>> GetAllForDoctorAsync(DoctorId doctorId,
            CancellationToken cancellationToken = default)
        {
            var context = await _dbContextProvider.GetAsync();

            return await context.DoctorAvailabilityTables
                .Where(x => x.DoctorId == doctorId.Value)
                .Select(x => new DoctorAvailability(new DoctorAvailabilityId(x.DoctorAvailabilityId), new DoctorId(x.DoctorId), x.DayOfWeek,
                    new TimeOfDay(x.StartsAt), new TimeOfDay(x.EndsAt)))
                .ToListAsync(cancellationToken);
        }
    }
}