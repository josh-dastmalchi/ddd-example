using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DddExample.Domain.Entities.Appointments;
using DddExample.Domain.Entities.Doctors;
using DddExample.Domain.Entities.Patients;

namespace DddExample.Api.Api.Appointments.Requests
{
    public class ScheduleAppointmentRequest
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime AppointmentAt { get; set; }
        public int AppointmentDurationInMinutes { get; set; }
    }
}