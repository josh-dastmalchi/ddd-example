using System;
using DddExample.Domain.Entities.Doctors;
using DddExample.Domain.Entities.Patients;

namespace DddExample.Domain.Entities.Appointments
{
    public class Appointment : AggregateBase
    {
        public Appointment(AppointmentId appointmentId, DoctorId doctorId, PatientId patientId, DateTime appointmentAt, AppointmentDuration appointmentDuration)
        {
            AppointmentId = appointmentId;
            DoctorId = doctorId;
            PatientId = patientId;
            AppointmentAt = appointmentAt;
            AppointmentDuration = appointmentDuration;
        }

        public AppointmentId AppointmentId { get; }
        public DoctorId DoctorId { get; }
        public PatientId PatientId { get; }
        public DateTime AppointmentAt { get; }
        public AppointmentDuration AppointmentDuration { get; }
    }
}