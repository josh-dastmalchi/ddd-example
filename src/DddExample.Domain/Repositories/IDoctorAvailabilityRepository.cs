using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DddExample.Domain.Entities.Doctors;

namespace DddExample.Domain.Repositories
{
    public interface IDoctorAvailabilityRepository
    {
        Task<IReadOnlyList<DoctorAvailability>> GetAllForDoctorAsync(DoctorId doctorId, CancellationToken cancellationToken = default);
    }
}