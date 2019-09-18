using System;

namespace DddExample.Application.Doctors.ReadModels
{
    public class DoctorAvailabilityReadModel
    {
        public Guid DoctorAvailabilityId { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan StartsAt { get; set; }
        public TimeSpan EndsAt { get; set; }
    }
}