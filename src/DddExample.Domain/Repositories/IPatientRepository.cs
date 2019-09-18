using System.Threading;
using System.Threading.Tasks;
using DddExample.Domain.Entities.Patients;

namespace DddExample.Domain.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient> GetByIdAsync(PatientId patientId, CancellationToken cancellation = default);
    }
}
