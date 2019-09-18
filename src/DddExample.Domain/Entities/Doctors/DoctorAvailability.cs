using System;
using DddExample.Domain.Entities.Appointments;

namespace DddExample.Domain.Entities.Doctors
{
    public class DoctorAvailability : AggregateBase
    {
        public DoctorAvailability(DoctorAvailabilityId doctorAvailabilityId, DoctorId doctorId, DayOfWeek dayOfWeek, TimeOfDay startsAt, TimeOfDay endsAt)
        {
            DoctorAvailabilityId = doctorAvailabilityId;
            DoctorId = doctorId;
            DayOfWeek = dayOfWeek;
            StartsAt = startsAt;
            EndsAt = endsAt;
        }

        public DoctorAvailabilityId DoctorAvailabilityId { get; }
        public DoctorId DoctorId { get; }
        public DayOfWeek DayOfWeek { get; }
        public TimeOfDay StartsAt { get; }
        public TimeOfDay EndsAt { get; }
    }
}