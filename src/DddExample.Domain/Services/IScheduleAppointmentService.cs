using System;
using System.Threading;
using System.Threading.Tasks;
using DddExample.Domain.Entities.Appointments;
using DddExample.Domain.Entities.Doctors;
using DddExample.Domain.Entities.Patients;

namespace DddExample.Domain.Services
{
    public interface IScheduleAppointmentService
    {
        Task ScheduleAsync(AppointmentId appointmentId, DoctorId doctorId, PatientId patientId, DateTime requestedAppointmentAt, AppointmentDuration requestedAppointmentDuration,
            CancellationToken cancellationToken = default);
    }
}