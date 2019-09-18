using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DddExample.Data.Schema;

namespace DddExample.Data.ReadModels
{
    public interface IReadModelData
    {
        Task<IQueryable<AppointmentTable>> GetAppointmentTablesAsync(CancellationToken cancellation = default);
        Task<IQueryable<DoctorAvailabilityTable>> GetDoctorAvailabilityTablesAsync(CancellationToken cancellation = default);
        Task<IQueryable<DoctorTable>> GetDoctorTablesAsync(CancellationToken cancellation = default);
        Task<IQueryable<PatientTable>> GetPatientTablesAsync(CancellationToken cancellation = default);
    }
}
