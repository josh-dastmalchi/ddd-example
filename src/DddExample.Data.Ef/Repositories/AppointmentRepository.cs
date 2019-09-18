using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DddExample.Common.Times;
using DddExample.Data.Ef.UnitOfWork;
using DddExample.Data.Schema;
using DddExample.Domain.Entities.Appointments;
using DddExample.Domain.Entities.Doctors;
using DddExample.Domain.Entities.Patients;
using DddExample.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DddExample.Data.Ef.Repositories
{
    internal class AppointmentRepository : IAppointmentRepository
    {
        private readonly IDbContextProvider<DddExampleDbContext> _dbContextProvider;
        private readonly ISystemClockService _systemClockService;

        public AppointmentRepository(IDbContextProvider<DddExampleDbContext> dbContextProvider, ISystemClockService systemClockService)
        {
            _dbContextProvider = dbContextProvider;
            _systemClockService = systemClockService;
        }

        public async Task<IReadOnlyList<Appointment>> GetAllUpcomingForDoctorAsync(DoctorId doctorId,
            CancellationToken cancellation = default)
        {
            var context = await _dbContextProvider.GetAsync();

            var now = _systemClockService.UtcNow;
            return await context.AppointmentTables
                .Where(x => x.AppointmentAt >= now)
                .Where(x => x.DoctorId == doctorId.Value)
                .Select(x => new Appointment(new AppointmentId(x.AppointmentId), new DoctorId(x.DoctorId), new PatientId(x.PatientId),
                    x.AppointmentAt, new AppointmentDuration(x.Duration)))
                .ToListAsync(cancellation);
        }

        public async Task<IReadOnlyList<Appointment>> GetAllUpcomingForPatientAsync(PatientId patientId,
            CancellationToken cancellation = default)
        {
            var context = await _dbContextProvider.GetAsync();

            var now = _systemClockService.UtcNow;
            return await context.AppointmentTables
                .Where(x => x.AppointmentAt >= now)
                .Where(x => x.PatientId == patientId.Value)
                .Select(x => new Appointment(new AppointmentId(x.AppointmentId), new DoctorId(x.DoctorId), new PatientId(x.PatientId),
                    x.AppointmentAt, new AppointmentDuration(x.Duration)))
                .ToListAsync(cancellation);
        }

        public async Task CreateAsync(Appointment appointment, CancellationToken cancellation = default)
        {
            var appointmentTable = new AppointmentTable
            {
                AppointmentAt = appointment.AppointmentAt,
                DoctorId = appointment.DoctorId.Value,
                PatientId = appointment.PatientId.Value,
                AppointmentId = appointment.AppointmentId.Value,
                Duration = appointment.AppointmentDuration.DurationInMinutes
            };

            var context = await _dbContextProvider.GetAsync();

            context.AppointmentTables.Add(appointmentTable);
            await context.SaveChangesAsync(cancellation);
        }
    }
}