using System;
using DddExample.Common.Cqrs;

namespace DddExample.Application.Appointments
{
    public class ScheduleAppointmentCommand : ICommand
    {
        public ScheduleAppointmentCommand(Guid appointmentId, Guid doctorId,Guid patientId,DateTime appointmentAt, int appointmentDurationInMinutes )
        {
            AppointmentId = appointmentId;
            DoctorId = doctorId;
            PatientId = patientId;
            AppointmentAt = appointmentAt;
            AppointmentDurationInMinutes = appointmentDurationInMinutes;
        }

        public Guid AppointmentId { get; }
        public Guid DoctorId { get; }
        public Guid PatientId { get; }
        public DateTime AppointmentAt { get; }
        public int AppointmentDurationInMinutes { get; }
    }
}