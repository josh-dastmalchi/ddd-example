using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DddExample.Domain.Entities.Appointments;
using DddExample.Domain.Entities.Doctors;
using DddExample.Domain.Entities.Patients;

namespace DddExample.Domain.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IReadOnlyList<Appointment>> GetAllUpcomingForDoctorAsync(DoctorId doctorId, CancellationToken cancellation = default);
        Task<IReadOnlyList<Appointment>> GetAllUpcomingForPatientAsync(PatientId patientId, CancellationToken cancellation = default);
        Task CreateAsync(Appointment appointment, CancellationToken cancellation = default);
    }
}