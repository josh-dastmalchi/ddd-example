using System;

namespace DddExample.Data.Schema
{
    public class AppointmentTable
    {
        public Guid AppointmentId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime AppointmentAt { get; set; }
        public int Duration { get; set; }
    }
}