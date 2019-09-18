using System.Threading;
using System.Threading.Tasks;
using DddExample.Domain.Entities.Doctors;

namespace DddExample.Domain.Repositories
{
    public interface IDoctorRepository
    {
        Task<Doctor> GetByIdAsync(DoctorId doctorId, CancellationToken cancellation = default);
    }
}