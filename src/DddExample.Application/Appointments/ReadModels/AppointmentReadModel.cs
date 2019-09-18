using System;

namespace DddExample.Application.Appointments.ReadModels
{
    public class AppointmentReadModel
    {
        public Guid AppointmentId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime AppointmentAt { get; set; }
        public int AppointmentDurationInMinutes { get; set; }
    }
}